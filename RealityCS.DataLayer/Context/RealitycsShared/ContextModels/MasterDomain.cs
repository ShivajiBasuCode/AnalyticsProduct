using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsShared.ContextModels
{
    public partial class MasterDomain : RealitycsSharedBase
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsRequireLicense { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<MasterEntityGroup> EntityGroups { get; set; }
    }
}
