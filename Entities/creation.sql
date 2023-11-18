use master

if db_id('TransUniverseDB') is not null
begin
	alter database TransUniverseDB set single_user with rollback immediate
	drop database TransUniverseDB
end

create database TransUniverseDB

use TransUniverseDB

create table Customers (
	id int primary key identity(1,1),
	name varchar(128) not null,
	address varchar(128) not null,
	email varchar(128) not null,
	corporative bit not null
)

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
	currentState int not null --FK
)

create table ScheduleElements (
	id int primary key identity(1,1),
	departureOrArrival bigint not null, --ticks
	plannedDepartureOrArrival bigint null, --ticks
	[order] int not null, --FK
	spaceship int null, --FK
	driver int null, --FK
	destinationOrStop int not null, --FK
	isStop bit not null,
	time bigint not null --ticks
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

create table Spaceships (
	id int primary key identity(1,1),
	name varchar(128) not null,
	model varchar(128) not null,
	class int not null, -- bit enum
	usageCost bigint not null,
	volume bigint not null,
	currentState int null -- FK
)

create table Users (
	id int primary key identity(1,1),
	login varchar(128) not null,
	passwordHash bigint not null,
	customer int null, --FK
	driver int null, --FK
	roles int not null --bit enum???
)
go

alter table Drivers
	add constraint Drivers_FK_CurrentState
	foreign key (currentState)
	references ScheduleElements(id)
go

alter table Edges
	add constraint Edges_FK_Start
	foreign key (start)
	references SpaceObjects(id)
go

alter table Edges
	add constraint Edges_FK_End
	foreign key ([end])
	references SpaceObjects(id)
go

alter table Orders
	add constraint Orders_FK_LoadingPort
	foreign key (loadingPort)
	references SpacePorts(id)
go

alter table Orders
	add constraint Orders_FK_UnloadingPort
	foreign key (unloadingPort)
	references SpacePorts(id)
go

alter table Orders
	add constraint Orders_FK_Spaceship
	foreign key (spaceship)
	references Spaceships(id)
go

alter table Orders
	add constraint Orders_FK_Driver
	foreign key (driver)
	references Drivers(id)
go

alter table Orders
	add constraint Orders_FK_CurrentState
	foreign key (currentState)
	references ScheduleElements(id)
go

alter table ScheduleElements
	add constraint ScheduleElements_FK_Spaceship
	foreign key (spaceship)
	references Spaceships(id)
go

alter table ScheduleElements
	add constraint ScheduleElements_FK_Driver
	foreign key (driver)
	references Drivers(id)
go

alter table ScheduleElements
	add constraint ScheduleElements_FK_Order
	foreign key ([order])
	references Orders(id)
go

alter table ScheduleElements
	add constraint ScheduleElements_FK_DestinationOrStop
	foreign key (destinationOrStop)
	references Spaceports(id)
go

alter table SpaceObjects
	add constraint SpaceObjects_FK_SystemCenter
	foreign key (systemCenter)
	references SpaceObjects(id)
go

alter table SpacePorts
	add constraint SpacePorts_FK_Planet
	foreign key (planet)
	references SpaceObjects(id)
go

alter table Spaceships
	add constraint Spaceships_FK_CurrentState
	foreign key (currentState)
	references ScheduleElements(id)
go

alter table Users
	add constraint Users_FK_Customer
	foreign key (customer)
	references Customers(id)
go

alter table Users
	add constraint Users_FK_Driver
	foreign key (driver)
	references Drivers(id)
go
