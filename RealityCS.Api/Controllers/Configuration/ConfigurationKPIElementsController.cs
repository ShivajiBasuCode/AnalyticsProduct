using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealityCS.BusinessLogic;
using RealityCS.BusinessLogic.Customer;
using RealityCS.BusinessLogic.KPIEntity;
using RealityCS.DTO.Admin;
using RealityCS.DTO.KPIEntity;
using Swashbuckle.AspNetCore.Annotations;

namespace RealityCS.Api.Controllers.Configuration
{
    //[SwaggerTag("Create, read, update and delete Kpi")]
    public class ConfigurationKPIElementsController : ConfigurationControllerBase
    {
        private readonly IKPIEntityConfigurationService kpiEntityService;
        private readonly IMapper mapper;
        private readonly IWorkContext workContext;


        public ConfigurationKPIElementsController(IKPIEntityConfigurationService kpiEntityService, IWorkContext workContext, IMapper mapper)
        {
            this.kpiEntityService = kpiEntityService;
            this.workContext = workContext;
            this.mapper = mapper;
        }

        // private readonly KpiSetupLogic kpiSetupLogic;
        // public KpiSetupController(NrtoaDashboardContext nrtoaDashboardContext, IMapper mapper)
        // {
        //     this.kpiSetupLogic = new KpiSetupLogic(nrtoaDashboardContext, mapper);
        // }
        // [SwaggerOperation(
        // Summary = "Add a new Kpi",
        // Description = "Add a new Kpi",
        // OperationId = "AddKpi",
        // Tags = new[] { "KpiSetup" }
        // )]

        // [SwaggerResponse(201, "kpi added", typeof(string))]
        // [SwaggerResponse(404, "Invalid request", typeof(string))]
        // [HttpPost]
        // public IActionResult AddKpi([FromBody]DTO_KPI kpi)
        // {
        //     var result = this.kpiSetupLogic.AddKpi(kpi);
        //     return Ok(new { Data = result });
        // }
        // [SwaggerOperation(
        //Summary = "Update an existing Kpi",
        //Description = "Update an existing Kpi",
        //OperationId = "UpdateKpi",
        //Tags = new[] { "KpiSetup" }
        //)]

        // [SwaggerResponse(201, "kpi updated", typeof(string))]
        // [SwaggerResponse(404, "Invalid request", typeof(string))]
        [HttpPost]
        public async Task<IActionResult> AddKpi([FromBody] ManageAddKpiDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedKPI = await kpiEntityService.AddKPI(payload);
            return Ok(operatedKPI);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateKpi([FromBody] ManageKpiDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedKPI = await kpiEntityService.UpdateKPI(payload);
            return Ok(operatedKPI);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteKpi([FromBody] ManageDeleteKpiDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedKPI = await kpiEntityService.DeleteKPI(payload);
            return Ok(operatedKPI);
        }
        [HttpPost]
        public async Task<IActionResult> AddRisk([FromBody] ManageAddRiskDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedRisk = await kpiEntityService.AddRisk(payload);
            return Ok(operatedRisk);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRisk([FromBody] ManageRiskDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedRisk = await kpiEntityService.UpdateRisk(payload);
            return Ok(operatedRisk);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRisk([FromBody] ManageDeleteRiskDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedRisk = await kpiEntityService.DeleteRisk(payload);
            return Ok(operatedRisk);
        }
        
        [HttpGet]
        public async Task<IActionResult> KPIs()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var kpis = await kpiEntityService.KPIs();

            return Ok(kpis);
        }

        [HttpGet]
        public async Task<IActionResult> KPIsLite()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var kpis = await kpiEntityService.KPIsLite();

            return Ok(kpis);
        }

        [HttpGet]
        public async Task<IActionResult> KPIInformation([FromBody]FetchKpiDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var kpi = await kpiEntityService.KPIInformation(payload);

            return Ok(kpi);
        }

        [HttpGet]
        public async Task<IActionResult> KPIDataInformation([FromBody]FetchKpiDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var kpi = await kpiEntityService.KPIDataInformation(payload);

            return Ok(kpi);
        }

        [HttpGet]
        public async Task<IActionResult> ValueStreams()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var valuestreams = await kpiEntityService.ValueStreams();

            return Ok(valuestreams);
        }


        [HttpGet]
        public async Task<IActionResult> Risks()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var risks = await kpiEntityService.Risks();

            return Ok(risks);
        }

        [HttpPost]
        public async Task<IActionResult> AddKPIDataElements([FromBody]List<ManageAddKpiDataElementDTO> payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedKPIDataElement = await kpiEntityService.AddKPIDataElements(payload);
            return Ok(operatedKPIDataElement);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateKPIDataElement([FromBody]ManageKpiDataElementDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedKPIDataElement = await kpiEntityService.UpdateKPIDataElement(payload);
            return Ok(operatedKPIDataElement);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteKPIDataElement([FromBody]ManageDeleteKpiDataElementDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedKPIDataElement = await kpiEntityService.DeleteKPIDataElement(payload);
            return Ok(operatedKPIDataElement);
        }

        [HttpPost]
        public async Task<IActionResult> AddKPIDrilldownDataElement([FromBody] ManageAddKpiDataElementDrilldownDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedKPIDrilldownDataElement = await kpiEntityService.AddKPIDrilldownDataElement(payload);
            return Ok(operatedKPIDrilldownDataElement);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateKPIDrilldownDataElement([FromBody] ManageKpiDataElementDrilldownDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedKPIDrilldownDataElement = await kpiEntityService.UpdateKPIDrilldownDataElement(payload);
            return Ok(operatedKPIDrilldownDataElement);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCascadeKPIDrilldownDataElement([FromBody] ManageDeleteCascadeKpiDrilldownDataElementDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedKPIDrilldownDataElement = await kpiEntityService.DeleteCascadeKPIDrilldownDataElement(payload);
            return Ok(operatedKPIDrilldownDataElement);
        }
        [HttpPost]
        public async Task<IActionResult> AddDatasourceRelation(ManageAddKPIJoiningRelationDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedKPIJoiningRelation = await kpiEntityService.AddDatasourceRelation(payload);
            return Ok(operatedKPIJoiningRelation);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDatasourceRelation(ManageKPIJoiningRelationDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedKPIJoiningRelation = await kpiEntityService.UpdateDatasourceRelation(payload);
            return Ok(operatedKPIJoiningRelation);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDatasourceRelation(ManageDeleteKPIJoiningRelationDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedKPIJoiningRelation = await kpiEntityService.DeleteDatasourceRelation(payload);
            return Ok(operatedKPIJoiningRelation);
        }
        [HttpGet]
        public async Task<IActionResult> DatasourceRelations()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedKPIJoiningRelation = await kpiEntityService.DatasourceRelations();
            return Ok(operatedKPIJoiningRelation);
        }
    }
}
