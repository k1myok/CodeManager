namespace BigData.ModelBuilding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelfieldsinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnalysisModelFieldsInfoes", "SourceName", c => c.String(maxLength: 100));
            AddColumn("dbo.AnalysisModelFieldsInfoes", "SourceType", c => c.Int(nullable: false));
            AddColumn("dbo.AnalysisModelFieldsInfoes", "OutputName", c => c.String(maxLength: 100));
            AddColumn("dbo.AnalysisModelFieldsInfoes", "OutputType", c => c.Int(nullable: false));
            AddColumn("dbo.AnalysisModelFieldsInfoes", "WhereCaluse", c => c.String(maxLength: 1000));
            AddColumn("dbo.AnalysisModelFieldsInfoes", "OrderFields", c => c.String(maxLength: 1000));
            DropColumn("dbo.AnalysisModelFieldsInfoes", "FieldValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AnalysisModelFieldsInfoes", "FieldValue", c => c.String(nullable: false, maxLength: 2000));
            DropColumn("dbo.AnalysisModelFieldsInfoes", "OrderFields");
            DropColumn("dbo.AnalysisModelFieldsInfoes", "WhereCaluse");
            DropColumn("dbo.AnalysisModelFieldsInfoes", "OutputType");
            DropColumn("dbo.AnalysisModelFieldsInfoes", "OutputName");
            DropColumn("dbo.AnalysisModelFieldsInfoes", "SourceType");
            DropColumn("dbo.AnalysisModelFieldsInfoes", "SourceName");
        }
    }
}
