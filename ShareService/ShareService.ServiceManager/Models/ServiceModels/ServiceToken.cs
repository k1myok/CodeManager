using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareService.ServiceManager.Models
{
    public partial class ServiceToken
    {
        [Key]
        public Guid Code { get; set; }

        [Required]
        [Display(Name = "服务资源")]
        public Guid ServiceCode { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Token")]
        public string Token { get; set; }

        [Required]
        [Display(Name = "开始日期")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "过期日期")]
        public DateTime ExpiredDate { get; set; }

        [Display(Name = "是否停用")]
        public bool IsPaused { get; set; }
    }
}
