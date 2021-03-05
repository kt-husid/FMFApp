namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDuplicateField_GenderCode_ToMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Member", "GenderCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Member", "GenderCode");
        }
    }
}
