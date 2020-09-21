CREATE UNIQUE NONCLUSTERED INDEX [IX_tblMovie_MovieIdentity]
	ON [dbo].tblMovie
	(MovieIdentity)
	INCLUDE(Title,ReleaseDate,AggregateId)
