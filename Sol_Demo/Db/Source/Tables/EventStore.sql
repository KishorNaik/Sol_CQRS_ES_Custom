CREATE TABLE [dbo].[EventStore]
(
	[EventId] Numeric(18,0) IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [AggregateId] UNIQUEIDENTIFIER NULL, 
    [StateId] UNIQUEIDENTIFIER NULL, 
    [EventName] VARCHAR(100) NULL, 
    [NewPayLoad] VARCHAR(MAX) NULL, 
    [OldPayLoad] VARCHAR(MAX) NULL, 
    [CreatedDate] DATETIME NULL,
	
)
