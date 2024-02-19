USE Night_Rider;
GO
print ''
print '' print '*** creating sp_select_vehicle_types ***'
GO
 -- AUTHOR: Chris Baenziger
CREATE PROCEDURE [dbo].[sp_select_vehicle_types]
AS
BEGIN
    SELECT [Vehicle_Type]
    FROM [Vehicle_Type]
    WHERE [Is_Active] = 1
END
GO