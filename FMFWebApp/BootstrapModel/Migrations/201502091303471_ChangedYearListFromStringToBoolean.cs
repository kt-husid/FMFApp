namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedYearListFromStringToBoolean : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShipType", "YearList", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShipType", "YearList", c => c.String());
        }
    }
}
