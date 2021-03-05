namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMemberFinancialAidPaymentPerDayToAppSetttings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppSettings", "MemberFinancialAidPaymentPerDay", c => c.Decimal(precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppSettings", "MemberFinancialAidPaymentPerDay");
        }
    }
}
