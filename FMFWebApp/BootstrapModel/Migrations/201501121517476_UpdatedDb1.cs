namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDb1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Member", "INN_IALT");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Member", "INN_IALT", c => c.Decimal(precision: 18, scale: 3));
        }
    }
}
