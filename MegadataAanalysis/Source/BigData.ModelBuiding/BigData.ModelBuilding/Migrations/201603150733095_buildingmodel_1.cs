namespace BigData.ModelBuilding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class buildingmodel_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuildingModels",
                c => new
                    {
                        Code = c.Guid(nullable: false),
                        ModelCode = c.Guid(),
                        Name = c.String(maxLength: 100),
                        Description = c.String(maxLength: 100),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        SourceName = c.String(maxLength: 100),
                        SourceType = c.Int(nullable: false),
                        OutputName = c.String(maxLength: 100),
                        OutputType = c.Int(nullable: false),
                        WhereCaluse = c.String(maxLength: 1000),
                        OrderFields = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BuildingModels");
        }
    }
}
