
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.Identity
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationUser : IdentityUser<Int32>
    {
        /*
        Asp.Net Identity Default Columns

        UserName	            nvarchar(256)	Checked
        NormalizedUserName	    nvarchar(256)	Checked
        Email	                nvarchar(256)	Checked
        NormalizedEmail	        nvarchar(256)	Checked
        EmailConfirmed	        bit	Unchecked
        PasswordHash	        nvarchar(MAX)	Checked
        SecurityStamp	        nvarchar(MAX)	Checked
        ConcurrencyStamp	    nvarchar(MAX)	Checked
        PhoneNumber	            nvarchar(MAX)	Checked
        PhoneNumberConfirmed	bit	Unchecked
        TwoFactorEnabled	    bit	Unchecked
        LockoutEnd	            datetimeoffset(7)	Checked
        LockoutEnabled	        bit	Unchecked
        AccessFailedCount	    int	Unchecked
        */

        public Nullable<Int32> FK_UserID_CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<TimeSpan> CreatedTime { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<Int32> FK_UserID_ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<TimeSpan> ModifiedTime { get; set; }
        public Nullable<Int32> FK_ClientID { get; set; }

        public string Password { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string DisplayUserName { get; set; }
        public string UserImagePath { get; set; }
        public Nullable<bool> IsSuperAdmin { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Device_UUID { get; set; }
       
        // public ICollection<tbl_AUTH_AspNet_UserRoles> UserRoles { get; set; }

        public virtual ICollection<tbl_AUTH_AspNet_UserClaims> Claims { get; set; }
        public virtual ICollection<tbl_AUTH_AspNet_UserLogins> Logins { get; set; }
        public virtual ICollection<tbl_AUTH_AspNet_UserTokens> Tokens { get; set; }
        public virtual ICollection<tbl_AUTH_AspNet_UserRoles> UserRoles { get; set; }

    }

    public class tbl_AUTH_AspNet_Roles : IdentityRole<Int32>
    {
        public Nullable<Int32> FK_UserID_CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<TimeSpan> CreatedTime { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<Int32> FK_UserID_ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<TimeSpan> ModifiedTime { get; set; }
        public Nullable<int> FK_ClientID { get; set; }
        public virtual ICollection<tbl_AUTH_AspNet_UserRoles> UserRoles { get; set; }
        public virtual ICollection<tbl_AUTH_AspNet_RoleClaims> RoleClaims { get; set; }

    }

    public class tbl_AUTH_AspNet_UserRoles : IdentityUserRole<Int32>
    {

        public virtual ApplicationUser User { get; set; }
        public virtual tbl_AUTH_AspNet_Roles Role { get; set; }

    }



    public class tbl_AUTH_AspNet_UserClaims : IdentityUserClaim<Int32>
    {
        public virtual ApplicationUser User { get; set; }
    }

    public class tbl_AUTH_AspNet_UserLogins : IdentityUserLogin<Int32>
    {
        public virtual ApplicationUser User { get; set; }
    }

    public class tbl_AUTH_AspNet_RoleClaims : IdentityRoleClaim<Int32>
    {
        public virtual tbl_AUTH_AspNet_Roles Role { get; set; }
    }

    public class tbl_AUTH_AspNet_UserTokens : IdentityUserToken<Int32>
    {
        public virtual ApplicationUser User { get; set; }
    }



    public class AspNetIdentityDbContext : IdentityDbContext<ApplicationUser, tbl_AUTH_AspNet_Roles, Int32, tbl_AUTH_AspNet_UserClaims, tbl_AUTH_AspNet_UserRoles, tbl_AUTH_AspNet_UserLogins, tbl_AUTH_AspNet_RoleClaims, tbl_AUTH_AspNet_UserTokens>
    {
        public AspNetIdentityDbContext(DbContextOptions<AspNetIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //https://stackoverflow.com/a/48484797
            if (!optionsBuilder.IsConfigured)
            {
                // WDDBProvider envConfig = new WDDBProvider();
                //  envConfig.ConfigureDB(optionsBuilder);

                //optionsBuilder.UseSqlServer("Server=KUNTAL;user id=sa;password=Rattlesnak3;Database=RealityCS_NRTOA;MultipleActiveResultSets=true");

            }
        }


        /**
        *[.NET Core 2.1 Identity get all users with their associated roles](https://stackoverflow.com/a/51005445)
        *
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(b =>
            {


                b.Property(p => p.Id).HasColumnName("PK_UserID").HasColumnType("int").UseIdentityColumn();
                b.Property(p => p.FK_UserID_CreatedBy).IsRequired(false);
                b.Property(p => p.CreatedDate).IsRequired(false);
                b.Property(p => p.CreatedTime).IsRequired(false);
                b.Property(p => p.IsDeleted).IsRequired(false);
                b.Property(p => p.IsActive).IsRequired(false);
                b.Property(p => p.FK_UserID_ModifiedBy).IsRequired(false);
                b.Property(p => p.ModifiedDate).IsRequired(false);
                b.Property(p => p.ModifiedTime).IsRequired(false);
                b.Property(p => p.FK_ClientID).IsRequired(false);

                b.Property(p => p.Password).IsRequired(false);
                b.Property(p => p.Title).IsRequired(false);
                b.Property(p => p.FirstName).IsRequired(false);
                b.Property(p => p.MiddleName).IsRequired(false);
                b.Property(p => p.LastName).IsRequired(false);
                b.Property(p => p.Gender).IsRequired(false);
                b.Property(p => p.MobileNo).IsRequired(false);
                b.Property(p => p.DisplayUserName).IsRequired(false);
                b.Property(p => p.UserImagePath).IsRequired(false);
                b.Property(p => p.IsSuperAdmin).IsRequired(false);
                b.Property(p => p.Latitude).IsRequired(false);
                b.Property(p => p.Longitude).IsRequired(false);
                b.Property(p => p.Device_UUID).IsRequired(false);
               

                // The relationships between User and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each User can have many UserClaims
                b.HasMany(e => e.Claims).WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins).WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens).WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany<tbl_AUTH_AspNet_UserRoles>(e => e.UserRoles).WithOne(e => e.User).HasForeignKey(ur => ur.UserId).IsRequired();
                
                


                b.ToTable("tbl_AUTH_AspNet_UserInformation");
            });


            modelBuilder.Entity<tbl_AUTH_AspNet_Roles>(b =>
            {

                b.Property(p => p.Id).HasColumnName("PK_RoleID").HasColumnType("int");
                

                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles).WithOne(e => e.Role).HasForeignKey(ur => ur.RoleId).IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims).WithOne(e => e.Role).HasForeignKey(rc => rc.RoleId).IsRequired();



                b.ToTable("tbl_AUTH_AspNet_Roles");
            });

            modelBuilder.Entity<tbl_AUTH_AspNet_UserRoles>(b =>
            {


                b.Property(p => p.UserId).HasColumnName("PK_UserID").HasColumnType("int");
                b.Property(p => p.RoleId).HasColumnName("PK_RoleID").HasColumnType("int");

                b.HasKey(ur => new { ur.UserId, ur.RoleId });

                b.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                b.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();


                b.ToTable("tbl_AUTH_AspNet_UserRoles");
            });



            modelBuilder.Entity<tbl_AUTH_AspNet_UserClaims>(b =>
            {
               
                b.Property(p => p.Id).HasColumnType("int");
                b.Property(p => p.UserId).HasColumnName("FK_UserId").HasColumnType("int");

                b.HasOne(uc => uc.User)
                  .WithMany(u => u.Claims)
                  .HasForeignKey(ur => ur.UserId)
                  .IsRequired();


                b.ToTable("tbl_AUTH_AspNet_UserClaims");
            });

            modelBuilder.Entity<tbl_AUTH_AspNet_RoleClaims>(b =>
            {

                b.Property(p => p.Id).HasColumnType("int");
                b.Property(p => p.RoleId).HasColumnName("FK_RoleID").HasColumnType("int");              

                b.ToTable("tbl_AUTH_AspNet_RoleClaims");
            });

            modelBuilder.Entity<tbl_AUTH_AspNet_UserLogins>(b =>
            {


                b.Property(p => p.UserId).HasColumnName("FK_UserId").HasColumnType("Int");
                b.ToTable("tbl_AUTH_AspNet_UserLogins");

            });

            modelBuilder.Entity<tbl_AUTH_AspNet_UserTokens>(b =>
            {

                b.Property<Int32>(p => p.UserId);

                b.ToTable("tbl_AUTH_AspNet_UserTokens");
            });
        }
    }

}
