namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableShareAddedColumnSharedItemTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shares", "Shared_Item_Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shares", "Shared_Item_Title");
        }
    }
}
