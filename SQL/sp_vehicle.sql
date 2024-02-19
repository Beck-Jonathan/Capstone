USE Night_Rider;
GO
print '' print '*** creating sp_add_vehicle ***'
GO
 -- AUTHOR: Chris Baenziger
CREATE PROCEDURE [dbo].[sp_add_vehicle]
    (
    @VIN                    [nvarchar](17),
    @Vehicle_Number         [nvarchar](10),
    @Vehicle_Mileage        [int],
    @Vehicle_License_plate  [nvarchar](10),
    @Vehicle_Type           [nvarchar](50),
    @Max_Passengers         [int],
    @Description            [nvarchar](256),
    @Date_Entered           [date]
)
AS
BEGIN
    INSERT INTO [dbo].[Vehicle]
        ([VIN],[Vehicle_Number],[Vehicle_Mileage],[Vehicle_License_Plate],[Vehicle_Type], [Max_Passengers], [Description], [Date_Entered])
    VALUES
        (@VIN, @Vehicle_Number, @Vehicle_Mileage, @Vehicle_License_Plate, @Vehicle_Type, @Max_Passengers, @Description, @Date_Entered)
    SELECT SCOPE_IDENTITY();
END
GO

/*************************
For Vehicle Lookup List 
***************/
-- Initial Creator: Everett DeVaux
-- Creation Date: 2024-02-08
-- Last Modified: Everett DeVaux
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all vehicles/vehicle details for the vehicle lookup list.
print '' print '*** creating sp_select_all_vehicles_for_vehicle_lookup_list ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_vehicles_for_vehicle_lookup_list]
AS
    BEGIN
        SELECT 
            [Vehicle].[Vehicle_Number],
            [Model_Lookup].[Vehicle_Make],
            [Model_Lookup].[Vehicle_Model],
            [Model_Lookup].[Max_Passengers],
            [Vehicle].[Vehicle_Mileage],
            [Vehicle].[Description]
        FROM  [dbo].[Vehicle]
        INNER JOIN [dbo].[Model_Lookup] ON [Vehicle].[VIN] = [Model_Lookup].[VIN]
    END
GO