USE [master]
GO
/****** Object:  Database [DocumentRegistry]    Script Date: 23.05.2021 13:33:23 ******/
CREATE DATABASE [DocumentRegistry]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DocumentRegistry', FILENAME = N'/var/opt/mssql/data/DocumentRegistry.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DocumentRegistry_log', FILENAME = N'/var/opt/mssql/data/DocumentRegistry_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DocumentRegistry] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DocumentRegistry].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DocumentRegistry] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DocumentRegistry] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DocumentRegistry] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DocumentRegistry] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DocumentRegistry] SET ARITHABORT OFF 
GO
ALTER DATABASE [DocumentRegistry] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DocumentRegistry] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DocumentRegistry] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DocumentRegistry] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DocumentRegistry] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DocumentRegistry] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DocumentRegistry] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DocumentRegistry] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DocumentRegistry] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DocumentRegistry] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DocumentRegistry] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DocumentRegistry] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DocumentRegistry] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DocumentRegistry] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DocumentRegistry] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DocumentRegistry] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DocumentRegistry] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DocumentRegistry] SET RECOVERY FULL 
GO
ALTER DATABASE [DocumentRegistry] SET  MULTI_USER 
GO
ALTER DATABASE [DocumentRegistry] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DocumentRegistry] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DocumentRegistry] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DocumentRegistry] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DocumentRegistry] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DocumentRegistry] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DocumentRegistry', N'ON'
GO
ALTER DATABASE [DocumentRegistry] SET QUERY_STORE = OFF
GO
USE [DocumentRegistry]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 23.05.2021 13:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
	[ModifyUserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
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
/****** Object:  Table [dbo].[DocumentType]    Script Date: 23.05.2021 13:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_DocumentType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 23.05.2021 13:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
	[ModifyUserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[FirstName] [nvarchar](150) NOT NULL,
	[LastName] [nvarchar](150) NOT NULL,
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Letter]    Script Date: 23.05.2021 13:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Letter](
	[Id] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
	[ModifyUserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
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
/****** Object:  Table [dbo].[PostCompany]    Script Date: 23.05.2021 13:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostCompany](
	[Id] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
	[ModifyUserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
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
/****** Object:  Table [dbo].[User]    Script Date: 23.05.2021 13:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
	[ModifyUserId] [int] NOT NULL,
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
USE [master]
GO
ALTER DATABASE [DocumentRegistry] SET  READ_WRITE 
GO
