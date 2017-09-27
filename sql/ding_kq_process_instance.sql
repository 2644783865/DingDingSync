USE [TxCard_MRO]
GO

/****** Object:  Table [dbo].[ding_kq_process_instance]    Script Date: 09/26/2017 10:19:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ding_kq_process_instance](
	[process_instance_id] [varchar](500) NOT NULL,
	[title] [varchar](500) NULL,
	[create_time] [varchar](500) NOT NULL,
	[finish_time] [varchar](500) NULL,
	[originator_userid] [varchar](500) NULL,
	[originator_dept_id] [varchar](500) NULL,
	[status] [varchar](500) NULL,
	[form_vo_name] [varchar](500) NULL,
	[form_vo_value] [varchar](max) NULL,
	[process_instance_result] [varchar](500) NULL,
	[begin_time] [varchar](500) NULL,
	[end_time] [varchar](500) NULL,
	[hours] [varchar](500) NULL,
	[unit] [varchar](500) NULL,
	[form_type] [varchar](500) NULL,
	[instance_type] [varchar](500) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

