namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableShare : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shares",
                c => new
                    {
                        Share_Id = c.Int(nullable: false, identity: true),
                        Shared_To_Id = c.Int(nullable: false),
                        Shared_Type = c.String(),
                        Shared_Type_Id = c.Int(nullable: false),
                        Shared_By_Id = c.Int(nullable: false),
                        Alert = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Share_Id)
                .ForeignKey("dbo.Users", t => t.Shared_To_Id, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.Shared_By_Id, cascadeDelete: false)
                .Index(t => t.Shared_To_Id)
                .Index(t => t.Shared_By_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shares", "Shared_By_Id", "dbo.Users");
            DropForeignKey("dbo.Shares", "Shared_To_Id", "dbo.Users");
            DropIndex("dbo.Shares", new[] { "Shared_By_Id" });
            DropIndex("dbo.Shares", new[] { "Shared_To_Id" });
            DropTable("dbo.Shares");
        }
    }
}
