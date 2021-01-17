using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealityCS.BusinessLogic.Customer;
using RealityCS.BusinessLogic.Security;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using RealityCS.DTO.RealitycsClient;
using RealityCS.SharedMethods;
using static RealityCS.SharedMethods.RealitycsConstants;

namespace RealityCS.BusinessLogic.Customer
{
    /// <summary>
    /// Customer registration service
    /// </summary>
    public partial class RealitycsUserRegistrationService : IRealitycsUserRegistrationService
    {
        #region Fields

        private readonly IClientUser legalEntityUserService;
        private readonly IEncryptionService _encryptionService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public RealitycsUserRegistrationService(
            IClientUser customerService,
            IEncryptionService encryptionService,
            IWorkContext workContext)
        {
            this.legalEntityUserService = customerService;
            _encryptionService = encryptionService;
            _workContext = workContext;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Check whether the entered password matches with a saved one
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        /// <param name="enteredPassword">The entered password</param>
        /// <returns>True if passwords match; otherwise false</returns>
        protected bool PasswordsMatch(RealitycsUserPassword customerPassword, string enteredPassword)
        {
            if (customerPassword == null || string.IsNullOrEmpty(enteredPassword))
                return false;

            var savedPassword = string.Empty;
            switch (customerPassword.PasswordFormat)
            {
                case PasswordFormat.Clear:
                    savedPassword = enteredPassword;
                    break;
                case PasswordFormat.Encrypted:
                    savedPassword = _encryptionService.EncryptText(enteredPassword);
                    break;
                case PasswordFormat.Hashed:
                    savedPassword = _encryptionService.CreatePasswordHash(enteredPassword, customerPassword.PasswordSalt, CustomerSettings.HashedPasswordFormat);
                    break;
            }

            if (customerPassword.Password == null)
                return false;

            return customerPassword.Password.Equals(savedPassword);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validate customer
        /// </summary>
        /// <param name="usernameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        public virtual async Task<UserLoginResults> ValidateLegalEntityUser(string usernameOrEmail, string password)
        {
            var customer =  await legalEntityUserService.GetUser(usernameOrEmail);

            if (customer == null)
                return UserLoginResults.CustomerNotExist;
            if (customer.IsDeleted)
                return UserLoginResults.Deleted;
            if (!customer.IsActive)
                return UserLoginResults.NotActive;
            //only registered can login
            //if (!customerService.IsRegistered(customer))
            //    return UserLoginResults.NotRegistered;
            //check whether a customer is locked out
            if (customer.CannotLoginUntilDateUtc.HasValue && customer.CannotLoginUntilDateUtc.Value > DateTime.UtcNow)
                return UserLoginResults.LockedOut;

            if (!PasswordsMatch(await legalEntityUserService.GetCurrentPassword(customer.PK_Id), password))
            {
                //wrong password
                customer.FailedLoginAttempts++;
                if (CustomerSettings.FailedPasswordAllowedAttempts > 0 &&
                    customer.FailedLoginAttempts >= CustomerSettings.FailedPasswordAllowedAttempts)
                {
                    //lock out
                    customer.CannotLoginUntilDateUtc = DateTime.UtcNow.AddMinutes(CustomerSettings.FailedPasswordLockoutMinutes);
                    //reset the counter
                    customer.FailedLoginAttempts = 0;
                }

                await legalEntityUserService.UpdateUser(customer);

                return UserLoginResults.WrongPassword;
            }

            //update login details
            customer.FailedLoginAttempts = 0;
            customer.CannotLoginUntilDateUtc = null;
            customer.RequireReLogin = false;
            customer.LastLoginDateUtc = DateTime.UtcNow;
            await legalEntityUserService.UpdateUser(customer);

            return UserLoginResults.Successful;
        }

        /// <summary>
        /// Register customer
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        public virtual async Task<CustomerRegistrationResult> RegisterLegalEntityUser(CustomerRegistrationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Customer == null)
                throw new ArgumentException("Can't load current customer");

            var result = new CustomerRegistrationResult();

            if (legalEntityUserService.IsRegistered(request.Customer))
            {
                result.AddError("Current customer is already registered");
                return result;
            }

            if (string.IsNullOrEmpty(request.Email))
            {
                result.AddError("Email Is Not Provided");
                return result;
            }

            if (!RealitycsCommonHelper.IsValidEmail(request.Email))
            {
                result.AddError("WrongEmail");
                return result;
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                result.AddError("Password Is Not Provided");
                return result;
            }

            int FK_LegalEntityId = _workContext.CurrentCustomer == null ? request.Customer.FK_LegalEntityId : _workContext.CurrentCustomer.FK_LegalEntityId;
            //validate unique user
            if (await legalEntityUserService.GetUser(request.Email, FK_LegalEntityId) != null)
            {
                result.AddError("Email Already Exists");
                return result;
            }

            //at this point request is valid
            request.Customer.EmailId = request.Email;

            var customerPassword = new RealitycsUserPassword
            {
                UserId = request.Customer.PK_Id,
                PasswordFormat = request.PasswordFormat,
                CreatedDate = DateTime.UtcNow,
                IsActive=true
            };
            switch (request.PasswordFormat)
            {
                case PasswordFormat.Clear:
                    customerPassword.Password = request.Password;
                    break;
                case PasswordFormat.Encrypted:
                    customerPassword.Password = _encryptionService.EncryptText(request.Password);
                    break;
                case PasswordFormat.Hashed:
                    var saltKey = _encryptionService.CreateSaltKey(PasswordSaltKeySize);
                    customerPassword.PasswordSalt = saltKey;
                    customerPassword.Password = _encryptionService.CreatePasswordHash(request.Password, saltKey, CustomerSettings.HashedPasswordFormat);
                    break;
            }
            List<RealitycsUserPassword> passwords = new List<RealitycsUserPassword>() { customerPassword };
            request.Customer.Password = request.Password;
            request.Customer.Passwords=passwords;
            await legalEntityUserService.InsertUserWithPassword(request.Customer);

            request.Customer.IsActive = request.IsApproved;

            await legalEntityUserService.UpdateUser(request.Customer);

            return result;
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        public virtual async Task<ChangePasswordResult> ChangePassword(ChangePasswordRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var result = new ChangePasswordResult();
            int legalEntityId=0;
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                result.AddError("Email Is Not Provided");
                return result;
            }

            if (string.IsNullOrWhiteSpace(request.NewPassword))
            {
                result.AddError("Password Is  NotProvided");
                return result;
            }

            var customer = await legalEntityUserService.GetUser(request.Email, legalEntityId);
            if (customer == null)
            {
                result.AddError("Email Not Found");
                return result;
            }

            //request isn't valid
            if (request.ValidateRequest && !PasswordsMatch(await legalEntityUserService.GetCurrentPassword(customer.PK_Id), request.OldPassword))
            {
                result.AddError("Old Password Doesnt Match");
                return result;
            }

            //check for duplicates
            //if (CustomerSettings.UnduplicatedPasswordsNumber > 0)
            //{
            //    //get some of previous passwords
            //    var previousPasswords = customerService.GetCurrentPassword(customer.Id);

            //    var newPasswordMatchesWithPrevious = previousPasswords.Any(password => PasswordsMatch(password, request.NewPassword));
            //    if (newPasswordMatchesWithPrevious)
            //    {
            //        result.AddError("Password Matches With Previous"));
            //        return result;
            //    }
            //}

            //at this point request is valid
            var realitycsUserPassword = new RealitycsUserPassword
            {
                UserId = customer.PK_Id,
                PasswordFormat = request.NewPasswordFormat,
                CreatedDate = DateTime.UtcNow
            };
            switch (request.NewPasswordFormat)
            {
                case PasswordFormat.Clear:
                    realitycsUserPassword.Password = request.NewPassword;
                    break;
                case PasswordFormat.Encrypted:
                    realitycsUserPassword.Password = _encryptionService.EncryptText(request.NewPassword);
                    break;
                case PasswordFormat.Hashed:
                    var saltKey = _encryptionService.CreateSaltKey(PasswordSaltKeySize);
                    realitycsUserPassword.PasswordSalt = saltKey;
                    realitycsUserPassword.Password = _encryptionService.CreatePasswordHash(request.NewPassword, saltKey,
                        request.HashedPasswordFormat ?? CustomerSettings.HashedPasswordFormat);
                    break;
            }

            await legalEntityUserService.InsertCustomerPassword(realitycsUserPassword);

            return result;
        }

        #endregion
    }
}