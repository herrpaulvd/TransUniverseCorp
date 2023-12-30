USE [master]
GO
/****** Object:  Database [TransUniverseDB]    Script Date: 27.12.2023 19:27:21 ******/
CREATE DATABASE [TransUniverseDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TransUniverseDB', FILENAME = N'/home/TransUniverseDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TransUniverseDB_log', FILENAME = N'/home/TransUniverseDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TransUniverseDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TransUniverseDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TransUniverseDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TransUniverseDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TransUniverseDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TransUniverseDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TransUniverseDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TransUniverseDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [TransUniverseDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TransUniverseDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TransUniverseDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TransUniverseDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TransUniverseDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TransUniverseDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TransUniverseDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TransUniverseDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TransUniverseDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TransUniverseDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TransUniverseDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TransUniverseDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TransUniverseDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TransUniverseDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TransUniverseDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TransUniverseDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TransUniverseDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TransUniverseDB] SET  MULTI_USER 
GO
ALTER DATABASE [TransUniverseDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TransUniverseDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TransUniverseDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TransUniverseDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TransUniverseDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TransUniverseDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TransUniverseDB] SET QUERY_STORE = OFF
GO
USE [TransUniverseDB]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 27.12.2023 19:27:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](128) NOT NULL,
	[address] [varchar](128) NOT NULL,
	[email] [varchar](128) NOT NULL,
	[corporative] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Drivers]    Script Date: 27.12.2023 19:27:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drivers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](128) NOT NULL,
	[address] [varchar](128) NOT NULL,
	[email] [varchar](128) NOT NULL,
	[qualificationClasses] [int] NOT NULL,
	[spaceshipClasses] [int] NOT NULL,
	[hiringCost] [bigint] NOT NULL,
	[currentState] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Edges]    Script Date: 27.12.2023 19:27:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Edges](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[start] [int] NOT NULL,
	[end] [int] NOT NULL,
	[time] [bigint] NOT NULL,
	[spaceshipClasses] [int] NOT NULL,
	[qualificationClasses] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 27.12.2023 19:27:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loadingTime] [bigint] NOT NULL,
	[loadingPort] [int] NOT NULL,
	[unloadingTime] [bigint] NOT NULL,
	[unloadingPort] [int] NOT NULL,
	[volume] [bigint] NOT NULL,
	[totalCost] [bigint] NOT NULL,
	[totalTime] [bigint] NOT NULL,
	[spaceship] [int] NOT NULL,
	[driver] [int] NOT NULL,
	[customer] [int] NOT NULL,
	[currentState] [int] NOT NULL,
	[status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScheduleElements]    Script Date: 27.12.2023 19:27:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScheduleElements](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[departureOrArrival] [bigint] NULL,
	[plannedDepartureOrArrival] [bigint] NULL,
	[order] [int] NULL,
	[spaceship] [int] NULL,
	[driver] [int] NULL,
	[destinationOrStop] [int] NULL,
	[isStop] [bit] NOT NULL,
	[time] [bigint] NOT NULL,
	[next] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpaceObjects]    Script Date: 27.12.2023 19:27:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpaceObjects](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](128) NOT NULL,
	[description] [varchar](1024) NOT NULL,
	[kind] [int] NOT NULL,
	[systemCenter] [int] NULL,
	[systemPosition] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpacePorts]    Script Date: 27.12.2023 19:27:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpacePorts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](128) NOT NULL,
	[description] [varchar](1024) NOT NULL,
	[planet] [int] NOT NULL,
	[longtitude] [float] NOT NULL,
	[latitude] [float] NOT NULL,
	[altitude] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Spaceships]    Script Date: 27.12.2023 19:27:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spaceships](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](128) NOT NULL,
	[model] [varchar](128) NOT NULL,
	[class] [int] NOT NULL,
	[usageCost] [bigint] NOT NULL,
	[volume] [bigint] NOT NULL,
	[currentState] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 27.12.2023 19:27:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[login] [varchar](128) NOT NULL,
	[passwordHash] [bigint] NOT NULL,
	[customer] [int] NULL,
	[driver] [int] NULL,
	[roles] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 
