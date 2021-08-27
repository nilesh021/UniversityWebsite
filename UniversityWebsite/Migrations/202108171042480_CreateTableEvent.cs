namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Event_Id = c.Int(nullable: false, identity: true),
                        Event_Name = c.String(nullable: false),
                        Start_Date = c.DateTime(nullable: false),
                        End_Date = c.DateTime(nullable: false),
                        Category = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Event_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Events");
        }
    }
}
