using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextMappings
{
    public class Tbl_Graphical_RealitycsEntityMapping : RealitycsClientEntityTypeConfiguration<Tbl_Graphical_RealitycsEntity>
    {

        public override void Configure(EntityTypeBuilder<Tbl_Graphical_RealitycsEntity> entity)
        {
            entity.ToTable(nameof(Tbl_Graphical_RealitycsEntity));

            entity.HasKey(p => p.PK_EntityId);

            entity.Property(p => p.EntityName).IsRequired().HasMaxLength(100);
            entity.Property(p => p.EntityDescription).IsRequired().HasMaxLength(300);

           
        }
      
    }
    
}
