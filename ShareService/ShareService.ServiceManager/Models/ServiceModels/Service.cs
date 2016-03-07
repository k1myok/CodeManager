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
        public string Decription { get; set; }

        [Required]
        public Guid Directory { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [MaxLength(100)]
        public string CreateUser { get; set; }

        [Range(0,1)]
        public int State { get; set; }

        public Guid FarmCode { get; set; }

        [NotMapped]
        public List<ServiceMetadata> MetadataDetails { get; set; }
    }
}
