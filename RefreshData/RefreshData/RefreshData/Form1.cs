using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RefreshData;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;
namespace RefreshData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = HttpHelper.GetHtmlExByByPost("http://www.bazhuayu.com/", "", Encoding.UTF8);
            if (result != null)
            {
                var sql="data source=.;initial catalog=RefreshData;user id=yang;pwd=kenyang123!@#";
                var connection = new SqlConnection(sql);
                var adapter = new SqlDataAdapter("select top 1 * from SinaData where 0>1", connection);
                var table = new DataTable();
                adapter.Fill(table);
                XElement temp = XElement.Parse(result);
                var tempuserid = temp.Descendants("title").FirstOrDefault().Value;
                
              
                var itemrow = temp.Descendants("item").ToList();
                foreach (var list in itemrow)
                {
                    var row = table.NewRow();
                    row["userid"] = tempuserid;
                    row["title"] = list.Element("title").Value;
                    row["description"] = list.Element("description").Value;
                    row["pubDate"] =Convert.ToDateTime(list.Element("pubDate").Value);
                    row["guid"]= list.Element("guid").Value;
                    row ["link"] = list.Element("link").Value;
                    row["createDate"] = System.DateTime.Now;
                    table.Rows.Add(row);
                }

                var command = new SqlCommand("delete from SinaData where 0>1", connection);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                if (command.ExecuteNonQuery() > -1)
                {
                    var builder = new SqlCommandBuilder(adapter);
                    adapter.InsertCommand = builder.GetInsertCommand();
                    adapter.Update(table);
                 
                }

            }

        }
    }
}
