using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareService.ServiceManager.Models
{
    public class UFUser
    {
        [Key]
        public Guid Code { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }

    public class UFRole
    {
        [Key]
        public Guid Code { get; set; }

        public string Name { get; set; }
    }

    public class UFGroup
    {
        [Key]
        public Guid Code { get; set; }

        public string Name { get; set; }
    }

    public class UFUserInRole {
        [Key, Column(Order = 0)]
        public Guid UserCode { get; set; }

        [Key, Column(Order = 1)]
        public Guid RoleCode { get; set; }
    }

    public class UFServicesOfUser {
        [Key, Column(Order = 0)]
        public Guid UserCode { get; set; }

        [Key, Column(Order = 1)]
        public Guid ServiceCode { get; set; }
    }

    public class UFUserInGroup
    {
        [Key, Column(Order = 0)]
        public Guid UserCode { get; set; }

        [Key, Column(Order = 1)]
        public Guid GroupCode { get; set; }
    }

    public class UFServicesOfGroup
    {
        [Key, Column(Order = 0)]
        public Guid GroupCode { get; set; }

        [Key, Column(Order = 1)]
        public Guid ServiceCode { get; set; }
    }

    public class UFGroupInRole
    {
        [Key, Column(Order = 0)]
        public Guid GroupCode { get; set; }

        [Key, Column(Order = 1)]
        public Guid RoleCode { get; set; }
    }

    public class UFServicesOfRole
    {
        [Key, Column(Order = 0)]
        public Guid RoleCode { get; set; }

        [Key, Column(Order = 1)]
        public Guid ServiceCode { get; set; }
    }

}
