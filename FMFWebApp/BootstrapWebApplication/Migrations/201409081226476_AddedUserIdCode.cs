namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserIdCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserIdCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserIdCode");
        }
    }
}
