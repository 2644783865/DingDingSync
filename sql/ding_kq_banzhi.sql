USE [TxCard_MRO]
GO

/****** Object:  Table [dbo].[ding_kq_banzhi]    Script Date: 09/26/2017 10:18:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ding_kq_banzhi](
	[class_id] [varchar](100) NOT NULL,
	[class_name] [varchar](100) NOT NULL,
	[check_time_on] [varchar](100) NOT NULL,
	[check_time_off] [varchar](100) NOT NULL,
	[rest_begin_time] [varchar](100) NULL,
	[rest_end_time] [varchar](100) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

