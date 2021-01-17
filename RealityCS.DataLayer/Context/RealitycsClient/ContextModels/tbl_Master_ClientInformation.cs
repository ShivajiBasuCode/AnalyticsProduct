using System;
using System.Collections.Generic;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextModels
{
    public partial class tbl_Master_ClientInformation :   RealitycsClientBase
    {
        public int PK_ClientID { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public TimeSpan? CreatedTime { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public TimeSpan? ModifiedTime { get; set; }
        public string CompanyName { get; set; }
        public string CityName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyLogoPath { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactEmailID { get; set; }
        public string ContactMobileNo { get; set; }
        public string ContactPhoneNo { get; set; }
        public DateTime? DateCol { get; set; }
        public TimeSpan? TimeCol { get; set; }
        public DateTime? DateTimeCol { get; set; }
        public bool? RadioCol { get; set; }
        public int? DropDownCol { get; set; }
        public string MultiSelect { get; set; }
    }
}
