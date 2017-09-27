USE [TxCard_MRO]
GO

/****** Object:  StoredProcedure [dbo].[sp_ding_banzhi_tx_sync]    Script Date: 09/26/2017 10:28:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_ding_banzhi_tx_sync]
as
    begin
    
    /*插入新增的班次到班次对应表*/
        begin
            insert  into ding_banzhi_relation
                    ( class_id
                    )
                    select distinct
                            class_id
                    from    dbo.ding_kq_banzhi
                    where   class_id not in ( select    class_id
                                              from      ding_banzhi_relation );
        end;
        
       /*钉钉班次转成同鑫班制格式*/ 
        begin
            delete  dbo.Kq_BanZhi_D
            where   Code in ( select    TX_banzhi_code
                              from      ding_banzhi_relation );
            insert  into dbo.Kq_BanZhi_D
                    ( Code
                    , BanCiXh
                    , SbMaxTime
                    , SbMaxTime2
                    , SbTime
                    , SbMinTime
                    , SbMinTime2
                    , XbMinTime
                    , XbMinTime2
                    , XbTime
                    , XbMaxTime
                    , XbMaxTime2
                    , FactTimeFirst1
                    , FactTimeFirst2
                    , XiuXi
                    , XiuXi0
                    , TcJbMaxSj
                    , TqJbMaxSj
                    , IfBjJb
                    , Zsj
                    , SjSj
                    , JiaBan
                    , IfJbkq
                    , IfJbkh
                    , IfFd
                    , IfSbDk
                    , IfXbDk
                    , IfTcjb
                    , IfTqjb
                    , IfKg
                    , IfChiDao
                    , IfZaoTui
                    , NotBjJb
                    , RptColXh
                    )
                    select distinct
                            z.TX_banzhi_code code
                          , '1' bancixh
                          , datediff(minute, '1970-01-01', convert(datetime, d.check_time_on, 120)) + 240 sbmaxtime
                          , null sbmaxtime2
                          , datediff(minute, '1970-01-01', convert(datetime, d.check_time_on, 120)) sbtime
                          , datediff(minute, '1970-01-01', convert(datetime, d.check_time_on, 120)) - 240 sbmintime
                          , null sbmintime2
                          , datediff(minute, '1970-01-01', convert(datetime, d.check_time_off, 120)) - 240 xbmintime
                          , null xbmintime2
                          , datediff(minute, '1970-01-01', convert(datetime, d.check_time_off, 120)) xbtime
                          , datediff(minute, '1970-01-01', convert(datetime, d.check_time_off, 120)) + 240 xbmaxtime
                          , null xbmaxtime2
                          , null FactTimeFirst1
                          , null FactTimeFirst2
                          , datediff(minute, '1970-01-01', convert(datetime, d.rest_end_time, 120)) xiuxi
                          , datediff(minute, '1970-01-01', convert(datetime, d.rest_begin_time, 120)) xiuxi0
                          , null TcJbMaxSj
                          , null TqJbMaxSj
                          , null IfBjJb
                          , null Zsj
                          , ( datediff(minute, '1970-01-01', convert(datetime, d.check_time_off, 120)) - datediff(minute,
                                                                                                        '1970-01-01',
                                                                                                        convert(datetime, d.check_time_on, 120)) )
                            - ( datediff(minute, '1970-01-01', convert(datetime, d.rest_end_time, 120))
                                - datediff(minute, '1970-01-01', convert(datetime, d.rest_begin_time, 120)) ) SjSj
                          , null JiaBan
                          , 0 IfJbkq
                          , 0 IfJbkh
                          , null IfFd
                          , null IfSbDk
                          , null IfXbDk
                          , 1 IfTcjb
                          , 0 IfTqjb
                          , 1 IfKg
                          , 1 IfChiDao
                          , 1 IfZaoTui
                          , null NotBjJb
                          , 1 RptColXh
                    from    ding_kq_banzhi d
                            join ding_banzhi_relation z on d.class_id = z.class_id
                                                           and not exists ( select  1
                                                                            from    Kq_BanZhi_D k
                                                                            where   k.Code = z.TX_banzhi_code );
        end;
        
        /*钉钉班制插入班制表kq_banzhi*/
        begin
            delete  dbo.Kq_BanZhi
            where   Code in ( select    TX_banzhi_code
                              from      ding_banzhi_relation );
                              
            insert  into dbo.Kq_BanZhi
                    ( Code
                    , Name
                    , Zsj
                    , BanCiCount
                    , IfFdJcsj
                    , IfFdjb
                    , CanZhi
                    , IfDLJCDZT
                    , Note
                    , IfBz
                    , Czy
                    , OperateDate
                    , UseDept
                    , Color
                    )
                    select  distinct
                            d.Code
                          , k.class_name
                          , d.Zsj
                          , 1 bancicount
                          , 0 ifdjcsj
                          , null iffdjb
                          , null canzhi
                          , null ifdljcdzt
                          , null Note
                          , null ifbz
                          , '钉钉更新' czy
                          , getdate() operationdate
                          , null usedept
                          , -2147483643 color
                    from    dbo.Kq_BanZhi_D d
                            join ding_banzhi_relation r on d.Code = r.TX_banzhi_code
                            join ( select distinct
                                            class_id
                                          , class_name
                                   from     ding_kq_banzhi
                                 ) k on r.class_id = k.class_id;
        end;


    end;   
    

GO

