// 源文件头信息：
// <copyright file="ToolsHelper.cs">
// Copyright(c)2014-2034 Kencery.All rights reserved.
// 个人博客：http://www.cnblogs.com/hanyinglong
// 创建人：韩迎龙(kencery)
// 创建时间：2014/12/10 10:17
// </copyright>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Words.Lists;

namespace KenceryCommonMethod
{
    /// <summary>
    /// 通用工具类，封装大量方便调用的方法
    /// <auther>
    ///     <name>Kencery</name>
    ///     <date>2014/7/3</date>
    /// </auther>
    /// 说明：
    ///     1.GetGuidInfo 封装去掉-的GUID
    ///     2.ConvertIntArrayInfo 将字符串按照指定格式转换为int数组
    ///     3.ConvertStringInfo   将字符串按照指定的格式转换为string集合
    ///     4.GetListStrArray   将指定的字符串按照分隔符转换成List集合
    ///     5.GetStrByList    将指定的字符串集合按照分隔符分隔成string
    ///     6.GetArrayValueStr  将字典集合按照分隔符分隔成string
    ///     7.DelStrSubSting，DelStrSubSting   对字符串进行删除操作
    ///     8.StrByteLength     得到字符串的字节数
    ///     9.HtmlToText     将HTML文件转换成文本文件
    ///     10.DateDiff    根据输入的开始时间和结束时间计算返回最后的日期差
	///		11.IsTrueObject  判断是否是引用类型/值类型
    /// </summary>
    public static class StringToolsHelper
    {
        /// <summary>
        /// 获取GUID随机字符串,除去“—”显示的信息
        /// </summary>
        /// <returns>返回移除GUID生成的-字符</returns>
        public static string GetGuidInfo()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 以指定的char类型的符号作为分隔符将指定的字符串分割成int数组
        /// stringComma格式为："1,2,3,4"
        /// </summary>
        /// <param name="stringComma">需要分割的字符串</param>
        /// <param name="strSplit">char类型的分隔符</param>
        /// <returns>返回分隔之后的int数组</returns>
        public static int[] ConvertIntArrayInfo(string stringComma, char strSplit = ',')
        {
            string[] stringArray = stringComma.Split(new[] {strSplit});
            var intPurchaseOrderIdS = new int[stringArray.Count()];
            for (int i = 0; i < stringArray.Count(); i++)
            {
                intPurchaseOrderIdS[i] = Convert.ToInt32(stringArray[i]);
            }
            return intPurchaseOrderIdS;
        }

