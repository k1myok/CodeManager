namespace ShareService.ServiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class servicetoken : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceTokens",
                c => new
                    {
                        Code = c.Guid(nullable: false),
                        ServiceCode = c.Guid(nullable: false),
                        Token = c.String(nullable: false, maxLength: 100),
                        StartDate = c.DateTime(nullable: false),
                        ExpiredDate = c.DateTime(nullable: false),
                        IsPaused = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ServiceTokens");
        }
    }
}
