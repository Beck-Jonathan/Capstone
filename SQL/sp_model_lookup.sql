USE Night_Rider;
GO
print '' print '*** creating sp_select_vehicle_makes ***'
GO
 -- AUTHOR: Chris Baenziger
CREATE PROCEDURE [dbo].[sp_select_vehicle_makes]
AS
BEGIN
    SELECT [Vehicle_Make]
    FROM [Model_Lookup]
    WHERE [Active] = 1
END
GO

print '' print '*** creating sp_select_vehicle_models ***'
GO
 -- AUTHOR: Chris Baenziger
CREATE PROCEDURE [dbo].[sp_select_vehicle_models]
AS
BEGIN
    SELECT [Vehicle_Model]
    FROM [Model_Lookup]
    WHERE [Active] = 1
END
GO