USE [DocumentRegister]
GO

begin transaction

ALTER TABLE [dbo].[Letter] DROP CONSTRAINT [FK_Letter_PostCompanyId]
GO

ALTER TABLE [dbo].[Letter] DROP CONSTRAINT [FK_Letter_ModifyUser]
GO

ALTER TABLE [dbo].[Letter] DROP CONSTRAINT [FK_Letter_Employee]
GO

ALTER TABLE [dbo].[Letter] DROP CONSTRAINT [FK_Letter_DocumentType]
GO

ALTER TABLE [dbo].[Letter] DROP CONSTRAINT [FK_Letter_CreateUser]
GO

ALTER TABLE [dbo].[Letter] DROP CONSTRAINT [FK_Letter_Company]
GO

/****** Object:  Table [dbo].[Letter]    Script Date: 23.05.2021 20:20:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Letter]') AND type in (N'U'))
DROP TABLE [dbo].[Letter]
GO

ALTER TABLE [dbo].[PostCompany] DROP CONSTRAINT [FK_PostCompany_ModifyUser]
GO

ALTER TABLE [dbo].[PostCompany] DROP CONSTRAINT [FK_PostCompany_CreateUserId]
GO

/****** Object:  Table [dbo].[PostCompany]    Script Date: 23.05.2021 20:23:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostCompany]') AND type in (N'U'))
DROP TABLE [dbo].[PostCompany]
GO

ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [FK_Employee_ModifyUser]
GO

ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [FK_Employee_CreateUser]
GO

ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [FK_Employee_Company]
GO

/****** Object:  Table [dbo].[Employee]    Script Date: 23.05.2021 20:22:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
DROP TABLE [dbo].[Employee]
GO

ALTER TABLE [dbo].[Company] DROP CONSTRAINT [FK_Company_ModifyUser]
GO

ALTER TABLE [dbo].[Company] DROP CONSTRAINT [FK_Company_CreateUser]
GO

/****** Object:  Table [dbo].[Company]    Script Date: 23.05.2021 20:22:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND type in (N'U'))
DROP TABLE [dbo].[Company]
GO




/****** Object:  Table [dbo].[User]    Script Date: 23.05.2021 20:23:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO



/****** Object:  Table [dbo].[User]    Script Date: 23.05.2021 20:23:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[Id] [int] identity(1,1) NOT NULL,
	[CreateDate] [datetime]  default getdate() NOT NULL,
	[CreateUserId] [int] default 1 NOT NULL,
	[ModifyDate] [datetime] default getdate() NOT NULL,
	[ModifyUserId] [int] default 1 NOT NULL,
	[IsActive] [bit] default 1 NOT NULL,
	[FirstName] [nvarchar](150) NULL,
	[LastName] [nvarchar](150) NULL,
	[Login] [nvarchar](150) NULL,
	[Email] [nvarchar](150) NOT NULL,
	[PasswordHash] [nvarchar](500) NOT NULL,
	[PasswordSalt] [nvarchar](150) NOT NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[Letter]    Script Date: 23.05.2021 20:20:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/****** Object:  Table [dbo].[Company]    Script Date: 23.05.2021 20:22:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Company](
	[Id] [int] identity(1,1) NOT NULL,
	[CreateDate] [datetime]  default getdate() NOT NULL,
	[CreateUserId] [int] default 1 NOT NULL,
	[ModifyDate] [datetime] default getdate() NOT NULL,
	[ModifyUserId] [int] default 1 NOT NULL,
	[IsActive] [bit] default 1 NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Street] [nvarchar](150) NULL,
	[City] [nvarchar](150) NULL,
	[Branch] [nvarchar](150) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[PostName] [nvarchar](150) NULL,
	[PostStreet] [nvarchar](150) NULL,
	[PostCity] [nvarchar](150) NULL,
	[PostPostalCode] [nvarchar](10) NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_CreateUser] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_CreateUser]
GO

ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_ModifyUser] FOREIGN KEY([ModifyUserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_ModifyUser]
GO



/****** Object:  Table [dbo].[Employee]    Script Date: 23.05.2021 20:22:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employee](
	[Id] [int] identity(1,1) NOT NULL,
	[CreateDate] [datetime]  default getdate() NOT NULL,
	[CreateUserId] [int] default 1 NOT NULL,
	[ModifyDate] [datetime] default getdate() NOT NULL,
	[ModifyUserId] [int] default 1 NOT NULL,
	[IsActive] [bit] default 1 NOT NULL,
	[FirstName] [nvarchar](150) NOT NULL,
	[LastName] [nvarchar](150) NOT NULL,
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([Id])
GO

ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Company]
GO

ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_CreateUser] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_CreateUser]
GO

ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_ModifyUser] FOREIGN KEY([ModifyUserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_ModifyUser]
GO

CREATE TABLE [dbo].[PostCompany](
	[Id] [int] identity(1,1) NOT NULL,
	[CreateDate] [datetime]  default getdate() NOT NULL,
	[CreateUserId] [int] default 1 NOT NULL,
	[ModifyDate] [datetime] default getdate() NOT NULL,
	[ModifyUserId] [int] default 1 NOT NULL,
	[IsActive] [bit] default 1 NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Code] [nvarchar](150) NULL,
	[City] [nvarchar](150) NULL,
	[Street] [nvarchar](150) NULL,
	[ContractNumber] [nvarchar](150) NULL,
	[ContractDate] [date] NULL,
	[PostOffice] [nvarchar](150) NULL,
 CONSTRAINT [PK_PostCompany] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PostCompany]  WITH CHECK ADD  CONSTRAINT [FK_PostCompany_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[PostCompany] CHECK CONSTRAINT [FK_PostCompany_CreateUserId]
GO

ALTER TABLE [dbo].[PostCompany]  WITH CHECK ADD  CONSTRAINT [FK_PostCompany_ModifyUser] FOREIGN KEY([ModifyUserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[PostCompany] CHECK CONSTRAINT [FK_PostCompany_ModifyUser]
GO


CREATE TABLE [dbo].[Letter](
	[Id] [int] identity(1,1) NOT NULL,
	[CreateDate] [datetime]  default getdate() NOT NULL,
	[CreateUserId] [int] default 1 NOT NULL,
	[ModifyDate] [datetime] default getdate() NOT NULL,
	[ModifyUserId] [int] default 1 NOT NULL,
	[IsActive] [bit] default 1 NOT NULL,
	[Number] [int] NOT NULL,
	[PostCompanyId] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[ReceiveDate] [date] NOT NULL,
	[Content] [nvarchar](150) NULL,
	[EmployeeId] [int] NULL,
	[CompanyId] [int] NULL,
	[DocumentTypeId] [int] NULL,
	[Other] [nvarchar](150) NULL,
	[PR] [bit] NULL,
	[PO] [bit] NULL,
	[CompanyName] [nvarchar](150) NULL,
	[CompanyStreet] [nvarchar](150) NULL,
	[CompanyCity] [nvarchar](150) NULL,
	[CompanyPostalCode] [nvarchar](10) NULL,
	[CompanyPostName] [nvarchar](150) NULL,
	[CompanyPostStreet] [nvarchar](1500) NULL,
	[CompanyPostCity] [nvarchar](150) NULL,
	[CompanyPostPostalCode] [nvarchar](10) NULL,
	[EmployeeFirstName] [nvarchar](150) NULL,
	[EmployeeLastName] [nvarchar](150) NULL,
	[Inbox] [bit] NULL,
	[Outbox] [bit] NULL,
 CONSTRAINT [PK_Letter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Letter]  WITH CHECK ADD  CONSTRAINT [FK_Letter_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([Id])
GO

ALTER TABLE [dbo].[Letter] CHECK CONSTRAINT [FK_Letter_Company]
GO

ALTER TABLE [dbo].[Letter]  WITH CHECK ADD  CONSTRAINT [FK_Letter_CreateUser] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Letter] CHECK CONSTRAINT [FK_Letter_CreateUser]
GO

ALTER TABLE [dbo].[Letter]  WITH CHECK ADD  CONSTRAINT [FK_Letter_DocumentType] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[DocumentType] ([Id])
GO

ALTER TABLE [dbo].[Letter] CHECK CONSTRAINT [FK_Letter_DocumentType]
GO

ALTER TABLE [dbo].[Letter]  WITH CHECK ADD  CONSTRAINT [FK_Letter_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO

ALTER TABLE [dbo].[Letter] CHECK CONSTRAINT [FK_Letter_Employee]
GO

ALTER TABLE [dbo].[Letter]  WITH CHECK ADD  CONSTRAINT [FK_Letter_ModifyUser] FOREIGN KEY([ModifyUserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Letter] CHECK CONSTRAINT [FK_Letter_ModifyUser]
GO

ALTER TABLE [dbo].[Letter]  WITH CHECK ADD  CONSTRAINT [FK_Letter_PostCompanyId] FOREIGN KEY([PostCompanyId])
REFERENCES [dbo].[PostCompany] ([Id])
GO

ALTER TABLE [dbo].[Letter] CHECK CONSTRAINT [FK_Letter_PostCompanyId]
GO

/****** Object:  Table [dbo].[PostCompany]    Script Date: 23.05.2021 20:23:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




commit tran