USE [master]
GO
/****** Object:  Database [RState]    Script Date: 01-Apr-2020 04:48:24 AM ******/
CREATE DATABASE [RState]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WebLand', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\WebLand.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WebLand_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\WebLand_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [RState] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RState].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RState] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RState] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RState] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RState] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RState] SET ARITHABORT OFF 
GO
ALTER DATABASE [RState] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RState] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [RState] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RState] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RState] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RState] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RState] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RState] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RState] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RState] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RState] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RState] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RState] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RState] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RState] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RState] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RState] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RState] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RState] SET RECOVERY FULL 
GO
ALTER DATABASE [RState] SET  MULTI_USER 
GO
ALTER DATABASE [RState] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RState] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RState] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RState] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [RState]
GO
/****** Object:  StoredProcedure [dbo].[sp_getLogin]    Script Date: 01-Apr-2020 04:48:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_getLogin]
  @usr nvarchar(256),
  @pwd nvarchar(256)
  as
  SELECT u.Id,u.Name,u.Email,u.UserPic,u.HashPwd
  FROM [dbo].[Tb_Users] u
  WHERE u.Email = @usr and u.HashPwd = @Pwd
GO
/****** Object:  StoredProcedure [dbo].[sp_getPropList]    Script Date: 01-Apr-2020 04:48:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_getPropList] as
SELECT b.Nob,p.Id,p.Title,p.Location,p.Price,(p.NoShare-b.Nob) as NoShare,p.ExpDate,p.Discount,p.Criteria,p.OfferStart,p.OfferEnd,p.Notes
FROM [dbo].[Tb_Properties] p,(select PropId,isnull(count(Id),0) Nob 
	FROM [dbo].[Tb_Bookings] WHERE IsAllow='1' group by PropId) b 
WHERE p.Id = b.PropId and b.Nob < p.NoShare and GETDATE() < p.ExpDate
order by p.Id desc;
GO
/****** Object:  StoredProcedure [dbo].[sp_getUserList]    Script Date: 01-Apr-2020 04:48:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_getUserList] as
SELECT b.PropId,u.Id,u.Name,u.CellNo,u.Email,u.City,u.Address,u.UserPic,b.InvAmt,b.PostedOn
 FROM [dbo].[Tb_Users] u,[dbo].[Tb_Bookings] b
WHERE u.Id = b.UserId and b.IsAllow = '1' and b.PropId = (select max(id) FROM [dbo].[Tb_Properties]);

  

GO
/****** Object:  Table [dbo].[Tb_Bookings]    Script Date: 01-Apr-2020 04:48:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Bookings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PropId] [int] NOT NULL,
	[InvAmt] [decimal](18, 2) NULL,
	[IsAllow] [bit] NOT NULL,
	[PostedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.PkBooking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Pictures]    Script Date: 01-Apr-2020 04:48:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_Pictures](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PropId] [int] NOT NULL,
	[PicUrl] [nvarchar](50) NOT NULL,
 CONSTRAINT [PkPict] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tb_Properties]    Script Date: 01-Apr-2020 04:48:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tb_Properties](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Location] [nvarchar](256) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[NoShare] [int] NOT NULL,
	[ExpDate] [datetime] NOT NULL,
	[Discount] [decimal](18, 0) NULL,
	[Criteria] [varchar](5) NULL,
	[OfferStart] [datetime] NULL,
	[OfferEnd] [datetime] NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.PkProp] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tb_Users]    Script Date: 01-Apr-2020 04:48:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tb_Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CellNo] [varchar](30) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[City] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[Address] [nvarchar](256) NULL,
	[Share] [decimal](18, 2) NULL,
	[Status] [bit] NOT NULL,
	[Role] [varchar](50) NULL,
	[UserPic] [nvarchar](max) NULL,
	[HashPwd] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.PkUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Tb_Bookings] ON 

INSERT [dbo].[Tb_Bookings] ([Id], [UserId], [PropId], [InvAmt], [IsAllow], [PostedOn]) VALUES (5, 1, 1, CAST(100000.00 AS Decimal(18, 2)), 1, CAST(0x0000AB7F01746B10 AS DateTime))
INSERT [dbo].[Tb_Bookings] ([Id], [UserId], [PropId], [InvAmt], [IsAllow], [PostedOn]) VALUES (1002, 2, 1, CAST(100000.00 AS Decimal(18, 2)), 1, CAST(0x0000AB8B006A88F4 AS DateTime))
SET IDENTITY_INSERT [dbo].[Tb_Bookings] OFF
SET IDENTITY_INSERT [dbo].[Tb_Pictures] ON 

INSERT [dbo].[Tb_Pictures] ([Id], [PropId], [PicUrl]) VALUES (2, 1, N'img_bg_2_vnuiJ5YNPr.jpg')
INSERT [dbo].[Tb_Pictures] ([Id], [PropId], [PicUrl]) VALUES (3, 1, N'20200328_020103782_RFvLKk4lJQ8p.jpg')
INSERT [dbo].[Tb_Pictures] ([Id], [PropId], [PicUrl]) VALUES (4, 1, N'20200328_020912132_iSVvhUrJ4g.jpg')
INSERT [dbo].[Tb_Pictures] ([Id], [PropId], [PicUrl]) VALUES (5, 1, N'20200328_020921332_bnIFiyKJeo.jpg')
INSERT [dbo].[Tb_Pictures] ([Id], [PropId], [PicUrl]) VALUES (6, 1, N'20200328_020925192_d6hvzLNJ3K.jpg')
INSERT [dbo].[Tb_Pictures] ([Id], [PropId], [PicUrl]) VALUES (7, 1, N'20200328_022436362_7WUwNRZOT1.jpg')
INSERT [dbo].[Tb_Pictures] ([Id], [PropId], [PicUrl]) VALUES (8, 1, N'20200328_022436412_91elfz5Qub.jpg')
SET IDENTITY_INSERT [dbo].[Tb_Pictures] OFF
SET IDENTITY_INSERT [dbo].[Tb_Properties] ON 

INSERT [dbo].[Tb_Properties] ([Id], [Title], [Location], [Price], [NoShare], [ExpDate], [Discount], [Criteria], [OfferStart], [OfferEnd], [Notes]) VALUES (1, N'Properties Base On Test', N'Home', CAST(999999.00 AS Decimal(18, 2)), 10, CAST(0x0000ABAD00000000 AS DateTime), CAST(0 AS Decimal(18, 0)), N'%', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Tb_Properties] OFF
SET IDENTITY_INSERT [dbo].[Tb_Users] ON 

INSERT [dbo].[Tb_Users] ([Id], [Name], [CellNo], [Email], [City], [Country], [Address], [Share], [Status], [Role], [UserPic], [HashPwd]) VALUES (1, N'Sumon Mahbub', N'01773419147', N'smsbd9@gmail.com', N'Dhaka', N'Bangladesh', N'Gulshan-2, Dhaka', CAST(10.00 AS Decimal(18, 2)), 1, N'Admin', N'20191216_140945.jpg', N'smsbd9@yahoo.com')
INSERT [dbo].[Tb_Users] ([Id], [Name], [CellNo], [Email], [City], [Country], [Address], [Share], [Status], [Role], [UserPic], [HashPwd]) VALUES (2, N'Sumon Mahbub Shah', N'01773419147', N'smsbd@gmail.com', N'Dhaka', N'Bangladesh', N'Gulshan-2, Dhaka', CAST(10.00 AS Decimal(18, 2)), 1, N'Admin', N'20191216140945_LCq0PtI6kv.jpg', N'123456')
SET IDENTITY_INSERT [dbo].[Tb_Users] OFF
ALTER TABLE [dbo].[Tb_Bookings] ADD  CONSTRAINT [DfBookInv]  DEFAULT ((0)) FOR [InvAmt]
GO
ALTER TABLE [dbo].[Tb_Bookings] ADD  CONSTRAINT [DfBookAllow]  DEFAULT ((0)) FOR [IsAllow]
GO
ALTER TABLE [dbo].[Tb_Bookings] ADD  CONSTRAINT [DfBookPost]  DEFAULT (getdate()) FOR [PostedOn]
GO
ALTER TABLE [dbo].[Tb_Properties] ADD  CONSTRAINT [DfDiscount]  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [dbo].[Tb_Properties] ADD  CONSTRAINT [DfCriteria]  DEFAULT ('%') FOR [Criteria]
GO
ALTER TABLE [dbo].[Tb_Users] ADD  CONSTRAINT [DfUserStatus]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Tb_Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Prop] FOREIGN KEY([PropId])
REFERENCES [dbo].[Tb_Properties] ([Id])
GO
ALTER TABLE [dbo].[Tb_Bookings] CHECK CONSTRAINT [FK_Booking_Prop]
GO
ALTER TABLE [dbo].[Tb_Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Booking_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Tb_Users] ([Id])
GO
ALTER TABLE [dbo].[Tb_Bookings] CHECK CONSTRAINT [FK_Booking_User]
GO
ALTER TABLE [dbo].[Tb_Pictures]  WITH CHECK ADD  CONSTRAINT [FK_Pic_Prop] FOREIGN KEY([PropId])
REFERENCES [dbo].[Tb_Properties] ([Id])
GO
ALTER TABLE [dbo].[Tb_Pictures] CHECK CONSTRAINT [FK_Pic_Prop]
GO
USE [master]
GO
ALTER DATABASE [RState] SET  READ_WRITE 
GO
