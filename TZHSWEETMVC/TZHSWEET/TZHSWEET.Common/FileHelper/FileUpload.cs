using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace TZHSWEET.Common
{
    /// <summary>
    /// 文件上传
    /// </summary>
   public class FileUpload
    {
        /// <summary>
        /// 单个文件上传(只获取第一个文件,返回的文件名是文件的md5值),返回为json数据格式,成功返回{status:"success",website:"a.jpg"},失败,返回{status:"error",website:"error"}
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="FilePath">文件路径</param>
        /// <param name="outFileName">返回文件的md5</param>
        /// <returns>返回json状态信息</returns>
       public static string FileUploadSingle(HttpContext context, string FilePath,out string outFileName)
       { 
           string json="";
        //找到目标文件对象
        HttpFileCollection hfc = context.Request.Files;
            HttpPostedFile hpf = hfc[0];

            if (hpf.ContentLength > 0)
            {
                //根据文件的md5的hash值做文件名,防止文件的重复和图片的浪费
                string FileName = CreateFileForFileNameByMd5(hpf.FileName); //CreateDateTimeForFileName(hpf.FileName);//自动生成文件名
                
                string file = System.IO.Path.Combine(FilePath,
                 FileName);
                if (!Directory.Exists(Path.GetDirectoryName(file)))
                {
                    Directory.CreateDirectory(file);
                }
                hpf.SaveAs(file);
                json = "{status:\"success\",website:\"" + FileName + "\"}";
                outFileName = FileName;
            }
            else {
                json = "{status:\"error\",website:\"error\"}";
                outFileName = FilePath;
            }
          
        return json;
       
       }
       /// <summary>
       /// 单个文件上传(只获取第一个文件,返回的文件名是文件的md5值),返回为json数据格式,成功返回{status:"success",website:"a.jpg"},失败,返回{status:"error",website:"error"}
       /// </summary>
       /// <param name="context">上下文</param>
       /// <param name="FilePath">文件路径</param>
       /// <param name="outFileName">返回文件的md5</param>
       /// <returns>返回json状态信息</returns>
       public static string FileUploadMulti(HttpContext context, string FilePath, out string[] outFileName)
       {
          
           string json = "";
           //找到目标文件对象
           HttpFileCollection hfc = context.Request.Files;
            outFileName=new  string[hfc.Count];
            for (int i=0;i<hfc.Count;i++)
            {
                if (hfc[i].ContentLength > 0)
                {
                    //根据文件的md5的hash值做文件名,防止文件的重复和图片的浪费
                    string FileName = CreateFileForFileNameByMd5(hfc[i].FileName); //CreateDateTimeForFileName(hpf.FileName);//自动生成文件名

                    string file = System.IO.Path.Combine(FilePath,
                     FileName);
                    if (!Directory.Exists(Path.GetDirectoryName(file)))
                    {
                        Directory.CreateDirectory(file);
                    }
                    hfc[i].SaveAs(file);
                    outFileName[i]= FileName;
                }
                
            }
           //如果文件非空,就返回json数据
            if (outFileName.Count() > 0)
                json = "{status:\"success\",website:\"" + string.Join(",",outFileName) + "\"}";
           else
             json = "{status:\"error\",website:\"error\"}";
           

           return json;

       }
       /// <summary>
       /// 根据文件的md5值当作文件的文件名(
       /// </summary>
       /// <param name="FileName">文件名</param>
       /// <returns>文件的md5值</returns>
       public static string CreateFileForFileNameByMd5(string FileName)
       {
          return  TZHSWEET.Common.MD5Provider.Hash(FileName)+Path.GetExtension(FileName);
       }
       /// <summary>
       /// 根据当前时间生成文件名
       /// </summary>
       /// <returns></returns>
       public static string CreateDateTimeForFileName(string FileName)
       {
           string datetime=DateTime.Now.ToString ("yyyyMMddhhmmssffff");
      string FilePath=datetime+Path.GetExtension(FileName);
           return FilePath;
       }
    }
}
