using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Util;
using System.Net.Mail;
using System.Net;
/// <summary>
/// 邮件发送类
/// </summary>
    public class SMTP
    {
        string Sender = ConfigurationManager.AppSettings["Number"];//发送方邮箱
        string pwd=ConfigurationManager.AppSettings["Pwd"];//发送方密码
        string To;//接收方邮箱账号
        string SmtpServer = ConfigurationManager.AppSettings["Server"];//服务器
        /// <summary>
        /// 邮件发送初始化
        /// </summary>
        /// <param name="To"></param>
        public SMTP(string To)//第1个参数：接收方邮箱账号
        { 

            this.To = To;
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">标题</param>
        /// <param name="bodyinfo">内容</param>
        /// <returns></returns>
        public bool sendemail(string subject,string bodyinfo)
        {
            bool flag = false;
            string formto = Sender;
            string to = To;
            string content = subject;
            string body = bodyinfo;
            string name = Sender;
            string upass = pwd;
            string smtp = SmtpServer;
            SmtpClient _smtpClient = new SmtpClient();
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            _smtpClient.Host = smtp; //指定SMTP服务器
            _smtpClient.Credentials = new System.Net.NetworkCredential(name, upass);//用户名和密码
            MailMessage _mailMessage = new MailMessage();
            //发件人，发件人名 
            _mailMessage.From = new MailAddress(formto, "工艺品平台密码找回业务");
            //收件人 
            _mailMessage.To.Add(to);
            _mailMessage.SubjectEncoding = System.Text.Encoding.GetEncoding("gb2312");
            _mailMessage.Subject = content;//主题

            _mailMessage.Body = body;//内容
            _mailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("gb2312");//正文编码
            _mailMessage.IsBodyHtml = true;//设置为HTML格式
            _mailMessage.Priority = MailPriority.High;//优先级    
            _mailMessage.IsBodyHtml = true;
            try
            {
                _smtpClient.Send(_mailMessage);
                flag = true;
            }
            catch (Exception)
            {

                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 邮箱激活
        /// </summary>
        /// <param name="url"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool Activation( string url,string ID)
        {
            bool flag = false;
            string formto = Sender;
            string to = To;
            string content = "工艺品平台:";
            string body = "尊敬的"+ID+"用户:请点击些链接激活:";
            body += "<a href=" + url +">" + url + "</a>";
            string name = Sender;
            string upass = pwd;
            string smtp = SmtpServer;// "smtp.qq.com";
            SmtpClient _smtpClient = new SmtpClient();
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            _smtpClient.Host = smtp; //指定SMTP服务器
            _smtpClient.Credentials = new System.Net.NetworkCredential(name, upass);//用户名和密码
            MailMessage _mailMessage = new MailMessage();
            //发件人，发件人名 
            _mailMessage.From = new MailAddress(formto, "工艺品网站平台激活验证");
            //收件人 
            _mailMessage.To.Add(to);
            _mailMessage.SubjectEncoding = System.Text.Encoding.GetEncoding("gb2312");
            _mailMessage.Subject = content;//主题

            _mailMessage.Body = body;//内容
            _mailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("gb2312");//正文编码
            _mailMessage.IsBodyHtml = true;//设置为HTML格式
            _mailMessage.Priority = MailPriority.High;//优先级    
            _mailMessage.IsBodyHtml = true;
            try
            {
                _smtpClient.Send(_mailMessage);
                flag = true;
            }
            catch (Exception)
            {

                flag = false;
            }
            return flag;
        }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="subject"></param>
      /// <param name="body"></param>
        public void SendMail(string subject,string body)//第一个参数邮箱主题，第二个参数邮箱内容
        {
            MailAddress from = new MailAddress(this.Sender);

            MailAddress to = new MailAddress(this.To);

            MailMessage oMail = new MailMessage(from, to);
            oMail.Subject = subject;//主题
            oMail.Body = body;//内容
            
            oMail.IsBodyHtml = true;
            oMail.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");
            oMail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Host = SmtpServer; //指定邮件服务器
            client.Credentials = new NetworkCredential(this.Sender,this.pwd);//指定服务器邮件,及密码 


            //发送 
            try
            {
                client.Send(oMail); //发送邮件 

            }
            catch(Exception cp)
            {


                //发送不成功！

              
            }

            oMail.Dispose(); //释放资源 
        }









    }
