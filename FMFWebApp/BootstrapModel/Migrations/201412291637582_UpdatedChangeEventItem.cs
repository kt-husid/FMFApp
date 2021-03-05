namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedChangeEventItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChangeEventItem", "Date", c => c.DateTime());
            AddColumn("dbo.ChangeEventItem", "UserIdCode", c => c.String());
            AddColumn("dbo.ChangeEventItem", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChangeEventItem", "Type");
            DropColumn("dbo.ChangeEventItem", "UserIdCode");
            DropColumn("dbo.ChangeEventItem", "Date");
        }
    }
}
