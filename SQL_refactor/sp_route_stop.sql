USE Night_Rider;
GO

/******************
Create sp_select_route_stops_by_route_id Stored Procedure
***************/
-- AUTHOR: Nathan Toothaker
print '' Print '***Creating sp_select_route_stops_by_route_id***' 
GO
CREATE PROCEDURE [dbo].[sp_select_route_stops_by_route_id]
(
    @p_Route_Id [int]
)
AS
    BEGIN 
        SELECT [Route_Stop].[Route_Id], 
            [Route_Stop].[Stop_Id], 
            [Route_Stop].[Route_Stop_Number], 
            [Route_Stop].[Start_Offset], 
            [Route_Stop].[Is_Active], 
            [Stop].[Street_Address], 
            [Stop].[Zip_Code],
            [Stop].[Latitude], 
            [Stop].[Longitude]
        FROM [dbo].[Route_Stop]
        JOIN [dbo].[Stop]
            ON [Route_Stop].[Stop_Id] = [Stop].[Stop_Id] 
        WHERE [Route_Stop].[Route_Id] = @p_Route_Id 
        ORDER BY [Route_Stop].[Route_Stop_Number]
    END 
GO