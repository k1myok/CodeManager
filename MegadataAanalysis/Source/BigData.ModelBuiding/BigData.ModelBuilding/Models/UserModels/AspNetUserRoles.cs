

namespace BigData.ModelBuilding.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    public partial class AspNetUserRoles
    {
        [Key]
        [Column(Order =0)]
        public string UserId { get; set; }
        [Key]
        [Column(Order =1)]
        public int RoleId { get; set; }
    }
}