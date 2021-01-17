using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsClient.ContextMappings
{
    public class MasterClientInformationMapping:RealitycsClientEntityTypeConfiguration<tbl_Master_ClientInformation>
    {
        public override void Configure(EntityTypeBuilder<tbl_Master_ClientInformation> entity)
        {
            entity.ToTable(nameof(tbl_Master_ClientInformation));
            entity.HasKey(e => e.PK_ClientID);

            entity.Property(e => e.CityName)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.CompanyAddress)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.CompanyLogoPath)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.CompanyName)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.ContactEmailID)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ContactMobileNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ContactPersonName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ContactPhoneNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CreatedDate).HasColumnType("date");

            entity.Property(e => e.DateCol).HasColumnType("date");

            entity.Property(e => e.DateTimeCol).HasColumnType("datetime");

            entity.Property(e => e.ModifiedDate).HasColumnType("date");

            entity.Property(e => e.MultiSelect)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
