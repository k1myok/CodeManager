using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
namespace TZHSWEET.Common
{
    /// <summary>
    ///XMlHelper 的摘要说明
    /// </summary>
    public class XMlHelper
    {
       
        public XMlHelper()
        {
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Read(path, "/Node", "")
         * XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
         ************************************************/
        public static string Read(string path, string node, string attribute)
        {
            string value = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
            }
            catch { }
            return value;
        }
     
        //public static void getinfo(string path,string node,string attribute,string value,int num)
        //{
        //    string xmlFilePath = path;
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(xmlFilePath);

        //    //使用xpath表达式选择文档中所有的student子节点
        //    XmlNodeList studentNodeList = doc.SelectNodes(node);
        //    if (studentNodeList != null)
        //    {
        //        foreach (XmlNode studentNode in studentNodeList)
        //        {
        //            //通过Attributes获得属性名字为name的属性
        //            string name = studentNode.Attributes[attribute].Value;


        //            //通过SelectSingleNode方法获得当前节点下的courses子节点
        //            XmlNode coursesNode = studentNode.SelectSingleNode(value);

        //            //通过ChildNodes属性获得courseNode的所有一级子节点
        //            XmlNodeList courseNodeList = coursesNode.ChildNodes;
        //            if (courseNodeList != null)
        //            {
        //                foreach (XmlNode courseNode in courseNodeList)
        //                {
        //                    //通过FirstNode属性可以获得课程节点的第一个子节点，LastNode可以获得最后一个子节点
        //                    XmlNode teacherCommentNode = courseNode.FirstChild;
        //                    //读取CData节点
        //                    XmlCDataSection cdata = (XmlCDataSection)teacherCommentNode.FirstChild;
        //                    teacherCommentNode.NextSibling.Value = "tianzhuanghu";


        //                }
        //            }
        //        }
        //    }

        //}
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "Element", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Element", "Attribute", "Value")
         * XmlHelper.Insert(path, "/Node", "", "Attribute", "Value")
         ************************************************/
        public static void Insert(string path, string node, string element, string attribute, string value)
        {
            try
            {

                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                if (element.Equals(""))
                {
                    if (!attribute.Equals(""))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    XmlElement xe = doc.CreateElement(element);
                    if (attribute.Equals(""))
                        xe.InnerText = value;
                    else
                        xe.SetAttribute(attribute, value);
                    xn.AppendChild(xe);
                }
                doc.Save(path);

            }
            catch
            {
            }
        }
        public static void Insertinfo(string path, string node, string element, string attribute, string value)
        {
            try
            {

                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                if (element.Equals(""))
                {
                    if (!attribute.Equals(""))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    XmlElement xe = doc.CreateElement(element);
                    if (attribute.Equals(""))
                        xe.InnerText = value;
                    else
                        xe.SetAttribute(attribute, value);

                    xn.AppendChild(xe);
                }
                doc.Save(path);
            }
            catch
            {
            }
        }
        static public void updateinfo(string path, string node, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node).NextSibling;

                XmlCDataSection cdata = (XmlCDataSection)xn;

                cdata.InnerText = value;

                doc.Save(path);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Attribute", "Value")
         ************************************************/
        public static void Update(string path, string node, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                //XmlCDataSection cdata = (XmlCDataSection)xn;
                if (attribute.Equals(""))

                    xe.InnerText = value;
                else
                    xe.SetAttribute(attribute, value);
                doc.Save(path);
            }
            catch { }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Delete(path, "/Node", "")
         * XmlHelper.Delete(path, "/Node", "Attribute")
         ************************************************/
        public static void Delete(string path, string node, string attribute)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(attribute);
                doc.Save(path);
            }
            catch { }
        }


        //==================================================
        //XmlFile.xml：
        //<?xml version="1.0" encoding="utf-8"?>
        //<Root />
        //==================================================
        //使用方法：
        //string xml = Server.MapPath("XmlFile.xml");
        //插入元素
        //XmlHelper.Insert(xml, "/Root", "Studio", "", "");
        //插入元素/属性
        //XmlHelper.Insert(xml, "/Root/Studio", "Site", "Name", "小路工作室");
        //XmlHelper.Insert(xml, "/Root/Studio", "Site", "Name", "丁香鱼工作室");
        //XmlHelper.Insert(xml, "/Root/Studio", "Site", "Name", "谱天城工作室");
        //XmlHelper.Insert(xml, "/Root/Studio/Site[@Name='谱天城工作室']", "Master", "", "红尘静思");
        //插入属性
        //XmlHelper.Insert(xml, "/Root/Studio/Site[@Name='小路工作室']", "", "Url", "http://www.wzlu.com/");
        //XmlHelper.Insert(xml, "/Root/Studio/Site[@Name='丁香鱼工作室']", "", "Url", "http://www.luckfish.net/");
        //XmlHelper.Insert(xml, "/Root/Studio/Site[@Name='谱天城工作室']", "", "Url", "http://www.putiancheng.com/");
        //修改元素值
        //XmlHelper.Update(xml, "/Root/Studio/Site[@Name='谱天城工作室']/Master", "", "RedDust");
        //修改属性值
        //XmlHelper.Update(xml, "/Root/Studio/Site[@Name='谱天城工作室']", "Url", "http://www.putiancheng.net/");
        //XmlHelper.Update(xml, "/Root/Studio/Site[@Name='谱天城工作室']", "Name", "PuTianCheng Studio");
        //读取元素值
        //Response.Write("<div>" + XmlHelper.Read(xml, "/Root/Studio/Site/Master", "") + "</div>");
        //读取属性值
        //Response.Write("<div>" + XmlHelper.Read(xml, "/Root/Studio/Site", "Url") + "</div>");
        //读取特定属性值
        //Response.Write("<div>" + XmlHelper.Read(xml, "/Root/Studio/Site[@Name='丁香鱼工作室']", "Url") + "</div>");
        //删除属性
        //XmlHelper.Delete(xml, "/Root/Studio/Site[@Name='小路工作室']", "Url");
        //删除元素
        //XmlHelper.Delete(xml, "/Root/Studio", "");

    }
}

