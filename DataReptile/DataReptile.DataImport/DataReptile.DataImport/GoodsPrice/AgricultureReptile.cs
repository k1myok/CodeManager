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

namespace DataReptile.DataImport
{
    public class AgricultureReptile : BaseExecuteService
    {
        public AgricultureReptile()
        {
            this.LogPath = "C:\\DataReptile\\Logs";
            this.LogKind = "GoodsPrice";
        }

        public override async Task<bool> Reptile()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CityService"].ConnectionString);
            var sqlText = string.Format("select * from AgricultureProduct where Createdt between  '{0}' and '{1}'", DateTime.Now.ToShortDateString(), DateTime.Now.AddDays(1).ToShortDateString());

            var adapter = new SqlDataAdapter(sqlText, connection);
            var builder = new SqlCommandBuilder(adapter);
            adapter.InsertCommand = builder.GetInsertCommand();
            var table = new DataTable();
            adapter.Fill(table);

            var page = 1;
            while (true)
            {
                if (!UpdateToTable(page, DateTime.Now, table))
                    break;
                page++;
            }

            if (table.GetChanges() != null && table.GetChanges().Rows.Count > 0)
                adapter.Update(table);

            return true;
        }

        public bool UpdateToTable(int page, DateTime dateTime, DataTable table)
        {
            var url = string.Format(
                "http://jiagesz.com/Ajax_Web/Ajax_Web.aspx?method=SearchProductList&page={0}&bdt={1}&edt={2}",
                page, dateTime.ToString("yyyy/MM/dd"), dateTime.ToString("yyyy/MM/dd"));

            var result = HttpHelper.GetHtmlEx(url, Encoding.UTF8);
            WriteLog(result);
            if (string.IsNullOrEmpty(result))
                return false;
            var json = JObject.Parse(result);
            if(json["total"].ToString() == "0" || json["data"].Count() == 0)
                return false;

            foreach (var item in json["data"])
            {
                if (table.AsEnumerable().Any(p => p["ID"].ToString() == item["ID"].ToString()))
                    continue;
                var row = table.NewRow();
                foreach (JProperty property in item)
                {
                    var value = property.Value as Newtonsoft.Json.Linq.JValue;
                    if (table.Columns.Contains(property.Name))
                        row[property.Name] = value.Type == JTokenType.Float ? value.Value : value.Value.ToString().Trim();
                }
                table.Rows.Add(row);
            }
            if (json["total"].ToString() == json["data"].Last()["rowid"].ToString())
                return false;

            return true;
        }

        public override int Interval
        {
            get { 
                return 3 * 60 * 60 * 1000; 
            }
        }
    }
}
