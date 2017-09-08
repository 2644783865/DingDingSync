using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Threading;
using Log_Easy;
using Operation;

namespace TongXineSyncService
{
    public partial class TXService : ServiceBase
    {
        public TXService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Loger.log.Info(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t启动服务");
            DataCollection.GetScheduleList("2017-09-01 10:00:00", 0, 200);
        }

        protected override void OnStop()
        {
        }
    }
}