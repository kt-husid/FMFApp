namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedAppSettings : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.AppSettings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AppSettings",
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
            
        }
    }
}
