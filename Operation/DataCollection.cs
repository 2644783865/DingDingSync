using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Net;

using DataObject;
using Newtonsoft.Json;
using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using Top.Api;
using Conf;

namespace Operation
{
    public class DataCollection
    {
        public static string DataFloder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        public static string AttendListScheduleURI = "https://eco.taobao.com/router/rest";
        public static string AttendSimpleGroupURI = "https://eco.taobao.com/router/rest";
        public static string AttendRecordURI = "https://oapi.dingtalk.com/attendance/listRecord?access_token=";
        public static string CheckinRecordURI = "https://eco.taobao.com/router/rest";
        public static string ProcessInstaceURI = "https://eco.taobao.com/router/rest";

        public static string TokenUrl = "https://oapi.dingtalk.com/gettoken?";
        private static DateTime Flag = new DateTime(2000, 01, 01, 0, 0, 0);

        private static string _Token;
        private static string _corpid;
        private static string _corpsecret;

        //创建数据存放目录
        private static string GetDataPath(string fileName)
        {
            if (!Directory.Exists(DataFloder))
            {
                Directory.CreateDirectory(DataFloder);
            }
            return Path.Combine(DataFloder, fileName);
        }

        //Token有效检查
        private static bool CheckToken()
        {
            bool retVal = false;
            DateTime curDateTime = DateTime.Now;
            TimeSpan ts = curDateTime - Flag;

            if (!string.IsNullOrEmpty(_Token))
            {
                if (ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes <= 7000)
                {
                    retVal = true;
                }
            }
            else if (Configuration.Load())
            {
                _corpid = Configuration.CorpID;
                _corpsecret = Configuration.CorpSecret;
            }
            return retVal;
        }

        //获取钉钉Token
        public static string GetToken()
        {
            if (!CheckToken())
            {
                string jsonStr = string.Empty;
                string url = TokenUrl + string.Format("corpid={0}&corpsecret={1}", _corpid, _corpsecret);

                HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
                req.Timeout = 5000;
                req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.101 Safari/537.36";

                using (HttpWebResponse rsp = req.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader sr = new StreamReader(rsp.GetResponseStream()))
                    {
                        jsonStr = sr.ReadToEnd();
                    }
                }
                Token token = JsonConvert.DeserializeObject<Token>(jsonStr);
                _Token = token.access_token;
            }
            return _Token;
        }

        //获取排班
        public static void GetScheduleList(string work_date, int offset, int size)
        {
            IDingTalkClient client = new DefaultDingTalkClient(AttendListScheduleURI, Constants.FORMAT_JSON);
            SmartworkAttendsListscheduleRequest req = new SmartworkAttendsListscheduleRequest
            {
                WorkDate = DateTime.Parse(work_date),
                Offset = offset,
                Size = size
            };

            SmartworkAttendsListscheduleResponse rsp = client.Execute(req, GetToken());
            string content = rsp.Body;
            using (FileStream fs = new FileStream(GetDataPath(string.Format(@"ScheduleListData{0}.json", DateTime.Now.ToString("yyyyMMdd"))), FileMode.Append))
            {
                fs.Write(Encoding.UTF8.GetBytes(content), 0, content.Length);
            }
        }

        //获取考勤组
        public static void GetAttendGroup()
        {
            IDingTalkClient client = new DefaultDingTalkClient(AttendSimpleGroupURI, Constants.FORMAT_JSON);
            SmartworkAttendsGetsimplegroupsRequest req = new SmartworkAttendsGetsimplegroupsRequest();
            SmartworkAttendsGetsimplegroupsResponse rsp = client.Execute(req, GetToken());
            string content = rsp.Body;
            using (FileStream fs = new FileStream(GetDataPath(string.Format(@"AttendGroup{0}.json", DateTime.Now.ToString("yyyyMMdd"))), FileMode.Create))
            {
                fs.Write(Encoding.UTF8.GetBytes(content), 0, content.Length);
            }
        }

        //获取打卡记录
        public static void GetAttendResult()
        {
            string data = string.Empty;
            string url = AttendRecordURI + GetToken();
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(Encoding.UTF8.GetBytes(data), 0, data.Length);
        }

        //获取签到数据
        public static void GetCheckinResult() { }

        //获取审批数据
        public static void GetProcessInstanceList() { }

        //批量执行打卡记录、签到数据、、审批数据
        public void GetMinuteResultCollection() { }

        //批量执行排班记录
        public void GetDayResultCollection() { }
    }

    //internal class Token
    //{
    //    private int _errcode;
    //    private string _errmsg;
    //    private string _access_token;

    //    public int errorcode
    //    {
    //        get
    //        {
    //            return this._errcode;
    //        }
    //        set
    //        {
    //            this._errcode = value;
    //        }
    //    }

    //    public string errmsg
    //    {
    //        get
    //        {
    //            return this._errmsg;
    //        }
    //        set
    //        {
    //            this._errmsg = value;
    //        }
    //    }

    //    public string access_token
    //    {
    //        get
    //        {
    //            return this._access_token;
    //        }
    //        set
    //        {
    //            this._access_token = value;
    //        }
    //    }
    //}
}