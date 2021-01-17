namespace RealityCS.DataLayer.Context.RealitycsShared.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
    public class MasterEntityGroupMapping : RealitycsSharedEntityTypeConfiguration<MasterEntityGroup>
    {
        public override void Configure(EntityTypeBuilder<MasterEntityGroup> entity)
        {
            entity.ToTable(nameof(MasterEntityGroup));

            entity.HasKey(p => p.PK_Id);
            entity.HasIndex(p => p.Name);

            entity.Property(p=> p.PK_Id)
                .UseIdentityColumn(1, 1)
                .ValueGeneratedOnAdd();
            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");
            entity.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnType("nvarchar(300)");

            entity.HasOne(p => p.Domain)
                  .WithMany(q => q.EntityGroups)
                  .HasForeignKey(q => q.DomainId);

        }
    }
}
