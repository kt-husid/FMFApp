namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedShipTypeCodePropertyInTrip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trip", "ShipTypeCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trip", "ShipTypeCode");
        }
    }
}
