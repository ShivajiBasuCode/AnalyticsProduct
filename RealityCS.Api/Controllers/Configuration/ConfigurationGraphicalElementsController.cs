using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealityCS.BusinessLogic;
using RealityCS.BusinessLogic.GraphicalEntity;
using RealityCS.DTO.GraphicalEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealityCS.Api.Controllers.Configuration
{
    public class ConfigurationGraphicalElementsController : ConfigurationControllerBase
    {
        private readonly IGraphicalEntityConfigurationService graphicalEntityService;
        private readonly IMapper mapper;
        private readonly IWorkContext workContext;

        public ConfigurationGraphicalElementsController(
                    IMapper mapper,
                    IGraphicalEntityConfigurationService graphicalEntityService,
                    IWorkContext workContext)
        {
            this.mapper = mapper;
            this.graphicalEntityService = graphicalEntityService;
            this.workContext = workContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] ManageAddGraphicalCardDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedCard = await graphicalEntityService.AddCard(payload);
            return Ok(operatedCard);
        }

        [HttpPost]
        public async Task<IActionResult> AddCards([FromBody] List<ManageAddGraphicalCardDTO> payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedCard = await graphicalEntityService.AddCards(payload);
            return Ok(operatedCard);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCard([FromBody] ManageGraphicalCardDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedCard = await graphicalEntityService.UpdateCard(payload);
            return Ok(operatedCard);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCards([FromBody] List<ManageGraphicalCardDTO> payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedCard = await graphicalEntityService.UpdateCards(payload);
            return Ok(operatedCard);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCard([FromBody] ManageDeleteGraphicalCardDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedCard = await graphicalEntityService.DeleteCard(payload);
            return Ok(operatedCard);
        }

        [HttpGet]
        public async Task<IActionResult> CardsInDashboard([FromBody]ManageSelectGraphicalDashboardDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var results = await graphicalEntityService.Cards(payload);
            return Ok(results);
        }
        [HttpGet]
        public async Task<IActionResult> Cards()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var results = await graphicalEntityService.Cards();
            return Ok(results);
        }
        [HttpPost]
        public async Task<IActionResult> AddDashboard([FromBody] ManageAddGraphicalDashboardDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedDashboard = await graphicalEntityService.AddDashboard(payload);
            return Ok(operatedDashboard);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDashboard([FromBody] ManageGraphicalDashboardDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedDashboard = await graphicalEntityService.UpdateDashboard(payload);
            return Ok(operatedDashboard);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDashboard([FromBody] ManageDeleteGraphicalDashboardDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedDashboard = await graphicalEntityService.DeleteDashboard(payload);
            return Ok(operatedDashboard);
        }

        [HttpGet]
        public async Task<IActionResult> Dashboards()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var results = await graphicalEntityService.Dashboards();
            return Ok(results);
        }
        [HttpPost]
        public async Task<IActionResult> AddDashboardTemplate([FromBody] ManageAddGraphicalTemplateDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedTemplate = await graphicalEntityService.AddDashboardTemplate(payload);
            return Ok(operatedTemplate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDashboardTemplate([FromBody] ManageGraphicalTemplateDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedTemplate = await graphicalEntityService.UpdateDashboardTemplate(payload);
            return Ok(operatedTemplate);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDashboardTemplate([FromBody] ManageDeleteGraphicalTemplateDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedTemplate = await graphicalEntityService.DeleteDashboardTemplate(payload);
            return Ok(operatedTemplate);
        }

        [HttpGet]
        public async Task<IActionResult> DashboardTemplates()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var results = await graphicalEntityService.DashboardTemplates();
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCardDataAttribute([FromBody] ManageAddGraphicalCardAttributeIdDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedCard = await graphicalEntityService.RegisterCardDataAttribute(payload);
            return Ok(operatedCard);
        }

        [HttpPost]
        public async Task<IActionResult> UnRegisterCardDataAttribute([FromBody]ManageGraphicalCardAttributeIdDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedCard = await graphicalEntityService.UnRegisterCardDataAttribute(payload);
            return Ok(operatedCard);
        }

    }
}
