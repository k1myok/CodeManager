using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialInsurance.Models;

namespace SocialInsurance.Models
{
    public class PInsuranceStatu
    {
        public string status { get; set; }
        public long  pages { get; set; }
        public long  cpages { get; set; }
        public string rowcount { get; set; }
        public List<PInsuranceStatuDetail> data{get;set;}

    }
}
