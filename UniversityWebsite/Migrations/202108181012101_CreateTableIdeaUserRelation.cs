namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableIdeaUserRelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Idea_User_Relation",
                c => new
                    {
                        Idea_Relation_Id = c.Int(nullable: false, identity: true),
                        Idea_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                        Vote = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Idea_Relation_Id)
                .ForeignKey("dbo.Ideas", t => t.Idea_Id, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: false)
                .Index(t => t.Idea_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Idea_User_Relation", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Idea_User_Relation", "Idea_Id", "dbo.Ideas");
            DropIndex("dbo.Idea_User_Relation", new[] { "User_Id" });
            DropIndex("dbo.Idea_User_Relation", new[] { "Idea_Id" });
            DropTable("dbo.Idea_User_Relation");
        }
    }
}
