using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace BootstrapWebApplication.Models
{
    public class UserSettings
    {
        public int Id { get; set; }
        public string LabelPrinterName { get; set; }
    }

    public class ApplicationSettings
    {
        public int Id { get; set; }

        public string PrintServerUrl { get; set; }

        public decimal? HolidayPayPercentage { get; set; }

        public decimal? MaternityPaymentPercentage { get; set; }

        public decimal? LaborInsurancePercentage { get; set; }

        public decimal? MemberFinancialAidPaymentPerDay { get; set; }

        public decimal? LaborInsuranceAmountPerDay { get; set; }
    }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserIdCode { get; set; }

        [Display(Name = "City", ResourceType = typeof(BootstrapResources.Resources))]
        public string HomeTown { get; set; }
        
        [Display(Name = "PhoneNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public string PhoneNumber { get; set; }

        [ForeignKey("UserSettings")]
        public int? UserSettingsId { get; set; }
        public virtual UserSettings UserSettings { get; set; }


        [ForeignKey("AppSettings")]
        public int? AppSettingsId { get; set; }
        public virtual ApplicationSettings AppSettings { get; set; }

        [Display(Name = "Birthday", ResourceType = typeof(BootstrapResources.Resources))]
        //[DataType(DataType.Date, ErrorMessageResourceType = typeof(BootstrapResources.Resources), ErrorMessageResourceName = "DateRequired")]
        public System.DateTime? Birthday { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<ApplicationSettings> AppSettings { get; set; }
        public virtual DbSet<UserSettings> UserSettings { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(18, 4));
            base.OnModelCreating(modelBuilder);
        }

       

        //public System.Data.Entity.DbSet<BootstrapWebApplication.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}