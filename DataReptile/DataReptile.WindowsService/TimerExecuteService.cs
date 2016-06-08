using DataReptile.DataImport;
using DataReptile.WindowsService.Open;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DataReptile.WindowsService
{
    public partial class TimerExecuteService : ServiceBase
    {
        private Timer timer;
        private Dictionary<string, DateTime> serviceLastExecuteTime = new Dictionary<string, DateTime>();

        public TimerExecuteService()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
           

            this.timer = new Timer(5000);
            this.timer.Elapsed += timer_Elapsed;
            this.timer.Start();
        }
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            StartExecute();
        }
        public void StartExecute()
        {
           

            var targets = new Dictionary<string, IExecuteService>();
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
               

                if (!key.EndsWith("_WindowsService"))
                    continue;
                var value = ConfigurationManager.AppSettings[key].Split(';');
                var assembly = Assembly.LoadFile(value[1]);
                var target = assembly.CreateInstance(value[0]) as IExecuteService;
                if (target != null)
                    targets.Add(key, target);
            }

            foreach (var item in targets)
            {
                
                try
                {
                    
                    if(!this.serviceLastExecuteTime.ContainsKey(item.Key))
                    {
                       
                        this.serviceLastExecuteTime.Add(item.Key, DateTime.Now);
                        item.Value.Execute(DateTime.Now);
                    }
                    else if((DateTime.Now - this.serviceLastExecuteTime[item.Key]).TotalMilliseconds > item.Value.Interval)
                    {
                        this.serviceLastExecuteTime[item.Key] = DateTime.Now;
                        item.Value.Execute(DateTime.Now);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(ex.Message, "C:\\DataReptile\\Logs", "TimerExecuteService");

                }
            }
        }

        protected override void OnStop()
        {
            timer.Stop();
            timer.Close();
        }
    }
}
