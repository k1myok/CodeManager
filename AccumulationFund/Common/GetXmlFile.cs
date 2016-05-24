using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelectProvidentFundService.Common
{
    public class GetXmlFile
    {
        /// <summary>
        /// 生成xml报文
        /// </summary>
        /// <param name="code"></param>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <param name="idCard"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string setXML(string code, string uid, string pwd, string idCard, string id)
        {
            var dt = DateTime.Now.ToString("yyyyMMddHHmm");
            var number = RandomNumber.GetRandom(1, 99999, 2);
            var reqserialno = code + dt + number[0].ToString() + number[1].ToString();

            var args = string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                <request>
	                <meta>
		                <channeltype>016</channeltype>
		                <reqserialno>{0}</reqserialno>
		                <trancode>{1}</trancode>
		                <sourceid>16</sourceid>
                        <userid>{2}</userid>
                        <mac>{3}</mac>
	                </meta>
	                <data>
		                <channeltype>016</channeltype>
		                <custacno>{4}</custacno>
		                <PaperKind>A</PaperKind>
		                <PaperId>{5}</PaperId>
	                </data>
                </request>
                ", reqserialno, code, uid, pwd, id, idCard);

            return args;
        }

        public static string setXML5(string code, string uid, string pwd, string idCard, string id, string page, string total, string pages)
        {
            var dt = DateTime.Now.ToString("yyyyMMddHHmm");
            var number = RandomNumber.GetRandom(1, 99999, 2);
            var reqserialno = code + dt + number[0].ToString() + number[1].ToString();

            var args = string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                <request>
	                <meta>
		                <channeltype>016</channeltype>
		                <reqserialno>{0}</reqserialno>
		                <trancode>{1}</trancode>
		                <sourceid>16</sourceid>
                        <userid>{2}</userid>
                        <mac>{3}</mac>
	                </meta>
	                <data>
		                <channeltype>016</channeltype>
                        <s_page>{4}</s_page>
                        <s_total>{5}</s_total>
                        <s_pages>{6}</s_pages>
		                <custacno>{7}</custacno>
		                <PaperKind>A</PaperKind>
		                <PaperId>{8}</PaperId>
	                </data>
                </request>
                ", reqserialno, code, uid, pwd, page, total, pages, id, idCard);

            return args;
        }
        /// <summary>
        /// 生成XML报文，使用J005L4接口
        /// </summary>
        /// <param name="code"></param>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <param name="idCard"></param>
        /// <returns></returns>
        public static string setXML2(string code, string uid, string pwd, string idCard)
        {
            var dt = DateTime.Now.ToString("yyyyMMddHHmm");
            var number = RandomNumber.GetRandom(1, 99999, 2);
            var reqserialno = code + dt + number[0].ToString() + number[1].ToString();

            var args = string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                <request>
	                <meta>
		                <channeltype>016</channeltype>
		                <reqserialno>{0}</reqserialno>
		                <trancode>{1}</trancode>
		                <sourceid>16</sourceid>
                        <userid>{2}</userid>
                        <mac>{3}</mac>
	                </meta>
	                <data>
		                <channeltype>016</channeltype>
		                <PaperKind>A</PaperKind>
		                <PaperId>{4}</PaperId>
	                </data>
                </request>
                ", reqserialno, code, uid, pwd, idCard);

            return args;
        }

        /// <summary>
        /// 生成xml报文，使用J021L1接口
        /// </summary>
        /// <param name="code"></param>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <param name="loanNum"></param>
        /// <returns></returns>
        public static string setXML3(string code, string uid, string pwd, string loanNum, string page, string total, string pages)
        {
            var dt = DateTime.Now.ToString("yyyyMMddHHmm");
            var number = RandomNumber.GetRandom(1, 99999, 2);
            var reqserialno = code + dt + number[0].ToString() + number[1].ToString();

            var args = string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                <request>
	                <meta>
		                <channeltype>016</channeltype>
		                <reqserialno>{0}</reqserialno>
		                <trancode>{1}</trancode>
		                <sourceid>16</sourceid>
                        <userid>{2}</userid>
                        <mac>{3}</mac>
	                </meta>
	                <data>
		                <channeltype>016</channeltype>
                        <s_page>{4}</s_page>
                        <s_total>{5}</s_total>
                        <s_pages>{6}</s_pages>
		                <LoanAcNo>{7}</LoanAcNo>
	                </data>
                </request>
                ", reqserialno, code, uid, pwd, page, total, pages, loanNum);

            return args;
        }

        /// <summary>
        /// CustAcNo PaperId mobile msgcheckcode
        /// </summary>
        /// <param name="code"></param>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <param name="loanNum"></param>
        /// <returns></returns>
        public static string setXML4(string code, string uid, string pwd, string custAcNo, string paperId, string mobile, string msgcheckcode)
        {
            var dt = DateTime.Now.ToString("yyyyMMddHHmm");
            var number = RandomNumber.GetRandom(1, 99999, 2);
            var reqserialno = code + dt + number[0].ToString() + number[1].ToString();

            var args = string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                <request>
	                <meta>
		                <channeltype>016</channeltype>
		                <reqserialno>{0}</reqserialno>
		                <trancode>{1}</trancode>
		                <sourceid>16</sourceid>
                        <userid>{2}</userid>
                        <mac>{3}</mac>
	                </meta>
	                <data>
		                <channeltype>016</channeltype>
		                <CustAcNo>{4}</CustAcNo>
		                <PaperId>{5}</PaperId>
		                <mobile>{6}</mobile>
		                <msgcheckcode>{7}</msgcheckcode>
	                </data>
                </request>
                ", reqserialno, code, uid, pwd, custAcNo, paperId, mobile, msgcheckcode);

            return args;
        }
        public static string setXML6(string code, string uid,string pwd, string idCard )
        {
            var dt = DateTime.Now.ToString("yyyyMMddHHmm");
            var number = RandomNumber.GetRandom(1, 99999, 2);
            var reqserialno = code + dt + number[0].ToString() + number[1].ToString();
            var args = string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                <request>
	                <meta>
		                <channeltype>016</channeltype>
		                <reqserialno>{0}</reqserialno>
		                <trancode>{1}</trancode>
		                <sourceid>16</sourceid>
                        <userid>{2}</userid>
                        <mac>{3}</mac>
	                </meta>
	                <data>
		                <channeltype>016</channeltype>
		                <PaperId>{4}</PaperId>
	                </data>
                </request>
                ", reqserialno,code,uid,pwd,idCard);
            return args;
        }
    }
}
