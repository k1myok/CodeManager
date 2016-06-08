using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigData.ModelBuilding.Models
{
    public partial class BaseField
    {
        [Key]
        public Guid Code { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "名称")]
        public string FieldName { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "类型")]
        public string FieldType { get; set; }
    }
}
