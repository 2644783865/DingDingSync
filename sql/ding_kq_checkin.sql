USE [TxCard_MRO]
GO

/****** Object:  Table [dbo].[ding_kq_checkin]    Script Date: 09/26/2017 10:18:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ding_kq_checkin](
	[userId] [varchar](100) NULL,
	[name] [varchar](100) NULL,
	[timestamp] [bigint] NULL,
	[remark] [varchar](100) NULL,
	[place] [varchar](100) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

