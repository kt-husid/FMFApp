namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedTripLineUnitPricePrecision : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TripLine", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 8));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TripLine", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 4));
        }
    }
}
