namespace ShareService.ServiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyServices : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "BelongedFarmName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Services", "BelongedFarmName");
        }
    }
}
