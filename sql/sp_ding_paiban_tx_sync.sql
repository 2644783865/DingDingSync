USE [TxCard_MRO]
GO

/****** Object:  StoredProcedure [dbo].[sp_ding_paiban_tx_sync]    Script Date: 09/26/2017 10:29:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ding_paiban_tx_sync]
AS
    DECLARE @begindate DATETIME;
    DECLARE @enddate DATETIME;

    SELECT  @begindate = CONVERT(VARCHAR(7), GETDATE(), 120) + '-1';
    SELECT  @enddate = DATEADD(MONTH, 1, CONVERT(DATETIME, @begindate, 120)) - 1;

    IF OBJECT_ID('#tb') > 0
        DROP TABLE #tb;

    IF DATEPART(DAY, GETDATE()) = 15
        TRUNCATE TABLE dbo.ding_kq_paiban_temp;

--插入钉钉排班临时表       
    INSERT  INTO dbo.ding_kq_paiban_temp
            ( userid ,
              plan_check_time ,
              class_id ,
              class_setting_id ,
              group_id ,
              approve_id
            )
            SELECT  userid ,
                    plan_check_time ,
                    class_id ,
                    class_setting_id ,
                    group_id ,
                    approve_id
            FROM    dbo.ding_kq_paiban d
            WHERE   NOT EXISTS ( SELECT 1
                                 FROM   ding_kq_paiban_temp t
                                 WHERE  t.userid = d.userid
                                        AND t.plan_check_time = d.plan_check_time
                                        AND t.class_id = d.class_id
                                        AND t.group_id = d.group_id
                                        AND t.approve_id = d.approve_id );
    
    TRUNCATE TABLE dbo.ding_kq_paiban;

    SELECT  tbl.sessionid ,
            tbl.empid ,
            tbl.B1 ,
            tbl.B2 ,
            tbl.B3 ,
            tbl.B4 ,
            tbl.B5 ,
            tbl.B6 ,
            tbl.B7 ,
            tbl.B8 ,
            tbl.B9 ,
            tbl.B10 ,
            tbl.B11 ,
            tbl.B12 ,
            tbl.B13 ,
            tbl.B14 ,
            tbl.B15 ,
            tbl.B16 ,
            tbl.B17 ,
            tbl.B18 ,
            tbl.B19 ,
            tbl.B20 ,
            tbl.B21 ,
            tbl.B22 ,
            tbl.B23 ,
            tbl.B24 ,
            tbl.B25 ,
            tbl.B26 ,
            tbl.B27 ,
            tbl.B28 ,
            tbl.B29 ,
            tbl.B30 ,
            tbl.B31
    INTO    #tb
    FROM    ( SELECT    sessionid ,
                        empid ,
                        B1 ,
                        B2 ,
                        B3 ,
                        B4 ,
                        B5 ,
                        B6 ,
                        B7 ,
                        B8 ,
                        B9 ,
                        B10 ,
                        B11 ,
                        B12 ,
                        B13 ,
                        B14 ,
                        B15 ,
                        B16 ,
                        B17 ,
                        B18 ,
                        B19 ,
                        B20 ,
                        B21 ,
                        B22 ,
                        B23 ,
                        B24 ,
                        B25 ,
                        B26 ,
                        B27 ,
                        B28 ,
                        B29 ,
                        B30 ,
                        B31
              FROM      ( SELECT    h.sessionid ,
                                    h.empid ,
                                    SUM(CASE day0
                                          WHEN 1 THEN h.class_id
                                          ELSE NULL
                                        END) B1 ,
                                    SUM(CASE day0
                                          WHEN 2 THEN h.class_id
                                          ELSE NULL
                                        END) B2 ,
                                    SUM(CASE day0
                                          WHEN 3 THEN h.class_id
                                          ELSE NULL
                                        END) B3 ,
                                    SUM(CASE day0
                                          WHEN 4 THEN h.class_id
                                          ELSE NULL
                                        END) B4 ,
                                    SUM(CASE day0
                                          WHEN 5 THEN h.class_id
                                          ELSE NULL
                                        END) B5 ,
                                    SUM(CASE day0
                                          WHEN 6 THEN h.class_id
                                          ELSE NULL
                                        END) B6 ,
                                    SUM(CASE day0
                                          WHEN 7 THEN h.class_id
                                          ELSE NULL
                                        END) B7 ,
                                    SUM(CASE day0
                                          WHEN 8 THEN h.class_id
                                          ELSE NULL
                                        END) B8 ,
                                    SUM(CASE day0
                                          WHEN 9 THEN h.class_id
                                          ELSE NULL
                                        END) B9 ,
                                    SUM(CASE day0
                                          WHEN 10 THEN h.class_id
                                          ELSE NULL
                                        END) B10 ,
                                    SUM(CASE day0
                                          WHEN 11 THEN h.class_id
                                          ELSE NULL
                                        END) B11 ,
                                    SUM(CASE day0
                                          WHEN 12 THEN h.class_id
                                          ELSE NULL
                                        END) B12 ,
                                    SUM(CASE day0
                                          WHEN 13 THEN h.class_id
                                          ELSE NULL
                                        END) B13 ,
                                    SUM(CASE day0
                                          WHEN 14 THEN h.class_id
                                          ELSE NULL
                                        END) B14 ,
                                    SUM(CASE day0
                                          WHEN 15 THEN h.class_id
                                          ELSE NULL
                                        END) B15 ,
                                    SUM(CASE day0
                                          WHEN 16 THEN h.class_id
                                          ELSE NULL
                                        END) B16 ,
                                    SUM(CASE day0
                                          WHEN 17 THEN h.class_id
                                          ELSE NULL
                                        END) B17 ,
                                    SUM(CASE day0
                                          WHEN 18 THEN h.class_id
                                          ELSE NULL
                                        END) B18 ,
                                    SUM(CASE day0
                                          WHEN 19 THEN h.class_id
                                          ELSE NULL
                                        END) B19 ,
                                    SUM(CASE day0
                                          WHEN 20 THEN h.class_id
                                          ELSE NULL
                                        END) B20 ,
                                    SUM(CASE day0
                                          WHEN 21 THEN h.class_id
                                          ELSE NULL
                                        END) B21 ,
                                    SUM(CASE day0
                                          WHEN 22 THEN h.class_id
                                          ELSE NULL
                                        END) B22 ,
                                    SUM(CASE day0
                                          WHEN 23 THEN h.class_id
                                          ELSE NULL
                                        END) B23 ,
                                    SUM(CASE day0
                                          WHEN 24 THEN h.class_id
                                          ELSE NULL
                                        END) B24 ,
                                    SUM(CASE day0
                                          WHEN 25 THEN h.class_id
                                          ELSE NULL
                                        END) B25 ,
                                    SUM(CASE day0
                                          WHEN 26 THEN h.class_id
                                          ELSE NULL
                                        END) B26 ,
                                    SUM(CASE day0
                                          WHEN 27 THEN h.class_id
                                          ELSE NULL
                                        END) B27 ,
                                    SUM(CASE day0
                                          WHEN 28 THEN h.class_id
                                          ELSE NULL
                                        END) B28 ,
                                    SUM(CASE day0
                                          WHEN 29 THEN h.class_id
                                          ELSE NULL
                                        END) B29 ,
                                    SUM(CASE day0
                                          WHEN 30 THEN h.class_id
                                          ELSE NULL
                                        END) B30 ,
                                    SUM(CASE day0
                                          WHEN 31 THEN h.class_id
                                          ELSE NULL
                                        END) B31
                          FROM      ( SELECT DISTINCT
                                                s.ID sessionid ,
                                                e.ID empid ,
                                                CONVERT(VARCHAR(7), k.plan_check_time, 120) date0 ,
                                                DATEPART(DAY, k.plan_check_time) day0 ,
                                                r.TX_banzhi_code class_id
                                      FROM      dbo.ding_kq_paiban_temp k
                                                JOIN ding_banzhi_relation r ON r.class_id = k.class_id
                                                                               AND CONVERT(DATETIME, k.plan_check_time, 120) BETWEEN @begindate AND @enddate
                                                JOIN dbo.S_session s ON k.plan_check_time BETWEEN s.Date0 AND s.Date1
                                                JOIN ZlEmployee e ON e.Code = k.userid
                                    ) h
                          GROUP BY  h.sessionid ,
                                    h.empid
                        ) q
            ) tbl;
                
--插入班制表
    BEGIN

        INSERT  INTO dbo.Kq_PaiBan
                ( SessionID ,
                  EmpID ,
                  B1 ,
                  B2 ,
                  B3 ,
                  B4 ,
                  B5 ,
                  B6 ,
                  B7 ,
                  B8 ,
                  B9 ,
                  B10 ,
                  B11 ,
                  B12 ,
                  B13 ,
                  B14 ,
                  B15 ,
                  B16 ,
                  B17 ,
                  B18 ,
                  B19 ,
                  B20 ,
                  B21 ,
                  B22 ,
                  B23 ,
                  B24 ,
                  B25 ,
                  B26 ,
                  B27 ,
                  B28 ,
                  B29 ,
                  B30 ,
                  B31 
                )
                SELECT  *
                FROM    #tb tb
                WHERE   NOT EXISTS ( SELECT 1
                                     FROM   dbo.Kq_PaiBan p
                                     WHERE  p.SessionID = tb.sessionid
                                            AND p.EmpID = tb.empid ); 
                

    END;
--更新班制表
    BEGIN 
        UPDATE  dbo.Kq_PaiBan
        SET     B1 = tb.B1 ,
                B2 = tb.B2 ,
                B3 = tb.B3 ,
                B4 = tb.B4 ,
                B5 = tb.B5 ,
                B6 = tb.B6 ,
                B7 = tb.B7 ,
                B8 = tb.B8 ,
                B9 = tb.B9 ,
                B10 = tb.B10 ,
                B11 = tb.B11 ,
                B12 = tb.B12 ,
                B13 = tb.B13 ,
                B14 = tb.B14 ,
                B15 = tb.B15 ,
                B16 = tb.B16 ,
                B17 = tb.B17 ,
                B18 = tb.B18 ,
                B19 = tb.B19 ,
                B20 = tb.B20 ,
                B21 = tb.B21 ,
                B22 = tb.B22 ,
                B23 = tb.B23 ,
                B24 = tb.B24 ,
                B25 = tb.B25 ,
                B26 = tb.B26 ,
                B27 = tb.B27 ,
                B28 = tb.B28 ,
                B29 = tb.B29 ,
                B30 = tb.B30 ,
                B31 = tb.B31
        FROM    #tb tb ,
                Kq_PaiBan k
        WHERE   tb.sessionid = k.SessionID
                AND tb.empid = k.EmpID
                AND EXISTS ( SELECT 1
                             FROM   dbo.Kq_PaiBan p
                             WHERE  p.SessionID = tb.sessionid
                                    AND p.EmpID = tb.empid );
    END;

    IF OBJECT_ID('#tb') > 0
        DROP TABLE #tb;



GO

