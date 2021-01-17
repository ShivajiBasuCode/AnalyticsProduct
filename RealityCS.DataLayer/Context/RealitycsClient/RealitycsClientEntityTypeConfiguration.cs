using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient
{
    public class RealitycsClientEntityTypeConfiguration<TEntity> : IRealitycsClientMappingConfiguration,
        IEntityTypeConfiguration<TEntity> where TEntity : RealitycsClientBase
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
