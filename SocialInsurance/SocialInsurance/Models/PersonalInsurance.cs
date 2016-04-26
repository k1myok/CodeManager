using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SocialInsurance.Models
{
    //个人参保情况情况查询
    public class PersonalInsurance
    {
        public string status { get; set; }
        public string pages { get; set; }
        public string cpage { get; set; }
        public string rowcount { get; set; }
        public List<InsuranceDetail>msg {get;set;}
    }
}
