using BigData.ModelBuilding.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace BigData.ModelBuilding.DAL
{
    public class ModelBuildingContext : DbContext
    {
        public ModelBuildingContext() : base("ModelBuildingDB")
        {
        }

        public DbSet<BigData.ModelBuilding.Models.AnalysisModel> AnalysisModel { get; set; }
        public DbSet<BigData.ModelBuilding.Models.AnalysisModelFieldsInfo> AnalysisModelFieldsInfo { get; set; }
        public DbSet<BigData.ModelBuilding.Models.AnalysisModelDirectory> AnalysisModelDirectory { get; set; }
        public DbSet<BigData.ModelBuilding.Models.BaseField> BaseField { get; set; }
        public DbSet<BigData.ModelBuilding.Models.BuildingModel> BuildingModel { get; set; }
        public DbSet<BigData.ModelBuilding.Models.BuildingModelDirectory> BuildingModelDirectory { get; set; }
    }
}
