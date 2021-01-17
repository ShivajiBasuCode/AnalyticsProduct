using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.KPIEntity.ContextModels
{
    public class RealitycsKPIStoredProcedureAliasNameProvider : RealyticsKPIBase
    {
        public int CustomerDataElementIdentifier { get; set; }
        public string AliasNameAssigned { get; set; }
        public string DataSourceName { get; set; }

        public int FK_KpiId { get; set; }
        public virtual RealyticsKPI FK_Kpi { get; set; }
    }
}
