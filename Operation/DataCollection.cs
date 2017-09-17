using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Net;
using System.Data;
using System.Threading;

using DataObject;
using Conf;
using DBTools;
using Newtonsoft.Json;
using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using Top.Api;

namespace Operation
{
    public class DataCollection
    {
        public static string DataFloder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        public static string ErrorFloder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Error");
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

        public static string GetDataPath(string folder, string fileName)
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder)))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder));
            }
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder, fileName);
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
        public static void GetScheduleList(string work_date, string corpid, string corpsecret, int offset, int size)
        {
            try
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
                using (FileStream fs = new FileStream(GetDataPath(string.Format(@"ScheduleListData{0}.json", DateTime.Now.ToString("yyyyMMddHHmmssfff"))), FileMode.Create))
                {
                    fs.Write(outBlock, 0, outBlock.Length);
                }
            }
            catch (Exception ex)
            {
                using (FileStream fs = new FileStream(Path.Combine(ErrorFloder, "error.log"), FileMode.Append))
                {
                    Message msg = new Message(ex.Message);
                    fs.Write(msg.bMsg, 0, msg.Length);
                }
            }
        }

        //获取考勤组
        public static void GetAttendGroup(string corpid, string corpsecret, int offset = 0, int size = 10)
        {
            try
            {
                GetToken(corpid, corpsecret);
                IDingTalkClient client = new DefaultDingTalkClient(AttendSimpleGroupURI, Constants.FORMAT_JSON);
                SmartworkAttendsGetsimplegroupsRequest req = new SmartworkAttendsGetsimplegroupsRequest();
                req.Offset = offset;
                req.Size = size;
                SmartworkAttendsGetsimplegroupsResponse rsp = client.Execute(req, _Token);
                string content = rsp.Body;
                byte[] outBlock = Encoding.UTF8.GetBytes(content);

                using (FileStream fs = new FileStream(GetDataPath(string.Format(@"AttendGroup{0}.json", DateTime.Now.ToString("yyyyMMddHHmmssfff"))), FileMode.Append))
                {
                    fs.Write(outBlock, 0, outBlock.Length);
                }
            }
            catch (Exception ex)
            {
                using (FileStream fs = new FileStream(Path.Combine(ErrorFloder, "error.log"), FileMode.Append))
                {
                    Message msg = new Message(ex.Message);
                    fs.Write(msg.bMsg, 0, msg.Length);
                }
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
            try
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

                using (FileStream fs = new FileStream(GetDataPath(string.Format(@"AttendResult{0}.json", DateTime.Now.ToString("yyyyMMddHHmmssfff"))), FileMode.Append))
                {
                    fs.Write(outBlock, 0, outBlock.Length);
                }
            }
            catch (Exception ex)
            {
                using (FileStream fs = new FileStream(Path.Combine(ErrorFloder, "error.log"), FileMode.Append))
                {
                    Message msg = new Message(ex.Message);
                    fs.Write(msg.bMsg, 0, msg.Length);
                }
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
        public static void GetCheckinResult(DateTime startTime, DateTime endTime, string corpid, string corpsecret)
        {
            try
            {
                GetToken(corpid, corpsecret);

                DateTime dt = new DateTime(1970, 1, 1, 8, 0, 0);
                TimeSpan startTimespan = startTime - dt;
                TimeSpan endTimespan = endTime - dt;

                byte[] outBlock;
                string url = string.Format(@"https://oapi.dingtalk.com/checkin/record?access_token={0}&department_id=1&start_time={1}&end_time={2}&offset=0&size=100&order=asc", _Token, (long)startTimespan.TotalMilliseconds, (long)endTimespan.TotalMilliseconds);
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    outBlock = Encoding.UTF8.GetBytes(sr.ReadToEnd());
                }

                //IDingTalkClient client = new DefaultDingTalkClient(CheckinRecordURI);
                //SmartworkCheckinRecordGetRequest req = new SmartworkCheckinRecordGetRequest
                //{
                //    UseridList = string.Join(",", userids),
                //    StartTime = startTimespan.Milliseconds,
                //    EndTime = endTimespan.Milliseconds,
                //    Cursor = cursor,
                //    Size = size
                //};
                //SmartworkCheckinRecordGetResponse response = client.Execute(req, _Token);
                //string content = response.Body;
                //byte[] outBlock = Encoding.UTF8.GetBytes(content);

                using (FileStream fs = new FileStream(GetDataPath(string.Format(@"CheckinResult{0}.json", DateTime.Now.ToString("yyyyMMddHHmmssfff"))), FileMode.Create))
                {
                    fs.Write(outBlock, 0, outBlock.Length);
                }
            }
            catch (Exception ex)
            {
                using (FileStream fs = new FileStream(Path.Combine(ErrorFloder, "error.log"), FileMode.Append))
                {
                    Message msg = new Message(ex.Message);
                    fs.Write(msg.bMsg, 0, msg.Length);
                }
            }
        }

        //获取审批数据
        public static void GetProcessInstanceList(string processCode, DateTime startTime, DateTime endTime, int cursor, int size, string corpid, string corpsecret)
        {
            try
            {
                GetToken(corpid, corpsecret);

                DateTime dt = new DateTime(1970, 1, 1, 8, 0, 0);
                TimeSpan startTimespan = startTime - dt;
                TimeSpan endTimespan = endTime - dt;

                IDingTalkClient client = new DefaultDingTalkClient(ProcessInstanceURI, Constants.FORMAT_JSON);
                SmartworkBpmsProcessinstanceListRequest request = new SmartworkBpmsProcessinstanceListRequest
                {
                    ProcessCode = processCode,
                    StartTime = (long)startTimespan.TotalMilliseconds,
                    EndTime = (long)endTimespan.TotalMilliseconds,
                    Cursor = cursor,
                    Size = size
                };

                SmartworkBpmsProcessinstanceListResponse response = client.Execute(request, _Token);
                string content = response.Body;
                byte[] outBlock = Encoding.UTF8.GetBytes(content);

                using (FileStream fs = new FileStream(GetDataPath(string.Format(@"ProcessInstance{0}.json", DateTime.Now.ToString("yyyyMMddHHmmssfff"))), FileMode.Create))
                {
                    fs.Write(outBlock, 0, outBlock.Length);
                }
            }
            catch (Exception ex)
            {
                using (FileStream fs = new FileStream(Path.Combine(ErrorFloder, "error.log"), FileMode.Append))
                {
                    Message msg = new Message(ex.Message);
                    fs.Write(msg.bMsg, 0, msg.Length);
                }
            }
        }

        //批量执行打卡记录、签到数据、、审批数据
        public static void GetMinuteResultCollection(object obj)
        {
            try
            {
                ProcessResultData();
                ProcessGroupData();
                ProcessScheudleListData();
                ProcessCheckinData();
                ProcessInstanceData();
            }
            catch (Exception ex)
            {
                using (FileStream fs = new FileStream(GetDataPath("Error", "error.log"), FileMode.Append))
                {
                    Message msg = new Message(ex.Message);
                    fs.Write(msg.bMsg, 0, msg.Length);
                }
            }
        }

        //批量执行排班记录
        public static void GetDayResultCollection(object obj)
        {
        }

        //从AttendGroup文件读数据到DB 并返回json分页标识
        public static bool ReadGroupJsonData()
        {
            //从Json文件反序列化考勤组对象
            AttendGroup group = null;
            string filePath = string.Empty;

            string[] files = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"));
            foreach (string file in files)
            {
                if (Path.GetFileName(file).StartsWith("AttendGroup"))
                {
                    filePath = file;
                    string data = File.ReadAllText(file);
                    group = JsonConvert.DeserializeObject<AttendGroup>(data);
                }
            }

            DataTable table = new DataTable("ding_kq_banzhi");
            table.Columns.Add("class_id");
            table.Columns.Add("class_name");
            table.Columns.Add("check_time_on");
            table.Columns.Add("check_time_off");
            table.Columns.Add("rest_begin_time");
            table.Columns.Add("rest_end_time");

            List<AttendGroupForTopVo> group_for_top_vo = group.dingtalk_smartwork_attends_getsimplegroups_response.result.result.groups.at_group_for_top_vo;

            foreach (AttendGroupForTopVo vo in group_for_top_vo)
            {
                foreach (AttendClassVo classvo in vo.selected_class.at_class_vo)
                {
                    DataRow dr = table.NewRow();
                    dr["class_id"] = classvo.class_id;
                    dr["class_name"] = classvo.class_name;
                    dr["check_time_on"] = classvo.sections.at_section_vo[0].times.at_time_vo[0].check_time;
                    dr["check_time_off"] = classvo.sections.at_section_vo[0].times.at_time_vo[1].check_time;
                    try
                    {
                        dr["rest_begin_time"] = classvo.setting.rest_begin_time.check_time;
                    }
                    catch (NullReferenceException ex)
                    {
                        dr["rest_begin_time"] = "1970-01-01 00:00:00";
                    }
                    try
                    {
                        dr["rest_end_time"] = classvo.setting.rest_end_time.check_time;
                    }
                    catch (NullReferenceException ex)
                    {
                        dr["rest_end_time"] = "1970-01-01 00:00:00";
                    }

                    table.Rows.Add(dr);
                }
            }

            //插入数据到数据库
            try
            {
                DBUtility db = new DBUtility();
                db.Insert(table);
                //移动数据文件至DataBak文件夹
                File.Move(filePath, GetDataPath("DataBak", Path.GetFileName(filePath)));
            }
            catch (Exception ex)
            {
                File.Move(filePath, GetDataPath("DataException", Path.GetFileName(filePath)));
                using (FileStream fs = new FileStream(GetDataPath("Error", "error.log"), FileMode.Append))
                {
                    Message msg = new Message(ex.Message);
                    fs.Write(msg.bMsg, 0, msg.Length);
                }
            }

            return group.dingtalk_smartwork_attends_getsimplegroups_response.result.result.has_more;
        }

        //从AttendResult文件读数据到DB 并返回json分页标识
        public static bool ReadResultJsonData()
        {
            //从Json文件反序列化考勤组对象
            AttendResult group = null;
            string filePath = string.Empty;

            string[] files = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"));
            foreach (string file in files)
            {
                if (Path.GetFileName(file).StartsWith("AttendResult"))
                {
                    filePath = file;
                    string data = File.ReadAllText(file);
                    group = JsonConvert.DeserializeObject<AttendResult>(data);
                }
            }

            DataTable table = new DataTable("ding_kq_source");
            table.Columns.Add("gmtModified");
            table.Columns.Add("baseCheckTime");
            table.Columns.Add("groupId");
            table.Columns.Add("timeResult");
            table.Columns.Add("classId");
            table.Columns.Add("workDate");
            table.Columns.Add("planId");
            table.Columns.Add("id");
            table.Columns.Add("checkType");
            table.Columns.Add("planCheckTime");
            table.Columns.Add("corpId");
            table.Columns.Add("locationResult");
            table.Columns.Add("isLegal");
            table.Columns.Add("gmtCreate");
            table.Columns.Add("userId");
            table.Columns.Add("userAddress");
            table.Columns.Add("sourceType");
            table.Columns.Add("userCheckTime");
            table.Columns.Add("locationMethod");

            List<AttendResultDetail> recordresult = group.recordresult;

            foreach (AttendResultDetail result in recordresult)
            {
                DataRow dr = table.NewRow();
                dr["gmtModified"] = result.gmtModified;
                dr["baseCheckTime"] = result.baseCheckTime;
                dr["groupId"] = result.groupId;
                dr["timeResult"] = result.timeResult;
                dr["classId"] = result.classId;
                dr["workDate"] = result.workDate;
                dr["planId"] = result.planId;
                dr["id"] = result.id;
                dr["checkType"] = result.checkType;
                dr["planCheckTime"] = result.planCheckTime;
                dr["corpId"] = result.corpId;
                dr["locationResult"] = result.locationResult;
                dr["isLegal"] = result.isLegal;
                dr["gmtCreate"] = result.gmtCreate;
                dr["userId"] = result.userId;
                dr["userAddress"] = result.userAddress;
                dr["sourceType"] = result.sourceType;
                dr["userCheckTime"] = result.userCheckTime;
                dr["locationMethod"] = result.locationMethod;

                table.Rows.Add(dr);
            }

            //插入数据到数据库
            try
            {
                DBUtility db = new DBUtility();
                db.Insert(table);
                //移动数据文件至DataBak文件夹
                File.Move(filePath, GetDataPath("DataBak", Path.GetFileName(filePath)));
            }
            catch (Exception ex)
            {
                File.Move(filePath, GetDataPath("DataException", Path.GetFileName(filePath)));
                using (FileStream fs = new FileStream(GetDataPath("Error", "error.log"), FileMode.Append))
                {
                    Message msg = new Message(ex.Message);
                    fs.Write(msg.bMsg, 0, msg.Length);
                }
            }

            return false;
        }

        //从ScheduleListData读取排班数据到DB 并返回json分页标识
        public static bool ReadScheduleListJsonData()
        {
            //从Json文件反序列化考勤组对象
            AttendListSchedule group = null;
            string filePath = string.Empty;

            string[] files = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"));
            foreach (string file in files)
            {
                if (Path.GetFileName(file).StartsWith("ScheduleList"))
                {
                    filePath = file;
                    string data = File.ReadAllText(file);
                    group = JsonConvert.DeserializeObject<AttendListSchedule>(data);
                }
            }

            DataTable table = new DataTable("ding_kq_paiban");
            table.Columns.Add("check_type");
            table.Columns.Add("class_id");
            table.Columns.Add("group_id");
            table.Columns.Add("class_setting_id");
            table.Columns.Add("plan_check_time");
            table.Columns.Add("userid");

            List<ResultDetail> recordresult = group.dingtalk_smartwork_attends_listschedule_response.result.result.schedules.at_schedule_for_top_vo;

            foreach (ResultDetail result in recordresult)
            {
                DataRow dr = table.NewRow();
                dr["check_type"] = result.check_type;
                dr["class_id"] = result.class_id;
                dr["group_id"] = result.group_id;
                dr["class_setting_id"] = result.class_setting_id;
                dr["plan_check_time"] = result.plan_check_time;
                dr["userid"] = result.userid;

                table.Rows.Add(dr);
            }

            //插入数据到数据库
            try
            {
                DBUtility db = new DBUtility();
                db.Insert(table);
                //移动数据文件至DataBak文件夹
                File.Move(filePath, GetDataPath("DataBak", Path.GetFileName(filePath)));
            }
            catch (Exception ex)
            {
                File.Move(filePath, GetDataPath("DataException", Path.GetFileName(filePath)));
                using (FileStream fs = new FileStream(GetDataPath("Error", "error.log"), FileMode.Append))
                {
                    Message msg = new Message(ex.Message);
                    fs.Write(msg.bMsg, 0, msg.Length);
                }
            }

            return group.dingtalk_smartwork_attends_listschedule_response.result.result.has_more;
        }

        //从CheckinResult读取签到记录到到DB
        public static bool ReadCheckinResultJsonData()
        {
            HTTPCheckinResult result = null;
            string filePath = string.Empty;

            string[] files = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"));
            foreach (string file in files)
            {
                if (Path.GetFileName(file).StartsWith("CheckinResult"))
                {
                    filePath = file;
                    string data = File.ReadAllText(file);
                    result = JsonConvert.DeserializeObject<HTTPCheckinResult>(data);
                }
            }

            DataTable table = new DataTable("ding_kq_checkin");

            table.Columns.Add("userId");
            table.Columns.Add("name");
            table.Columns.Add("timestamp");
            table.Columns.Add("remark");
            table.Columns.Add("place");

            foreach (HTTPCheckinDetail detail in result.data)
            {
                DataRow dr = table.NewRow();
                dr["userId"] = detail.userId;
                dr["name"] = detail.name;
                dr["timestamp"] = detail.timestamp;
                dr["remark"] = detail.remark;
                dr["place"] = detail.place;

                table.Rows.Add(dr);
            }
            //插入数据到数据库
            try
            {
                DBUtility db = new DBUtility();
                db.Insert(table);
                //移动数据文件至DataBak文件夹
                File.Move(filePath, GetDataPath("DataBak", Path.GetFileName(filePath)));
            }
            catch (Exception ex)
            {
                File.Move(filePath, GetDataPath("DataException", Path.GetFileName(filePath)));
                using (FileStream fs = new FileStream(GetDataPath("Error", "error.log"), FileMode.Append))
                {
                    Message msg = new Message(ex.Message);
                    fs.Write(msg.bMsg, 0, msg.Length);
                }
            }
            return false;
        }

        //从ProcessInstance读取审批记录到DB
        public static int ReadProcessInstanceJsonData()
        {
            int hasMore = 0;
            ProcessInstance group = null;
            string filePath = string.Empty;

            string[] files = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"));
            foreach (string file in files)
            {
                if (Path.GetFileName(file).StartsWith("ProcessInstance"))
                {
                    filePath = file;
                    string data = File.ReadAllText(file);
                    group = JsonConvert.DeserializeObject<ProcessInstance>(data);
                }
            }

            DataTable table = new DataTable("ding_kq_process_instance");
            table.Columns.Add("process_instance_id");
            table.Columns.Add("title");
            table.Columns.Add("create_time");
            table.Columns.Add("finish_time");
            table.Columns.Add("originator_userid");
            table.Columns.Add("originator_dept_id");
            table.Columns.Add("status");
            table.Columns.Add("form_vo_name");
            table.Columns.Add("form_vo_value");
            table.Columns.Add("process_instance_result");

            List<ResultList> resultlist = group.dingtalk_smartwork_bpms_processinstance_list_response.result.result.list.process_instance_top_vo;

            if (resultlist == null)
            {
                File.Move(filePath, GetDataPath("DataException", Path.GetFileName(filePath)));
                return 0;
            }

            foreach (ResultList result in resultlist)
            {
                DataRow dr = table.NewRow();
                dr["process_instance_id"] = result.process_instance_id;
                dr["title"] = result.title;
                dr["create_time"] = result.create_time;
                dr["finish_time"] = result.finish_time;
                dr["originator_userid"] = result.originator_userid;
                dr["originator_dept_id"] = result.originator_dept_id;
                dr["status"] = result.status;
                dr["form_vo_name"] = result.form_component_values.form_component_value_vo[0].name;
                dr["form_vo_value"] = result.form_component_values.form_component_value_vo[0].value;
                dr["process_instance_result"] = result.process_instance_result;

                table.Rows.Add(dr);
            }
            //插入数据到数据库
            try
            {
                DBUtility db = new DBUtility();
                db.Insert(table);
                //移动数据文件至DataBak文件夹
                File.Move(filePath, GetDataPath("DataBak", Path.GetFileName(filePath)));
            }
            catch (Exception ex)
            {
                File.Move(filePath, GetDataPath("DataException", Path.GetFileName(filePath)));
                using (FileStream fs = new FileStream(GetDataPath("Error", "error.log"), FileMode.Append))
                {
                    Message msg = new Message(ex.Message);
                    fs.Write(msg.bMsg, 0, msg.Length);
                }
            }

            try
            {
                hasMore = group.dingtalk_smartwork_bpms_processinstance_list_response.result.result.next_cursor;
            }
            catch (NullReferenceException ex)
            {
                hasMore = 0;
            }

            return hasMore;
        }

        #region 数据文件处理

        //处理考勤组数据
        public static void ProcessGroupData()
        {
            int offset = 0;
            GetAttendGroup(Configuration.CorpID, Configuration.CorpSecret, offset);
            while (ReadGroupJsonData())
            {
                offset += 10;
                GetAttendGroup(Configuration.CorpID, Configuration.CorpSecret, offset);
            }
        }

        //处理考勤原始记录
        public static void ProcessResultData()
        {
            try
            {
                DBUtility db = new DBUtility();
                //查询在职及近7天离职的员工工号
                //DataTable dt = db.Select("zlemployee", new string[] { "code" }, "abs(datediff(day,isnull(lzdate,getdate()),getdate()))<=7");
                DataTable dt = db.Select("zlemployee", new string[] { "code" });
                List<string> userids = new List<string>();

                foreach (DataRow dr in dt.Rows)
                {
                    userids.Add(dr["code"].ToString());
                }

                AttendRequestBody attendReqBody = new AttendRequestBody();
                attendReqBody.checkDateFrom = DateTime.Now.AddHours(0 - Configuration.TimeRange).ToString("yyyy-MM-dd HH:mm:ss");
                attendReqBody.checkDateTo = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                if (userids.Count <= 50)
                {
                    //员工数一次最多不能超过50
                    attendReqBody.userIds = userids.GetRange(0, userids.Count);
                    string reqBody = JsonConvert.SerializeObject(attendReqBody);
                    GetAttendResult(reqBody, Configuration.CorpID, Configuration.CorpSecret);
                    ReadResultJsonData();
                }
                else
                {
                    int cycleCount = userids.Count / 50;
                    int tailNum = userids.Count % 50;
                    int seed = 0;

                    for (int index = 0; index < cycleCount; index++)
                    {
                        //员工数一次最多不能超过50
                        attendReqBody.userIds = userids.GetRange(seed * 50, 50);
                        string reqBody = JsonConvert.SerializeObject(attendReqBody);
                        GetAttendResult(reqBody, Configuration.CorpID, Configuration.CorpSecret);
                        ReadResultJsonData();

                        seed++;
                    }
                    if (tailNum > 0)
                    {
                        attendReqBody.userIds = userids.GetRange(seed * 50, userids.Count - seed * 50);
                        string reqBody = JsonConvert.SerializeObject(attendReqBody);
                        GetAttendResult(reqBody, Configuration.CorpID, Configuration.CorpSecret);
                        ReadResultJsonData();
                    }
                }
            }
            catch (Exception ex)
            {
                using (FileStream fs = new FileStream(GetDataPath("Error", "error.log"), FileMode.Append))
                {
                    Message msg = new Message(ex.Message);
                    fs.Write(msg.bMsg, 0, msg.Length);
                }
            }
        }

        //处理全量员工排班信息
        public static void ProcessScheudleListData()
        {
            DateTime today = DateTime.Now;
            for (int index = 0 - Configuration.ScheduleRange; index <= Configuration.ScheduleRange; index++)
            {
                DateTime work_date = today.AddDays(index);
                int offset = 0;
                GetScheduleList(work_date.ToString("yyyy-MM-dd HH:mm:ss"), Configuration.CorpID, Configuration.CorpSecret, 0, 200);
                while (ReadScheduleListJsonData())
                {
                    offset += 200;
                    GetScheduleList(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Configuration.CorpID, Configuration.CorpSecret, offset, 200);
                }
            }
        }

        //处理签到记录
        public static void ProcessCheckinData()
        {
            GetCheckinResult(DateTime.Now.AddDays(-30), DateTime.Now, Configuration.CorpID, Configuration.CorpSecret);
            ReadCheckinResultJsonData();
        }

        //处理审批记录
        public static void ProcessInstanceData()
        {
            foreach (string processCode in Configuration.ProcessCodes)
            {
                GetProcessInstanceList(processCode, DateTime.Now.AddDays(0 - Configuration.ProcessRange), DateTime.Now, 0, 10, Configuration.CorpID, Configuration.CorpSecret);
                int cursor = ReadProcessInstanceJsonData();
                while (cursor > 0)
                {
                    GetProcessInstanceList(processCode, DateTime.Now.AddDays(0 - Configuration.ProcessRange), DateTime.Now, cursor, 10, Configuration.CorpID, Configuration.CorpSecret);
                    cursor = ReadProcessInstanceJsonData();
                }
            }
        }

        #endregion 数据文件处理
    }

    internal class Message
    {
        public byte[] bMsg { get; set; }
        public int Length { get; private set; }

        public Message(string ex)
        {
            bMsg = Encoding.UTF8.GetBytes(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + ex + "\r\n");
            Length = bMsg.Length;
        }
    }
}