namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelationToIdeaCategoryInIdea : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ideas", "IdeaCategory_Category_Id", "dbo.IdeaCategories");
            DropIndex("dbo.Ideas", new[] { "IdeaCategory_Category_Id" });
            RenameColumn(table: "dbo.Ideas", name: "IdeaCategory_Category_Id", newName: "Category_Id");
            AlterColumn("dbo.Ideas", "Category_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Ideas", "Category_Id");
            AddForeignKey("dbo.Ideas", "Category_Id", "dbo.IdeaCategories", "Category_Id", cascadeDelete: true);
            DropColumn("dbo.Ideas", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ideas", "Category", c => c.String(nullable: false));
            DropForeignKey("dbo.Ideas", "Category_Id", "dbo.IdeaCategories");
            DropIndex("dbo.Ideas", new[] { "Category_Id" });
            AlterColumn("dbo.Ideas", "Category_Id", c => c.Int());
            RenameColumn(table: "dbo.Ideas", name: "Category_Id", newName: "IdeaCategory_Category_Id");
            CreateIndex("dbo.Ideas", "IdeaCategory_Category_Id");
            AddForeignKey("dbo.Ideas", "IdeaCategory_Category_Id", "dbo.IdeaCategories", "Category_Id");
        }
    }
}
