using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StudentRegistrationLogin.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Firstname required")]
        [Display(Name = "First Name")]        
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Lastname required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "DOB")]
        public DateTime? DOB { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Rolename required")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        public string ImagePath { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "FullName required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string PrefixName { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }
        public string IsActive { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            this.SecurityStamp = Guid.NewGuid().ToString();
            this.CreatedDateUtc = DateTime.UtcNow;
            //this.IsActive = "1";
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class ApplicationRole : IdentityRole
    {
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }
        public string IsActive { get; set; }
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }
        //public ApplicationRole(string IsActive) : base(IsActive) { }
    }
    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Specialty> Specialty { get; set; }
        public DbSet<DoctorSpeciality> DoctorSpeciality { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<PrefixMaster> PrefixMaster { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public override Task<int> SaveChangesAsync()
        {
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
            {
                if (entry.Entity is Specialty)
                {
                    entry.Property("IsActive").CurrentValue = "0";
                    entry.State = EntityState.Modified;
                }
                if (entry.Entity is DoctorSpeciality)
                {
                    entry.Property("IsActive").CurrentValue = "0";
                    entry.State = EntityState.Modified;
                }
                if (entry.Entity is Department)
                {
                    entry.Property("IsActive").CurrentValue = "0";
                    entry.State = EntityState.Modified;
                }
                if (entry.Entity is ApplicationRole)
                {
                    entry.Property("IsActive").CurrentValue = "0";
                    entry.State = EntityState.Modified;
                }
                

            }
            return base.SaveChangesAsync();
        }
        public override int SaveChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
            {
                
                if (entry.Entity is ApplicationUser)
                {
                    entry.Property("IsActive").CurrentValue = "0";
                    entry.State = EntityState.Modified;
                }
                

            }
            return base.SaveChanges();
        }
        //public System.Data.Entity.DbSet<StudentRegistrationLogin.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<StudentRegistrationLogin.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<StudentRegistrationLogin.Models.RegisterModel> RegisterModels { get; set; }

        //public System.Data.Entity.DbSet<StudentRegistrationLogin.Models.ApplicationUser> Users { get; set; }



        //public System.Data.Entity.DbSet<StudentRegistrationLogin.Models.RoleViewModel> RoleViewModels { get; set; }
    }
}