using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HospitalMVC.Models
{
    public class Register
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string IDCard { get; set; }
        public string OPenId { get; set; }
        public string HospName { get; set; }
        public string DepartName { get; set; }
        public string DoctorName { get; set; }
        public string Phone { get; set; }
        public System.DateTime RegisteDate { get; set; }
        public System.DateTime ClinicDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string RegisterFee { get; set; }
        public string SN { get; set; }
        public string Status { get; set; }
        public string DocRate { get; set; }
        public string Style { get; set; }
        public string comment { get; set; }
        public string ClinicSerialNo { get; set; }
    }
}