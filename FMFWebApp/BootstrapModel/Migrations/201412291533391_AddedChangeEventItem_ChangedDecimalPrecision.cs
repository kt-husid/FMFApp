namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedChangeEventItem_ChangedDecimalPrecision : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChangeEventItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChangeEventId = c.Int(),
                        TableName = c.String(),
                        ColumnName = c.String(),
                        ChangedFrom = c.String(),
                        ChangedTo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .Index(t => t.ChangeEventId);
            
            AlterColumn("dbo.AidPrice", "Price", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.ShipType", "PctToShip", c => c.Decimal(nullable: false, precision: 18, scale: 3));
            AlterColumn("dbo.ShipType", "PctToCrewMember", c => c.Decimal(nullable: false, precision: 18, scale: 3));
            AlterColumn("dbo.Ship", "Tonnage", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Ship", "HK", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Ship", "KG_IALT", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.ShippingCompany", "KG_IALT", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.ShippingCompany", "FRT_LON", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.ShippingCompany", "FRT_LON_NU", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "Crew", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "TotalWeight", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "TotalAmount", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "CrewSharePercentage", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "CrewShareMoney", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "MANNING_I", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "MANNING_X", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "SKIP_STUD", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "MAN_STUD", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "FRADRAG", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "TIL_MANN_TOT", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "FRADRAG2", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "MANN_PART", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "PR_DAG", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "MINLON", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "DAGLON", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "TOTAL_KR", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "MANN_PART_IS", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "MANN_PART_I", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "TRYG_KR", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Trip", "TRYG_KRHB", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Company", "KG_IALT", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Job", "PART", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Job", "TIL_DG", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Job", "TIL_TUR", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Member", "BURT_DG", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Member", "TUR_IALT", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Member", "INN_IALT", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.Member", "LG_KR", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.DEB_CON", "SALDO", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.MemberFinancialAid", "SocialServicePayment", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.MemberFinancialAid", "PaymentPerDay", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.MemberFinancialAid", "OurPayment", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.HolidayPay", "Amount", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.HolidayPay", "TRSP", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.HolidayPay", "LO_KR_A", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.HolidayPay", "LO_KR_F", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.HolidayPay", "LO_KR_FRT", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.HolidayPay", "SAML_KR", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.HolidayPay", "SAML_KR2", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.HolidayPay_HOVD", "Amount", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.SignOn", "PART", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.SignOn", "KR_IALT", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.SignOn", "TIL_PR_DG", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.SignOn", "TIL_PR_TUR", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.SignOn", "I_PART", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.SignOn", "TRYG_KR", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.MemberPayment2", "U1", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.MemberPayment2", "U2", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.MemberPayment2", "U3", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.MemberPayment2", "U4", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.MemberPayment2", "U5", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.MemberPayment2", "U6", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.MemberPayment2", "U7", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.P400", "KR", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.MemberPayment", "Price", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.MemberPayment", "HolidayPay", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.MemberPayment", "LGJ", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.MemberPayment", "TRANSP_IALT", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.TripCrewAid", "Amount", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.TripCrewAid", "Price", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.TripCrewAid", "AlternativePrice", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.TripCrewAid", "KR", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.TripDeduction", "Amount", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.TripDeduction", "Price", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.TripLine", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 3));
            AlterColumn("dbo.TripLine", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 3));
            AlterColumn("dbo.TripLine", "AlternativePrice", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.TripLine", "KR", c => c.Decimal(nullable: false, precision: 18, scale: 3));
            AlterColumn("dbo.TripLine", "STUD_M", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.TripLine", "STUD_S", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.TripShipAid", "Amount", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.TripShipAid", "Price", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.TripShipAid", "AlternativePrice", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.TripShipAid", "KR", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.AppSettings", "HolidayPayPercentage", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.FishPrice", "Price", c => c.Decimal(precision: 18, scale: 3));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChangeEventItem", "ChangeEventId", "dbo.ChangeEvent");
            DropIndex("dbo.ChangeEventItem", new[] { "ChangeEventId" });
            AlterColumn("dbo.FishPrice", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.AppSettings", "HolidayPayPercentage", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TripShipAid", "KR", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TripShipAid", "AlternativePrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TripShipAid", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TripShipAid", "Amount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TripLine", "STUD_S", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TripLine", "STUD_M", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TripLine", "KR", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TripLine", "AlternativePrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TripLine", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TripLine", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TripDeduction", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TripDeduction", "Amount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TripCrewAid", "KR", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TripCrewAid", "AlternativePrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TripCrewAid", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TripCrewAid", "Amount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.MemberPayment", "TRANSP_IALT", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.MemberPayment", "LGJ", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.MemberPayment", "HolidayPay", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.MemberPayment", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.P400", "KR", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.MemberPayment2", "U7", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.MemberPayment2", "U6", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.MemberPayment2", "U5", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.MemberPayment2", "U4", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.MemberPayment2", "U3", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.MemberPayment2", "U2", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.MemberPayment2", "U1", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.SignOn", "TRYG_KR", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.SignOn", "I_PART", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.SignOn", "TIL_PR_TUR", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.SignOn", "TIL_PR_DG", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.SignOn", "KR_IALT", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.SignOn", "PART", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.HolidayPay_HOVD", "Amount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.HolidayPay", "SAML_KR2", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.HolidayPay", "SAML_KR", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.HolidayPay", "LO_KR_FRT", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.HolidayPay", "LO_KR_F", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.HolidayPay", "LO_KR_A", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.HolidayPay", "TRSP", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.HolidayPay", "Amount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.MemberFinancialAid", "OurPayment", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.MemberFinancialAid", "PaymentPerDay", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.MemberFinancialAid", "SocialServicePayment", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.DEB_CON", "SALDO", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Member", "LG_KR", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Member", "INN_IALT", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Member", "TUR_IALT", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Member", "BURT_DG", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Job", "TIL_TUR", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Job", "TIL_DG", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Job", "PART", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Company", "KG_IALT", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "TRYG_KRHB", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "TRYG_KR", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "MANN_PART_I", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "MANN_PART_IS", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "TOTAL_KR", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "DAGLON", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "MINLON", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "PR_DAG", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "MANN_PART", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "FRADRAG2", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "TIL_MANN_TOT", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "FRADRAG", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "MAN_STUD", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "SKIP_STUD", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "MANNING_X", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "MANNING_I", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "CrewShareMoney", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "CrewSharePercentage", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "TotalAmount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "TotalWeight", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Trip", "Crew", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ShippingCompany", "FRT_LON_NU", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ShippingCompany", "FRT_LON", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ShippingCompany", "KG_IALT", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Ship", "KG_IALT", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Ship", "HK", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Ship", "Tonnage", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ShipType", "PctToCrewMember", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ShipType", "PctToShip", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AidPrice", "Price", c => c.Decimal(precision: 18, scale: 2));
            DropTable("dbo.ChangeEventItem");
        }
    }
}
