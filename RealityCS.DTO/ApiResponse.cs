using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RealityCS.DTO
{
    [DataContract]
    public class ApiResponse<T>
    { 
        [DataMember(Name = "StatusCode")]
        public int StatusCode { get; set; }
        
        [DataMember(Name = "IsSuccess")]
        public bool IsSuccess { get; set; }

        [DataMember(Name = "ReturnMessage")]
        public string ReturnMessage { get; set; }

        [DataMember(Name = "Data")]
        public T Data { get; set; }

        [DataMember(Name = "Error")]
        public object Error { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }
}
