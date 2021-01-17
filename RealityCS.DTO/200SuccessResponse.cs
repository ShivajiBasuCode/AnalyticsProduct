using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RealityCS.DTO
{
    [DataContract]
    public class _200ErrorResponse<T>
    {
        [DataMember(Name = "IsSuccess")]
        public bool IsSuccess { get; set; }

        [DataMember(Name = "ReturnMessage")]
        public string ReturnMessage { get; set; }

        [DataMember(Name = "Data")]
        public T Data { get; set; }

    }

    public class _200SuccessResponseExample: IExamplesProvider<_200ErrorResponse<string>>
    {
        public _200ErrorResponse<string> GetExamples()
        {
            return new _200ErrorResponse<string>
            {
                IsSuccess = true,
                ReturnMessage = "Successful responses",
                Data = "Operation succeded."
            };
        }
    }

    [DataContract]
    public class _201ErrorResponse<T>
    {
        [DataMember(Name = "IsSuccess")]
        public bool IsSuccess { get; set; }

        [DataMember(Name = "ReturnMessage")]
        public string ReturnMessage { get; set; }

        [DataMember(Name = "Data")]
        public T Data { get; set; }

    }

    public class _201SuccessResponseExample : IExamplesProvider<_201ErrorResponse<string>>
    {
        public _201ErrorResponse<string> GetExamples()
        {
            return new _201ErrorResponse<string>
            {
                IsSuccess = true,
                ReturnMessage = "Successful responses",
                Data = "The request has succeeded and a new resource has been created as a result."
            };
        }
    }
}
