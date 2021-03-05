namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFishPrice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FishPrice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FishSpeciesCode = c.String(),
                        From = c.DateTime(),
                        To = c.DateTime(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        ChangeEventId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChangeEvent", t => t.ChangeEventId)
                .Index(t => t.ChangeEventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FishPrice", "ChangeEventId", "dbo.ChangeEvent");
            DropIndex("dbo.FishPrice", new[] { "ChangeEventId" });
            DropTable("dbo.FishPrice");
        }
    }
}
