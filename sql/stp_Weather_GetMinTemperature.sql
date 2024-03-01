/*
-- =============================================
-- Author: Piotr S.
-- Create date: 29-02-2024
-- Description:  Provides information about min temperature, max wind
-- speed, country name where the min temperature in the country was less
-- than a specific temperature.
-- =============================================
-- Change History:
-- Date			Changed by      Description
-- 29-02-2024   Piotr S.	    Init vesrion
-- =============================================
*/
CREATE PROCEDURE [dbo].[stp_Weather_GetMinTemperature]
   @Temperature float = 0

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    BEGIN TRY

		SELECT MIN(w.Temperature) AS MinTemperature, 
			   MAX(w.WindSpeed) AS MaxWindSpeed, 
			   l.Country
		FROM Weathers w
		JOIN Locations l ON w.CityId = l.Id
		GROUP BY l.Country
		HAVING MIN(w.Temperature) < @Temperature ;

	END TRY
	BEGIN CATCH
		DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY();

		THROW 66000, @ErrorMessage, @ErrorSeverity;
	END CATCH
END
GO


