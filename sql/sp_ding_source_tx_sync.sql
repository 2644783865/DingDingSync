USE [TxCard_MRO]
GO

/****** Object:  StoredProcedure [dbo].[sp_ding_source_tx_sync]    Script Date: 09/26/2017 10:29:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_ding_source_tx_sync]
as
    declare @initDate datetime;
    select  @initDate = '1970-01-01 08:00:00';
    begin try
        begin
            insert  into dbo.Kq_Source
                    ( EmpID
                    , FType
                    , CardNo
                    , MachNo
                    , FDateTime
                    , Flag
                    , Door
                    , IsDel
                    , SN
                    , Remark
                    , Fdate
                    )
                    select  empid
                          , ftype
                          , cardno
                          , machno
                          , fdatetime
                          , flag
                          , door
                          , isdel
                          , sn
                          , remark
                          , fdate
                    from    ( select distinct
                                        e.ID empid
                                      , '1' ftype
                                      , userId cardno
                                      , '1' machno
                                      , dateadd(second, convert(bigint, d.userCheckTime) / 1000, @initDate) fdatetime
                                      , null flag
                                      , null door
                                      , null isdel
                                      , null sn
                                      , d.sourceType remark
                                      , getdate() fdate
                              from      dbo.ding_kq_source d
                                        join ZlEmployee e on d.userId = e.Code
                            ) r
                    where   not exists ( select 1
                                         from   Kq_Source k
                                         where  k.EmpID = r.empid
                                                and k.FDateTime = r.fdatetime
                                                and k.MachNo = r.machno
                                                and k.FDateTime between dateadd(month, -1, getdate()) and getdate() );
            truncate table dbo.ding_kq_source;
        end;

    end try
    begin catch
        begin
            insert  into dbo.ding_kq_source_error
                    ( gmtModified
                    , baseCheckTime
                    , groupId
                    , timeResult
                    , classId
                    , workDate
                    , planId
                    , id
                    , checkType
                    , planCheckTime
                    , corpId
                    , locationResult
                    , isLegal
                    , gmtCreate
                    , userId
                    , userAddress
                    , sourceType
                    , userCheckTime
                    , locationMethod
                    )
                    select  *
                    from    dbo.ding_kq_source;
             --插入log记录
            insert  into ding_sync_error_log
                    ( errCode
                    , errProc
                    , errMsg
                    , errTime
                    )
                    select  error_number()
                          , 'ding_source_tx_sync'
                          , error_message()
                          , getdate();
            truncate table dbo.ding_kq_source;
        end;
        
    end catch;
   
GO

