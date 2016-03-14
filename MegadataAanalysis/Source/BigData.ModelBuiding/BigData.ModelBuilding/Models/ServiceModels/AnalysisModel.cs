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
    }

    public partial class AnalysisModelFieldsInfo
    {
        [Key]
        public Guid Code { get; set; }

        public Guid? ModelCode { get; set; }

        [Required]
        public Guid FieldCode { get; set; }

        [Required]
        [MaxLength(2000)]
        public string FieldValue { get; set; }

        [NotMapped]
        public BaseField BaseField { get; set; }
    }
}