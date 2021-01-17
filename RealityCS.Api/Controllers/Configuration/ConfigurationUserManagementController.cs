using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealityCS.Api.Controllers.Configuration;
using RealityCS.BusinessLogic.Customer;
using RealityCS.DTO.Admin;
using RealityCS.DTO.RealitycsClient;
using RealityCS.Identity;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace RealityCS.Api.Controllers.Axiom
{

    public class ConfigurationUserManagementController : ConfigurationControllerBase
    {

        // private readonly UserManager<ApplicationUser> userManager;
        // private readonly RoleManager<tbl_AUTH_AspNet_Roles> roleManager;
        private readonly IClientUser clientUser;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public ConfigurationUserManagementController(IClientUser clientUser)
        {

            this.clientUser = clientUser;

        }
        //   [SwaggerOperation(
        //      Summary = "Get user list",
        //      Description = "Get list of users based on search criteria",
        //      OperationId = "ListUsers",
        //      Tags = new[] { "UserManagement" }
        //  )]
        //   [SwaggerResponse(201, "users found", typeof(string))]
        //   [SwaggerResponse(404, "Invalid request", typeof(string))]
        //   /// <summary>
        //   /// 
        //   /// </summary>
        //   /// <param name="userSearchCriteria"></param>
        //   /// <returns></returns>
        //   [HttpPost]
        //   public async Task<IActionResult> ListUsers([FromBody] DTOUserSearchCriteria userSearchCriteria)
        //   {

        //       try
        //       {
        //           var tmpusers = (from u in this.userManager.Users
        //                           from ur in u.UserRoles
        //                           join r in this.roleManager.Roles on ur.RoleId equals r.Id
        //                           //join m in this.userManager.Users on u.FKUserId_ReportsTo equals m.Id into tmpm
        //                          // from lftm in tmpm.DefaultIfEmpty()
        //                           where u.IsActive == userSearchCriteria.IsActive
        //                           select new
        //                           {
        //                               u.Id,
        //                               Name = string.Join(' ', u.FirstName, u.MiddleName, u.LastName),
        //                               u.Email,
        //                               Role = r.Name,
        //                               //Manager = lftm != null ? string.Join(' ', lftm.FirstName, lftm.MiddleName, lftm.LastName) : "",
        //                               u.IsActive
        //                           }).AsEnumerable();


        //           tmpusers = tmpusers.OrderBy(o => o.Name);

        //           if (string.IsNullOrEmpty(userSearchCriteria.Name) == false)
        //           {
        //               tmpusers = tmpusers.Where(x => x.Name.ToLower().Contains(userSearchCriteria.Name.ToLower()));

        //           }
        //           if (string.IsNullOrEmpty(userSearchCriteria.Email) == false)
        //           {
        //               tmpusers = tmpusers.Where(x => x.Email.Contains(userSearchCriteria.Email));
        //           }


        //           var users = await Task.FromResult(tmpusers.ToList<dynamic>());



        //           return Ok(new { Data = users });
        //       }
        //       catch (Exception ex)
        //       {
        //           return null;
        //       }
        //   }
        //   [SwaggerOperation(
        //      Summary = "Get a user's details",
        //      Description = "Get user's details by Id",
        //      OperationId = "GetUserById",
        //      Tags = new[] { "UserManagement" }
        //  )]
        //   [SwaggerResponse(201, "user found", typeof(string))]
        //   [SwaggerResponse(404, "Invalid request", typeof(string))]
        //   /// <summary>
        //   /// 
        //   /// </summary>
        //   /// <param name="userId"></param>
        //   /// <returns></returns>
        //   [HttpGet]
        //   public async Task<IActionResult> GetUserById([FromQuery,SwaggerParameter("User's Id",Required =true)]int userId)
        //   {


        //       var tmpuser = (from u in this.userManager.Users
        //                      from ur in u.UserRoles
        //                      join r in this.roleManager.Roles on ur.RoleId equals r.Id
        //                      //join m in this.userManager.Users on u.FKUserId_ReportsTo equals m.Id into tmpm
        //                      //from lftm in tmpm.DefaultIfEmpty()
        //                      where u.Id == userId
        //                      select new DTOUser
        //                      {
        //                          Id = u.Id,
        //                          FirstName = u.FirstName,
        //                          MiddleName = u.MiddleName,
        //                          LastName = u.LastName,
        //                          Gender = u.Gender,
        //                          Role = r.Id,
        //                         // RepotsTo = lftm.Id,
        //                          Email = u.Email,
        //                          IsActive = (bool)u.IsActive,
        //                          Password = u.Password

        //                      });


        //       var result = await Task.FromResult(tmpuser.SingleOrDefault());
        //       var user = (result == null) ? new DTOUser() : result;


        //       return Ok(new { Data = user });

        //   }

        //   [SwaggerOperation(
        //     Summary = "Add a new user",
        //     Description = "Add a new user",
        //     OperationId = "AddUser",
        //     Tags = new[] { "UserManagement" }
        // )]
        //   [SwaggerResponse(201, "user added", typeof(string))]
        //   [SwaggerResponse(404, "Invalid request", typeof(string))]
        //   /// <summary>
        //   /// 
        //   /// </summary>
        //   /// <param name="dtoUser"></param>
        //   /// <returns></returns>
        //   //https://stackoverflow.com/a/36645048/4336330
        //   [HttpPost]
        //   public async Task<dynamic> AddUser([FromBody] DTOUser dtoUser)
        //   {

        //       var user = new ApplicationUser
        //       {
        //           UserName = dtoUser.Email,
        //           Email = dtoUser.Email,
        //           FirstName = dtoUser.FirstName,
        //           MiddleName = dtoUser.MiddleName,
        //           LastName = dtoUser.LastName,
        //           PhoneNumber = dtoUser.PhoneNumber,
        //           MobileNo = dtoUser.PhoneNumber,
        //           Gender = dtoUser.Gender,                
        //           Password = dtoUser.Password,
        //           DisplayUserName = string.Join(' ', dtoUser.FirstName, dtoUser.MiddleName, dtoUser.LastName),
        //           IsActive = dtoUser.IsActive,
        //           IsDeleted = false
        //       };

        //       using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //       {

        //           IdentityResult identityResult = await this.userManager.CreateAsync(user, dtoUser.Password);

        //           var error = identityResult.Errors.Select(x => new { x.Code, x.Description }).FirstOrDefault();


        //           if (identityResult.Succeeded && error == null)
        //           {
        //               var role = await roleManager.FindByIdAsync(dtoUser.Role.ToString());

        //               await this.userManager.AddToRoleAsync(user, role.Name);



        //               scope.Complete();
        //               return new { Data = new { StatusCode = 1, Description = "User Created" } };
        //           }
        //           else
        //           {
        //               scope.Dispose();
        //               return new { Data = new { StatusCode = 0, error.Description } };
        //           }
        //       }
        //   }
        //   [SwaggerOperation(
        //    Summary = "Update existing user",
        //    Description = "Update existing user",
        //    OperationId = "UpdateUser",
        //    Tags = new[] { "UserManagement" }
        //)]
        //   [SwaggerResponse(201, "user updated", typeof(string))]
        //   [SwaggerResponse(404, "Invalid request", typeof(string))]
        //   /// <summary>
        //   /// 
        //   /// </summary>
        //   /// <param name="dtoUser"></param>
        //   /// <returns></returns>
        //   [HttpPost]
        //   public async Task<dynamic> UpdateUser([FromBody]DTOUser dtoUser)
        //   {
        //       var existinguser = await this.userManager.FindByIdAsync(dtoUser.Id.ToString());
        //       bool bresult = true;

        //       existinguser.UserName = dtoUser.Email;
        //       existinguser.Email = dtoUser.Email;
        //       existinguser.FirstName = dtoUser.FirstName;
        //       existinguser.MiddleName = dtoUser.MiddleName;
        //       existinguser.LastName = dtoUser.LastName;
        //       existinguser.PhoneNumber = dtoUser.PhoneNumber;
        //       existinguser.MobileNo = dtoUser.PhoneNumber;
        //       existinguser.Gender = dtoUser.Gender;
        //       //existinguser.FKUserId_ReportsTo = dtoUser.RepotsTo;
        //       existinguser.DisplayUserName = string.Join(' ', dtoUser.FirstName, dtoUser.MiddleName, dtoUser.LastName);

        //       using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //       {

        //           try
        //           {

        //               IdentityResult identityResult = await this.userManager.UpdateAsync(existinguser);

        //               var error = identityResult.Errors.Select(x => new { x.Code, x.Description }).FirstOrDefault();

        //               if (identityResult.Succeeded && error == null)
        //               {
        //                   var currentrole = await this.roleManager.FindByIdAsync(dtoUser.Role.ToString());

        //                   if (!await this.userManager.IsInRoleAsync(existinguser, currentrole.Name))
        //                   {
        //                       var existingrole = existinguser.UserRoles.Select(x => x.Role).SingleOrDefault();

        //                       if (existingrole != null)
        //                       {
        //                           var removeroleresult = await this.userManager.RemoveFromRoleAsync(existinguser, existingrole.Name.ToString());
        //                           if (removeroleresult.Succeeded)
        //                           {
        //                               var addtoroleresult = await this.userManager.AddToRoleAsync(existinguser, currentrole.Name);
        //                               if (addtoroleresult.Succeeded)
        //                               {
        //                                   scope.Complete();
        //                                   bresult = true;
        //                               }
        //                               else
        //                               {
        //                                   scope.Dispose();
        //                                   bresult = false;
        //                               }
        //                           }
        //                           else
        //                           {
        //                               scope.Dispose();
        //                               bresult = false;
        //                           }
        //                       }
        //                       else
        //                       {
        //                           var addtoroleresult = await this.userManager.AddToRoleAsync(existinguser, currentrole.Name);
        //                           if (addtoroleresult.Succeeded)
        //                           {
        //                               scope.Complete();
        //                               bresult = true;
        //                           }
        //                           else
        //                           {
        //                               scope.Dispose();
        //                               bresult = false;
        //                           }
        //                       }


        //                       // scope.Complete();

        //                   }
        //                   else
        //                   {
        //                       scope.Complete();
        //                       bresult = true;
        //                   }
        //               }
        //               else
        //               {
        //                   scope.Dispose();
        //                   bresult = false;
        //               }

        //           }
        //           catch (Exception e)
        //           {
        //               scope.Dispose();
        //               bresult = false;
        //           }
        //       }

        //       if (bresult == true)
        //       {
        //           return new { Data = new { StatusCode = 1, Description = "User updated ." } };
        //       }
        //       else
        //       {
        //           return new { Data = new { StatusCode = 0, Description = "User update failed ." } };
        //       }
        //   }       
        //   [SwaggerOperation(
        //    Summary = "Get all users",
        //    Description = "Get all active ssers",
        //    OperationId = "GetUsers",
        //    Tags = new[] { "UserManagement" }
        //)]
        //   [SwaggerResponse(201, "user list", typeof(string))]
        //   [SwaggerResponse(404, "Invalid request", typeof(string))]

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await clientUser.GetUsers();

            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> AccessGroups()
        {
            var result = await clientUser.AccessGroups();

            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> AccessOperation()
        {
            var masterOperations = await clientUser.MasterAccessOperation();
            return Ok(masterOperations);
        }

        [HttpGet]
        public new async Task<IActionResult> User(int userid)
        {
            var users = await clientUser.GetUsers();

            return Ok(new { Data = users });
        }

        [HttpPost]
        public new async Task<IActionResult> User(MangeClientUserDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var users = await clientUser.SaveUser(payload);
            return Ok(new { });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(MangeClientUserDTO payload)
        {
            try
            {
                var users = await clientUser.UpdateUser(payload);
                return Ok(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAccessGroup(int id)
        {
            var result = await clientUser.GetAccessGroup(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDetails(int id)
        {
            var result = await clientUser.GetUserDetails(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AccessGroup(ManageAccessGroupDTO payload)
        {
            var users = await clientUser.SaveAccessGroup(payload);
            return Ok(new { });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAccessGroup(ManageAccessGroupDTO payload)
        {
            var users = await clientUser.UpdateAccessGroup(payload);
            return Ok(new { });
        }
    }
}