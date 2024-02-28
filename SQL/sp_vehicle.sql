USE Night_Rider;
GO
print ''
print '*** Create the sp_add_vehicle stored procedure ***'
GO
-- AUTHOR: Chris Baenziger
CREATE PROCEDURE [dbo].[sp_add_vehicle]
    (
    @VIN                    [nvarchar](17),
    @Vehicle_Number         [nvarchar](10),
    @Vehicle_Mileage        [int],
    @Vehicle_License_plate  [nvarchar](10),
    @Model_Lookup_ID        [int],
    @Vehicle_Type           [nvarchar](50),
    @Description            [nvarchar](256),
    @Date_Entered           [date],
    @Rental                 [bit]
)
AS
BEGIN
    INSERT INTO [dbo].[Vehicle]
        ([VIN],[Vehicle_Number],[Vehicle_Mileage],[Vehicle_License_Plate],[Model_Lookup_ID],[Vehicle_Type], [Description], [Date_Entered], [Rental])
    VALUES
        (@VIN, @Vehicle_Number, @Vehicle_Mileage, @Vehicle_License_Plate, @Model_Lookup_ID, @Vehicle_Type, @Description, @Date_Entered, @Rental)
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
-- Editor: Chris Baenziger
-- Edit Date: 2024-02-17
-- Modification: added additional fields
-- Editor: Chris Baenziger
-- Edit Date: 2024-02-22
-- Modification: added Is_Active to selection
print ''
print '*** creating sp_select_all_vehicles_for_vehicle_lookup_list ***'
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
        [Vehicle].[Description],
        [Vehicle].[Vehicle_License_Plate],
        [Vehicle].[Vehicle_Type],
        [Vehicle].[Rental]
    FROM [dbo].[Vehicle]
        INNER JOIN [dbo].[Model_Lookup] ON [Vehicle].[Model_Lookup_ID] = [Model_Lookup].[Model_Lookup_ID]
    WHERE [Vehicle].[Is_Active] = 1
END
GO

print ''
print '*** Create the sp_select_vehicle_by_vehicle_number ***'
GO
-- AUTHOR: Chris Baenziger
CREATE PROCEDURE [dbo].[sp_select_vehicle_by_vehicle_number]
    (
    @Vehicle_Number [nvarchar](10)
)
AS
BEGIN
    SELECT [Vehicle_Number], [VIN], [Vehicle].[Model_Lookup_ID], [Model_Lookup].[Vehicle_Make], [Model_Lookup].[Vehicle_Model], [Model_Lookup].[Vehicle_Year], [Vehicle_Mileage], [Vehicle_License_Plate], [Description], [Date_Entered], [Model_Lookup].[Max_Passengers], [Vehicle_Type], [Rental], [Is_Active]
    FROM [Vehicle]
        JOIN [Model_Lookup]
        ON [Vehicle].[Model_Lookup_ID] = [Model_Lookup].[Model_Lookup_ID]
    WHERE @Vehicle_Number = [Vehicle].[Vehicle_Number]
END
GO


print ''
print '*** Create sp_update_vehicle ***'
GO
-- AUTHOR: Chris Baenziger
CREATE PROCEDURE [dbo].[sp_update_vehicle]
    (
    @VIN                    [nvarchar](17),

    @OldVehicle_Number         [nvarchar](10),
    @OldVehicle_Mileage        [int],
    @OldModel_Lookup_ID        [int],
    @OldVehicle_License_Plate  [nvarchar](10),
    @OldVehicle_Type           [nvarchar](50),
    @OldDate_Entered           [date],
    @OldDescription            [nvarchar](256),
    @OldRental                 [bit],

    @OldMax_Passengers         [int],
    @OldVehicle_Year           [int],
    @OldVehicle_Make           [nvarchar](255),
    @OldVehicle_Model          [nvarchar](255),

    @Vehicle_Number         [nvarchar](10),
    @Vehicle_Mileage        [int],
    @Model_Lookup_ID        [int],
    @Vehicle_License_Plate  [nvarchar](10),
    @Vehicle_Type           [nvarchar](50),
    @Description            [nvarchar](256),
    @Date_Entered           [date],
    @Rental                 [bit],

    @Max_Passengers         [int],
    @Vehicle_Year           [int],
    @Vehicle_Make           [nvarchar](255),
    @Vehicle_Model          [nvarchar](255)
)
AS
BEGIN
    BEGIN TRANSACTION
    UPDATE [Model_Lookup]
    SET [Max_Passengers] = @Max_Passengers,
    [Vehicle_Make] = @Vehicle_Make,
    [Vehicle_Model] = @Vehicle_Model,
    [Vehicle_Year] = @Vehicle_Year

    WHERE @Model_Lookup_ID = [Model_Lookup].[Model_Lookup_ID]
        AND @OldMax_Passengers = [Max_Passengers]
        AND @OldVehicle_Make = [Vehicle_Make]
        AND @OldVehicle_Model = [Vehicle_Model]
        AND @OldVehicle_Year = [Vehicle_Year]

    UPDATE [Vehicle]
    SET [Vehicle_Number] = @Vehicle_Number,
    [Vehicle_Mileage] = @Vehicle_Mileage,
    [Vehicle].[Model_Lookup_ID] = @Model_Lookup_ID,
    [Vehicle_License_Plate] = @Vehicle_License_Plate,
    [Vehicle_Type] = @Vehicle_Type,
    [Description] = @Description,
    [Date_Entered] = @Date_Entered,
    [Rental] = @Rental
    WHERE @VIN = [VIN]
        AND @OldVehicle_Number = [Vehicle_Number]
        AND @OldVehicle_Mileage = [Vehicle_Mileage]
        AND @OldModel_Lookup_ID = [Vehicle].[Model_Lookup_ID]
        AND @OldVehicle_License_Plate = [Vehicle_License_Plate]
        AND @OldVehicle_Type = [Vehicle_Type]
        AND @OldDescription = [Description]
        AND @OldDate_Entered = [Date_Entered]
        AND @OldRental = [Rental]
    COMMIT TRANSACTION
    RETURN @@ROWCOUNT
END
GO

print ''
print '*** Create sp_deactivate_vehicle ***'
GO
-- Author: Chris Baenziger
CREATE PROCEDURE [dbo].[sp_deactivate_vehicle]
    (
    @VIN                    [nvarchar](17),
    @Vehicle_Number         [nvarchar](10),
    @Vehicle_Mileage        [int],
    @Vehicle_License_plate  [nvarchar](10),
    @Model_Lookup_ID        [int],
    @Vehicle_Type           [nvarchar](50),
    @Description            [nvarchar](256),
    @Date_Entered           [date],
    @Rental                 [bit]
)
AS
BEGIN
    UPDATE [Vehicle]
    SET [Is_Active] = 0
    WHERE @VIN = [VIN]
        AND @Vehicle_Number = [Vehicle_Number]
        AND @Vehicle_Mileage = [Vehicle_Mileage]
        AND @Model_Lookup_ID = [Vehicle].[Model_Lookup_ID]
        AND @Vehicle_License_Plate = [Vehicle_License_Plate]
        AND @Vehicle_Type = [Vehicle_Type]
        AND @Description = [Description]
        AND @Date_Entered = [Date_Entered]
        AND @Rental = [Rental]
END
GO