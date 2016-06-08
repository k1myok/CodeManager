namespace BigData.ModelBuilding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orgindb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnalysisModels",
                c => new
                    {
                        Code = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 300),
                        OutTemplateFilePath = c.String(nullable: false, maxLength: 300),
                        Description = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.AnalysisModelDirectories",
                c => new
                    {
                        Code = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 300),
                        Parent = c.Guid(),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.AnalysisModelFieldsInfoes",
                c => new
                    {
                        Code = c.Guid(nullable: false),
                        ModelCode = c.Guid(),
                        FieldCode = c.Guid(nullable: false),
                        FieldValue = c.String(nullable: false, maxLength: 2000),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.BaseFields",
                c => new
                    {
                        Code = c.Guid(nullable: false),
                        FieldName = c.String(nullable: false, maxLength: 300),
                        FieldType = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BaseFields");
            DropTable("dbo.AnalysisModelFieldsInfoes");
            DropTable("dbo.AnalysisModelDirectories");
            DropTable("dbo.AnalysisModels");
        }
    }
}
