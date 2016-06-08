using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SelectProvidentFundService
{
    public class Encryption
    {
        /// <summary>   
        /// MD5加密   
        /// </summary>   
        /// <param name="str"></param>   
        /// <returns></returns>   
        public static string Md5(string str)
        { 
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.GetEncoding("UTF-8").GetBytes(str)); 
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2").ToLower());
            }
            return sBuilder.ToString();
        }
    }
}
