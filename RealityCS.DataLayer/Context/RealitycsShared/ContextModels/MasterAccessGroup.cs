using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsShared.ContextModels
{
    public partial class MasterAccessGroup : RealitycsSharedBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LegalEntityId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
      
        public virtual ICollection<AccessGroupAndOperationRelation> AccessGroupAndOperationRelation { get; set; }
    }
}
