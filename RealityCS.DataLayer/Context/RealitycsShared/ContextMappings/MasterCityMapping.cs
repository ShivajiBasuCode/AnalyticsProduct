namespace RealityCS.DataLayer.Context.RealitycsShared.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
    public class MasterCityMapping : RealitycsSharedEntityTypeConfiguration<MasterCity>
    {
        public override void Configure(EntityTypeBuilder<MasterCity> entity)
        {
            entity.ToTable(nameof(MasterCity));

            entity.HasKey(x => x.PK_Id);

            entity.Property(p => p.Name).HasMaxLength(500).IsRequired();
            entity.Property(x => x.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            entity.HasOne(p => p.States)
             .WithMany(p => p.Cities)
             .HasForeignKey(p => p.StateId)
             .IsRequired();

        }
    }
}
