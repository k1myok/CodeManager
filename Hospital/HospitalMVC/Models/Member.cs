using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalMVC.Models
{
    public class Member 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string IDCard { get; set; }
        public string Phone { get; set; }
        public string Style { get; set; }
        public string openID { get; set; }

    }
}
