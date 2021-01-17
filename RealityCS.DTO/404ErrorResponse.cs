using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RealityCS.DTO
{

        [DataContract]
        public class _404ErrorResponse<T>
        {
            [DataMember(Name = "IsSuccess")]
            public bool IsSuccess { get; set; }

            [DataMember(Name = "ReturnMessage")]
            public string ReturnMessage { get; set; }

            [DataMember(Name = "Data")]
            public T Data { get; set; }
        
    }
    public class _404ErrorResponseExample : IExamplesProvider<_404ErrorResponse<string>>
    {
        public _404ErrorResponse<string> GetExamples()
        {
            return new _404ErrorResponse<string>
            {
                IsSuccess = false,
                ReturnMessage = "Bad Request",
                Data = "The server could not understand the request due to invalid syntax."
            };
        }
    }
}
