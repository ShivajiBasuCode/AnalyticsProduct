using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.GraphicalEntity.ContextModels;

namespace RealityCS.DataLayer.Context.GraphicalEntity.ContextMappings
{
    public class RealyticsGraphicalDashboardMapping : RealitycsGraphicalEntityTypeConfiguration<RealyticsGraphicalDashboard>
    {
        public override void Configure(EntityTypeBuilder<RealyticsGraphicalDashboard> entity)
        {
            entity.ToTable(nameof(RealyticsGraphicalDashboard));

            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.FK_LegalEntityId)
                .IsRequired();

            entity.Property(x => x.UsedTemplateId)
                .IsRequired();

            entity.Property(x => x.ValueStreamId)
                .IsRequired();

            entity.Property(x => x.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Description)
                .HasColumnType("nvarchar(300)")
                .HasMaxLength(300);

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
