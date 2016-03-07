namespace ShareService.ServiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShareServiceContext : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ServerFarms", "Name", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ServerFarms", "Name", c => c.String(maxLength: 300));
        }
    }
}
