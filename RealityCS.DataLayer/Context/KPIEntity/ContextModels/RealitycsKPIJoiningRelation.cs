using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.KPIEntity.ContextModels
{
    public class RealitycsKPIJoiningRelation : RealyticsKPIBase
    {
        public int JoiningCustomerDataElementIdentifier { get; set; }//Data source
        public string JoiningAttribute { get; set; }
        public EnumerationJoinTypes JoiningRelationship { get; set; }
        public int JoiningCustomerDataElementIdentifierInRelation { get; set; } //Data source
        public string JoiningAttributeInRelation { get; set; }

        public int FK_KpiId { get; set; }
        public virtual RealyticsKPI FK_Kpi { get; set; }
    }
}
