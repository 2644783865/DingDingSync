USE [TxCard_MRO]
GO

/****** Object:  Table [dbo].[ding_banzhi_relation]    Script Date: 09/26/2017 10:18:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ding_banzhi_relation](
	[class_id] [varchar](20) NOT NULL,
	[TX_banzhi_code] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

