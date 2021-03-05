namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequiredTitleAttrInPerson : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Person", "TitleId", "dbo.PersonTitle");
            DropIndex("dbo.Person", new[] { "TitleId" });
            AlterColumn("dbo.Person", "TitleId", c => c.Int());
            CreateIndex("dbo.Person", "TitleId");
            AddForeignKey("dbo.Person", "TitleId", "dbo.PersonTitle", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Person", "TitleId", "dbo.PersonTitle");
            DropIndex("dbo.Person", new[] { "TitleId" });
            AlterColumn("dbo.Person", "TitleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Person", "TitleId");
            AddForeignKey("dbo.Person", "TitleId", "dbo.PersonTitle", "Id", cascadeDelete: true);
        }
    }
}
