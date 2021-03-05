namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmailStuff : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SocialNetwork", name: "Uri", newName: "UriString");
            RenameColumn(table: "dbo.Website", name: "Uri", newName: "UriString");
            RenameColumn(table: "dbo.MemberPayment", name: "LGJ", newName: "MembershipPayment");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.MemberPayment", name: "MembershipPayment", newName: "LGJ");
            RenameColumn(table: "dbo.Website", name: "UriString", newName: "Uri");
            RenameColumn(table: "dbo.SocialNetwork", name: "UriString", newName: "Uri");
        }
    }
}
