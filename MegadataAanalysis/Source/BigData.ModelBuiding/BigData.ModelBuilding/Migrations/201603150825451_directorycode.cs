namespace BigData.ModelBuilding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class directorycode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnalysisModels", "DirectoryCode", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnalysisModels", "DirectoryCode");
        }
    }
}
