using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealityCS.BusinessLogic;
using RealityCS.BusinessLogic.Customer;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using RealityCS.DTO.Login;
using RealityCS.SharedMethods;

namespace RealityCS.Api.Controllers.Common
{
    public class AccountController : CommonControllerBase
    {
        private readonly IRealitycsUserRegistrationService userRegistrationService;
        private readonly IWorkContext workContext;
        private readonly ITokenManager tokenManager;
        private readonly IClientUser customerService;
        public AccountController(IRealitycsUserRegistrationService userRegistrationService, IWorkContext workContext, ITokenManager tokenManager, IClientUser customerService)
        {
            this.userRegistrationService = userRegistrationService;
            this.workContext = workContext;
            this.tokenManager= tokenManager;
            this.customerService = customerService;
        }

        [HttpPost] 
        public async Task<IActionResult> Login(LoginRequest request)
        {

            try
            {
                
                User loggedInUser = null;
                UserLoginResults loginResult = await userRegistrationService.ValidateLegalEntityUser(request.username, request.password);
                switch (loginResult)
                {
                    case UserLoginResults.Successful:
                    {

                        loggedInUser = await customerService.GetUser(request.username);

                        workContext.CurrentCustomer = loggedInUser;

                        LoginResponse loginResultModel = await CreateLoginResult(loggedInUser);

                         
                        return Ok(loginResultModel);
                    }
                    case UserLoginResults.CustomerNotExist:
                        ModelState.AddModelError("", "WrongCredentials");
                        break;
                    case UserLoginResults.Deleted:
                        ModelState.AddModelError("", "User Deleted");
                        break;
                    case UserLoginResults.NotActive:
                        ModelState.AddModelError("", "NotActive");
                        break;
                    case UserLoginResults.LockedOut:
                        ModelState.AddModelError("", "CommunityUser is locked out use ForgotPassword link to unlock your account.");
                        break;
                    case UserLoginResults.WrongPassword:
                    default:
                        ModelState.AddModelError("", "Wrong Credentials");
                        break;
                }

                return BadRequest(ModelState);
            }
            catch (RealitycsException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }

        }

        private async Task<LoginResponse> CreateLoginResult(User loggedInUser)
        {

            var token =await tokenManager.GenarateToken(loggedInUser.EmailId, loggedInUser.FK_LegalEntityId);
            return new LoginResponse() { access_token = token };
        }

       
    }


}
