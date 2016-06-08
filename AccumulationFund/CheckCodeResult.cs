using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelectProvidentFundService
{
    public class CheckCodeResult
    {
        public bool Result { get; set; }

        public string Message { get; set; }

        public string CheckCode { get; set; }
    }
}