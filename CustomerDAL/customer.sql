use master

if db_id('CustomerDB') is not null
begin
	alter database CustomerDB set single_user with rollback immediate
	drop database CustomerDB
end

create database CustomerDB
go

use CustomerDB

create table Customers (
	id int primary key identity(1,1),
	name varchar(128) not null,
	address varchar(128) not null,
	email varchar(128) not null,
	corporative bit not null
)

SET IDENTITY_INSERT [dbo].[Customers] ON 
GO
INSERT [dbo].[Customers] ([id], [name], [address], [email], [corporative]) VALUES (1, N'Leonid Mironov', N'Super7', N'mironov@bk.ru', 0)
GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO

