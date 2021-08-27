namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class helpmodeluseridadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreateHelpTicketViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Issue = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        AdminResolution = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Helps", "User_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Helps", "User_Id");
            DropTable("dbo.CreateHelpTicketViewModels");
        }
    }
}
