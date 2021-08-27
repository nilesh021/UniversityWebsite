namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableClubUserRelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Club_User_Relation",
                c => new
                    {
                        Relation_Id = c.Int(nullable: false, identity: true),
                        Club_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                        Designation = c.String(nullable: false),
                        Joined_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Relation_Id)
                .ForeignKey("dbo.Clubs", t => t.Club_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Club_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Club_User_Relation", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Club_User_Relation", "Club_Id", "dbo.Clubs");
            DropIndex("dbo.Club_User_Relation", new[] { "User_Id" });
            DropIndex("dbo.Club_User_Relation", new[] { "Club_Id" });
            DropTable("dbo.Club_User_Relation");
        }
    }
}
