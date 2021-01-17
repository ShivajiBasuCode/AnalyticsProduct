using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealityCS.DTO
{
   public abstract class DTO_Base
    {
        [JsonIgnore]
        public int? FK_UserID_CreatedBy { get; set; } // int, null
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; } // date, null
        [JsonIgnore]
        public TimeSpan? CreatedTime { get; set; } // time(7), null
        [JsonIgnore]
        public bool IsDeleted { get; set; } = false; // bit, null
        [SwaggerSchema(ReadOnly = true)]
        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true; // bit, null 
        [JsonIgnore]
        public int? FK_UserID_ModifiedBy { get; set; } // int, null
        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; } // date, null
        [JsonIgnore]
        public TimeSpan? ModifiedTime { get; set; } // time(7), null        
        [JsonIgnore]
        public string Changes { get; set; }
    }
}
