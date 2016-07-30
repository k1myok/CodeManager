﻿using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace YunLianLHelp
{
  public  class EmailHelp
    {
        public class Email
        {
            #region  --------------------字段--------------------

            private string _serviceType = "SMTP";
            private string _host;

            #endregion

            #region  --------------------属性--------------------

            /// <summary>
            /// 发送者邮箱
            /// </summary>
            public string From { get; set; }

            /// <summary>
            /// 接收者邮箱列表
            /// </summary>
            public string[] To { get; set; }

            /// <summary>
            /// 抄送者邮箱列表
            /// </summary>
            public string[] Cc { get; set; }

            /// <summary>
            /// 秘抄者邮箱列表
            /// </summary>
            public string[] Bcc { get; set; }

            /// <summary>
            /// 邮件主题
            /// </summary>
            public string Subject { get; set; }

            /// <summary>
            /// 邮件内容
            /// </summary>
            public string Body { get; set; }

            /// <summary>
            /// 是否是HTML格式
            /// </summary>
            public bool IsBodyHtml { get; set; }

            /// <summary>
            /// 附件列表
            /// </summary>
            public string[] Attachments { get; set; }

            /// <summary>
            /// 邮箱服务类型(Pop3,SMTP,IMAP,MAIL等)，默认为SMTP
            /// </summary>
            public string ServiceType
            {
                get { return _serviceType; }
                set { _serviceType = value; }
            }

            /// <summary>
            /// 邮箱服务器，如果没有定义邮箱服务器，则根据serviceType和Sender组成邮箱服务器
            /// </summary>
            //public string Host
            //{
            //    get
            //    {
            //        return string.IsNullOrEmpty(_host)
            //            ? (this.ServiceType + "." +
            //               Sender.Split("@".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1])
            //            : _host;
            //    }
            //    set { _host = value; }
            //}

            /// <summary>
            /// 邮箱账号(默认为发送者邮箱的账号)
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 邮箱密码(默认为发送者邮箱的密码)，默认格式GB2312
            /// </summary>
            public string Password { get; set; }

            /// <summary>
            /// 邮箱优先级
            /// </summary>
            public MailPriority MailPriority { get; set; }

            /// <summary>
            ///  邮件正文编码格式
            /// </summary>
            public Encoding Encoding { get; set; }

            #endregion

            #region  ------------------调用方法------------------

            /// <summary>
            /// 构造参数，发送邮件，使用方法备注：公开方法中调用
            /// </summary>
            public void Send()
            {
                var mailMessage = new MailMessage();

                //读取To  接收者邮箱列表
                if (this.To != null && this.To.Length > 0)
                {
                    foreach (string to in this.To)
                    {
                        if (string.IsNullOrEmpty(to)) continue;
                        try
                        {
                            mailMessage.To.Add(new MailAddress(to.Trim()));
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
                //读取Cc  抄送者邮件地址
                if (this.Cc != null && this.Cc.Length > 0)
                {
                    foreach (var cc in this.Cc)
                    {
                        if (string.IsNullOrEmpty(cc)) continue;
                        try
                        {
                            mailMessage.CC.Add(new MailAddress(cc.Trim()));
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
                //读取Attachments 邮件附件
                if (this.Attachments != null && this.Attachments.Length > 0)
                {
                    foreach (var attachment in this.Attachments)
                    {
                        if (string.IsNullOrEmpty(attachment)) continue;
                        try
                        {
                            mailMessage.Attachments.Add(new Attachment(attachment));
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
                //读取Bcc 秘抄人地址
                if (this.Bcc != null && this.Bcc.Length > 0)
                {
                    foreach (var bcc in this.Bcc)
                    {
                        if (string.IsNullOrEmpty(bcc)) continue;
                        try
                        {
                            mailMessage.Bcc.Add(new MailAddress(bcc.Trim()));
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
                //读取From 发送人地址
                try
                {
                    mailMessage.From = new MailAddress(this.From);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                //邮件标题
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                mailMessage.Subject = string.Format("?={0}?B?{1}?=", encoding.HeaderName,
                    Convert.ToBase64String(encoding.GetBytes(this.Subject), Base64FormattingOptions.None));
                //邮件正文是否为HTML格式
                mailMessage.IsBodyHtml = this.IsBodyHtml;
                //邮件正文
                mailMessage.Body = this.Body;
                mailMessage.BodyEncoding = this.Encoding;
                //邮件优先级
                mailMessage.Priority = this.MailPriority;

                //发送邮件代码实现
                var smtpClient = new SmtpClient
                {
                    // Host = this.Host,
                    Credentials = new NetworkCredential(this.UserName, this.Password)
                };
                //认证
                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            #endregion
        }

        /// <summary>
        ///邮件发送接口调用：bool isTrue=EmailInfo.SendEmail(参数,........);   参数解释参考方法
        /// <auther>
        ///   <name>Kencery</name>
        ///   <date>2015-8-18</date>
        /// </auther>
        /// </summary>
        public static class EmailInfo
        {
            /// <summary>
            /// 邮件发送方法，传递参数(使用中如出现问题，请调试)
            /// </summary>
            /// <param name="from">发送者邮箱名称(从配置文件中读取，比如：934532778@qq.com)(必填项)</param>
            /// <param name="to">接收者邮箱列表，可以传递多个，使用string[]表示(从配置文件中读取)(必填项)</param>
            /// <param name="cc">抄送者邮箱列表，可以传递多个，使用string[]表示(从配置文件中读取)</param>
            /// <param name="bcc">秘抄者邮箱列表，可以传递多个，使用string[]表示(从配置文件中读取)</param>
            /// <param name="subject">邮件主题，构造(必填项)</param>
            /// <param name="body">邮件内容，构造发送的邮件内容，可以发送网页(必填项)</param>
            /// <param name="isBodyHtml">是否是HTML格式，true为是，false为否</param>
            /// <param name="attachments">邮箱附件，可以传递多个，使用string[]表示(从配置文件中读取)，可空</param>
            /// <param name="host">邮箱服务器(从配置文件中读取，如：smtp@qq.com)(必填项)</param>
            /// <param name="password">邮箱密码(从配置文件中读取，from邮箱的密码)(必填项)</param>
            /// <returns>邮件发送成功，返回true,否则返回false</returns>
            public static bool SendEmail(string from, string[] to, string[] cc, string[] bcc, string subject, string body,
                bool isBodyHtml, string[] attachments, string host, string password)
            {
                //邮箱发送不满足，限制这些参数必须传递
                if (from == "" || to.Length <= 0 || subject == "" || body == "" || host == "" || password == "")
                {
                    return false;
                }

                var emil = new Email
                {
                    From = @from,
                    To = to,
                    Cc = cc,
                    Bcc = bcc,
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isBodyHtml,
                    Attachments = attachments,
                    //Host = host,
                    Password = password
                };
                try
                {
                    emil.Send();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
