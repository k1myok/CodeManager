namespace BigData.ModelBuilding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class buildingmodeldirectory1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuildingModelDirectories",
                c => new
                    {
                        Code = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 300),
                        Parent = c.Guid(),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BuildingModelDirectories");
        }
    }
}
