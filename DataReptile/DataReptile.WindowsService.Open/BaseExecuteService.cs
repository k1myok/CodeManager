using DataReptile.DataImport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReptile.WindowsService.Open
{
    public class BaseExecuteService : IExecuteService
    {
        public virtual async Task Execute(DateTime dateTime)
        {
            WriteLog(string.Format("开始时间:{0}--------------->", dateTime));
            try
            {
                this.Reptile();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        public virtual async Task<bool> Reptile()
        {
            return false;
        }

        public virtual int Interval
        {
            get {
                return 1000;
            }
        }

        protected void WriteLog(string logMessage)
        {
            LogHelper.WriteLog(logMessage, this.LogPath, this.LogKind);
        }

        public string LogKind { get; set; }
        public string LogPath { get; set; }
    }
}
