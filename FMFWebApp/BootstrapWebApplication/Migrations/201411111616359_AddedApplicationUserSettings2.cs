namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedApplicationUserSettings2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "Settings_Id", newName: "SettingsId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Settings_Id", newName: "IX_SettingsId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_SettingsId", newName: "IX_Settings_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "SettingsId", newName: "Settings_Id");
        }
    }
}
