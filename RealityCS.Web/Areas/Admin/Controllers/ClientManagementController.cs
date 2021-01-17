using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RealityCS.DTO;
using RealityCS.DTO.RealitycsClient;
using RealityCS.Web.Utility;

namespace RealityCS.Web.Areas.Admin.Controllers
{
    [Area("admin")]

    public class ClientManagementController : RestrictedControllerBase
    {

        public ClientManagementController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor, configuration, "AdminApiBase")
        {

        }

        public IActionResult Index()
        {
            return View();
        }
      
        public async Task<IActionResult> Add(int? id = 0)
        {
            var model = await this.GetById((int)id);

            return View(model);
        }

        #region Api Call

        [HttpPost]
        public async Task<IActionResult> List([FromBody] DTO_ClientInformationSearch clientInformationSearch)
        {
            string uri = "ClientManagement/List";
            var result = await this.ApiClient.PostAsync<ApiResponse<List<DTO_ClientInformation>>, DTO_ClientInformationSearch>(uri, clientInformationSearch);
            return Ok(result);
        }
        private async Task<DTO_ClientInformation> GetById(Int32 ClientID)
        {
            string uri = $"ClientManagement/GetById?ClientID={ClientID}";
            var result = await this.ApiClient.GetAsync<ApiResponse<DTO_ClientInformation>>(uri);
            return result.Data;
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Add(DTO_ClientInformation clientInformation)
        {
            if (ModelState.IsValid)
            {
                string uri = clientInformation.ClientID <= 0 ? "ClientManagement/Add" : "ClientManagement/Update";
                var result = await this.ApiClient.PostAsync<ApiResponse<dynamic>, DTO_ClientInformation>(uri, clientInformation);
                return Ok(result);
            }
            else
            {
                var error = ModelState.Root.Errors.Select(x => x.ErrorMessage).SingleOrDefault().ToString();
                return Ok(new { Data = new { StatusCode = 2, Description = error } });
            }
        }
       
        [HttpGet]
        public async Task<IActionResult> ChangeStatus(int ClientID, bool IsActive)
        {
            string uri = $"ClientManagement/ChangeStatus?ClientID={ClientID}&IsActive={IsActive}";
            var result = await this.ApiClient.GetAsync<ApiResponse<dynamic>>(uri);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int ClientID)
        {
            string uri = $"ClientManagement/Delete?ClientID={ClientID}";
            var result = await this.ApiClient.GetAsync<ApiResponse<dynamic>>(uri);
            return Ok(result);
        }


        #endregion

    }
}
