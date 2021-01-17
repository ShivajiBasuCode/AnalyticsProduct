using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RealityCS.DTO
{
    [DataContract]
    public class _500ErrorResponse<T>
    {
        [DataMember(Name = "IsSuccess")]
        public bool IsSuccess { get; set; }

        [DataMember(Name = "ReturnMessage")]
        public string ReturnMessage { get; set; }

        [DataMember(Name = "Data")]
        public T Data { get; set; }

    }
    public class _500ErrorResponseExample : IExamplesProvider<_500ErrorResponse<string>>
    {
        public _500ErrorResponse<string> GetExamples()
        {
            return new _500ErrorResponse<string>
            {
                IsSuccess = false,
                ReturnMessage = "Internal Server Error",
                Data = "Returned error message from server."
            };
        }
    }
}
