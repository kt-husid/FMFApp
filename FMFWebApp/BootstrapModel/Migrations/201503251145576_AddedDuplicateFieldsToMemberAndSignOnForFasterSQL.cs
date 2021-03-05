namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDuplicateFieldsToMemberAndSignOnForFasterSQL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Member", "MemberTypeCode", c => c.String());
            AddColumn("dbo.Member", "MemberTypeDescription", c => c.String());
            AddColumn("dbo.Member", "JobCode", c => c.String());
            AddColumn("dbo.Member", "JobDescription", c => c.String());
            AddColumn("dbo.Member", "CountryCode", c => c.String());
            AddColumn("dbo.Member", "CountryName", c => c.String());
            AddColumn("dbo.Member", "AddressLine", c => c.String());
            AddColumn("dbo.Member", "PostalCode", c => c.Int());
            AddColumn("dbo.Member", "CityName", c => c.String());
            AddColumn("dbo.Member", "PhoneCountryCode", c => c.Int());
            AddColumn("dbo.Member", "PhoneNumber", c => c.String());
            AddColumn("dbo.SignOn", "ShipCode", c => c.String());
            AddColumn("dbo.SignOn", "ShipName", c => c.String());
            AddColumn("dbo.Person", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "FullName");
            DropColumn("dbo.SignOn", "ShipName");
            DropColumn("dbo.SignOn", "ShipCode");
            DropColumn("dbo.Member", "PhoneNumber");
            DropColumn("dbo.Member", "PhoneCountryCode");
            DropColumn("dbo.Member", "CityName");
            DropColumn("dbo.Member", "PostalCode");
            DropColumn("dbo.Member", "AddressLine");
            DropColumn("dbo.Member", "CountryName");
            DropColumn("dbo.Member", "CountryCode");
            DropColumn("dbo.Member", "JobDescription");
            DropColumn("dbo.Member", "JobCode");
            DropColumn("dbo.Member", "MemberTypeDescription");
            DropColumn("dbo.Member", "MemberTypeCode");
        }
    }
}
