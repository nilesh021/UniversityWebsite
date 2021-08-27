namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetDefaultValuesForVoteCommentInIdeaUserRelation : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Idea_User_Relation SET Vote = 0 WHERE Vote = NULL");
            Sql("UPDATE Idea_User_Relation SET Comment = ' ' WHERE Comment = NULL");
        }
        
        public override void Down()
        {
        }
    }
}
