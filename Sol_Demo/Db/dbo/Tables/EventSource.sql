CREATE TABLE [dbo].[EventSource]
(
	[EventId] Numeric(18,0) IDENTITY(1,1) NOT NULL PRIMARY KEY,
	AggregateId Varchar(100),
	StateId Varchar(100),
	EventName Varchar(100),
	NewPayload Varchar(MAX),
	OldPayload Varchar(MAX),
	[CreatedDate] DateTime
)
