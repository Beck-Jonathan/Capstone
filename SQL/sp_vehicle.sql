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
    @Vehicle_Model_ID        [int],
    @Vehicle_Type_ID        [nvarchar](50),
    @Description            [nvarchar](256),
    @Date_Entered           [date],
    @Rental                 [bit]
)
AS
BEGIN
    INSERT INTO [dbo].[Vehicle]
        ([VIN],[Vehicle_Number],[Vehicle_Mileage],[Vehicle_License_Plate],[Vehicle_Model_ID],[Vehicle_Type_ID], [Description], [Date_Entered], [Rental])
    VALUES
        (@VIN, @Vehicle_Number, @Vehicle_Mileage, @Vehicle_License_Plate, @Vehicle_Model_ID, @Vehicle_Type_ID, @Description, @Date_Entered, @Rental)
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
-- Editor: Jared Hutton
-- Edit Date: 2024-03-02
-- Modification: change inner join on vehicle model to left outer join
print ''
print '*** creating sp_select_all_vehicles_for_vehicle_lookup_list ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_vehicles_for_vehicle_lookup_list]
AS
BEGIN
    SELECT
        [Vehicle].[Vehicle_Number],
        [Vehicle_Model].[Make],
        [Vehicle_Model].[Name],
        [Vehicle_Model].[Vehicle_Model_ID],
        [Vehicle_Model].[Max_Passengers],
        [Vehicle].[Vehicle_Mileage],
        [Vehicle].[Description],
        [Vehicle].[Vehicle_License_Plate],
        [Vehicle].[Vehicle_Type_ID],
        [Vehicle].[Rental],
        [Vehicle].[VIN]
    FROM [dbo].[Vehicle]
        LEFT JOIN [dbo].[Vehicle_Model] ON [Vehicle].[Vehicle_Model_ID] = [Vehicle_Model].[Vehicle_Model_ID]
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
    SELECT [Vehicle_Number], [VIN], [Vehicle].[Vehicle_Model_ID], [Vehicle_Model].[Make], [Vehicle_Model].[Name], [Vehicle_Model].[Year], [Vehicle_Mileage], [Vehicle_License_Plate], [Description], [Date_Entered], [Vehicle_Model].[Max_Passengers], [Vehicle].[Vehicle_Type_ID], [Rental], [Vehicle].[Is_Active]
    FROM [Vehicle]
        JOIN [Vehicle_Model]
        ON [Vehicle].[Vehicle_Model_ID] = [Vehicle_Model].[Vehicle_Model_ID]
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
    @OldVehicle_Model_ID        [int],
    @OldVehicle_License_Plate  [nvarchar](10),
    @OldDate_Entered           [date],
    @OldDescription            [nvarchar](256),
    @OldRental                 [bit],
    @OldMax_Passengers         [int],

    @Vehicle_Number         [nvarchar](10),
    @Vehicle_Mileage        [int],
    @Vehicle_Model_ID        [int],
    @Vehicle_License_Plate  [nvarchar](10),
    @Vehicle_Type_ID        [nvarchar](50),
    @Description            [nvarchar](256),
    @Date_Entered           [date],
    @Rental                 [bit],
    @Max_Passengers         [int]
)
AS
BEGIN
    UPDATE [Vehicle]
    SET [Vehicle_Number] = @Vehicle_Number,
    [Vehicle_Mileage] = @Vehicle_Mileage,
    [Vehicle].[Vehicle_Model_ID] = @Vehicle_Model_ID,
    [Vehicle_License_Plate] = @Vehicle_License_Plate,
    [Vehicle_Type_ID] = @Vehicle_Type_ID,
    [Description] = @Description,
    [Date_Entered] = @Date_Entered,
    [Rental] = @Rental
    WHERE @VIN = [VIN]
        AND @OldVehicle_Number = [Vehicle_Number]
        AND @OldVehicle_Mileage = [Vehicle_Mileage]
        AND @OldVehicle_Model_ID = [Vehicle].[Vehicle_Model_ID]
        AND @OldVehicle_License_Plate = [Vehicle_License_Plate]
        AND @OldDescription = [Description]
        AND @OldDate_Entered = [Date_Entered]
        AND @OldRental = [Rental];
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
    @Vehicle_Model_ID        [int],
    @Vehicle_Type_ID        [nvarchar](50),
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
        AND @Vehicle_Model_ID = [Vehicle].[Vehicle_Model_ID]
        AND @Vehicle_License_Plate = [Vehicle_License_Plate]
        AND @Vehicle_Type_ID = [Vehicle_Type_ID]
        AND @Description = [Description]
        AND @Date_Entered = [Date_Entered]
        AND @Rental = [Rental]
END
GO

/******************
Create sp_select_vehicle_by_vin Stored Procedure
***************/
-- Initial Creator: Ben Collins
-- Creation Date: 2024-03-24
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select a Vehicle record by the VIN.
print ''
print '*** creating sp_select_vehicle_by_vin ***'
GO
CREATE PROCEDURE [dbo].[sp_select_vehicle_by_vin]
    @VIN [nvarchar](17)
AS
BEGIN
    SELECT [Vehicle_Number],
        [Vehicle_Mileage],
        [Vehicle_License_Plate],
        [Vehicle_Model_ID],
        [Vehicle_Type_ID],
        [Date_Entered],
        [Description],
        [Maintenance_Notes],
        [Rental],
        [Is_Active]
    FROM [dbo].[Vehicle]
    WHERE [Vehicle].[VIN] = @VIN
END
GO

print ''
print '*** Create sp_vehicle_vin_and_number_for_dropdown ***'
GO
-- Author: Jonathan Beck
CREATE PROCEDURE [dbo].[sp_vehicle_vin_and_number_for_dropdown]

AS
BEGIN
    select
        [VIN],
        [Vehicle_Number]

    from [Vehicle]
    Where [Is_Active] = 1

END
GO
