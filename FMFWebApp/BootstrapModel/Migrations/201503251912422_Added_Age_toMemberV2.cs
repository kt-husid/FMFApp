namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Age_toMemberV2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Member", "Age", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Member", "Age");
        }
    }
}
