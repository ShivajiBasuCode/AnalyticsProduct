using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.KPIEntity
{
    public class RealitycsKPIEntityTypeConfiguration<TEntity> : IRealitycsKPIMappingConfiguration,
        IEntityTypeConfiguration<TEntity> where TEntity : RealyticsKPIBase
    {

        //For custom configuration
        protected virtual void PostConfigure(EntityTypeBuilder<TEntity> builder)
        {

        }
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            this.PostConfigure(builder);
        }
        public void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }
    }
}
