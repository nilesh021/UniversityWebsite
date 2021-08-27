namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdeaCategoryToIdeaClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ideas", "IdeaCategory_Category_Id", c => c.Int());
            CreateIndex("dbo.Ideas", "IdeaCategory_Category_Id");
            AddForeignKey("dbo.Ideas", "IdeaCategory_Category_Id", "dbo.IdeaCategories", "Category_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ideas", "IdeaCategory_Category_Id", "dbo.IdeaCategories");
            DropIndex("dbo.Ideas", new[] { "IdeaCategory_Category_Id" });
            DropColumn("dbo.Ideas", "IdeaCategory_Category_Id");
        }
    }
}
