namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStatusToMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Member", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Member", "Status");
        }
    }
}
