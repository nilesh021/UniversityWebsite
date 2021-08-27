namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableHelp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Helps",
                c => new
                    {
                        HelpId = c.Guid(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Issue = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        DOT = c.DateTime(nullable: false),
                        AdminResolution = c.String(),
                    })
                .PrimaryKey(t => t.HelpId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Helps");
        }
    }
}
