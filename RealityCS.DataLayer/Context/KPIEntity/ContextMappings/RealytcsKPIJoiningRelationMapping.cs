using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.KPIEntity.ContextMappings
{
    class RealytcsKPIJoiningRelationMapping : RealitycsKPIEntityTypeConfiguration<RealitycsKPIJoiningRelation>
    {
        public override void Configure(EntityTypeBuilder<RealitycsKPIJoiningRelation> entity)
        {
            entity.ToTable(nameof(RealitycsKPIJoiningRelation));
            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.FK_LegalEntityId)
                .IsRequired();

            entity.Property(x => x.JoiningCustomerDataElementIdentifier);
            entity.Property(x => x.JoiningAttribute)
                .HasColumnType("nvarchar(300)")
                .HasMaxLength(300);
            entity.HasIndex(x => x.JoiningRelationship);
            entity.Property(x => x.JoiningCustomerDataElementIdentifierInRelation);
            entity.Property(x => x.JoiningAttributeInRelation)
                .HasColumnType("nvarchar(300)")
                .HasMaxLength(300);

            //Relationship with KPI Elements
            entity.HasOne(x => x.FK_Kpi)
                .WithMany(k => k.JoiningRelationship)
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