create table dbo.Product (
	[id] int IDENTITY(0,1) not null,
	[code] varchar(15) not null,
	[name] varchar(100) not NULL,
	[description] varchar(200) not null,
	[date] datetime not null
);
