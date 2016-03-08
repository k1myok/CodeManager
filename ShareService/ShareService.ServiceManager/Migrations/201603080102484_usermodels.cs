namespace ShareService.ServiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usermodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UFGroups",
                c => new
                    {
                        Code = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.UFGroupInRoles",
                c => new
                    {
                        GroupCode = c.Guid(nullable: false),
                        RoleCode = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupCode, t.RoleCode });
            
            CreateTable(
                "dbo.UFRoles",
                c => new
                    {
                        Code = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.UFServicesOfGroups",
                c => new
                    {
                        GroupCode = c.Guid(nullable: false),
                        ServiceCode = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupCode, t.ServiceCode });
            
            CreateTable(
                "dbo.UFServicesOfRoles",
                c => new
                    {
                        RoleCode = c.Guid(nullable: false),
                        ServiceCode = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleCode, t.ServiceCode });
            
            CreateTable(
                "dbo.UFServicesOfUsers",
                c => new
                    {
                        UserCode = c.Guid(nullable: false),
                        ServiceCode = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserCode, t.ServiceCode });
            
            CreateTable(
                "dbo.UFUsers",
                c => new
                    {
                        Code = c.Guid(nullable: false),
                        Name = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.UFUserInGroups",
                c => new
                    {
                        UserCode = c.Guid(nullable: false),
                        GroupCode = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserCode, t.GroupCode });
            
            CreateTable(
                "dbo.UFUserInRoles",
                c => new
                    {
                        UserCode = c.Guid(nullable: false),
                        RoleCode = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserCode, t.RoleCode });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UFUserInRoles");
            DropTable("dbo.UFUserInGroups");
            DropTable("dbo.UFUsers");
            DropTable("dbo.UFServicesOfUsers");
            DropTable("dbo.UFServicesOfRoles");
            DropTable("dbo.UFServicesOfGroups");
            DropTable("dbo.UFRoles");
            DropTable("dbo.UFGroupInRoles");
            DropTable("dbo.UFGroups");
        }
    }
}
