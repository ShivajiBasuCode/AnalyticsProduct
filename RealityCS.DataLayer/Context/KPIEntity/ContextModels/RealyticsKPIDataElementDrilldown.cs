using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.KPIEntity.ContextModels
{
    public class RealyticsKPIDrilldownElement : RealyticsKPIDataBase
    {
        public int KpiId { get; set; }
        public int FK_KpiDataElementId { get; set; }
        public EnumerationDrilldownOrder DrillDownOrder { get; set; }
        public int CustomerDataElementIdentifier { get; set; } //this wil be same as present in RealytcsJoiningRelation
        public string CustomerDataAttribute { get; set; }
        public int NextDrilldownId { get; set; }

        //public virtual RealyticsKPI Kpi { get; set; }
        public virtual RealyticsKPIDataElement FK_KpiDataElement { get; set; }
    }
}
