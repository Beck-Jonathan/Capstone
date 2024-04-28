USE Night_Rider;
GO

/******************
Create sp_get_assigned_routes Stored Procedure
***************/
-- Initial Creator: Steven Sanchez
-- Creation Date: 2024/03/24
-- Last Modified: Steven Sanchez
-- Modification Description: Initial creation.
-- Stored Procedure Description: gets assigned routes for a driver
print'' print'*** Creating sp_get_assigned_routes ***'
GO

CREATE PROCEDURE [dbo].[sp_get_assigned_routes]
(
    @DriverID INT
)
AS
BEGIN
    SELECT
        ra.[Assignment_ID],
        r.[Route_ID],
        r.[Route_Name],
        r.[Route_Start_Time],
        r.[Route_End_Time],
        rs.[Route_Stop_Number],
        s.[Street_Address],
        s.[Zip_Code],
        s.[Latitude],
        s.[Longitude]
    FROM
        [dbo].[Route_Assignment] ra
    INNER JOIN
        [dbo].[Route] r ON ra.[Route_ID] = r.[Route_ID]
    INNER JOIN
        [dbo].[Route_Stop] rs ON r.[Route_ID] = rs.[Route_ID]
    INNER JOIN
        [dbo].[Stop] s ON rs.[Stop_ID] = s.[Stop_ID]
    WHERE
        ra.[Driver_ID] = @DriverID
        AND ra.[Is_Active] = 1
        AND r.[Is_Active] = 1
        AND rs.[Is_Active] = 1
        AND s.[Is_Active] = 1
    ORDER BY
        ra.[Assignment_ID], rs.[Route_Stop_Number];
END
GO

/******************
Create sp_get_route_assignments_by_route_id
***************/
-- Initial Creator: James Williams
-- Creation Date: 2024-04-15
-- Last Modified: 
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all active route assignments by Route_ID
print'' print '*** creating sp_get_route_assignments_by_route_id ***'
GO
CREATE PROCEDURE [dbo].[sp_get_route_assignments_by_route_id]
(
	@p_Route_ID[int]
)
AS
BEGIN
	SELECT 
	[Assignment_ID], [Route_ID], [Date_Assignment_Started], [Date_Assignment_Ended],
	[Is_Active], [Driver_ID], [VIN]
	FROM [dbo].[Route_Assignment]
	WHERE Is_Active = 1 AND @p_Route_ID = Route_ID
END
GO

/******************
Create sp_create_route_assignment
***************/
-- Initial Creator: James Williams
-- Creation Date: 2024-04-15
-- Last Modified: 
-- Modification Description: Initial Creation
-- Stored Procedure Description: Create route assignment
print'' print'*** creating sp_create_route_assignment ***'
GO
CREATE PROCEDURE [dbo].[sp_create_route_assignment]
(
	@p_Driver_ID		[int],
	@p_Route_ID		[int],
	@p_VIN   nvarchar(17),
	@p_Date_Assignment_Started [datetime],
	@p_Date_Assignment_Ended 	[datetime]
)
AS
BEGIN
	INSERT INTO [dbo].[Route_Assignment]
	([Driver_ID],[Route_ID],[VIN],[Date_Assignment_Started],[Date_Assignment_Ended])
	VALUES 
	(@p_Driver_ID,@p_Route_ID, @p_VIN, @p_Date_Assignment_Started, @p_Date_Assignment_Ended)
END
GO


/******************
Create sp_get_available_drivers_by_date_and_max_capacity
***************/
-- Initial Creator: James Williams
-- Creation Date: 2024-04-15
-- Last Modified: 
-- Modification Description: Initial Creation
-- Stored Procedure Description: Return available drivers by date and capacity
PRINT '*** create sp_get_available_drivers_by_date_and_max_capacity ***'
GO

CREATE PROCEDURE [dbo].[sp_get_available_drivers_by_date_and_max_capacity]
    (
		@p_Start_Date DATETIME,
		@p_End_Date DATETIME,
		@p_Capacity INT
	 )
