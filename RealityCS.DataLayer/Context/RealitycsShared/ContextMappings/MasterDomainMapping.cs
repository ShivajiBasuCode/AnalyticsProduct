namespace RealityCS.DataLayer.Context.RealitycsShared.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
    public class MasterDomainMapping : RealitycsSharedEntityTypeConfiguration<MasterDomain>
    {
        public override void Configure(EntityTypeBuilder<MasterDomain> entity)
        {
            entity.ToTable(nameof(MasterDomain));

            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)")
                .IsUnicode(false);
            entity.Property(x => x.Description)
                .HasMaxLength(300)
                .HasColumnType("nvarchar(300)")
                .IsUnicode(false);

            entity.Property(x => x.CreatedBy)
                 .IsRequired();
            entity.Property(x => x.CreatedDate)
                 .IsRequired();
            entity.Property(x => x.ModifiedBy);
            entity.Property(x => x.ModifiedDate);
        }
    }
}
