using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RealityCS.DTO;
using RealityCS.DTO.Admin;
using RealityCS.Web.Utility;

namespace RealityCS.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class KpiSetupController : RestrictedControllerBase
    {
        public KpiSetupController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor, configuration, "AdminApiBase")
        {

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            var model = new DTO_KPI();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddKpi(DTO_KPI kpi)
        {
            //var error = ModelState.Root.Errors.Select(x => x.ErrorMessage).SingleOrDefault().ToString();
            if (ModelState.IsValid)
            {
                string uri = kpi.KPI_ID <= 0 ? "KpiSetup/AddKpi" : "KpiSetup/UpdateKpi";

                var result = await this.ApiClient.PostAsync<ApiResponse<dynamic>, DTO_KPI>(uri, kpi);

                return Ok(result);
            }
            return Ok(false);
        }
    }
}
