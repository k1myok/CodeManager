﻿// 源文件头信息：
// <copyright file="GetComputerHelper.cs">
// Copyright(c)2014-2034 Kencery.All rights reserved.
// 个人博客：http://www.cnblogs.com/hanyinglong
// 创建人：韩迎龙(kencery)
// 创建时间：2015/04/29 11:17
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Web;

namespace KenceryCommonMethod
{
    /// <summary>
    /// 获取电脑本机的信息(IP，名称，Mac地址)
    /// <auther>
    ///     <name>Kencery</name>
    ///     <date>2014/12/11</date>
    /// </auther>
    /// </summary>
    public static class GetComputerHelper
    {
        #region--------------------Web程序获取电脑IP信息--------------------

        /// <summary>
        /// 获取电脑的IP信息
        /// 使用：var computerIp=GetComputerHelper.GetComputerIp();
        /// </summary>
        /// <returns>返回获取到的电脑IP信息</returns>
        public static string GetComputerIp()
        {
            //定义变量获取Ip,赋给Ip默认值
            bool isError = false; //true表示IP地址错误，false表示IP地址正确
            string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] ??
                        HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (ip.Length > 15) //IP地址的长度不可能大于15位数(192.168.159.123)
            {
                isError = true;
            }
            else //IP地址初步判断是正确的，在else中还要进行再次的判断
            {
                string[] tempIps = ip.Split('.');
                if (tempIps.Length == 4) //证明是IP地址，否则不正确
                {
                    foreach (var tempIp in tempIps.Where(tempIp => tempIp.Length > 3))
                    {
                        isError = true;
                    }
                }
                else
                {
                    isError = true;
                }
            }
            return isError ? "1.1.1.1" : ip;
        }

        /// <summary>
        /// 获取电脑的IP信息(集合)
        /// 使用：var computerIpList=GetComputerHelper.GetComputerIpList();
        /// </summary>
        /// <returns>返回获取到的电脑的IP信息</returns>
        public static List<string> GetComputerIpList()
        {
            IPAddress[] ipAddresses = Dns.GetHostEntry(GetHostName()).AddressList;
            return ipAddresses.Select(ipAddress => ipAddress.ToString()).ToList();
        }

        #endregion

        #region-------------------Web程序获取电脑主机名称-------------------

        /// <summary>
        /// 获取电脑的主机名称
        /// 使用：var computerHostName=GetComputerHelper.GetHostName();
        /// </summary>
        /// <returns>返回获取电脑的主机名称</returns>
        public static string GetHostName()
        {
            return Dns.GetHostName();
        }

        #endregion

        #region--------------------Web程序获取电脑Mac地址-------------------

        /// <summary>
        /// 获取电脑的Mac地址
        /// 使用：var computerMac=GetComputerHelper.GetMacList();
        /// </summary>
        /// <returns>返回获取电脑的Mac地址</returns>
        public static List<string> GetMacList()
        {
            var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection mocs = mc.GetInstances();
            return (from ManagementBaseObject moc in mocs
                where moc["IPEnabled"].ToString() == "True"
                select moc["MacAddress"].ToString()).ToList();
        }

        #endregion
    }
}