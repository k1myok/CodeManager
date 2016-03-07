namespace ShareService.ServiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServerInstances", "FarmCode", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServerInstances", "FarmCode");
        }
    }
}
