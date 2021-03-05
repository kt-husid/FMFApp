namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedMINLON_to_MinimumWage_in_Trip : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Trip", "MINLON", "MinimumWage");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Trip", "MinimumWage", "MINLON");
        }
    }
}
