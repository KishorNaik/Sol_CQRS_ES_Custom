CREATE NONCLUSTERED INDEX IX_tblMovie_IsDelete
ON [dbo].tblMovie
	(IsDelete)
	INCLUDE(MovieIdentity,Title,ReleaseDate,AggregateId)