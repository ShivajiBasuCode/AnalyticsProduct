using RealityCS.DataLayer.Context.GraphicalEntity;
using RealityCS.DataLayer.Context.KPIEntity;
using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealityCS.DataLayer.Context.GraphicalEntity.ContextModels
{

    public class RealyticsGraphicalCard : RealyticsGraphicalBase
    {
        public int KpiId { get; set; }
        public int FK_DashboardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EnumerationGraphAxis ?ReferenceAxis { get; set; }
        public EnumerationGraphAxis DataPlotterAxis { get; set; }
        public EnumerationSupportedGraphType SelectedGraphType { get; set; }
        public int CustomerDataSourceIdentifier { get; set; }
        public string? ReferenceAxisAttribute { get; set; }
        public virtual List<RealyticsGraphicalCardDataPlotterAxisAttribute> DataPlotAxisDataAttributes { get; set; }
        public virtual RealyticsGraphicalDashboard FK_GraphicalDashboard { get; set; }
    }
}
