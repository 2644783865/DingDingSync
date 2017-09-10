using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Net;
using System.Data;

using DataObject;
using Conf;
using DBTools;
using Newtonsoft.Json;
using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using Top.Api;

//using Conf;

namespace Operation
{
    public class DataCollection
    {
        public static string DataFloder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        public static string AttendListScheduleURI = "https://eco.taobao.com/router/rest";
        public static string AttendSimpleGroupURI = "https://eco.taobao.com/router/rest";
        public static string AttendRecordURI = "https://oapi.dingtalk.com/attendance/listRecord?access_token=";
        public static string CheckinRecordURI = "https://eco.taobao.com/router/rest";
        public static string ProcessInstanceURI = "https://eco.taobao.com/router/rest";

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
        private static bool CheckToken(string corpid, string corpsecret)
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
            else
            {
                _corpid = corpid;
                _corpsecret = corpsecret;
            }
            return retVal;
        }

        //获取钉钉Token
        public static string GetToken(string coprid, string corpsecret)
        {
            if (!CheckToken(coprid, corpsecret))
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
        public static void GetScheduleList(string work_date, int offset, int size, string corpid, string corpsecret)
        {
            GetToken(corpid, corpsecret);
            IDingTalkClient client = new DefaultDingTalkClient(AttendListScheduleURI, Constants.FORMAT_JSON);
            SmartworkAttendsListscheduleRequest req = new SmartworkAttendsListscheduleRequest
            {
                WorkDate = DateTime.Parse(work_date),
                Offset = offset,
                Size = size
            };

            SmartworkAttendsListscheduleResponse rsp = client.Execute(req, _Token);
            string content = rsp.Body;
            byte[] outBlock = Encoding.UTF8.GetBytes(content);
            using (FileStream fs = new FileStream(GetDataPath(string.Format(@"ScheduleListData{0}.json", DateTime.Now.ToString("yyyyMMddHHmmss"))), FileMode.Create))
            {
                fs.Write(outBlock, 0, outBlock.Length);
            }
        }

        //获取考勤组
        public static void GetAttendGroup(string corpid, string corpsecret)
        {
            GetToken(corpid, corpsecret);
            IDingTalkClient client = new DefaultDingTalkClient(AttendSimpleGroupURI, Constants.FORMAT_JSON);
            SmartworkAttendsGetsimplegroupsRequest req = new SmartworkAttendsGetsimplegroupsRequest();
            SmartworkAttendsGetsimplegroupsResponse rsp = client.Execute(req, _Token);
            string content = rsp.Body;
            byte[] outBlock = Encoding.UTF8.GetBytes(content);

            using (FileStream fs = new FileStream(GetDataPath(string.Format(@"AttendGroup{0}.json", DateTime.Now.ToString("yyyyMMddHHmmss"))), FileMode.Create))
            {
                fs.Write(outBlock, 0, outBlock.Length);
            }
        }

        /// <summary>
        /// 获取全员指定时段考勤记录
        /// </summary>
        /// <param name="reqBody">{"userids":["12345","23456"],"checkDateFrom":"2017-01-01 12:00:00","checkDateTo":"2017-01-01 14:00:00"} 一次请求userIds最多50人</param>
        /// <param name="corpid"></param>
        /// <param name="corpsecret"></param>
        //获取全部员工打卡记录
        public static void GetAttendResult(string reqBody, string corpid, string corpsecret)
        {
            GetToken(corpid, corpsecret);
            byte[] outBlock;
            byte[] inBlock = Encoding.UTF8.GetBytes(reqBody);
            string url = AttendRecordURI + _Token;
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(inBlock, 0, inBlock.Length);
            using (HttpWebResponse rsp = request.GetResponse() as HttpWebResponse)
            {
                using (StreamReader sr = new StreamReader(rsp.GetResponseStream()))
                {
                    outBlock = Encoding.UTF8.GetBytes(sr.ReadToEnd());
                }
            }

            using (FileStream fs = new FileStream(GetDataPath(string.Format(@"AttendResult{0}.json", DateTime.Now.ToString("yyyyMMddHHmmss"))), FileMode.Create))
            {
                fs.Write(outBlock, 0, outBlock.Length);
            }
        }

        /// <summary>
        /// 获取签卡记录
        /// </summary>
        /// <param name="userids">工号List</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间：一个人数据最大为10天，多个人数据最大为1天</param>
        /// <param name="corpid"></param>
        /// <param name="corpsecret"></param>
        /// <param name="cursor">分页游标</param>
        /// <param name="size">分页数据大小 最大100</param>
        //获取签到数据
        public static void GetCheckinResult(List<string> userids, DateTime startTime, DateTime endTime, int cursor, int size, string corpid, string corpsecret)
        {
            GetToken(corpid, corpsecret);

            DateTime dt = new DateTime(1970, 1, 1, 8, 0, 0);
            TimeSpan startTimespan = startTime - dt;
            TimeSpan endTimespan = endTime - dt;

            IDingTalkClient client = new DefaultDingTalkClient(CheckinRecordURI);
            SmartworkCheckinRecordGetRequest req = new SmartworkCheckinRecordGetRequest
            {
                UseridList = string.Join(",", userids),
                StartTime = startTimespan.Milliseconds,
                EndTime = endTimespan.Milliseconds,
                Cursor = cursor,
                Size = size
            };
            SmartworkCheckinRecordGetResponse response = client.Execute(req, _Token);
            string content = response.Body;
            byte[] outBlock = Encoding.UTF8.GetBytes(content);
            using (FileStream fs = new FileStream(GetDataPath(string.Format(@"CheckinResult{0}.json", DateTime.Now.ToString("yyyyMMddHHmmss"))), FileMode.Create))
            {
                fs.Write(outBlock, 0, outBlock.Length);
            }
        }

        //获取审批数据
        public static void GetProcessInstanceList(string processCode, DateTime startTime, DateTime endTime, int cursor, int size, string corpid, string corpsecret)
        {
            GetToken(corpid, corpsecret);

            DateTime dt = new DateTime(1970, 1, 1, 8, 0, 0);
            TimeSpan startTimepspan = startTime - dt;
            TimeSpan endTimespan = endTime - dt;

            IDingTalkClient client = new DefaultDingTalkClient(ProcessInstanceURI);
            SmartworkBpmsProcessinstanceListRequest request = new SmartworkBpmsProcessinstanceListRequest
            {
                ProcessCode = processCode,
                StartTime = startTimepspan.Milliseconds,
                EndTime = endTimespan.Milliseconds,
                Cursor = cursor,
                Size = size
            };

            SmartworkBpmsProcessinstanceListResponse response = client.Execute(request, _Token);
            string content = response.Body;
            byte[] outBlock = Encoding.UTF8.GetBytes(content);

            using (FileStream fs = new FileStream(GetDataPath(string.Format(@"ProcessInstance{0}.json", DateTime.Now.ToString("yyyyMMddHHmmss"))), FileMode.Create))
            {
                fs.Write(outBlock, 0, outBlock.Length);
            }
        }

        //批量执行打卡记录、签到数据、、审批数据
        public static void GetMinuteResultCollection(object obj)
        {
            DBUtility db = DBUtility.Create();
            DataTable dt = db.Select("zlemployee", new string[] { "code" });
            List<string> userids = new List<string>();

            foreach (DataRow dr in dt.Rows)
            {
                userids.Add(dr["code"].ToString());
            }

            AttendRequestBody attendReqBody = new AttendRequestBody
            {
                userIds = userids.GetRange(0, 10),
                checkDateFrom = "2017-09-01 00:00:00",
                checkDateTo = "2017-09-01 23:59:59"
            };
            string reqBody = JsonConvert.SerializeObject(attendReqBody);

            GetAttendResult(reqBody, Configuration.CorpID, Configuration.CorpSecret);
            GetAttendGroup(Configuration.CorpID, Configuration.CorpSecret);
            GetScheduleList("2017-09-01", 0, 200, Configuration.CorpID, Configuration.CorpSecret);
        }

        //批量执行排班记录
        public static void GetDayResultCollection() { }
    }
}