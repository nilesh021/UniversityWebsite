namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableVolunteeAndService : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Service_Id = c.Int(nullable: false),
                        Admin_Id = c.Int(nullable: false),
                        Service_Name = c.String(nullable: false, maxLength: 20),
                        Service_Description = c.String(nullable: false, maxLength: 100),
                        Reqired_Volunteer = c.Int(nullable: false),
                        Start_Date = c.DateTime(nullable: false, storeType: "date"),
                        Participated_Volunteer = c.Int(),
                        Admin_User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Service_Id)
                .ForeignKey("dbo.Admins", t => t.Admin_User_Id)
                .Index(t => t.Admin_User_Id);
            
            CreateTable(
                "dbo.Volunteers",
                c => new
                    {
                        Volunteer_Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(nullable: false),
                        Service_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Volunteer_Id)
                .ForeignKey("dbo.Services", t => t.Service_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Service_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Volunteers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Volunteers", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.Services", "Admin_User_Id", "dbo.Admins");
            DropIndex("dbo.Volunteers", new[] { "Service_Id" });
            DropIndex("dbo.Volunteers", new[] { "User_Id" });
            DropIndex("dbo.Services", new[] { "Admin_User_Id" });
            DropTable("dbo.Volunteers");
            DropTable("dbo.Services");
        }
    }
}