AS
BEGIN
    SELECT DISTINCT 
        e.Employee_ID,
        e.Given_Name, 
        e.Family_Name, 
        d.Driver_License_Class_ID, 
        dlc.Max_Passenger_Count
    FROM [dbo].[Employee] AS e 
    JOIN [dbo].[Driver] AS d ON d.Employee_ID = e.Employee_ID
    JOIN [dbo].[Driver_License_Class] AS dlc ON d.Driver_License_Class_ID = dlc.Driver_License_Class_ID
    LEFT JOIN [dbo].[Driver_Unavailable] AS du ON du.Driver_ID = e.Employee_ID
	WHERE @p_Capacity <= dlc.Max_Passenger_Count
	AND NOT EXISTS (
	-- 'SELECT 1' will stop the sub-query if a record is found
      SELECT 1
      FROM [dbo].[Driver_Unavailable] AS du2
	  -- This checks if there is already are any existing unavailbilities for the driver between the p_Start_Date and p_End_Date
      WHERE du2.Driver_ID = e.Employee_ID AND
            (
                @p_Start_Date BETWEEN du2.Start_Datetime AND du2.End_Datetime OR
                @p_End_Date BETWEEN du2.Start_Datetime AND du2.End_Datetime
            )
	)
	-- see above
	AND NOT EXISTS (
      SELECT 1
      FROM [dbo].[Route_Assignment] AS ra
      WHERE ra.Driver_ID = e.Employee_ID AND
            (
                @p_Start_Date BETWEEN ra.Date_Assignment_Started AND ra.Date_Assignment_Ended OR
                @p_End_Date BETWEEN ra.Date_Assignment_Started AND ra.Date_Assignment_Ended
            )
  )

END;
GO


/******************
Create sp_get_available_vehicles_by_date_and_max_capacity
***************/
-- Initial Creator: James Williams
-- Creation Date: 2024-04-15
-- Last Modified: 
-- Modification Description: Initial Creation
-- Stored Procedure Description: Return available vehicles by date and capacity
print'' print'*** create sp_get_available_vehicles_by_date_and_max_capacity***'
GO
CREATE PROCEDURE [dbo].[sp_get_available_vehicles_by_date_and_max_capacity]
    (@p_Start_Date DATETIME,
     @p_End_Date DATETIME,
     @p_Capacity INT)
AS
BEGIN
    SELECT DISTINCT 
        v.VIN,
        vm.Name,
        vm.Make,
        vm.Max_Passengers
    FROM [dbo].[Vehicle] AS v
    JOIN [dbo].[Vehicle_Model] AS vm ON vm.Vehicle_Model_ID = v.Vehicle_Model_ID
    LEFT JOIN [dbo].[Vehicle_Unavailable] AS vu ON vu.VIN = v.VIN
    WHERE @p_Capacity <= vm.Max_Passengers
	  AND NOT EXISTS (
	    --'SELECT 1' stops the query once a record is found
		  SELECT 1
		  FROM [dbo].[Vehicle_Unavailable] AS vu2
		  WHERE vu2.VIN = v.VIN AND
		  -- Remove any returned records that already have unavailbilities between the p_Start_Date and p_End_Date
		  vu2.End_Datetime IS NOT NULL AND
			(
				@p_Start_Date BETWEEN vu2.Start_Datetime AND vu2.End_Datetime OR
				@p_End_Date BETWEEN vu2.Start_Datetime AND vu2.End_Datetime
			)
				
		)
		-- See Above
	  AND NOT EXISTS (
		  SELECT 1
		  FROM [dbo].[Route_Assignment] AS ra
		  WHERE ra.VIN = v.VIN AND
				ra.Date_Assignment_Ended IS NOT NULL AND
				(
				  @p_Start_Date BETWEEN ra.Date_Assignment_Started AND ra.Date_Assignment_Ended OR
				  @p_End_Date BETWEEN ra.Date_Assignment_Started AND ra.Date_Assignment_Ended
				)
		)
END;
GO


/******************
Create sp_get_route_assignments_by_route_id_and_dates
***************/
-- Initial Creator: James Williams
-- Creation Date: 2024-04-15
-- Last Modified: 
-- Modification Description: Initial Creation
-- Stored Procedure Description: Return route assignments by the Route_ID and dates
print '' print'*** create sp_get_route_assignments_by_route_id_and_dates ***'
GO

