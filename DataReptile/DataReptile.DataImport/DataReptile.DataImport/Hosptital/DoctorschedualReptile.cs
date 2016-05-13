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
using DataReptile.DataImport.Hosptital;


namespace DataReptile.DataImport
{
    public class Doctorschedual:BaseExecuteService
    {
        public Doctorschedual()
        {
            this.LogPath = "C:\\DataReptile\\Logs";
            this.LogKind = "DoctorSchedual";
        }
        public async Task<bool>Reptile()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CityService"].ConnectionString);
            connection.Open();
            var command = new SqlCommand("select HospName from Hospital", connection);
            SqlDataReader rd = command.ExecuteReader();
            var hospitalName = string.Empty;
            while (rd.Read())
            {
                hospitalName = rd[0].ToString();
                HandSchedual(hospitalName);
            }

            connection.Close();
            return true;
        }

        private async void HandSchedual(string hospitalName,int timeCount=1)
        {
            if (timeCount > 5)
                return;
            var xmlData = GetSchedualList(hospitalName);
            this.WreteLog(xmlData);
            var result = ConvertToNodes(xmlData);
            if (result == null || result.Count == 0)
                this.WreteLog("将XMLdata 转换为XML Nodes时结果为空！");
            var shedual = UpdateToDB(hospitalName, result);
            this.WreteLog("医生排班信息更新到数据库结果为：" + hospitalName + "-" + shedual);
        }

        public bool GetHospitallist()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CityService"].ConnectionString);
            connection.Open();
            var command = new SqlCommand("select HospName from Hospital", connection);
            SqlDataReader rd = command.ExecuteReader();
            var hospitalName = string.Empty;
            while (rd.Read())
            {
                hospitalName = rd[0].ToString();
                var xmlData = GetSchedualList(hospitalName);
                var result = ConvertToNodes(xmlData);
                var shedual = UpdateToDB(hospitalName,result);
           }

            connection.Close();
            return true;
        }
        public string GetSchedualList(string hospitalName)
        {
            var BeginDate = DateTime.Now.ToString("yyyy-MM-dd");
            var EndDate = DateTime.Now.AddDays(+7).ToString("yyyy-MM-dd");
            var fileName = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resource\Template\\hospitalInfo_req.txt");
            var content = System.IO.File.ReadAllText(fileName);
            var args = string.Format(content, "SZYunLian", "YunLian1156", "GetSchedualList", hospitalName, "", "", BeginDate,EndDate, "");
            var result = HttpHelper.GetHtmlExByByPost("http://plat.jssz12320.cn:8001/szregplat/yuyue.wsdl", args, Encoding.UTF8);
            return result;
        }

        public List<XElement> ConvertToNodes(string xmlData)
        {
            if (xmlData != null)
            {
                XDocument document = XDocument.Parse(xmlData);
                var bodyNode = document.Root.Element(XName.Get("Body", document.Root.Name.NamespaceName));
                var responseNode = bodyNode.Element(XName.Get("GetHospInfoRsp", "http://new.webservice.namespace"));
                var hospitalDocNode = responseNode.Element(XName.Get("Hospital", "http://new.webservice.namespace"));
                var shedual = hospitalDocNode.Elements(XName.Get("Depart", "http://new.webservice.namespace"));
                return shedual.ToList();
            }
            else
            { return null; }
        }
        public bool UpdateToDB(string hospitalName, List<XElement> departNodes,int timeCount=1)
        {
            if(timeCount>5)
                return false;
            if (departNodes == null || departNodes.Count == 0)
                return false;
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CityService"].ConnectionString);
            var adapter = new SqlDataAdapter("select top 1 * from schedual where 0>1 ", connection);
            var table = new DataTable();
            adapter.Fill(table);
            foreach (var depart in departNodes)
            { 
                //普通科室
                var shedualNodes = depart.Elements(XName.Get("Schedual", "http://new.webservice.namespace"));
                if (shedualNodes != null && shedualNodes.Count() > 0)
                {
                    foreach (XElement schedual in shedualNodes)
                    {
                        var row = table.NewRow();
                        row["HospName"] = hospitalName;
                        row["DepartName"] = depart.Element(XName.Get("DepartName", "http://new.webservice.namespace")).Value;
                        foreach (XElement shedualDetail in schedual.Elements())
                        {
                            var xmlName = shedualDetail.Name.LocalName;
                            if (table.Columns.Contains(xmlName))
                                row[xmlName] = shedualDetail.Value;
                        }
                        table.Rows.Add(row);
                    }
                }
                //专家科室
                var shedualDocNodes = depart.Elements(XName.Get("Doctor", "http://new.webservice.namespace"));
                if (shedualDocNodes != null && shedualDocNodes.Count() > 0)
                {
                    foreach (XElement doc in shedualDocNodes)
                    {
                        
                        var doctorSchedualNodes = doc.Elements(XName.Get("Schedual", "http://new.webservice.namespace"));
                        if (doctorSchedualNodes != null && doctorSchedualNodes.Count() > 0)
                        {
                                foreach (XElement doctorSchedualNode in doctorSchedualNodes)
                                {
                                    var row = table.NewRow();
                                    row["HospName"] = hospitalName;
                                    row["DepartName"] = depart.Element(XName.Get("DepartName", "http://new.webservice.namespace")).Value;
                                    row["DoctorName"] = doc.Element(XName.Get("DoctorName", "http://new.webservice.namespace")).Value;
                                    foreach (XElement doctorShedualField in doctorSchedualNode.Elements())
                                    {
                                        var xmlName = doctorShedualField.Name.LocalName;
                                        if (table.Columns.Contains(xmlName))
                                        {
                                            row[xmlName] = doctorShedualField.Value;
                                        }
                                    }
                                    table.Rows.Add(row);

                                }
                        } 
                    }
                }
            }

            var command = new SqlCommand("delete from  schedual where Hospname='" + hospitalName + "'", connection);
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            if (command.ExecuteNonQuery() > -1)
            {
                var builder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = builder.GetInsertCommand();
                if (adapter.Update(table) > 0)
                {
                    return true;
                }
                else
                    return UpdateToDB(hospitalName, departNodes,timeCount + 1);
            }
            else
            return false;

        }
        private void WreteLog(string log)
        {
            LogHelper.WriteLog(log, "C:\\DataReptile\\Logs", "DocotSchedual");
        }
    }
}
