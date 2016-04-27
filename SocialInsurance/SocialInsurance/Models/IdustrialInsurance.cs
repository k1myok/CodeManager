using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialInsurance.Models
{
    public class IdustrialInsurance
    {
        public string status { get; set; }
        public string pages { get; set; }
        public string cpages { get; set; }
        public string rowcount { get; set; }
        public List<IdustrialInsuranceDetail> data { get; set; }
    }
}