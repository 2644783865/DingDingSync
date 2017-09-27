USE [TxCard_MRO]
GO

/****** Object:  Table [dbo].[ding_sync_error_log]    Script Date: 09/26/2017 10:25:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ding_sync_error_log](
	[errCode] [int] NULL,
	[errProc] [varchar](50) NULL,
	[errMsg] [varchar](500) NULL,
	[errTime] [datetime] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

