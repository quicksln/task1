/*
-- =============================================
-- Author: Piotr S.
-- Create date: 29-02-2024
-- Description:  Provides information about the max wind speed, country name for a specific country
-- =============================================
-- Change History:
-- Date			Changed by      Description
-- 29-02-2024   Piotr S.	    Init vesrion
-- =============================================
*/
CREATE PROCEDURE [dbo].[stp_Weather_GetMaxWindSpeed]
   @CountName NVARCHAR(255)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    BEGIN TRY

		SELECT MAX(w.WindSpeed) AS MaxWindSpeed, l.Country
		FROM Weathers w
		JOIN Locations l ON w.CityId = l.Id
		WHERE l.Country LIKE '%' + @CountName
		GROUP BY l.Country;

	END TRY
	BEGIN CATCH
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY();

		THROW 66000, @ErrorMessage, @ErrorSeverity;
	END CATCH
END
GO