GO
INSERT [dbo].[Customers] ([id], [name], [address], [email], [corporative]) VALUES (1, N'Leonid Mironov', N'Super7', N'mironov@bk.ru', 0)
GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Drivers] ON 
GO
INSERT [dbo].[Drivers] ([id], [name], [address], [email], [qualificationClasses], [spaceshipClasses], [hiringCost], [currentState]) VALUES (1, N'Ivan Lankin', N'Dubki', N'ivanlankin@gmail.com', 16, 16, 3, 26)
GO
SET IDENTITY_INSERT [dbo].[Drivers] OFF
GO
SET IDENTITY_INSERT [dbo].[Edges] ON 
GO
INSERT [dbo].[Edges] ([id], [start], [end], [time], [spaceshipClasses], [qualificationClasses]) VALUES (1, 2, 3, 8640000000000, 16, 16)
GO
INSERT [dbo].[Edges] ([id], [start], [end], [time], [spaceshipClasses], [qualificationClasses]) VALUES (2, 3, 5, 43200000000000, 16, 16)
GO
INSERT [dbo].[Edges] ([id], [start], [end], [time], [spaceshipClasses], [qualificationClasses]) VALUES (3, 2, 5, 34560000000000, 16, 16)
GO
INSERT [dbo].[Edges] ([id], [start], [end], [time], [spaceshipClasses], [qualificationClasses]) VALUES (4, 2, 4, 17280000000000, 16, 16)
GO
INSERT [dbo].[Edges] ([id], [start], [end], [time], [spaceshipClasses], [qualificationClasses]) VALUES (5, 4, 5, 12960000000000, 16, 16)
GO
INSERT [dbo].[Edges] ([id], [start], [end], [time], [spaceshipClasses], [qualificationClasses]) VALUES (6, 5, 2, 51840000000000, 16, 16)
GO
SET IDENTITY_INSERT [dbo].[Edges] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 
GO
INSERT [dbo].[Orders] ([id], [loadingTime], [loadingPort], [unloadingTime], [unloadingPort], [volume], [totalCost], [totalTime], [spaceship], [driver], [customer], [currentState], [status]) VALUES (1, 638397324000000000, 1, 638686692000000000, 4, 3, 238350184, 297937733939310, 1, 1, 1, 6, 1)
GO
INSERT [dbo].[Orders] ([id], [loadingTime], [loadingPort], [unloadingTime], [unloadingPort], [volume], [totalCost], [totalTime], [spaceship], [driver], [customer], [currentState], [status]) VALUES (2, 638713548000000000, 1, 639002052000000000, 4, 3, 490631384, 613289239203505, 1, 1, 1, 12, 1)
GO
INSERT [dbo].[Orders] ([id], [loadingTime], [loadingPort], [unloadingTime], [unloadingPort], [volume], [totalCost], [totalTime], [spaceship], [driver], [customer], [currentState], [status]) VALUES (3, 638713548000000000, 1, 639002052000000000, 4, 3, 490629928, 613287417619793, 1, 1, 1, 18, 1)
GO
INSERT [dbo].[Orders] ([id], [loadingTime], [loadingPort], [unloadingTime], [unloadingPort], [volume], [totalCost], [totalTime], [spaceship], [driver], [customer], [currentState], [status]) VALUES (4, 638713548000000000, 1, 639002052000000000, 4, 3, 489519032, 611898797611461, 1, 1, 1, 24, 1)
GO
INSERT [dbo].[Orders] ([id], [loadingTime], [loadingPort], [unloadingTime], [unloadingPort], [volume], [totalCost], [totalTime], [spaceship], [driver], [customer], [currentState], [status]) VALUES (5, 638740332000000000, 1, 639002052000000000, 4, 3, 487170224, 608962789357597, 1, 1, 1, 26, 0)
GO
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[ScheduleElements] ON 
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (1, 638387816340956067, NULL, NULL, NULL, 1, 1, 1, 0, NULL)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (2, 638387816340000000, NULL, NULL, 1, NULL, 1, 1, 0, NULL)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (3, NULL, 638397324000000000, 1, 1, 1, 1, 1, 8569733939310, 6)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (4, NULL, 638414604000000000, 1, 1, 1, 3, 0, 17280000000000, NULL)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (5, NULL, 638427564000000000, 1, 1, 1, 4, 0, 12960000000000, NULL)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (6, NULL, 638686692000000000, 1, 1, 1, 4, 1, 259128000000000, NULL)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (7, NULL, NULL, NULL, 1, 1, 4, 1, 0, NULL)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (8, NULL, 638440602760796495, 2, 1, 1, 1, 0, 51840000000000, 12)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (9, NULL, 638713548000000000, 2, 1, 1, 1, 1, 272945239203505, NULL)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (10, NULL, 638730828000000000, 2, 1, 1, 3, 0, 17280000000000, NULL)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (11, NULL, 638743788000000000, 2, 1, 1, 4, 0, 12960000000000, NULL)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (12, NULL, 639002052000000000, 2, 1, 1, 4, 1, 258264000000000, NULL)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (13, NULL, NULL, NULL, 1, 1, 4, 1, 0, NULL)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (14, NULL, 638440604582380207, 3, 1, 1, 1, 0, 51840000000000, 15)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (15, NULL, 638713548000000000, 3, 1, 1, 1, 1, 272943417619793, 16)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (16, NULL, 638730828000000000, 3, 1, 1, 3, 0, 17280000000000, 17)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (17, NULL, 638743788000000000, 3, 1, 1, 4, 0, 12960000000000, 18)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (18, NULL, 639002052000000000, 3, 1, 1, 4, 1, 258264000000000, NULL)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (19, NULL, NULL, NULL, 1, 1, 4, 1, 0, NULL)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (20, NULL, 638441993202388539, 4, 1, 1, 1, 0, 51840000000000, 21)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (21, NULL, 638713548000000000, 4, 1, 1, 1, 1, 271554797611461, 22)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (22, NULL, 638730828000000000, 4, 1, 1, 3, 0, 17280000000000, 23)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (23, NULL, 638743788000000000, 4, 1, 1, 4, 0, 12960000000000, 24)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (24, NULL, 639002052000000000, 4, 1, 1, 4, 1, 258264000000000, NULL)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (25, NULL, NULL, NULL, 1, 1, 4, 1, 0, NULL)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (26, NULL, 638444929210642403, 5, 1, 1, 1, 0, 51840000000000, 27)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (27, NULL, 638740332000000000, 5, 1, 1, 1, 1, 295402789357597, 28)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (28, NULL, 638757612000000000, 5, 1, 1, 3, 0, 17280000000000, 29)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (29, NULL, 638770572000000000, 5, 1, 1, 4, 0, 12960000000000, 30)
GO
INSERT [dbo].[ScheduleElements] ([id], [departureOrArrival], [plannedDepartureOrArrival], [order], [spaceship], [driver], [destinationOrStop], [isStop], [time], [next]) VALUES (30, NULL, 639002052000000000, 5, 1, 1, 4, 1, 231480000000000, NULL)
GO
SET IDENTITY_INSERT [dbo].[ScheduleElements] OFF
GO
SET IDENTITY_INSERT [dbo].[SpaceObjects] ON 
GO
INSERT [dbo].[SpaceObjects] ([id], [name], [description], [kind], [systemCenter], [systemPosition]) VALUES (1, N'SuperStar', N'main star', 1, NULL, 1)
GO
INSERT [dbo].[SpaceObjects] ([id], [name], [description], [kind], [systemCenter], [systemPosition]) VALUES (2, N'A', N'A planet', 0, 1, 1)
GO
INSERT [dbo].[SpaceObjects] ([id], [name], [description], [kind], [systemCenter], [systemPosition]) VALUES (3, N'B', N'B planet', 0, 1, 2)
GO
INSERT [dbo].[SpaceObjects] ([id], [name], [description], [kind], [systemCenter], [systemPosition]) VALUES (4, N'C', N'C planet', 0, 1, 3)
GO
INSERT [dbo].[SpaceObjects] ([id], [name], [description], [kind], [systemCenter], [systemPosition]) VALUES (5, N'D', N'D planet', 0, 1, 4)
GO
SET IDENTITY_INSERT [dbo].[SpaceObjects] OFF
GO
SET IDENTITY_INSERT [dbo].[SpacePorts] ON 
GO
INSERT [dbo].[SpacePorts] ([id], [name], [description], [planet], [longtitude], [latitude], [altitude]) VALUES (1, N'PA', N'PA port', 2, 0.003, 0.006, 0.006)
GO
INSERT [dbo].[SpacePorts] ([id], [name], [description], [planet], [longtitude], [latitude], [altitude]) VALUES (2, N'PB', N'PB port', 3, 0.003, 0.01, 0.006)
GO
INSERT [dbo].[SpacePorts] ([id], [name], [description], [planet], [longtitude], [latitude], [altitude]) VALUES (3, N'PC', N'PC port', 4, 0.003, 0.006, 0.006)
GO
INSERT [dbo].[SpacePorts] ([id], [name], [description], [planet], [longtitude], [latitude], [altitude]) VALUES (4, N'PD', N'PD port', 5, 0.003, 0.006, 0.006)
GO
SET IDENTITY_INSERT [dbo].[SpacePorts] OFF
GO
SET IDENTITY_INSERT [dbo].[Spaceships] ON 
GO
INSERT [dbo].[Spaceships] ([id], [name], [model], [class], [usageCost], [volume], [currentState]) VALUES (1, N'Artem', N'Sinkevich', 16, 5, 10, 26)
GO
SET IDENTITY_INSERT [dbo].[Spaceships] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([id], [login], [passwordHash], [customer], [driver], [roles]) VALUES (1, N'admin', 1324537282, NULL, NULL, 8)
GO
INSERT [dbo].[Users] ([id], [login], [passwordHash], [customer], [driver], [roles]) VALUES (2, N'driver', 2040117766, NULL, 1, 1)
GO
INSERT [dbo].[Users] ([id], [login], [passwordHash], [customer], [driver], [roles]) VALUES (3, N'customer', -1648926218, 1, NULL, 2)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD  CONSTRAINT [Drivers_FK_CurrentState] FOREIGN KEY([currentState])
REFERENCES [dbo].[ScheduleElements] ([id])
GO
ALTER TABLE [dbo].[Drivers] CHECK CONSTRAINT [Drivers_FK_CurrentState]
GO
ALTER TABLE [dbo].[Edges]  WITH CHECK ADD  CONSTRAINT [Edges_FK_End] FOREIGN KEY([end])
REFERENCES [dbo].[SpaceObjects] ([id])
GO
ALTER TABLE [dbo].[Edges] CHECK CONSTRAINT [Edges_FK_End]
GO
ALTER TABLE [dbo].[Edges]  WITH CHECK ADD  CONSTRAINT [Edges_FK_Start] FOREIGN KEY([start])
REFERENCES [dbo].[SpaceObjects] ([id])
GO
ALTER TABLE [dbo].[Edges] CHECK CONSTRAINT [Edges_FK_Start]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [Orders_FK_CurrentState] FOREIGN KEY([currentState])
REFERENCES [dbo].[ScheduleElements] ([id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [Orders_FK_CurrentState]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [Orders_FK_Customer] FOREIGN KEY([customer])
REFERENCES [dbo].[Customers] ([id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [Orders_FK_Customer]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [Orders_FK_Driver] FOREIGN KEY([driver])
REFERENCES [dbo].[Drivers] ([id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [Orders_FK_Driver]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [Orders_FK_LoadingPort] FOREIGN KEY([loadingPort])
REFERENCES [dbo].[SpacePorts] ([id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [Orders_FK_LoadingPort]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [Orders_FK_Spaceship] FOREIGN KEY([spaceship])
REFERENCES [dbo].[Spaceships] ([id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [Orders_FK_Spaceship]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [Orders_FK_UnloadingPort] FOREIGN KEY([unloadingPort])
REFERENCES [dbo].[SpacePorts] ([id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [Orders_FK_UnloadingPort]
GO
ALTER TABLE [dbo].[ScheduleElements]  WITH CHECK ADD  CONSTRAINT [ScheduleElements_FK_DestinationOrStop] FOREIGN KEY([destinationOrStop])
REFERENCES [dbo].[SpacePorts] ([id])
GO
ALTER TABLE [dbo].[ScheduleElements] CHECK CONSTRAINT [ScheduleElements_FK_DestinationOrStop]
GO
ALTER TABLE [dbo].[ScheduleElements]  WITH CHECK ADD  CONSTRAINT [ScheduleElements_FK_Driver] FOREIGN KEY([driver])
REFERENCES [dbo].[Drivers] ([id])
GO
ALTER TABLE [dbo].[ScheduleElements] CHECK CONSTRAINT [ScheduleElements_FK_Driver]
GO
ALTER TABLE [dbo].[ScheduleElements]  WITH CHECK ADD  CONSTRAINT [ScheduleElements_FK_Order] FOREIGN KEY([order])
REFERENCES [dbo].[Orders] ([id])
GO
ALTER TABLE [dbo].[ScheduleElements] CHECK CONSTRAINT [ScheduleElements_FK_Order]
GO
ALTER TABLE [dbo].[ScheduleElements]  WITH CHECK ADD  CONSTRAINT [ScheduleElements_FK_ScheduleElements] FOREIGN KEY([next])
REFERENCES [dbo].[ScheduleElements] ([id])
GO
ALTER TABLE [dbo].[ScheduleElements] CHECK CONSTRAINT [ScheduleElements_FK_ScheduleElements]
GO
ALTER TABLE [dbo].[ScheduleElements]  WITH CHECK ADD  CONSTRAINT [ScheduleElements_FK_Spaceship] FOREIGN KEY([spaceship])
REFERENCES [dbo].[Spaceships] ([id])
GO
ALTER TABLE [dbo].[ScheduleElements] CHECK CONSTRAINT [ScheduleElements_FK_Spaceship]
GO
ALTER TABLE [dbo].[SpaceObjects]  WITH CHECK ADD  CONSTRAINT [SpaceObjects_FK_SystemCenter] FOREIGN KEY([systemCenter])
REFERENCES [dbo].[SpaceObjects] ([id])
GO
ALTER TABLE [dbo].[SpaceObjects] CHECK CONSTRAINT [SpaceObjects_FK_SystemCenter]
GO
ALTER TABLE [dbo].[SpacePorts]  WITH CHECK ADD  CONSTRAINT [SpacePorts_FK_Planet] FOREIGN KEY([planet])
REFERENCES [dbo].[SpaceObjects] ([id])
GO
ALTER TABLE [dbo].[SpacePorts] CHECK CONSTRAINT [SpacePorts_FK_Planet]
GO
ALTER TABLE [dbo].[Spaceships]  WITH CHECK ADD  CONSTRAINT [Spaceships_FK_CurrentState] FOREIGN KEY([currentState])
REFERENCES [dbo].[ScheduleElements] ([id])
GO
ALTER TABLE [dbo].[Spaceships] CHECK CONSTRAINT [Spaceships_FK_CurrentState]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [Users_FK_Customer] FOREIGN KEY([customer])
REFERENCES [dbo].[Customers] ([id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [Users_FK_Customer]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [Users_FK_Driver] FOREIGN KEY([driver])
REFERENCES [dbo].[Drivers] ([id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [Users_FK_Driver]
GO
USE [master]
GO
ALTER DATABASE [TransUniverseDB] SET  READ_WRITE 
GO
