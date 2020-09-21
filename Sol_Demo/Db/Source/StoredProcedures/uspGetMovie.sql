CREATE PROCEDURE [dbo].[uspGetMovie]
	@Command Varchar(100)=NULL,

	@Title Varchar(100)=NULL,

	@ReleaseStartDate datetime=NULL,
	@ReleaseEndDate datetime=NULL
AS
	DECLARE @ErrorMessage Varchar(100);

	IF @Command='All-Movie-Data'
		BEGIN
			
			BEGIN TRY
				
				BEGIN TRANSACTION

					SELECT 
						M.MovieIdentity,
						M.Title,
						M.ReleaseDate,
						M.AggregateId
					FROM 
						tblMovie as M WITH(NOLOCK)
					WHERE
						M.IsDelete=0
					ORDER BY
						M.MovieId DESC

				COMMIT TRANSACTION

			END TRY

			BEGIN CATCH 
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,16,1);
				ROLLBACK TRANSACTION
			END CATCH
		END
	ELSE IF @Command='Movie-Title'
		BEGIN
			
			BEGIN TRY
				
				BEGIN TRANSACTION

					SELECT 
						M.MovieIdentity,
						M.Title,
						M.ReleaseDate,
						M.AggregateId
					FROM 
						tblMovie as M WITH(NOLOCK)
					WHERE
						M.Title LIKE '%'+CASE WHEN (LEN(@Title)>=1) THEN @Title ELSE NULL END+'%'
						AND
						M.IsDelete=0
					ORDER BY
						M.MovieId DESC

				COMMIT TRANSACTION

			END TRY

			BEGIN CATCH 
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,16,1);
				ROLLBACK TRANSACTION
			END CATCH
		END
	ELSE IF @Command='Movie-Release-Date'
		BEGIN
			
			BEGIN TRY
				
				BEGIN TRANSACTION

					SELECT 
						M.MovieIdentity,
						M.Title,
						M.ReleaseDate,
						M.AggregateId
					FROM 
						tblMovie as M WITH(NOLOCK)
					WHERE
						M.ReleaseDate between @ReleaseStartDate AND @ReleaseEndDate
						AND
						M.IsDelete=0
					ORDER BY
						M.MovieId DESC

				COMMIT TRANSACTION

			END TRY

			BEGIN CATCH 
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,16,1);
				ROLLBACK TRANSACTION
			END CATCH
		END

GO