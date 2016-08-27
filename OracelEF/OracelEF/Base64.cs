using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class DealImage
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data">"data:image/jpeg;base64,/9j/4AAQS</param>
    public DealImage(string data)
    {
        var args = data.Split(';');
        this.ExtensionName = args[0].Split('/')[1];
        this.DataType = args[1].Substring(0, args[1].IndexOf(','));
        this.Base64Data = args[1].Substring(args[1].IndexOf(',') + 1);
    }

    public string ExtensionName { get; private set; }

    public string Base64Data { get; private set; }

    public string DataType { get; private set; }

    public bool ExportToFile(string fileName)
    {
        try
        {
            var path = System.IO.Path.GetDirectoryName(fileName);
            if (System.IO.File.Exists(path))
            {
                File.Delete(path);
            }
            Directory.CreateDirectory(path);
            if (this.DataType == "base64")
            {
                var bitmap = Base64StringToImage(Base64Data);
                if (bitmap == null)
                    return false;
                bitmap.Save(fileName);
                return true;
            }
            else
            {
                //var bytes = byte(Base64Data);
                //System.IO.File.WriteAllBytes(fileName, bytes);
                return true;
            }
            //return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    /// 清空指定的文件夹，但不删除文件夹
    /// </summary>
    /// <param name="dir"></param>
    public static void DeleteFolder(string dir)
    {
        foreach (string d in Directory.GetFileSystemEntries(dir))
        {
            if (File.Exists(d))
            {
                FileInfo fi = new FileInfo(d);
                if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                    fi.Attributes = FileAttributes.Normal;
                File.Delete(d);//直接删除其中的文件  
            }
            else
            {
                DirectoryInfo d1 = new DirectoryInfo(d);
                if (d1.GetFiles().Length != 0)
                {
                    DeleteFolder(d1.FullName);////递归删除子文件夹
                }
                Directory.Delete(d);
            }
        }
    }

    private Bitmap Base64StringToImage(string baseImageData)
    {
        try
        {
            byte[] bt = Convert.FromBase64String(baseImageData);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bt);
            Bitmap bitmap = new Bitmap(stream);
            return bitmap;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}


