using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TrashCollectorExercise.Models;

[assembly: OwinStartupAttribute(typeof(TrashCollectorExercise.Startup))]
namespace TrashCollectorExercise
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        // crete default user roles and Admin from loggin
        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // in startup create first admin role and create default admin user
            if (!roleManager.RoleExists("Admin"))
            {
                // first create admin role
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                // create admin user who will maintain website

                var user = new ApplicationUser();
                user.UserName = "alexhardy872";
                user.Email = "alexhardy872@gmail.com";

                string userPWD = "test872@UWL";

                var checkUser = UserManager.Create(user, userPWD);

                //Add default user o role admin
                if (checkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }

            }

       

            // creating Creating Employee role
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }

            // creating Creating Customer role
            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }




        }

    }
}
