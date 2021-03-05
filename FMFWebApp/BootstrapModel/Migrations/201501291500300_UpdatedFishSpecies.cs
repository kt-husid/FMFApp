namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedFishSpecies : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FishSpecies", "OldCode", c => c.String());
            AddColumn("dbo.FishSpecies", "OldName", c => c.String());
            AddColumn("dbo.FishSpecies", "FishSpeciesNumber", c => c.Int(nullable: true));
            AddColumn("dbo.FishSpecies", "IsActive", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FishSpecies", "IsActive");
            DropColumn("dbo.FishSpecies", "FishSpeciesNumber");
            DropColumn("dbo.FishSpecies", "OldName");
            DropColumn("dbo.FishSpecies", "OldCode");
        }
    }
}
