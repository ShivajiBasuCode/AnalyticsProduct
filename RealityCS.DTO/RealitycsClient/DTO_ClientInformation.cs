

using Microsoft.AspNetCore.Http.Connections;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealityCS.DTO.RealitycsClient
{
    public class DTO_ClientInformation : DTO_Base
    {

       
        public int ClientID { get; set; } // int, not null        

        [SwaggerSchema("The company name what you want to add")]
        [MaxLength(150)]
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; } // varchar(150), null

        [SwaggerSchema("The City name what you want to add")]
        [MaxLength(150)]
        [Display(Name = "City Name")]
        public string CityName { get; set; } // varchar(150), null

        [SwaggerSchema("The Company Address what you want to add")]
        [MaxLength(500)]
        [Display(Name = "Company Address")]
        public string CompanyAddress { get; set; } // varchar(500), null

        [SwaggerSchema("The Company Address what you want to add")]
        [MaxLength(500)]
        [Display(Name = "Logo")]
        public string CompanyLogoPath { get; set; } // varchar(500), null

        [SwaggerSchema("The Contact PersonName what you want to add")]
        [MaxLength(100)]
        [Required]
        [Display(Name = "Contact Person Name")]
        public string ContactPersonName { get; set; } // varchar(100), null

        [SwaggerSchema("The ContactEmail what you want to add")]
        [MaxLength(100)]
        [Required]
        [EmailAddress]
        [Display(Name = "Contact Email")]
        public string ContactEmail { get; set; } // varchar(100), null

        [SwaggerSchema("The ContactMobileNo what you want to add")]
        [MaxLength(10)]
        [MinLength(10)]
        [Required]
        [Display(Name = "Contact Mobile No")]
        public string ContactMobileNo { get; set; } // varchar(100), null

        [SwaggerSchema("The ContactPhoneNo what you want to add")]
        [MaxLength(100)]
        [Display(Name = "Contact Phone No")]
        public string ContactPhoneNo { get; set; } // varchar(100), null

        [SwaggerSchema("Sample date field")]        
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime? DateCol { get; set; } // date, null

        [SwaggerSchema("Sample time field")]
        [Display(Name = "Time")]
        //[DataType(DataType.Time)]
        public TimeSpan? TimeCol { get; set; } // time(7), null

        [SwaggerSchema("Sample date time field")]
        [Display(Name = "Date Time")]
        [DataType(DataType.DateTime)]
        public DateTime? DateTimeCol { get; set; } // datetime, null

        [SwaggerSchema("Sample radio field")]
        [Display(Name = "Date Time")]        
        public bool? RadioCol { get; set; } // bit, null
        [SwaggerSchema("Sample drop down field")]
        [Display(Name = "Drop Down")]
        public int? DropDownCol { get; set; } // int, null

        [SwaggerSchema("Sample multi select field")]
        [Display(Name = "Multi Select")]
        [MaxLength(100)]
        public string[] MultiSelect { get; set; } // varchar(100), null


    }

    public class DTO_ClientInformationSearch:DTO_SearchBase
    {
        [SwaggerSchema("Client's name")]
        public string ClientName { get; set; }
        
       
    }

    public class ClientInformationListResponseExamples : IExamplesProvider<ApiResponse<List<DTO_ClientInformation>>>
    {
        public ApiResponse<List<DTO_ClientInformation>> GetExamples()
        {

            return new ApiResponse<List<DTO_ClientInformation>>
            {
                IsSuccess = true,
                ReturnMessage = "Client List",
                Data = new List<DTO_ClientInformation>
                        {
                            new DTO_ClientInformation { ClientID=1,IsActive=true,IsDeleted=false,CompanyName="Company Name 1",CityName="City Name 1",CompanyAddress="Company Address 1"
                                                        ,CompanyLogoPath="../../CompanyLogoPath1.jpg",ContactPersonName="Contact Person Name 1",
                                                        ContactEmail="ContactEmail1@example.com",ContactMobileNo="9999999999",ContactPhoneNo="",DateTimeCol=DateTime.Now,DropDownCol=1,
                                                        MultiSelect=new string[]{ "1","2"} },

                             new DTO_ClientInformation { ClientID=2,IsActive=true,IsDeleted=false,CompanyName="Company Name 2",CityName="City Name 21",CompanyAddress="Company Address 2"
                                                        ,CompanyLogoPath="../../CompanyLogoPath2.jpg",ContactPersonName="Contact Person Name 2",
                                                        ContactEmail="ContactEmail2@example.com",ContactMobileNo="1111111111",ContactPhoneNo="",DateTimeCol=DateTime.Now,DropDownCol=1,
                                                        MultiSelect=new string[]{ "1","2"} },
                        }
            };

        }
    }

    public class ClientInformationSingleResponseExamples : IExamplesProvider<ApiResponse<DTO_ClientInformation>>
    {
        public ApiResponse<DTO_ClientInformation> GetExamples()
        {

            return new ApiResponse<DTO_ClientInformation>
            {
                IsSuccess = true,
                ReturnMessage = "Client",
                Data = new DTO_ClientInformation
                {
                    ClientID = 1,
                    IsActive = true,
                    IsDeleted = false,
                    CompanyName = "Company Name 1",
                    CityName = "City Name 1",
                    CompanyAddress = "Company Address 1",
                    CompanyLogoPath = "../../CompanyLogoPath1.jpg",
                    ContactPersonName = "Contact Person Name 1",
                    ContactEmail = "ContactEmail1@example.com",
                    ContactMobileNo = "9999999999",
                    ContactPhoneNo = "",
                    DateTimeCol = DateTime.Now,
                    DropDownCol = 1,
                    MultiSelect = new string[] { "1", "2" }
                }
            };


        }
    }

}
