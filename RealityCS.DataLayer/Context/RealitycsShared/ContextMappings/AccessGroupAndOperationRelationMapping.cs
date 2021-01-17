namespace RealityCS.DataLayer.Context.RealitycsShared.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
    public class AccessGroupAndOperationRelationMapping : RealitycsSharedEntityTypeConfiguration<AccessGroupAndOperationRelation>
    {
        public override void Configure(EntityTypeBuilder<AccessGroupAndOperationRelation> entity)
        {
            entity.ToTable(nameof(AccessGroupAndOperationRelation));

            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.PK_Id)
                  .ValueGeneratedOnAdd();
           

            entity.Property(x => x.CreatedBy)
                  .IsRequired();
            entity.Property(x => x.CreatedDate)
                 .IsRequired();
            entity.Property(x => x.ModifiedBy);
            entity.Property(x => x.ModifiedDate);

            entity.HasOne(x => x.AccessGroup).WithMany(y=>y.AccessGroupAndOperationRelation).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.AccessOperation).WithMany(y=>y.AccessGroupAndOperationRelation).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
