use master

if db_id('DriverDB') is not null
begin
	alter database DriverDB set single_user with rollback immediate
	drop database DriverDB
end

create database DriverDB
go

use DriverDB

create table Drivers (
	id int primary key identity(1,1),
	name varchar(128) not null,
	address varchar(128) not null,
	email varchar(128) not null,
	qualificationClasses int not null, -- bit enum
	spaceshipClasses int not null,
	hiringCost bigint not null,
	currentState int null -- FK
)

create table Spaceships (
	id int primary key identity(1,1),
	name varchar(128) not null,
	model varchar(128) not null,
	class int not null, -- bit enum
	usageCost bigint not null,
	volume bigint not null,
	currentState int null -- FK
)

SET IDENTITY_INSERT [dbo].[Drivers] ON 
GO
INSERT [dbo].[Drivers] ([id], [name], [address], [email], [qualificationClasses], [spaceshipClasses], [hiringCost], [currentState]) VALUES (1, N'Ivan Lankin', N'Dubki', N'ivanlankin@gmail.com', 16, 16, 3, 26)
GO
SET IDENTITY_INSERT [dbo].[Drivers] OFF
GO

SET IDENTITY_INSERT [dbo].[Spaceships] ON 
GO
INSERT [dbo].[Spaceships] ([id], [name], [model], [class], [usageCost], [volume], [currentState]) VALUES (1, N'Artem', N'Sinkevich', 16, 5, 10, 26)
GO
SET IDENTITY_INSERT [dbo].[Spaceships] OFF
GO
