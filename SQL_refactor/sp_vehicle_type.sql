USE Night_Rider;
GO

/******************
Create sp_select_vehicle_types Stored Procedure
***************/
 -- AUTHOR: Chris Baenziger
print '' print '*** creating sp_select_vehicle_types ***'
GO
CREATE PROCEDURE [dbo].[sp_select_vehicle_types]
AS
    BEGIN
        SELECT [Vehicle_Type_ID]
        FROM [dbo].[Vehicle_Type]
        WHERE [Is_Active] = 1
    END
GO
