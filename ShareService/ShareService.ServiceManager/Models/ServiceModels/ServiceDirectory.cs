using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareService.ServiceManager.Models
{
    public partial class ServiceDirectory
    {
        [Key]
        public Guid Code { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "名称")]
        public string Name { get; set; }

        public Guid? Parent { get; set; }

        [NotMapped]
        public List<ServiceDirectory> Children { get; set; }
    }
}
