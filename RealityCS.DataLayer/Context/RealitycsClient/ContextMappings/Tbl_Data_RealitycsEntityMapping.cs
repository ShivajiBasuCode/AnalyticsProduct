using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextMappings
{
    public class Tbl_Data_RealitycsEntityMapping : RealitycsClientEntityTypeConfiguration<Tbl_Data_RealitycsEntity>
    {

        public override void Configure(EntityTypeBuilder<Tbl_Data_RealitycsEntity> entity)
        {
            entity.ToTable("data.RealitycsEntity");

            entity.HasKey(p => p.PK_Id);

            entity.Property(p => p.EntityName).IsRequired().HasMaxLength(100);
            entity.Property(p => p.EntityDescription).IsRequired().HasMaxLength(300);
            entity.HasOne(x => x.LegalEntity).WithMany().HasForeignKey(y => y.FK_LegalEntityId).OnDelete(DeleteBehavior.Cascade);

        }

    }
    
}
