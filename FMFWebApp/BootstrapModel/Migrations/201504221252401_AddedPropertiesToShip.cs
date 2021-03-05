namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPropertiesToShip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ship", "CountryCode", c => c.String());
            AddColumn("dbo.Ship", "CountryName", c => c.String());
            AddColumn("dbo.Ship", "AddressLine", c => c.String());
            AddColumn("dbo.Ship", "PostalCode", c => c.Int());
            AddColumn("dbo.Ship", "CityName", c => c.String());
            AddColumn("dbo.Ship", "PhoneCountryCode", c => c.Int());
            AddColumn("dbo.Ship", "PhoneNumber", c => c.String());
            AddColumn("dbo.ShippingCompany", "PhoneCountryCode", c => c.Int());
            AddColumn("dbo.ShippingCompany", "PhoneNumber", c => c.String());
            AddColumn("dbo.Company", "PhoneCountryCode", c => c.Int());
            AddColumn("dbo.Company", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Company", "PhoneNumber");
            DropColumn("dbo.Company", "PhoneCountryCode");
            DropColumn("dbo.ShippingCompany", "PhoneNumber");
            DropColumn("dbo.ShippingCompany", "PhoneCountryCode");
            DropColumn("dbo.Ship", "PhoneNumber");
            DropColumn("dbo.Ship", "PhoneCountryCode");
            DropColumn("dbo.Ship", "CityName");
            DropColumn("dbo.Ship", "PostalCode");
            DropColumn("dbo.Ship", "AddressLine");
            DropColumn("dbo.Ship", "CountryName");
            DropColumn("dbo.Ship", "CountryCode");
        }
    }
}
