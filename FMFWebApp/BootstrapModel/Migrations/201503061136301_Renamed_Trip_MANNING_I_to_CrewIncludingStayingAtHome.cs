namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renamed_Trip_MANNING_I_to_CrewIncludingStayingAtHome : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Trip", "MANNING_I", "CrewIncludingStayingAtHome");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Trip", "CrewIncludingStayingAtHome", "MANNING_I");
        }
    }
}
