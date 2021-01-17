using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RealityCS.DTO.Admin;
using RealityCS.Web.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealityCS.DTO;

namespace RealityCS.Web.Areas.RealityCS.Controllers
{
    [Area("admin")]
    public class UsermanagementController : RestrictedControllerBase
    {
        public UsermanagementController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor, configuration, "AdminApiBase")
        {

        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Add(int? id = 0)
        {
            string uri = $"UserManagement/GetUserById?userId={id}";
            var result = await this.ApiClient.GetAsync<ApiResponse<DTOUser>>(uri);
            var model = result.Data;
            return View(model);
        }

        #region API Call

        [HttpPost]
        public async Task<IActionResult> ListUsers([FromBody]DTOUserSearchCriteria userSearchCriteria)
        {
            string uri = "UserManagement/ListUsers";

            var result = await this.ApiClient.PostAsync<ApiResponse<List<dynamic>>, DTOUserSearchCriteria>(uri, userSearchCriteria);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(DTOUser dTOUser)
        {

            if (ModelState.IsValid)
            {
                string uri = dTOUser.Id <= 0 ? "UserManagement/AddUser" : "UserManagement/UpdateUser";

                var result = await this.ApiClient.PostAsync<ApiResponse<dynamic>, DTOUser>(uri, dTOUser);

                return Ok(result);
            }
            else
            {
               

                var error = ModelState.Root.Errors.Select(x =>x.ErrorMessage).SingleOrDefault().ToString();
                return Ok(new { Data = new { StatusCode = 2, Description = error } });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            string uri = "RoleManager/Roles";
            var result = await this.ApiClient.GetAsync<ApiResponse<List<dynamic>>>(uri);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            string uri = $"UserManagement/GetUsers";
            var result = await this.ApiClient.GetAsync<ApiResponse<List<dynamic>>>(uri);

            return Ok(result);
        }

        #endregion

    }
}