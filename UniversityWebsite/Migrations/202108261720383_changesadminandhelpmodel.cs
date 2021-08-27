namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesadminandhelpmodel : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Helps");
            AddColumn("dbo.Admins", "A1", c => c.String(nullable: false));
            AddColumn("dbo.Admins", "A2", c => c.String(nullable: false));
            AddColumn("dbo.Admins", "A3", c => c.String(nullable: false));
            AlterColumn("dbo.Helps", "HelpId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Helps", "HelpId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Helps");
            AlterColumn("dbo.Helps", "HelpId", c => c.Guid(nullable: false, identity: true));
            DropColumn("dbo.Admins", "A3");
            DropColumn("dbo.Admins", "A2");
            DropColumn("dbo.Admins", "A1");
            AddPrimaryKey("dbo.Helps", "HelpId");
        }
    }
}
