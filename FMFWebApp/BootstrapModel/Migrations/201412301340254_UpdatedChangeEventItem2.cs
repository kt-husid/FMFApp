namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedChangeEventItem2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChangeEventItem", "EntityId", c => c.Int());
            CreateIndex("dbo.ChangeEventItem", "EntityId");
            AddForeignKey("dbo.ChangeEventItem", "EntityId", "dbo.Entity", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChangeEventItem", "EntityId", "dbo.Entity");
            DropIndex("dbo.ChangeEventItem", new[] { "EntityId" });
            DropColumn("dbo.ChangeEventItem", "EntityId");
        }
    }
}
