using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealityCS.Core.Helper;
using RealityCS.Core.Infrastructure;
using RealityCS.DataLayer;
using RealityCS.DataLayer.Context.BaseContext;
using RealityCS.DataLayer.Context.RealitycsClient;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
using RealityCS.DTO.RealitycsClient;
using RealityCS.SharedMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.Customer
{

    public class ClientUser : IClientUser
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<User> userRepository;
        private readonly IGenericRepository<MasterAccessGroup> accessGroupRepository;
        private readonly IGenericRepository<MasterAccessOperation> accessOperationepository;
        private readonly IGenericRepository<RealitycsUserPassword> passwordRepository;
        private readonly IWorkContext workContext;
        private readonly IWebHelper webHelper;
        private readonly IRealitycsDataProvider dataProvider;
        private readonly RealitycsClientContext clientContext;

        public ClientUser(IMapper mapper,
            IGenericRepository<User> userRepository, IGenericRepository<RealitycsUserPassword> passwordRepository,
            IGenericRepository<MasterAccessGroup> accessGroupRepository,
            IGenericRepository<MasterAccessOperation> accessOperationepository
            , IWorkContext workContext, IWebHelper webHelper,
            IRealitycsDataProvider dataProvider,
            RealitycsClientContext clientContext
            )
        {
            _mapper = mapper;
            this.userRepository = userRepository;
            this.passwordRepository = passwordRepository;
            this.accessGroupRepository = accessGroupRepository;
            this.accessOperationepository = accessOperationepository;
            this.workContext = workContext;
            this.webHelper = webHelper;
            this.dataProvider = dataProvider;
            this.clientContext = clientContext;
        }



        public bool IsRegistered(User user)
        {
            return userRepository.Find(x => x.EmailId == user.EmailId) != null ? true : false;
        }
        public async Task<int> InsertUserWithPassword(User request)
        {
            var passwords = request.Passwords;
            request.Passwords = null;
            var result = await userRepository.InsertAsync(request, true);
            passwords.ToList().ForEach(x => x.UserId = request.PK_Id);
            foreach (var item in passwords)
            {
                await InsertCustomerPassword(item);
            }
            return request.PK_Id;
        }
        /// <summary>
        /// Get User for Current LegalEntity
        /// </summary>
        /// <returns></returns>
        public async Task<List<ClientUserDTO>> GetUsers()
        {

            try
            {
                //prepare input parameters
                var pLegalEntityId = dataProvider.GetInt32Parameter("LegalEntityId", workContext.CurrentCustomer.FK_LegalEntityId);
                var pSearch = dataProvider.GetStringParameter("Search", null);
                var pPageIndex = dataProvider.GetInt32Parameter("PageIndex", 1);
                var pPageSize = dataProvider.GetInt32Parameter("pageSize", 100);
                var pSortColumn = dataProvider.GetStringParameter("SortColumn", null);
                var pSortOrder = dataProvider.GetStringParameter("SortOrder", null);

                //invoke stored procedure
                var spResult = clientContext.QueryFromSql<ViewUserList>("client.usp_AllUsers",
                        pLegalEntityId, pSearch, pPageIndex, pPageSize, pSortColumn, pSortOrder).ToList();

                var result = _mapper.Map<List<ClientUserDTO>>(spResult);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        //public async Task<ClientUserDto> GetById(int id)
        //{
        //    var result = new List<ClientUserDto>();
        //    var row = await _context.ClientUsers.FirstOrDefaultAsync(x => x.PK_Id == id);
        //    if (row != null)
        //    {
        //        return new ClientUserDto()
        //        {
        //            Id = row.PK_Id,
        //            UserName = row.UserName,
        //            EmailId = row.EmailId,
        //            LegalEntityId = row.FK_LegalEntityId,
        //            EmplooyeId = row.FK_EmployeeId,
        //            IsActive = row.IsActive
        //        };
        //    }
        //    return null;
        //}

        public async Task<int> UpdateUser(User user)
        {
            if (user == null)
                throw new RealitycsException("User Not NUll");
            await userRepository.UpdateAsync(user, true);
            return user.PK_Id;
        }

        protected async Task InActiveAllPreviousPassword(int userId)
        {
            var allActivePassword = await passwordRepository.FindAllAsync(x => x.UserId == userId && x.IsActive);
            if (allActivePassword != null && allActivePassword.Count() > 0)
            {
                allActivePassword.ToList().ForEach(x => x.IsActive = false);
                await passwordRepository.UpdateAsync(allActivePassword, true);
            }
        }

        /// <summary>
        /// Insert a customer password
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        public virtual async Task InsertCustomerPassword(RealitycsUserPassword customerPassword)
        {
            await InActiveAllPreviousPassword(customerPassword.UserId);
            await passwordRepository.InsertAsync(customerPassword, true);
        }

        public async Task<RealitycsUserPassword> GetCurrentPassword(int userId)
        {
            if (userId == 0)
                throw new RealitycsException("User Not NUll");
            var user = await userRepository.FindAsync(x => x.PK_Id == userId);
            if (userId == 0)
                throw new RealitycsException("Wrong UserId");

            var currentPassword = user.Passwords.OrderBy(x => x.CreatedDate).Where(x => x.IsActive).FirstOrDefault();
            return currentPassword;
        }

        public async Task<User> GetUser(string userName, int legalEntityId = 0)
        {
            var result = new List<ClientUserDTO>();
            User user = null;
            if (legalEntityId == 0)
            {
                user = await userRepository.FindAsync(x => x.EmailId == userName || x.UserName == userName); //SB|29-12-2020|allow login by user name or email id
            }
            else
            {
                user = await userRepository.FindAsync(x => x.FK_LegalEntityId == legalEntityId && (x.EmailId == userName || x.UserName == userName));//SB|29-12-2020| allow login by user name or email id
            }
            if (user != null)
            {
                return user;
            }
            return null;
        }
/** No longer in use - SB|29.12.2020
        public async Task<ClientUserDTO> GetByEmail(string emailId, int legalEntityId)
        {
            var result = new List<ClientUserDTO>();
            var user = await userRepository.FindAsync(x => x.EmailId == emailId && x.FK_LegalEntityId == legalEntityId);
            if (user != null)
            {
                return new ClientUserDTO()
                {
                    Id = user.PK_Id,
                    UserName = user.UserName,
                    EmailId = user.EmailId,
                    LegalEntityId = user.FK_LegalEntityId,
                    EmployeeId = user.FK_EmployeeId,
                    IsActive = user.IsActive
                };
            }
            return null;
        }
*/
        /// <summary>
        /// Register New User in system 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> SaveUser(MangeClientUserDTO user)
        {
            var userRegistrationService = RealitycsEngineContext.Current.Resolve<IRealitycsUserRegistrationService>();
            User newUser = new User()
            {
                EmailId = user.EmailId,
                UserName = user.UserName,
                CreatedBy = workContext.CurrentCustomer.PK_Id,
                IsActive = true,
                FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                FK_AccessGroupId = user.AccessGroupId, //AccessOperation
                CreatedDate = DateTime.UtcNow,
                Password = user.Password,
                LastIpAddress = webHelper.GetCurrentIpAddress(),

            };

            var results =
                await userRegistrationService.RegisterLegalEntityUser(
                    new DTO.RealitycsClient.CustomerRegistrationRequest(
                        newUser, newUser.EmailId, newUser.UserName, newUser.Password, PasswordFormat.Hashed)
                    { });

            return 1;
        }

        public async Task<bool> UpdateUser(MangeClientUserDTO user)
        {
            try
            {
                var userDetail = userRepository.Find(x => x.PK_Id == user.id && x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId);
                userDetail.EmailId = user.EmailId;
                userDetail.FK_AccessGroupId = user.AccessGroupId;
                userDetail.UserName = user.UserName;
                await userRepository.UpdateAsync(userDetail,true);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<AccessGroupDTO>> AccessGroups()
        {

            var aceesGroups = await (from accessGrp in accessGroupRepository.Table
                                     where accessGrp.LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId || accessGrp.LegalEntityId == 0
                                     select new AccessGroupDTO
                                     {
                                         Id = accessGrp.PK_Id,
                                         Name = accessGrp.Name,
                                         Description = accessGrp.Description,
                                         IsActive = accessGrp.IsActive
                                     }).ToListAsync();
            return aceesGroups;
        }

        public async Task<List<AccessOperationDTO>> MasterAccessOperation()
        {
            var masterOperation = await accessOperationepository.Table.Select(x => new AccessOperationDTO()
            {
                Id = x.PK_Id,
                DomainName = x.Domain.Name,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive ? "Active" : "DeActive",
                EntityGroupName = x.EntityGroup.Name,
                EntityName = x.Entity.Name
            }).ToListAsync();
            return masterOperation;
        }


        public async Task<EditAccessGroupDTO> GetAccessGroup(int id)
        {
            var accessGroupAndOperationRelationRepository = RealitycsEngineContext.Current.Resolve<IGenericRepository<AccessGroupAndOperationRelation>>();
            var accessGroups = await accessGroupRepository.FindAsync(x => x.PK_Id == id);

            var operations = (from op in accessOperationepository.Table
                              join opm in accessGroupAndOperationRelationRepository.Table on op.PK_Id equals opm.AccessOperationId
                              where opm == null || opm.AccessGroupId == id
                              select new AccessOperationDTO
                              {
                                  Id = op.PK_Id,
                                  Description = op.Description,
                                  DomainName = op.Domain.Name,
                                  EntityGroupName = op.EntityGroup.Name,
                                  EntityName = op.Entity.Name,
                                  Name = op.Name,
                                  IsActive = op.IsActive ? "Active" : "DeActive",
                                  Assigned = opm != null ? true : false
                              }).ToList();

            EditAccessGroupDTO result = new EditAccessGroupDTO()
            {
                Id = accessGroups.PK_Id,
                Name = accessGroups.Name,
                Description = accessGroups.Description,
                IsActive = accessGroups.IsActive,
                operations = operations
            };
            return result;
        }


        public async Task<ClientUserDTO> GetUserDetails(int id)
        {
            var user = await userRepository.FindAsync(x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId && x.PK_Id == id);
            int accessGroupId = accessGroupRepository.Find(x => x.PK_Id == user.FK_AccessGroupId).PK_Id;
            var response = new ClientUserDTO()
            {
                Id = user.PK_Id,
                UserName = user.UserName,
                EmailId = user.EmailId,
                LegalEntityId = user.FK_LegalEntityId,
                EmployeeId = user.FK_EmployeeId,
                IsActive = user.IsActive,
                AccessGroupId = accessGroupId,
            };

            return response;
        }

        public async Task<int> SaveAccessGroup(ManageAccessGroupDTO accessGroup )
        {

            var aceessOperationMap = new List<AccessGroupAndOperationRelation>();
            MasterAccessGroup newGroup = new MasterAccessGroup()
            {
                Name = accessGroup.Name,
                Description = accessGroup.Description,
                CreatedBy = workContext.CurrentCustomer.PK_Id,
                IsActive = true,
                LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                CreatedDate = DateTime.UtcNow,

            };
           
            foreach (int operationId in accessGroup.Operations)
                aceessOperationMap.Add(new AccessGroupAndOperationRelation() { IsActive = true, AccessGroupId = newGroup.PK_Id, AccessOperationId = operationId, CreatedDate = DateTime.UtcNow, });
            newGroup.AccessGroupAndOperationRelation = aceessOperationMap;

            await accessGroupRepository.InsertAsync(newGroup, true);
            return 1;

            //public async Task ChangePassword()
            //{ }
        }

        public async Task<int> UpdateAccessGroup(ManageAccessGroupDTO accessGroup)
        {
            var accessOperationRepository = RealitycsEngineContext.Current.Resolve<IGenericRepository<AccessGroupAndOperationRelation>>();
            var accesGroupResult = accessGroupRepository.GetById(accessGroup.Id);
            if (accesGroupResult != null)
            {
                accesGroupResult.Name = accessGroup.Name;
                accesGroupResult.Description = accessGroup.Description;
                accesGroupResult.ModifiedBy = workContext.CurrentCustomer.PK_Id;
                accesGroupResult.ModifiedDate = DateTime.UtcNow;

                var allOperations = accesGroupResult.AccessGroupAndOperationRelation;

                foreach(var operation in allOperations)
                {
                    if (!accessGroup.Operations.Contains(operation.AccessOperationId))
                    {
                        //Delete
                        accessOperationRepository.Delete(operation);
                    }
                    else
                    {
                        var item = accessGroup.Operations.Where(x => x == operation.AccessOperationId).FirstOrDefault();
                        accessGroup.Operations.Remove(item);
                    }
                }
                var aceessOperationMap = new List<AccessGroupAndOperationRelation>();
                //All Left Id
                foreach (int operationId in accessGroup.Operations)
                    aceessOperationMap.Add(new AccessGroupAndOperationRelation() { IsActive = true, AccessGroupId = accesGroupResult.PK_Id, AccessOperationId = operationId, CreatedDate = DateTime.UtcNow });
                accesGroupResult.AccessGroupAndOperationRelation = aceessOperationMap;

                await accessGroupRepository.UpdateAsync(accesGroupResult, true);
                return 1;
            }
            else
                 throw new RealitycsException("Access Group Not NUll"); ;

            //public async Task ChangePassword()
            //{ }
        }
    }
}
