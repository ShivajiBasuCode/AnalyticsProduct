namespace RealityCS.DataLayer.Context.RealitycsShared.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
    public class MasterAccessOperationMapping : RealitycsSharedEntityTypeConfiguration<MasterAccessOperation>
    {
        public override void Configure(EntityTypeBuilder<MasterAccessOperation> entity)
        {
            entity.ToTable(nameof(MasterAccessOperation));

            entity.HasKey(x => x.PK_Id);
            entity.HasIndex(x => x.Name);
            entity.HasIndex(x => x.Description); ;

            entity.Property(x => x.PK_Id)
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

            entity.HasOne(x => x.Domain).WithMany().OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.EntityGroup).WithMany().OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.Entity).WithMany().OnDelete(DeleteBehavior.Restrict);

        }
    }
}
