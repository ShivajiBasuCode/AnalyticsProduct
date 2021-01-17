using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
using RealityCS.DataLayer.Context.RealitycsClient;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// DB Context for KPI 
/// </summary>
namespace RealityCS.DataLayer.Context.KPIEntity.ContextModels
{
    public class RealyticsKPI : RealyticsKPIBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Objective { get; set; }
        public int ?IndustryId { get; set; }
        public int ?DepartmentId { get; set; }
        public int ?DivisionId { get; set; }
        public int ?KpiValueStreamId { get; set; }
        //public int CustomerDataElementIdentifier { get; set; } //moved to KPI data elements
        public virtual ICollection<RealitycsKPIJoiningRelation> JoiningRelationship { get; set; }

        public virtual ICollection<RealyticsKPIDataElement> DataElements { get; set; }

        public virtual RealyticsKPIValueStream RealitycsKPIValueStream { get; set; }
    }
}
