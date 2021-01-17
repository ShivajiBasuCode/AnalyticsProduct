using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.KPIEntity.ContextModels;


namespace RealityCS.DataLayer.Context.KPIEntity.ContextMappings
{

    public class RealyticsKPIMapping : RealitycsKPIEntityTypeConfiguration<RealyticsKPI>
    {
        public override void Configure(EntityTypeBuilder<RealyticsKPI> entity)
        {
            entity.ToTable(nameof(RealyticsKPI));
            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.FK_LegalEntityId)
                .IsRequired();
            entity.Property(x => x.IndustryId);
            entity.Property(x => x.DepartmentId);
            entity.Property(x => x.DivisionId);

/* moved to KPI data elements
            entity.Property(x => x.CustomerDataElementIdentifier)
                .IsRequired();
*/

            entity.Property(x => x.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.Description)
                .HasColumnType("nvarchar(300)")
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(x => x.Objective)
                .HasColumnType("nvarchar(200)")
                .IsRequired()
                .HasMaxLength(200);

            entity.HasOne(v => v.RealitycsKPIValueStream)
                .WithMany()
                .HasForeignKey(v => v.KpiValueStreamId)
                .OnDelete(DeleteBehavior.SetNull);

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
