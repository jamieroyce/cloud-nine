USE [cloud-nine]
GO
ALTER TABLE [dbo].[user] DROP CONSTRAINT [FK_32]
GO
ALTER TABLE [dbo].[unit] DROP CONSTRAINT [FK_49]
GO
ALTER TABLE [dbo].[unit] DROP CONSTRAINT [FK_126]
GO
ALTER TABLE [dbo].[tenant_payment] DROP CONSTRAINT [FK_174]
GO
ALTER TABLE [dbo].[tenant_payment] DROP CONSTRAINT [FK_160]
GO
ALTER TABLE [dbo].[tenant_payment] DROP CONSTRAINT [FK_155]
GO
ALTER TABLE [dbo].[tenant] DROP CONSTRAINT [FK_85]
GO
ALTER TABLE [dbo].[tenant] DROP CONSTRAINT [FK_56]
GO
ALTER TABLE [dbo].[tenant] DROP CONSTRAINT [FK_170]
GO
ALTER TABLE [dbo].[tenant] DROP CONSTRAINT [FK_131]
GO
ALTER TABLE [dbo].[service_pro] DROP CONSTRAINT [FK_96]
GO
ALTER TABLE [dbo].[service_pro] DROP CONSTRAINT [FK_37]
GO
ALTER TABLE [dbo].[service_payment] DROP CONSTRAINT [FK_191]
GO
ALTER TABLE [dbo].[service_payment] DROP CONSTRAINT [FK_188]
GO
ALTER TABLE [dbo].[service_payment] DROP CONSTRAINT [FK_185]
GO
ALTER TABLE [dbo].[property_manager] DROP CONSTRAINT [FK_118]
GO
ALTER TABLE [dbo].[property_manager] DROP CONSTRAINT [FK_115]
GO
ALTER TABLE [dbo].[property] DROP CONSTRAINT [FK_42]
GO
ALTER TABLE [dbo].[owner] DROP CONSTRAINT [FK_93]
GO
ALTER TABLE [dbo].[owner] DROP CONSTRAINT [FK_165]
GO
ALTER TABLE [dbo].[manager] DROP CONSTRAINT [FK_106a]
GO
ALTER TABLE [dbo].[lease] DROP CONSTRAINT [FK_71]
GO
ALTER TABLE [dbo].[lease] DROP CONSTRAINT [FK_136]
GO
ALTER TABLE [dbo].[contract] DROP CONSTRAINT [FK_150]
GO
ALTER TABLE [dbo].[contract] DROP CONSTRAINT [FK_145]
GO
ALTER TABLE [dbo].[broker] DROP CONSTRAINT [FK_198]
GO
/****** Object:  Table [dbo].[user_type]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_type]') AND type in (N'U'))
DROP TABLE [dbo].[user_type]
GO
/****** Object:  Table [dbo].[user]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user]') AND type in (N'U'))
DROP TABLE [dbo].[user]
GO
/****** Object:  Table [dbo].[unit_status]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[unit_status]') AND type in (N'U'))
DROP TABLE [dbo].[unit_status]
GO
/****** Object:  Table [dbo].[unit]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[unit]') AND type in (N'U'))
DROP TABLE [dbo].[unit]
GO
/****** Object:  Table [dbo].[tenant_type]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tenant_type]') AND type in (N'U'))
DROP TABLE [dbo].[tenant_type]
GO
/****** Object:  Table [dbo].[tenant_status]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tenant_status]') AND type in (N'U'))
DROP TABLE [dbo].[tenant_status]
GO
/****** Object:  Table [dbo].[tenant_payment]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tenant_payment]') AND type in (N'U'))
DROP TABLE [dbo].[tenant_payment]
GO
/****** Object:  Table [dbo].[tenant]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tenant]') AND type in (N'U'))
DROP TABLE [dbo].[tenant]
GO
/****** Object:  Table [dbo].[service_pro_type]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[service_pro_type]') AND type in (N'U'))
DROP TABLE [dbo].[service_pro_type]
GO
/****** Object:  Table [dbo].[service_pro]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[service_pro]') AND type in (N'U'))
DROP TABLE [dbo].[service_pro]
GO
/****** Object:  Table [dbo].[service_payment]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[service_payment]') AND type in (N'U'))
DROP TABLE [dbo].[service_payment]
GO
/****** Object:  Table [dbo].[property_type]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[property_type]') AND type in (N'U'))
DROP TABLE [dbo].[property_type]
GO
/****** Object:  Table [dbo].[property_manager]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[property_manager]') AND type in (N'U'))
DROP TABLE [dbo].[property_manager]
GO
/****** Object:  Table [dbo].[property]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[property]') AND type in (N'U'))
DROP TABLE [dbo].[property]
GO
/****** Object:  Table [dbo].[payment_type]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[payment_type]') AND type in (N'U'))
DROP TABLE [dbo].[payment_type]
GO
/****** Object:  Table [dbo].[payment_status]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[payment_status]') AND type in (N'U'))
DROP TABLE [dbo].[payment_status]
GO
/****** Object:  Table [dbo].[owner_type]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[owner_type]') AND type in (N'U'))
DROP TABLE [dbo].[owner_type]
GO
/****** Object:  Table [dbo].[owner]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[owner]') AND type in (N'U'))
DROP TABLE [dbo].[owner]
GO
/****** Object:  Table [dbo].[manager]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[manager]') AND type in (N'U'))
DROP TABLE [dbo].[manager]
GO
/****** Object:  Table [dbo].[lease_status]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lease_status]') AND type in (N'U'))
DROP TABLE [dbo].[lease_status]
GO
/****** Object:  Table [dbo].[lease]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lease]') AND type in (N'U'))
DROP TABLE [dbo].[lease]
GO
/****** Object:  Table [dbo].[contract_status]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[contract_status]') AND type in (N'U'))
DROP TABLE [dbo].[contract_status]
GO
/****** Object:  Table [dbo].[contract]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[contract]') AND type in (N'U'))
DROP TABLE [dbo].[contract]
GO
/****** Object:  Table [dbo].[broker_payment]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[broker_payment]') AND type in (N'U'))
DROP TABLE [dbo].[broker_payment]
GO
/****** Object:  Table [dbo].[broker]    Script Date: 5/31/2021 10:47:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[broker]') AND type in (N'U'))
DROP TABLE [dbo].[broker]
GO
/****** Object:  Table [dbo].[broker]    Script Date: 5/31/2021 10:47:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[broker](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[company] [varchar](100) NULL,
	[user_id] [int] NULL,
	[notes] [varchar](200) NOT NULL,
 CONSTRAINT [PK_broker] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[broker_payment]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[broker_payment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[amount] [money] NOT NULL,
	[desc] [varchar](100) NOT NULL,
	[date_created] [datetime] NOT NULL,
	[date_modified] [datetime] NOT NULL,
	[status] [varchar](50) NOT NULL,
	[notes] [varchar](200) NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[status_id] [int] NOT NULL,
	[type_id] [int] NOT NULL,
	[broker_id] [int] NOT NULL,
 CONSTRAINT [PK_broker_payment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[contract]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contract](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[service_pro_id] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
	[start_date] [date] NOT NULL,
	[end_date] [date] NOT NULL,
	[value] [money] NOT NULL,
	[status_id] [int] NOT NULL,
	[date_created] [datetime] NOT NULL,
	[date_modified] [datetime] NOT NULL,
	[is_deleted] [bit] NOT NULL,
 CONSTRAINT [PK_contract] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[contract_status]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contract_status](
	[id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_contact_status] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lease]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lease](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tenant_id] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
	[months] [smallint] NOT NULL,
	[status_id] [int] NOT NULL,
	[start_date] [date] NOT NULL,
	[end_date] [date] NOT NULL,
	[rent_amount] [money] NOT NULL,
	[due_day] [smallint] NOT NULL,
	[date_created] [datetime] NOT NULL,
	[date_modified] [datetime] NOT NULL,
	[is_deleted] [bit] NOT NULL,
 CONSTRAINT [PK_lease] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lease_status]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lease_status](
	[id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_lease_status] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[manager]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[manager](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[company] [varchar](100) NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_manager] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[owner]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[owner](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[company] [varchar](100) NULL,
	[user_id] [int] NOT NULL,
	[type_id] [int] NOT NULL,
 CONSTRAINT [PK_owner] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[owner_type]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[owner_type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[desc] [varchar](100) NOT NULL,
 CONSTRAINT [PK_owner_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payment_status]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payment_status](
	[id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_payment_status] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payment_type]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payment_type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[desc] [varchar](100) NOT NULL,
 CONSTRAINT [PK_payment_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[property]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[property](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[owner_id] [int] NOT NULL,
	[type_id] [int] NOT NULL,
 CONSTRAINT [PK_property] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[property_manager]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[property_manager](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[property_id] [int] NOT NULL,
	[manager_id] [int] NOT NULL,
 CONSTRAINT [PK_property_manager] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[property_type]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[property_type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[desc] [varchar](100) NOT NULL,
 CONSTRAINT [PK_property_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[service_payment]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[service_payment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[amount] [money] NOT NULL,
	[desc] [varchar](100) NOT NULL,
	[date_created] [datetime] NOT NULL,
	[date_modified] [datetime] NOT NULL,
	[status] [varchar](50) NOT NULL,
	[notes] [varchar](200) NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[unit_id] [int] NOT NULL,
	[status_id] [int] NOT NULL,
	[type_id] [int] NOT NULL,
 CONSTRAINT [PK_service_payment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[service_pro]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[service_pro](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[company] [varchar](100) NULL,
	[service_pro_type_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_service_pro] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[service_pro_type]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[service_pro_type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[desc] [varchar](100) NOT NULL,
 CONSTRAINT [PK_service_pro_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tenant]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tenant](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[date_created] [datetime] NOT NULL,
	[date_modified] [datetime] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[user_id] [int] NOT NULL,
	[unit_id] [int] NOT NULL,
	[status_id] [int] NOT NULL,
	[type_id] [int] NOT NULL,
 CONSTRAINT [PK_tenant] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tenant_payment]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tenant_payment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[amount] [money] NOT NULL,
	[desc] [varchar](100) NOT NULL,
	[date_created] [datetime] NOT NULL,
	[date_modified] [datetime] NOT NULL,
	[status] [varchar](50) NOT NULL,
	[notes] [varchar](200) NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[status_id] [int] NOT NULL,
	[type_id] [int] NOT NULL,
	[tenant_id] [int] NOT NULL,
 CONSTRAINT [PK_tenant_payment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tenant_status]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tenant_status](
	[id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tenant_status] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tenant_type]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tenant_type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[desc] [varchar](100) NOT NULL,
 CONSTRAINT [PK_tenant_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[unit]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[unit](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[property_id] [int] NOT NULL,
	[status_id] [int] NOT NULL,
 CONSTRAINT [PK_owner_clone_clone_clone_1_clone_clone] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[unit_status]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[unit_status](
	[id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_unit_status] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[email] [varchar](150) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[type_id] [int] NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_type]    Script Date: 5/31/2021 10:47:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[desc] [varchar](100) NOT NULL,
 CONSTRAINT [PK_user_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[broker]  WITH CHECK ADD  CONSTRAINT [FK_198] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[broker] CHECK CONSTRAINT [FK_198]
GO
ALTER TABLE [dbo].[contract]  WITH CHECK ADD  CONSTRAINT [FK_145] FOREIGN KEY([service_pro_id])
REFERENCES [dbo].[service_pro] ([id])
GO
ALTER TABLE [dbo].[contract] CHECK CONSTRAINT [FK_145]
GO
ALTER TABLE [dbo].[contract]  WITH CHECK ADD  CONSTRAINT [FK_150] FOREIGN KEY([status_id])
REFERENCES [dbo].[contract_status] ([id])
GO
ALTER TABLE [dbo].[contract] CHECK CONSTRAINT [FK_150]
GO
ALTER TABLE [dbo].[lease]  WITH CHECK ADD  CONSTRAINT [FK_136] FOREIGN KEY([status_id])
REFERENCES [dbo].[lease_status] ([id])
GO
ALTER TABLE [dbo].[lease] CHECK CONSTRAINT [FK_136]
GO
ALTER TABLE [dbo].[lease]  WITH CHECK ADD  CONSTRAINT [FK_71] FOREIGN KEY([tenant_id])
REFERENCES [dbo].[tenant] ([id])
GO
ALTER TABLE [dbo].[lease] CHECK CONSTRAINT [FK_71]
GO
ALTER TABLE [dbo].[manager]  WITH CHECK ADD  CONSTRAINT [FK_106a] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[manager] CHECK CONSTRAINT [FK_106a]
GO
ALTER TABLE [dbo].[owner]  WITH CHECK ADD  CONSTRAINT [FK_165] FOREIGN KEY([type_id])
REFERENCES [dbo].[owner_type] ([id])
GO
ALTER TABLE [dbo].[owner] CHECK CONSTRAINT [FK_165]
GO
ALTER TABLE [dbo].[owner]  WITH CHECK ADD  CONSTRAINT [FK_93] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[owner] CHECK CONSTRAINT [FK_93]
GO
ALTER TABLE [dbo].[property]  WITH CHECK ADD  CONSTRAINT [FK_42] FOREIGN KEY([owner_id])
REFERENCES [dbo].[owner] ([id])
GO
ALTER TABLE [dbo].[property] CHECK CONSTRAINT [FK_42]
GO
ALTER TABLE [dbo].[property_manager]  WITH CHECK ADD  CONSTRAINT [FK_115] FOREIGN KEY([property_id])
REFERENCES [dbo].[property] ([id])
GO
ALTER TABLE [dbo].[property_manager] CHECK CONSTRAINT [FK_115]
GO
ALTER TABLE [dbo].[property_manager]  WITH CHECK ADD  CONSTRAINT [FK_118] FOREIGN KEY([manager_id])
REFERENCES [dbo].[manager] ([id])
GO
ALTER TABLE [dbo].[property_manager] CHECK CONSTRAINT [FK_118]
GO
ALTER TABLE [dbo].[service_payment]  WITH CHECK ADD  CONSTRAINT [FK_185] FOREIGN KEY([unit_id])
REFERENCES [dbo].[unit] ([id])
GO
ALTER TABLE [dbo].[service_payment] CHECK CONSTRAINT [FK_185]
GO
ALTER TABLE [dbo].[service_payment]  WITH CHECK ADD  CONSTRAINT [FK_188] FOREIGN KEY([status_id])
REFERENCES [dbo].[payment_status] ([id])
GO
ALTER TABLE [dbo].[service_payment] CHECK CONSTRAINT [FK_188]
GO
ALTER TABLE [dbo].[service_payment]  WITH CHECK ADD  CONSTRAINT [FK_191] FOREIGN KEY([type_id])
REFERENCES [dbo].[payment_type] ([id])
GO
ALTER TABLE [dbo].[service_payment] CHECK CONSTRAINT [FK_191]
GO
ALTER TABLE [dbo].[service_pro]  WITH CHECK ADD  CONSTRAINT [FK_37] FOREIGN KEY([service_pro_type_id])
REFERENCES [dbo].[service_pro_type] ([id])
GO
ALTER TABLE [dbo].[service_pro] CHECK CONSTRAINT [FK_37]
GO
ALTER TABLE [dbo].[service_pro]  WITH CHECK ADD  CONSTRAINT [FK_96] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[service_pro] CHECK CONSTRAINT [FK_96]
GO
ALTER TABLE [dbo].[tenant]  WITH CHECK ADD  CONSTRAINT [FK_131] FOREIGN KEY([status_id])
REFERENCES [dbo].[tenant_status] ([id])
GO
ALTER TABLE [dbo].[tenant] CHECK CONSTRAINT [FK_131]
GO
ALTER TABLE [dbo].[tenant]  WITH CHECK ADD  CONSTRAINT [FK_170] FOREIGN KEY([type_id])
REFERENCES [dbo].[tenant_type] ([id])
GO
ALTER TABLE [dbo].[tenant] CHECK CONSTRAINT [FK_170]
GO
ALTER TABLE [dbo].[tenant]  WITH CHECK ADD  CONSTRAINT [FK_56] FOREIGN KEY([unit_id])
REFERENCES [dbo].[unit] ([id])
GO
ALTER TABLE [dbo].[tenant] CHECK CONSTRAINT [FK_56]
GO
ALTER TABLE [dbo].[tenant]  WITH CHECK ADD  CONSTRAINT [FK_85] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[tenant] CHECK CONSTRAINT [FK_85]
GO
ALTER TABLE [dbo].[tenant_payment]  WITH CHECK ADD  CONSTRAINT [FK_155] FOREIGN KEY([status_id])
REFERENCES [dbo].[payment_status] ([id])
GO
ALTER TABLE [dbo].[tenant_payment] CHECK CONSTRAINT [FK_155]
GO
ALTER TABLE [dbo].[tenant_payment]  WITH CHECK ADD  CONSTRAINT [FK_160] FOREIGN KEY([type_id])
REFERENCES [dbo].[payment_type] ([id])
GO
ALTER TABLE [dbo].[tenant_payment] CHECK CONSTRAINT [FK_160]
GO
ALTER TABLE [dbo].[tenant_payment]  WITH CHECK ADD  CONSTRAINT [FK_174] FOREIGN KEY([tenant_id])
REFERENCES [dbo].[tenant] ([id])
GO
ALTER TABLE [dbo].[tenant_payment] CHECK CONSTRAINT [FK_174]
GO
ALTER TABLE [dbo].[unit]  WITH CHECK ADD  CONSTRAINT [FK_126] FOREIGN KEY([status_id])
REFERENCES [dbo].[unit_status] ([id])
GO
ALTER TABLE [dbo].[unit] CHECK CONSTRAINT [FK_126]
GO
ALTER TABLE [dbo].[unit]  WITH CHECK ADD  CONSTRAINT [FK_49] FOREIGN KEY([property_id])
REFERENCES [dbo].[property] ([id])
GO
ALTER TABLE [dbo].[unit] CHECK CONSTRAINT [FK_49]
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_32] FOREIGN KEY([type_id])
REFERENCES [dbo].[user_type] ([id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_32]
GO
