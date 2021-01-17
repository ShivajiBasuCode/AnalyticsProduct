using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextModels
{
    public partial class Tbl_Data_RealitycsEntity : RealitycsClientBase
    {
        public int FK_LegalEntityId { get; set; }
        public string EntityName { get; set; }
        public string EntityDescription { get; set; }
        public int FK_EntityGroupId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual List<Tbl_Data_RealitycsAttribute> Attributes {get; set;}
        public virtual LegalEntity LegalEntity { get; set; }

    }
}
