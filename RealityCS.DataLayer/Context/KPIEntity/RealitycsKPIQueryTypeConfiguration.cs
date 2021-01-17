using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.KPIEntity
{
    //For register Stored Procedure 
    public class RealitycsKPIQueryTypeConfiguration<TQuery> : IRealitycsKPIMappingConfiguration,
        IEntityTypeConfiguration<TQuery> where TQuery : class
    {

        //For custom configuration
        protected virtual void PostConfigure(EntityTypeBuilder<TQuery> builder)
        {

        }
        public virtual void Configure(EntityTypeBuilder<TQuery> builder)
        {
            this.PostConfigure(builder);
        }
        public void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }
    }
}
