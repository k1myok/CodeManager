using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [Display(Name = "服务资源")]
        public Guid? ServiceCode { get; set; }

        [Required]
        [Display(Name = "用户名")]
        public Guid UserCode { get; set; }

        [Required]
        [Display(Name = "开始日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "过期日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpiredDate { get; set; }

        [Display(Name = "是否停用")]
        public bool IsPaused { get; set; }

        [Display(Name = "单一服务")]
        public bool SingleService { get; set; }

    }
}
