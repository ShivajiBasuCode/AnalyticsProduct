﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsEnumeration.ContextMappings
{
    public class EnumerationSupportedGraphTypeMapping : RealitycsEnumerationEntityTypeConfiguration<EnumerationSupportedGraphTypesTable>
    {
        public override void Configure(EntityTypeBuilder<EnumerationSupportedGraphTypesTable> entity)
        {
            entity.ToTable("RealitycsSupportedGraphType");
            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.PK_Id).ValueGeneratedNever();
            entity.Property(x => x.Name).HasMaxLength(255);
            entity.HasIndex(x => x.Name).IsUnique();
            entity.Property(x => x.Description).HasMaxLength(2000).IsRequired();

            base.Configure(entity);
        }
    }
}
