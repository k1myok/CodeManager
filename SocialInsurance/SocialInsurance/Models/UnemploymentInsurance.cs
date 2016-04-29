using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialInsurance.Models
{
    public class UnemploymentInsurance
    {
        public string status { get; set; }
        public string cpage { get; set; }
        public string pages { get; set; }
        public string rowcount { get; set; }
        public List<UnemploymentInsuranceDetail> data { get; set; }
    }
}