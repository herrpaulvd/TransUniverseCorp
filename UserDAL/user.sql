use master

if db_id('UserDB') is not null
begin
	alter database UserDB set single_user with rollback immediate
	drop database UserDB
end

create database UserDB
go

use UserDB

create table Users (
	id int primary key identity(1,1),
	login varchar(128) not null,
	passwordHash bigint not null,
	customer int null, --FK
	driver int null, --FK
	roles int not null --bit enum???
)
go

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

