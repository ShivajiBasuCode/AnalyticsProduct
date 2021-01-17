using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsShared.ContextModels
{
    public partial class AccessGroupAndOperationRelation : RealitycsSharedBase
    {
        public int AccessGroupId { get; set; }
        public int AccessOperationId { get; set; }
        public bool IsActive { get; set; }
      
        public virtual MasterAccessOperation AccessOperation { get; set; }
        public virtual MasterAccessGroup AccessGroup { get; set; }

    }
}
