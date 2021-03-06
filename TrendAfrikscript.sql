USE [master]
GO
/****** Object:  Database [TrendAfriqOnlineCatalogue]    Script Date: 25/10/2017 11:51:51 ******/
CREATE DATABASE [TrendAfriqOnlineCatalogue]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TrendAfriqOnlineCatalogue', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQL2012\MSSQL\DATA\TrendAfriqOnlineCatalogue.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TrendAfriqOnlineCatalogue_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQL2012\MSSQL\DATA\TrendAfriqOnlineCatalogue_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TrendAfriqOnlineCatalogue].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET ARITHABORT OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET RECOVERY FULL 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET  MULTI_USER 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [TrendAfriqOnlineCatalogue]
GO
/****** Object:  User [trendafriq]    Script Date: 25/10/2017 11:51:53 ******/
CREATE USER [trendafriq] FOR LOGIN [trendafriq] WITH DEFAULT_SCHEMA=[db_owner]
GO
/****** Object:  User [NT AUTHORITY\SYSTEM]    Script Date: 25/10/2017 11:51:54 ******/
CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
/****** Object:  StoredProcedure [dbo].[spAddCatalogue]    Script Date: 25/10/2017 11:51:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAddCatalogue]
	@CatalogueName varchar(350)
	AS
BEGIN
	Insert into tblCatalogue(catalogueName) values(@CatalogueName);
END
GO
/****** Object:  StoredProcedure [dbo].[spAddEnquiryToDB]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAddEnquiryToDB]
	@Comment varchar(500),
	@Email varchar(100),
	@FirstName varchar(50),
	@LastName varchar(50)
	AS
BEGIN
	Insert into tblContactUs(Comment,
	Email,
	FirstName,
	LastName, EnquiryDate) values(@Comment,
	@Email,
	@FirstName,
	@LastName,
	getdate());
END
GO
/****** Object:  StoredProcedure [dbo].[spAddSellerDetails]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAddSellerDetails] 
	@sellerAddress varchar(500),
	@sellerExpiryDate smalldatetime,
	@sellerName varchar(500),
	@sellerPhone varchar(500),
	@sellerEmail varchar(550),
	@sellerWebsite varchar(500)
	
	AS
BEGIN
	Insert into tblSellerDetails(sellerExpiryDate, sellerName, sellerPhone, sellerAddress, sellerEmail, sellerWebsite) 
	values(@sellerExpiryDate, @sellerName, @sellerPhone, @sellerAddress, @sellerEmail, @sellerWebsite);
END
GO
/****** Object:  StoredProcedure [dbo].[spAddSellerItems]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAddSellerItems]
	@itemAlternatePicName varchar(150),
	@itemDescription varchar(550),
	@itemName varchar(550),
	@itemPicture varchar(350),
	@itemPrice varchar(150),
	@catalogueId decimal(18,0),
	@sellerId decimal(18,0),
	@pictureA varchar(250),
	@pictureB varchar(250),
	@pictureC varchar(250)
	AS
BEGIN
--Declare @newid decimal(18,0)=null;

	Insert into tblSellerItems(catalogueId, itemAlternatePicName, itemDescription,itemName, itemPicture, itemPrice,sellerId, pictureA, pictureB, pictureC) 
	values(@catalogueId, @itemAlternatePicName, @itemDescription,@itemName, @itemPicture, @itemPrice,@sellerId, @pictureA, @pictureB, @pictureC);

	--select @newid = scope_identity();

	--insert into tblSellerSubItems(pictureA, pictureB, pictureC, itemId)
	--values(@pictureA, @pictureB, @pictureC,@newid);
END
GO
/****** Object:  StoredProcedure [dbo].[spDeleteCatalogue]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spDeleteCatalogue]
	@itemId decimal(18,0)
	AS
BEGIN
	delete from tblSellerItems where itemId=@itemId;

END

GO
/****** Object:  StoredProcedure [dbo].[spGetAboutToExpireAdvertSlots]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetAboutToExpireAdvertSlots]
	AS
BEGIN
	Select a.*,datediff(day, getdate(), advertExpiryDate) as daysLeftToExpire  from tblSellerDetails a 
	left join tblSellerItems b on a.sellerId=b.sellerId where advertExpiryDate is not null and
	 datediff(day, getdate(), advertExpiryDate  )<=3 and datediff(day, getdate(), advertExpiryDate  )>=0  ;
END

GO
/****** Object:  StoredProcedure [dbo].[spGetAboutToExpiredClients]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetAboutToExpiredClients]
	AS
BEGIN
	Select *,datediff(day, getdate(), sellerExpiryDate) as daysLeftToExpire  from tblSellerDetails where datediff(day, getdate(), sellerExpiryDate  )>=0 and datediff(day, getdate(), sellerExpiryDate  )<=5 ;
END

GO
/****** Object:  StoredProcedure [dbo].[spGetAdminDetails]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetAdminDetails] 
	
AS
BEGIN
	Select * from tblAdminLogon;
END

GO
/****** Object:  StoredProcedure [dbo].[spGetAllCatalogueType]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetAllCatalogueType] 
	
AS
BEGIN
	Select * from tblCatalogue;
END

GO
/****** Object:  StoredProcedure [dbo].[spGetAllSellerDetails]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetAllSellerDetails] 

AS
BEGIN
Select * from tblSellerDetails;
	END

GO
/****** Object:  StoredProcedure [dbo].[spGetAllSellerItems]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetAllSellerItems]
	AS
BEGIN
	Select * from tblSellerItems ;
	
END

GO
/****** Object:  StoredProcedure [dbo].[spGetItemsForCatalogueType]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetItemsForCatalogueType] 
	@catalogueId decimal(18,0)
	AS
BEGIN
	select a.*, b.sellerName,b.sellerAddress, b.sellerPhone, b.sellerEmail, b.sellerWebsite from tblSellerItems a left join tblSellerDetails b on a.sellerId = b.sellerId
	where datediff(day, getdate(), b.sellerExpiryDate  )>=0 and a.catalogueId = @catalogueId;

	--select a.* from tblSellerItems a where  a.catalogueId = @catalogueId and datediff(day, b.sellerExpiryDate,  getdate())>=0;
END

GO
/****** Object:  StoredProcedure [dbo].[spGetItemsForSearchRequest]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetItemsForSearchRequest] 
	 @searchItem varchar(250)
	AS
BEGIN
	select a.*, b.sellerName,b.sellerAddress, b.sellerPhone, b.sellerEmail from tblSellerItems a 
	left join tblSellerDetails b on a.sellerId = b.sellerId
	where datediff(day, getdate(), b.sellerExpiryDate  )>=0 and  Upper(a.itemName) like '%' + Upper(@searchItem) +'%';

	--select a.* from tblSellerItems a where  a.catalogueId = @catalogueId and datediff(day, b.sellerExpiryDate,  getdate())>=0;
END

GO
/****** Object:  StoredProcedure [dbo].[spGetSellerItemsToChange]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetSellerItemsToChange]
@id int
	AS
BEGIN
	Select * from tblSellerItems where itemId = @id;
	
END

GO
/****** Object:  StoredProcedure [dbo].[spSaveChangesCatalogue]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spSaveChangesCatalogue] 
	@catalogueName varchar(550),
	@catalogueId decimal(18,0)
	AS
BEGIN
	update tblCatalogue set catalogueName=@catalogueName where catalogueId =@catalogueId;
END

GO
/****** Object:  StoredProcedure [dbo].[spSaveChangesSellerDetails]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spSaveChangesSellerDetails]
	@sellerAddress varchar(550),
	@sellerExpiryDate smalldatetime,
	@sellerName varchar(550),
	@sellerPhone varchar(150),
	@sellerId decimal(18,0),
	@sellerEmail varchar(550),
	@sellerWebsite varchar(500)
	AS
BEGIN
	update tblSellerDetails set sellerExpiryDate=@sellerExpiryDate , sellerName=@sellerName, sellerPhone= @sellerPhone,
	sellerAddress=@sellerAddress, sellerEmail=@sellerEmail, sellerWebsite=@sellerWebsite
	where sellerId =@sellerId;
END

GO
/****** Object:  StoredProcedure [dbo].[spSaveChangesSellerItems]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spSaveChangesSellerItems] 
	@itemAlternatePicName varchar(150),
	@itemDescription varchar(550),
	@itemName varchar(250),
	@itemPicture varchar(250),
	@itemPrice varchar(150),
	@catalogueId decimal(18,0),
	@sellerId decimal(18,0),
	@pictureA varchar(250),
	@pictureB varchar(250),
	@pictureC varchar(250),
	@itemId decimal(18,0)
	AS
BEGIN
	update tblSellerItems set catalogueId=@catalogueId, itemAlternatePicName=@itemAlternatePicName, itemPicture=@itemPicture,
	itemPrice=@itemPrice, sellerId=@sellerId, itemDescription= @itemDescription, itemName=@itemName,
	pictureA=@pictureA, pictureB=@pictureB, pictureC=@pictureC
	 where itemId =@itemId;

	 --update tblSellerSubItems set pictureA=@pictureA, pictureB=@pictureB, pictureC=@pictureC
	 --where itemId =@itemId
END

GO
/****** Object:  Table [dbo].[tblAdminLogon]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAdminLogon](
	[AdminEmail] [varchar](250) NULL,
	[LoginEmail] [varchar](250) NULL,
	[LoginPwd] [varchar](250) NULL,
	[AcctNumber] [varchar](15) NULL,
	[AcctName] [varchar](150) NULL,
	[AcctBank] [varchar](150) NULL,
	[AdminPhone] [varchar](15) NULL,
	[SubscriptionFee] [varchar](10) NULL,
	[AdvertSubscriptionFee] [varchar](10) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblCatalogue]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblCatalogue](
	[catalogueId] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[catalogueName] [varchar](250) NULL,
 CONSTRAINT [PK_tblCatalogue] PRIMARY KEY CLUSTERED 
(
	[catalogueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblContactUs]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblContactUs](
	[ContactUsId] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[EnquiryDate] [smalldatetime] NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](150) NULL,
	[Comment] [varchar](500) NULL,
 CONSTRAINT [PK_tblContactUs] PRIMARY KEY CLUSTERED 
(
	[ContactUsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSellerDetails]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSellerDetails](
	[sellerId] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[sellerName] [varchar](550) NULL,
	[sellerAddress] [varchar](950) NULL,
	[sellerPhone] [varchar](60) NULL,
	[sellerExpiryDate] [smalldatetime] NULL,
	[sellerEmail] [varchar](550) NULL,
	[sellerWebsite] [varchar](500) NULL,
 CONSTRAINT [PK_tblSellerDetails] PRIMARY KEY CLUSTERED 
(
	[sellerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSellerItems]    Script Date: 25/10/2017 11:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSellerItems](
	[itemId] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[itemName] [varchar](350) NULL,
	[itemDescription] [varchar](950) NULL,
	[catalogueId] [decimal](18, 0) NULL,
	[sellerId] [decimal](18, 0) NULL,
	[itemPrice] [varchar](50) NULL,
	[itemPicture] [varchar](350) NULL,
	[itemAlternatePicName] [varchar](50) NULL,
	[pictureA] [varchar](350) NULL,
	[pictureB] [varchar](350) NULL,
	[pictureC] [varchar](350) NULL,
	[advertExpiryDate] [smalldatetime] NULL,
 CONSTRAINT [PK_tblSellerItems] PRIMARY KEY CLUSTERED 
(
	[itemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[tblAdminLogon] ([AdminEmail], [LoginEmail], [LoginPwd], [AcctNumber], [AcctName], [AcctBank], [AdminPhone], [SubscriptionFee], [AdvertSubscriptionFee]) VALUES (N'info@trendafrik.com', N'info@trendafrik.com', N'Asd123!@#', N'0025023618', N'Quicky Sales Ltd', N'Diamond Bank', N'08120042929', N'5,000', N'1,000')
SET IDENTITY_INSERT [dbo].[tblCatalogue] ON 

INSERT [dbo].[tblCatalogue] ([catalogueId], [catalogueName]) VALUES (CAST(1 AS Decimal(18, 0)), N'Laces')
INSERT [dbo].[tblCatalogue] ([catalogueId], [catalogueName]) VALUES (CAST(2 AS Decimal(18, 0)), N'Jewelry')
INSERT [dbo].[tblCatalogue] ([catalogueId], [catalogueName]) VALUES (CAST(3 AS Decimal(18, 0)), N'Bags')
INSERT [dbo].[tblCatalogue] ([catalogueId], [catalogueName]) VALUES (CAST(4 AS Decimal(18, 0)), N'Hollandais')
INSERT [dbo].[tblCatalogue] ([catalogueId], [catalogueName]) VALUES (CAST(5 AS Decimal(18, 0)), N'Shedah')
INSERT [dbo].[tblCatalogue] ([catalogueId], [catalogueName]) VALUES (CAST(6 AS Decimal(18, 0)), N'Brocade')
INSERT [dbo].[tblCatalogue] ([catalogueId], [catalogueName]) VALUES (CAST(7 AS Decimal(18, 0)), N'Advert')
SET IDENTITY_INSERT [dbo].[tblCatalogue] OFF
SET IDENTITY_INSERT [dbo].[tblContactUs] ON 

INSERT [dbo].[tblContactUs] ([ContactUsId], [EnquiryDate], [FirstName], [LastName], [Email], [Comment]) VALUES (CAST(1 AS Decimal(18, 0)), CAST(0xA5C302B3 AS SmallDateTime), N'zimpi', N'sfdsfsd', N'sfdds@sfsdf.com', N'regermg ermg erg ergergefgergergergrgre')
SET IDENTITY_INSERT [dbo].[tblContactUs] OFF
SET IDENTITY_INSERT [dbo].[tblSellerDetails] ON 

INSERT [dbo].[tblSellerDetails] ([sellerId], [sellerName], [sellerAddress], [sellerPhone], [sellerExpiryDate], [sellerEmail], [sellerWebsite]) VALUES (CAST(2 AS Decimal(18, 0)), N'TrendAfriq', N'Abuja, Leg Qtrs', N'99999999999', CAST(0xA6230000 AS SmallDateTime), N'zimpik@yahoo.com', NULL)
INSERT [dbo].[tblSellerDetails] ([sellerId], [sellerName], [sellerAddress], [sellerPhone], [sellerExpiryDate], [sellerEmail], [sellerWebsite]) VALUES (CAST(3 AS Decimal(18, 0)), N'Advert', N'Advert', N'99999999999', CAST(0xA4B50000 AS SmallDateTime), N'zimpik@yahoo.com', NULL)
SET IDENTITY_INSERT [dbo].[tblSellerDetails] OFF
SET IDENTITY_INSERT [dbo].[tblSellerItems] ON 

INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(2 AS Decimal(18, 0)), N'Claudette Collection', N'Claudette Collection', CAST(7 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'9000', N'~/Images/claudette_2.png', N'Claudette', N'~/Images/claudette_2.png', N'~/Images/claudette_2.png', N'~/Images/claudette_2.png', CAST(0xA5CD0000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(3 AS Decimal(18, 0)), N'Blue Rounded Open Lace', N'Blue Rounded design open lace', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'550 per yard', N'~/Images/netlace43.jpg', N'Rounded blue lace', N'~/Images/netlace43.jpg', N'~/Images/netlace43.jpg', N'~/Images/netlace43.jpg', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(4 AS Decimal(18, 0)), N'Lovely Necklace', N'Lovely african made necklace', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'1500', N'~/Images/netlace50.jpg', N'colour supreme', N'~/Images/netlace50.jpg', N'~/Images/netlace50.jpg', N'~/Images/netlace50.jpg', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(5 AS Decimal(18, 0)), N'Sabjoz_ad', N'Beauty at Your Finger tip', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'Negotiable', N'~/Images/sabjoz_6.png', N'Sabjoz', N'~/Images/sabjoz_25.png', N'~/Images/sabjoz_26.png', N'~/Images/sabjoz_2.png', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(6 AS Decimal(18, 0)), N'Sabjoz_ad', N'Beauty at Your Finger tip', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'Negotiable', N'~/Images/sabjoz_7.png', N'SabJoz', N'~/Images/sabjoz_3.png', N'~/Images/sabjoz_7.png', N'~/Images/sabjoz_3.png', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(7 AS Decimal(18, 0)), N'White Gold Lace', N'Open White Lace with Gold endings and trimmings', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'1400', N'~/Images/beadss35.jpg', N'White', N'~/Images/beadss35.jpg', N'~/Images/beadss35.jpg', N'~/Images/beadss35.jpg', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(8 AS Decimal(18, 0)), N'MakeUpRicci', N'MakeUpRicci', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'Negotiable', N'~/Images/makeupricci_3.png', N'MakeUpRicci', N'~/Images/makeupricci_3.png', N'~/Images/makeupricci_3.png', N'~/Images/makeupricci_3.png', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(9 AS Decimal(18, 0)), N'Blue Rounded Open Lace', N'Blue Rounded design open lace', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'550', N'~/Images/netlace50.jpg', N'Rounded blue lace', N'~/Images/netlace50.jpg', N'~/Images/netlace50.jpg', N'~/Images/netlace50.jpg', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(10 AS Decimal(18, 0)), N'Colour Supreme', N'Main Yellow and earthy colours', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'1500', N'~/Images/netlace43.jpg', N'colour supreme', N'~/Images/netlace43.jpg', N'~/Images/netlace43.jpg', N'~/Images/netlace43.jpg', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(11 AS Decimal(18, 0)), N'Golden blue lace', N'Golden blue open lace', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'1400', N'~/Images/netlace69.jpg', N'bluegold', N'~/Images/netlace69.jpg', N'~/Images/netlace69.jpg', N'~/Images/netlace69.jpg', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(12 AS Decimal(18, 0)), N'Sabjoz_ad', N'Beauty at Your Finger tip', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'Negotiable', N'~/Images/sabjoz_6.png', N'sabjoz', N'~/Images/sabjoz_25.png', N'~/Images/sabjoz_26.png', N'~/Images/sabjoz_2.png', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(13 AS Decimal(18, 0)), N'Claudette Collection', N'High Heel Pom Pom Black Shoes', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'9000', N'~/Images/claudette_2.png', N'Claudette Collection', N'~/Images/claudette_2.png', N'~/Images/claudette_2.png', N'~/Images/claudette_2.png', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(14 AS Decimal(18, 0)), N'Blue Rounded Open Lace', N'Blue Rounded design open lace', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'550', N'~/Images/ta_6.png', N'Rounded blue lace', N'~/Images/ta_6.png', N'~/Images/ta_6.png', N'~/Images/ta_6.png', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(15 AS Decimal(18, 0)), N'Colour Supreme', N'Main Yellow and earthy colours', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'1500', N'~/Images/ta_7.png', N'colour supreme', N'~/Images/ta_7.png', N'~/Images/ta_7.png', N'~/Images/ta_7.png', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(16 AS Decimal(18, 0)), N'Sabjoz_ad', N'Beauty at Your Finger tip', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'Negotiable', N'~/Images/sabjoz_6.png', N'Claudette Collection', N'~/Images/claudette_2.png', N'~/Images/claudette_2.png', N'~/Images/claudette_2.png', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(17 AS Decimal(18, 0)), N'Claudette Collection', N'High Heel Pom Pom Black Shoes', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'9000', N'~/Images/claudette_2.png', N'green', N'~/Images/ta_5.png', N'~/Images/ta_5.png', N'~/Images/ta_5.png', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(18 AS Decimal(18, 0)), N'White Gold Lace', N'Open White Lace with Gold endings and trimmings', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'1400', N'~/Images/ta_6.png', N'White', N'~/Images/ta_6.png', N'~/Images/ta_6.png', N'~/Images/ta_6.png', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(19 AS Decimal(18, 0)), N'Deep Blue Open Lace', N'Deep Blue Open Lace, with beautiful endings and frails', CAST(1 AS Decimal(18, 0)), CAST(3 AS Decimal(18, 0)), N'1500', N'~/Images/ta_7.png', N'Blue lace', N'~/Images/ta_7.png', N'~/Images/ta_7.png', N'~/Images/ta_7.png', CAST(0xA6290000 AS SmallDateTime))
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(20 AS Decimal(18, 0)), N'Blue Rounded Open Lace', N'Blue Rounded design open lace', CAST(1 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), N'550 per yard', N'~/Images/roundbluelace.png', N'Rounded blue lace', N'~/Images/roundbluelace_B.png', N'~/Images/roundbluelace.png', N'~/Images/roundbluelace_B.png', NULL)
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(21 AS Decimal(18, 0)), N'Colour Supreme', N'Main Yellow and earthy colours', CAST(1 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), N'1500 per yard', N'~/Images/colourfullace.png', N'colour supreme', N'~/Images/colourfullace.png', N'~/Images/colourfullace.png', N'~/Images/colourfullace.png', NULL)
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(22 AS Decimal(18, 0)), N'Golden blue lace', N'Golden blue open lace', CAST(1 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), N'1400 per yard', N'~/Images/blungoldlace.png', N'bluegold', N'~/Images/blungoldlace.png', N'~/Images/blungoldlace.png', N'~/Images/blungoldlace.png', NULL)
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(23 AS Decimal(18, 0)), N'Flowery Green Lace', N'Open Flowery Green Shine Lace', CAST(1 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), N'2100 per yard', N'~/Images/flowerygreenlace.png', N'green', N'~/Images/flowerygreenlace.png', N'~/Images/flowerygreenlace.png', N'~/Images/flowerygreenlace.png', NULL)
INSERT [dbo].[tblSellerItems] ([itemId], [itemName], [itemDescription], [catalogueId], [sellerId], [itemPrice], [itemPicture], [itemAlternatePicName], [pictureA], [pictureB], [pictureC], [advertExpiryDate]) VALUES (CAST(24 AS Decimal(18, 0)), N'White Gold Lace', N'Open White Lace with Gold endings and trimmings', CAST(1 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), N'1400 per yard', N'~/Images/whitengoldlace.png', N'White', N'~/Images/whitengoldlace.png', N'~/Images/whitengoldlace.png', N'~/Images/whitengoldlace.png', NULL)
SET IDENTITY_INSERT [dbo].[tblSellerItems] OFF
ALTER TABLE [dbo].[tblSellerItems]  WITH CHECK ADD  CONSTRAINT [FK__tblSeller__catal__1B0907CE] FOREIGN KEY([catalogueId])
REFERENCES [dbo].[tblCatalogue] ([catalogueId])
GO
ALTER TABLE [dbo].[tblSellerItems] CHECK CONSTRAINT [FK__tblSeller__catal__1B0907CE]
GO
ALTER TABLE [dbo].[tblSellerItems]  WITH CHECK ADD  CONSTRAINT [FK__tblSeller__selle__1BFD2C07] FOREIGN KEY([sellerId])
REFERENCES [dbo].[tblSellerDetails] ([sellerId])
GO
ALTER TABLE [dbo].[tblSellerItems] CHECK CONSTRAINT [FK__tblSeller__selle__1BFD2C07]
GO
USE [master]
GO
ALTER DATABASE [TrendAfriqOnlineCatalogue] SET  READ_WRITE 
GO
