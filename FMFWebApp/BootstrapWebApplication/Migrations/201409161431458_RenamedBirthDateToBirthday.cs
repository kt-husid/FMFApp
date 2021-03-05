namespace BootstrapWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedBirthDateToBirthday : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime());
            DropColumn("dbo.AspNetUsers", "BirthDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime());
            DropColumn("dbo.AspNetUsers", "Birthday");
        }
    }
}
