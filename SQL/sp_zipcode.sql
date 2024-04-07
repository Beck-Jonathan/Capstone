USE Night_Rider;
GO

/******************
Create sp_select_location_by_zip Stored Procedure
***************/
-- AUTHOR: Isabella Rosenbohm
print '' print '*** creating sp_select_location_by_zip ***'
GO
CREATE PROCEDURE [dbo].[sp_select_location_by_zip]
(
	@p_ZipCode			[nvarchar](15)
)
AS 
	BEGIN
		SELECT	[Zip_Code], [City], [State]
		FROM	[Zipcode]
		WHERE 	@p_ZipCode = [Zip_Code]
	END
GO

-- TODO: need to figure out how to deal with order of files and table creation/insertion for zipcode table for foreign keys on Client and Employee