using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealityCS.DataLayer.Context.KPIEntity.ContextMappings
{
    public class RealyticsKPIValueStreamMapping : RealitycsKPIEntityTypeConfiguration<RealyticsKPIValueStream>
    {
        public override void Configure(EntityTypeBuilder<RealyticsKPIValueStream> entity)
        {
            entity.ToTable(nameof(RealyticsKPIValueStream));
            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.FK_LegalEntityId)
                .IsRequired();

            entity.Property(x => x.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Description)
                .HasColumnType("nvarchar(300)")
                .IsRequired()
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
