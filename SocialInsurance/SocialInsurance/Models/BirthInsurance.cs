using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialInsurance.Models
{
  public  class BirthInsurance
    {
      public string status { get; set; }
      public string cpage  { get; set; }
      public string pages { get; set; }
      public string rowcount { get; set; }
      public List<BirthInsuranceDetail> data   { get; set; }



    }
}
