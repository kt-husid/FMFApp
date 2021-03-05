namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedSignOnTrans : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SignOnTrans", "SignOnId", "dbo.SignOn");
            DropIndex("dbo.SignOnTrans", new[] { "SignOnId" });
            DropTable("dbo.SignOnTrans");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SignOnTrans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IND_DATO = c.DateTime(),
                        IND_ID = c.String(),
                        Status = c.String(),
                        From = c.String(),
                        To = c.String(),
                        FELT = c.Int(),
                        SignOnId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.SignOnTrans", "SignOnId");
            AddForeignKey("dbo.SignOnTrans", "SignOnId", "dbo.SignOn", "Id");
        }
    }
}
