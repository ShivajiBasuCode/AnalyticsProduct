using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.GraphicalEntity.ContextModels
{
    public class RealyticsGraphicalCardDataPlotterAxisAttribute : RealyticsGraphicalBase
    {
        public int DashboardId { get; set; }
        public int FK_CardId { get; set; }
        public int CustomerDataSourceIdentifier { get; set; }
        public string DataPlotterAxisAttribute { get; set; }
        public virtual RealyticsGraphicalCard GraphicalCard { get; set; }
    }
}
