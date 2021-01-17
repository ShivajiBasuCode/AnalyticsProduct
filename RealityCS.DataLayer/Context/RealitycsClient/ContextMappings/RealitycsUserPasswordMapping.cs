namespace RealityCS.DataLayer.Context.RealitycsClient.ContextMappings
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;

    public class RealitycsUserPasswordMapping : RealitycsClientEntityTypeConfiguration<RealitycsUserPassword>
    {

        public override void Configure(EntityTypeBuilder<RealitycsUserPassword> entity)
        {
            entity.ToTable(nameof(RealitycsUserPassword));

            entity.HasKey(x => x.PK_Id);
            entity.Property(x => x.PK_Id).UseIdentityColumn(1,1).ValueGeneratedOnAdd();
            
            entity.Property(x => x.CreatedBy)
                .IsRequired();
            entity.Property(x => x.CreatedDate)
                .IsRequired();

            entity.Property(x => x.ModifiedBy);
            entity.Property(x => x.ModifiedDate);

            entity.HasOne(p => p.User)
            .WithMany(b => b.Passwords)
            .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
