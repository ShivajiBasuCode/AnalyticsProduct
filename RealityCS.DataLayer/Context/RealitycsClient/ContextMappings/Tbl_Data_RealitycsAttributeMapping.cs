using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextMappings
{
    public class Tbl_Data_RealitycsAttributeMapping : RealitycsClientEntityTypeConfiguration<Tbl_Data_RealitycsAttribute>
    {

        public override void Configure(EntityTypeBuilder<Tbl_Data_RealitycsAttribute> entity)
        {
            try
            {
                entity.ToTable("data.RealitycsAttribute");
                entity.HasKey(p => p.PK_Id);

                entity.Property(p => p.AttributeName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.AttributeDescription).IsRequired().HasMaxLength(300);


                entity.Property(x => x.FK_EntityId).IsRequired();
                entity.HasOne(x => x.Entity).WithMany(y=>y.Attributes).HasForeignKey(y => y.FK_EntityId).OnDelete(DeleteBehavior.Cascade);
                //entity.HasOne(x => x.FK_EntityGroup).WithMany().HasForeignKey(y => y.FK_EntityGroupId).OnDelete(DeleteBehavior.Cascade);
            }
            catch(Exception ex)
            {

            }
        }

    }
    
}