CREATE PROCEDURE [dbo].[sp_get_route_assignments_by_route_id_and_dates]
(
    @p_Start_Date DATETIME,
    @p_End_Date DATETIME,
    @p_Route_ID INT
)
AS
BEGIN
    SELECT 
        Assignment_ID, 
        Driver_ID, 
        Route_ID, 
        VIN, 
        Date_Assignment_Started,
        Date_Assignment_Ended, 
        Is_Active
    FROM Route_Assignment
    WHERE Route_ID = @p_Route_ID AND
		Is_Active = 1 AND
        Date_Assignment_Started <= @p_End_Date AND
        (Date_Assignment_Ended IS NULL OR Date_Assignment_Ended >= @p_Start_Date)
END
GO

/******************
Create sp_create_driver_and_vehicle_unavailability
***************/
-- Initial Creator: James Williams
-- Creation Date: 2024-04-15
-- Last Modified: 
-- Modification Description: Initial Creation
-- Stored Procedure Description: Inserts Vehicle_Unavailable and Driver_Unavailable records
print '' print '*** creating sp_create_driver_and_vehicle_unavailability ***'
GO
CREATE PROCEDURE [sp_create_driver_and_vehicle_unavailability]
(
	@p_VIN				nvarchar(17),
	@p_Driver_ID		int,
	@p_Start_Date		datetime,
	@p_End_Date			datetime,
	@p_Reason			nvarchar(1000)
)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION [Tran1]
			INSERT INTO [dbo].[Driver_Unavailable] 
			([Driver_ID],[Start_Datetime],[End_Datetime],[Reason])
			VALUES
			(@p_Driver_ID, @p_Start_Date, @p_End_Date, @p_Reason)
			
			INSERT INTO [dbo].[Vehicle_Unavailable] 
			([VIN],[Start_Datetime],[End_Datetime],[Reason])
			VALUES
			(@p_VIN, @p_Start_Date, @p_End_Date, @p_Reason)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION[Tran1]	
	END CATCH
END
GO


/******************
Create sp_get_available_drivers_by_route_assignment_id
***************/
-- Initial Creator: James Williams
-- Creation Date: 2024-04-25
-- Last Modified: 
-- Modification Description: Initial Creation
-- Stored Procedure Description: Return available drivers by route_assignment_id
GO
CREATE PROCEDURE [dbo].[sp_get_route_assignment_driver_by_route_assignment_id]
(
	@p_Route_Assignment_ID			[int]
)
AS
BEGIN
	SELECT
	e.Employee_ID, e.Given_Name, e.Family_Name, dlc.Driver_License_Class_ID, dlc.Max_Passenger_Count
	FROM [dbo].[Employee] as e
	JOIN [dbo].[Driver] as d ON d.Employee_ID = e.Employee_ID
	JOIN [dbo].[Driver_License_Class] as dlc ON d.Driver_License_Class_ID = dlc.Driver_License_Class_ID
	JOIN [dbo].[Route_Assignment] as ra ON d.Employee_ID = ra.Driver_ID
	WHERE @p_Route_Assignment_ID = ra.Assignment_ID
END
GO

/******************
Create sp_get_available_drivers_by_date
***************/
-- Initial Creator: James Williams
-- Creation Date: 2024-04-25
-- Last Modified: 
-- Modification Description: Initial Creation
-- Stored Procedure Description: Return available drivers by date
PRINT '*** create sp_get_available_drivers_by_date_and_max_capacity ***'
GO

CREATE PROCEDURE [dbo].[sp_get_available_drivers_by_date]
    (
		@p_Start_Date DATETIME,
		@p_End_Date DATETIME
		
	 )
AS
BEGIN
    SELECT DISTINCT 
        e.Employee_ID,
        e.Given_Name, 
        e.Family_Name, 
        d.Driver_License_Class_ID, 
        dlc.Max_Passenger_Count
    FROM [dbo].[Employee] AS e 
    JOIN [dbo].[Driver] AS d ON d.Employee_ID = e.Employee_ID
    JOIN [dbo].[Driver_License_Class] AS dlc ON d.Driver_License_Class_ID = dlc.Driver_License_Class_ID
    LEFT JOIN [dbo].[Driver_Unavailable] AS du ON du.Driver_ID = e.Employee_ID
	WHERE NOT EXISTS (
	-- 'SELECT 1' will stop the sub-query if a record is found
      SELECT 1
      FROM [dbo].[Driver_Unavailable] AS du2
	  -- This checks if there is already are any existing unavailbilities for the driver between the p_Start_Date and p_End_Date
      WHERE du2.Driver_ID = e.Employee_ID AND
            (
                @p_Start_Date BETWEEN du2.Start_Datetime AND du2.End_Datetime OR
                @p_End_Date BETWEEN du2.Start_Datetime AND du2.End_Datetime
            )
	)
	-- see above
	AND NOT EXISTS (
      SELECT 1
      FROM [dbo].[Route_Assignment] AS ra
      WHERE ra.Driver_ID = e.Employee_ID AND
            (
                @p_Start_Date BETWEEN ra.Date_Assignment_Started AND ra.Date_Assignment_Ended OR
                @p_End_Date BETWEEN ra.Date_Assignment_Started AND ra.Date_Assignment_Ended
            )
  )

