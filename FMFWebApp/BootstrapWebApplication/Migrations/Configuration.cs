namespace BootstrapWebApplication.Migrations
{
    using BootstrapWebApplication.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    //using System.Web;
    using System.Net.Http;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "BootstrapWebApplication.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            if (db.AppSettings.ToList().Count == 0)
            {
                var appSettings = GetDefaultAppSettings();
                db.AppSettings.Add(appSettings);
            }

            db.Roles.AddOrUpdate(x => x.Name, new IdentityRole() { Name = "User" });

            InitializeAdminIdentity(db);
        }

        public static ApplicationSettings GetDefaultAppSettings()
        {
            return new ApplicationSettings()
            {
                PrintServerUrl = "http://localhost:9000",
                HolidayPayPercentage = 12.0m,
                MaternityPaymentPercentage = 0.62m,
                LaborInsurancePercentage = 0.99m,
                MemberFinancialAidPaymentPerDay = 475.05m,
                LaborInsuranceAmountPerDay = 23m
            };
        }

        //private static ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}
        //private static ApplicationUserManager _userManager;

        //Create User=benjamin@kthusid.com with password=Qwer1234 in the Admin role        
        public static void InitializeAdminIdentity(ApplicationDbContext db)
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);  

            //var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string name = "benjamin@kthusid.com";
            const string password = "Qwer1234";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}
