namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableShareChangedAlertToSeen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shares", "Seen", c => c.Boolean(nullable: false));
            DropColumn("dbo.Shares", "Alert");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shares", "Alert", c => c.Boolean(nullable: false));
            DropColumn("dbo.Shares", "Seen");
        }
    }
}
