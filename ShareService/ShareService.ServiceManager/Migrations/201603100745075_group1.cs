namespace ShareService.ServiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class group1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UFGroups", "Parent", c => c.Guid());
            DropColumn("dbo.UFGroups", "ParentCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UFGroups", "ParentCode", c => c.Guid(nullable: false));
            DropColumn("dbo.UFGroups", "Parent");
        }
    }
}
