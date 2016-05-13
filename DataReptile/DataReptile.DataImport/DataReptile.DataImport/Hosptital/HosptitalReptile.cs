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


namespace DataReptile.DataImport
{
    public class HosptitalReptile : BaseExecuteService
    {
        public HosptitalReptile()
        {
            this.LogPath = "C:\\DataReptile\\Logs";
            this.LogKind = "Hosptital";
        }

        public override async Task<bool> Reptile()
        {
            try
            {
                var xmlData = this.GetHospitials();
                this.WriteLog(xmlData);
                var nodes = this.FindHosptitalNodes(xmlData);
                if (nodes == null || nodes.Count == 0)
                {
                    this.WriteLog("将XMLdata 转换为XML Nodes时结果为空！");
                }
                var result = this.UpdateToDB(nodes);
                this.WriteLog("医院信息更新到数据库结果为：" + result);
                if (result)
                {
                    new HosptitalDocReptile().Reptile();
                    new HosptitalDepartmentsReptile().Reptile();
                    new Doctorschedual().Reptile();
                }
                return result;

            }
            catch (Exception ex)
            {
                this.WriteLog(ex.Message);
                return false;
            }
        }

        public string GetHospitials()
        {
            var fileName = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resource\Template\\hospitalInfo_req.txt");
            var content = System.IO.File.ReadAllText(fileName);
            var args = string.Format(content, "SZYunLian", "YunLian1156", "GetHospBasic", "", "", "", "", "", "");
            var result = HttpHelper.GetHtmlExByByPost("http://plat.jssz12320.cn:8001/szregplat/yuyue.wsdl", args, Encoding.UTF8);
            return result;
        }

        public List<XElement> FindHosptitalNodes(string xmlData)
        {
            XDocument document = XDocument.Parse(xmlData);
            var bodyNode = document.Root.Element(XName.Get("Body", document.Root.Name.NamespaceName));
            var responseNode = bodyNode.Element(XName.Get("GetHospInfoRsp", "http://new.webservice.namespace"));
            var hospitalNodes = responseNode.Elements(XName.Get("Hospital", "http://new.webservice.namespace"));
            return hospitalNodes.ToList();
        }
        /// <summary>
        ///主要功能是获取医院的信息，更新到公司的数据库Hospital； 
        /// </summary>
        /// <param name="hosptitalNodes">返回医院信息列表，格式为XML</param>
        public bool UpdateToDB(List<XElement> hosptitalNodes)
        {
            if (hosptitalNodes == null || hosptitalNodes.Count == 0)
                return false;
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CityService"].ConnectionString);

            var adapter = new SqlDataAdapter("select top 1 * from Hospital where 0>1", connection);
            var table = new DataTable();
            adapter.Fill(table);
            //遍历hospitalNode 并且获取各个节点的 值并存储到DataTabel 的row中
            //使用的类是system.xml.Linq;
            foreach (var hospital in hosptitalNodes)
            {
                var row = table.NewRow();
                foreach (DataColumn column in table.Columns)
                {
                    if (column.ColumnName == "HospName")
                        row[column] = hospital.Element(XName.Get("HospName", "http://new.webservice.namespace")).Value;
                    else
                        row[column] = hospital.Element(XName.Get("HospBasic", "http://new.webservice.namespace")).Element(XName.Get(column.ColumnName, "http://new.webservice.namespace")).Value;
                }
                table.Rows.Add(row);
            }
            var command = new SqlCommand("delete from Hospital", connection);
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            if (command.ExecuteNonQuery() > -1)
            {
                var builder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = builder.GetInsertCommand();
                if (adapter.Update(table) > 0)
                {
                    interval = 1 * 60 * 60 * 1000;
                    return true;
                }
                else
                    interval = 60 * 1000;
            }
            else
                interval = 1 * 60 * 60 * 1000;

            return false;
        }

        public int interval = 1* 60 * 60 * 1000;
        public override int Interval
        {
            get { return interval; }
        }
    }
}
