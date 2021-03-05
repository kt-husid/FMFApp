namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedStatusToMemberTypeInSignOn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SignOn", "StatusId", "dbo.Status");
            DropIndex("dbo.SignOn", new[] { "StatusId" });
            AddColumn("dbo.SignOn", "MemberTypeId", c => c.Int());
            CreateIndex("dbo.SignOn", "MemberTypeId");
            AddForeignKey("dbo.SignOn", "MemberTypeId", "dbo.MemberType", "Id");
            DropColumn("dbo.SignOn", "StatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SignOn", "StatusId", c => c.Int());
            DropForeignKey("dbo.SignOn", "MemberTypeId", "dbo.MemberType");
            DropIndex("dbo.SignOn", new[] { "MemberTypeId" });
            DropColumn("dbo.SignOn", "MemberTypeId");
            CreateIndex("dbo.SignOn", "StatusId");
            AddForeignKey("dbo.SignOn", "StatusId", "dbo.Status", "Id");
        }
    }
}
