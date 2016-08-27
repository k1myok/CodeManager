using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace TZHSWEET.Common
{
    /// <summary>
    /// DES算法加密解密
    /// </summary>
    public class DESProvider
    {
        private DESProvider()
        {
        }
        //默认的初始化密钥
        private static string key = "netskycn";

        /// <summary>
        /// 对称加密解密的密钥
        /// </summary>
        public static string Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }
        #region 加密
        /// <summary>
        /// 采用DES算法对字符串加密
        /// </summary>
        /// <param name="encryptString">要加密的字符串</param>
        /// <param name="key">加密的密钥</param>
        /// <returns></returns>
        public static string EncryptString(string encryptString, string key)
        {
            //加密加密字符串是否为空
            if (string.IsNullOrEmpty(encryptString))
            {
                throw new ArgumentNullException("encryptString", "不能为空");
            }
            //加查密钥是否为空
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "不能为空");
            }
            //将密钥转换成字节数组
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            //设置初始化向量
            byte[] keyIV = keyBytes;
            //将加密字符串转换成UTF8编码的字节数组
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            //调用EncryptBytes方法加密
            byte[] resultByteArray = EncryptBytes(inputByteArray, keyBytes, keyIV);
            //将字节数组转换成字符串并返回
            return Convert.ToBase64String(resultByteArray);
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptString">要加密的字符串</param>
        /// <returns></returns>
        public static string EncryptString(string encryptString)
        {
            return EncryptString(encryptString, key);
        }
        /// <summary>
        /// 采用DES算法对字节数组加密
        /// </summary>
        /// <param name="sourceBytes">要加密的字节数组</param>
        /// <param name="keyBytes">算法的密钥，长度为8的倍数，最大长度64</param>
        /// <param name="keyIV">算法的初始化向量，长度为8的倍数，最大长度64</param>
        /// <returns></returns>
        public static byte[] EncryptBytes(byte[] sourceBytes, byte[] keyBytes, byte[] keyIV)
        {
            if (sourceBytes == null || keyBytes == null || keyIV == null)
            {
                throw new ArgumentNullException("sourceBytes和keyBytes", "不能为空。");
            }
            else
            {
                //检查密钥数组长度是否是8的倍数并且长度是否小于64
                keyBytes = CheckByteArrayLength(keyBytes);
                //检查初始化向量数组长度是否是8的倍数并且长度是否小于64
                keyIV = CheckByteArrayLength(keyIV);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                //实例化内存流MemoryStream
                MemoryStream mStream = new MemoryStream();
                //实例化CryptoStream
                CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                cStream.Write(sourceBytes, 0, sourceBytes.Length);
                cStream.FlushFinalBlock();
                //将内存流转换成字节数组
                byte[] buffer = mStream.ToArray();
                mStream.Close();//关闭流
                cStream.Close();//关闭流
                return buffer;
            }
        }
        #endregion
        #region 解密
        public static string DecryptString(string decryptString, string key)
        {
            if (string.IsNullOrEmpty(decryptString))
            {
                throw new ArgumentNullException("decryptString", "不能为空");
            }
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "不能为空");
            }
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyIV = keyBytes;
            //将解密字符串转换成Base64编码字节数组
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            //调用DecryptBytes方法解密
            byte[] resultByteArray = DecryptBytes(inputByteArray, keyBytes, keyIV);
            //将字节数组转换成UTF8编码的字符串
            return Encoding.UTF8.GetString(resultByteArray);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="decryptString">要解密的字符串</param>
        /// <returns></returns>
        public static string DecryptString(string decryptString)
        {
            return DecryptString(decryptString, key);
        }

        /// <summary>
        /// 采用DES算法对字节数组解密
        /// </summary>
        /// <param name="sourceBytes">要加密的字节数组</param>
        /// <param name="keyBytes">算法的密钥，长度为8的倍数，最大长度64</param>
        /// <param name="keyIV">算法的初始化向量，长度为8的倍数，最大长度64</param>
        /// <returns></returns>
        public static byte[] DecryptBytes(byte[] sourceBytes, byte[] keyBytes, byte[] keyIV)
        {
            if (sourceBytes == null || keyBytes == null || keyIV == null)
            {
                throw new ArgumentNullException("soureBytes和keyBytes及keyIV", "不能为空。");
            }
            else
            {
                //检查密钥数组长度是否是8的倍数并且长度是否小于64
                keyBytes = CheckByteArrayLength(keyBytes);
                //检查初始化向量数组长度是否是8的倍数并且长度是否小于64
                keyIV = CheckByteArrayLength(keyIV);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                cStream.Write(sourceBytes, 0, sourceBytes.Length);
                cStream.FlushFinalBlock();
                //将内存流转换成字节数组
                byte[] buffer = mStream.ToArray();
                mStream.Close();//关闭流
                cStream.Close();//关闭流
                return buffer;
            }
        }
        #endregion
        /// <summary>
        /// 检查密钥或初始化向量的长度，如果不是8的倍数或长度大于64则截取前8个元素
        /// </summary>
        /// <param name="byteArray">要检查的数组</param>
        /// <returns></returns>
        private static byte[] CheckByteArrayLength(byte[] byteArray)
        {
            byte[] resultBytes = new byte[8];
            //如果数组长度小于8
            if (byteArray.Length < 8)
            {
                return Encoding.UTF8.GetBytes("12345678");
            }
            //如果数组长度不是8的倍数
            else if (byteArray.Length % 8 != 0 || byteArray.Length > 64)
            {
                Array.Copy(byteArray, 0, resultBytes, 0, 8);
                return resultBytes;
            }
            else
            {
                return byteArray;
            }
        }
    }
}