CREATE PROCEDURE [dbo].[uspSetMovie]
	@Command Varchar(100),

	@MovieIdentity uniqueidentifier=NULL,

	@Title Varchar(100),
	@ReleaseDate Varchar(100),

	@IsDelete bit,

	@AggregateId uniqueidentifier
AS
	
	DECLARE @ErrorMessage Varchar(100);
	DECLARE @IsMovieExists bit;

	IF @Command='Add'
		BEGIN
			
			BEGIN TRY
				
				BEGIN TRANSACTION

					SET @IsMovieExists=dbo.IsMovieExists(@MovieIdentity,@Title);

					IF @IsMovieExists=0
						BEGIN
							
							SET @MovieIdentity=NEWID();

							INSERT INTO tblMovie
							(
								MovieIdentity,
								Title,
								ReleaseDate,
								IsDelete,
								AggregateId
							)
							VALUES
							(
								@MovieIdentity,
								@Title,
								@ReleaseDate,
								0,
								@AggregateId
							)

							SELECT 'Add' As 'Message';
							SELECT @MovieIdentity AS 'MovieIdentity';
							
						END
					ELSE
						BEGIN
							SELECT 'Exist' As 'Message';
						END

				COMMIT TRANSACTION

			END TRY

			BEGIN CATCH 
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,16,1);
				ROLLBACK TRANSACTION
			END CATCH



		END
	IF @Command='Update'
		BEGIN
			
			BEGIN TRY
				
				BEGIN TRANSACTION

					SET @IsMovieExists=dbo.IsMovieExists(@MovieIdentity,@Title);

					IF @IsMovieExists=0
						BEGIN
							
							SELECT 'Update' As 'Message';

							-- Get Old Data for Event Store.
							SELECT 
								M.MovieIdentity,
								M.Title,
								M.ReleaseDate,
								M.AggregateId
							FROM 
								tblMovie As M WITH(NOLOCK)
							WHERE
								M.MovieIdentity=@MovieIdentity;

							SELECT 
								@Title=CASE WHEN @Title IS NULL THEN M.Title ELSE @Title END,
								@ReleaseDate=CASE WHEN @ReleaseDate IS NULL THEN M.ReleaseDate ELSE @ReleaseDate END
							FROM 
								tblMovie AS M WITH(NOLOCK)
							WHERE
								M.MovieIdentity=@MovieIdentity

							UPDATE tblMovie
								SET 
									Title=@Title,
									ReleaseDate=@ReleaseDate
								WHERE
									MovieIdentity=@MovieIdentity
							
						END
					ELSE
						BEGIN
							SELECT 'Exist' As 'Message';
						END

				COMMIT TRANSACTION

			END TRY

			BEGIN CATCH 
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,16,1);
				ROLLBACK TRANSACTION
			END CATCH

		END
		IF @Command='Delete'
		BEGIN
			
			BEGIN TRY
				
				BEGIN TRANSACTION

					UPDATE tblMovie
						SET @IsDelete=1
					WHERE
						MovieIdentity=@MovieIdentity

					-- Get Old Data for Event Store.
							SELECT 
								M.MovieIdentity,
								M.Title,
								M.ReleaseDate,
								M.AggregateId
							FROM 
								tblMovie As M WITH(NOLOCK)
							WHERE
								M.MovieIdentity=@MovieIdentity;
					
				COMMIT TRANSACTION

			END TRY

			BEGIN CATCH 
				SET @ErrorMessage=ERROR_MESSAGE();
				RAISERROR(@ErrorMessage,16,1);
				ROLLBACK TRANSACTION
			END CATCH



		END

GO
	