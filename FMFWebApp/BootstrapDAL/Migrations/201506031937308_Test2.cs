namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test2 : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Address", "EntityId", "dbo.Entity");
            //DropForeignKey("dbo.BankAccount", "EntityId", "dbo.Entity");
            //DropForeignKey("dbo.Comment", "EntityId", "dbo.Entity");
            //DropForeignKey("dbo.EmailAddress", "EntityId", "dbo.Entity");
            //DropForeignKey("dbo.Phone", "EntityId", "dbo.Entity");
            //DropForeignKey("dbo.SocialNetwork", "EntityId", "dbo.Entity");
            //DropForeignKey("dbo.Website", "EntityId", "dbo.Entity");
            //DropIndex("dbo.Address", new[] { "EntityId" });
            //DropIndex("dbo.BankAccount", new[] { "EntityId" });
            //DropIndex("dbo.Comment", new[] { "EntityId" });
            //DropIndex("dbo.EmailAddress", new[] { "EntityId" });
            //DropIndex("dbo.Phone", new[] { "EntityId" });
            //DropIndex("dbo.SocialNetwork", new[] { "EntityId" });
            //DropIndex("dbo.Website", new[] { "EntityId" });
            RenameColumn(table: "dbo.SocialNetwork", name: "Uri", newName: "UriString");
            RenameColumn(table: "dbo.Website", name: "Uri", newName: "UriString");
            RenameColumn(table: "dbo.MemberPayment", name: "LGJ", newName: "MembershipPayment");
            //AlterColumn("dbo.Address", "EntityId", c => c.Int());
            //AlterColumn("dbo.BankAccount", "EntityId", c => c.Int());
            //AlterColumn("dbo.Comment", "EntityId", c => c.Int());
            //AlterColumn("dbo.EmailAddress", "EntityId", c => c.Int());
            //AlterColumn("dbo.Phone", "RawNumber", c => c.String());
            //AlterColumn("dbo.Phone", "EntityId", c => c.Int());
            //AlterColumn("dbo.SocialNetwork", "UriString", c => c.String());
            //AlterColumn("dbo.SocialNetwork", "EntityId", c => c.Int());
            //AlterColumn("dbo.Website", "UriString", c => c.String());
            //AlterColumn("dbo.Website", "EntityId", c => c.Int());
            //AlterColumn("dbo.PersonTitle", "Description", c => c.String());
            //CreateIndex("dbo.Address", "EntityId");
            //CreateIndex("dbo.BankAccount", "EntityId");
            //CreateIndex("dbo.Comment", "EntityId");
            //CreateIndex("dbo.EmailAddress", "EntityId");
            //CreateIndex("dbo.Phone", "EntityId");
            //CreateIndex("dbo.SocialNetwork", "EntityId");
            //CreateIndex("dbo.Website", "EntityId");
            //AddForeignKey("dbo.Address", "EntityId", "dbo.Entity", "Id");
            //AddForeignKey("dbo.BankAccount", "EntityId", "dbo.Entity", "Id");
            //AddForeignKey("dbo.Comment", "EntityId", "dbo.Entity", "Id");
            //AddForeignKey("dbo.EmailAddress", "EntityId", "dbo.Entity", "Id");
            //AddForeignKey("dbo.Phone", "EntityId", "dbo.Entity", "Id");
            //AddForeignKey("dbo.SocialNetwork", "EntityId", "dbo.Entity", "Id");
            //AddForeignKey("dbo.Website", "EntityId", "dbo.Entity", "Id");
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Website", "EntityId", "dbo.Entity");
            //DropForeignKey("dbo.SocialNetwork", "EntityId", "dbo.Entity");
            //DropForeignKey("dbo.Phone", "EntityId", "dbo.Entity");
            //DropForeignKey("dbo.EmailAddress", "EntityId", "dbo.Entity");
            //DropForeignKey("dbo.Comment", "EntityId", "dbo.Entity");
            //DropForeignKey("dbo.BankAccount", "EntityId", "dbo.Entity");
            //DropForeignKey("dbo.Address", "EntityId", "dbo.Entity");
            //DropIndex("dbo.Website", new[] { "EntityId" });
            //DropIndex("dbo.SocialNetwork", new[] { "EntityId" });
            //DropIndex("dbo.Phone", new[] { "EntityId" });
            //DropIndex("dbo.EmailAddress", new[] { "EntityId" });
            //DropIndex("dbo.Comment", new[] { "EntityId" });
            //DropIndex("dbo.BankAccount", new[] { "EntityId" });
            //DropIndex("dbo.Address", new[] { "EntityId" });
            //AlterColumn("dbo.PersonTitle", "Description", c => c.String(nullable: false));
            //AlterColumn("dbo.Website", "EntityId", c => c.Int(nullable: false));
            //AlterColumn("dbo.Website", "UriString", c => c.String(nullable: false, maxLength: 2083));
            //AlterColumn("dbo.SocialNetwork", "EntityId", c => c.Int(nullable: false));
            //AlterColumn("dbo.SocialNetwork", "UriString", c => c.String(nullable: false, maxLength: 2083));
            //AlterColumn("dbo.Phone", "EntityId", c => c.Int(nullable: false));
            //AlterColumn("dbo.Phone", "RawNumber", c => c.String(nullable: false));
            //AlterColumn("dbo.EmailAddress", "EntityId", c => c.Int(nullable: false));
            //AlterColumn("dbo.Comment", "EntityId", c => c.Int(nullable: false));
            //AlterColumn("dbo.BankAccount", "EntityId", c => c.Int(nullable: false));
            //AlterColumn("dbo.Address", "EntityId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.MemberPayment", name: "MembershipPayment", newName: "LGJ");
            RenameColumn(table: "dbo.Website", name: "UriString", newName: "Uri");
            RenameColumn(table: "dbo.SocialNetwork", name: "UriString", newName: "Uri");
            //CreateIndex("dbo.Website", "EntityId");
            //CreateIndex("dbo.SocialNetwork", "EntityId");
            //CreateIndex("dbo.Phone", "EntityId");
            //CreateIndex("dbo.EmailAddress", "EntityId");
            //CreateIndex("dbo.Comment", "EntityId");
            //CreateIndex("dbo.BankAccount", "EntityId");
            //CreateIndex("dbo.Address", "EntityId");
            //AddForeignKey("dbo.Website", "EntityId", "dbo.Entity", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.SocialNetwork", "EntityId", "dbo.Entity", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.Phone", "EntityId", "dbo.Entity", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.EmailAddress", "EntityId", "dbo.Entity", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.Comment", "EntityId", "dbo.Entity", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.BankAccount", "EntityId", "dbo.Entity", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.Address", "EntityId", "dbo.Entity", "Id", cascadeDelete: true);
        }
    }
}
