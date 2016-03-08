using ShareService.ServiceManager.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareService.ServiceManager.Models
{
    public partial class Service
    {
        [Key]
        public Guid Code { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name ="名称")]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name ="地址")]
        public string URL { get; set; }

        [MaxLength(2000)]
        [Display(Name = "说明")]
        public string Decription { get; set; }

        [Required]
        public Guid Directory { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "更新时间")]
        public DateTime UpdateDate { get; set; }

        [MaxLength(100)]
        [Display(Name = "创建用户")]
        public string CreateUser { get; set; }

        [Range(0,1)]
        public int State { get; set; }

        [Display(Name = "集群")]
        public Guid FarmCode { get; set; }

        [NotMapped]
        public List<ServiceMetadata> MetadataDetails { get; set; }
    }
}
