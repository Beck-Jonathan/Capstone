USE Night_Rider;
GO

print ''
print '*** Create sp_insert_route_fulfillment ***'
GO
-- AUTHOR: Chris Baenziger
-- CREATED: 2024-04-16
CREATE PROCEDURE [dbo].[sp_insert_route_fulfillment]
    (
    @Assignment_ID      [int],
    @Driver_ID          [int],
    @VIN                [nvarchar](17),
    @Start_Time         [datetime]
)
AS
BEGIN
    INSERT INTO [Route_Fulfillment]
        ([Assignment_ID], [Actual_Driver_ID], [Actual_VIN], [Start_Time])
    VALUES
        (@Assignment_ID, @Driver_ID, @VIN, @Start_Time)
END
GO

print ''
print '*** Create sp_update_route_fulfillment_end_time ***'
GO
-- AUTHOR: Chris Baenziger
-- CREATED: 2024-04-16
CREATE PROCEDURE [dbo].[sp_update_route_fulfillment_end_time]
    (
    @Assignment_ID      [int],
    @Driver_ID          [int],
    @VIN                [nvarchar](17),
    @End_Time         [datetime]
)
AS
BEGIN
    UPDATE [Route_Fulfillment]
	SET [End_Time] = @End_Time
    WHERE @Assignment_ID = [Assignment_ID]
        AND @Driver_ID = [Actual_Driver_ID]
        AND @VIN = [Actual_VIN]
END
GO