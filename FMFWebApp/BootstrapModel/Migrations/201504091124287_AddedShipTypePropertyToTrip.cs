namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedShipTypePropertyToTrip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trip", "ShipTypeId", c => c.Int());
            CreateIndex("dbo.Trip", "ShipTypeId");
            AddForeignKey("dbo.Trip", "ShipTypeId", "dbo.ShipType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trip", "ShipTypeId", "dbo.ShipType");
            DropIndex("dbo.Trip", new[] { "ShipTypeId" });
            DropColumn("dbo.Trip", "ShipTypeId");
        }
    }
}
