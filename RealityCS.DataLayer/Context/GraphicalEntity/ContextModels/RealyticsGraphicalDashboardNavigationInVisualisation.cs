using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.GraphicalEntity.ContextModels
{
    public class RealyticsGraphicalDashboardNavigationInVisualisation
    {
        public int DashboardId { get; set; }
        public int ValuestreamId { get; set; }
        public string DashboardName { get; set; }
        public string DashboardDescription { get; set; }
        public string ValueStreamName { get; set; }
        public string ValueStreamDescription { get; set; }

    }
}
