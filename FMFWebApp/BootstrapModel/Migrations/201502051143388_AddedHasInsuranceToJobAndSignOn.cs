namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedHasInsuranceToJobAndSignOn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Job", "HasInsurance", c => c.Boolean(nullable: false));
            AddColumn("dbo.SignOn", "HasInsurance", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SignOn", "HasInsurance");
            DropColumn("dbo.Job", "HasInsurance");
        }
    }
}
