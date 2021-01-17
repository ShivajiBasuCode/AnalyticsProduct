using System;
using System.Collections.Generic;
using System.Text;



namespace RealityCS.DataLayer.Context.KPIEntity.ContextModels
{
    /// <summary>
    /// DB Context for KPI Value Stream Mapping
    /// </summary>
    public class RealyticsKPIValueStream : RealyticsKPIBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
