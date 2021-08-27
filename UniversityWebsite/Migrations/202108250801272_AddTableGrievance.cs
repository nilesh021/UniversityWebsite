namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableGrievance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grievances",
                c => new
                    {
                        Grievance_Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                        User_Id = c.Int(nullable: false),
                        Topic = c.String(nullable: false, maxLength: 150),
                        Sub_Topic = c.String(nullable: false, maxLength: 150),
                        Details = c.String(nullable: false, maxLength: 300),
                        Expected_Resolution_Date = c.DateTime(nullable: false, storeType: "date"),
                        Status = c.String(nullable: false, maxLength: 50),
                        Admin_Comment = c.String(maxLength: 150),
                        Created_On = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Grievance_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Grievances");
        }
    }
}
