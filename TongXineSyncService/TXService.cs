using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using DingTalk.Api;
using System.IO;
using System.Threading;

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
            if (Configuration.Load())
            {
                using (FileStream fs = new FileStream("log.txt", FileMode.Append))
                {
                    byte[] inBlock = Encoding.UTF8.GetBytes(Configuration.CorpSecret);
                    fs.Write(inBlock, 0, inBlock.Length);
                }
            }
            else
            {
                using (FileStream fs = new FileStream("log.txt", FileMode.Append))
                {
                    byte[] inBlock = Encoding.UTF8.GetBytes("abc");
                    fs.Write(inBlock, 0, inBlock.Length);
                }
            }
        }

        protected override void OnStop()
        {
        }
    }
}