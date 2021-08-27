namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableNews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        News_Id = c.Int(nullable: false, identity: true),
                        News_Name = c.String(nullable: false),
                        Start_Date = c.DateTime(nullable: false),
                        End_Date = c.DateTime(nullable: false),
                        Category = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.News_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News");
        }
    }
}
