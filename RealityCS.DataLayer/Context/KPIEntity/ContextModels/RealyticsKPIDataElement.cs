using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;
using static RealityCS.DataLayer.Context.KPIEntity.ContextModels.RealyticsKPIDrilldownElement;

namespace RealityCS.DataLayer.Context.KPIEntity.ContextModels
{
    /// <summary>
    /// DB Context to store all the data source elements for KPI
    /// </summary>
    public class RealyticsKPIDataElement:RealyticsKPIDataBase
    {
        public int CustomerDataElementIdentifierOne { get; set; } //this wil be same as present in RealytcsJoiningRelation
        public string CustomerDataAttributeOne { get; set; }
        public int CustomerDataElementIdentifierTwo { get; set; }
        public string ?CustomerDataAttributeTwo { get; set; }
        public bool UsedForTimeStampFilter { get; set; }
        //public EnumerationKPIDataElementInformation DataElementInformation { get; set; }
        public int BenchmarkValue { get; set; }
        public int RedThresholdValue { get; set; }
        public int AmberThreshholdValue { get; set; }
        public int GreenThresholdValue { get; set; }
        public EnumerationKPIFormulas FormulaToBeApplied { get; set; }
        public virtual List<RealyticsKPIDrilldownElement> KpiDrilldownElements { get; set; }
        public int AccessGroupId { get; set; }

        public int FK_KpiId { get; set; }
        public virtual RealyticsKPI FK_Kpi { get; set; }

    }
}
