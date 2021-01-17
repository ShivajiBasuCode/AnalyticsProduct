using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealityCS.DataLayer.Context.KPIEntity.ContextMappings
{
    public class RealyticsKPIRiskRegisterMapping : RealitycsKPIEntityTypeConfiguration<RealyticsKPIRiskRegister>
    {
        public override void Configure(EntityTypeBuilder<RealyticsKPIRiskRegister> entity)
        {
            entity.ToTable(nameof(RealyticsKPIRiskRegister));
            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.FK_LegalEntityId)
                .IsRequired();

            entity.Property(x => x.Risk)
                .HasColumnType("nvarchar(100)")
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Description)
                .HasColumnType("nvarchar(300)")
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(x => x.RiskMitigationPlan)
                .HasColumnType("nvarchar(500)")
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(x => x.RiskContiguencyPlan)
                .HasColumnType("nvarchar(500)")
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(x => x.RiskValue)
                .HasColumnType("Decimal(14, 2)");

            entity.HasOne(v => v.KPIValueStreamForContiguency)
                .WithMany()
                .HasForeignKey(v => v.KPIValueStreamForContiguencyId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(v => v.KPIValueStreamForMitigation)
                .WithMany()
                .HasForeignKey(v => v.KPIValueStreamForMitigationId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(x => x.DepartmentId);

            entity.Property(x => x.DivisionId);

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
