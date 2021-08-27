namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableIdea : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ideas",
                c => new
                    {
                        Idea_Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(nullable: false),
                        Idea_Content = c.String(nullable: false),
                        Club_Id = c.Int(nullable: false),
                        Category = c.String(nullable: false),
                        Votes = c.Int(nullable: false),
                        Posted_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Idea_Id)
                .ForeignKey("dbo.Clubs", t => t.Club_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Club_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ideas", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Ideas", "Club_Id", "dbo.Clubs");
            DropIndex("dbo.Ideas", new[] { "Club_Id" });
            DropIndex("dbo.Ideas", new[] { "User_Id" });
            DropTable("dbo.Ideas");
        }
    }
}
