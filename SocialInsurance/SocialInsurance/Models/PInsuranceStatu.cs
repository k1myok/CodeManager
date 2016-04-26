using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialInsurance.Models;

namespace SocialInsurance.Controllers
{
    public class PInsuranceStatu
    {
        public string status { get; set; }
        public string pages { get; set; }
        public string cpages { get; set; }
        public string rowcount { get; set; }
        public List<PInsuranceStatuDetail> data{get;set;}

    }
}
