using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealityCS.DTO.RealitycsAuth
{
   public class DTO_Role:DTO_Base
    {
        public int RoleID { get; set; }
        [SwaggerSchema("Client's Id")]
        public Nullable<int> ClientID { get; set; }
        [SwaggerSchema("Role's name")]
        [Required(ErrorMessage = "Role name cannot be blank.")]
        public string RoleName { get; set; }
        [SwaggerSchema("CompanyName for client specific role.This is used for list population only.")]
        public string CompanyName { get; set; }

    }
    public class DTO_RoleSearch
    {
        [SwaggerSchema("Client's Id")]
        public Nullable<int> ClientID { get; set; }
        [SwaggerSchema("Role's name")]
        public string RoleName { get; set; }
    }

    public class RoleListResponseExamples : IExamplesProvider<ApiResponse<List<DTO_Role>>>
    {
        public ApiResponse<List<DTO_Role>> GetExamples()
        {

            return new ApiResponse<List<DTO_Role>>
            {
                IsSuccess = true,
                ReturnMessage = "Role List",
                Data = new List<DTO_Role>
                        {
                            new DTO_Role { RoleID=1,ClientID=null,RoleName="Administrator",CompanyName="" },
                            new DTO_Role { RoleID=2,ClientID=1,RoleName="Client Custom Role",CompanyName="Sample Company" },
                        }
            };

        }
    }

    public class RoleSingleResponseExamples : IExamplesProvider<ApiResponse<DTO_Role>>
    {
        public ApiResponse<DTO_Role> GetExamples()
        {
            return new ApiResponse<DTO_Role>
            {
                IsSuccess = true,
                ReturnMessage = "Role",
                Data = new DTO_Role
                {
                    RoleID = 1,
                    ClientID = null,
                    RoleName = "Administrator"
                }
            };


        }
    }

}
