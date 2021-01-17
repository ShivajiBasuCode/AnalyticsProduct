using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RealityCS.Web.Utility;
using System.Dynamic;
using RealityCS.DTO.Admin.Dashboard;
using RealityCS.DTO;

namespace RealityCS.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class BenchmarkController : RestrictedControllerBase
    {
        public BenchmarkController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor, configuration, "AdminApiBase")
        {

        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult GetUserControl(string KPI_TYPE,string kpiname, string kpicd, string operation)
        {
            string view = string.Empty;
            dynamic model = new ExpandoObject();
            model.kpiname = kpiname;
            model.kpicd = kpicd;
            model.operation = operation;
            switch (KPI_TYPE.ToUpper())
            {
                case "GEN":                   
                    view = ViewList.Admin.Benchmark.Partial.Regular._BMRegular;
                    break;
                case "ITEMD":
                    view = ViewList.Admin.Benchmark.Partial.Department._DepartmentBenchmark;
                    break;
                case "ITEMC":
                    view = ViewList.Admin.Benchmark.Partial.Category._CategoryBenchmark;
                    break;
                case "ITEM":
                    view = ViewList.Admin.Benchmark.Partial.Item._SKUItemBenchmark;
                    break;
                case "ITEMSBC":
                    view = ViewList.Admin.Benchmark.Partial.SubCategory._SubCategoryBenchmark;
                    break;
                case "BSNS":
                    view = ViewList.Admin.Benchmark.Partial.Store._StoreBenchmark;
                    break;
                case "REG":
                    view = ViewList.Admin.Benchmark.Partial.Region._RegionBenchmark;
                    break;
                default:
                    break;

            }

            return PartialView(view, model);
        }

        #region Api Call

        public async Task<IActionResult> SetBenchMarkMenuByRole(string role)
        {
            string uri = $"Benchmark/SetBenchMarkMenuByRole?role={role}";
            var result = await this.ApiClient.GetAsync<ApiResponse<dynamic>>(uri);
            return Ok(result);
        }

        public async Task<IActionResult> PopulateGrid(string kpicid, string operation)
        {
            string uri = $"Benchmark/PopulateGrid?kpicid={kpicid}&Operation={operation}";
            var result = await this.ApiClient.GetAsync<ApiResponse<dynamic>>(uri);
            return Ok(result);
        }

        public async Task<IActionResult> Timelines(string kpicid)
        {
            string uri = $"Benchmark/Timelines?kpcd={kpicid}";
            var result = await this.ApiClient.GetAsync<ApiResponse<dynamic>>(uri);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBenchMark([FromBody]List<DTO_BENCHMARK> benchmarks)
        {
            string uri = $"Benchmark/SaveBenchMark";
            var result = await this.ApiClient.PostAsync<dynamic, DTO_BENCHMARK>(uri, benchmarks); 

            return Ok();
        }

        #endregion
    }
}