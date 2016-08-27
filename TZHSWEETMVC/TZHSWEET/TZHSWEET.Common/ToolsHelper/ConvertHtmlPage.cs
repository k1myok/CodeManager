 /*  作者：       tianzh
 *  创建时间：   2012/7/15 11:03:09
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TZHSWEET.Common
{
    /// <summary>
    ///ConvertHtmlPage 生成静态页面
    /// </summary>
    public class ConvertHtmlPage
    {
        /// <summary>
        ///  生成HTML文件
        /// </summary>
        /// <param name="templatePath">模板路径</param>
        /// <param name="templateName">模板名称</param>
        /// <param name="htmlPath">生成HTML的路径</param>
        /// <param name="htmlName">生成HTML的名称</param>
        /// <param name="format">替换的内容</param>
        /// <returns></returns>
        public static bool CreatePage(string templatePath, string templateName, string htmlPath, string htmlName, List<string> format)
        {
            try
            {
                //读取模板文件
                StringBuilder htmltext = new StringBuilder();
                using (StreamReader sr = new StreamReader(templatePath + templateName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        htmltext.AppendLine(line);
                    }
                    sr.Close();
                }
                //替换HTML中的标记内容
                for (int i = 0; i < format.Count; i++)
                {
                    htmltext.Replace("$htmlformat[" + i + "]", format[i]);
                }
                //生成HTML文件
                using (StreamWriter sw = new StreamWriter(htmlPath + htmlName, false, System.Text.Encoding.GetEncoding("GB2312")))
                {
                    sw.WriteLine(htmltext);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
