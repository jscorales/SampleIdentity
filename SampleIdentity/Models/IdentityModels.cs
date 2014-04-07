using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

namespace WebApplication2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AtomUser : IdentityUser<int, AtomUserLogin, AtomUserRole, AtomUserClaim>
    {
        public virtual string ProfileId { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }

        public virtual ICollection<AtomUserCourse> Courses { get; set; }
        public virtual ICollection<AtomUserLoginInfo> LoginInfo { get; set; }
    }

    public class AtomUserCourse
    {
        public virtual int Id { get; set; }
        public virtual string CourseAccessId { get; set; }
        public virtual int AtomUserId { get; set; }

        public virtual AtomUser AtomUser { get; set; }
    }

    public class AtomUserLoginInfo
    {
        public virtual int Id { get; set; }
        public virtual int AtomUserId { get; set; }
        public virtual string JasperSessionKey { get; set; }

        public virtual AtomUser AtomUser { get; set; }
    }

    //Login
    public class AtomUserLogin : IdentityUserLogin<int>
    {

    }

    //Role
    public class AtomUserRole : IdentityUserRole<int>
    {
        public AtomUserRole()
            : base()
        {
        }
    }

    //Atom Role
    public class AtomRole : IdentityRole<int, AtomUserRole>
    {
        public AtomRole()
            : base()
        {
        }

        public AtomRole(string name)
            : base()
        {
            Name = name;
        }
    }

    //Claim
    public class AtomUserClaim : IdentityUserClaim<int>
    {

    }

    public class AtomUserStore : UserStore<AtomUser, AtomRole, int, AtomUserLogin, AtomUserRole, AtomUserClaim>
    {
        public AtomUserStore(AtomDbContext context)
            : base(context)
        {
        }

        public static AtomDbContext Create()
        {
            return new AtomDbContext();
        }
    }

    public class AtomDbContext : IdentityDbContext<AtomUser, AtomRole, int, AtomUserLogin, AtomUserRole, AtomUserClaim>
    {
        public AtomDbContext()
            : base("DefaultConnection")
        {
        }

        public static AtomDbContext Create()
        {
            return new AtomDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Change the name of the table to be Users instead of AspNetUsers
            modelBuilder.Entity<AtomUser>().ToTable("Users");
            modelBuilder.Entity<AtomRole>().ToTable("Roles");
            modelBuilder.Entity<AtomUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<AtomUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<AtomUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<AtomUserLoginInfo>().ToTable("UserLoginInfos");
            modelBuilder.Entity<AtomUserCourse>().ToTable("UserCourse");

            var entity = modelBuilder.Entity<AtomUser>();
            entity.HasKey(x => x.Id);
            entity.Ignore(x => x.EmailConfirmed);
            entity.Ignore(x => x.PhoneNumber);
            entity.Ignore(x => x.PhoneNumberConfirmed);
            entity.Ignore(x => x.TwoFactorEnabled);
            entity.Ignore(x => x.LockoutEnabled);
            entity.Ignore(x => x.LockoutEndDateUtc);
            entity.Ignore(x => x.AccessFailedCount);
            entity.Ignore(x => x.SecurityStamp);
            entity.Ignore(x => x.PasswordHash);

            modelBuilder.Entity<AtomUserCourse>().Property(x => x.AtomUserId).HasColumnName("UserId");
            modelBuilder.Entity<AtomUserLoginInfo>().Property(x => x.AtomUserId).HasColumnName("UserId");
        }

        public virtual DbSet<AtomUserCourse> UserCourses { get; set; }
        public virtual DbSet<AtomUserLoginInfo> UserLoginInfos { get; set; }
    }
}