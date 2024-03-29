USE [master]
GO
/****** Object:  Database [Shop]    Script Date: 2020/7/8 11:14:52 ******/
CREATE DATABASE [Shop] ON  PRIMARY 
( NAME = N'Shop', FILENAME = N'D:\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Shop.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Shop_log', FILENAME = N'D:\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Shop_log.ldf' , SIZE = 6912KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Shop] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Shop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Shop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Shop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Shop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Shop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Shop] SET ARITHABORT OFF 
GO
ALTER DATABASE [Shop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Shop] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Shop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Shop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Shop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Shop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Shop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Shop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Shop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Shop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Shop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Shop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Shop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Shop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Shop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Shop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Shop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Shop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Shop] SET RECOVERY FULL 
GO
ALTER DATABASE [Shop] SET  MULTI_USER 
GO
ALTER DATABASE [Shop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Shop] SET DB_CHAINING OFF 
GO
USE [Shop]
GO
/****** Object:  User [x]    Script Date: 2020/7/8 11:14:52 ******/
CREATE USER [x] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[accountLog]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accountLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[tradeType] [nvarchar](50) NULL,
	[tradeChannel] [int] NOT NULL,
	[tradeNo] [nvarchar](50) NOT NULL,
	[ouTradeNo] [nvarchar](50) NOT NULL,
	[incomeAmount] [decimal](18, 0) NOT NULL,
	[gmtCreate] [datetime] NULL,
	[gmtPayment] [datetime] NULL,
	[remarks] [nvarchar](max) NULL,
 CONSTRAINT [PK_pb_account_log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AdminInfo]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdminInfo](
	[ID] [int] NOT NULL,
	[userCode] [varchar](20) NULL,
	[userName] [varchar](64) NOT NULL,
	[realName] [varchar](100) NOT NULL,
	[password] [varchar](32) NOT NULL,
	[telephone] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[createTime] [datetime] NOT NULL,
	[state] [int] NOT NULL,
	[roleCode] [int] NOT NULL,
	[Remark] [nvarchar](20) NULL,
	[img] [nvarchar](50) NULL,
 CONSTRAINT [PK_hf_admin_user] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[adminLog]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[adminLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[logTime] [datetime] NOT NULL,
	[userId] [nvarchar](50) NOT NULL,
	[logInfo] [varchar](255) NULL,
 CONSTRAINT [PK_hf_admin_log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[adminState]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[adminState](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LoginState] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Area]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Area](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Cityname] [nchar](10) NOT NULL,
	[PID] [int] NOT NULL,
 CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[article]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[article](
	[articleId] [int] IDENTITY(1,1) NOT NULL,
	[articleCode] [nvarchar](50) NOT NULL,
	[Author] [nvarchar](50) NULL,
	[content] [text] NULL,
	[subject] [varchar](200) NULL,
	[publishDate] [datetime] NULL,
	[commentNo] [int] NULL,
	[picture] [text] NULL,
	[isDelete] [int] NULL,
 CONSTRAINT [PK_ar] PRIMARY KEY CLUSTERED 
(
	[articleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[attribute]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[attribute](
	[attrId] [int] IDENTITY(1,1) NOT NULL,
	[attrName] [varchar](64) NOT NULL,
	[attrValues] [text] NULL,
	[attrType] [smallint] NULL,
	[attrIndex] [smallint] NULL,
	[sortOrder] [smallint] NULL,
	[is_linked] [smallint] NULL,
	[isDelete] [int] NULL,
 CONSTRAINT [PK_hf_attribute] PRIMARY KEY CLUSTERED 
(
	[attrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[backGoods]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[backGoods](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[backCode] [nvarchar](50) NOT NULL,
	[goodsCode] [nvarchar](50) NOT NULL,
	[backType] [int] NOT NULL,
	[backGoodsPrice] [decimal](10, 2) NOT NULL,
	[backgoodsNumber] [int] NOT NULL,
	[statusBack] [int] NOT NULL,
	[statusRefund] [int] NOT NULL,
	[States] [int] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_hf_back_goods] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cart]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cart](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[goodsSn] [nvarchar](50) NOT NULL,
	[goodsName] [nvarchar](50) NOT NULL,
	[marketPrice] [decimal](18, 0) NOT NULL,
	[postage] [decimal](18, 0) NULL,
	[buyNumber] [int] NOT NULL,
	[totalPrice] [decimal](18, 0) NOT NULL,
	[goodsType] [int] NULL,
	[isGift] [int] NULL,
	[isShipping] [int] NULL,
	[shippingNum] [int] NULL,
	[attrId] [int] NULL,
	[addTime] [datetime] NOT NULL,
	[promotePrice] [decimal](18, 0) NULL,
	[thumbUrl] [nvarchar](max) NULL,
	[enable] [int] NOT NULL,
	[state] [varchar](20) NOT NULL,
 CONSTRAINT [PK_cart] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Contacts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[subject] [varchar](50) NOT NULL,
	[information] [text] NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Goods]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Goods](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[goodType] [int] NOT NULL,
	[goodsSn] [nvarchar](50) NOT NULL,
	[goodsName] [nvarchar](max) NOT NULL,
	[clickCount] [int] NOT NULL,
	[supplier] [int] NOT NULL,
	[goodsNumber] [int] NULL,
	[goodsWeight] [decimal](18, 0) NULL,
	[marketPrice] [decimal](18, 0) NULL,
	[promotePrice] [decimal](18, 0) NULL,
	[promoteStartDate] [datetime] NULL,
	[keywords] [nvarchar](max) NOT NULL,
	[goodsDesc] [nvarchar](max) NULL,
	[isOnSale] [int] NULL,
	[isShipping] [int] NULL,
	[addTime] [datetime] NOT NULL,
	[isDelete] [int] NOT NULL,
	[isPromote] [int] NULL,
	[lastUpdate] [datetime] NOT NULL,
	[postage] [decimal](18, 0) NULL,
	[salesVolume] [int] NULL,
	[attrId] [int] NOT NULL,
 CONSTRAINT [PK_Goods] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[goodsImg]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[goodsImg](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[goodsCode] [int] NOT NULL,
	[goodsType] [int] NOT NULL,
	[imgUrl] [text] NOT NULL,
	[imgDesc] [text] NULL,
 CONSTRAINT [PK_fast_goods_img] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[goodsType]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[goodsType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[typeNarme] [varchar](50) NOT NULL,
	[enabled] [smallint] NOT NULL,
 CONSTRAINT [PK_fast_goods_type] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[income]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[income](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[incomeCode] [nvarchar](50) NOT NULL,
	[orderSn] [nvarchar](50) NOT NULL,
	[payType] [varchar](20) NOT NULL,
	[payName] [varchar](20) NOT NULL,
	[IncomeFee] [decimal](10, 2) NOT NULL,
	[createTime] [datetime] NOT NULL,
	[remark] [text] NULL,
 CONSTRAINT [PK_income] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InStorage]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InStorage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InStorageCode] [varchar](50) NOT NULL,
	[InType] [varchar](50) NOT NULL,
	[goodsSn] [nvarchar](50) NOT NULL,
	[SupplierID] [int] NOT NULL,
	[Num] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Amount] [float] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateUser] [varchar](50) NOT NULL,
	[CreateType] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Remark] [nvarchar](50) NULL,
 CONSTRAINT [PK_InStorage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[massage]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[massage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[massageCode] [nvarchar](50) NOT NULL,
	[userName] [nvarchar](50) NULL,
	[messageContent] [varchar](255) NULL,
	[messageTime] [datetime] NULL,
	[articleId] [nvarchar](50) NOT NULL,
	[isDelete] [int] NULL,
 CONSTRAINT [PK_massage] PRIMARY KEY CLUSTERED 
(
	[massageCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrderInfo]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[orderSn] [nvarchar](50) NOT NULL,
	[userId] [int] NOT NULL,
	[goodsSn] [nvarchar](50) NOT NULL,
	[orderStatus] [int] NOT NULL,
	[shippingStatus] [int] NULL,
	[consignee] [nvarchar](50) NOT NULL,
	[province] [nvarchar](20) NULL,
	[city] [nvarchar](20) NULL,
	[district] [nvarchar](20) NULL,
	[address] [nvarchar](50) NOT NULL,
	[zipcode] [nvarchar](50) NOT NULL,
	[tel] [nvarchar](50) NULL,
	[mobile] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[bestTime] [datetime] NULL,
	[payState] [int] NULL,
	[shippingName] [nvarchar](50) NULL,
	[payId] [int] NOT NULL,
	[goodsAmount] [decimal](18, 0) NULL,
	[shippingFee] [decimal](18, 0) NULL,
	[payFee] [decimal](18, 0) NULL,
	[buyNumber] [int] NULL,
	[integralMoney] [decimal](18, 0) NULL,
	[bonus] [decimal](18, 0) NULL,
	[orderAmount] [decimal](18, 0) NOT NULL,
	[deliverySn] [nvarchar](50) NULL,
	[addTime] [datetime] NOT NULL,
	[confirmTime] [datetime] NULL,
	[payTime] [datetime] NULL,
	[shippingTime] [datetime] NULL,
	[toBuyer] [nvarchar](50) NULL,
	[payNote] [nvarchar](50) NULL,
	[is_pickup] [int] NULL,
	[shippingEimeEnd] [int] NULL,
 CONSTRAINT [PK_hf_order_info] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[orderLog]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orderLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[orderSn] [nvarchar](50) NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Remark] [text] NOT NULL,
 CONSTRAINT [PK_orderLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[payment]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[payment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[payName] [varchar](20) NOT NULL,
 CONSTRAINT [PK_payment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[reply]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reply](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[commentCode] [int] NOT NULL,
	[commentA] [int] NULL,
	[commentB] [int] NULL,
	[content] [text] NULL,
	[replyDate] [datetime] NULL,
 CONSTRAINT [PK_reply] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Resource]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resource](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ResNum] [nvarchar](50) NOT NULL,
	[ResName] [nvarchar](50) NOT NULL,
	[ParentNum] [nvarchar](50) NULL,
	[img] [nvarchar](50) NULL,
	[url] [nvarchar](50) NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[role]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[roleNum] [varchar](64) NOT NULL,
	[roleName] [varchar](64) NOT NULL,
	[IsDelete] [int] NOT NULL,
	[createTime] [datetime] NOT NULL,
	[Remark] [nvarchar](200) NULL,
 CONSTRAINT [PK_hf_role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoleRight]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleRight](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[ResourceID] [int] NOT NULL,
 CONSTRAINT [PK_RoleRight] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[supplier]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[supplier](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[supplierCode] [nvarchar](50) NOT NULL,
	[supplierName] [nvarchar](50) NOT NULL,
	[companyName] [nvarchar](50) NULL,
	[address] [nvarchar](50) NULL,
	[tel] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[guimo] [nvarchar](50) NULL,
	[Remark] [nvarchar](200) NULL,
 CONSTRAINT [PK_hf_supplier] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[userAddress]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userAddress](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[consignee] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NULL,
	[province] [nvarchar](50) NOT NULL,
	[city] [nvarchar](50) NOT NULL,
	[district] [nvarchar](50) NOT NULL,
	[address] [nvarchar](50) NOT NULL,
	[zipcode] [nvarchar](50) NULL,
	[tel] [nvarchar](50) NOT NULL,
	[IsDelete] [int] NOT NULL,
 CONSTRAINT [PK_hf_user_address] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[Userid] [int] IDENTITY(1,1) NOT NULL,
	[LoginName] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[LoginPwd] [nvarchar](50) NOT NULL,
	[Sex] [int] NULL,
	[Age] [int] NULL,
	[Tel] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[RegditDate] [date] NOT NULL,
	[BirthDay] [date] NULL,
	[Address] [nvarchar](50) NOT NULL,
	[State] [int] NOT NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[Userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[XFgoodimg]    Script Date: 2020/7/8 11:14:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XFgoodimg](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[goodsCode] [int] NOT NULL,
	[goodsType] [int] NOT NULL,
	[imgUrl] [text] NOT NULL,
	[imgDesc] [text] NULL,
 CONSTRAINT [PK_FMgoodimg] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[accountLog] ON 

INSERT [dbo].[accountLog] ([ID], [userId], [tradeType], [tradeChannel], [tradeNo], [ouTradeNo], [incomeAmount], [gmtCreate], [gmtPayment], [remarks]) VALUES (3, 1, N'1', 1, N'z_001', N'2020629001', CAST(200 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), CAST(0x0000ABE900000000 AS DateTime), NULL)
INSERT [dbo].[accountLog] ([ID], [userId], [tradeType], [tradeChannel], [tradeNo], [ouTradeNo], [incomeAmount], [gmtCreate], [gmtPayment], [remarks]) VALUES (5, 2, N'1', 2, N'w_001', N'2020629001', CAST(150 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), CAST(0x0000ABE900000000 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[accountLog] OFF
INSERT [dbo].[AdminInfo] ([ID], [userCode], [userName], [realName], [password], [telephone], [Email], [createTime], [state], [roleCode], [Remark], [img]) VALUES (1, N'Sys0001', N'zhangsan', N'张三', N'123456', N'13408038576', N'13408038576@163.com', CAST(0x0000ABF000962C78 AS DateTime), 2, 1, NULL, N'/Areas/Admin/content/img/1.jpg')
INSERT [dbo].[AdminInfo] ([ID], [userCode], [userName], [realName], [password], [telephone], [Email], [createTime], [state], [roleCode], [Remark], [img]) VALUES (3, N'Sys0002', N'lisi', N'李四', N'123456', N'15528036071', N'15528036071@163.com', CAST(0x0000ABED0143165E AS DateTime), 1, 3, N'321312312312', N'../../../../Areas/Admin/content/img/avatar-2.jpg')
INSERT [dbo].[AdminInfo] ([ID], [userCode], [userName], [realName], [password], [telephone], [Email], [createTime], [state], [roleCode], [Remark], [img]) VALUES (4, N'sys0004', N'hjf', N'121', N'123', N'15729661900', N'3213', CAST(0x0000ABEE0112F706 AS DateTime), 2, 1, N'123', N'/Areas/Admin/content/img/huoying.jpg')
SET IDENTITY_INSERT [dbo].[adminLog] ON 

INSERT [dbo].[adminLog] ([ID], [logTime], [userId], [logInfo]) VALUES (1, CAST(0x0000ABF1013EF492 AS DateTime), N'Sys0001', N'登录')
INSERT [dbo].[adminLog] ([ID], [logTime], [userId], [logInfo]) VALUES (2, CAST(0x0000ABF1013F1547 AS DateTime), N'Sys0001', N'登录')
INSERT [dbo].[adminLog] ([ID], [logTime], [userId], [logInfo]) VALUES (3, CAST(0x0000ABF1013F4A2F AS DateTime), N'Sys0001', N'登录')
INSERT [dbo].[adminLog] ([ID], [logTime], [userId], [logInfo]) VALUES (4, CAST(0x0000ABF1014485F0 AS DateTime), N'Sys0001', N'登录')
INSERT [dbo].[adminLog] ([ID], [logTime], [userId], [logInfo]) VALUES (5, CAST(0x0000ABF101450799 AS DateTime), N'Sys0001', N'登录')
INSERT [dbo].[adminLog] ([ID], [logTime], [userId], [logInfo]) VALUES (6, CAST(0x0000ABF10145AA1C AS DateTime), N'Sys0001', N'登录')
INSERT [dbo].[adminLog] ([ID], [logTime], [userId], [logInfo]) VALUES (7, CAST(0x0000ABF10145DE0A AS DateTime), N'Sys0001', N'登录')
SET IDENTITY_INSERT [dbo].[adminLog] OFF
SET IDENTITY_INSERT [dbo].[adminState] ON 

INSERT [dbo].[adminState] ([ID], [name]) VALUES (1, N'正常')
INSERT [dbo].[adminState] ([ID], [name]) VALUES (2, N'无效')
SET IDENTITY_INSERT [dbo].[adminState] OFF
SET IDENTITY_INSERT [dbo].[Area] ON 

INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (1, N'四川省       ', 0)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (2, N'江苏省       ', 0)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (3, N'湖南省       ', 0)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (4, N'广东省       ', 0)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (5, N'海南省       ', 0)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (6, N'成都市       ', 1)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (7, N'自贡市       ', 1)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (8, N'绵阳市       ', 1)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (9, N'广元市       ', 1)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (10, N'锦江区       ', 6)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (11, N'青羊区       ', 6)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (12, N'金牛区       ', 6)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (13, N'武侯区       ', 6)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (14, N'自流井区      ', 7)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (15, N'大安区       ', 7)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (16, N'涪城区       ', 8)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (17, N'游仙区       ', 8)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (18, N'安州区       ', 8)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (19, N'利州区       ', 9)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (20, N'朝天区       ', 9)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (21, N'南京市       ', 2)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (22, N'徐州市       ', 2)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (23, N'玄武区       ', 21)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (24, N'浦口区       ', 21)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (25, N'云龙区       ', 22)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (26, N'泉山区       ', 22)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (27, N'长沙市       ', 3)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (28, N'衡阳市       ', 3)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (29, N'芙蓉区       ', 27)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (30, N'天心区       ', 27)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (31, N'珠晖区       ', 28)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (32, N'雁峰区       ', 28)
INSERT [dbo].[Area] ([ID], [Cityname], [PID]) VALUES (33, N'南岳区       ', 28)
SET IDENTITY_INSERT [dbo].[Area] OFF
SET IDENTITY_INSERT [dbo].[article] ON 

INSERT [dbo].[article] ([articleId], [articleCode], [Author], [content], [subject], [publishDate], [commentNo], [picture], [isDelete]) VALUES (1, N'000001', N'熊朝鲜', N'文章内容1', N'文章主题', CAST(0x0000ABEB00000000 AS DateTime), 0, N'../../assets/img/Vegetables/huangguamain.jpg', 0)
INSERT [dbo].[article] ([articleId], [articleCode], [Author], [content], [subject], [publishDate], [commentNo], [picture], [isDelete]) VALUES (2, N'000002', N'测试', N'文章内容2', N'主题2', CAST(0x0000ABEB00000000 AS DateTime), 0, N'../../assets/img/Vegetables/huangguamain.jpg', 0)
INSERT [dbo].[article] ([articleId], [articleCode], [Author], [content], [subject], [publishDate], [commentNo], [picture], [isDelete]) VALUES (3, N'000003', N'小花', N'《文章》包括策划似乎试试', N'文章', CAST(0x0000ABEB00000000 AS DateTime), 0, N'../../assets/img/Vegetables/huangguamain.jpg', 0)
SET IDENTITY_INSERT [dbo].[article] OFF
SET IDENTITY_INSERT [dbo].[attribute] ON 

INSERT [dbo].[attribute] ([attrId], [attrName], [attrValues], [attrType], [attrIndex], [sortOrder], [is_linked], [isDelete]) VALUES (1, N'重量', N'10kg', 0, 1, NULL, NULL, 0)
INSERT [dbo].[attribute] ([attrId], [attrName], [attrValues], [attrType], [attrIndex], [sortOrder], [is_linked], [isDelete]) VALUES (2, N'个数', N'5', 1, 1, NULL, NULL, 0)
INSERT [dbo].[attribute] ([attrId], [attrName], [attrValues], [attrType], [attrIndex], [sortOrder], [is_linked], [isDelete]) VALUES (3, N'毫升', N'100sml', 1, 1, NULL, NULL, 0)
INSERT [dbo].[attribute] ([attrId], [attrName], [attrValues], [attrType], [attrIndex], [sortOrder], [is_linked], [isDelete]) VALUES (4, N'测试属性78679', N'45', 1, 0, 2, 0, 1)
INSERT [dbo].[attribute] ([attrId], [attrName], [attrValues], [attrType], [attrIndex], [sortOrder], [is_linked], [isDelete]) VALUES (5, N'测试属性7880998', N'45', 1, 0, 3, 0, 1)
INSERT [dbo].[attribute] ([attrId], [attrName], [attrValues], [attrType], [attrIndex], [sortOrder], [is_linked], [isDelete]) VALUES (6, N'测试属性78789879', N'45', 1, 0, 3, 0, 1)
SET IDENTITY_INSERT [dbo].[attribute] OFF
SET IDENTITY_INSERT [dbo].[cart] ON 

INSERT [dbo].[cart] ([ID], [userId], [goodsSn], [goodsName], [marketPrice], [postage], [buyNumber], [totalPrice], [goodsType], [isGift], [isShipping], [shippingNum], [attrId], [addTime], [promotePrice], [thumbUrl], [enable], [state]) VALUES (13, 1, N'g_0007', N'猕猴桃', CAST(9 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), 3, CAST(27 AS Decimal(18, 0)), 3, 0, 2, 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(9 AS Decimal(18, 0)), N'../../assets/img/Key products/sg15.jpg', 0, N'未购买')
INSERT [dbo].[cart] ([ID], [userId], [goodsSn], [goodsName], [marketPrice], [postage], [buyNumber], [totalPrice], [goodsType], [isGift], [isShipping], [shippingNum], [attrId], [addTime], [promotePrice], [thumbUrl], [enable], [state]) VALUES (14, 1, N's_0003', N'茄子', CAST(4 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), 1, CAST(4 AS Decimal(18, 0)), 2, 0, 2, 0, 2, CAST(0x0000ABE900000000 AS DateTime), CAST(4 AS Decimal(18, 0)), N'../../assets/img/Vegetables/qiezimain1.jpg', 0, N'未购买')
SET IDENTITY_INSERT [dbo].[cart] OFF
SET IDENTITY_INSERT [dbo].[Contacts] ON 

INSERT [dbo].[Contacts] ([ID], [Name], [Email], [subject], [information]) VALUES (1, N'王麻子', N'4442534979@qq.com', N'网站非常好', N'gertyer')
SET IDENTITY_INSERT [dbo].[Contacts] OFF
SET IDENTITY_INSERT [dbo].[Goods] ON 

INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (3, 2, N's_0001', N'菠菜', 2, 1, 100, CAST(20 AS Decimal(18, 0)), CAST(6 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'波', NULL, 1, 1, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 12, 2)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (4, 2, N's_0002', N'胡萝卜', 2, 1, 104, CAST(10 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'胡', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 17, 2)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (5, 2, N's_0003', N'茄子', 2, 1, 105, CAST(10 AS Decimal(18, 0)), CAST(4 AS Decimal(18, 0)), CAST(4 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'茄', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 15, 2)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (6, 2, N's_0004', N'苋菜', 2, 1, 110, CAST(10 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'苋', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 13, 2)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (7, 2, N's_0005', N'西兰花', 2, 1, 113, CAST(20 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'西', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 12, 2)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (8, 2, N's_0006', N'西红柿', 2, 1, 110, CAST(6 AS Decimal(18, 0)), CAST(4 AS Decimal(18, 0)), CAST(4 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'红', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 15, 2)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (9, 3, N'g_0001', N'苹果', 13, 3, 70, CAST(6 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(7 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'苹', NULL, 1, 1, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 50, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (10, 3, N'g_0002', N'香蕉', 15, 3, 80, CAST(10 AS Decimal(18, 0)), CAST(9 AS Decimal(18, 0)), CAST(7 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'香', NULL, 1, 1, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 33, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (11, 3, N'g_0003', N'柑橘', 24, 3, 90, CAST(5 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(7 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'梨', NULL, 1, 1, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 35, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (14, 4, N'y_0001', N'龙虾', 32, 4, 50, CAST(8 AS Decimal(18, 0)), CAST(9 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'龙', NULL, 1, 1, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 35, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (15, 4, N'y_0002', N'小黄鱼', 35, 4, 60, CAST(9 AS Decimal(18, 0)), CAST(11 AS Decimal(18, 0)), CAST(10 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'黄', NULL, 1, 1, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 30, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (16, 4, N'y_0003', N'金昌鱼', 45, 4, 40, CAST(10 AS Decimal(18, 0)), CAST(12 AS Decimal(18, 0)), CAST(12 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'金', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 40, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (17, 4, N'y_0004', N'生蚝', 44, 4, 50, CAST(7 AS Decimal(18, 0)), CAST(15 AS Decimal(18, 0)), CAST(15 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'生', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 26, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (18, 4, N'y_0005', N'鱿鱼', 43, 4, 60, CAST(10 AS Decimal(18, 0)), CAST(14 AS Decimal(18, 0)), CAST(14 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'鱿', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 26, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (19, 5, N'r_0001', N'牛肉', 23, 5, 50, CAST(15 AS Decimal(18, 0)), CAST(28 AS Decimal(18, 0)), CAST(27 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'牛', NULL, 1, 1, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 24, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (20, 5, N'r_0002', N'羊肉', 33, 5, 60, CAST(15 AS Decimal(18, 0)), CAST(30 AS Decimal(18, 0)), CAST(30 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'羊', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 22, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (21, 5, N'r_0003', N'猪肉', 43, 5, 70, CAST(15 AS Decimal(18, 0)), CAST(29 AS Decimal(18, 0)), CAST(28 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'猪', NULL, 1, 1, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 26, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (22, 2, N's_0007', N'豌豆荚', 2, 1, 109, CAST(6 AS Decimal(18, 0)), CAST(4 AS Decimal(18, 0)), CAST(4 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'豌', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 15, 2)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (23, 2, N's_0008', N'黄瓜', 2, 1, 110, CAST(13 AS Decimal(18, 0)), CAST(4 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'黄', NULL, 1, 1, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 15, 2)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (24, 2, N's_0009', N'花菜', 2, 1, 110, CAST(14 AS Decimal(18, 0)), CAST(6 AS Decimal(18, 0)), CAST(6 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'花', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 15, 2)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (25, 2, N's_0010', N'白蘑菇', 2, 1, 110, CAST(5 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'白', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 15, 2)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (26, 2, N's_0011', N'西葫芦', 2, 1, 110, CAST(10 AS Decimal(18, 0)), CAST(4 AS Decimal(18, 0)), CAST(4 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'葫', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 15, 2)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (27, 2, N's_0012', N'辣椒', 2, 1, 110, CAST(6 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'辣', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 15, 2)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (28, 2, N's_0013', N'玉米', 2, 1, 110, CAST(10 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'玉', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 15, 2)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (29, 2, N's_0014', N'豇豆', 2, 1, 110, CAST(22 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'豇', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 15, 2)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (30, 3, N'g_0004', N'青桔', 25, 3, 99, CAST(12 AS Decimal(18, 0)), CAST(7 AS Decimal(18, 0)), CAST(7 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'青', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 37, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (31, 3, N'g_0005', N'橄榄', 25, 3, 99, CAST(9 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'橄', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 37, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (32, 3, N'g_0006', N'樱桃', 25, 3, 99, CAST(2 AS Decimal(18, 0)), CAST(9 AS Decimal(18, 0)), CAST(9 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'樱', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 37, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (33, 3, N'g_0007', N'猕猴桃', 25, 3, 99, CAST(8 AS Decimal(18, 0)), CAST(9 AS Decimal(18, 0)), CAST(9 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'猕', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 37, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (34, 3, N'g_0008', N'水蜜桃', 25, 3, 99, CAST(8 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'水', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 37, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (35, 3, N'g_0009', N'葡萄', 25, 3, 99, CAST(2 AS Decimal(18, 0)), CAST(6 AS Decimal(18, 0)), CAST(5 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'葡', NULL, 1, 1, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 37, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (36, 3, N'g_0010', N'柠檬', 25, 3, 99, CAST(8 AS Decimal(18, 0)), CAST(6 AS Decimal(18, 0)), CAST(6 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'柠', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 37, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (37, 3, N'g_0011', N'菠萝', 25, 3, 99, CAST(15 AS Decimal(18, 0)), CAST(6 AS Decimal(18, 0)), CAST(6 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'菠', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 37, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (38, 3, N'g_0012', N'哈密瓜', 25, 3, 97, CAST(15 AS Decimal(18, 0)), CAST(7 AS Decimal(18, 0)), CAST(7 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'哈', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 37, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (39, 3, N'g_0013', N'龙眼', 25, 3, 99, CAST(2 AS Decimal(18, 0)), CAST(9 AS Decimal(18, 0)), CAST(9 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'龙', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 37, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (40, 3, N'g_0014', N'百香果', 25, 3, 99, CAST(7 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'百', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 37, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (41, 4, N'y_0006', N'扇贝', 43, 4, 60, CAST(6 AS Decimal(18, 0)), CAST(12 AS Decimal(18, 0)), CAST(12 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'扇', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 26, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (42, 4, N'y_0007', N'三文鱼', 43, 4, 60, CAST(9 AS Decimal(18, 0)), CAST(15 AS Decimal(18, 0)), CAST(15 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'三', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 26, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (43, 4, N'y_0008', N'大闸蟹', 43, 4, 60, CAST(9 AS Decimal(18, 0)), CAST(17 AS Decimal(18, 0)), CAST(17 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'闸', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 26, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (44, 4, N'y_0009', N'皮皮虾', 43, 4, 60, CAST(8 AS Decimal(18, 0)), CAST(16 AS Decimal(18, 0)), CAST(16 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'皮', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 26, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (45, 5, N'r_0004', N'鸡肉', 33, 5, 60, CAST(15 AS Decimal(18, 0)), CAST(20 AS Decimal(18, 0)), CAST(20 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'鸡', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 22, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (46, 5, N'r_0005', N'鸭肉', 33, 5, 60, CAST(15 AS Decimal(18, 0)), CAST(22 AS Decimal(18, 0)), CAST(22 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'鸭', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 22, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (47, 3, N'g_0015', N'牛油果', 25, 3, 99, CAST(7 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'百', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 37, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (48, 3, N'g_0016', N'荔枝', 25, 3, 99, CAST(7 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'百', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 37, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (49, 2, N's_0015', N'包包菜', 2, 1, 110, CAST(22 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'豇', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 15, 2)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (50, 3, N'g_0015', N'芒果', 0, 3, 99, CAST(7 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(8 AS Decimal(18, 0)), CAST(0x0000ABE900000000 AS DateTime), N'百', NULL, 1, 2, CAST(0x0000ABE900000000 AS DateTime), 0, 1, CAST(0x0000ABE900000000 AS DateTime), CAST(0 AS Decimal(18, 0)), 37, 1)
INSERT [dbo].[Goods] ([ID], [goodType], [goodsSn], [goodsName], [clickCount], [supplier], [goodsNumber], [goodsWeight], [marketPrice], [promotePrice], [promoteStartDate], [keywords], [goodsDesc], [isOnSale], [isShipping], [addTime], [isDelete], [isPromote], [lastUpdate], [postage], [salesVolume], [attrId]) VALUES (58, 3, N'g_0016', N'测试', 5, 3, 9, CAST(4 AS Decimal(18, 0)), CAST(7 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0x0000ABED00CB3F40 AS DateTime), N'测', NULL, 1, 2, CAST(0x0000ABED00CB3F40 AS DateTime), 1, 2, CAST(0x0000ABED00CB3F40 AS DateTime), CAST(0 AS Decimal(18, 0)), 6, 2)
SET IDENTITY_INSERT [dbo].[Goods] OFF
SET IDENTITY_INSERT [dbo].[goodsImg] ON 

INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (1, 3, 2, N'../../assets/img/product/product8.jpg', N'菠菜（Spinacia oleracea L.）又bai名波斯菜,正宗有机养殖，新鲜健康，吃着新鲜，吃着放心')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (3, 4, 2, N'../../assets/img/Vegetables/huluobo12.jpg', N'老品种的胡萝卜，通常都只有一握粗细，红彤彤的，布满须根，下zhi部分叉。那形象，就像带着璎珞帽子的赤身小人儿，非常可爱。胡萝卜就像蒜苗、小葱一样，类似于菜肴的佐料，主要是用来作点缀的。乡下人偏爱红色，红色象征着喜庆、吉祥。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (5, 5, 2, N'../../assets/img/Vegetables/qiezimain1.jpg', N'茄子含多种维生素bai、蛋白质、糖及矿物质等。特别是茄子富含维生素Ｐ，含量最多的部位是紫色表皮和果肉的接合处，故茄子以紫色品种为上品。１0０克紫茄中维生素Ｐ的含量高达７２０毫克以上。维生素Ｐ能增强人体细胞间的黏着力，改善微细血管脆性，防止小血管出血。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (7, 6, 2, N'../../assets/img/Vegetables/zicaimain.jpg', N'一年生草bai本植物。叶对生，卵形或菱形，有绿紫du两色。花黄绿色。种子极小，黑色而有光泽。嫩苗可作蔬菜。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (9, 7, 2, N'../../assets/img/product/product5.jpg', N'西兰花为1-2年生草bai本植物，原产于地中du海东部沿岸地区，目前我国南zhi北方均有栽培，已成为日常主要蔬菜之一。西兰花营养丰富，含蛋白质、糖、脂肪、维生素和胡萝卜素，营养成份位居同类蔬菜之首，被誉为“蔬菜皇冠”。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (11, 8, 2, N'../../assets/img/product/product7.jpg', N'番茄红素具有独特的抗氧化能力，能清除自由基，保护细胞，使脱氧核糖核酸及基因免遭破坏，能阻止癌变进程。西红柿除了对前列腺癌有预防作用外，还能有效减少胰腺癌、直肠癌、喉癌、口腔癌、乳腺癌等癌症的发病危险。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (13, 9, 3, N'../../assets/img/product/product13.jpg', N'苹果是蔷薇科苹果亚科苹果属植物，其树为落叶乔木。苹果营养价值很高，富含矿物质和维生素，含钙量丰富，有助于代谢掉体内多余盐分，苹果酸可代谢热量，防止下半身肥胖。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (15, 10, 3, N'../../assets/img/Fruits/bannanamain.jpg', N'香蕉含有多种微量元素和维生素，能帮助肌肉松弛，使人身心愉悦 ，并具有一定的减肥功效。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (17, 11, 3, N'../../assets/img/Fruits/sg9.jpg', N'正宗广西沃柑当季新鲜柑橘 、水果皮薄甘甜多汁肉嫩')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (19, 14, 4, N'../../assets/img/Seafood/hx1main1.jpg', N'五洲嘉宾共赏太阳岛美景，四海亲朋同品十八香龙虾')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (21, 15, 4, N'../../assets/img/Seafood/xhymain.jpg', N'50米深度培养出95%的野生品质，三都澳福屿岛海捕小黄鱼，原生态的营养珍馐！')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (23, 16, 4, N'../../assets/img/Seafood/jcymain.jpg', N' 金鲳鱼肉质结实细嫩、无肉中刺，含脂肪多，味道浓厚，是优质的食用鱼类。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (25, 17, 4, N'../../assets/img/Seafood/shmain.jpg', N'生蚝富含高蛋白低脂肪，含有多种人体必需的氨基酸、微量元素、丰富的钙、铁、锌、硒营养丰富口感鲜美。实惠的价格，地道的口味、挑战味蕾新极限！')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (27, 18, 4, N'../../assets/img/Seafood/youyumain.jpg', N'鱿鱼除富含蛋白质和人体所需的氨基酸外，鱿鱼还含有大量的牛黄酸，可抑制血液中的胆固醇含量，缓解疲劳，恢复视力，改善肝脏功能；')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (29, 19, 5, N'../../assets/img/Key products/rlmain3.jpg', N'阉牛和小母牛肉质相似，但阉牛的脂肪更少，年纪大的母牛和公牛肉质粗硬，常用来做牛肉末，肉牛一般需要经过育肥，饲以谷物、膳食纤维、蛋白质、维生素和矿物质。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (31, 20, 5, N'../../assets/img/Key products/yrmain.jpg', N'羊肉鲜嫩，营养价值高，凡肾阳不足、腰膝酸软、腹中冷痛、虚劳不足者皆可用它作食疗品。羊肉营养丰富，对肺结核、气管炎、哮喘、贫血、产后气血两虚、腹部冷痛、体虚畏寒、营养不良等作用。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (33, 21, 5, N'../../assets/img/Meat/zrmain.jpg', N'肉的蛋白质含量最低，脂肪含量最高。瘦猪肉含蛋白质较高，每100克可含高达29克的蛋白质，含脂肪6克。经煮炖后，猪肉的脂肪含量还会降低。猪肉还含有丰富的维生素B．，可以使身体感到更有力气。猪肉还能提供人体必需的脂肪酸。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (35, 22, 2, N'../../assets/img/product/product9.jpg', N'新鲜豌豆，就地取材豌豆蛋白质营养价值不能充分发挥的原因是其可消化性较差。豌豆籽实营养丰富，含蛋白质23.4%，脂肪1.8%，碳水化合物52.6%，还含有胡萝卜素、维生素B1、B2等。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (37, 23, 2, N'../../assets/img/Vegetables/huangguamain.jpg', N'黄瓜味甘，甜、性凉、苦、无毒，入脾、胃、大肠；具有除热，利水利尿，清热解毒的功效；主治烦渴，咽喉肿痛，火眼，火烫伤。还有减肥功效。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (39, 24, 2, N'../../assets/img/Best selling products/bhuacai.jpg', N'花椰菜富含膳食纤维、蛋白质、维生素、脂肪、碳水化合物及矿物质等。其中胡萝卜素含量是大白菜的 8 倍，维生素 B2 的含量是大白菜的 2 倍。钙含量较高，堪与牛奶中的钙含量媲美。是含有类黄酮最多的食物之一。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (41, 25, 2, N'../../assets/img/Vegetables/mugu1.jpg', N'双孢蘑菇每100g鲜品中约含蛋白质3.7g、脂肪0.2g、糖3.0g、纤维素0.8g、磷110mg、钙9mg、铁0.6mg。双孢蘑菇含有多种氨基酸、核苷酸、维生素B1、维生素B2、维生素C、维生素PP、维生素D原等。双孢蘑菇所含的酪氨酶有明显降低血压作用，多糖的醌类化合物与巯基结合，可抑制脱氧核糖核酸合成，在医学上，有抑制肿瘤细胞活性的作用。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (43, 26, 2, N'../../assets/img/product/product18.jpg', N'西葫芦含有较多维生素C、葡萄糖等其他营养物质，
西葫芦
西葫芦
尤其是钙的含量极高。不同品种每100g可食部分（鲜重）营养物质含量如下：蛋白质0.6-0.9g，脂肪0.1-0.2g，纤维素0.8-0.9g，糖类2.5-3.3g，胡萝卜素20-40微克，维生素C 2.5-9毫克，钙22-29毫克。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (45, 27, 2, N'../../assets/img/Vegetables/lajiamain.jpg', N'青椒能增强人的体力，缓解因工作、生活压力造成的疲劳。其特有的味道和所含的辣椒素有刺激唾液和胃液分泌的作用，能增进食欲，帮助消化，促进肠蠕动，防止便秘。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (47, 28, 2, N'../../assets/img/Recently added/ymmain.jpg', N'玉米中的维生素含量非常高，是稻米、小麦的5-10倍，在所有主食中，玉米的营养价值和保健作用是最高的。玉米中含有的核黄素等高营养物质，对人体是十分有益的。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (49, 29, 2, N'../../assets/img/Recently added/gdmain.jpg', N'豇豆提供了易于消化吸收的优质蛋白质，适量的碳水化合物及多种维生素、微量元素等，可补充机体的营养素。豇豆的嫩豆荚和豆粒味道鲜美，食用方法多种多样，可炒、煮、炖、拌、做馅等。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (51, 30, 3, N'../../assets/img/product/product1.jpg', N'青橘子中丰富的营养成分有降血脂、抗动脉粥样硬化等作用，对于预防心血管疾病的发生大有益处。橘汁中含有一种名为“诺米林”的物质，具有抑制和杀死癌细胞的能力，对胃癌有预防作用。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (53, 31, 3, N'../../assets/img/product/product11.jpg', N'橄榄营养丰富，果肉内含蛋白质、碳水化合物、脂肪、维生素C以及钙、磷、铁等矿物质，其中维生素C的含量是苹果的10倍，梨、桃的5倍，含钙量也很高，且易被人体吸收，尤适于女性、儿童食用。冬春季节，每日嚼食两三枚鲜橄榄，可防止上呼吸道感染，故民间有“冬春橄榄赛人参”之誉。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (55, 32, 3, N'../../assets/img/Fruits/yintaomain.jpg', N'樱桃是色、香、味、形俱佳的鲜果，除了鲜食外，还可以加工制作成樱桃酱、樱桃汁、樱桃罐头和果脯、露酒等，具有艳红色泽，杏仁般的香气，食之使人迷醉。樱桃也是菜肴较好的配料。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (57, 33, 3, N'../../assets/img/Key products/sg15.jpg', N'猕猴桃含有丰富的矿物质，包括丰富的钙、磷、铁，还含有胡萝卜素和多种维生素，对保持人体健康具有重要的作用。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (59, 34, 3, N'../../assets/img/Fruits/smtmain.jpg', N'水蜜桃皮很薄，果肉丰富，宜于生食。刚熟的桃子硬而甜，熟透的桃子软而多汁。水蜜桃的种子，外壳呈长扁形，两端稍尖，表面疙疙瘩瘩的，比较硬。核由两片合成，敲开便可看到桃仁。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (61, 35, 3, N'../../assets/img/Fruits/ptmain.jpg', N'成熟的浆果中葡萄含糖量高达10%-30%，以葡萄糖为主。葡萄中的多种果酸有助于消化，适当多吃些葡萄，能健脾和胃。葡萄中含有矿物质钙、钾、磷、铁以及多种维生素B1、维生素B2、维生素B6、维生素C和维生素P等，还含有多种人体所需的氨基酸，常食葡萄对神经衰弱、疲劳过度大有裨益。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (63, 36, 3, N'../../assets/img/Fruits/sg1.jpg', N'柠檬富含维生素C、糖类、钙、磷、铁、维生素B1、维生素B2、烟
柠檬
柠檬(5张)
酸、奎宁酸、柠檬酸、苹果酸、橙皮苷、柚皮苷、香豆精、高量钾元素和低量钠元素等，对人体十分有益。维生素C能维持人体各种组织和细胞间质的生成，并保持它们正常的生理机能。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (65, 37, 3, N'../../assets/img/Best selling products/bomain.jpg', N'菠萝作为鲜食，肉色金黄，香味浓郁，甜酸适口，清脆多汁。菠萝果实除鲜食外，多用以制罐头，因其能保持原来风味而受到广泛喜爱。加工制品菠萝罐头被誉为“国际性果品罐头”，还可制成多种加工制品，广受消费者的欢迎。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (67, 38, 3, N'../../assets/img/Best selling products/hmgmain.jpg', N'其色泽金黄，滋味清香，还有令人愉快的水果滋味；充分保留了哈密瓜籽中独有的药用、美容、保健营养成分不受破坏。具有超强的抗氧能力，实属上等纯天然药用、美容、保健、营养精油，可广泛应用在医药、美容、保健、营养食品等领域。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (69, 39, 3, N'../../assets/img/Recently added/lymain.jpg', N'实营养丰富，是名贵的高级滋补品，龙眼药用始载于《神农本草经》性温，味甘，具有补益心脾、养血安神的功能。主治气血不足、心悸不宁、健忘失眠、血虚萎黄等症，以及中老年虚弱、高血压、高血脂和冠心病等。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (71, 40, 3, N'../../assets/img/Recently added/productbig5.jpg', N'天然纯净、浓郁甘美、酸甜可口，生津止渴；提神醒脑、润肠通便，预防大肠癌；补肾健身、排毒养颜；解酒护肝、消除疲劳；对高血压、高血糖降低胆固醇有功效；口腔溃疡、牙周炎、咽喉炎有功效；对结肠炎、肠胃病有功效；调节血糖、有助预防糖尿病；形成强健骨骼、舒缓骨质疏松；提高人体免疫力，预防疾病。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (73, 41, 4, N'../../assets/img/Seafood/sbmain.jpg', N'扇贝的蛋白质含量很高，干贝中高达55.6%。研究表明，贝类的必需氨基酸含量丰富且均衡，包括丙氨酸、苏氨酸、赖氨酸、组氨酸、精氨酸等人体内不能合成的氨基酸，并与陆上植物的必需氨基酸存在一定的互补性。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (75, 42, 4, N'../../assets/img/Seafood/swymain.jpg', N'三文鱼可降低患心脑血管病率，三文鱼富含的omega-3高不饱和脂肪酸，可以有效预防心脑血管疾病，对脑组织健康具有积极的作用。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (77, 43, 4, N'../../assets/img/Seafood/dzxmain.jpg', N'蟹肉之中，又分“四味”：大腿肉，丝短纤细，味同干贝；小腿肉，丝长细嫩，美如银鱼；蟹身肉，洁白晶莹，胜似白鱼；蟹黄，妙不可言，无法比喻。而蟹子曝干后则是海鲜珍品，为海鲜第一味。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (79, 44, 4, N'../../assets/img/Recently added/ppxmain.jpg', N'皮皮虾营养丰富，含蛋白质、氨基酸、脂肪、钙、磷等成分，具有滋补身体、通乳、消除“时差症”、缓解神经衰弱、增强免疫、预防动脉硬化等功效作用。其肉质含水分较多，肉味鲜甜嫩滑，淡而柔软，并且有一种特殊诱人的鲜味，因此很多人都非常喜爱吃皮皮虾。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (81, 45, 4, N'../../assets/img/Meat/jrmain.jpg', N'鸡肉含有维生素C、E等，蛋白质的含量比例较高，种类多，而且消化率高，很容易被人体吸收利用，有增强体力、强壮身体的作用，另外含有对人体生长发育有重要作用的磷脂类，是中国人膳食结构中脂肪和磷脂的重要来源之一。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (83, 46, 4, N'../../assets/img/Recently added/yrmain.jpg', N'鸭肉的营养价值很高，蛋白质含量比畜肉高得多。而鸭肉的脂肪、碳水化合物含量适中，特别是脂肪均匀地分布于全身组织中。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (85, 47, 3, N'../../assets/img/Key products/sg10.jpg', N'牛油果深受人们的喜爱，主要是因为其富含的营养成分极高，吉尼斯世界纪录甚至把牛油果评为最有营养的水果。牛油果含有多种维生素、丰富的脂肪酸、蛋白质以及大量钾、镁、钙等元素。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (87, 48, 3, N'../../assets/img/Fruits/lzmain.jpg', N'荔枝营养丰富，含葡萄糖、蔗糖、蛋白质、脂肪以及维生素A、B、C等，并含叶酸、精氨酸、色氨酸等各种营养素，对人体健康十分有益。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (89, 49, 2, N'../../assets/img/product/product14.jpg', N'甘蓝营养元素很丰富如优质蛋白，纤维素，矿物质，维生素等等，
紫甘蓝
紫甘蓝(5张)
吃紫甘蓝可以补充营养，强身健体。')
INSERT [dbo].[goodsImg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (91, 50, 3, N'../../assets/img/Fruits/mgmain.jpg', N'食用芒果具有清肠胃的功效，对于晕车、晕船有一定的止吐作
芒果干
芒果干(17张)
用。抗癌。据现代食疗观点而言，芒果含有大量的维生素A，因此具有防癌、抗癌的作用。美化肌肤。由于芒果中含有大量的维生素，因此经常食用芒果，可以起到滋润肌肤的作用。')
SET IDENTITY_INSERT [dbo].[goodsImg] OFF
SET IDENTITY_INSERT [dbo].[goodsType] ON 

INSERT [dbo].[goodsType] ([ID], [typeNarme], [enabled]) VALUES (2, N'蔬菜', 1)
INSERT [dbo].[goodsType] ([ID], [typeNarme], [enabled]) VALUES (3, N'水果', 1)
INSERT [dbo].[goodsType] ([ID], [typeNarme], [enabled]) VALUES (4, N'海鲜', 1)
INSERT [dbo].[goodsType] ([ID], [typeNarme], [enabled]) VALUES (5, N'鲜肉', 0)
INSERT [dbo].[goodsType] ([ID], [typeNarme], [enabled]) VALUES (6, N'测试新增', 0)
INSERT [dbo].[goodsType] ([ID], [typeNarme], [enabled]) VALUES (7, N'测试新增34', 0)
INSERT [dbo].[goodsType] ([ID], [typeNarme], [enabled]) VALUES (8, N'测试新增565', 0)
INSERT [dbo].[goodsType] ([ID], [typeNarme], [enabled]) VALUES (9, N'测试类别79', 0)
SET IDENTITY_INSERT [dbo].[goodsType] OFF
SET IDENTITY_INSERT [dbo].[massage] ON 

INSERT [dbo].[massage] ([ID], [massageCode], [userName], [messageContent], [messageTime], [articleId], [isDelete]) VALUES (1, N'000001', N'张三', N'评论内容1', CAST(0x0000ABEB00000000 AS DateTime), N'1', 0)
INSERT [dbo].[massage] ([ID], [massageCode], [userName], [messageContent], [messageTime], [articleId], [isDelete]) VALUES (3, N'000002', N'李四', N'评论内容2', CAST(0x0000ABEB00000000 AS DateTime), N'2', 0)
INSERT [dbo].[massage] ([ID], [massageCode], [userName], [messageContent], [messageTime], [articleId], [isDelete]) VALUES (4, N'000003', N'熊朝鲜', N'评论内容4', CAST(0x0000ABEB00000000 AS DateTime), N'3', 0)
INSERT [dbo].[massage] ([ID], [massageCode], [userName], [messageContent], [messageTime], [articleId], [isDelete]) VALUES (5, N'000004', N'王五', N'评论内容6', CAST(0x0000ABEB00000000 AS DateTime), N'4', 0)
INSERT [dbo].[massage] ([ID], [massageCode], [userName], [messageContent], [messageTime], [articleId], [isDelete]) VALUES (6, N'000005', N'王麻子', N'评论内容5', CAST(0x0000ABEB00000000 AS DateTime), N'5', 0)
INSERT [dbo].[massage] ([ID], [massageCode], [userName], [messageContent], [messageTime], [articleId], [isDelete]) VALUES (7, N'000006', N'王五455', N'测试一次788798090', CAST(0x0000ABEB016A5ECC AS DateTime), N'6', 1)
SET IDENTITY_INSERT [dbo].[massage] OFF
SET IDENTITY_INSERT [dbo].[orderLog] ON 

INSERT [dbo].[orderLog] ([ID], [orderSn], [CreateUser], [CreateTime], [Remark]) VALUES (1, N'Osn_0000019', N'Sys0001', CAST(0x0000ABF200980F89 AS DateTime), N'审核通过')
INSERT [dbo].[orderLog] ([ID], [orderSn], [CreateUser], [CreateTime], [Remark]) VALUES (2, N'Osn_0000020', N'Sys0001', CAST(0x0000ABF200981428 AS DateTime), N'审核通过')
INSERT [dbo].[orderLog] ([ID], [orderSn], [CreateUser], [CreateTime], [Remark]) VALUES (3, N'Osn_0000001', N'Sys0001', CAST(0x0000ABF200983DBE AS DateTime), N'审核通过')
INSERT [dbo].[orderLog] ([ID], [orderSn], [CreateUser], [CreateTime], [Remark]) VALUES (4, N'Osn_0000005', N'Sys0001', CAST(0x0000ABF2009865D2 AS DateTime), N'审核通过')
SET IDENTITY_INSERT [dbo].[orderLog] OFF
SET IDENTITY_INSERT [dbo].[payment] ON 

INSERT [dbo].[payment] ([ID], [payName]) VALUES (1, N'微信')
INSERT [dbo].[payment] ([ID], [payName]) VALUES (2, N'支付宝')
INSERT [dbo].[payment] ([ID], [payName]) VALUES (3, N'网银')
INSERT [dbo].[payment] ([ID], [payName]) VALUES (4, N'银行卡')
SET IDENTITY_INSERT [dbo].[payment] OFF
SET IDENTITY_INSERT [dbo].[Resource] ON 

INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (1, N'Sys_1000', N'商品模块', N'', N'icon-form', NULL)
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (2, N'Sys_2000', N'商品管理', N'Sys_1000', NULL, N'../ShopList/ShopManage')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (3, N'Sys_2001', N'商品评论管理', N'Sys_1000', NULL, N'../ShopReviews/ShopReviewsManage')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (4, N'Sys_2002', N'商品属性管理', N'Sys_1000', NULL, N'../ShopAttr/ShopAttr')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (5, N'Base_1000', N'销售模块', N'', N'fa fa-bar-chart', NULL)
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (6, N'Base_2000', N'促销管理', N'Base_1000', NULL, N'../Sales/Index')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (7, N'User_1000', N'内容模块', N'', N'icon-grid', NULL)
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (8, N'User_2000', N'文章管理', N'User_1000', N'', N'../ReportForm/Index')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (9, N'Storage_1000', N'订单模块', N'', N'icon-interface-windows', NULL)
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (10, N'Storage_2000', N'订单管理', N'Storage_1000', NULL, N'../Order/Index')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (11, N'Storage_2001', N'订单日志管理', N'Storage_1000', NULL, N'../OrderLog/Index')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (12, N'Storage_2002', N'订单审核管理', N'Storage_1000', NULL, N'../OrderAudit/Index')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (13, N'Report_1000', N'库存模块', N'', N'icon-interface-windows', NULL)
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (14, N'Report_2001', N'库存管理', N'Report_1000', NULL, N'../InStorage/Index')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (15, N'Report_2002', N'备货与发货', N'Report_1000', NULL, NULL)
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (16, N'Report_2003', N'退/换货', N'Report_1000', NULL, N'../backGoodsInfo/Index')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (17, N'Conme_1000', N'客户模块', N'', N'icon-mail', NULL)
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (18, N'Conme_2000', N'客户管理', N'Conme_1000', N'', N'../Customer/Index')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (19, N'RePart_1000', N'报表模块', N'', N'icon-grid', NULL)
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (20, N'RePart_2001', N'用户明细统计', N'RePart_1000', NULL, N'../UserInfo/userIn')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (21, N'RePart_2002', N'库存明细', N'RePart_1000', NULL, N'../Goods/MVCChart')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (22, N'RePart_2003', N'订单汇总', N'RePart_1000', NULL, N'../OrderInfo/MVCChart')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (23, N'RePart_2004', N'退/换明细', N'RePart_1000', NULL, N'../backGoods/MVCChart')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (24, N'All_1000', N'系统模块', N'', N'icon-flask', NULL)
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (25, N'All_2000', N'权限设置', N'All_1000', NULL, N'../Quanxian/Index')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (26, N'Sys_2003', N'商品类别管理', N'Sys_1000', NULL, N'../ShopTyeManage/ShopTyeManage')
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (27, N'Goods_1000', N'货源模块', N'', N'fa fa-bar-chart', NULL)
INSERT [dbo].[Resource] ([ID], [ResNum], [ResName], [ParentNum], [img], [url]) VALUES (28, N'Goods_2000', N'供应商管理', N'Goods_1000', NULL, N'../GoodsPlay/Index')
SET IDENTITY_INSERT [dbo].[Resource] OFF
SET IDENTITY_INSERT [dbo].[role] ON 

INSERT [dbo].[role] ([ID], [roleNum], [roleName], [IsDelete], [createTime], [Remark]) VALUES (1, N'A_001', N'管理员', 0, CAST(0x0000ABE900000000 AS DateTime), NULL)
INSERT [dbo].[role] ([ID], [roleNum], [roleName], [IsDelete], [createTime], [Remark]) VALUES (3, N'P_001', N'普通用户', 0, CAST(0x0000ABE900000000 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[role] OFF
SET IDENTITY_INSERT [dbo].[RoleRight] ON 

INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (83, 3, 6)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (102, 1, 2)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (103, 1, 3)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (104, 1, 4)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (105, 1, 26)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (106, 1, 6)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (107, 1, 8)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (108, 1, 10)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (109, 1, 11)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (110, 1, 12)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (111, 1, 14)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (112, 1, 15)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (113, 1, 16)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (114, 1, 18)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (115, 1, 20)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (116, 1, 21)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (117, 1, 22)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (118, 1, 23)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (119, 1, 25)
INSERT [dbo].[RoleRight] ([ID], [RoleId], [ResourceID]) VALUES (120, 1, 28)
SET IDENTITY_INSERT [dbo].[RoleRight] OFF
SET IDENTITY_INSERT [dbo].[supplier] ON 

INSERT [dbo].[supplier] ([ID], [supplierCode], [supplierName], [companyName], [address], [tel], [email], [guimo], [Remark]) VALUES (1, N's_001', N'四川蔬菜批发商', N'四川蔬菜批发商', N'四川成都', N'13098762354', N'13098762354@163.com', N'中小', NULL)
INSERT [dbo].[supplier] ([ID], [supplierCode], [supplierName], [companyName], [address], [tel], [email], [guimo], [Remark]) VALUES (3, N'g_001', N'四川九龙水果公司', N'四川九龙水果批发公司', N'四川成都', N'14809076452', N'14809076452@163.com', N'中小', NULL)
INSERT [dbo].[supplier] ([ID], [supplierCode], [supplierName], [companyName], [address], [tel], [email], [guimo], [Remark]) VALUES (4, N'y_001', N'四川海鲜批发商', N'四川海鲜公司', N'四川成都', N'13508069743', N'13508069743@163.com', N'中', NULL)
INSERT [dbo].[supplier] ([ID], [supplierCode], [supplierName], [companyName], [address], [tel], [email], [guimo], [Remark]) VALUES (5, N'r_001', N'四川大胖子批发', N'四川大胖子肉食公司', N'四川成都', N'15508764356', N'15508764356@163.com', N'中', NULL)
SET IDENTITY_INSERT [dbo].[supplier] OFF
SET IDENTITY_INSERT [dbo].[userAddress] ON 

INSERT [dbo].[userAddress] ([ID], [userId], [consignee], [email], [province], [city], [district], [address], [zipcode], [tel], [IsDelete]) VALUES (5, 1, N'舒珊珊', N'1962566967@qq.com', N'广东省', N'', N'', N'广东省广州市天河区太阳小区', N'000000', N'18030946061', 0)
SET IDENTITY_INSERT [dbo].[userAddress] OFF
SET IDENTITY_INSERT [dbo].[UserInfo] ON 

INSERT [dbo].[UserInfo] ([Userid], [LoginName], [UserName], [LoginPwd], [Sex], [Age], [Tel], [Email], [RegditDate], [BirthDay], [Address], [State]) VALUES (1, N'wangwu', N'王五', N'12345', 0, 21, N'14781872833', N'14781872833@163.com', CAST(0x44410B00 AS Date), CAST(0x59230B00 AS Date), N'四川成都', 1)
INSERT [dbo].[UserInfo] ([Userid], [LoginName], [UserName], [LoginPwd], [Sex], [Age], [Tel], [Email], [RegditDate], [BirthDay], [Address], [State]) VALUES (2, N'zsss', NULL, N'12345', NULL, NULL, N'15356479454', NULL, CAST(0x46410B00 AS Date), NULL, N'四川成都', 1)
INSERT [dbo].[UserInfo] ([Userid], [LoginName], [UserName], [LoginPwd], [Sex], [Age], [Tel], [Email], [RegditDate], [BirthDay], [Address], [State]) VALUES (3, N'qss', NULL, N'123456', NULL, NULL, N'15356479454', NULL, CAST(0x46410B00 AS Date), NULL, N'四川成都', 1)
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
SET IDENTITY_INSERT [dbo].[XFgoodimg] ON 

INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (2, 3, 2, N'../../assets/img/Vegetables/yangyangcai.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (4, 4, 2, N'../../assets/img/Vegetables/huluobo22.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (6, 5, 2, N'../../assets/img/Vegetables/qiezhi2.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (8, 6, 2, N'../../assets/img/Vegetables/xiancai.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (10, 7, 2, N'../../assets/img/Vegetables/huacai.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (12, 8, 2, N'../../assets/img/Vegetables/nushenguo.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (14, 9, 3, N'../../assets/img/Fruits/sg17.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (16, 10, 3, N'../../assets/img/Fruits/sg25.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (18, 11, 3, N'../../assets/img/Fruits/ganju.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (20, 14, 4, N'../../assets/img/Seafood/lx1.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (22, 15, 4, N'../../assets/img/Seafood/hx2.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (24, 16, 4, N'../../assets/img/Seafood/jcy.jfif', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (26, 17, 4, N'../../assets/img/Seafood/hx3.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (28, 18, 4, N'../../assets/img/Seafood/youyu2.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (30, 19, 5, N'../../assets/img/Key products/rl5.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (32, 20, 5, N'../../assets/img/Key products/yr1.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (34, 21, 5, N'../../assets/img/Meat/rl2.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (36, 22, 2, N'../../assets/img/Vegetables/wandou.png', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (38, 23, 2, N'../../assets/img/Vegetables/huanggua22.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (40, 24, 2, N'../../assets/img/Best selling products/bhuacai2.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (42, 25, 2, N'../../assets/img/Vegetables/mogu2.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (44, 26, 2, N'../../assets/img/Vegetables/xihulu (2).jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (46, 27, 2, N'../../assets/img/Vegetables/qinjiao.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (48, 28, 2, N'../../assets/img/Recently added/ym.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (50, 29, 2, N'../../assets/img/Recently added/gd.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (52, 30, 3, N'../../assets/img/Fruits/sg20.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (54, 31, 3, N'../../assets/img/Fruits/sg23.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (56, 32, 3, N'../../assets/img/Fruits/sg24.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (58, 33, 3, N'../../assets/img/Key products/sg16.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (60, 34, 3, N'../../assets/img/Fruits/sg266.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (62, 35, 3, N'../../assets/img/Fruits/sg22.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (64, 36, 3, N'../../assets/img/Fruits/sg18.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (66, 37, 3, N'../../assets/img/Best selling products/boluo2.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (68, 38, 3, N'../../assets/img/Best selling products/hmg.jfif', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (70, 39, 3, N'../../assets/img/Recently added/ly.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (72, 40, 3, N'../../assets/img/Recently added/bxg.jpeg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (74, 41, 4, N'../../assets/img/Seafood/sb2.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (76, 42, 4, N'../../assets/img/Seafood/swy1.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (78, 43, 4, N'../../assets/img/Seafood/dzx.jfif', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (80, 44, 4, N'../../assets/img/Recently added/ppx.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (82, 45, 4, N'../../assets/img/Meat/jir.jfif', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (84, 46, 4, N'../../assets/img/Recently added/yr.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (86, 47, 3, N'../../assets/img/Key products/sg11.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (88, 48, 3, N'../../assets/img/Fruits/sg3.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (90, 49, 2, N'../../assets/img/product/product15.jpg', N'鼠标悬浮出现页面页面')
INSERT [dbo].[XFgoodimg] ([ID], [goodsCode], [goodsType], [imgUrl], [imgDesc]) VALUES (92, 50, 3, N'../../assets/img/Fruits/sg7.jpg', N'鼠标悬浮出现页面页面')
SET IDENTITY_INSERT [dbo].[XFgoodimg] OFF
ALTER TABLE [dbo].[cart] ADD  CONSTRAINT [DF_cart_isGift]  DEFAULT ((0)) FOR [isGift]
GO
ALTER TABLE [dbo].[cart] ADD  CONSTRAINT [DF_cart_isShipping]  DEFAULT ((0)) FOR [isShipping]
GO
ALTER TABLE [dbo].[cart] ADD  CONSTRAINT [DF_cart_shippingNum]  DEFAULT ((0)) FOR [shippingNum]
GO
ALTER TABLE [dbo].[cart] ADD  CONSTRAINT [DF_cart_enable]  DEFAULT ((0)) FOR [enable]
GO
ALTER TABLE [dbo].[cart] ADD  CONSTRAINT [DF_cart_state]  DEFAULT ('未购买') FOR [state]
GO
ALTER TABLE [dbo].[userAddress] ADD  CONSTRAINT [DF_userAddress_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_state]  DEFAULT ((1)) FOR [State]
GO
ALTER TABLE [dbo].[AdminInfo]  WITH CHECK ADD  CONSTRAINT [FK_AdminInfo_adminState] FOREIGN KEY([state])
REFERENCES [dbo].[adminState] ([ID])
GO
ALTER TABLE [dbo].[AdminInfo] CHECK CONSTRAINT [FK_AdminInfo_adminState]
GO
ALTER TABLE [dbo].[AdminInfo]  WITH CHECK ADD  CONSTRAINT [FK_AdminInfo_role] FOREIGN KEY([roleCode])
REFERENCES [dbo].[role] ([ID])
GO
ALTER TABLE [dbo].[AdminInfo] CHECK CONSTRAINT [FK_AdminInfo_role]
GO
ALTER TABLE [dbo].[Goods]  WITH CHECK ADD  CONSTRAINT [FK_Goods_attribute] FOREIGN KEY([attrId])
REFERENCES [dbo].[attribute] ([attrId])
GO
ALTER TABLE [dbo].[Goods] CHECK CONSTRAINT [FK_Goods_attribute]
GO
ALTER TABLE [dbo].[Goods]  WITH CHECK ADD  CONSTRAINT [FK_Goods_goodsType] FOREIGN KEY([goodType])
REFERENCES [dbo].[goodsType] ([ID])
GO
ALTER TABLE [dbo].[Goods] CHECK CONSTRAINT [FK_Goods_goodsType]
GO
ALTER TABLE [dbo].[goodsImg]  WITH CHECK ADD  CONSTRAINT [FK_goodsImg_Goods] FOREIGN KEY([goodsCode])
REFERENCES [dbo].[Goods] ([ID])
GO
ALTER TABLE [dbo].[goodsImg] CHECK CONSTRAINT [FK_goodsImg_Goods]
GO
ALTER TABLE [dbo].[InStorage]  WITH CHECK ADD  CONSTRAINT [FK_InStorage_supplier] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[supplier] ([ID])
GO
ALTER TABLE [dbo].[InStorage] CHECK CONSTRAINT [FK_InStorage_supplier]
GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD  CONSTRAINT [FK_OrderInfo_UserInfo] FOREIGN KEY([userId])
REFERENCES [dbo].[UserInfo] ([Userid])
GO
ALTER TABLE [dbo].[OrderInfo] CHECK CONSTRAINT [FK_OrderInfo_UserInfo]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_adminState] FOREIGN KEY([State])
REFERENCES [dbo].[adminState] ([ID])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_adminState]
GO
ALTER TABLE [dbo].[XFgoodimg]  WITH CHECK ADD  CONSTRAINT [FK_XFgoodimg_Goods] FOREIGN KEY([goodsCode])
REFERENCES [dbo].[Goods] ([ID])
GO
ALTER TABLE [dbo].[XFgoodimg] CHECK CONSTRAINT [FK_XFgoodimg_Goods]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注册用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'accountLog', @level2type=N'COLUMN',@level2name=N'userId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'买家id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'accountLog', @level2type=N'COLUMN',@level2name=N'tradeType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'交易类型:1下订单，2充值，3购买礼物' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'accountLog', @level2type=N'COLUMN',@level2name=N'tradeChannel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'交易渠道:0账户余额，1支付宝，2微信' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'accountLog', @level2type=N'COLUMN',@level2name=N'tradeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'交易号（支付宝或微信）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'accountLog', @level2type=N'COLUMN',@level2name=N'ouTradeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商户订单号（交易单号）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'accountLog', @level2type=N'COLUMN',@level2name=N'incomeAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收入金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'accountLog', @level2type=N'COLUMN',@level2name=N'gmtCreate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支出金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'accountLog', @level2type=N'COLUMN',@level2name=N'gmtPayment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'交易备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'accountLog', @level2type=N'COLUMN',@level2name=N'remarks'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ρ1管理 员用户ID(分配)用户user商家suppli' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdminInfo', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'管理员用户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdminInfo', @level2type=N'COLUMN',@level2name=N'userName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'真实姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdminInfo', @level2type=N'COLUMN',@level2name=N'realName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdminInfo', @level2type=N'COLUMN',@level2name=N'password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdminInfo', @level2type=N'COLUMN',@level2name=N'telephone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用状态0香1是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdminInfo', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdminInfo', @level2type=N'COLUMN',@level2name=N'createTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdminInfo', @level2type=N'COLUMN',@level2name=N'state'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'article', @level2type=N'COLUMN',@level2name=N'Author'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'article', @level2type=N'COLUMN',@level2name=N'content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发表时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'article', @level2type=N'COLUMN',@level2name=N'publishDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论人数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'article', @level2type=N'COLUMN',@level2name=N'commentNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章照片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'article', @level2type=N'COLUMN',@level2name=N'picture'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'attribute', @level2type=N'COLUMN',@level2name=N'attrName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'选择输入。则ttr. name对应的值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'attribute', @level2type=N'COLUMN',@level2name=N'attrValues'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性是否多选; 0否; 1是如果可以多选则可以自定义属性' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'attribute', @level2type=N'COLUMN',@level2name=N'attrType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性是否可以检索:0不需要I关键字检索2范围检索' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'attribute', @level2type=N'COLUMN',@level2name=N'attrIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性显示的顺字数字越大越靠前，如果数字一样则按d顺' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'attribute', @level2type=N'COLUMN',@level2name=N'sortOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否关联0不关联1关联:如果关联那么用户在购买该作' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'attribute', @level2type=N'COLUMN',@level2name=N'is_linked'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0没有删除，1已经删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'attribute', @level2type=N'COLUMN',@level2name=N'isDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退货id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'backGoods', @level2type=N'COLUMN',@level2name=N'backCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'backGoods', @level2type=N'COLUMN',@level2name=N'goodsCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品id.取之于product表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'backGoods', @level2type=N'COLUMN',@level2name=N'backType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品序列号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'backGoods', @level2type=N'COLUMN',@level2name=N'backGoodsPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'backGoods', @level2type=N'COLUMN',@level2name=N'backgoodsNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'品牌名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'backGoods', @level2type=N'COLUMN',@level2name=N'statusBack'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品唯一-货号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'backGoods', @level2type=N'COLUMN',@level2name=N'statusRefund'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0可用 1不可用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cart', @level2type=N'COLUMN',@level2name=N'enable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品分类id，取值category的cat_id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'goodType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品的唯一货号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'goodsSn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品简介' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'goodsName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品点击数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'clickCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'供货人的名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'supplier'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品库存数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'goodsNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品的重量，以千克为单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'goodsWeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'市场售价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'marketPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'促销价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'promotePrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'促销价格开始日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'promoteStartDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品关键字，放在商品页的关键字中，为搜索引擎收录用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'keywords'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品的描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'goodsDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该商品是否上架销售，1，是；0，否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'isOnSale'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否促销2，否；1，是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'isShipping'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品的添加时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'addTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品是否已经删除，0，否；1，已删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'isDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否包邮；2，否；1，是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'isPromote'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最近一次更新商品配置的时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'lastUpdate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮递费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'postage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'销量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Goods', @level2type=N'COLUMN',@level2name=N'salesVolume'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'三级分类id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'goodsImg', @level2type=N'COLUMN',@level2name=N'goodsCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际图片url' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'goodsImg', @level2type=N'COLUMN',@level2name=N'imgDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类型id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'goodsType', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'goodsType', @level2type=N'COLUMN',@level2name=N'typeNarme'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型状态1.为可用: 0为不可用:不可用的类型,在渤' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'goodsType', @level2type=N'COLUMN',@level2name=N'enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单号，唯一' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'orderSn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id,同users的user_id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'userId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单状态 0未确认，1确认，2,已取消，3无效，4退货' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'orderStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品配送状态 0未发货，1已发货，2,已收货，4退货' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'shippingStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收货人的姓名 用户页面填写，默认取值 user_address' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'consignee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'省份，默认取值于表user_address' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'province'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'城市，默认取值于表user_address' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'city'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收货人地区，默认取值于表user_address' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'district'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'详细地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮编，默认取值于表user_address' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'zipcode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收货人电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'tel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收货人手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收货人电子邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最佳送货时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'bestTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户选择的配送方式id,取值表shipping' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'payState'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户选择的配送方式的名称,取值表shipping' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'shippingName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付方式的id,取值表payment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'payId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品的总金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'goodsAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'shippingFee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'payFee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已付款金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'buyNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用积分金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'integralMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用红包金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'bonus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单金额、应付款金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'orderAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发货单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'deliverySn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单生成时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'addTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单确认时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'confirmTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单支付时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'payTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单邮递时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'shippingTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商家给客户的留言,当该字段值时可以在订单查询看到' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'toBuyer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'付款备注, 在订单管理编辑修改' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'payNote'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮递完成时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderInfo', @level2type=N'COLUMN',@level2name=N'shippingEimeEnd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'reply', @level2type=N'COLUMN',@level2name=N'commentCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论方' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'reply', @level2type=N'COLUMN',@level2name=N'commentA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'被评论方' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'reply', @level2type=N'COLUMN',@level2name=N'commentB'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'reply', @level2type=N'COLUMN',@level2name=N'content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回复时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'reply', @level2type=N'COLUMN',@level2name=N'replyDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'供货商名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'supplier', @level2type=N'COLUMN',@level2name=N'supplierCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'店铺登陆密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'supplier', @level2type=N'COLUMN',@level2name=N'supplierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'supplier', @level2type=N'COLUMN',@level2name=N'companyName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司详细地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'supplier', @level2type=N'COLUMN',@level2name=N'address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'supplier', @level2type=N'COLUMN',@level2name=N'tel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子邮件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'supplier', @level2type=N'COLUMN',@level2name=N'email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司规模' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'supplier', @level2type=N'COLUMN',@level2name=N'guimo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0代表女，1代表男' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Sex'
GO
USE [master]
GO
ALTER DATABASE [Shop] SET  READ_WRITE 
GO
