using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Net.Mail;
namespace HttpHelp
{
    public class HttpHelper
    {
        public static string GetHtmlEx(string url, Encoding encoding)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "get";
            request.UserAgent = @"Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 1.1.4322; .NET CLR 3.0.04506.30)";
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, encoding);
            String html = reader.ReadToEnd();
            response.Close();

            return html;
        }
        public static string GetHtmlExByByPost(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                //webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.UserAgent = @"Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 1.1.4322; .NET CLR 3.0.04506.30)";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), dataEncode);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                return null;
            }
            return ret;
        }
        public static string GetHtmlExByByPost(string postUrl, string paramData, Encoding dataEncode, Action<HttpWebRequest> action)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.UserAgent = @"Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 1.1.4322; .NET CLR 3.0.04506.30)";
                //webReq.Accept ="image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                webReq.ContentLength = byteArray.Length;
                if (action != null)
                    action(webReq);
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), dataEncode);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                return null;
            }
            return ret;
        }
        public static System.DateTime ConvertIntDateTime(double milliseconds)
        {
            System.DateTime time = System.DateTime.MinValue;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddMilliseconds(milliseconds);
            return time;
        }
    }
    public class DealWithFile
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

        public static void WtiteLog(string path,string mes)
        {
           
            var filepath = path + @"123.txt";

            if (!File.Exists(filepath))
            {
                File.CreateText(filepath);
                
                File.AppendAllLines(filepath,new string[] {mes},Encoding.UTF8);
                  //  (filepath,mes,);
                //FileStream fs1 = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                //StreamWriter sw = new StreamWriter(fs1);
                //sw.WriteLine(mes);
                //sw.Close();
                //fs1.Close();
                
            }
            else
            {
                File.AppendAllLines(filepath, new string[] { mes }, Encoding.UTF8);
                //FileStream fs = new FileStream(filepath, FileMode.Append, FileAccess.Write);
                //StreamWriter sr = new StreamWriter(fs);
                //sr.WriteLine(mes);//开始写入值
                //sr.Close();
                //fs.Close();
            }

        }
        /// <summary>
        /// 复制文件夹中的所有文件夹与文件到另一个文件夹
        /// </summary>
        /// <param name="sourcePath">源文件夹</param>
        /// <param name="destPath">目标文件夹</param>
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
    }
    public class DealMap
    {
         public const double X_PI = 3.14159265358979324 * 3000.0 / 180.0;

        public const double PI = 3.1415926535897932384626;
        public const double A = 6378245.0;
        public const double EE = 0.00669342162296594323;

        /// <summary>
        /// 百度坐标系(BD-09)与火星坐标系(GCJ-02)的转换
        /// 即百度转谷歌、高德
        /// </summary>
        /// <param name="BD_Point">百度坐标</param>
        /// <returns>火星坐标</returns>
        public static MapPoint BD09toGCJ02(MapPoint BD_Point)
        {
            MapPoint GCJ_Point = new MapPoint();
            try
            {

                double x = BD_Point.Lon - 0.0065;
                double y = BD_Point.Lat - 0.006;
                double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * X_PI);
                double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * X_PI);
                GCJ_Point.Lon = z * Math.Cos(theta);
                GCJ_Point.Lat = z * Math.Sin(theta);
            }
            catch
            {
                return null;
            }
            return GCJ_Point;
        }
        /// <summary>
        /// 火星坐标系 (GCJ-02) 与百度坐标系 (BD-09) 的转换
        /// 即谷歌、高德转百度
        /// </summary>
        /// <param name="GCJ_Point"></param>
        /// <returns></returns>
        public static MapPoint GCJ02toBD09(MapPoint GCJ_Point)
        {
            MapPoint BD_Point = new MapPoint();
            try
            {

                double z = Math.Sqrt(GCJ_Point.Lon * GCJ_Point.Lon + GCJ_Point.Lat * GCJ_Point.Lat) + 0.00002 * Math.Sin(GCJ_Point.Lat * X_PI);
                double theta = Math.Atan2(GCJ_Point.Lat, GCJ_Point.Lon) + 0.000003 * Math.Cos(GCJ_Point.Lon * X_PI);
                BD_Point.Lon = z * Math.Cos(theta) + 0.0065;
                BD_Point.Lat = z * Math.Sin(theta) + 0.006;
            }
            catch
            {
                return null;
            }
            return BD_Point;
        }
        /// <summary>
        /// WGS84转GCJ02
        /// 即标准WGS84转火星坐标系
        /// </summary>
        /// <param name="WGS_Point">WGS坐标</param>
        /// <returns>在中国范围内，则返回转化后的坐标；否则，返回原始坐标</returns>
        public static MapPoint WGS84toGCJ02(MapPoint WGS_Point)
        {
            MapPoint GCJ_Point = new MapPoint();
            try
            {
                if (IsPointOutOfChina(WGS_Point))
                {
                    return WGS_Point;
                }
                double dlon = TransformLon(WGS_Point.Lon - 105.0, WGS_Point.Lat - 35.0);
                double dlat = TransformLat(WGS_Point.Lon - 105.0, WGS_Point.Lat - 35.0);
                double radlat = WGS_Point.Lat / 180.0 * PI;
                double magic = Math.Sin(radlat);
                magic = 1 - EE * magic * magic;
                double sqrtMagic = Math.Sqrt(magic);
                dlon = (dlon * 180.0) / (A / sqrtMagic * Math.Cos(radlat) * PI);
                dlat = (dlat * 180.0) / ((A * (1 - EE)) / (magic * sqrtMagic) * PI);
                GCJ_Point.Lon = WGS_Point.Lon + dlon;
                GCJ_Point.Lat = WGS_Point.Lat + dlat;
            }
            catch
            {
                return null;
            }
            return GCJ_Point;
        }
        /// <summary>
        /// GCJ02转换为WGS84
        /// 即火星坐标系转标准WGS84坐标系
        /// </summary>
        /// <param name="GCJ_Point">火星坐标</param>
        /// <returns>在中国范围内，则返回转化后的坐标；否则，返回原始坐标</returns>
        public  MapPoint GCJ02toWGS84(MapPoint GCJ_Point)
        {
            MapPoint WGS_Point = new MapPoint();

            try
            {
                if (IsPointOutOfChina(GCJ_Point))
                {
                    return GCJ_Point;
                }
                double dlon = TransformLon(GCJ_Point.Lon - 105.0, GCJ_Point.Lat - 35.0);
                double dlat = TransformLat(GCJ_Point.Lon - 105.0, GCJ_Point.Lat - 35.0);
                double radlat = GCJ_Point.Lat / 180.0 * PI;
                double magic = Math.Sin(radlat);
                magic = 1 - EE * magic * magic;
                double sqrtMagic = Math.Sqrt(magic);
                dlon = (dlon * 180.0) / (A / sqrtMagic * Math.Cos(radlat) * PI);
                dlat = (dlat * 180.0) / ((A * (1 - EE)) / (magic * sqrtMagic) * PI);
                WGS_Point.Lon = GCJ_Point.Lon * 2 - (GCJ_Point.Lon + dlon);
                WGS_Point.Lat = GCJ_Point.Lat * 2 - (GCJ_Point.Lat + dlat);
            }
            catch
            {
                return null;
            }
            return WGS_Point;
        }



        /// <summary>
        /// 纬度转换函数
        /// </summary>
        /// <param name="Lon"></param>
        /// <param name="Lat"></param>
        /// <returns></returns>
        public static double TransformLat(double Lon, double Lat)
        {
            var ret = -100.0 + 2.0 * Lon + 3.0 * Lat + 0.2 * Lat * Lat + 0.1 * Lon * Lat + 0.2 * Math.Sqrt(Math.Abs(Lon));
            ret += (20.0 * Math.Sin(6.0 * Lon * PI) + 20.0 * Math.Sin(2.0 * Lon * PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(Lat * PI) + 40.0 * Math.Sin(Lat / 3.0 * PI)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(Lat / 12.0 * PI) + 320 * Math.Sin(Lat * PI / 30.0)) * 2.0 / 3.0;
            return ret;
        }
        /// <summary>
        /// 经度转换函数
        /// </summary>
        /// <param name="Lon"></param>
        /// <param name="Lat"></param>
        /// <returns></returns>
        public static double TransformLon(double Lon, double Lat)
        {
            double ret = 300.0 + Lon + 2.0 * Lat + 0.1 * Lon * Lon + 0.1 * Lon * Lat + 0.1 * Math.Sqrt(Math.Abs(Lon));
            ret += (20.0 * Math.Sin(6.0 * Lon * PI) + 20.0 * Math.Sin(2.0 * Lon * PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(Lon * PI) + 40.0 * Math.Sin(Lon / 3.0 * PI)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(Lon / 12.0 * PI) + 300.0 * Math.Sin(Lon / 30.0 * PI)) * 2.0 / 3.0;
            return ret;
        }

        /// <summary>
        /// 判断点是否在中国范围内，在才做偏移，不在不做偏移
        /// </summary>
        /// <param name="p">点</param>
        /// <returns>在，返回False;不在，返回False</returns>
        private  static bool IsPointOutOfChina(MapPoint p)
        {
            if ((p.Lon < 72.004 || p.Lon > 137.8347) || (p.Lat < 0.8293 || p.Lat > 55.8271))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    /// <summary>
    /// 定义MapPoint类，存储点的经纬度坐标
    /// </summary>
    public class MapPoint
    {
        private double _Lon;
        public double Lon
        {
            get
            {
                return _Lon;
            }
            set
            {
                _Lon = value;
            }
        }
        private double _Lat;
        public double Lat
        {
            get
            {
                return _Lat;
            }
            set
            {
                _Lat = value;
            }
        }


        public MapPoint()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        public MapPoint(double lon, double lat)
        {
            this._Lon = lon;
            this._Lat = lat;
        }

    }

    public class DataTableToJson 
    {
    /// <summary>     
        /// Datatable转换为Json     
        /// </summary>    
        /// <param name="table">Datatable对象     
        /// <returns>Json字符串</returns>     
        public static string ToJson(DataTable dt)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }
        /// <summary>
        /// 格式化字符型、日期型、布尔型
        /// </summary>
        /// <param name="str">
        /// <param name="type">
        /// <returns></returns>
        private static string StringFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            else if (type != typeof(string) && string.IsNullOrEmpty(str))
            {
                str = "\"" + str + "\"";
            }
            return str;
        }
        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="s">字符串
        /// <returns>json字符串</returns>
        private static string String2Json(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }
    
    
    
    
    
    
    }
    public class Email
    {
        public static string sendMail(string topic, string attachmentUrl, string body)
        {
            string sendAddress = "username@sina.com";//发件者邮箱地址
            string sendPassword = "password";//发件者邮箱密码
            string receiveAddress = "251469031@qq.com";//收件人收箱地址
            string mailTopic = topic;//主题
            string mailAttachment = attachmentUrl;//附件
            string mailBody = body;//内容
            string[] sendUsername = sendAddress.Split('@');

            SmtpClient client = new SmtpClient("smtp." + sendUsername[1].ToString()); //设置邮件协议

            client.UseDefaultCredentials = false;//这一句得写前面
            //client.EnableSsl = true;//服务器不支持SSL连接

            client.DeliveryMethod = SmtpDeliveryMethod.Network; //通过网络发送到Smtp服务器
            client.Credentials = new NetworkCredential(sendUsername[0].ToString(), sendPassword); //通过用户名和密码 认证
            MailMessage mmsg = new MailMessage(new MailAddress(sendAddress), new MailAddress(receiveAddress)); //发件人和收件人的邮箱地址
            mmsg.Subject = mailTopic;//邮件主题
            mmsg.SubjectEncoding = Encoding.UTF8;//主题编码
            mmsg.Body = mailBody;//邮件正文
            mmsg.BodyEncoding = Encoding.UTF8;//正文编码
            mmsg.IsBodyHtml = true;//设置为HTML格式 
            mmsg.Priority = MailPriority.High;//优先级
            if (mailAttachment.Trim() != "")
            {
                mmsg.Attachments.Add(new Attachment(mailAttachment));//增加附件
            }
            try
            {
                client.Send(mmsg);
                return null;
            }
            catch (Exception ee)
            {
                return ee.Message;
            }
        }
    
    
    
    }
}

