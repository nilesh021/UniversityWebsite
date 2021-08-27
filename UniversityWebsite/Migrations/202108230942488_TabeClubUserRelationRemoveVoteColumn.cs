namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabeClubUserRelationRemoveVoteColumn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Club_User_Relation", "Vote");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Club_User_Relation", "Vote", c => c.Int(nullable: false));
        }
    }
}
