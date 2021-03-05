namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMinimumWage : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MinimumWage", "ChangeEventId", "dbo.ChangeEvent");
            DropIndex("dbo.MinimumWage", new[] { "ChangeEventId" });
            DropTable("dbo.MinimumWage");
        }
    }
}
