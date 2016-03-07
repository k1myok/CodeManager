namespace ShareService.ServiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoleGroups",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        GroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.GroupID });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AspNetRoleGroups");
            DropTable("dbo.AspNetGroups");
        }
    }
}
