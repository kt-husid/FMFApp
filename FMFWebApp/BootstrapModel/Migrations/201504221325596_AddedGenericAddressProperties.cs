namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGenericAddressProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShippingCompany", "CountryCode", c => c.String());
            AddColumn("dbo.ShippingCompany", "CountryName", c => c.String());
            AddColumn("dbo.ShippingCompany", "AddressLine", c => c.String());
            AddColumn("dbo.ShippingCompany", "PostalCode", c => c.Int());
            AddColumn("dbo.ShippingCompany", "CityName", c => c.String());
            AddColumn("dbo.Company", "CountryCode", c => c.String());
            AddColumn("dbo.Company", "CountryName", c => c.String());
            AddColumn("dbo.Company", "AddressLine", c => c.String());
            AddColumn("dbo.Company", "PostalCode", c => c.Int());
            AddColumn("dbo.Company", "CityName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Company", "CityName");
            DropColumn("dbo.Company", "PostalCode");
            DropColumn("dbo.Company", "AddressLine");
            DropColumn("dbo.Company", "CountryName");
            DropColumn("dbo.Company", "CountryCode");
            DropColumn("dbo.ShippingCompany", "CityName");
            DropColumn("dbo.ShippingCompany", "PostalCode");
            DropColumn("dbo.ShippingCompany", "AddressLine");
            DropColumn("dbo.ShippingCompany", "CountryName");
            DropColumn("dbo.ShippingCompany", "CountryCode");
        }
    }
}
