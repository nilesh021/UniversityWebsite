namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceTableRenameColumnToUserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Services", "Admin_User_Id", "dbo.Admins");
            DropIndex("dbo.Services", new[] { "Admin_User_Id" });
            RenameColumn(table: "dbo.Services", name: "Admin_User_Id", newName: "User_Id");
            AlterColumn("dbo.Services", "User_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Services", "User_Id");
            AddForeignKey("dbo.Services", "User_Id", "dbo.Admins", "User_Id", cascadeDelete: true);
            DropColumn("dbo.Services", "Admin_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Services", "Admin_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Services", "User_Id", "dbo.Admins");
            DropIndex("dbo.Services", new[] { "User_Id" });
            AlterColumn("dbo.Services", "User_Id", c => c.Int());
            RenameColumn(table: "dbo.Services", name: "User_Id", newName: "Admin_User_Id");
            CreateIndex("dbo.Services", "Admin_User_Id");
            AddForeignKey("dbo.Services", "Admin_User_Id", "dbo.Admins", "User_Id");
        }
    }
}
