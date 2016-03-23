using BigData.ModelBuilding.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigData.ModelBuilding.Models
{
    public partial class AnalysisModel
    {
        [Key]
        public Guid Code { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name ="名称")]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "输出模板")]
        public string OutTemplateFilePath { get; set; }

        [MaxLength(2000)]
        [Display(Name = "模型概要")]
        public string Description { get; set; }

        public Guid DirectoryCode { get; set; }
    }

    public partial class BuildingModel
    {
        [Key]
        public Guid Code { get; set; }

        [Required]
        public Guid DirectoryCode { get; set; }

        public Guid? ModelCode { get; set; }

        [MaxLength(100)]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [MaxLength(100)]
        [Display(Name = "描述")]
        public string Description { get; set; }

        [Display(Name = "创建日期")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "更新日期")]
        public DateTime UpdateDate { get; set; }

        [MaxLength(100)]
        [Display(Name = "源名称")]
        public string SourceName { get; set; }

        [Display(Name = "源类型")]
        public int SourceType { get; set; }

        [MaxLength(100)]
        [Display(Name = "输出名称")]
        public string OutputName { get; set; }

        [Display(Name = "输出类型")]
        public int OutputType { get; set; }

        [MaxLength(1000)]
        [Display(Name = "筛选条件")]
        public string WhereCaluse { get; set; }

        [MaxLength(1000)]
        [Display(Name = "排序字段")]
        public string OrderFields { get; set; }
    }

    public partial class AnalysisModelFieldsInfo
    {
        [Key]
        public Guid Code { get; set; }

        public Guid? ModelCode { get; set; }

        [Required]
        public Guid FieldCode { get; set; }

        [NotMapped]
        public BaseField BaseField { get; set; }
    }
}