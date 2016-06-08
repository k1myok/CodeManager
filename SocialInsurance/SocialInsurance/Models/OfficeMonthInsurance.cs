using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialInsurance.Models
{
    public class OfficeMonthInsurance
    {
        public string status { get; set; }
        public long pages { get; set; }
        public long cpage { get; set; }
        public string rowcount { get; set; }
        public List<OfficeMonthInsuranceDetail> data { get; set; }
    }
}