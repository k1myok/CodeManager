namespace ShareService.ServiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRequiedFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ServerInstances", "Address", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Services", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Services", "URL", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Services", "URL", c => c.String(maxLength: 300));
            AlterColumn("dbo.Services", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.ServerInstances", "Address", c => c.String(maxLength: 50));
        }
    }
}
