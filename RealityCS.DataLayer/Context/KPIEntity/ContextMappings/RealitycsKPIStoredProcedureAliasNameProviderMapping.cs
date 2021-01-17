using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.KPIEntity.ContextMappings
{
    public class RealitycsKPIStoredProcedureAliasNameProviderMapping : RealitycsKPIEntityTypeConfiguration<RealitycsKPIStoredProcedureAliasNameProvider>
    {
        public override void Configure(EntityTypeBuilder<RealitycsKPIStoredProcedureAliasNameProvider> entity)
        {
            entity.ToTable(nameof(RealitycsKPIStoredProcedureAliasNameProvider));
            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.FK_LegalEntityId)
                .IsRequired();

            entity.Property(x => x.DataSourceName)
                .HasColumnType("nvarchar(300)")
                .HasMaxLength(300);
            
            entity.Property(x => x.AliasNameAssigned)
                .HasColumnType("nvarchar(20)")
                .HasMaxLength(20);

            entity.Property(x => x.CustomerDataElementIdentifier)
                .IsRequired();
        }
    }
}
