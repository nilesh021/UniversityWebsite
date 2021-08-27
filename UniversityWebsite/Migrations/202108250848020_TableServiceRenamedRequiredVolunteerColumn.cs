namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableServiceRenamedRequiredVolunteerColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "Required_Volunteer", c => c.Int(nullable: false));
            DropColumn("dbo.Services", "Reqired_Volunteer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Services", "Reqired_Volunteer", c => c.Int(nullable: false));
            DropColumn("dbo.Services", "Required_Volunteer");
        }
    }
}
