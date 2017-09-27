

/****** Object:  StoredProcedure [dbo].[sp_ding_instance_tx_sync]    Script Date: 09/26/2017 10:28:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--SET QUOTED_IDENTIFIER ON|OFF
--SET ANSI_NULLS ON|OFF
--GO

create procedure [dbo].[sp_ding_instance_tx_sync]
as
    update  dbo.ding_kq_process_instance
    set     form_type = case when isnull(instance_type, '') like '%外出%' then '外出'
                             when isnull(instance_type, '') like '%出差%' then '出差'
                             when isnull(instance_type, '') like '%加班%' then '加班'
                             else form_type
                        end; 


--缂哄嫟鐧昏澶勭悊
--ID	Ifzm	EmpID	BanZhi	QqType	QqIfDk	FDatetime0	FDatetime1	TotalHour	Wjbh	Note	Sqr	Czy	Qsr	ShLevel	ShNote	FlwInfo	FlwState	OperateDate	OpterateDate
--1	1	619	3   	G_sj	1	2017-09-22 08:30:00.000	2017-09-22 09:00:00.000	0.50	NULL	NULL	NULL	鍚岄懌绉戞妧  	NULL	NULL	NULL	NULL	NULL	2017-09-24 14:59:17.787	NULL
    insert  into dbo.Kq_Djqq
            ( Ifzm
            , EmpID
            , BanZhi
            , QqType
            , QqIfDk
            , FDatetime0
            , FDatetime1
            , TotalHour
            , Wjbh
            , Note
            , Sqr
            , Czy
            , Qsr
            , ShLevel
            , ShNote
            , FlwInfo
            , FlwState
            , OperateDate
            , OpterateDate
            )
            select distinct
                    1
                  , e.ID
                  , ''
                  , q.Code
                  , 1
                  , p.begin_time
                  , p.end_time
                  , p.hours
                  , null
                  , '钉钉同步转入'
                  , null
                  , '钉钉'
                  , null
                  , null
                  , null
                  , null
                  , null
                  , getdate()
                  , getdate()
            from    dbo.ding_kq_process_instance p
                    join ZlEmployee e on p.originator_userid = e.Code
                                         and p.process_instance_result = 'agree'
                    join ( select   Code
                                  , Name
                           from     dbo.G_Hsxm
                           where    XmType = 6
                                    and IfUsed = 1
                         ) q on p.form_type = q.Name
                                and not exists ( select 1
                                                 from   Kq_Djqq d
                                                 where  d.EmpID = e.ID
                                                        and d.QqType = q.Code
                                                        and d.FDatetime0 = p.begin_time
                                                        and d.FDatetime1 = p.end_time
                                                        and e.Code = p.originator_userid );




--鍔犵彮澶勭悊
    insert  into dbo.Kq_Day
            ( Sh
            , EmpID
            , FDate
            , FDate1
            , FlwInfo
            , FlwState
            , Wjbh
            , Qsr
            , Sqr
            , CZY
            , OperateDate
            , Note
            , G_jbsq
            , G_time0
            , G_time1
            , G_Note
            )
            select distinct
                    1
                  , e.ID
                  , convert(datetime, left(p.begin_time, 10), 120)
                  , null
                  , null
                  , null
                  , null
                  , null
                  , null
                  , '钉钉'
                  , getdate()
                  , null
                  , p.hours
                  , p.begin_time
                  , p.end_time
                  , null
            from    dbo.ding_kq_process_instance p
                    join dbo.ZlEmployee e on p.originator_userid = e.Code
                                             and p.form_type = '加班'
                                             and p.process_instance_result = 'agree'
                                             and not exists ( select    1
                                                              from      Kq_Day d
                                                              where     d.EmpID = e.ID
                                                                        and e.Code = p.originator_userid
                                                                        and convert(datetime, left(p.begin_time, 10), 120) = d.FDate
                                                                        and p.form_type = '加班' );
                             
    insert  into dbo.ding_kq_process_instance_temp
            ( process_instance_id
            , title
            , create_time
            , finish_time
            , originator_userid
            , originator_dept_id
            , status
            , form_vo_name
            , form_vo_value
            , process_instance_result
            , begin_time
            , end_time
            , hours
            , unit
            , form_type
            , instance_type
            )
            select  *
            from    dbo.ding_kq_process_instance;
            
            
    truncate table dbo.ding_kq_process_instance;
                             
                                                                        
                                                                        
                                                                        
                                                                        

GO

