using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealityCS.BusinessLogic.Customer;
using RealityCS.DTO;
using RealityCS.DTO.RealitycsClient;
using RealityCS.Identity;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace RealityCS.Api.Controllers.Common
{
    
    public class FillComboController : CommonControllerBase
    {
       
        private readonly RoleManager<tbl_AUTH_AspNet_Roles> roleManager;
        private readonly IFillComboLogic fillComboLogic;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nrtoaDashboardContext"></param>
        /// <param name="roleManager"></param>
        public FillComboController(RoleManager<tbl_AUTH_AspNet_Roles> roleManager,IFillComboLogic fillComboLogic)
        {
            
            this.roleManager = roleManager;
            this.fillComboLogic = fillComboLogic;
        }

        [SwaggerOperation(
        Summary = "Get all roles",
        Description = "Get all roles",
        OperationId = "Roles",
        Tags = new[] { "FillCombo" }
    )]
        [SwaggerResponse(200, "client list", typeof(IEnumerable<DTO_FillDropdown>))]
        [SwaggerResponseExample(200, typeof( FillDropdownListResponseExamples))]
        [SwaggerResponse(404, "Bad Request", typeof(ApiResponse<string>))]
        [SwaggerResponseExample(400, typeof(_404ErrorResponseExample))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ApiResponse<string>))]
        [SwaggerResponseExample(500, typeof(_500ErrorResponseExample))]

        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            var roles = await Task.FromResult(this.roleManager.Roles.Select(s => new {id= s.Id,text= s.Name }).ToList<dynamic>());
            return Ok(new { Data = roles });
        }

        
        [SwaggerOperation(
           Summary = "Dropdown items list",
           Description = "Return a list Dropdown.",
           OperationId = "DropDown",
           Tags = new[] { "FillCombo" }
       )]

        [SwaggerResponse(200, "Dropdown items list", typeof(IEnumerable<DTO_FillDropdown>))]
        [SwaggerResponseExample(200, typeof(FillDropdownListResponseExamples))]
        [SwaggerResponse(404, "Bad Request", typeof(ApiResponse<string>))]
        [SwaggerResponseExample(400, typeof(_404ErrorResponseExample))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ApiResponse<string>))]
        [SwaggerResponseExample(500, typeof(_500ErrorResponseExample))]
        [HttpGet]
        public IActionResult DropDown()
        {
            var result = this.fillComboLogic.DropDown();

            return Ok(new { IsSuccess = true, ReturnMessage = "Dropdown items", Data = result });
        }

        [SwaggerOperation(
           Summary = "Search Parameters",
           Description = "Fill search parameters dynamically for a table.",
           OperationId = "SearchParameters",
           Tags = new[] { "FillCombo" }
       )]

        [SwaggerResponse(200, "Dropdown items list", typeof(IEnumerable<DTO_FillDropdown>))]
        [SwaggerResponseExample(200, typeof(FillDropdownListResponseExamples))]
        [SwaggerResponse(404, "Bad Request", typeof(ApiResponse<string>))]
        [SwaggerResponseExample(400, typeof(_404ErrorResponseExample))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ApiResponse<string>))]
        [SwaggerResponseExample(500, typeof(_500ErrorResponseExample))]
        [HttpGet]
        public IActionResult SearchParameters(string Table)
        {
            var result = this.fillComboLogic.SearchParameters(Table);

            return Ok(new { IsSuccess = true, ReturnMessage = "Dropdown items", Data = result });
        }



    }
}
