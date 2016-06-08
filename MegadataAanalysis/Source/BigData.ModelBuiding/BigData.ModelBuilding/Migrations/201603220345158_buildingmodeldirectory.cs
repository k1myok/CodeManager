namespace BigData.ModelBuilding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class buildingmodeldirectory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BuildingModels", "DirectoryCode", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BuildingModels", "DirectoryCode");
        }
    }
}
