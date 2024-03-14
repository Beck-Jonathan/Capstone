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

print ''
print '*** Create sp_activate_route_by_id ***'
GO
-- AUTHOR: Chris Baenziger
-- CREATED: 2024-03-02
CREATE PROCEDURE [dbo].[sp_activate_route_by_id]
	(
	@RouteID        [int]
)
AS
BEGIN
	UPDATE [Route]
    SET [Is_Active] = 1
    WHERE @RouteID = [Route_ID]
END
GO

print ''
print '*** Create sp_deactivate_route_by_id ***'
GO
-- AUTHOR: Chris Baenziger
-- CREATED: 2024-03-02
CREATE PROCEDURE [dbo].[sp_deactivate_route_by_id]
	(
	@RouteID        [int]
)
AS
BEGIN
	UPDATE [Route]
    SET [Is_Active] = 0
    WHERE @RouteID = [Route_ID]
END
GO