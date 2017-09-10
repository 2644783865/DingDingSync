using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace DataObject
{
    public class Token
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string access_token { get; set; }
    }

    //考勤打卡记录请求包对象
    public class AttendRequestBody
    {
        public List<string> userIds { get; set; } //企业内的员工id列表，最多不能超过50个
        public string checkDateFrom { get; set; } // 开始时间 yyyy-MM-dd hh:mm:ss
        public string checkDateTo { get; set; }   //结束时间 yyyy-MM-dd hh:mm:ss
    }

    #region 排班详情

    /* 排班返回JSON示例
     {
    "dingtalk_smartwork_attends_listschedule_response":{
        "result":{
            "ding_open_errcode":0,
            "error_msg":"ok",
            "success":true,
            "result":{
                "has_more":true,
                "schedules":{
                    "at_schedule_for_top_vo":[
                        {
                            "group_id":1,
                            "plan_check_time":"2017-04-11 11:11:11",
                            "class_id":1,
                            "userid":"00001",
                            "check_type":"OnDuty",
                            "class_setting_id":1,
                            "approve_id":1
                        }
                    ]
                }
            }
        }
    }
}

     */

    public class AttendListSchedule
    {
        [JsonProperty(PropertyName = "dingtalk_smartwork_attends_listschedule_response")]
        public AttendResponseResult RespResult { get; set; }
    }

    public class AttendResponseResult
    {
        [JsonProperty(PropertyName = "ding_open_errcode")]
        public int ErrorCode { get; set; }

        [JsonProperty(PropertyName = "err_msg")]
        public string ErrMsg { get; set; }

        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "result")]
        public AttentResult Result { get; set; }
    }

    public class AttentResult
    {
        [JsonProperty(PropertyName = "has_more")]
        public bool Hasmore { get; set; }

        [JsonProperty(PropertyName = "schedules")]
        public AttendSchedules Schedules { get; set; }
    }

    public class AttendSchedules
    {
        [JsonProperty(PropertyName = "at_schedule_for_top_vo")]
        public List<ResultDetail> ScheduleList { get; set; }
    }

    public class ResultDetail
    {
        [JsonProperty(PropertyName = "group_id")]
        public int GoupID { get; set; }

        [JsonProperty(PropertyName = "plan_check_time")]
        public string PlanCheckTime { get; set; }

        [JsonProperty(PropertyName = "class_id")]
        public int ClassID { get; set; }

        [JsonProperty(PropertyName = "userid")]
        public string UserID { get; set; }

        [JsonProperty(PropertyName = "check_type")]
        public string CheckType { get; set; }

        [JsonProperty(PropertyName = "class_setting_id")]
        public int ClassSettingID { get; set; }

        [JsonProperty(PropertyName = "approve_id")]
        public int ApproveID { get; set; }
    }

    #endregion 排班详情

    #region 考勤组

    //{
    //    "dingtalk_smartwork_attends_getsimplegroups_response":{
    //        "result":{
    //            "ding_open_errcode":0,
    //            "error_msg":"ok",
    //            "success":true,
    //            "result":{
    //                "has_more":true,
    //                "groups":{
    //                    "at_group_for_top_vo":[
    //                        {
    //                            "group_id":20015047,
    //                            "is_default":false,
    //                            "group_name":"固定排班",
    //                            "selected_class":{
    //                                "at_class_vo":[
    //                                    {
    //                                        "setting":{
    //                                            "class_setting_id":1,
    //                                            "rest_begin_time":{
    //                                                "check_time":"2017-04-11 11:11:11"
    //                                            },
    //                                            "permit_late_minutes":10,
    //                                            "work_time_minutes":-1,
    //                                            "rest_end_time":{
    //                                                "check_time":"2017-04-11 11:11:11"
    //                                            },
    //                                            "absenteeism_late_minutes":30,
    //                                            "serious_late_minutes":20,
    //                                            "is_off_duty_free_check":"Y"
    //                                        },
    //                                        "class_id":20008010,
    //                                        "sections":{
    //                                            "at_section_vo":[
    //                                                {
    //                                                    "times":{
    //                                                        "at_time_vo":[
    //                                                            {
    //                                                                "check_time":"2017-04-11 11:11:11",
    //                                                                "check_type":"OnDuty",
    //                                                                "across":0
    //                                                            }
    //                                                        ]
    //                                                    }
    //                                                }
    //                                            ]
    //                                        },
    //                                        "class_name":"A"
    //                                    }
    //                                ]
    //                            },
    //                            "type":"FIXED",
    //                            "member_count":1,
    //                            "default_class_id":111,
    //                            "work_day_list":{
    //                                "string":[
    //                                    "1"
    //                                ]
    //                            },
    //                            "classes_list":{
    //                                "string":[
    //                                    "周一、二 班次A:09:00-18:00",
    //                                    " 周六、周日 休息"
    //                                ]
    //                            },
    //                            "manager_list":{
    //                                "string":[
    //                                    "1，2"
    //                                ]
    //                            },
    //                            "dept_name_list":{
    //                                "string":[
    //                                    "部门1",
    //                                    "部门2"
    //                                ]
    //                            }
    //                        }
    //                    ]
    //                }
    //            }
    //        }
    //    }
    //}
    public class AttendGroup { }

    public class AttendGroupResopnse { }

    public class AttendGroupResponseResult { }

    public class AttendGroupResponseResultList { }

    public class AttendGroupResultDetail { }

    #endregion 考勤组
}