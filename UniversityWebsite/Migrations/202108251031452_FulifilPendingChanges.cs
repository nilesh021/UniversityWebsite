namespace UniversityWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FulifilPendingChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Services", "Start_Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Services", "Start_Date", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
