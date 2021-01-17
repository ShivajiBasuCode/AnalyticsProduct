using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextModels
{
   public partial class Tbl_Graphical_RealitycsEntity : RealitycsClientBase
    {
        public int PK_EntityId { get; set; }
        public int FK_LegalEntityId { get; set; }
        public string EntityName { get; set; }
        public string EntityDescription { get; set; }
        public int FK_EntityGroupId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
