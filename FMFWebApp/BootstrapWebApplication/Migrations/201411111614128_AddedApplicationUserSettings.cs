namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedApplicationUserSettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LabelPrinterName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Settings_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Settings_Id");
            AddForeignKey("dbo.AspNetUsers", "Settings_Id", "dbo.ApplicationUserSettings", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Settings_Id", "dbo.ApplicationUserSettings");
            DropIndex("dbo.AspNetUsers", new[] { "Settings_Id" });
            DropColumn("dbo.AspNetUsers", "Settings_Id");
            DropTable("dbo.ApplicationUserSettings");
        }
    }
}
