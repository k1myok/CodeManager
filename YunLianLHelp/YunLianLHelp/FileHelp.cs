using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Security.Cryptography;
namespace YunLianLHelp
{
    #region 文件操作
    public class FileHelp
    {
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        /// <summary>
        /// 写日志的方法
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="mes">要写入的信息</param>
        public static bool WriteLog(string path, string mes)
        {
            var filepath = path + DateTime.Now.ToLongDateString().ToString() + ".txt";
            if (!File.Exists(filepath))
            {
                File.CreateText(filepath);
            }
            try
            {
               File.AppendAllLines(filepath, new string[] { mes }, Encoding.UTF8);
               return true;
            }
            catch (Exception ex)
            {
               var result = ex.Message; 
               return false;
            }

        }
        public static bool StreamWriteLog(string filepath, string mes)
        {
            var path= filepath + DateTime.Now.ToLongDateString().ToString() + ".txt";
            if (!File.Exists(path))
            {
                File.CreateText(path);
            }
            try
            {
                FileStream fs = new FileStream(filepath, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(mes);
                sw.Close();
                fs.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
          
        }
        #region 
        public static bool WriteLog(string log, string logPath, string logKind)
        {
            var path = logPath + "\\" + logKind;
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            var file = System.IO.Path.Combine(path, DateTime.Now.ToString("yyyy-MM-dd HH-mm") + ".txt");
            try
            {
                System.IO.File.AppendAllText(file, log == null ? "空内容" : log);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //Console.WriteLine(ex.Message);
            }
        }
        #endregion
        public static void CopyFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    //目标目录不存在则创建
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("创建目标目录失败：" + ex.Message);
                    }
                }
                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    File.Copy(c, destFile, true);//覆盖模式
                });
                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));
                folders.ForEach(c =>
                {
                    string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //采用递归的方法实现
                    CopyFolder(c, destDir);
                });
            }
            else
            {
                throw new DirectoryNotFoundException("源目录不存在！");
            }
        }
        /// <summary>
        /// 移动文件夹中的所有文件夹与文件到另一个文件夹
        /// </summary>
        /// <param name="sourcePath">源文件夹</param>
        /// <param name="destPath">目标文件夹</param>
        public static void MoveFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    //目标目录不存在则创建
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("创建目标目录失败：" + ex.Message);
                    }
                }
                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //覆盖模式
                    if (File.Exists(destFile))
                    {
                        File.Delete(destFile);
                    }
                    File.Move(c, destFile);
                });
                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));

                folders.ForEach(c =>
                {
                    string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //Directory.Move必须要在同一个根目录下移动才有效，不能在不同卷中移动。
                    //Directory.Move(c, destDir);

                    //采用递归的方法实现
                    MoveFolder(c, destDir);
                });
            }
            else
            {
                throw new DirectoryNotFoundException("源目录不存在！");
            }
        }
        /// <summary>
        /// 删除指定目录下所有内容：方法一--删除目录，再创建空目录
        /// </summary>
        /// <param name="dirPath"></param>
        public static void DeleteFolderEx(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                Directory.Delete(dirPath);
                Directory.CreateDirectory(dirPath);
            }
        }

        /// <summary>
        /// 删除指定目录下所有内容：方法二--找到所有文件和子文件夹删除
        /// </summary>
        /// <param name="dirPath"></param>
        public static void DeleteFolder(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                foreach (string content in Directory.GetFileSystemEntries(dirPath))
                {
                    if (Directory.Exists(content))
                    {
                        Directory.Delete(content, true);
                    }
                    else if (File.Exists(content))
                    {
                        File.Delete(content);
                    }
                }
            }
        }
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
    #endregion
}
