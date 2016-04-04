using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareService.Service.ARR
{
    public class ARRToken
    {
        static string tokenKey = "B@aJ94&>";

        public string UserCode { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpiredDate { get; set; }

        public string ServiceCode { get; set; }

        public string ToToken()
        {
            var token = string.Format("{0},{1},{2},{3}", 
                this.UserCode, 
                this.StartDate, 
                this.ExpiredDate, 
                this.ServiceCode);
            return token.MD5Encrypt(tokenKey);
        }

        public static ARRToken Parse(string token)
        {
            var factToken = token.MD5Decrypt(tokenKey);
            var args = factToken.Split(',');
            return new ARRToken()
            {
                UserCode = args[0],
                StartDate = DateTime.Parse(args[1]),
                ExpiredDate = DateTime.Parse(args[2]),
                ServiceCode = args[3]
            };
        }
    }
}