        /// <summary>
        /// 以指定字符串作为分割符将指定字符串分割成数组
        /// </summary>
        /// <param name="value">要分割的字符串</param>
        /// <param name="strSplit">字符串类型的分隔符</param>
        /// <param name="removeEmptyEntries">是否移除数据中元素为空字符串的项，true为移除，false为不移除</param>
        /// <returns>返回分割后的字符串数组</returns>
        public static string[] ConvertStringInfo(this string value, string strSplit = ",",
            bool removeEmptyEntries = false)
        {
            return value.Split(new[] {strSplit},
                removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
        }

        /// <summary>
        /// 将指定的字符串按照分隔符转换成List集合
        /// </summary>
        /// <param name="stringComma">需要转换的字符串</param>
        /// <param name="toLower">小写或者大写的限制，true为小写，false为大写</param>
        /// <param name="strSplit">分隔符，默认为','</param>
        /// <returns>返回转换后的集合信息</returns>
        public static List<string> GetListStrArray(string stringComma, bool toLower = false, char strSplit = ',')
        {
            //首先将字符串转换为字符串数组string[]
            string[] strArrays = stringComma.Split(strSplit);
            //最后循环处理生成List集合
            return (from strArray in strArrays
                where !string.IsNullOrEmpty(strArray) && strArray != strSplit.ToString(CultureInfo.InvariantCulture)
                select toLower ? strArray.ToLower() : strArray.ToUpper()).ToList();
        }

        /// <summary>
        /// 将List字符串集合按照分隔符组装成字符串string
        /// </summary>
        /// <param name="listStrs">需要组装的字符串集合</param>
        /// <param name="strSplit">分隔符</param>
        /// <param name="toLower">小写或者大写的限制，true为小写，false为大写</param>
        /// <returns>返回转换后的字符串信息</returns>
        public static string GetStrByList(List<string> listStrs, string strSplit = ",", bool toLower = false)
        {
            var sb = new StringBuilder();
            //循环字符串集合进行操作
            foreach (var listStr in listStrs)
            {
                sb.Append(listStr).Append(strSplit);
            }
            //添加完成之后去掉字符串最后添加的一个，信息
            //进行转换大小写信息
            string sbStr = sb.Remove(sb.Length - 1, 1).ToString(); //去掉最后一个添加的分隔符
            sbStr = toLower ? sbStr.ToLower() : sbStr.ToUpper();
            return sbStr;
        }

        /// <summary>
        /// 将字典集合按照分隔符组装成字符串string
        /// </summary>
        /// <param name="dicLists">需要组装的字符串集合</param>
        /// <param name="strSplit">分隔符</param>
        /// <param name="toLower">小写或者大写的限制，true为小写，false为大写</param>
        /// <returns>返回转换后的字符串信息</returns>
        public static string GetArrayValueStr(Dictionary<int, int> dicLists, string strSplit = ",", bool toLower = false)
        {
            var sb = new StringBuilder();
            foreach (var dicList in dicLists)
            {
                sb.Append(dicList + ",");
            }
            string sbStr = sb.Remove(sb.Length - 1, 1).ToString(); //去掉最后一个添加的分隔符
            sbStr = toLower ? sbStr.ToLower() : sbStr.ToUpper();
            return sbStr;
        }

        /// <summary>
        /// 将指定的字符串进行删除操作
        /// </summary>
        /// <param name="str">需要操作的字符串</param>
        /// <param name="start">开始删除的位置</param>
        /// <param name="delLength">需要删除的长度</param>
        /// <returns>返回需要的字符串</returns>
        public static string DelStrSubSting(string str, int start, int delLength)
        {
            return str.Substring(start, delLength);
        }

        /// <summary>
        /// 将指定的字符串进行删除操作
        /// </summary>
        /// <param name="str">需要操作的字符串</param>
        /// <param name="strSplit">删除标准，删除按照，分割的最后的字符</param>
        /// <returns>返回需要的字符串</returns>
        public static string DelStrSubSting(string str, string strSplit = ",")
        {
            return str.Substring(str.Length - 1, str.LastIndexOf(strSplit));
        }

        /// <summary>
        /// 得到字符串的字节数
        /// </summary>
        /// <param name="str">需要计算的字符串</param>
        /// <returns>返回最后的字节长度</returns>
        public static int StrByteLength(string str)
        {
            var asciiEncoding = new ASCIIEncoding();
            int tempLength = 0; //计算字符串的长度
            byte[] bytes = asciiEncoding.GetBytes(str);
            foreach (byte t in bytes)
            {
                if (t == 63)
                {
                    tempLength = tempLength + 2;
                }
                else
                {
                    tempLength = tempLength + 1;
                }
            }
            return tempLength;
        }

        /// <summary>
        /// 将HTML格式的内容转换成文本形式
        /// </summary>
        /// <param name="strHtml">传递的需要转换的HTML</param>
        /// <returns>返回解析后的TEXT</returns>
        public static string HtmlToText(string strHtml)
        {
            string[] aryReg =
            {
                @"<script[^>]*?>.*?</script>",
                @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                @"([\r\n])[\s]+", @"&(quot|#34);", @"&(amp|#38);", @"&(lt|#60);", @"&(gt|#62);",
                @"&(nbsp|#160);", @"&(iexcl|#161);", @"&(cent|#162);", @"&(pound|#163);", @"&(copy|#169);",
                @"&#(\d+);", @"-->", @"<!--.*\n"
            };
            ////返回最后转换成功的字符串
            string strOutPut = aryReg.Select(t => new Regex(t, RegexOptions.IgnoreCase))
                .Aggregate(strHtml, (current, regex) => regex.Replace(current, string.Empty));
            strOutPut.Replace("<", "").Replace(">", "").Replace("\r\n", "");
            return strOutPut;
        }

        /// <summary>
        /// 根据输入的开始时间和结束时间计算返回最后的日期差
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>返回最终的日期差，负数为失败，将错误原因写入事务日志</returns>
        public static int DateDiff(DateTime startTime, DateTime endTime)
        {
            try
            {
                double dateDiff = 0;
                while (startTime.AddDays(dateDiff) < endTime)
                {
                    dateDiff++;
                }
                return Convert.ToInt32(dateDiff);
            }
            catch (Exception ex)
            {
                //写入事务日志 ex
                return -1;
            }
        }

        /// <summary>
        /// 格式化日期时间，格式化成自己想要的各种格式
        /// </summary>
        /// <param name="dateTime">需要格式话的日期</param>
        /// <param name="dateMode">现实的模式(0-9中格式实现)</param>
        /// <returns>返回转换后的时间信息</returns>
        public static string FormatDate(DateTime dateTime, string dateMode = "0")
        {
            switch (dateMode)
            {
                case "0":
                    return dateTime.ToString("yyyy-MM-dd");
                case "1":
                    return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                case "2":
                    return dateTime.ToString("yyyy/MM/dd");
                case "3":
                    return dateTime.ToString("yyyy年MM月dd日");
                case "4":
                    return dateTime.ToString("MM-dd");
                case "5":
                    return dateTime.ToString("MM/dd");
                case "6":
                    return dateTime.ToString("MM月dd日");
                case "7":
                    return dateTime.ToString("yyyy-MM");
                case "8":
                    return dateTime.ToString("yyyy/MM");
                case "9":
                    return dateTime.ToString("yyyy年MM月");
                default:
                    return dateTime.ToString(CultureInfo.InvariantCulture);
            }
        }
		
		/// <summary>
        /// 判断对象是值类型还是引用类型
        /// </summary>
        /// <param name="obj">需要判断的对象</param>
        /// <returns> true为值类型，false为引用类型</returns>
        public static bool IsTrueObject(Object obj)
        {
            return obj.GetType().IsValueType;
        }
    }
}