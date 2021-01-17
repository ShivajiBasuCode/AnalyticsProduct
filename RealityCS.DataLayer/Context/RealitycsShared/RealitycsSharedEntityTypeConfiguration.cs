using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsShared
{
    public class RealitycsSharedEntityTypeConfiguration<TEntity> : IRealitycsSharedMappingConfiguration,
        IEntityTypeConfiguration<TEntity> where TEntity : RealitycsSharedBase
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
