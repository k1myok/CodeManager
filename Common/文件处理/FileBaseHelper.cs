// 源文件头信息：
// <copyright file="FileBaseHelper.cs">
// Copyright(c)2014-2034 Kencery.All rights reserved.
// 个人博客：http://www.cnblogs.com/hanyinglong
// 创建人：韩迎龙(kencery)
// 创建时间：2015/04/30
// </copyright>

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KenceryCommonMethod
{
    /// <summary>
    /// 文件操作基础类,（判断文件是否存在，检查目录是否存在...）
    /// <auther>
    ///     <name>Kencery</name>
    ///     <date>2015/04/30</date>
    /// </auther>
    /// 修改：合并文件加密解密共用类 2016-4-15  Kencery
    /// </summary>
    public static class FileBaseHelper
    {
        /// <summary>
        /// 检查某个文件是否真的存在
        /// </summary>
        /// <param name="path">需要检查的文件的路径(包括路径的文件全名)</param>
        /// <returns>返回true则表示存在，false为不存在</returns>
        public static bool IsFileExists(string path)
        {
            try
            {
                return File.Exists(path);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 检查文件目录是否真的存在
        /// </summary>
        /// <param name="path">需要检查的文件目录</param>
        /// <returns>返回true则表示存在，false为不存在</returns>
        public static bool IsDirectoryExists(string path)
        {
            try
            {
                return Directory.Exists(Path.GetDirectoryName(path));
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 查找文件中是否存在匹配的内容
        /// </summary>
        /// <param name="fileInfo">查找的文件流信息</param>
        /// <param name="lineTxt">在文件中需要查找的行文本</param>
        /// <param name="lowerUpper">是否区分大小写，true为区分，false为不区分</param>
        /// <returns>返回true则表示存在，false为不存在</returns>
        public static bool FindLineTextFromFile(FileInfo fileInfo, string lineTxt, bool lowerUpper = false)
        {
            bool isTrue = false; //表示没有查询到信息
            try
            {
                //首先判断文件是否存在
                if (fileInfo.Exists)
                {
                    var streamReader = new StreamReader(fileInfo.FullName);
                    do
                    {
                        string readLine = streamReader.ReadLine(); //读取的信息
                        if (String.IsNullOrEmpty(readLine))
                        {
                            break;
                        }
                        if (lowerUpper)
                        {
                            if (readLine.Trim() != lineTxt.Trim())
                            {
                                continue;
                            }
                            isTrue = true;
                            break;
                        }
                        if (readLine.Trim().ToLower() != lineTxt.Trim().ToLower())
                        {
                            continue;
                        }
                        isTrue = true;
                        break;
                    } while (streamReader.Peek() != -1);
                    streamReader.Close(); //继承自IDisposable接口，需要手动释放资源
                }
            }
            catch (Exception)
            {
                isTrue = false;
            }
            return isTrue;
        }

        public const string FileKey = "ihlih*0037JOHT*)(PIJY*(()JI^)IO%"; //加密密钥

        /// <summary>
        /// 对文件进行加密
        /// 调用:FileEncryptHelper.FileEncryptInfo(Server.MapPath("~" +路径), Server.MapPath("~" +路径), FileHelper.FileEncrityKey)
        /// </summary>
        /// <param name="fileOriginalPath">需要加密的文件路径</param>
        /// <param name="fileFinshPath">加密完成后存放的文件路径</param>
        /// <param name="fileKey">文件密钥</param>
        public static void FileEncryptInfo(string fileOriginalPath, string fileFinshPath, string fileKey)
        {
            //分组加密算法的实现
            using (var fileStream = new FileStream(fileOriginalPath, FileMode.Open))
            {
                var buffer = new Byte[fileStream.Length];
                fileStream.Read(buffer, 0, buffer.Length); //得到需要加密的字节数组
                //设置密钥，密钥向量，两个一样，都是16个字节byte
                var rDel = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(fileKey),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform cryptoTransform = rDel.CreateEncryptor();
                byte[] cipherBytes = cryptoTransform.TransformFinalBlock(buffer, 0, buffer.Length);
                using (var fileSEncrypt = new FileStream(fileFinshPath, FileMode.Create, FileAccess.Write))
                {
                    fileSEncrypt.Write(cipherBytes, 0, cipherBytes.Length);
                }
            }
        }

        /// <summary>
        /// 对文件进行解密
        /// 调用:FileEncryptHelper.FileDecryptInfo(Server.MapPath("~" +路径), Server.MapPath("~" +路径), FileHelper.FileEncrityKey)
        /// </summary>
        /// <param name="fileFinshPath">传递需要解密的文件路径</param>
        /// <param name="fileOriginalPath">解密后文件存放的路径</param>
        /// <param name="fileKey">密钥</param>
        public static void FileDecryptInfo(string fileFinshPath, string fileOriginalPath, string fileKey)
        {
            using (var fileStreamIn = new FileStream(fileFinshPath, FileMode.Open, FileAccess.Read))
            {
                using (var fileStreamOut = new FileStream(fileOriginalPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    var rDel = new RijndaelManaged
                    {
                        Key = Encoding.UTF8.GetBytes(fileKey),
                        Mode = CipherMode.ECB,
                        Padding = PaddingMode.PKCS7
                    };
                    using (var cryptoStream = new CryptoStream(fileStreamOut, rDel.CreateDecryptor(),
                        CryptoStreamMode.Write))
                    {
                        var bufferLen = 4096;
                        var buffer = new byte[bufferLen];
                        int bytesRead;
                        do
                        {
                            bytesRead = fileStreamIn.Read(buffer, 0, bufferLen);
                            cryptoStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead != 0);
                    }
                }
            }
        }
    }
}