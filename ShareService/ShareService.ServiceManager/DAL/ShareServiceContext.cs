using ShareService.ServiceManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace ShareService.ServiceManager.DAL
{
    public class ShareServiceContext : DbContext
    {
        public ShareServiceContext() : base("ShareServiceDB")
        {
        }

        public DbSet<ShareService.ServiceManager.Models.Service> Service { get; set; }
        public DbSet<ShareService.ServiceManager.Models.ServiceDirectory> ServiceDirectory { get; set; }
        public DbSet<ShareService.ServiceManager.Models.BaseMetadata> BaseMetadata { get; set; }
        public DbSet<ShareService.ServiceManager.Models.ServiceMetadata> ServiceMetadata { get; set; }

        public DbSet<ShareService.ServiceManager.Models.ServerFarm> ServerFarm { get; set; }

        public DbSet<ShareService.ServiceManager.Models.ServerInstance> ServerInstance { get; set; }
        public DbSet<ShareService.ServiceManager.Models.Groups> Groups { get; set; }
        public DbSet<ShareService.ServiceManager.Models.RolesInGroups> RoleInGroups { get; set; }

        public DbSet<ShareService.ServiceManager.Models.UFUser> UFUser { get; set; }
        public DbSet<ShareService.ServiceManager.Models.UFRole> UFRole { get; set; }
        public DbSet<ShareService.ServiceManager.Models.UFGroup> UFGroup { get; set; }
        public DbSet<ShareService.ServiceManager.Models.UFUserInRole> UFUserInRole { get; set; }
        public DbSet<ShareService.ServiceManager.Models.UFServicesOfUser> UFServicesOfUser { get; set; }
        public DbSet<ShareService.ServiceManager.Models.UFUserInGroup> UFUserInGroup { get; set; }
        public DbSet<ShareService.ServiceManager.Models.UFServicesOfGroup> UFServicesOfGroup { get; set; }
        public DbSet<ShareService.ServiceManager.Models.UFGroupInRole> UFGroupInRole { get; set; }
        public DbSet<ShareService.ServiceManager.Models.UFServicesOfRole> UFServicesOfRole { get; set; }

        [NotMapped]
        public DbSet<AspNetUsers> AspNetUsers { get; set; }
        [NotMapped]
        public DbSet<AspNetRoles> AspNetRoles { get; set; }
        [NotMapped]
        public DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
    }
}
