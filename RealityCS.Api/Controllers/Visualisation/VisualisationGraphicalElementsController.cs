using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealityCS.BusinessLogic;
using RealityCS.BusinessLogic.GraphicalEntity;
using RealityCS.DTO.GraphicalEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealityCS.Api.Controllers.Visualisation
{
    public class VisualisationGraphicalElementsController:VisualisationControllerBase
    {
        private readonly IGraphicalEntityVisualisationService graphicalEntityService;
        private readonly IMapper mapper;
        private readonly IWorkContext workContext;
        public VisualisationGraphicalElementsController(IMapper mapper,
                    IGraphicalEntityVisualisationService graphicalEntityService,
                    IWorkContext workContext)
        {
            this.mapper = mapper;
            this.graphicalEntityService = graphicalEntityService;
            this.workContext = workContext;
        }

        [HttpGet]
        public async Task<IActionResult> Card([FromBody] ManageFetchRealyticsCardInformationInVisualisationDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedCard = await graphicalEntityService.GetCard(payload);
            return Ok(operatedCard);
        }

        [HttpGet]
        public async Task<IActionResult> CardsAcrossDashboards([FromBody] ManageFetchRealyticsMultipleCardInformationInVisualisationDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var operatedCard = await graphicalEntityService.GetCards(payload.Cards);
            return Ok(operatedCard);
        }

        [HttpGet]
        public async Task<IActionResult> Cards([FromBody] ManageFetchAllRealyticsCardInformationForDashboardInVisualisationDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedCard = await graphicalEntityService.GetCards(payload);
            return Ok(operatedCard);
        }

        [HttpGet]
        public async Task<IActionResult> NavigationDashboards()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var results = await graphicalEntityService.NavigationDashboards();
            return Ok(results);
        }
    }
}
