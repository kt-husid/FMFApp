namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsAlivePropertyToPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "IsAlive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "IsAlive");
        }
    }
}
