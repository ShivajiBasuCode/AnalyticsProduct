namespace RealityCS.DataLayer.Context.RealitycsShared.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
    public class MasterStateMapping : RealitycsSharedEntityTypeConfiguration<MasterState>
    {
        public override void Configure(EntityTypeBuilder<MasterState> entity)
        {
            entity.ToTable(nameof(MasterState));

            entity.HasKey(x => x.PK_Id);

            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
            entity.Property(x => x.CreatedBy)
                 .IsRequired();
            entity.Property(x => x.CreatedDate).HasDefaultValueSql("GETUTCDATE()")
                 .IsRequired();
            entity.Property(x => x.ModifiedBy);
            entity.Property(x => x.ModifiedDate);

            entity.HasOne(state => state.Country)
                .WithMany(country => country.States)
                .HasForeignKey(state => state.CountryId)
                .IsRequired();

        }
    }
}
