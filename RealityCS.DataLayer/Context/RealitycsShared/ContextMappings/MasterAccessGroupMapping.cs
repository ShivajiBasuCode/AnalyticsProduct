namespace RealityCS.DataLayer.Context.RealitycsShared.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
    public class MasterAccessGroupMapping : RealitycsSharedEntityTypeConfiguration<MasterAccessGroup>
    {
        public override void Configure(EntityTypeBuilder<MasterAccessGroup> entity)
        {
            entity.ToTable(nameof(MasterAccessGroup));

            entity.HasKey(x => x.PK_Id);
            entity.HasIndex(x => x.Name).IsUnique();
            entity.HasIndex(x => x.Description).IsUnique(); ;

            entity.Property(x => x.PK_Id)
                  .UseIdentityColumn(0, 1)
                  .ValueGeneratedOnAdd();
            entity.Property(x => x.Name)
                  .HasColumnType("nvarchar(100)")
                  .IsRequired()
                  .HasMaxLength(100);
            entity.Property(x => x.Description)
                  .HasColumnType("nvarchar(300)")
                  .IsRequired()
                  .HasMaxLength(300);

            entity.Property(x => x.CreatedBy)
                  .IsRequired();
            entity.Property(x => x.CreatedDate)
                 .IsRequired();
            entity.Property(x => x.ModifiedBy);
            entity.Property(x => x.ModifiedDate);
        }
    }
}
