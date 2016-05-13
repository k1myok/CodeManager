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

    public class HosptitalDocReptile:BaseExecuteService
    {
        public HosptitalDocReptile()
        {
            this.LogPath = "C:\\DataReptile\\Logs";
            this.LogKind = "Doctor";
        }
        public async Task<bool> Reptile()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CityService"].ConnectionString);
            connection.Open();
            var command = new SqlCommand("select HospName from Hospital", connection);
            SqlDataReader rd = command.ExecuteReader();
            var hospitalName = string.Empty;
            while (rd.Read())
            {
                var hosptitalName = rd[0].ToString();
                HandleItem(hosptitalName);
            }
            connection.Close();
            return true;
        }

        private async void HandleItem(string hospitalName, int timeCount = 1)
        {
            if (timeCount > 5)
                return;
            try
            {
                var xmlData = this.GetHospitialsDoc(hospitalName);
                this.WreteLog(xmlData);
                var result = this.ConvertToNodes(xmlData);
                if (result == null || result.Count == 0)
                    this.WreteLog("将XMLdata 转换为XML Nodes时结果为空！");
                var shedual = this.UpdateToDB(hospitalName, result);
                this.WreteLog("医生科室信息更新到数据库结果为：" + hospitalName + "-" + shedual);
            }
            catch (Exception ex)
            {
                WreteLog("HosptitalDocReptile:" + hospitalName + ":" + ex.Message);
                HandleItem(hospitalName, timeCount + 1);
            }
        }

        public bool GetDoctorlist()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CityService"].ConnectionString);
            connection.Open();
            var command = new SqlCommand("select HospName from Hospital", connection);
            SqlDataReader rd = command.ExecuteReader();
            var hospitalName = string.Empty;
            while (rd.Read())
            {
                hospitalName = rd[0].ToString();
                var xmlData =this.GetHospitialsDoc(hospitalName);
                var result =this.ConvertToNodes(xmlData);
                var isSuccess = this.UpdateToDB(hospitalName, result);
            }
            connection.Close();
            return true;
        }

        public string GetHospitialsDoc(string hospitalName)
        {
            var BeginDate = DateTime.Now.ToString("yyyy-MM-dd");
            var EndDate = DateTime.Now.AddDays(+7).ToString("yyyy-MM-dd");
            var fileName = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resource\Template\\hospitalInfo_req.txt");
            var content = System.IO.File.ReadAllText(fileName);
            var args = string.Format(content, "SZYunLian", "YunLian1156", "GetHospDocBasic", hospitalName, "", "", BeginDate, EndDate, "");
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
                var hospitalDepartNode = responseNode.Element(XName.Get("Hospital", "http://new.webservice.namespace"));
                var doctor = hospitalDepartNode.Elements(XName.Get("Depart", "http://new.webservice.namespace"));
                return doctor.ToList();
            }
            else
            { 
                return null; 
            }
        }
     
        public bool UpdateToDB(string hospitalName, List<XElement> departments, int timeCount=1)
        {
            if (timeCount > 5)
                return false;

            if (departments == null || departments.Count == 0)
                return false;
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CityService"].ConnectionString);
            var adapter = new SqlDataAdapter("select top 1 * from doctor where 0>1 ", connection);
            var table = new DataTable();
            adapter.Fill(table);
            foreach (var department in departments)
            {
                var departName = department.Element(XName.Get("DepartName", "http://new.webservice.namespace")).Value;

                var doctorsNodes = department.Elements(XName.Get("Doctor", "http://new.webservice.namespace"));
                if (doctorsNodes == null || doctorsNodes.Count() == 0)
                {
                    var row = table.NewRow();
                    row["HospName"] = hospitalName;
                    row["DepartName"] = departName;
                    table.Rows.Add(row);
                }
                else
                {
                    foreach (var doctorNode in doctorsNodes)
                    {
                        var doctorRow = table.NewRow();
                        doctorRow["HospName"] = hospitalName;
                        doctorRow["DepartName"] = departName;
                        doctorRow["DoctorName"] = doctorNode.Element(XName.Get("DoctorName", "http://new.webservice.namespace")).Value;
                        var doctorbasicNode = doctorNode.Element(XName.Get("DocBasic", "http://new.webservice.namespace"));
                        if (doctorbasicNode != null)
                        {
                            foreach (XElement element in doctorbasicNode.Elements())
                            {
                                var xmlName = element.Name.LocalName;
                                if (table.Columns.Contains(xmlName))
                                    doctorRow[xmlName] = element.Value;
                            }
                        }
                        table.Rows.Add(doctorRow);
                    }
                }
            }
            var command = new SqlCommand("delete from doctor where HospName='" + hospitalName + "'", connection);
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
            }
            else
                return UpdateToDB(hospitalName, departments, timeCount + 1);
            return false;
        }

        private void WreteLog(string log)
        {
            LogHelper.WriteLog(log, "C:\\DataReptile\\Logs", "Doctor");
        }
    }
}
