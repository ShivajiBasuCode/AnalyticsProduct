using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.GraphicalEntity;
using RealityCS.DataLayer.Context.GraphicalEntity.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.GraphicalEntity.ContextMappings
{
    public class RealyticsGraphicalDashboardNavigationInVisualisationMapping : RealitycsGraphicalQueryTypeConfiguration<RealyticsGraphicalDashboardNavigationInVisualisation>
    {
        public override void Configure(EntityTypeBuilder<RealyticsGraphicalDashboardNavigationInVisualisation> entity)
        {
            entity.HasNoKey();
            entity.ToView(nameof(RealyticsGraphicalDashboardNavigationInVisualisation));
            base.Configure(entity);
        }
    }
}
