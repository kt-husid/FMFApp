namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedUnusedPropertiesFromSignOn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SignOn", "KG_IALT");
            DropColumn("dbo.SignOn", "FRT_PART");
            DropColumn("dbo.SignOn", "PART_NETTO");
            DropColumn("dbo.SignOn", "Birthday");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SignOn", "Birthday", c => c.DateTime());
            AddColumn("dbo.SignOn", "PART_NETTO", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.SignOn", "FRT_PART", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.SignOn", "KG_IALT", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
