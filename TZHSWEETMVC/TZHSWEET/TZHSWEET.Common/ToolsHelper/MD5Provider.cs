using System;
using System.Text;
using System.Security.Cryptography;

namespace TZHSWEET.Common
{
    /// <summary>
    /// MD5Provider 的摘要说明
    /// </summary>
    public class MD5Provider
    {
        private MD5Provider()
        {
        }
        /// <summary>
        /// 计算指定字符串的MD5哈希值
        /// </summary>
        /// <param name="message">要进行哈希计算的字符串</param>
        /// <returns></returns>
        public static string Hash(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return string.Empty;
            }
            else
            {
                MD5 md5 = MD5.Create();
                byte[] source = Encoding.UTF8.GetBytes(message);
                byte[] result = md5.ComputeHash(source);
                StringBuilder buffer = new StringBuilder(result.Length);

                for (int i = 0; i < result.Length; i++)
                {
                    buffer.Append(result[i].ToString("x"));//将byte值转换成十六进制字符串
                }
                return buffer.ToString() ;
            }

        }
    }
}