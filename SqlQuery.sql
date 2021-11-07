USE [master]
GO
/****** Object:  Database [Identity]  ******/

CREATE DATABASE [Identity]

GO

GO
USE [Identity]
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/*  
    "email": "user@example.com",
	"password": "user@example.com"	
*/
INSERT [dbo].[users] ( [FirstName], [LastName], [UserName], [Email], [PasswordHash], [PasswordSalt]) VALUES ( N'firstName', N'lastName', N'user@example.com', N'user@example.com', 0xB53B85C70DC1BE38E31FDE73BCEDFB779AD9C2BE3FD308C80BB152D0470D360D9AF2D47848817D9743BD466B04AFE00DFFBC3304E2D067FCD30F40A8C057B1A8, 0xFCEBD24E9997B6A5A976CDE1CEEF6A3F5CF0C236DB287AAC38D44025106C167FFB5F182D2D7B6E758A8FFA76E53749DED7A6E4686B6649CE5AEBDFC974874D986A688E11AFD722D42E3AD2F554B80CEF313159FD74B423D75EED1CBC32B51975D17368F916033861071D864D73CEA5B0084E5C9B7EB0B77F3362EEFC64D3EAB9)

/****** Object:  Database [Catalog]	 ******/
CREATE DATABASE [Catalog]
GO

GO
USE [Catalog]
GO

CREATE TABLE [dbo].[products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](250) NULL,
	[price] [decimal](18, 2) NOT NULL,
	[stock] [decimal](18, 2) NOT NULL CONSTRAINT [DF_products_stock]  DEFAULT ((0)),
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


INSERT [dbo].[products] ( [name], [description], [price], [stock]) VALUES ( N'test', NULL, CAST(12.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[products] ( [name], [description], [price], [stock]) VALUES ( N'test2', NULL, CAST(3.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))

/****** Object:  Database [Purchase]	 ******/
CREATE DATABASE [Purchases]

GO

GO
USE [Purchases]
GO

CREATE TABLE [Purchases](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [PurchaseDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[PurchaseId] [int] NULL,
 CONSTRAINT [PK_PurchaseDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]