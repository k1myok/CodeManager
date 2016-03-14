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

        public string HospName { get; set; }
        public string Grade { get; set; }
        public string Kind { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Intro { get; set; }
    }
}