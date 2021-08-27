namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serviceadmindrop : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Services", "User_Id", "dbo.Admins");
            DropIndex("dbo.Services", new[] { "User_Id" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Services", "User_Id");
            AddForeignKey("dbo.Services", "User_Id", "dbo.Admins", "User_Id", cascadeDelete: true);
        }
    }
}
