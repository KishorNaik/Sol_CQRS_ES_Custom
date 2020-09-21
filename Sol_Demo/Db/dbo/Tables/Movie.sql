CREATE TABLE [dbo].[Movie]
(
	[MovieId] Numeric(18,0) Identity(1,1) NOT NULL PRIMARY KEY,
	MovieIdentity Varchar(100) UNIQUE,
	AggregateId Varchar(100) UNIQUE,
	Title Varchar(100),
	ReleaseDate DateTime,
	IsDelete bit
)
