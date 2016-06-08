using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareService.ServiceManager.Models
{
    public partial class ServiceMetadata
    {
        [Key]
        public Guid Code { get; set; }

        public Guid? ServiceCode { get; set; }

        [Required]
        public Guid MetadataCode { get; set; }

        [Required]
        [MaxLength(2000)]
        public string MetadataValue { get; set; }

        [NotMapped]
        public BaseMetadata BaseMetadata { get; set; }
    }
}
