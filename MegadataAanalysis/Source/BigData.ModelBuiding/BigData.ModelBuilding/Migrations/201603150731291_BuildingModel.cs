namespace BigData.ModelBuilding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuildingModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AnalysisModelFieldsInfoes", "SourceName");
            DropColumn("dbo.AnalysisModelFieldsInfoes", "SourceType");
            DropColumn("dbo.AnalysisModelFieldsInfoes", "OutputName");
            DropColumn("dbo.AnalysisModelFieldsInfoes", "OutputType");
            DropColumn("dbo.AnalysisModelFieldsInfoes", "WhereCaluse");
            DropColumn("dbo.AnalysisModelFieldsInfoes", "OrderFields");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AnalysisModelFieldsInfoes", "OrderFields", c => c.String(maxLength: 1000));
            AddColumn("dbo.AnalysisModelFieldsInfoes", "WhereCaluse", c => c.String(maxLength: 1000));
            AddColumn("dbo.AnalysisModelFieldsInfoes", "OutputType", c => c.Int(nullable: false));
            AddColumn("dbo.AnalysisModelFieldsInfoes", "OutputName", c => c.String(maxLength: 100));
            AddColumn("dbo.AnalysisModelFieldsInfoes", "SourceType", c => c.Int(nullable: false));
            AddColumn("dbo.AnalysisModelFieldsInfoes", "SourceName", c => c.String(maxLength: 100));
        }
    }
}
