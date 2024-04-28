USE Night_Rider;
GO

print '' Print '***Creating sp_select_route_stops_by_route_id***' 
 go 
CREATE PROCEDURE [dbo].[sp_select_route_stops_by_route_id]
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
,[Route_Stop].[Route_Stop_ID]


 FROM [Route_Stop]
 JOIN [Stop]
 ON [Route_Stop].[Stop_Id] = [Stop].[Stop_Id] 
where [Route_Id]=@p_Route_Id 
ORDER BY [Route_Stop_Number];
 END 
 GO

 
 
 
/******************
Create sp_insert_route_stop Stored Procedure
***************/
-- AUTHOR: Nathan Toothaker

 print '' print '***Creating sp_insert_route_stop***'
 GO
 CREATE PROCEDURE [dbo].[sp_insert_route_stop](
	@p_Route_Id INT,
	@p_Stop_Id INT,
	@p_Route_Stop_Number INT,
	@p_Start_Offset TIME
)
AS
BEGIN
	INSERT INTO [dbo].[route_stop] (
		Route_ID,
		Stop_ID,
		Route_Stop_Number,
		Start_Offset,
		Is_Active
	) VALUES (
		@p_Route_Id,
		@p_Stop_Id,
		@p_Route_Stop_Number,
		@p_Start_Offset,
		1
	);
	RETURN SCOPE_IDENTITY();
END
GO

/******************
Create sp_update_ordinal Stored Procedure
***************/
-- AUTHOR: Nathan Toothaker

print '' print '***Creating sp_update_ordinal***'
GO
CREATE PROCEDURE [dbo].[sp_update_ordinal](
	@p_Route_Stop_Id INT,
	@p_Route_Id INT,
	@p_Stop_Id INT,
	@p_new_ordinal INT
)
AS
BEGIN
	UPDATE [dbo].[route_stop]
	SET Route_Stop_Number = @p_new_ordinal
	WHERE Route_Id = @p_Route_Id
		AND Stop_Id = @p_Stop_Id
		AND Route_Stop_Id = @p_Route_Stop_ID
	RETURN	@@ROWCOUNT
END
GO

/******************
Create sp_deactivate_route_stop Stored Procedure
***************/
-- AUTHOR: Nathan Toothaker

print '' print '***Creating sp_deactivate_route_stop***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_route_stop](
	@p_Route_Stop_Id INT,
	@p_Route_Id INT,
	@p_Stop_Id INT,
	@p_ordinal INT,
	@p_Start_Offset TIME
)
AS
BEGIN
	DELETE FROM [dbo].[route_stop]
	WHERE Route_Id = @p_Route_Id
		AND Stop_Id = @p_Stop_Id
		AND Route_Stop_Id = @p_Route_Stop_ID
		AND Route_Stop_Number = @p_ordinal
		AND Start_Offset = @p_Start_Offset
	RETURN	@@ROWCOUNT
END
GO