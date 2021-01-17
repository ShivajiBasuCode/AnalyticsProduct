using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealityCS.BusinessLogic;
using RealityCS.BusinessLogic.Global;
using RealityCS.DataLayer;
using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
using RealityCS.DTO.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealityCS.Api.Controllers.Common
{
    public class MasterController: CommonControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWorkContext workContext;
        private readonly IIndustryService industryService;
        private readonly IDepartmentService departmentService;
        private readonly IDivisionService divisionService;


        public MasterController(
            IMapper mapper,
            IWorkContext workContext,
            IIndustryService industryService,
            IDepartmentService departmentService,
            IDivisionService divisionService
            )
        {
            this.mapper = mapper;
            this.workContext = workContext;
            this.industryService = industryService;
            this.departmentService = departmentService;
            this.divisionService = divisionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddIndustry([FromBody] ManageAddIndustryDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedIndustry = await industryService.AddIndustry(payload);
            return Ok(operatedIndustry);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] ManageAddDepartmentDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedDepartment = await departmentService.AddDepartment(payload);
            return Ok(operatedDepartment);
        }

        [HttpPost]
        public async Task<IActionResult> AddDivision([FromBody] ManageAddDivisionDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedDivision = await divisionService.AddDivision(payload);
            return Ok(operatedDivision);
        }
        [HttpGet]
        public async Task<IActionResult> Industries()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedIndustry = await industryService.Industries();
            return Ok(operatedIndustry);
        }

        [HttpGet]
        public async Task<IActionResult> Departments()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedDepartment = await departmentService.Departments();
            return Ok(operatedDepartment);
        }

        [HttpGet]
        public async Task<IActionResult> Divisions()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedDivision = await divisionService.Divisions();
            return Ok(operatedDivision);
        }
    }
}
