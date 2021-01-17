using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextMappings
{
    public class Tbl_Data_RealitycsAttributeDateTimeValueMapping : RealitycsClientEntityTypeConfiguration<Tbl_Data_RealitycsAttributeDateTimeValue>
    {

        public override void Configure(EntityTypeBuilder<Tbl_Data_RealitycsAttributeDateTimeValue> entity)
        {
            try
            {
                entity.ToTable("data.RealitycsAttributeDateTimeValue");
                entity.HasKey(p => p.PK_Id);

                entity.Property(p => p.Value).HasColumnType("datetime").IsRequired();

                entity.HasOne(x => x.FK_Entity).WithMany().HasForeignKey(y => y.FK_EntityId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.FK_Attribute).WithMany(y=>y.DateTimeValue).HasForeignKey(y => y.FK_AttributeId).OnDelete(DeleteBehavior.Cascade);
            }
            catch(Exception ex)
            {

            }
        }

    }
    
}
