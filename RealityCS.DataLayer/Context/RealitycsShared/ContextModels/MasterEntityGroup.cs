using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsShared.ContextModels
{
    public partial class MasterEntityGroup : RealitycsSharedBase
    {    
        public string Name { get; set; }
        public string Description { get; set; }
        public int DomainId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public virtual MasterDomain Domain { get; set; }
        public virtual ICollection<MasterEntity> MasterEntities { get; set; }
    }
}
