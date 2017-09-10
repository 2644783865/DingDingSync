using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log_Easy;
using System.IO;
using Conf;

namespace TongXineSyncService
{
    internal class Loger
    {
        public static string LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log\\log.txt");

        public static clsLogger log = new clsLogger(GetLogPath());

        private static string GetLogPath()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log"));
            }
            if (!File.Exists(LogPath))
            {
                var logfile = File.Create(LogPath);
                logfile.Close();
            }
            return LogPath;
        }
    }
}