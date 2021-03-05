namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBokingarListToToTripAndCreatedNewModelLundinBokingar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LundinAlkaPaymentBooking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BokID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        JournalNumber = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Description = c.String(),
                        ShipCode = c.String(),
                        TripNumber = c.Int(),
                        TripId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Trip", t => t.TripId)
                .Index(t => t.TripId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.LundinMembershipPaymentBooking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BokID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        JournalNumber = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Description = c.String(),
                        ShipCode = c.String(),
                        TripNumber = c.Int(),
                        TripId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Trip", t => t.TripId)
                .Index(t => t.TripId)
                .Index(t => t.ChangeEventId);
            
            AddColumn("dbo.Trip", "MembershipPaymentPaid", c => c.Decimal(precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LundinMembershipPaymentBooking", "TripId", "dbo.Trip");
            DropForeignKey("dbo.LundinMembershipPaymentBooking", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.LundinAlkaPaymentBooking", "TripId", "dbo.Trip");
            DropForeignKey("dbo.LundinAlkaPaymentBooking", "ChangeEventId", "dbo.ChangeEvent");
            DropIndex("dbo.LundinMembershipPaymentBooking", new[] { "ChangeEventId" });
            DropIndex("dbo.LundinMembershipPaymentBooking", new[] { "TripId" });
            DropIndex("dbo.LundinAlkaPaymentBooking", new[] { "ChangeEventId" });
            DropIndex("dbo.LundinAlkaPaymentBooking", new[] { "TripId" });
            DropColumn("dbo.Trip", "MembershipPaymentPaid");
            DropTable("dbo.LundinMembershipPaymentBooking");
            DropTable("dbo.LundinAlkaPaymentBooking");
        }
    }
}
