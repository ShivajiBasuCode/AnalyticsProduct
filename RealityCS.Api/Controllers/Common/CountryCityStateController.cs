using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealityCS.BusinessLogic;
using RealityCS.DataLayer.Context.RealitycsShared;
using RealityCS.DTO.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealityCS.Api.Controllers.Common
{
    public class CountryCityStateController : CommonControllerBase
    {
        private readonly IMapper mapper;
        private IWorkContext workContext;
        private IRealitycsGeographicLocationService realitycsGeoLocationService;
        public CountryCityStateController(
            IMapper mapper,
            IWorkContext workContext,
            IRealitycsGeographicLocationService realitycsLocalisationService
            )
        {
            this.mapper = mapper;
            this.workContext = workContext;
            this.realitycsGeoLocationService = realitycsLocalisationService;
        }
        [HttpGet]
        public async Task<IActionResult> Countries()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedCountries = await realitycsGeoLocationService.Countries();

            return Ok(operatedCountries);
        }
        [HttpGet]
        public async Task<IActionResult> States()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedStates = await realitycsGeoLocationService.States();

            return Ok(operatedStates);
        }
        [HttpGet]
        public async Task<IActionResult> States([FromBody] ManageFetchStatesOnCountryDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedStates = await realitycsGeoLocationService.States(payload);

            return Ok(operatedStates);
        }

        [HttpGet]
        public async Task<IActionResult> Cities()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedCities = await realitycsGeoLocationService.Cities();

            return Ok(operatedCities);
        }
        [HttpGet]
        public async Task<IActionResult> Cities([FromBody]ManageFetchCitiesOnStateDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedCities = await realitycsGeoLocationService.Cities(payload);

            return Ok(operatedCities);
        }
        
        [HttpGet]
        public async Task<IActionResult> Cities([FromBody] ManageFetchCitiesOnCountryAndStateDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedCities = await realitycsGeoLocationService.Cities(payload);

            return Ok(operatedCities);
        }

    }
}
