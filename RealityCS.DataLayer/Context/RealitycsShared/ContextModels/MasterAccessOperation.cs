using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsShared.ContextModels
{
    public partial class MasterAccessOperation : RealitycsSharedBase
    {
        public string Name { get; set; }
        public string SystemName {get; set;}
        public string Description { get; set; }
        public int EntityId { get; set; }
        public int EntityGroupId { get; set; }
        public int DomainId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

#warning RelationShip Mapped for EntityId,EntityGroupId,DomainId

        public virtual MasterEntityGroup EntityGroup { get; set; }

        public virtual MasterDomain Domain { get; set; }
        public virtual MasterEntity Entity { get; set; }

        public virtual ICollection<AccessGroupAndOperationRelation> AccessGroupAndOperationRelation { get; set; }
    }
}
