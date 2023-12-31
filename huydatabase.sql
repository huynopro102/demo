USE [QLSP]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 9/27/2023 2:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] NOT NULL,
	[CategoryName] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 9/27/2023 2:04:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [nchar](10) NOT NULL,
	[ProductName] [nchar](10) NULL,
	[Price] [int] NULL,
	[Category] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (1, N'do kho    ')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (2, N'do uot    ')
GO
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Category]) VALUES (N'2         ', N'2         ', 2, 2)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Category]) VALUES (N'3         ', N'3         ', 3, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Category]) VALUES (N'4         ', N'4         ', 4, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Category]) VALUES (N'd         ', N'f         ', 6, 2)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Category]) VALUES (N'huy123    ', N'1         ', 1, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Category]) VALUES (N'huy1234   ', N'sp huy123 ', 1000, 2)
GO
