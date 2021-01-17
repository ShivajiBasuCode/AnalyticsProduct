using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO
{
    public class DTO_FillDropdown
    {
        [SwaggerSchema("Dropdown value")]
        public object id { get; set; }
        [SwaggerSchema("Dropdown text")]
        public string text { get; set; }
        [SwaggerSchema("Default selection")]
        [JsonIgnore]
        public Boolean selected { get; set; }
        [JsonIgnore]
        public int DisplayOrder { get; set; }

    }

    public class FillDropdownListResponseExamples : IExamplesProvider<ApiResponse<List<DTO_FillDropdown>>>
    {
        public ApiResponse<List<DTO_FillDropdown>> GetExamples()
        {
                                                                                                                                
            return new ApiResponse<List<DTO_FillDropdown>>
            {
                IsSuccess = true,
                ReturnMessage = "Dropdown items list",
                Data = new List<DTO_FillDropdown>
                        {
                            new DTO_FillDropdown { id=1,text="Item 1" },
                             new DTO_FillDropdown { id=2,text="Item 2" },
                              new DTO_FillDropdown { id=3,text="Item 3" },
                               new DTO_FillDropdown { id=4,text="Item 4" },
                                new DTO_FillDropdown { id=5,text="Item 5" },
                        }
            };

        }
    }
}
