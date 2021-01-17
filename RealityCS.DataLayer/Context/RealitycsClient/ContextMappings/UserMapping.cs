namespace RealityCS.DataLayer.Context.RealitycsClient.ContextMappings
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;

    public class UserMapping : RealitycsClientEntityTypeConfiguration<User>
    {

        public override void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("User");

            entity.HasKey(x => x.PK_Id);
            entity.HasIndex(x => x.UserName).IsUnique();
            entity.HasIndex(x => x.EmailId).IsUnique();

            entity.Property(x => x.PK_Id).UseIdentityColumn(1,1).ValueGeneratedOnAdd();
            entity.Property(x => x.UserName)
                  .IsRequired()
                  .HasMaxLength(256)
                  .HasColumnType("nvarchar(256)");
            entity.Property(x => x.EmailId)
                .IsRequired()
                .HasMaxLength(320)
                .HasColumnType("nvarchar(320)");
            entity.Property(x => x.Password)
                  .IsRequired()
                  .HasMaxLength(256)
                  .HasColumnType("nvarchar(256)");
            entity.Property(x => x.FK_LegalEntityId)
                .IsRequired();
            entity.Property(x => x.FK_EmployeeId)
                .IsRequired();

            entity.Property(x => x.CreatedBy)
                .IsRequired();
            entity.Property(x => x.CreatedDate)
                .IsRequired();

            entity.Property(x => x.ModifiedBy);
            entity.Property(x => x.ModifiedDate);

            entity.HasOne(p => p.LegalEntity)
            .WithMany(b => b.ClientUsers)
            .HasForeignKey(p => p.FK_LegalEntityId);

           

        }
    }
}
