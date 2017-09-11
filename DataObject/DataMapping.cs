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

    #region 考勤打卡记录

    /*记录结构示例

    {
	  "errmsg": "ok",
	  "recordresult": [{
		      "gmtModified": 1492574948000,
		      "isLegal": "N",
		      "baseCheckTime": 1492568460000,
		      "id": 991197412,
		      "userAddress": "北京市朝阳区崔各庄镇阿里中心.望京A座阿里巴巴绿地中心",
		      "userId": "manager7078",
		      "checkType": "OnDuty",
		      "timeResult": "Normal",
		      "deviceId": "cb7ace07d52fe9be14f4d8bec5e1ba79",
		      "corpId": "ding7536bfee6fb1fa5a35c2f4657eb6378f",
		      "sourceType": "USER",
		      "workDate": 1492531200000,
		      "planCheckTime": 1492568497000,
		      "gmtCreate": 1492574948000,
		      "locationMethod": "MAP",
		      "locationResult": "Outside",
		      "userLongitude": 116.486888,
		      "planId": 4556390053,
		      "groupId": 121325603,
		      "userAccuracy": 65,
		      "userCheckTime": 1492568497000,
		      "userLatitude": 39.999946    
    } ],
	  "errcode": 0
}
*/

    public class AttendResult
    {
        public string errmsg { get; set; }

        [JsonProperty(PropertyName = "recordresult")]
        public List<AttendResultDetail> results { get; set; }

        public int errcode { get; set; }
    }

    public class AttendResultDetail
    {
        public long gmtModified { get; set; }
        public string isLegal { get; set; }
        public long baseCheckTime { get; set; }
        public int id { get; set; }
        public string userAddress { get; set; }
        public string userId { get; set; }
        public string checkType { get; set; }
        public string timeResult { get; set; }
        public string deviceId { get; set; }
        public string corpId { get; set; }
        public string sourceType { get; set; }
        public long workDate { get; set; }
        public long planCheckTime { get; set; }
        public long gmtCreate { get; set; }
        public string locationMethod { get; set; }
        public string locationResult { get; set; }
        public long userLongitude { get; set; }
        public int planId { get; set; }
        public int groupId { get; set; }
        public int userAccuracy { get; set; }
        public long userCheckTime { get; set; }
        public double userLatitude { get; set; }
    }

    #endregion 考勤打卡记录

    #region 签到记录

    /*签到记录结构示例
 {
    "dingtalk_smartwork_checkin_record_get_response":{
        "result":{
            "result":{
                "next_cursor":100,
                "page_list":{
                    "checkin_record_vo":[
                        {
                            "checkin_time":1494852872446,
                            "image_list":{
                                "string":[
                                    "[\"http:\/\/image\"]"
                                ]
                            },
                            "detail_place":"家里详细地址",
                            "remark":"备注",
                            "userid":"080517",
                            "place":"家里",
                            "longitude":"30.28030734592014",
                            "latitude":"31.28030734592014"
                        }
                    ]
                }
            },
            "ding_open_errcode":0,
            "success":true,
            "error_msg":"OK"
        }
    }
}
 */

    public class CheckinResult
    {
        public CheckinResponse dingtalk_smartwork_checkin_record_get_response { get; set; }
    }

    public class CheckinResponse
    {
        public ResponseResult result { get; set; }
        public int ding_open_errcode { get; set; }
        public bool success { get; set; }
        public string error_msg { get; set; }
    }

    public class ResponseResult
    {
        public int next_cursor { get; set; }
        public PageList page_list { get; set; }
    }

    public class PageList
    {
        public CheckiDetail checkin_record_vo { get; set; }
    }

    public class CheckiDetail
    {
        public long checkin_time { get; set; }
        public ImageList image_list { get; set; }
        public string detial_place { get; set; }
        public string remark { get; set; }
        public string userid { get; set; }
        public string place { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
    }

    public class ImageList
    {
        [JsonProperty(PropertyName = "string")]
        public List<string> images { get; set; }
    }

    #endregion 签到记录

    #region 审批记录

    /*
     * {
    "dingtalk_smartwork_bpms_processinstance_list_response":{
        "result":{
            "result":{
                "list":{
                    "process_instance_top_vo":[
                        {
                            "process_instance_id":"cbf290af-fc41-4368-bdd7-8b52a28bc",
                            "title":"测试",
                            "create_time":"1497249913000",
                            "finish_time":"1497249913000",
                            "originator_userid":"08051744586",
                            "originator_dept_id":"-1",
                            "status":"RUNNING",
                            "approver_userid_list":{
                                "string":[
                                    "0874458646137"
                                ]
                            },
                            "cc_userid_list":{
                                "string":[
                                    "0874458646137"
                                ]
                            },
                            "form_component_values":{
                                "form_component_value_vo":[
                                    {
                                        "name":"测试标签",
                                        "value":"测试值"
                                    }
                                ]
                            },
                            "process_instance_result":"agree"
                        }
                    ]
                },
                "next_cursor":2
            },
            "ding_open_errcode":0,
            "error_msg":"成功",
            "success":true
        }
    }
}

     */

    public class ProcessInstance
    {
        public ProcessResponse dingtalk_smartwork_bpms_processinstance_list_response { get; set; }
    }

    public class ProcessResponse
    {
        public ProcessResponseResult result { get; set; }
        public int ding_open_errcode { get; set; }
        public string error_msg { get; set; }
        public bool success { get; set; }
    }

    public class ProcessResponseResult
    {
        public InstanceResponseResult list { get; set; }
        public int next_cursor { get; set; }
    }

    public class InstanceResponseResult
    {
        public List<ResultList> process_instance_top_vo { get; set; }
    }

    public class ResultList
    {
        public string process_instance_id { get; set; }
        public string title { get; set; }
        public string create_time { get; set; }
        public string finish_time { get; set; }
        public string originator_userid { get; set; }
        public string originator_dept_id { get; set; }
        public string status { get; set; }
        public ApproverUserList approver_userid_list { get; set; }
        public CCUserList cc_userid_list { get; set; }
        public FormComponentValues form_component_values { get; set; }
        public string process_instance_result { get; set; }
    }

    public class ApproverUserList
    {
        [JsonProperty(PropertyName = "string")]
        public List<string> approverlist { get; set; }
    }

    public class CCUserList
    {
        [JsonProperty(PropertyName = "string")]
        public List<string> cclist { get; set; }
    }

    public class FormComponentValues
    {
        public List<FormComponentValueVo> form_componet_value_vo { get; set; }
    }

    public class FormComponentValueVo
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    #endregion 审批记录
}