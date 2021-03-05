namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_CrewSharePart_to_Trip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trip", "CrewSharePart", c => c.Decimal(precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trip", "CrewSharePart");
        }
    }
}
