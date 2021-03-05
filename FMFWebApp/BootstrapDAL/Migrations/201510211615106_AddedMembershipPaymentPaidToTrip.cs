namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMembershipPaymentPaidToTrip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trip", "MembershipPaymentPaid", c => c.Decimal(precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trip", "MembershipPaymentPaid");
        }
    }
}
