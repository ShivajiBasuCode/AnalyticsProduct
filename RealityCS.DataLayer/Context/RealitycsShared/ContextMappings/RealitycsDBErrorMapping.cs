namespace RealityCS.DataLayer.Context.RealitycsShared.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
    public class RealitycsDBErrorMapping : RealitycsSharedEntityTypeConfiguration<RealitycsDBError>
    {
        public override void Configure(EntityTypeBuilder<RealitycsDBError> entity)
        {
            entity.ToTable(nameof(RealitycsDBError));

            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.PK_Id)
                  .ValueGeneratedOnAdd();
           
        }
    }
}
