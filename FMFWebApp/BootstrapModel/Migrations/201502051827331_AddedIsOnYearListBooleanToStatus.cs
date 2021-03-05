namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsOnYearListBooleanToStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Status", "IsOnYearList", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Status", "IsOnYearList");
        }
    }
}
