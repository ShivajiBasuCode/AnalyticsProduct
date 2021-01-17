//using Microsoft.AspNetCore.Identity;
//using RealityCS.DataLayer.Context.RealitycsClient;
//using RealityCS.DTO.RealitycsAuth;
//using RealityCS.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RealityCS.BusinessLogic.Admin
//{
//    public class RoleManagementLogic : IRoleManagementLogic
//    {
//        private readonly UserManager<ApplicationUser> userManager;
//        private readonly RoleManager<tbl_AUTH_AspNet_Roles> roleManager;
//        private readonly RealitycsClientContext realitycsClientContext;

//        public RoleManagementLogic(UserManager<ApplicationUser> userManager, RoleManager<tbl_AUTH_AspNet_Roles> roleManager,
//            RealitycsClientContext realitycsClientContext)
//        {
//            this.userManager = userManager;
//            this.roleManager = roleManager;
//            this.realitycsClientContext = realitycsClientContext;
//        }
//        /// <summary>
//        /// List all active role
//        /// </summary>
//        /// <param name="roleSearch"></param>
//        /// <returns></returns>
//        public List<DTO_Role> List(DTO_RoleSearch roleSearch)
//        {
//            var roles = roleManager.Roles.Where(x => x.IsDeleted == false && x.IsActive == true
//              && (roleSearch.ClientID == 0 ? x.FK_ClientID == x.FK_ClientID : x.FK_ClientID == roleSearch.ClientID)
//              && (string.IsNullOrEmpty(roleSearch.RoleName) == true ? x.Name == x.Name : x.Name.Contains(roleSearch.RoleName))
//            ).Select(x => x).ToList();


//            var result = (from client in realitycsClientContext.tbl_Master_ClientInformation
//                          join role in roles on client.PK_ClientID equals role.FK_ClientID into tmprole
//                          from lftrole in tmprole.DefaultIfEmpty()
//                          select new DTO_Role
//                          {
//                              RoleID = lftrole.Id,
//                              RoleName = lftrole.Name,
//                              CompanyName = client.CompanyName

//                          }).ToList();

//            return result;

//        }
//        /// <summary>
//        /// Get a role details by roleid
//        /// </summary>
//        /// <param name="RoleID"></param>
//        /// <returns></returns>
//        public DTO_Role GetById(int RoleID)
//        {
//            var role = roleManager.Roles.Where(x => x.IsDeleted == false && x.IsActive == true && x.Id == RoleID
//            ).Select(x => new DTO_Role
//            {
//                RoleID = x.Id,
//                ClientID = x.FK_ClientID,
//                RoleName = x.Name

//            }).SingleOrDefault()??new DTO_Role();

//            return role;
//        }



//        /// <summary>
//        /// Add a new role
//        /// </summary>
//        /// <param name="role"></param>
//        /// <returns></returns>
//        public async Task<DTO_Role> Add(DTO_Role role)
//        {
//            var newrole = new tbl_AUTH_AspNet_Roles()
//            {
//                Name = role.RoleName,
//                FK_ClientID = role.ClientID,
//                IsActive = role.IsActive,
//                IsDeleted = false
//            };
//            var result = await this.roleManager.CreateAsync(newrole);

//            if (result.Succeeded)
//            {
//                return role;
//            }
//            return null;

//        }
//        /// <summary>
//        /// Update existing role
//        /// </summary>
//        /// <param name="role"></param>
//        /// <returns></returns>
//        public async Task<DTO_Role> Update(DTO_Role role)
//        {

//            var existingrole = await this.roleManager.FindByIdAsync(role.RoleID.ToString());
//            existingrole.Name = role.RoleName;
//            existingrole.IsActive = role.IsActive;

//            var result = await this.roleManager.UpdateAsync(existingrole);
//            if (result.Succeeded)
//            {
//                return role;
//            }
//            return null;

//        }
//        /// <summary>
//        /// Change a role status
//        /// </summary>
//        /// <param name="RoleID"></param>
//        /// <param name="IsActive"></param>
//        /// <returns></returns>
//        public async Task<bool> ChangeStatus(int RoleID, bool IsActive)
//        {
//            var existingrole = await this.roleManager.FindByIdAsync(RoleID.ToString());
//            existingrole.IsActive = IsActive;
//            var result = await this.roleManager.UpdateAsync(existingrole);

//            return result.Succeeded;
//        }
//        /// <summary>
//        /// Delete a role by roleid
//        /// </summary>
//        /// <param name="RoleID"></param>
//        /// <returns></returns>
//        public async Task<bool> Delete(int RoleID)
//        {
//            var existingrole = await this.roleManager.FindByIdAsync(RoleID.ToString());
//            existingrole.IsActive = false;
//            existingrole.IsDeleted = true;

//            var result = await this.roleManager.UpdateAsync(existingrole);

//            return result.Succeeded;
//        }




//    }
//}
