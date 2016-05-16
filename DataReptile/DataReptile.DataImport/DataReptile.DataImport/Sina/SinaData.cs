using DataReptile.WindowsService.Open;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Oracle.DataAccess;
using Oracle.ManagedDataAccess;
using System.Data.OracleClient;



namespace DataReptile.DataImport
{
    public class SinaData: BaseExecuteService
    {
        public SinaData()
        {
            this.LogPath = "C:\\DataReptile\\Logs";
            this.LogKind = "SinaData";
        }
        public override async Task<bool> Reptile()
        {
            var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString);
            connection.Open();
            var command = new OracleCommand("select USERID from TB_SINAUSER ", connection);
           OracleDataReader rd = command.ExecuteReader();
            var UserID = string.Empty;
            while (rd.Read())
            {
                UserID = rd[0].ToString();
                var result=UpdarteToDB(UserID);
                this.WriteLog("新浪微博数据抓取：" + UserID + "-" + result);
                //var result = this.ConvertToNodes(xmlData);
                //if (result == null || result.Count == 0)
                //    this.WreteLog("将XMLdata 转换为XML Nodes时结果为空！");
                //var shedual = this.UpdateToDB(hospitalName, result);
                //this.WreteLog("医生科室信息更新到数据库结果为：" + hospitalName + "-" + shedual);
            }
            connection.Close();
            return true;
           
        }
        public bool UpdarteToDB(string userId)
        {
                var url = "http://rss.weibodangan.com/weibo/rss/" + userId + "/";
                var result = HttpHelper.GetHtmlExByByPost(url, "", Encoding.UTF8);
                if (result != null)
                {
                    var sql = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString);
                    sql.Open();
                    //var  = "data source=.;initial catalog=RefreshData;user id=yang;pwd=kenyang123!@#";
                    var adapter = new OracleDataAdapter("select * from TB_WEIBOINFO where 0>1", sql);
                    var table = new DataTable();
                    adapter.Fill(table);
                    XElement temp = XElement.Parse(result);
                    var tempuserid = temp.Descendants("title").FirstOrDefault().Value;
                    var itemrow = temp.Descendants("item").ToList();
                    foreach (var list in itemrow)
                    {
                        var row = table.NewRow();
                        var guid = list.Element("guid").Value;

                        row["userid"] = tempuserid;
                        row["title"] = list.Element("title").Value;
                        row["description"] = list.Element("description").Value;
                        row["pubDate"] = Convert.ToDateTime(list.Element("pubDate").Value);
                        row["guid"] = guid;
                        row["link"] = list.Element("link").Value;
                        row["createDate"] = System.DateTime.Now;
                        var tempsql = "select count(*) from TB_WEIBOINFO where guid='" + guid + "'";
                        var cmd = new OracleCommand(tempsql, sql);
                        var No =Convert.ToInt32(cmd.ExecuteScalar());
                        if (No<1)
                        {
                            table.Rows.Add(row);
                        }
                        else
                        {
                            this.WriteLog("该条信息已经存入数据库：" + list.Element("title").Value);
                        }
                    }
                    var command = new OracleCommand("delete from TB_WEIBOINFO  where 0>1", sql);
                    if (sql.State == ConnectionState.Closed)
                        sql.Open();
                    if (command.ExecuteNonQuery() > -1)
                    {
                       var builder = new OracleCommandBuilder(adapter);
                        adapter.InsertCommand = builder.GetInsertCommand();
                        if (adapter.Update(table) > 0)
                        {

                            interval = 1*60*60*1000;
                            return true;
                        }
                        else
                            interval = 5 * 60 * 1000;
                        return false;
                    } 
                }
                interval = 10 * 60 * 1000;
                return false;

        }



        public int interval = 1*60*60*1000;
        public override int Interval
        {
            get { return interval; }
        }
    }
}
