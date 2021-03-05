namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLaborInsuranceAmountPerDayToApplicationSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationSettings", "LaborInsuranceAmountPerDay", c => c.Decimal(precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationSettings", "LaborInsuranceAmountPerDay");
        }
    }
}
