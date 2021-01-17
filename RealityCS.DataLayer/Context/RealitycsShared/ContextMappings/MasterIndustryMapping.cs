using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsShared.ContextMappings
{
    public class MasterIndustryMapping : RealitycsSharedEntityTypeConfiguration<MasterIndustry>
    {
        public override void Configure(EntityTypeBuilder<MasterIndustry> entity)
        {
            entity.ToTable(nameof(MasterIndustry));

            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)")
                .IsUnicode(false);

            entity.Property(x => x.Description)
                .HasMaxLength(300)
                .HasColumnType("nvarchar(300)")
                .IsUnicode(false);

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