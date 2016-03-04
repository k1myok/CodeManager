using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalData;

namespace HospitalMVC.Models
{
    public class Depart 
    {
        public string HospName { get; set; }
        public string DepartName { get; set; }
        public string DepartType { get; set; }
        public string DepartIntro { get; set; }
        public string Limited { get; set; }
        public string RegistryFee { get; set; }
        public string ClinicFee { get; set; }
        public string DepartSex { get; set; }
        public string ChildAge { get; set; }

    }
}
