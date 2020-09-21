CREATE FUNCTION [dbo].[IsMovieExists]
(
	@MovieIdentity uniqueidentifier,
	@Title Varchar(100)
)
RETURNS bit
AS
BEGIN
	
	DECLARE @Status bit=0;


	IF @MovieIdentity IS NULL
		BEGIN
			-- Insert
			IF EXISTS
					(
						SELECT 
							M.MovieId
						FROM 
							tblMovie as M WITH(NOLOCK)
						WHERE M.Title=@Title
					)
			BEGIN
				SET @Status=1
			END
		END
	ELSE 
		BEGIN
			-- Update
			IF EXISTS
					(
						SELECT 
							M.MovieId
						FROM 
							tblMovie as M WITH(NOLOCK)
						WHERE 
							M.Title=@Title
							AND
							M.MovieIdentity<>@MovieIdentity
					)
			BEGIN
				SET @Status=1
			END
		END

		RETURN @Status

END
