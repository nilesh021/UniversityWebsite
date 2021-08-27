namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        User_Id = c.Int(nullable: false, identity: true),
                        First_Name = c.String(nullable: false),
                        Last_Name = c.String(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        ContactNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        A1 = c.String(nullable: false),
                        A2 = c.String(nullable: false),
                        A3 = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
