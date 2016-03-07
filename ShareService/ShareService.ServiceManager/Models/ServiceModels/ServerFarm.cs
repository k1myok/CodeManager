using ShareService.ServiceManager.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShareService.ServiceManager.Models
{
    public partial class ServerFarm
    {
        [Key]
        public Guid Code { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name ="名称")]
        public string Name { get; set; }

        [Display(Name = "启用")]
        public bool Enable { get; set; }

        private static ShareServiceContext context = new ShareServiceContext();
        public IEnumerable<SelectListItem> FarmNamesSet
        {
            get
            {
                var farms = context.ServerFarm.ToList();
                foreach (var f in farms)
                {
                    yield return new SelectListItem { Text = f.Name, Value = f.Code.ToString() };
                }
            }
            private set { }
        }
    }
}