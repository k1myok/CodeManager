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
//using DataReptile.DataImport.Hosptital;

namespace DataReptile.DataImport
{
    public class HosptitalDepartmentsReptile:BaseExecuteService
    {
        public HosptitalDepartmentsReptile()
        {
            this.LogPath = "C:\\DataReptile\\Logs";
            this.LogKind = "Department";
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
                HandleDepart(hospitalName);
            }
            connection.Close();
            return true;
        }

        private async void HandleDepart(string hospitalName, int timeCount = 1)
        {
            if (timeCount > 5)
                return;
            try
            {
                var xmlData = GetHospitialDepartments(hospitalName);
                //this.WreteLog(xmlData);
                var result = ConvertToNodes(xmlData);
                if (result == null || result.Count == 0)
                    this.WriteLog("将XMLdata 转换为XML Nodes时结果为空！");
                var Departresult = this.UpdateToDB(hospitalName, result);
                this.WreteLog("医院科室更新到数据库结果为:" + hospitalName + "-" + Departresult);
            }
            catch (Exception ex)
            {
                WreteLog("HosptitalDepartmentsReptile:" + hospitalName + ":" + ex.Message);
                HandleDepart(hospitalName, timeCount + 1);
            }

        }
        public bool DepartmentList()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CityService"].ConnectionString);
            connection.Open();
            var command = new SqlCommand("select HospName from Hospital", connection);
            SqlDataReader rd = command.ExecuteReader();
            var hospitalName = string.Empty;
            while (rd.Read())
            {
                hospitalName = rd[0].ToString();
                var xmlData = GetHospitialDepartments(hospitalName);
                var result = this.ConvertToNodes(xmlData);
                if (result == null || result.Count == 0)
                    this.WreteLog("将XMLdata 转换为XML Nodes时结果为空！");
                var shedual =this.UpdateToDB(hospitalName, result);
                this.WreteLog("医生科室信息更新到数据库结果为：" + hospitalName + "-" + shedual);
            }
            connection.Close();
            return true;
        }

        public string GetHospitialDepartments(string hospitalName)
        {
            var fileName = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Resource\Template\\hospitalInfo_req.txt");
            var content = System.IO.File.ReadAllText(fileName);
            var args = string.Format(content, "SZYunLian", "YunLian1156", "GetHospDepartBasic", hospitalName, "", "", "", "", "");
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
                var department = hospitalDocNode.Elements(XName.Get("Depart", "http://new.webservice.namespace"));
                return department.ToList();
           }
            else
           { return null; }
        }

        public bool UpdateToDB(string hospitalName, List<XElement> departmentsNodes, int timeCount = 1)
        {
            if (timeCount > 5)
                return false;
            if (departmentsNodes == null || departmentsNodes.Count == 0)
                return false;
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CityService"].ConnectionString);
            var adapter = new SqlDataAdapter("select top 1 * from Depart where 0>1 ", connection);
            var table = new DataTable();
            adapter.Fill(table);
            foreach (var department in departmentsNodes)
            {
                var row = table.NewRow();
                row["HospName"] = hospitalName;
                row["DepartName"] = department.Element(XName.Get("DepartName", "http://new.webservice.namespace")).Value;

                var basicNode = department.Element(XName.Get("DepartBasic", "http://new.webservice.namespace"));
                if (basicNode != null)
                {
                    foreach (XElement element in basicNode.Elements())
                    {
                        var xmlName = element.Name.LocalName;
                        if (table.Columns.Contains(xmlName))
                            row[xmlName] = element.Value;
                    }
                }

                table.Rows.Add(row);
            }
            var command = new SqlCommand("delete from depart where HospName='"+ hospitalName +"'",connection);
            if(connection.State == ConnectionState.Closed)
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
                    return UpdateToDB(hospitalName,departmentsNodes,timeCount+1);
            }
            else
            return false;
        }
        private void WreteLog(string log)
        {
            LogHelper.WriteLog(log, "C:\\DataReptile\\Logs", "Department");
        }
    }
}
