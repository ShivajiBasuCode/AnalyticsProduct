using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.KPIEntity.ContextModels;

namespace RealityCS.DataLayer.Context.KPIEntity.ContextMappings
{

    public class RealyticsKPIDataElementMapping : RealitycsKPIEntityTypeConfiguration<RealyticsKPIDataElement>
    {
        public override void Configure(EntityTypeBuilder<RealyticsKPIDataElement> entity)
        {
            entity.ToTable(nameof(RealyticsKPIDataElement));
            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.FK_LegalEntityId)
                .IsRequired();

            entity.Property(x => x.CustomerDataElementIdentifierOne);
            entity.Property(x => x.CustomerDataAttributeOne)
                .HasColumnType("nvarchar(300)")
                .HasMaxLength(300)
                .IsRequired();

            entity.Property(x => x.CustomerDataElementIdentifierTwo);
            entity.Property(x => x.CustomerDataAttributeTwo)
                .HasColumnType("nvarchar(300)")
                .HasMaxLength(300);

            //entity.HasIndex(x => x.DataElementInformation);//.IsUnique();
 
            entity.Property(x => x.BenchmarkValue);

            entity.Property(x => x.RedThresholdValue);

            entity.Property(x => x.AmberThreshholdValue);

            entity.Property(x => x.GreenThresholdValue);

            entity.HasIndex(x => x.FormulaToBeApplied);//.IsUnique();

             entity.Property(x => x.AccessGroupId);


            //Relationship with KPI Elements
            entity.HasOne(x => x.FK_Kpi)
                .WithMany(k => k.DataElements)
                .HasForeignKey(x => x.FK_KpiId)
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
