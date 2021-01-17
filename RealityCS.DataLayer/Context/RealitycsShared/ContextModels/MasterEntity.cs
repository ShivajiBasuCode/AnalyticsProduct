using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsShared.ContextModels
{
    public partial class MasterEntity : RealitycsSharedBase
    {    
        public string Name { get; set; }
        public string Description { get; set; }
        public int EntityGroupId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public virtual MasterEntityGroup EntityGroup { get; set; }
    }
}
