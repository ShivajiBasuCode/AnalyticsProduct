using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO
{
   public abstract class DTO_SearchBase
    {
        public string Field { get; set; } = string.Empty;
        public object Query { get; set; } = string.Empty;

        [SwaggerSchema("To include inactive clients")]
        public bool IncludeInactive { get; set; }
        [SwaggerSchema("To include deleted clients")]
        public bool IncludeDeleted { get; set; }
    }
}
