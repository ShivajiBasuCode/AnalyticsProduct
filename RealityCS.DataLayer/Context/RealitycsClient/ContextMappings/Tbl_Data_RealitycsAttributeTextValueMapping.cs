using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextMappings
{
    public class Tbl_Data_RealitycsAttributeTextValueMapping : RealitycsClientEntityTypeConfiguration<Tbl_Data_RealitycsAttributeTextValue>
    {

        public override void Configure(EntityTypeBuilder<Tbl_Data_RealitycsAttributeTextValue> entity)
        {
            try
            {
                entity.ToTable("data.RealitycsAttributeTextValue");
                entity.HasKey(p => p.PK_Id);

                entity.Property(p => p.Value).IsRequired();
                entity.Ignore(p => p.DataType);
                entity.HasOne(x => x.FK_Entity).WithMany().HasForeignKey(y => y.FK_EntityId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(x => x.FK_Attribute).WithMany(y=>y.TextValue).HasForeignKey(y => y.FK_AttributeId).OnDelete(DeleteBehavior.Cascade);
            }
            catch(Exception ex)
            {

            }
        }

    }
    
}
