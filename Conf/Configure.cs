using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Conf
{
    public class Configuration
    {
        public static string BaseDir = AppDomain.CurrentDomain.BaseDirectory;

        public static string config_file = Path.Combine(BaseDir, "ServiceSetting.xml");

        #region 配置参数

        //钉钉corpid,corpsecret,token

        private static string _corpid;
        private static string _corpsecret;
        //private static string _token;

        //本地服务器连接参数

        private static string _server;
        private static string _user;
        private static string _password;
        private static string _db;
        private static string _connectionString;

        //同步内容参数

        private static bool _schedule;         //考勤排班记录
        private static bool _attendenceSource; //考勤打卡记录
        private static bool _attendenceSign;   //考勤签到记录
        private static bool _auditResult;      //审批记录

        //同步时间参数

        private static DateTime _startDate; //开始时间
        private static int _interval; //间隔时间分钟

        #region 属性

        public static string CorpID
        {
            get { return _corpid; }
            set { _corpid = value; }
        }

        public static string CorpSecret
        {
            get { return _corpsecret; }
            set { _corpsecret = value; }
        }

        public static string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        public static string User
        {
            get { return _user; }
            set { _user = value; }
        }

        public static string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public static string DB
        {
            get { return _db; }
            set { _db = value; }
        }

        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public static bool Schedule
        {
            get { return _schedule; }
            set { _schedule = value; }
        }

        public static bool AttendenceSource
        {
            get { return _attendenceSource; }
            set { _attendenceSource = value; }
        }

        public static bool AttendenceSign
        {
            get { return _attendenceSign; }
            set { _attendenceSign = value; }
        }

        public static bool AuditResult
        {
            get { return _auditResult; }
            set { _auditResult = value; }
        }

        public static DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public static int Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        #endregion 属性

        #endregion 配置参数

        //参数加载
        public static bool Load()
        {
            bool retVal = false;
            XmlDocument xmlDoc = CreateXmlReader();
            if (xmlDoc != null)
            {
                try
                {
                    //企业CorpID CorpSecret
                    CorpID = DesHelper.DesHelper.DecryptString(GetSingleAttr(xmlDoc, "root/remote/corpid[1]"));
                    CorpSecret = DesHelper.DesHelper.DecryptString(GetSingleAttr(xmlDoc, "root/remote/corpsecret[1]"));

                    //本地数据库
                    Server = GetSingleAttr(xmlDoc, "root/localConnection/server[1]");
                    User = GetSingleAttr(xmlDoc, "root/localConnection/user[1]");
                    Password = DesHelper.DesHelper.DecryptString(GetSingleAttr(xmlDoc, "root/localConnection/password[1]"));
                    DB = GetSingleAttr(xmlDoc, "root/localConnection/database[1]");
                    ConnectionString = DesHelper.DesHelper.DecryptString(GetSingleAttr(xmlDoc, "root/localConnection/connectionString[1]"));

                    //同步内容选项
                    Schedule = GetSingleAttr(xmlDoc, "root/remote/apis/schedule[1]") == "1" ? true : false;
                    AttendenceSource = GetSingleAttr(xmlDoc, "root/remote/apis/attendenceSource[1]") == "1" ? true : false;
                    AttendenceSign = GetSingleAttr(xmlDoc, "root/remote/apis/attendenceSign[1]") == "1" ? true : false;
                    AuditResult = GetSingleAttr(xmlDoc, "root/remote/apis/auditResult[1]") == "1" ? true : false;

                    //同步时间选项
                    StartDate = DateTime.Parse(string.IsNullOrEmpty(GetSingleAttr(xmlDoc, "root/syncParam/startTime[1]")) ? DateTime.Now.ToString() : GetSingleAttr(xmlDoc, "root/syncParam/startTime[1]"));
                    Interval = Convert.ToInt16(GetSingleAttr(xmlDoc, "root/syncParam/interval[1]"));
                    retVal = true;
                }
                catch (Exception e)
                {
                    retVal = false;
                }
            }
            return retVal;
        }

        private static string GetSingleAttr(XmlDocument xmlDoc, string nodeName, string attr = "value")
        {
            XmlNode node = xmlDoc.SelectSingleNode(nodeName);
            string retVal = ((XmlElement)node).GetAttribute(attr);
            return retVal;
        }

        private static XmlDocument CreateXmlReader()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlReaderSettings readSettings = new XmlReaderSettings()
            {
                IgnoreComments = true
            };
            XmlReader reader = null;
            try
            {
                reader = XmlReader.Create(config_file);
                xmlDoc.Load(reader);
            }
            catch (Exception e)
            {
                xmlDoc = null;
                return xmlDoc;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return xmlDoc;
        }
    }
}