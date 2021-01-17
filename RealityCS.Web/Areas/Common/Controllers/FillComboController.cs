using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RealityCS.DTO;
using RealityCS.Web.Utility;

namespace RealityCS.Web.Areas.Common.Controllers
{
    [Area("common")]
    public class FillComboController : RestrictedControllerBase
    {
        public FillComboController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor, configuration, "CommonApiBase")
        {

        }


        [HttpGet]
        public async Task<IActionResult> DropDown()
        {
            string uri = "FillCombo/DropDown";
            var result = await this.ApiClient.GetAsync<ApiResponse<List<dynamic>>>(uri);

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> SearchParameters(string table)
        {
            string uri = $"FillCombo/SearchParameters?Table={table}";
            var result = await this.ApiClient.GetAsync<ApiResponse<List<dynamic>>>(uri);
            
            return Ok(result);
        }
    }
}
