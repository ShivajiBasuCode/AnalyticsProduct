using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsShared.ContextMappings
{
    public class RealitycsLogMapping : RealitycsSharedEntityTypeConfiguration<RealitycsLog>
    {
        public override void Configure(EntityTypeBuilder<RealitycsLog> entity)
        {
            entity.ToTable(nameof(RealitycsLog));
            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.PK_Id)
                  .UseIdentityColumn(0, 1)
                  .ValueGeneratedOnAdd();
        }
    }
}
