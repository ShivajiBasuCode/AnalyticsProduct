using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextMappings
{
    public class Tbl_Data_RealitycsAttributeIntValueMapping : RealitycsClientEntityTypeConfiguration<Tbl_Data_RealitycsAttributeIntValue>
    {

        public override void Configure(EntityTypeBuilder<Tbl_Data_RealitycsAttributeIntValue> entity)
        {
            try
            {
                entity.ToTable("data.RealitycsAttributeIntValue");
                entity.HasKey(p => p.PK_Id);

                entity.Property(p => p.Value).IsRequired();

                entity.HasOne(x => x.FK_Entity).WithMany().HasForeignKey(y => y.FK_EntityId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.FK_Attribute).WithMany(y=>y.IntValue).HasForeignKey(y => y.FK_AttributeId).OnDelete(DeleteBehavior.Cascade);
            }
            catch(Exception ex)
            {

            }
        }

    }
    
}
