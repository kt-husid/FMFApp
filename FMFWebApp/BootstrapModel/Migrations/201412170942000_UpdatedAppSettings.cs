namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedAppSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppSettings", "HolidayPayPercentage", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppSettings", "HolidayPayPercentage");
        }
    }
}
