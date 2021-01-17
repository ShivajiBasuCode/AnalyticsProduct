using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;

namespace RealityCS.DataLayer.Context.KPIEntity.ContextMappings
{

    public class RealyticsKPIDataElementDrilldownMapping : RealitycsKPIEntityTypeConfiguration<RealyticsKPIDrilldownElement>
    {
        public override void Configure(EntityTypeBuilder<RealyticsKPIDrilldownElement> entity)
        {
            entity.ToTable(nameof(RealyticsKPIDrilldownElement));
            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.FK_LegalEntityId)
                .IsRequired();

            entity.Property(x => x.CustomerDataAttribute)
                .HasColumnType("nvarchar(300)")
                .HasMaxLength(300)
                .IsRequired();

            entity.HasIndex(x => x.DrillDownOrder);//.IsUnique();
/*            entity.Property(x => x.DrillDownOrder)
            .HasConversion(new EnumToStringConverter<EnumerationDrilldownOrder>());*/

            entity.Property(x => x.NextDrilldownId);

            entity.Property(x => x.KpiId)
                .IsRequired();
            //Relationship with KPI
            /*entity.HasOne(k => k.Kpi)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);*/

            //Relationship with KPI Data Element
            entity.HasOne(dd => dd.FK_KpiDataElement)
                .WithMany(de => de.KpiDrilldownElements)
                .HasForeignKey(dd => dd.FK_KpiDataElementId)
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
