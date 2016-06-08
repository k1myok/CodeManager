using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialInsurance.Models
{
    //查询条件
    public class condition
    {   
        [MaxLength(10)]
        [Display(Name="社保编号")]
        [Required]
        public string ID {get;set;}
        [MaxLength(18)]
        [Display(Name = "身份证号")]
        [Required]
        public string IDCard { get; set;}
    }
}