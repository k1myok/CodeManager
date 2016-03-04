using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalMVC.Models
{
    public class Doctor 
    {
        public int ID { get; set; }
        public string HospName { get; set; }
        public string DepartName { get; set; }
        public string DoctorName { get; set; }
        public string DocSex { get; set; }
        public string DocRank { get; set; }
        public string DoctorInro { get; set; }
        public string DocMajor { get; set; }
        public string IsExpert { get; set; }
        public string RegistryFee { get; set; }
        public string ClinicFee { get; set; }
        public string DocPhotoURL { get; set; }

    }
}
