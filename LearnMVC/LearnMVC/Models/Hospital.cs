using System;

using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnMVC.Models
{
    public class Hospital
    {
         [Required]
        
        public string HospName { get; set; }
        public string Kind { get; set; }
        public string Location { get; set; }
        public string Tel { get; set; }
    }
}