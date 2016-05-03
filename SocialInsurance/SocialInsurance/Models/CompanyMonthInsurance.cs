using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialInsurance.Models
{
    public class CompanyMonthInsurance
    {
        public string status { get; set; }
        public long  cpage { get; set; }
        public long pages { get; set; }
        public string rowcount { get; set; }
        public List<CompanyMonthInsuranceDetail> data { get; set; }


    }
}
