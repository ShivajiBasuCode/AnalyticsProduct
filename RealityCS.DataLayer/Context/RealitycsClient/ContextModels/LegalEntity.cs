using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextModels
{
    public partial class LegalEntity : RealitycsClientBase
    {
        public string Name { get; set; }
        public string ?LegalEntityIdentifier { get; set; }
        public string PrimaryEmailId { get; set; }
        public string PrimaryPhoneNumber { get; set; }
        public string ?Address { get; set; }
        public int CountryCodeOfOperation { get; set; }
        public int IndustryId { get; set; }
        public string LogoFileName { get; set; }
        public string ?WebSite { get; set; }
        public int CurrencyId { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<User> ClientUsers { get; set; }

    }
}
