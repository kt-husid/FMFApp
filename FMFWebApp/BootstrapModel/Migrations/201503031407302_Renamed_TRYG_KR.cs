namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renamed_TRYG_KR : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Trip", "TRYG_KR", "LaborInsurance");
            RenameColumn("dbo.SignOn", "TRYG_KR", "LaborInsuranceAmountPerDay");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Trip", "LaborInsurance", "TRYG_KR");
            RenameColumn("dbo.SignOn", "LaborInsuranceAmountPerDay", "TRYG_KR");
        }
    }
}
