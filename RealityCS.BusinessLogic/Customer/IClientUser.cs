using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using RealityCS.DTO.RealitycsClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.BusinessLogic.Customer
{
    public interface IClientUser
    {
        //Task<int> Add(MangeClientUserDto request);
        //Task<List<ClientUserDto>> GeUsers();
        //Task<ClientUserDto> GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> InsertUserWithPassword(User request);


        //Task<ClientUserDTO> GetByEmail(string emailId,int legalEntityId);
        //Task ChangePassword();

        Task<User> GetUser(string emailId, int legalEntityId=0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<RealitycsUserPassword> GetCurrentPassword(int userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<int> UpdateUser(User user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerPassword"></param>
        /// <returns></returns>
        Task InsertCustomerPassword(RealitycsUserPassword customerPassword);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool IsRegistered(User user);
        /// <summary>
        /// Return Legal Entity User
        /// </summary>
        /// <returns></returns>
        Task<List<ClientUserDTO>> GetUsers();

        Task<int> SaveUser(MangeClientUserDTO payload);

        Task<List<AccessGroupDTO>> AccessGroups();

        Task<List<AccessOperationDTO>> MasterAccessOperation();

        Task<EditAccessGroupDTO> GetAccessGroup(int id);

        Task<ClientUserDTO> GetUserDetails(int id);

        Task<int> SaveAccessGroup(ManageAccessGroupDTO accessGroup);
        Task<bool> UpdateUser(MangeClientUserDTO user);
        Task<int> UpdateAccessGroup(ManageAccessGroupDTO accessGroup);
    }
}
