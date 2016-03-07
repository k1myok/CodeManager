using ShareService.ServiceManager.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShareService.ServiceManager.Models
{
    public partial class ServerInstance
    {
                

        [Key]
        public Guid Code { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name ="主机")]
        //[RegularExpression(@" ^ http://（[/w-]+/.）+[/w-]+（/[/w-./？％&=]*）？￥")]
        public string Address { get; set; }

        [Required]
        [Range(0,65535)]
        [Display(Name = "端口")]
        public int HttpPort { get; set; }

        [Display(Name = "启用")]
        public bool Enable { get; set; }
        public Guid FarmCode { get; set; }       
    }
}
