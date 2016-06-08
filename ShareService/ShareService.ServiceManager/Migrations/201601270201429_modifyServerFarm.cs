namespace ShareService.ServiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyServerFarm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServerInstances", "BelongedFarmName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServerInstances", "BelongedFarmName");
        }
    }
}
