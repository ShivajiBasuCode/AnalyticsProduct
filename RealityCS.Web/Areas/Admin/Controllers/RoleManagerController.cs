using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RealityCS.DTO.Admin;
using RealityCS.Web.Utility;
using RealityCS.DTO;
using RealityCS.DTO.RealitycsAuth;

namespace RealityCS.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class RoleManagerController : RestrictedControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="configuration"></param>
        public RoleManagerController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor, configuration, "AdminApiBase")
        {

        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Add(int? id = 0)
        {
            string uri = $"RoleManager/GetRoleById?RoleID={id}";
            var result = await this.ApiClient.GetAsync<ApiResponse<DTO_Role>>(uri);
            var model = result.Data;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(DTO_Role role)
        {

            if (ModelState.IsValid)
            {
                string uri = role.RoleID <= 0 ? "RoleManager/Add" : "RoleManager/Update";

                var result = await this.ApiClient.PostAsync<ApiResponse<dynamic>, DTO_Role>(uri, role);

                return Ok(result);
            }
            else
            {
                var error = ModelState.Root.Errors.Select(x => x.ErrorMessage).SingleOrDefault().ToString();
                return Ok(new { Data = new { StatusCode = 2, Description = error } });
            }
        }

        #region API Call

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            string uri = "RoleManager/Roles";

            var result = await this.ApiClient.GetAsync<ApiResponse<List<dynamic>>>(uri);

            return Ok(result);
        }

        #endregion


    }
}