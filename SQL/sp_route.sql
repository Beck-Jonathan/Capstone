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

print '' print '*** Create sp_insert_route ***'
GO
-- AUTHOR: Nathan Toothaker
-- CREATED: 2024-04-12
CREATE PROCEDURE [dbo].[sp_insert_route]
(
	@Route_Name NVARCHAR(255),
	@Route_Start_Time DATETIME,
	@Route_End_Time DATETIME,
	@Route_Cycle TIME(7),
	@Days_Of_Service CHAR(7)
) AS
BEGIN
	INSERT INTO [Route]
	(
	Route_Name, 
	Route_Start_Time, 
	Route_End_Time, 
	Route_Cycle, 
	Days_Of_Service
	)
	VALUES
	(
	@Route_Name,
	@Route_Start_Time,
	@Route_End_Time,
	@Route_Cycle,
	@Days_Of_Service
	)
	RETURN SCOPE_IDENTITY();
END
GO

print '' print '*** Create sp_edit_route ***'
GO
-- AUTHOR: Nathan Toothaker
-- CREATED: 2024-04-12
CREATE PROCEDURE [dbo].[sp_update_route]
(
	@RouteID INT,
	@Old_Route_Name NVARCHAR(255),
	@Old_Route_Start_Time DATETIME,
	@Old_Route_End_Time DATETIME,
	@Old_Route_Cycle TIME(7),
	@Old_Days_Of_Service CHAR(7),
	@New_Route_Name NVARCHAR(255),
	@New_Route_Start_Time DATETIME,
	@New_Route_End_Time DATETIME,
	@New_Route_Cycle TIME(7),
	@New_Days_Of_Service CHAR(7)
) AS
BEGIN
	UPDATE [Route]
	SET Route_Name = @New_Route_Name,
		Route_Start_Time = @New_Route_Start_Time,
		Route_End_Time = @New_Route_End_Time,
		Route_Cycle = @New_Route_Cycle, 
		Days_Of_Service = @New_Days_Of_Service
	WHERE Route_ID = @RouteID
		AND Route_Name = @Old_Route_Name
		AND Route_Start_Time = @Old_Route_Start_Time
		AND Route_End_Time = @Old_Route_End_Time
		AND Route_Cycle = @Old_Route_Cycle
		AND Days_Of_Service = @Old_Days_Of_Service
	
	RETURN @@rowcount;
END
GO