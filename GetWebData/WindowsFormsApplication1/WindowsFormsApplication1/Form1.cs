using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string html =HttpHelper.HttpGet("http://www.cnblogs.com/");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            //获取文章列表
            var artlist = doc.DocumentNode.SelectNodes("//div[@class='post_item']");
            foreach (var item in artlist)
            {
                HtmlDocument adoc = new HtmlDocument();
                adoc.LoadHtml(item.InnerHtml);
                var html_a = adoc.DocumentNode.SelectSingleNode("//a[@class='titlelnk']");
                Response.Write(string.Format("标题为：{0}，链接为：{1}<br>", html_a.InnerText, html_a.Attributes["href"].Value));
            }

        }
    }
}
