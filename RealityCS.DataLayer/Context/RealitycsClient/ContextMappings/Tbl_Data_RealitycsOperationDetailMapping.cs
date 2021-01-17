using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextMappings
{
    public class Tbl_Data_RealitycsOperationDetailMapping : RealitycsClientEntityTypeConfiguration<Tbl_Data_RealitycsOperationDetail>
    {

        public override void Configure(EntityTypeBuilder<Tbl_Data_RealitycsOperationDetail> entity)
        {
            try
            {
                entity.ToTable("data.RealitycsOperationDetail");
                entity.HasKey(p => p.PK_Id);
            }
            catch(Exception ex)
            {

            }
        }

    }
    
}
