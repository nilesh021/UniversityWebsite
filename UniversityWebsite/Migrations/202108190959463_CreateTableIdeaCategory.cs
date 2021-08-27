namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableIdeaCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IdeaCategories",
                c => new
                    {
                        Category_Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Category_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IdeaCategories");
        }
    }
}
