namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedApplicationSettingsToSimpleMembership : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserSettings", newName: "UserSettings");
            RenameColumn(table: "dbo.AspNetUsers", name: "SettingsId", newName: "UserSettingsId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_SettingsId", newName: "IX_UserSettingsId");
            CreateTable(
                "dbo.ApplicationSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrintServerUrl = c.String(),
                        HolidayPayPercentage = c.Decimal(precision: 18, scale: 4),
                        MaternityPaymentPercentage = c.Decimal(precision: 18, scale: 4),
                        LaborInsurancePercentage = c.Decimal(precision: 18, scale: 4),
                        MemberFinancialAidPaymentPerDay = c.Decimal(precision: 18, scale: 4),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "AppSettingsId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "AppSettingsId");
            AddForeignKey("dbo.AspNetUsers", "AppSettingsId", "dbo.ApplicationSettings", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "AppSettingsId", "dbo.ApplicationSettings");
            DropIndex("dbo.AspNetUsers", new[] { "AppSettingsId" });
            DropColumn("dbo.AspNetUsers", "AppSettingsId");
            DropTable("dbo.ApplicationSettings");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_UserSettingsId", newName: "IX_SettingsId");
            RenameColumn(table: "dbo.AspNetUsers", name: "UserSettingsId", newName: "SettingsId");
            RenameTable(name: "dbo.UserSettings", newName: "ApplicationUserSettings");
        }
    }
}