END;
GO

/******************
Create sp_get_available_vehicles_by_date
***************/
-- Initial Creator: James Williams
-- Creation Date: 2024-04-25
-- Last Modified: 
-- Modification Description: Initial Creation
-- Stored Procedure Description: Return available vehicles by date
print'' print'*** create sp_get_available_vehicles_by_date_and_max_capacity***'
GO
CREATE PROCEDURE [dbo].[sp_get_available_vehicles_by_date]
    (
		@p_Start_Date 	[DATETIME],
		@p_End_Date 	[DATETIME]
     )
AS
BEGIN
    SELECT DISTINCT 
        v.VIN,
        vm.Name,
        vm.Make,
        vm.Max_Passengers
    FROM [dbo].[Vehicle] AS v
    JOIN [dbo].[Vehicle_Model] AS vm ON vm.Vehicle_Model_ID = v.Vehicle_Model_ID
    LEFT JOIN [dbo].[Vehicle_Unavailable] AS vu ON vu.VIN = v.VIN
    WHERE NOT EXISTS (
	    --'SELECT 1' stops the query once a record is found
		  SELECT 1
		  FROM [dbo].[Vehicle_Unavailable] AS vu2
		  WHERE vu2.VIN = v.VIN AND
		  -- Remove any returned records that already have unavailbilities between the p_Start_Date and p_End_Date
		  vu2.End_Datetime IS NOT NULL AND
			(
				@p_Start_Date BETWEEN vu2.Start_Datetime AND vu2.End_Datetime OR
				@p_End_Date BETWEEN vu2.Start_Datetime AND vu2.End_Datetime
			)
				
		)
		-- See Above
	  AND NOT EXISTS (
		  SELECT 1
		  FROM [dbo].[Route_Assignment] AS ra
		  WHERE ra.VIN = v.VIN AND
				ra.Date_Assignment_Ended IS NOT NULL AND
				(
				  @p_Start_Date BETWEEN ra.Date_Assignment_Started AND ra.Date_Assignment_Ended OR
				  @p_End_Date BETWEEN ra.Date_Assignment_Started AND ra.Date_Assignment_Ended
				)
		)
END;
GO

/******************
Create sp_update_route_assignmet_driver
***************/
-- Initial Creator: James Williams
-- Creation Date: 2024-04-25
-- Last Modified: 
-- Modification Description: Initial Creation
-- Stored Procedure Description: Update Route_Assignment driver
print'' print'*** creating sp_update_route_assignmet_driver***'
GO
CREATE PROCEDURE [dbo].[sp_update_route_assignment_driver]
(
	@p_Assignment_ID				[int],
	@p_Driver_ID					[int]
)
AS
BEGIN
	UPDATE [dbo].[Route_Assignment]
	SET [Driver_ID] = @p_Driver_ID
	WHERE @p_Assignment_ID = [Assignment_ID]
END
GO

/******************
Create sp_update_route_assignmet_vehicle
***************/
-- Initial Creator: James Williams
-- Creation Date: 2024-04-25
-- Last Modified: 
-- Modification Description: Initial Creation
-- Stored Procedure Description: Update Route_Assignment vehicle
print'' print'*** creating sp_update_route_assignmet_driver***'
GO
CREATE PROCEDURE [dbo].[sp_update_route_assignment_vehicle]
(
	@p_Assignment_ID			[int],
	@p_VIN						[nvarchar](17)
)
AS
BEGIN
	UPDATE [dbo].[Route_Assignment]
	SET [VIN] = @p_VIN
	WHERE @p_Assignment_ID = [Assignment_ID]
END
GO