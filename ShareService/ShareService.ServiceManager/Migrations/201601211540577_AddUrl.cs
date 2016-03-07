namespace ShareService.ServiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServerFarms",
                c => new
                    {
                        Code = c.Guid(nullable: false),
                        Name = c.String(maxLength: 300),
                        Enable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.ServerInstances",
                c => new
                    {
                        Code = c.Guid(nullable: false),
                        Address = c.String(maxLength: 50),
                        HttpPort = c.Int(nullable: false),
                        Enable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Code = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                        URL = c.String(maxLength: 300),
                        Decription = c.String(maxLength: 2000),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        Company = c.String(maxLength: 300),
                        Department = c.String(maxLength: 300),
                        CreateUser = c.String(maxLength: 100),
                        Telphone = c.String(maxLength: 50),
                        State = c.Int(nullable: false),
                        FarmCode = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Services");
            DropTable("dbo.ServerInstances");
            DropTable("dbo.ServerFarms");
        }
    }
}
