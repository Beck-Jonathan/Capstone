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