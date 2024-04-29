USE Night_Rider;
GO

print '' print 'creating sp_insert_ride'
GO
CREATE PROCEDURE [dbo].[sp_insert_ride] (
  @Client_ID [int],
  @Operation [nvarchar](255),
  @Pickup_Location [nvarchar](255),
  @Dropoff_Location [nvarchar](255),
  @Scheduled_Pickup_Time [datetime]
)
AS
BEGIN
  INSERT INTO [dbo].[Ride]
  ([Client_ID], [Operation], [Pickup_Location], [Dropoff_Location], [Scheduled_Pickup_Time])
  VALUES (@Client_Id, @Operation, @Pickup_Location, @Dropoff_Location, @Scheduled_Pickup_Time);

  SELECT SCOPE_IDENTITY();
END;
GO

print '' print 'creating sp_select_ride_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_ride_by_id] (
  @Ride_ID [int]
)
AS
BEGIN
  SELECT [Ride_ID],
         [Client_ID],
         [Operation],
         [Driver_ID],
         [VIN],
         [Pickup_Location],
         [Dropoff_Location],
         [Scheduled_Pickup_Time],
         [Is_Active]
  FROM [dbo].[Ride]
  WHERE [Ride_ID] = @Ride_ID;
END;
GO

print '' print 'creating sp_update_ride'
GO
CREATE PROCEDURE [dbo].[sp_update_ride] (
  @Ride_ID [int],
  @Driver_ID [int] NULL,
  @VIN [nvarchar](17) NULL,
  @Pickup_Location [nvarchar](255),
  @Dropoff_Location [nvarchar](255),
  @Scheduled_Pickup_Time [datetime]
)
AS
BEGIN
  UPDATE [dbo].[Ride]
  SET [Driver_ID] = @Driver_ID,
      [VIN] = @VIN,
      [Pickup_Location] = @Pickup_Location,
      [Dropoff_Location] = @Dropoff_Location,
      [Scheduled_Pickup_Time] = @Scheduled_Pickup_Time
  WHERE [Ride_ID] = @Ride_ID;
END;
GO

print '' print 'creating sp_update_ride_is_active'
GO
CREATE PROCEDURE [dbo].[sp_update_ride_is_active] (
  @Ride_ID [int],
  @Is_Active [bit]
)
AS
BEGIN
  UPDATE [dbo].[Ride] SET [Is_Active] = @Is_Active WHERE [Ride_ID] = @Ride_ID;
END;
GO

/******************
Create sp_select_ride_requests_by_client_id Stored Procedure
***************/
print '' print '*** creating sp_select_ride_requests_by_client_id ***'
GO
-- AUTHOR: Jacob Wendt
CREATE PROCEDURE [dbo].[sp_select_ride_requests_by_client_id]
(
	@Client_ID			[int]
)
AS
	BEGIN
		SELECT 	[Ride_ID],
				[Client_ID],
				[Operation],
				[Driver_ID],
				[VIN],
				[Pickup_Location],
				[Dropoff_Location],
				[Scheduled_Pickup_Time],
				[Estimated_Dropoff_Time],
				[Actual_Pickup_Time],
				[Actual_Dropoff_Time],
				[Requested],
				[Is_Active]
		FROM 	[Ride] 		
		WHERE	@Client_ID = [Client_ID]
	END;
GO
