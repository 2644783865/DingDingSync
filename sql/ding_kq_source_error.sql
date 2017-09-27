USE [TxCard_MRO]
GO

/****** Object:  Table [dbo].[ding_kq_source_error]    Script Date: 09/26/2017 10:23:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ding_kq_source_error](
	[gmtModified] [varchar](100) NULL,
	[baseCheckTime] [varchar](100) NULL,
	[groupId] [varchar](100) NULL,
	[timeResult] [varchar](100) NULL,
	[classId] [varchar](100) NULL,
	[workDate] [varchar](100) NULL,
	[planId] [varchar](100) NULL,
	[id] [varchar](100) NULL,
	[checkType] [varchar](100) NULL,
	[planCheckTime] [varchar](100) NULL,
	[corpId] [varchar](100) NULL,
	[locationResult] [varchar](100) NULL,
	[isLegal] [varchar](100) NULL,
	[gmtCreate] [varchar](100) NULL,
	[userId] [varchar](100) NULL,
	[userAddress] [varchar](100) NULL,
	[sourceType] [varchar](100) NULL,
	[userCheckTime] [varchar](100) NULL,
	[locationMethod] [varchar](100) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

