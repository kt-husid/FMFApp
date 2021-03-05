namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedLAST_DATOFromMember : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Member", "StatusId", "dbo.Status");
            DropIndex("dbo.Member", new[] { "StatusId" });
            DropColumn("dbo.Member", "StatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Member", "StatusId", c => c.Int());
            CreateIndex("dbo.Member", "StatusId");
            AddForeignKey("dbo.Member", "StatusId", "dbo.Status", "Id");
        }
    }
}
