CREATE TABLE [dbo].[tblMovie]
(
	[MovieId] NUmeric(18,0) IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[MovieIdentity] UNIQUEIDENTIFIER Unique, 
    [Title] VARCHAR(50) NULL, 
    [ReleaseDate] DATETIME NULL, 
    [IsDelete] BIT NULL, 
    [AggregateId] UNIQUEIDENTIFIER NULL,


)
