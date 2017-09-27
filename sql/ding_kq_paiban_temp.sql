USE [TxCard_MRO]
GO

/****** Object:  Table [dbo].[ding_kq_paiban_temp]    Script Date: 09/26/2017 10:19:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ding_kq_paiban_temp](
	[userid] [varchar](100) NULL,
	[plan_check_time] [varchar](100) NULL,
	[class_id] [varchar](100) NULL,
	[check_type] [varchar](100) NULL,
	[class_setting_id] [varchar](100) NULL,
	[group_id] [varchar](100) NULL,
	[approve_id] [varchar](100) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

