using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.KPIEntity.ContextModels
{
    /// <summary>
    /// DB Context for KPI Risk Register
    /// </summary>
    public class RealyticsKPIRiskRegister : RealyticsKPIBase
    {
        public string Risk { get; set; }
        public string Description { get; set; }
        /*
        public float RiskProbablity { get; set; }
        public EnumerationKPIRiskImpact RiskImpact { get; set; }
        public float RiskValue {get; set; }
        */
        public string RiskMitigationPlan { get; set; }
        public int KPIValueStreamForMitigationId { get; set; }
        public string RiskContiguencyPlan { get; set; }
        public decimal RiskValue { get; set; }
        public int KPIValueStreamForContiguencyId { get; set; }
        public int ?DepartmentId { get; set; }
        public int ?DivisionId { get; set; }


        public virtual RealyticsKPIValueStream KPIValueStreamForMitigation { get; set; }
        public virtual RealyticsKPIValueStream KPIValueStreamForContiguency { get; set; }
    }
}
