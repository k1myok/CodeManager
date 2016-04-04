namespace ShareService.ServiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceTokens", "UserCode", c => c.Guid(nullable: false));
            AddColumn("dbo.ServiceTokens", "SingleService", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ServiceTokens", "ServiceCode", c => c.Guid());
            DropColumn("dbo.ServiceTokens", "Token");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceTokens", "Token", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.ServiceTokens", "ServiceCode", c => c.Guid(nullable: false));
            DropColumn("dbo.ServiceTokens", "SingleService");
            DropColumn("dbo.ServiceTokens", "UserCode");
        }
    }
}
