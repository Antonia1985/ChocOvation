using ChocOvation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(ChocOvation.Startup))]
namespace ChocOvation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }


        // In this method we will create default User roles and Admin user for login  
        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));




            // In Startup iam creating first Admin Role and creating a default Admin User   
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool  
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                 

                var user = new ApplicationUser();
                user.UserName = "Antonia";
                user.Email = "antoniapav@gmail.com";
                user.FirstName = "Antonia";
                user.LastName = "Sbdy";
                user.VATNumber = "987654321";
                string userPWD = "Antonia&Giorgos21";

                var chkUser = userManager.Create(user, userPWD);

                //Add default User to Role Admin  
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating CEO role   
            if (!roleManager.RoleExists("CEO"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "CEO";
                roleManager.Create(role);


                //Here we create a CEO super user who will have some priviledges                 

                var user = new Employee();
                user.UserName = "Giorgos";
                user.Email = "satravelas@gmail.com";
                user.FirstName = "Giorgos";
                user.LastName = "Satras";
                user.VATNumber = "123456789";
                user.HireDate = new DateTime(2018, 05, 15);
                string userPWD = "Antonia&Giorgos22";



                var chkUser = userManager.Create(user, userPWD);

                //Add default User to Role Ceo  
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "CEO");

                }
            }

            //Creating Employee role   
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

            }

            //Creating Supplier role   
            if (!roleManager.RoleExists("Supplier"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Supplier";
                roleManager.Create(role);

            }

            //Creating Employee role   
            if (!roleManager.RoleExists("Production Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Production Manager";
                roleManager.Create(role);

            }

            //Creating Finance Manager role   
            if (!roleManager.RoleExists("Finance Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Finance Manager";
                roleManager.Create(role);

            }
            //Creating ChocoStore Manager role   
            if (!roleManager.RoleExists("ChocoStore Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "ChocoStore Manager";
                roleManager.Create(role);

            }

        }

    }
}