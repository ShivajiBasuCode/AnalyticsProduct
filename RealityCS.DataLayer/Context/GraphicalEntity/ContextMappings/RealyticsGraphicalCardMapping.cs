using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealityCS.DataLayer.Context.GraphicalEntity.ContextModels;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;

namespace RealityCS.DataLayer.Context.GraphicalEntity.ContextMappings
{
    public class RealyticsGraphicalCardMapping : RealitycsGraphicalEntityTypeConfiguration<RealyticsGraphicalCard>
    {

        public override void Configure(EntityTypeBuilder<RealyticsGraphicalCard> entity)
        {
            entity.ToTable(nameof(RealyticsGraphicalCard));

            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.FK_LegalEntityId)
                .IsRequired();

            entity.Property(x => x.KpiId)
                .IsRequired();

            entity.Property(x => x.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.Description)
                .HasColumnType("nvarchar(300)")
                .HasMaxLength(300);

            entity.HasIndex(x => x.ReferenceAxis);//.IsUnique();

            entity.Property(x => x.CustomerDataSourceIdentifier)
                .IsRequired();

            entity.Property(x => x.ReferenceAxisAttribute)
                .HasMaxLength(300)
                .HasColumnType("nvarchar(300)");

            entity.HasIndex(x => x.DataPlotterAxis);//.IsUnique();

            //entity.Property(x => x.DataPlotAxisAttributeIds)
            //    .IsRequired();
            /*
                        entity.Property(x => x.SelectedGraphType)
                            .HasConversion(new EnumToNumberConverter<EnumerationSupportedGraphType, int>());*/

            entity.HasIndex(x => x.SelectedGraphType);//.IsUnique();

            entity.HasOne(c => c.FK_GraphicalDashboard)
                .WithMany(d => d.GraphicalCards)
                .HasForeignKey(c => c.FK_DashboardId)
                .OnDelete(DeleteBehavior.Cascade);

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
