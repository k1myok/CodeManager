using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigData.ModelBuilding.Models
{
    public partial class AnalysisModelDirectory
    {
        [Key]
        public Guid Code { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "名称")]
        public string Name { get; set; }

        public Guid? Parent { get; set; }

        [NotMapped]
        public List<AnalysisModelDirectory> Children { get; set; }
    }

    public partial class BuildingModelDirectory
    {
        [Key]
        public Guid Code { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "名称")]
        public string Name { get; set; }

        public Guid? Parent { get; set; }

        [NotMapped]
        public List<BuildingModelDirectory> Children { get; set; }
    }

}
