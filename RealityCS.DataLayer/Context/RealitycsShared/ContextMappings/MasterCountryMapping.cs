namespace RealityCS.DataLayer.Context.RealitycsShared.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
    public class MasterCountryMapping : RealitycsSharedEntityTypeConfiguration<MasterCountry>
    {
        public override void Configure(EntityTypeBuilder<MasterCountry> entity)
        {
            entity.ToTable(nameof(MasterCountry));

            entity.HasKey(x => x.PK_Id);

            entity.HasIndex(p => p.DisplayOrder);
            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
            entity.Property(x => x.TwoLetterIsoCode).HasMaxLength(2);
            entity.Property(x => x.PhoneCode).HasMaxLength(8);
            entity.Property(x => x.CreatedBy)
                 .IsRequired();
            entity.Property(x => x.CreatedDate).HasDefaultValueSql("GETUTCDATE()")
                 .IsRequired();
            entity.Property(x => x.ModifiedBy);
            entity.Property(x => x.ModifiedDate);
        }
    }
}
