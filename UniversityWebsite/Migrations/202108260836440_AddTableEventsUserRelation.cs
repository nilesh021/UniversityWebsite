namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableEventsUserRelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events_User_Relation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Event_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                        Like_Dislike = c.Int(),
                        Interest = c.Int(),
                        Comment = c.String(),
                        Attendance = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .Index(t => t.Event_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events_User_Relation", "Event_Id", "dbo.Events");
            DropIndex("dbo.Events_User_Relation", new[] { "Event_Id" });
            DropTable("dbo.Events_User_Relation");
        }
    }
}
