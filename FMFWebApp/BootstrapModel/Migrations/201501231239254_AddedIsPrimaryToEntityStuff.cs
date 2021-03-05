namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsPrimaryToEntityStuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailAddress", "IsPrimary", c => c.Boolean(nullable: false));
            AddColumn("dbo.SocialNetwork", "IsPrimary", c => c.Boolean(nullable: false));
            AddColumn("dbo.Website", "IsPrimary", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Website", "IsPrimary");
            DropColumn("dbo.SocialNetwork", "IsPrimary");
            DropColumn("dbo.EmailAddress", "IsPrimary");
        }
    }
}
