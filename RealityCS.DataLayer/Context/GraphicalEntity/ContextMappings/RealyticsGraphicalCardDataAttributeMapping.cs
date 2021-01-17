using RealityCS.DataLayer.Context.GraphicalEntity.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealityCS.DataLayer.Context.GraphicalEntity.ContextMappings
{
    class RealyticsGraphicalCardDataAttributeMapping : RealitycsGraphicalEntityTypeConfiguration<RealyticsGraphicalCardDataPlotterAxisAttribute>
    {
        public override void Configure(EntityTypeBuilder<RealyticsGraphicalCardDataPlotterAxisAttribute> entity)
        {
            entity.ToTable(nameof(RealyticsGraphicalCardDataPlotterAxisAttribute));

            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.FK_LegalEntityId)
                .IsRequired();

            entity.Property(x => x.DashboardId)
                .IsRequired();

            entity.Property(x => x.CustomerDataSourceIdentifier)
                .IsRequired();

            entity.Property(x => x.DataPlotterAxisAttribute)
                .HasColumnType("nvarchar(300)")
                .HasMaxLength(300)
                .IsRequired();

            entity.HasOne(a => a.GraphicalCard)
                .WithMany(c => c.DataPlotAxisDataAttributes)
                .HasForeignKey(a => a.FK_CardId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
