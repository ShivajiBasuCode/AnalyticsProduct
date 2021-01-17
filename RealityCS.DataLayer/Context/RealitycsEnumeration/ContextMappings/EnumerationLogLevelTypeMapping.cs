using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.RealitycsEnumeration;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextMappings
{
    public class EnumerationLogLevelTypeMapping : RealitycsEnumerationEntityTypeConfiguration<EnumerationLogLevelTypeTable>
    {
        public override void Configure(EntityTypeBuilder<EnumerationLogLevelTypeTable> entity)
        {
            entity.ToTable("LogLevelType");
            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.PK_Id).ValueGeneratedNever();
            entity.Property(x => x.Name).HasMaxLength(255);
            entity.HasIndex(x => x.Name).IsUnique();
            entity.Property(x => x.Description).HasMaxLength(2000).IsRequired();
            base.Configure(entity);
        }
    }
}
