using BootstrapWebApplication.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BootstrapWebApplication.DAL
{
    //[DbConfigurationType(typeof(BootstrapConfiguration))]
    public partial class BootstrapContext : DbContext
    {
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<ChangeEvent> ChangeEvents { get; set; }
        public virtual DbSet<ChangeEventItem> ChangeEventItems { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DEB_CON> DEB_CONs { get; set; }
        public virtual DbSet<EmailAddress> EmailAddresses { get; set; }
        public virtual DbSet<HolidayPay_HOVD> HolidayPay_HOVDs { get; set; }
        public virtual DbSet<HolidayPay> HolidayPays { get; set; }
        public virtual DbSet<FishSpecies> FishSpecies { get; set; }
        public virtual DbSet<FishPrice> FishPrices { get; set; }
        public virtual DbSet<PersonGender> PersonGenders { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberComment> MemberComments { get; set; }
        public virtual DbSet<MemberPayment> MemberPayments { get; set; }
        public virtual DbSet<MemberType> MemberTypes { get; set; }
        public virtual DbSet<MemberPayment2> MemberPayments2 { get; set; }
        public virtual DbSet<MI2_HOVD> MI2_HOVDs { get; set; }
        public virtual DbSet<P400> P400s { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PersonTitle> PersonTitles { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<Postal> Postals { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Ship> Ships { get; set; }
        public virtual DbSet<ShipComment> ShipComments { get; set; }
        public virtual DbSet<ShippingCompany> ShippingCompanies { get; set; }
        public virtual DbSet<SignOn> SignOns { get; set; }
        //public virtual DbSet<SignOnTrans> SignOnTranses { get; set; }
        public virtual DbSet<SocialNetwork> SocialNetworks { get; set; }
        public virtual DbSet<AidType> AidTypes { get; set; }
        public virtual DbSet<AidPrice> AidPrices { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<TripDeduction> TripDeductions { get; set; }
        public virtual DbSet<TripLine> TripLines { get; set; }
        //public virtual DbSet<TripCrewAid> TripCrewAids { get; set; }
        //public virtual DbSet<TripShipAid> TripShipAids { get; set; }
        public virtual DbSet<ShipType> ShipTypes { get; set; }
        public virtual DbSet<MemberFinancialAid> FinancialAids { get; set; }
        public virtual DbSet<Website> Websites { get; set; }
        public virtual DbSet<DeductionType> DeductionTypes { get; set; }
        //public virtual DbSet<TR_TXT> TR_TXTs { get; set; }
        //public virtual DbSet<TLF_IN> TLF_INs { get; set; }
        public virtual DbSet<MinimumWage> MinimumWages { get; set; }

        public BootstrapContext()
            : base("name=FMFWebAppContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BootstrapContext, BootstrapWebApplication.Migrations.Configuration>());
            //Configuration.AutoDetectChangesEnabled = false;
            
            //Database.SetInitializer<BootstrapContext>(new BootstrapInitializer());

            //set the initializer to migration
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<BootstrapContext, BootstrapWebApplication.Migrations.Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<SignOn>().Property(x => x.PART).HasPrecision(16, 3);
            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(18, 4));
            modelBuilder.Entity<TripLine>().Property(x => x.UnitPrice).HasPrecision(18, 8);

            base.OnModelCreating(modelBuilder);
        }
    }
}