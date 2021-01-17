using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.GraphicalEntity.ContextModels
{
    public class RealyticsGraphicalDashboard : RealyticsGraphicalBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UsedTemplateId { get; set; }
        public int ValueStreamId { get; set; }
        public virtual List<RealyticsGraphicalCard> GraphicalCards { get; set; }
    }
}
