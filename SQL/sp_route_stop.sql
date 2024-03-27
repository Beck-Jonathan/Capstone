USE Night_Rider;
GO

print '' Print '***Creating sp_select_route_stops_by_route_id***' 
 go 
CREATE PROCEDURE [DBO].[sp_select_route_stops_by_route_id]
(@p_Route_Id [int]
)
as
 Begin 
 select 
[Route_Stop].[Route_Id] 
,[Route_Stop].[Stop_Id] 
,[Route_Stop].[Route_Stop_Number] 
,[Route_Stop].[Start_Offset] 
,[Route_Stop].[Is_Active] 
,[Stop].[Street_Address]
,[Stop].[Zip_Code]
,[Stop].[Latitude]
,[Stop].[Longitude]


 FROM [Route_Stop]
 JOIN [Stop]
 ON [Route_Stop].[Stop_Id] = [Stop].[Stop_Id] 
where [Route_Id]=@p_Route_Id 
ORDER BY [Route_Stop_Number];
 END 
 GO