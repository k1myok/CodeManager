namespace ShareService.ServiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class groupparent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UFGroups", "ParentCode", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UFGroups", "ParentCode");
        }
    }
}
