using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using StudentRegistrationLogin.Models;

[assembly: OwinStartupAttribute(typeof(StudentRegistrationLogin.Startup))]
namespace StudentRegistrationLogin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
        private void createDefaultUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!roleManager.RoleExists("Admin"))
            {
                var user = new ApplicationUser();
                user.Email = "shivanimahadevwala1996@gmail.com";
                user.FirstName = "Sonu";
                user.LastName = "Mahadevwala";
                user.ImagePath = "/UploadedImage/female-icon.jpg";
                user.RoleName = "Admin";
                user.EmailConfirmed = true;
                user.UserName = "Shivmahadevwala1716@gmail.com";
                user.FullName = "Sonu Mahadev";
                string userPWD = "julsqgnqxpyubifg";
                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }
                
        }
    }
}
