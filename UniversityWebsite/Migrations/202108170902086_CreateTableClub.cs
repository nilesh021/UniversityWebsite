namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableClub : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clubs",
                c => new
                    {
                        Club_Id = c.Int(nullable: false, identity: true),
                        Club_Name = c.String(nullable: false),
                        Department = c.String(nullable: false),
                        Max_Member = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Club_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Clubs");
        }
    }
}
