namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDbRelease : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CareOf = c.String(),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsPrimary = c.Boolean(nullable: false),
                        PostalId = c.Int(),
                        StateProvinceRegion = c.String(),
                        CountryId = c.Int(nullable: false),
                        EntityId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Entity", t => t.EntityId)
                .ForeignKey("dbo.Postal", t => t.PostalId)
                .Index(t => t.PostalId)
                .Index(t => t.CountryId)
                .Index(t => t.EntityId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.ChangeEvent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedByUserIdCode = c.String(),
                        UpdatedByUserIdCode = c.String(),
                        DeletedByUserIdCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChangeEventItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        UserIdCode = c.String(),
                        Type = c.Int(nullable: false),
                        TableName = c.String(),
                        ColumnName = c.String(),
                        ChangedFrom = c.String(),
                        ChangedTo = c.String(),
                        EntityId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Entity", t => t.EntityId)
                .Index(t => t.EntityId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.Entity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BankAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BankId = c.Int(),
                        AccountNumber = c.Int(nullable: false),
                        IsPrimary = c.Boolean(nullable: false),
                        EntityId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bank", t => t.BankId)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Entity", t => t.EntityId)
                .Index(t => t.BankId)
                .Index(t => t.EntityId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.Bank",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegNumber = c.Int(nullable: false),
                        Name = c.String(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        EntityId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Entity", t => t.EntityId)
                .Index(t => t.EntityId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.EmailAddress",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Local = c.String(),
                        Domain = c.String(),
                        IsVerified = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        EntityId = c.Int(),
                        IsPrimary = c.Boolean(nullable: false),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Entity", t => t.EntityId)
                .Index(t => t.EntityId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.Phone",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsPrimary = c.Boolean(nullable: false),
                        Extension = c.String(),
                        CountryCode = c.Int(nullable: false),
                        AreaCode = c.Int(),
                        Number = c.String(),
                        RawNumber = c.String(),
                        EntityId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Entity", t => t.EntityId)
                .Index(t => t.EntityId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.SocialNetwork",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Description = c.String(),
                        UriString = c.String(),
                        EntityId = c.Int(),
                        IsPrimary = c.Boolean(nullable: false),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Entity", t => t.EntityId)
                .Index(t => t.EntityId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.Website",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UriString = c.String(),
                        EntityId = c.Int(),
                        IsPrimary = c.Boolean(nullable: false),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Entity", t => t.EntityId)
                .Index(t => t.EntityId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.Postal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        CityName = c.String(),
                        Amount = c.Int(nullable: false),
                        CountryCode = c.String(),
                        OKI = c.Int(nullable: false),
                        FAX_SP = c.Int(nullable: false),
                        FAX_FB = c.Int(nullable: false),
                        FAX_SJ = c.Int(nullable: false),
                        FAX_F = c.Int(nullable: false),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.AidPrice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.DateTime(),
                        To = c.DateTime(),
                        Price = c.Decimal(precision: 18, scale: 4),
                        Code = c.String(),
                        SKIP = c.String(),
                        FID = c.String(),
                        FishSpeciesId = c.Int(),
                        ShipTypeId = c.Int(),
                        AidTypeId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AidType", t => t.AidTypeId)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.FishSpecies", t => t.FishSpeciesId)
                .ForeignKey("dbo.ShipType", t => t.ShipTypeId)
                .Index(t => t.FishSpeciesId)
                .Index(t => t.ShipTypeId)
                .Index(t => t.AidTypeId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.AidType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AidTypeCode = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        LINK = c.Int(),
                        Type = c.String(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.FishSpecies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        OldCode = c.String(),
                        Name = c.String(),
                        OldName = c.String(),
                        Description = c.String(),
                        FishSpeciesNumber = c.Int(),
                        ALIAS = c.String(),
                        LINK = c.Int(),
                        RAD = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.ShipType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        YearList = c.Boolean(nullable: false),
                        PctToShip = c.Decimal(nullable: false, precision: 18, scale: 4),
                        PctToCrewMember = c.Decimal(nullable: false, precision: 18, scale: 4),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.Ship",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        ContactCompanyName = c.String(),
                        ContactPersonName = c.String(),
                        IND_REG = c.DateTime(),
                        UD_REG = c.DateTime(),
                        Status = c.String(),
                        SK_TYPA = c.String(),
                        Tonnage = c.Decimal(precision: 18, scale: 4),
                        HK = c.Decimal(precision: 18, scale: 4),
                        KG_IALT = c.Decimal(precision: 18, scale: 4),
                        ALT_ID = c.String(),
                        Group = c.Int(),
                        ETI_DATO = c.DateTime(),
                        ETI_ID = c.String(),
                        EmployerNumber = c.Int(),
                        ShippingCompanyId = c.Int(),
                        ShipTypeId = c.Int(),
                        EntityId = c.Int(),
                        CountryCode = c.String(),
                        CountryName = c.String(),
                        AddressLine = c.String(),
                        PostalCode = c.Int(),
                        CityName = c.String(),
                        PhoneCountryCode = c.Int(),
                        PhoneNumber = c.String(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Entity", t => t.EntityId)
                .ForeignKey("dbo.ShippingCompany", t => t.ShippingCompanyId)
                .ForeignKey("dbo.ShipType", t => t.ShipTypeId)
                .Index(t => t.ShippingCompanyId)
                .Index(t => t.ShipTypeId)
                .Index(t => t.EntityId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.ShipComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        ShipId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Ship", t => t.ShipId)
                .Index(t => t.ShipId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.ShippingCompany",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(),
                        FaxNumber = c.Int(),
                        ContactCompanyName = c.String(),
                        ContactPersonName = c.String(),
                        KG_IALT = c.Decimal(precision: 18, scale: 4),
                        PREFIX = c.String(),
                        SKIB_IALT = c.Int(),
                        ALT_ID = c.Int(),
                        ETI_DATO = c.DateTime(),
                        ETI_ID = c.String(),
                        EmployerNumber = c.Int(),
                        FRT_LON = c.Decimal(precision: 18, scale: 4),
                        FRT_LON_DATO = c.DateTime(),
                        FRT_LON_NU = c.Decimal(precision: 18, scale: 4),
                        A_REG = c.Int(),
                        A_KTO = c.Int(),
                        StatusId = c.Int(),
                        EntityId = c.Int(),
                        CountryCode = c.String(),
                        CountryName = c.String(),
                        AddressLine = c.String(),
                        PostalCode = c.Int(),
                        CityName = c.String(),
                        PhoneCountryCode = c.Int(),
                        PhoneNumber = c.String(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Entity", t => t.EntityId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .Index(t => t.StatusId)
                .Index(t => t.EntityId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        STUTT = c.String(),
                        BREV_UT = c.String(),
                        FLYTAST = c.String(),
                        DLISTA = c.String(),
                        BLISTA = c.String(),
                        YearList = c.String(),
                        IsOnYearList = c.Boolean(nullable: false),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.Trip",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TripId = c.Int(nullable: false),
                        From = c.DateTime(),
                        To = c.DateTime(),
                        TripNumber = c.Int(),
                        PairTrawlerDocumentId = c.Int(),
                        ShipTypeCode = c.String(),
                        ShipTypeId = c.Int(),
                        TotalWeight = c.Decimal(precision: 18, scale: 4),
                        TotalAmount = c.Decimal(precision: 18, scale: 4),
                        CrewSharePercentage = c.Decimal(precision: 18, scale: 4),
                        CrewSharePart = c.Decimal(precision: 18, scale: 4),
                        CrewShareMoney = c.Decimal(precision: 18, scale: 4),
                        VIDAR = c.String(),
                        Crew = c.Decimal(precision: 18, scale: 4),
                        CrewIncludingStayingAtHome = c.Decimal(precision: 18, scale: 4),
                        MANNING_X = c.Decimal(precision: 18, scale: 4),
                        SKIP_STUD = c.Decimal(precision: 18, scale: 4),
                        MAN_STUD = c.Decimal(precision: 18, scale: 4),
                        FRADRAG = c.Decimal(precision: 18, scale: 4),
                        TIL_MANN_TOT = c.Decimal(precision: 18, scale: 4),
                        FRADRAG2 = c.Decimal(precision: 18, scale: 4),
                        MANN_PART = c.Decimal(precision: 18, scale: 4),
                        PR_DAG = c.Decimal(precision: 18, scale: 4),
                        MinimumWage = c.Decimal(precision: 18, scale: 4),
                        DAGLON = c.Decimal(precision: 18, scale: 4),
                        SKIB_TXT = c.String(),
                        TOTAL_KR = c.Decimal(precision: 18, scale: 4),
                        MANN_PART_IS = c.Decimal(precision: 18, scale: 4),
                        MID_AR = c.Int(),
                        MANN_PART_I = c.Decimal(precision: 18, scale: 4),
                        EmployerNumber = c.Int(),
                        PAR_ART = c.String(),
                        PAR_TUR_ID = c.String(),
                        ALT_MP = c.String(),
                        ALT_MA = c.Int(),
                        CHECK = c.String(),
                        TRYG_ANT = c.Int(),
                        LaborInsurance = c.Decimal(precision: 18, scale: 4),
                        TRYG_KRHB = c.Decimal(precision: 18, scale: 4),
                        TRYG_KVIT = c.String(),
                        TRYG_BILAG = c.Int(),
                        TRYG_DATO = c.DateTime(),
                        PortOfLandingId = c.Int(),
                        DateOfLanding = c.DateTime(),
                        ShipId = c.Int(),
                        PairShipId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Ship", t => t.PairShipId)
                .ForeignKey("dbo.Company", t => t.PortOfLandingId)
                .ForeignKey("dbo.ShipType", t => t.ShipTypeId)
                .ForeignKey("dbo.Ship", t => t.ShipId)
                .Index(t => t.ShipTypeId)
                .Index(t => t.PortOfLandingId)
                .Index(t => t.ShipId)
                .Index(t => t.PairShipId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.MI2_HOVD",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PERS_NR = c.Int(),
                        SKIB_ID = c.String(),
                        AR = c.String(),
                        From = c.DateTime(),
                        To = c.DateTime(),
                        MI_NRGR = c.Int(nullable: false),
                        PERS_NRGR = c.Int(nullable: false),
                        SK_NRGR = c.Int(nullable: false),
                        STARV20 = c.String(),
                        DAGAR_GR = c.Int(nullable: false),
                        MemberId = c.Int(),
                        ShippingCompanyId = c.Int(),
                        ShipId = c.Int(),
                        TR_HOVDId = c.Int(),
                        JobId = c.Int(),
                        StatusId = c.Int(),
                        ShipTypeId = c.Int(),
                        PostalId = c.Int(),
                        CompanyId = c.Int(),
                        MemberTypeId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Job", t => t.JobId)
                .ForeignKey("dbo.Member", t => t.MemberId)
                .ForeignKey("dbo.MemberType", t => t.MemberTypeId)
                .ForeignKey("dbo.Postal", t => t.PostalId)
                .ForeignKey("dbo.Ship", t => t.ShipId)
                .ForeignKey("dbo.ShippingCompany", t => t.ShippingCompanyId)
                .ForeignKey("dbo.ShipType", t => t.ShipTypeId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .ForeignKey("dbo.Trip", t => t.TR_HOVDId)
                .Index(t => t.MemberId)
                .Index(t => t.ShippingCompanyId)
                .Index(t => t.ShipId)
                .Index(t => t.TR_HOVDId)
                .Index(t => t.JobId)
                .Index(t => t.StatusId)
                .Index(t => t.ShipTypeId)
                .Index(t => t.PostalId)
                .Index(t => t.CompanyId)
                .Index(t => t.MemberTypeId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CID = c.String(),
                        Code = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        ContactCompanyName = c.String(),
                        ContactPersonName = c.String(),
                        AVR_IALT = c.Int(),
                        KG_IALT = c.Decimal(precision: 18, scale: 4),
                        LAST_DATO = c.DateTime(),
                        LAST_ID = c.String(),
                        EntityId = c.Int(),
                        CountryCode = c.String(),
                        CountryName = c.String(),
                        AddressLine = c.String(),
                        PostalCode = c.Int(),
                        CityName = c.String(),
                        PhoneCountryCode = c.Int(),
                        PhoneNumber = c.String(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Entity", t => t.EntityId)
                .Index(t => t.EntityId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.CompanyComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        CompanyId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OldId = c.Int(nullable: false),
                        Code = c.String(),
                        Description = c.String(),
                        Description2 = c.String(),
                        STUTT = c.String(),
                        Amount = c.Int(),
                        TAL_DATO = c.DateTime(),
                        TAL_ID = c.String(),
                        PART = c.Decimal(precision: 18, scale: 4),
                        TIL_DG = c.Decimal(precision: 18, scale: 4),
                        TIL_TUR = c.Decimal(precision: 18, scale: 4),
                        Organization = c.String(),
                        HasInsurance = c.Boolean(nullable: false),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NR = c.Int(nullable: false),
                        BURT_DG = c.Decimal(precision: 18, scale: 4),
                        TUR_IALT = c.Decimal(precision: 18, scale: 4),
                        LAST_DATO = c.DateTime(),
                        LAST_ID = c.String(),
                        LastSignOnId = c.Int(),
                        JobTitle = c.String(),
                        JobId = c.Int(),
                        MemberTypeId = c.Int(),
                        PRI_OWN = c.Int(),
                        BETALER = c.Int(),
                        M_STATUS = c.String(),
                        MI_TOT = c.Int(),
                        LG_TOT = c.Int(),
                        MI_AR = c.String(),
                        MI_ID = c.String(),
                        LG_AR = c.String(),
                        LG_KR = c.Decimal(precision: 18, scale: 4),
                        ETI_DATO = c.DateTime(),
                        ETI_ID = c.String(),
                        MemberTypeCode = c.String(),
                        MemberTypeDescription = c.String(),
                        JobCode = c.String(),
                        JobDescription = c.String(),
                        GenderCode = c.String(),
                        Age = c.Int(),
                        Status = c.String(),
                        PersonId = c.Int(),
                        CountryCode = c.String(),
                        CountryName = c.String(),
                        AddressLine = c.String(),
                        PostalCode = c.Int(),
                        CityName = c.String(),
                        PhoneCountryCode = c.Int(),
                        PhoneNumber = c.String(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Job", t => t.JobId)
                .ForeignKey("dbo.SignOn", t => t.LastSignOnId)
                .ForeignKey("dbo.MemberType", t => t.MemberTypeId)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .Index(t => t.LastSignOnId)
                .Index(t => t.JobId)
                .Index(t => t.MemberTypeId)
                .Index(t => t.PersonId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.MemberComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        MemberId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Member", t => t.MemberId)
                .Index(t => t.MemberId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.DEB_CON",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NR = c.Int(),
                        NR_DEB = c.Int(),
                        SALDO = c.Decimal(precision: 18, scale: 4),
                        JobId = c.Int(),
                        PostalId = c.Int(),
                        MemberTypeId = c.Int(),
                        MemberId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Job", t => t.JobId)
                .ForeignKey("dbo.Member", t => t.MemberId)
                .ForeignKey("dbo.MemberType", t => t.MemberTypeId)
                .ForeignKey("dbo.Postal", t => t.PostalId)
                .Index(t => t.JobId)
                .Index(t => t.PostalId)
                .Index(t => t.MemberTypeId)
                .Index(t => t.MemberId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.MemberType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        BREV_UT = c.String(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.MemberFinancialAid",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.DateTime(),
                        To = c.DateTime(),
                        Days = c.Int(),
                        SocialServicePayment = c.Decimal(precision: 18, scale: 4),
                        PaymentPerDay = c.Decimal(precision: 18, scale: 4),
                        OurPayment = c.Decimal(precision: 18, scale: 4),
                        PrintedOn = c.DateTime(),
                        PrintedById = c.String(),
                        PrintedAmount = c.Int(),
                        MemberId = c.Int(),
                        BankAccountId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccount", t => t.BankAccountId)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Member", t => t.MemberId)
                .Index(t => t.MemberId)
                .Index(t => t.BankAccountId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.HolidayPay",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.String(),
                        Amount = c.Decimal(precision: 18, scale: 4),
                        TRSP = c.Decimal(precision: 18, scale: 4),
                        FRT_DATO = c.DateTime(),
                        TRS_DATO = c.DateTime(),
                        LO_KR_A = c.Decimal(precision: 18, scale: 4),
                        LO_KR_F = c.Decimal(precision: 18, scale: 4),
                        LO_KR_FRT = c.Decimal(precision: 18, scale: 4),
                        LO_DATO = c.DateTime(),
                        DG_AR = c.Int(),
                        TU_AR = c.Int(),
                        DATO_AR = c.DateTime(),
                        ID_AR = c.String(),
                        SAML_KR = c.Decimal(precision: 18, scale: 4),
                        SAML_KR2 = c.Decimal(precision: 18, scale: 4),
                        MemberId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Member", t => t.MemberId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.HolidayPay_HOVD",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployerNumber = c.Int(),
                        TransferDate = c.DateTime(),
                        Amount = c.Decimal(precision: 18, scale: 4),
                        ART = c.String(),
                        KOYR_DATO = c.DateTime(),
                        FRT_NR = c.Int(),
                        KONTO = c.Int(),
                        REG_NR = c.Int(),
                        KONTO_FF = c.Int(),
                        REG_NR_FF = c.Int(),
                        PLUS = c.String(),
                        TR_NR = c.Int(),
                        X = c.Int(),
                        MemberId = c.Int(),
                        BankAccountId = c.Int(),
                        BankAccountFFId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccount", t => t.BankAccountId)
                .ForeignKey("dbo.BankAccount", t => t.BankAccountFFId)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Member", t => t.MemberId)
                .Index(t => t.MemberId)
                .Index(t => t.BankAccountId)
                .Index(t => t.BankAccountFFId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.SignOn",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SignOnNumber = c.Int(),
                        PersonNumber = c.Int(),
                        Year = c.String(),
                        From = c.DateTime(),
                        To = c.DateTime(),
                        SK_ID = c.String(),
                        PART = c.Decimal(precision: 18, scale: 4),
                        KR_IALT = c.Decimal(precision: 18, scale: 4),
                        ID_TUR = c.String(),
                        TR_NR = c.Int(),
                        TIL_PR_DG = c.Decimal(precision: 18, scale: 4),
                        TIL_PR_TUR = c.Decimal(precision: 18, scale: 4),
                        EmployerNumber = c.Int(),
                        I_PART = c.Decimal(precision: 18, scale: 4),
                        FRT_NR = c.Int(),
                        TRYG_JN = c.String(),
                        HasInsurance = c.Boolean(nullable: false),
                        LaborInsuranceAmountPerDay = c.Decimal(precision: 18, scale: 4),
                        TRYG_KVT = c.String(),
                        MemberId = c.Int(),
                        ShippingCompanyId = c.Int(),
                        ShipCode = c.String(),
                        ShipName = c.String(),
                        TripId = c.Int(),
                        JobWhileSignedOnId = c.Int(),
                        MemberTypeId = c.Int(),
                        CompanyId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Job", t => t.JobWhileSignedOnId)
                .ForeignKey("dbo.MemberType", t => t.MemberTypeId)
                .ForeignKey("dbo.ShippingCompany", t => t.ShippingCompanyId)
                .ForeignKey("dbo.Trip", t => t.TripId)
                .ForeignKey("dbo.Member", t => t.MemberId)
                .Index(t => t.MemberId)
                .Index(t => t.ShippingCompanyId)
                .Index(t => t.TripId)
                .Index(t => t.JobWhileSignedOnId)
                .Index(t => t.MemberTypeId)
                .Index(t => t.CompanyId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.MemberPayment2",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberPaymentNR = c.Int(),
                        U1 = c.Decimal(precision: 18, scale: 4),
                        U2 = c.Decimal(precision: 18, scale: 4),
                        U3 = c.Decimal(precision: 18, scale: 4),
                        U4 = c.Decimal(precision: 18, scale: 4),
                        U5 = c.Decimal(precision: 18, scale: 4),
                        U6 = c.Decimal(precision: 18, scale: 4),
                        U7 = c.Decimal(precision: 18, scale: 4),
                        JobId = c.Int(),
                        PostalId = c.Int(),
                        MemberId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Job", t => t.JobId)
                .ForeignKey("dbo.Member", t => t.MemberId)
                .ForeignKey("dbo.Postal", t => t.PostalId)
                .Index(t => t.JobId)
                .Index(t => t.PostalId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.P400",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NR = c.Int(),
                        FNAVN = c.String(),
                        ENAVN = c.String(),
                        POST = c.Int(),
                        KR = c.Decimal(precision: 18, scale: 4),
                        ART = c.String(),
                        UD_DATO = c.DateTime(),
                        UD_ID = c.String(),
                        BankAccountId = c.Int(),
                        MemberId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccount", t => t.BankAccountId)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Member", t => t.MemberId)
                .Index(t => t.BankAccountId)
                .Index(t => t.MemberId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.MemberPayment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberPaymentNR = c.Int(),
                        MemberNR = c.Int(),
                        Year = c.String(),
                        Price = c.Decimal(precision: 18, scale: 4),
                        PaidOn = c.DateTime(),
                        PaidById = c.String(),
                        HolidayPay = c.Decimal(precision: 18, scale: 4),
                        MembershipPayment = c.Decimal(precision: 18, scale: 4),
                        Code = c.String(),
                        TRANSP_IALT = c.Decimal(precision: 18, scale: 4),
                        K1 = c.String(),
                        MemberId = c.Int(),
                        BankAccountId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccount", t => t.BankAccountId)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Member", t => t.MemberId)
                .Index(t => t.MemberId)
                .Index(t => t.BankAccountId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityId = c.Int(),
                        SSN = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        FullName = c.String(),
                        Birthday = c.DateTime(),
                        IsAlive = c.Boolean(nullable: false),
                        GenderId = c.Int(),
                        TitleId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Entity", t => t.EntityId)
                .ForeignKey("dbo.PersonGender", t => t.GenderId)
                .ForeignKey("dbo.PersonTitle", t => t.TitleId)
                .Index(t => t.EntityId)
                .Index(t => t.GenderId)
                .Index(t => t.TitleId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.PersonGender",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.PersonTitle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.TripCrewAid",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TripNumber = c.Int(),
                        ART = c.String(),
                        Amount = c.Decimal(precision: 18, scale: 4),
                        Price = c.Decimal(precision: 18, scale: 4),
                        AlternativePrice = c.Decimal(precision: 18, scale: 4),
                        KR = c.Decimal(precision: 18, scale: 4),
                        PortOfLanding = c.String(),
                        DateOfLanding = c.DateTime(),
                        CompanyId = c.Int(),
                        ShippingCompanyId = c.Int(),
                        ShipId = c.Int(),
                        ShipTypeId = c.Int(),
                        PostalId = c.Int(),
                        TripId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Postal", t => t.PostalId)
                .ForeignKey("dbo.Ship", t => t.ShipId)
                .ForeignKey("dbo.ShippingCompany", t => t.ShippingCompanyId)
                .ForeignKey("dbo.ShipType", t => t.ShipTypeId)
                .ForeignKey("dbo.Trip", t => t.TripId)
                .Index(t => t.CompanyId)
                .Index(t => t.ShippingCompanyId)
                .Index(t => t.ShipId)
                .Index(t => t.ShipTypeId)
                .Index(t => t.PostalId)
                .Index(t => t.TripId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.TripDeduction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TripNumber = c.Int(),
                        ART = c.String(),
                        Amount = c.Decimal(precision: 18, scale: 4),
                        Price = c.Decimal(precision: 18, scale: 4),
                        DateOfLanding = c.DateTime(),
                        PortOfLandingId = c.Int(),
                        ShippingCompanyId = c.Int(),
                        ShipId = c.Int(),
                        ShipTypeId = c.Int(),
                        TripId = c.Int(),
                        DeductionTypeId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.DeductionType", t => t.DeductionTypeId)
                .ForeignKey("dbo.Company", t => t.PortOfLandingId)
                .ForeignKey("dbo.Ship", t => t.ShipId)
                .ForeignKey("dbo.ShippingCompany", t => t.ShippingCompanyId)
                .ForeignKey("dbo.ShipType", t => t.ShipTypeId)
                .ForeignKey("dbo.Trip", t => t.TripId)
                .Index(t => t.PortOfLandingId)
                .Index(t => t.ShippingCompanyId)
                .Index(t => t.ShipId)
                .Index(t => t.ShipTypeId)
                .Index(t => t.TripId)
                .Index(t => t.DeductionTypeId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.DeductionType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Type = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.TripLine",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TripNumber = c.Int(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 4),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 8),
                        AlternativePrice = c.Decimal(precision: 18, scale: 4),
                        KR = c.Decimal(nullable: false, precision: 18, scale: 4),
                        PortOfLanding = c.String(),
                        DateOfLanding = c.DateTime(nullable: false),
                        RAD = c.String(),
                        Year = c.String(),
                        STUD_M = c.Decimal(precision: 18, scale: 4),
                        STUD_S = c.Decimal(precision: 18, scale: 4),
                        ShippingCompanyId = c.Int(),
                        TripId = c.Int(),
                        FishSpeciesId = c.Int(),
                        ShipTypeId = c.Int(),
                        PostalId = c.Int(),
                        CompanyId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.FishSpecies", t => t.FishSpeciesId)
                .ForeignKey("dbo.Postal", t => t.PostalId)
                .ForeignKey("dbo.ShippingCompany", t => t.ShippingCompanyId)
                .ForeignKey("dbo.ShipType", t => t.ShipTypeId)
                .ForeignKey("dbo.Trip", t => t.TripId)
                .Index(t => t.ShippingCompanyId)
                .Index(t => t.TripId)
                .Index(t => t.FishSpeciesId)
                .Index(t => t.ShipTypeId)
                .Index(t => t.PostalId)
                .Index(t => t.CompanyId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.TripShipAid",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TripNumber = c.Int(),
                        ART = c.String(),
                        Amount = c.Decimal(precision: 18, scale: 4),
                        Price = c.Decimal(precision: 18, scale: 4),
                        AlternativePrice = c.Decimal(precision: 18, scale: 4),
                        KR = c.Decimal(precision: 18, scale: 4),
                        PortOfLanding = c.String(),
                        DateOfLanding = c.DateTime(),
                        CompanyId = c.Int(),
                        ShippingCompanyId = c.Int(),
                        ShipId = c.Int(),
                        ShipTypeId = c.Int(),
                        PostalId = c.Int(),
                        TripId = c.Int(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Postal", t => t.PostalId)
                .ForeignKey("dbo.Ship", t => t.ShipId)
                .ForeignKey("dbo.ShippingCompany", t => t.ShippingCompanyId)
                .ForeignKey("dbo.ShipType", t => t.ShipTypeId)
                .ForeignKey("dbo.Trip", t => t.TripId)
                .Index(t => t.CompanyId)
                .Index(t => t.ShippingCompanyId)
                .Index(t => t.ShipId)
                .Index(t => t.ShipTypeId)
                .Index(t => t.PostalId)
                .Index(t => t.TripId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.FishPrice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FishSpeciesCode = c.String(),
                        From = c.DateTime(),
                        To = c.DateTime(),
                        Price = c.Decimal(precision: 18, scale: 4),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.MinimumWage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PerMonth = c.Decimal(precision: 18, scale: 4),
                        PerDay = c.Decimal(precision: 18, scale: 4),
                        DG_MIN = c.Decimal(precision: 18, scale: 4),
                        DG_MAX = c.Decimal(precision: 18, scale: 4),
                        GRAD = c.Decimal(precision: 18, scale: 4),
                        DG_ST = c.Decimal(precision: 18, scale: 4),
                        Code = c.String(),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .Index(t => t.ChangeEventId);
            
            CreateTable(
                "dbo.TripShippingCompany",
                c => new
                    {
                        Trip_Id = c.Int(nullable: false),
                        ShippingCompany_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trip_Id, t.ShippingCompany_Id })
                .ForeignKey("dbo.Trip", t => t.Trip_Id, cascadeDelete: true)
                .ForeignKey("dbo.ShippingCompany", t => t.ShippingCompany_Id, cascadeDelete: true)
                .Index(t => t.Trip_Id)
                .Index(t => t.ShippingCompany_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MinimumWage", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.FishPrice", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.AidPrice", "ShipTypeId", "dbo.ShipType");
            DropForeignKey("dbo.Trip", "ShipId", "dbo.Ship");
            DropForeignKey("dbo.Ship", "ShipTypeId", "dbo.ShipType");
            DropForeignKey("dbo.Ship", "ShippingCompanyId", "dbo.ShippingCompany");
            DropForeignKey("dbo.TripShipAid", "TripId", "dbo.Trip");
            DropForeignKey("dbo.TripShipAid", "ShipTypeId", "dbo.ShipType");
            DropForeignKey("dbo.TripShipAid", "ShippingCompanyId", "dbo.ShippingCompany");
            DropForeignKey("dbo.TripShipAid", "ShipId", "dbo.Ship");
            DropForeignKey("dbo.TripShipAid", "PostalId", "dbo.Postal");
            DropForeignKey("dbo.TripShipAid", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.TripShipAid", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.TripLine", "TripId", "dbo.Trip");
            DropForeignKey("dbo.TripLine", "ShipTypeId", "dbo.ShipType");
            DropForeignKey("dbo.TripLine", "ShippingCompanyId", "dbo.ShippingCompany");
            DropForeignKey("dbo.TripLine", "PostalId", "dbo.Postal");
            DropForeignKey("dbo.TripLine", "FishSpeciesId", "dbo.FishSpecies");
            DropForeignKey("dbo.TripLine", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.TripLine", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.TripDeduction", "TripId", "dbo.Trip");
            DropForeignKey("dbo.TripDeduction", "ShipTypeId", "dbo.ShipType");
            DropForeignKey("dbo.TripDeduction", "ShippingCompanyId", "dbo.ShippingCompany");
            DropForeignKey("dbo.TripDeduction", "ShipId", "dbo.Ship");
            DropForeignKey("dbo.TripDeduction", "PortOfLandingId", "dbo.Company");
            DropForeignKey("dbo.TripDeduction", "DeductionTypeId", "dbo.DeductionType");
            DropForeignKey("dbo.DeductionType", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.TripDeduction", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.TripCrewAid", "TripId", "dbo.Trip");
            DropForeignKey("dbo.TripCrewAid", "ShipTypeId", "dbo.ShipType");
            DropForeignKey("dbo.TripCrewAid", "ShippingCompanyId", "dbo.ShippingCompany");
            DropForeignKey("dbo.TripCrewAid", "ShipId", "dbo.Ship");
            DropForeignKey("dbo.TripCrewAid", "PostalId", "dbo.Postal");
            DropForeignKey("dbo.TripCrewAid", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.TripCrewAid", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.Trip", "ShipTypeId", "dbo.ShipType");
            DropForeignKey("dbo.TripShippingCompany", "ShippingCompany_Id", "dbo.ShippingCompany");
            DropForeignKey("dbo.TripShippingCompany", "Trip_Id", "dbo.Trip");
            DropForeignKey("dbo.Trip", "PortOfLandingId", "dbo.Company");
            DropForeignKey("dbo.Trip", "PairShipId", "dbo.Ship");
            DropForeignKey("dbo.MI2_HOVD", "TR_HOVDId", "dbo.Trip");
            DropForeignKey("dbo.MI2_HOVD", "StatusId", "dbo.Status");
            DropForeignKey("dbo.MI2_HOVD", "ShipTypeId", "dbo.ShipType");
            DropForeignKey("dbo.MI2_HOVD", "ShippingCompanyId", "dbo.ShippingCompany");
            DropForeignKey("dbo.MI2_HOVD", "ShipId", "dbo.Ship");
            DropForeignKey("dbo.MI2_HOVD", "PostalId", "dbo.Postal");
            DropForeignKey("dbo.MI2_HOVD", "MemberTypeId", "dbo.MemberType");
            DropForeignKey("dbo.MI2_HOVD", "MemberId", "dbo.Member");
            DropForeignKey("dbo.MI2_HOVD", "JobId", "dbo.Job");
            DropForeignKey("dbo.SignOn", "MemberId", "dbo.Member");
            DropForeignKey("dbo.Member", "PersonId", "dbo.Person");
            DropForeignKey("dbo.Person", "TitleId", "dbo.PersonTitle");
            DropForeignKey("dbo.PersonTitle", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.Person", "GenderId", "dbo.PersonGender");
            DropForeignKey("dbo.PersonGender", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.Person", "EntityId", "dbo.Entity");
            DropForeignKey("dbo.Person", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.MemberPayment", "MemberId", "dbo.Member");
            DropForeignKey("dbo.MemberPayment", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.MemberPayment", "BankAccountId", "dbo.BankAccount");
            DropForeignKey("dbo.P400", "MemberId", "dbo.Member");
            DropForeignKey("dbo.P400", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.P400", "BankAccountId", "dbo.BankAccount");
            DropForeignKey("dbo.Member", "MemberTypeId", "dbo.MemberType");
            DropForeignKey("dbo.MemberPayment2", "PostalId", "dbo.Postal");
            DropForeignKey("dbo.MemberPayment2", "MemberId", "dbo.Member");
            DropForeignKey("dbo.MemberPayment2", "JobId", "dbo.Job");
            DropForeignKey("dbo.Member", "LastSignOnId", "dbo.SignOn");
            DropForeignKey("dbo.SignOn", "TripId", "dbo.Trip");
            DropForeignKey("dbo.SignOn", "ShippingCompanyId", "dbo.ShippingCompany");
            DropForeignKey("dbo.SignOn", "MemberTypeId", "dbo.MemberType");
            DropForeignKey("dbo.SignOn", "JobWhileSignedOnId", "dbo.Job");
            DropForeignKey("dbo.SignOn", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.SignOn", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.Member", "JobId", "dbo.Job");
            DropForeignKey("dbo.HolidayPay_HOVD", "MemberId", "dbo.Member");
            DropForeignKey("dbo.HolidayPay_HOVD", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.HolidayPay_HOVD", "BankAccountFFId", "dbo.BankAccount");
            DropForeignKey("dbo.HolidayPay_HOVD", "BankAccountId", "dbo.BankAccount");
            DropForeignKey("dbo.HolidayPay", "MemberId", "dbo.Member");
            DropForeignKey("dbo.MemberFinancialAid", "MemberId", "dbo.Member");
            DropForeignKey("dbo.MemberFinancialAid", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.MemberFinancialAid", "BankAccountId", "dbo.BankAccount");
            DropForeignKey("dbo.DEB_CON", "PostalId", "dbo.Postal");
            DropForeignKey("dbo.DEB_CON", "MemberTypeId", "dbo.MemberType");
            DropForeignKey("dbo.MemberType", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.DEB_CON", "MemberId", "dbo.Member");
            DropForeignKey("dbo.DEB_CON", "JobId", "dbo.Job");
            DropForeignKey("dbo.DEB_CON", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.MemberComment", "MemberId", "dbo.Member");
            DropForeignKey("dbo.MemberComment", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.Member", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.Job", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.MI2_HOVD", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Company", "EntityId", "dbo.Entity");
            DropForeignKey("dbo.CompanyComment", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.CompanyComment", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.Company", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.MI2_HOVD", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.Trip", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.ShippingCompany", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Status", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.ShippingCompany", "EntityId", "dbo.Entity");
            DropForeignKey("dbo.ShippingCompany", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.Ship", "EntityId", "dbo.Entity");
            DropForeignKey("dbo.ShipComment", "ShipId", "dbo.Ship");
            DropForeignKey("dbo.ShipComment", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.Ship", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.ShipType", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.AidPrice", "FishSpeciesId", "dbo.FishSpecies");
            DropForeignKey("dbo.FishSpecies", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.AidPrice", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.AidPrice", "AidTypeId", "dbo.AidType");
            DropForeignKey("dbo.AidType", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.Address", "PostalId", "dbo.Postal");
            DropForeignKey("dbo.Postal", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.Address", "EntityId", "dbo.Entity");
            DropForeignKey("dbo.Address", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Country", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.Address", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.ChangeEventItem", "EntityId", "dbo.Entity");
            DropForeignKey("dbo.Website", "EntityId", "dbo.Entity");
            DropForeignKey("dbo.Website", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.SocialNetwork", "EntityId", "dbo.Entity");
            DropForeignKey("dbo.SocialNetwork", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.Phone", "EntityId", "dbo.Entity");
            DropForeignKey("dbo.Phone", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.EmailAddress", "EntityId", "dbo.Entity");
            DropForeignKey("dbo.EmailAddress", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.Comment", "EntityId", "dbo.Entity");
            DropForeignKey("dbo.Comment", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.BankAccount", "EntityId", "dbo.Entity");
            DropForeignKey("dbo.BankAccount", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.BankAccount", "BankId", "dbo.Bank");
            DropForeignKey("dbo.Bank", "ChangeEventId", "dbo.ChangeEvent");
            DropForeignKey("dbo.ChangeEventItem", "ChangeEventId", "dbo.ChangeEvent");
            DropIndex("dbo.TripShippingCompany", new[] { "ShippingCompany_Id" });
            DropIndex("dbo.TripShippingCompany", new[] { "Trip_Id" });
            DropIndex("dbo.MinimumWage", new[] { "ChangeEventId" });
            DropIndex("dbo.FishPrice", new[] { "ChangeEventId" });
            DropIndex("dbo.TripShipAid", new[] { "ChangeEventId" });
            DropIndex("dbo.TripShipAid", new[] { "TripId" });
            DropIndex("dbo.TripShipAid", new[] { "PostalId" });
            DropIndex("dbo.TripShipAid", new[] { "ShipTypeId" });
            DropIndex("dbo.TripShipAid", new[] { "ShipId" });
            DropIndex("dbo.TripShipAid", new[] { "ShippingCompanyId" });
            DropIndex("dbo.TripShipAid", new[] { "CompanyId" });
            DropIndex("dbo.TripLine", new[] { "ChangeEventId" });
            DropIndex("dbo.TripLine", new[] { "CompanyId" });
            DropIndex("dbo.TripLine", new[] { "PostalId" });
            DropIndex("dbo.TripLine", new[] { "ShipTypeId" });
            DropIndex("dbo.TripLine", new[] { "FishSpeciesId" });
            DropIndex("dbo.TripLine", new[] { "TripId" });
            DropIndex("dbo.TripLine", new[] { "ShippingCompanyId" });
            DropIndex("dbo.DeductionType", new[] { "ChangeEventId" });
            DropIndex("dbo.TripDeduction", new[] { "ChangeEventId" });
            DropIndex("dbo.TripDeduction", new[] { "DeductionTypeId" });
            DropIndex("dbo.TripDeduction", new[] { "TripId" });
            DropIndex("dbo.TripDeduction", new[] { "ShipTypeId" });
            DropIndex("dbo.TripDeduction", new[] { "ShipId" });
            DropIndex("dbo.TripDeduction", new[] { "ShippingCompanyId" });
            DropIndex("dbo.TripDeduction", new[] { "PortOfLandingId" });
            DropIndex("dbo.TripCrewAid", new[] { "ChangeEventId" });
            DropIndex("dbo.TripCrewAid", new[] { "TripId" });
            DropIndex("dbo.TripCrewAid", new[] { "PostalId" });
            DropIndex("dbo.TripCrewAid", new[] { "ShipTypeId" });
            DropIndex("dbo.TripCrewAid", new[] { "ShipId" });
            DropIndex("dbo.TripCrewAid", new[] { "ShippingCompanyId" });
            DropIndex("dbo.TripCrewAid", new[] { "CompanyId" });
            DropIndex("dbo.PersonTitle", new[] { "ChangeEventId" });
            DropIndex("dbo.PersonGender", new[] { "ChangeEventId" });
            DropIndex("dbo.Person", new[] { "ChangeEventId" });
            DropIndex("dbo.Person", new[] { "TitleId" });
            DropIndex("dbo.Person", new[] { "GenderId" });
            DropIndex("dbo.Person", new[] { "EntityId" });
            DropIndex("dbo.MemberPayment", new[] { "ChangeEventId" });
            DropIndex("dbo.MemberPayment", new[] { "BankAccountId" });
            DropIndex("dbo.MemberPayment", new[] { "MemberId" });
            DropIndex("dbo.P400", new[] { "ChangeEventId" });
            DropIndex("dbo.P400", new[] { "MemberId" });
            DropIndex("dbo.P400", new[] { "BankAccountId" });
            DropIndex("dbo.MemberPayment2", new[] { "MemberId" });
            DropIndex("dbo.MemberPayment2", new[] { "PostalId" });
            DropIndex("dbo.MemberPayment2", new[] { "JobId" });
            DropIndex("dbo.SignOn", new[] { "ChangeEventId" });
            DropIndex("dbo.SignOn", new[] { "CompanyId" });
            DropIndex("dbo.SignOn", new[] { "MemberTypeId" });
            DropIndex("dbo.SignOn", new[] { "JobWhileSignedOnId" });
            DropIndex("dbo.SignOn", new[] { "TripId" });
            DropIndex("dbo.SignOn", new[] { "ShippingCompanyId" });
            DropIndex("dbo.SignOn", new[] { "MemberId" });
            DropIndex("dbo.HolidayPay_HOVD", new[] { "ChangeEventId" });
            DropIndex("dbo.HolidayPay_HOVD", new[] { "BankAccountFFId" });
            DropIndex("dbo.HolidayPay_HOVD", new[] { "BankAccountId" });
            DropIndex("dbo.HolidayPay_HOVD", new[] { "MemberId" });
            DropIndex("dbo.HolidayPay", new[] { "MemberId" });
            DropIndex("dbo.MemberFinancialAid", new[] { "ChangeEventId" });
            DropIndex("dbo.MemberFinancialAid", new[] { "BankAccountId" });
            DropIndex("dbo.MemberFinancialAid", new[] { "MemberId" });
            DropIndex("dbo.MemberType", new[] { "ChangeEventId" });
            DropIndex("dbo.DEB_CON", new[] { "ChangeEventId" });
            DropIndex("dbo.DEB_CON", new[] { "MemberId" });
            DropIndex("dbo.DEB_CON", new[] { "MemberTypeId" });
            DropIndex("dbo.DEB_CON", new[] { "PostalId" });
            DropIndex("dbo.DEB_CON", new[] { "JobId" });
            DropIndex("dbo.MemberComment", new[] { "ChangeEventId" });
            DropIndex("dbo.MemberComment", new[] { "MemberId" });
            DropIndex("dbo.Member", new[] { "ChangeEventId" });
            DropIndex("dbo.Member", new[] { "PersonId" });
            DropIndex("dbo.Member", new[] { "MemberTypeId" });
            DropIndex("dbo.Member", new[] { "JobId" });
            DropIndex("dbo.Member", new[] { "LastSignOnId" });
            DropIndex("dbo.Job", new[] { "ChangeEventId" });
            DropIndex("dbo.CompanyComment", new[] { "ChangeEventId" });
            DropIndex("dbo.CompanyComment", new[] { "CompanyId" });
            DropIndex("dbo.Company", new[] { "ChangeEventId" });
            DropIndex("dbo.Company", new[] { "EntityId" });
            DropIndex("dbo.MI2_HOVD", new[] { "ChangeEventId" });
            DropIndex("dbo.MI2_HOVD", new[] { "MemberTypeId" });
            DropIndex("dbo.MI2_HOVD", new[] { "CompanyId" });
            DropIndex("dbo.MI2_HOVD", new[] { "PostalId" });
            DropIndex("dbo.MI2_HOVD", new[] { "ShipTypeId" });
            DropIndex("dbo.MI2_HOVD", new[] { "StatusId" });
            DropIndex("dbo.MI2_HOVD", new[] { "JobId" });
            DropIndex("dbo.MI2_HOVD", new[] { "TR_HOVDId" });
            DropIndex("dbo.MI2_HOVD", new[] { "ShipId" });
            DropIndex("dbo.MI2_HOVD", new[] { "ShippingCompanyId" });
            DropIndex("dbo.MI2_HOVD", new[] { "MemberId" });
            DropIndex("dbo.Trip", new[] { "ChangeEventId" });
            DropIndex("dbo.Trip", new[] { "PairShipId" });
            DropIndex("dbo.Trip", new[] { "ShipId" });
            DropIndex("dbo.Trip", new[] { "PortOfLandingId" });
            DropIndex("dbo.Trip", new[] { "ShipTypeId" });
            DropIndex("dbo.Status", new[] { "ChangeEventId" });
            DropIndex("dbo.ShippingCompany", new[] { "ChangeEventId" });
            DropIndex("dbo.ShippingCompany", new[] { "EntityId" });
            DropIndex("dbo.ShippingCompany", new[] { "StatusId" });
            DropIndex("dbo.ShipComment", new[] { "ChangeEventId" });
            DropIndex("dbo.ShipComment", new[] { "ShipId" });
            DropIndex("dbo.Ship", new[] { "ChangeEventId" });
            DropIndex("dbo.Ship", new[] { "EntityId" });
            DropIndex("dbo.Ship", new[] { "ShipTypeId" });
            DropIndex("dbo.Ship", new[] { "ShippingCompanyId" });
            DropIndex("dbo.ShipType", new[] { "ChangeEventId" });
            DropIndex("dbo.FishSpecies", new[] { "ChangeEventId" });
            DropIndex("dbo.AidType", new[] { "ChangeEventId" });
            DropIndex("dbo.AidPrice", new[] { "ChangeEventId" });
            DropIndex("dbo.AidPrice", new[] { "AidTypeId" });
            DropIndex("dbo.AidPrice", new[] { "ShipTypeId" });
            DropIndex("dbo.AidPrice", new[] { "FishSpeciesId" });
            DropIndex("dbo.Postal", new[] { "ChangeEventId" });
            DropIndex("dbo.Country", new[] { "ChangeEventId" });
            DropIndex("dbo.Website", new[] { "ChangeEventId" });
            DropIndex("dbo.Website", new[] { "EntityId" });
            DropIndex("dbo.SocialNetwork", new[] { "ChangeEventId" });
            DropIndex("dbo.SocialNetwork", new[] { "EntityId" });
            DropIndex("dbo.Phone", new[] { "ChangeEventId" });
            DropIndex("dbo.Phone", new[] { "EntityId" });
            DropIndex("dbo.EmailAddress", new[] { "ChangeEventId" });
            DropIndex("dbo.EmailAddress", new[] { "EntityId" });
            DropIndex("dbo.Comment", new[] { "ChangeEventId" });
            DropIndex("dbo.Comment", new[] { "EntityId" });
            DropIndex("dbo.Bank", new[] { "ChangeEventId" });
            DropIndex("dbo.BankAccount", new[] { "ChangeEventId" });
            DropIndex("dbo.BankAccount", new[] { "EntityId" });
            DropIndex("dbo.BankAccount", new[] { "BankId" });
            DropIndex("dbo.ChangeEventItem", new[] { "ChangeEventId" });
            DropIndex("dbo.ChangeEventItem", new[] { "EntityId" });
            DropIndex("dbo.Address", new[] { "ChangeEventId" });
            DropIndex("dbo.Address", new[] { "EntityId" });
            DropIndex("dbo.Address", new[] { "CountryId" });
            DropIndex("dbo.Address", new[] { "PostalId" });
            DropTable("dbo.TripShippingCompany");
            DropTable("dbo.MinimumWage");
            DropTable("dbo.FishPrice");
            DropTable("dbo.TripShipAid");
            DropTable("dbo.TripLine");
            DropTable("dbo.DeductionType");
            DropTable("dbo.TripDeduction");
            DropTable("dbo.TripCrewAid");
            DropTable("dbo.PersonTitle");
            DropTable("dbo.PersonGender");
            DropTable("dbo.Person");
            DropTable("dbo.MemberPayment");
            DropTable("dbo.P400");
            DropTable("dbo.MemberPayment2");
            DropTable("dbo.SignOn");
            DropTable("dbo.HolidayPay_HOVD");
            DropTable("dbo.HolidayPay");
            DropTable("dbo.MemberFinancialAid");
            DropTable("dbo.MemberType");
            DropTable("dbo.DEB_CON");
            DropTable("dbo.MemberComment");
            DropTable("dbo.Member");
            DropTable("dbo.Job");
            DropTable("dbo.CompanyComment");
            DropTable("dbo.Company");
            DropTable("dbo.MI2_HOVD");
            DropTable("dbo.Trip");
            DropTable("dbo.Status");
            DropTable("dbo.ShippingCompany");
            DropTable("dbo.ShipComment");
            DropTable("dbo.Ship");
            DropTable("dbo.ShipType");
            DropTable("dbo.FishSpecies");
            DropTable("dbo.AidType");
            DropTable("dbo.AidPrice");
            DropTable("dbo.Postal");
            DropTable("dbo.Country");
            DropTable("dbo.Website");
            DropTable("dbo.SocialNetwork");
            DropTable("dbo.Phone");
            DropTable("dbo.EmailAddress");
            DropTable("dbo.Comment");
            DropTable("dbo.Bank");
            DropTable("dbo.BankAccount");
            DropTable("dbo.Entity");
            DropTable("dbo.ChangeEventItem");
            DropTable("dbo.ChangeEvent");
            DropTable("dbo.Address");
        }
    }
}
