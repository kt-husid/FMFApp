namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedEmailModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SocialNetwork", name: "Uri", newName: "UriString");
            RenameColumn(table: "dbo.Website", name: "Uri", newName: "UriString");
            RenameColumn(table: "dbo.MemberPayment", name: "LGJ", newName: "MembershipPayment");
            AddColumn("dbo.EmailAddress", "Address", c => c.String());
            DropColumn("dbo.EmailAddress", "Local");
            DropColumn("dbo.EmailAddress", "Domain");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmailAddress", "Domain", c => c.String());
            AddColumn("dbo.EmailAddress", "Local", c => c.String());
            DropColumn("dbo.EmailAddress", "Address");
            RenameColumn(table: "dbo.MemberPayment", name: "MembershipPayment", newName: "LGJ");
            RenameColumn(table: "dbo.Website", name: "UriString", newName: "Uri");
            RenameColumn(table: "dbo.SocialNetwork", name: "UriString", newName: "Uri");
        }
    }
}
