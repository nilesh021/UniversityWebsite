namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkOnUserReaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Club_User_Relation", "Vote", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Club_User_Relation", "Vote");
        }
    }
}
