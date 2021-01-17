using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.GraphicalEntity.ContextModels;

namespace RealityCS.DataLayer.Context.GraphicalEntity.ContextMappings
{
    public class RealyticsGraphicalDashboardTemplateMapping : RealitycsGraphicalEntityTypeConfiguration<RealyticsGraphicalDashboardTemplate>
    {
        public override void Configure(EntityTypeBuilder<RealyticsGraphicalDashboardTemplate> entity)
        {
            entity.ToTable(nameof(RealyticsGraphicalDashboardTemplate));

            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.FK_LegalEntityId)
                .IsRequired();

            entity.Property(x => x.LinkedUITemplateId);

            //Mapping of base properties
            entity.Property(x => x.CreatedBy)
                 .IsRequired();
            entity.Property(x => x.CreatedDate)
                 .IsRequired();
            entity.Property(x => x.ModifiedBy);
            entity.Property(x => x.ModifiedDate);

        }

    }
}
