namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMaternityPaymentPercentageToAppSetttings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppSettings", "MaternityPaymentPercentage", c => c.Decimal(precision: 18, scale: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppSettings", "MaternityPaymentPercentage");
        }
    }
}
