USE Night_Rider;
GO

/******************
Create sp_select_all_route Stored Procedure
***************/
-- AUTHOR: Nathan Toothaker
print '' print '*** creating sp_select_all_route ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_route]
AS 
	BEGIN
		SELECT	[Route_ID],
				[Route_Name],
				[Route_Start_Time],
				[Route_Cycle],
				[Route_End_Time],
				[Days_Of_Service],
				[Is_Active]
		FROM	[Route]
	END
GO