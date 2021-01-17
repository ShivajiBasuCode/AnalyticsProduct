namespace RealityCS.DataLayer.Context.RealitycsShared.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealityCS.DataLayer.Context.RealitycsClient;
    using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
    public class LegalEntityMapping : RealitycsClientEntityTypeConfiguration<LegalEntity>
    {
        public override void Configure(EntityTypeBuilder<LegalEntity> entity)
        {
            entity.ToTable(nameof(LegalEntity));

            entity.HasKey(x => x.PK_Id);
            entity.HasIndex(x => x.Name).IsUnique();
            entity.HasIndex(x => x.PrimaryEmailId).IsUnique();

            entity.Property(x => x.PK_Id).UseIdentityColumn(1, 1).ValueGeneratedOnAdd();
            entity.Property(x => x.Name)
                  .IsRequired()
                  .HasMaxLength(256)
                  .HasColumnType("nvarchar(256)");
            
            /**
             * In addition to restrictions on syntax, there is a length limit on email addresses. 
             * That limit is a maximum of 64 characters (octets) in the "local part" (before the "@") 
             * and a maximum of 255 characters (octets) in the domain part (after the "@") 
             * for a total length of 320 characters.
             * */
            entity.Property(x => x.PrimaryEmailId)
                .IsRequired()
                .HasMaxLength(320)
                .HasColumnType("nvarchar(320)");

            /**
             * An LEI consists of a 20-character alphanumeric string, 
             * with the first four characters identifying the local operating unit (LOU) that issued the LEI. 
             * Characters 5-18 are the unique alphanumeric string assigned to the organisation by the LOU.
             */
            entity.Property(x => x.LegalEntityIdentifier)
                .HasMaxLength(30)
                .HasColumnType("nvarchar(30)");

            /**
             * The International Telecommunication Union (ITU) has established a comprehensive numbering plan, 
             * designated E. 164, for uniform interoperability of the networks of its member state or regional administrations. 
             * It is an open numbering plan, however, imposing a maximum length of 15 digits to telephone numbers
             */
            entity.Property(x => x.PrimaryPhoneNumber)
                .HasMaxLength(15)
                .HasColumnType("nvarchar(15)")
                .IsRequired();

            entity.Property(x => x.Address)
                .HasMaxLength(200)
                .HasColumnType("nvarchar(200)");

            entity.Property(x => x.CountryCodeOfOperation)
                .IsRequired();

            entity.Property(x => x.IndustryId)
                .IsRequired();

            entity.Property(x => x.LogoFileName)
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            /**
             * Each element of a domain name separated by [.] is called a “label.” 
             * The maximum length of each label is 63 characters, 
             * and a full domain name can have a maximum of 253 characters.
             */
            entity.Property(x => x.WebSite)
                .HasMaxLength(253)
                .HasColumnType("nvarchar(253)");

            entity.Property(x => x.CurrencyId)
                .IsRequired();
              

            //Mapping of base properties
            entity.Property(x => x.CreatedBy)
                .IsRequired();
            entity.Property(x => x.CreatedDate)
                .IsRequired();

            entity.Property(x => x.ModifiedBy);
            entity.Property(x => x.ModifiedDate);

        }
    }
}
