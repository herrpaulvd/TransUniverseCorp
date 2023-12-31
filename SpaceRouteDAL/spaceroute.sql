use master

if db_id('SpaceRouteDB') is not null
begin
	alter database SpaceRouteDB set single_user with rollback immediate
	drop database SpaceRouteDB
end

create database SpaceRouteDB
go

use SpaceRouteDB

create table Edges (
	id int primary key identity(1,1),
	start int not null, --FK
	[end] int not null, --FK
	time bigint not null, -- through ticks
	spaceshipClasses int not null, -- bit enum
	qualificationClasses int not null -- bit enum
)

create table [Orders] (
	id int primary key identity(1,1),
	loadingTime bigint not null, -- through ticks
	loadingPort int not null, --FK
	unloadingTime bigint not null, -- through ticks
	unloadingPort int not null, --FK
	volume bigint not null,
	totalCost bigint not null,
	totalTime bigint not null,
	spaceship int not null, --FK
	driver int not null, --FK
	customer int not null, --FK
	currentState int not null, --FK
	[status] int not null
)

create table ScheduleElements (
	id int primary key identity(1,1),
	departureOrArrival bigint null, --ticks
	plannedDepartureOrArrival bigint null, --ticks
	[order] int null, --FK
	spaceship int null, --FK
	driver int null, --FK
	destinationOrStop int null, --FK
	isStop bit not null,
	time bigint not null, --ticks
	[next] int null -- FK
)

create table SpaceObjects (
	id int primary key identity(1,1),
	name varchar(128) not null, 
	description varchar(1024) not null,
	kind int not null, -- enum
	systemCenter int null, -- FK
	systemPosition int not null
)

create table SpacePorts (
	id int primary key identity(1,1),
	name varchar(128) not null,
	description varchar(1024) not null,
	planet int not null, -- FK
	longtitude float not null, 
	latitude float not null,
	altitude float not null
)

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
