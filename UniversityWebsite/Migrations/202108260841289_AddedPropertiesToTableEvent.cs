namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPropertiesToTableEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Participated_User", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Participated_User");
        }
    }
}
