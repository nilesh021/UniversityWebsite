namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValueToIdeaCategory : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO IdeaCategories (Name) VALUES ('Event')");
            Sql("INSERT INTO IdeaCategories (Name) VALUES ('Activity')");
            Sql("INSERT INTO IdeaCategories (Name) VALUES ('Competitions')");
            Sql("INSERT INTO IdeaCategories (Name) VALUES ('Trip')");
            Sql("INSERT INTO IdeaCategories (Name) VALUES ('Others')");
        }
        
        public override void Down()
        {
        }
    }
}
