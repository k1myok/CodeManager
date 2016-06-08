using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialInsurance.Models
{
    public class EmployeehealthInsurance
    {
        public string status { get; set; }
        public long cpage { get; set; }
        public long pages { get; set; }
        public string rowcount { get; set; }
        public List<EmployeehealthInsuranceDetail> data { get; set; }

    }
}