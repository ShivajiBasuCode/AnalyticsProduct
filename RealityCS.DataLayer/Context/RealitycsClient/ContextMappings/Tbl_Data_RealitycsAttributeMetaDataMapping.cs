using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextMappings
{
    public class Tbl_Data_RealitycsAttributeMetaDataMapping : RealitycsClientEntityTypeConfiguration<Tbl_Data_RealitycsAttributeMetaData>
    {

        public override void Configure(EntityTypeBuilder<Tbl_Data_RealitycsAttributeMetaData> entity)
        {
            try
            {
                entity.ToTable("data.RealitycsAttributeMetaData");
                entity.HasKey(p => p.PK_Id);

                entity.HasOne(x => x.Attribute)
                   .WithOne(y => y.AttributeMetaData)
                   .HasForeignKey<Tbl_Data_RealitycsAttributeMetaData>(x => x.FK_AttributeId);
                //entity.HasOne(x => x.FK_EntityGroup).WithMany().HasForeignKey(y => y.FK_EntityGroupId).OnDelete(DeleteBehavior.Cascade);
            }
            catch(Exception ex)
            {

            }
        }

    }
    
}
