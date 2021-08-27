namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vmchange : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CreateHelpTicketViewModels", "AdminResolution");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CreateHelpTicketViewModels", "AdminResolution", c => c.String());
        }
    }
}
