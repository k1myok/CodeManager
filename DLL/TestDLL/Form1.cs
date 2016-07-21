using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HttpHelp;

namespace TestDLL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var filepath = string.Empty;
            var path=@"D:CSDS\";
            var mes = "wefqergfewrkqlglweqglmklekgdfkgsfdglfdlsfdlklfsadl;fsdalksklfsdklfsdkleriklvmdfkvbaslkllkdskl";
            HttpHelp.DealWithFile.WtiteLog(path,mes);
         //   OpenFileDialog file = new OpenFileDialog();
         //   if (file.ShowDialog() == DialogResult.OK)
         //   {
         //       filepath = file.FileName;
            
         //   }
          
         //   //var result = HttpHelper.GetHtmlEx(url,Encoding.UTF8);
         //DealWithFile.DeleteFile(filepath);
          
        }
    }
}
