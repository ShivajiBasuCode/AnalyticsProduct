using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealityCS.BusinessLogic;
using RealityCS.BusinessLogic.Customer;
using RealityCS.DTO.RealitycsClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealityCS.Api.Controllers.Configuration
{
    public class ConfigurationLegalEntityController:ConfigurationControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWorkContext workContext;
        private readonly ILegalEntityService legalEntityService;

        public ConfigurationLegalEntityController(
            IMapper mapper,
            IWorkContext workContext,
            ILegalEntityService legalEntityService
            )
        {
            this.mapper = mapper;
            this.workContext = workContext;
            this.legalEntityService = legalEntityService;
        }
        [HttpPost]
        public async Task<IActionResult> AddLegalEntity([FromBody] ManageAddLegalEntityDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedLegalEntity = await legalEntityService.AddLegalEntity(payload);
            return Ok(operatedLegalEntity);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLegalEntity([FromBody] ManageLegalEntityDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var operatedLegalEntity = await legalEntityService.UpdateLegalEntity(payload);
            return Ok(operatedLegalEntity);
        }
        [HttpGet]
        public async Task<IActionResult> LegalEntities()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var legalEntities = await legalEntityService.LegalEntities();

            return Ok(legalEntities);
        }
        [HttpGet]
        public async Task<IActionResult> GetA()
        {

            return Ok(new { });
        }


        [HttpPost]
        public async Task<IActionResult> ImportData([FromForm] ClientDataImportDTO request)
        {
            try
            {
                foreach (var item in request.item)
                {
                    var file = item.file;
                    if (file == null || file.Length <= 0)
                    {
                        ModelState.AddModelError("", "File empty");
                        return BadRequest();
                    }
                    if (!Path.GetExtension(file.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError("", "Not Support file extension");
                        return BadRequest();
                    }
                    item.dataSourceType = "csv";
                }

                var result = await legalEntityService.ImportClientData(request);
                return Ok(new { });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDataSourceName()
        {
            try
            {
                var data = await this.legalEntityService.GetDataSourceName();
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDataSource(int id)
        {
            try
            {
                var data = await this.legalEntityService.GetDataSource(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDataSourceElements(int id)
        {
            try
            {
                var data = await this.legalEntityService.GetDataSourceElements(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostDataSourceElementsAliasName(SaveAttributeAliasNameDTO data)
        {
            try
            {
                await this.legalEntityService.DataSourceElementsAliasName(data);
                return Ok(new { Message = "Alias Name Updates Sucessfully!" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
