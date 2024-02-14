
USE Night_Rider;
GO
print ''
print '*** Create the sp_add_vehicle stored procedure ***'
GO

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


print ''
print '*** Create the sp_select_vehicle_types stored procedure ***'
GO

CREATE PROCEDURE [dbo].[sp_select_vehicle_types]
AS
BEGIN
    SELECT [Vehicle_Type]
    FROM [Vehicle_Type]
    WHERE [Is_Active] = 1
END
GO


print ''
print '*** Create the sp_select_vehicle_makes stored procedure ***'
GO

CREATE PROCEDURE [dbo].[sp_select_vehicle_makes]
AS
BEGIN
    SELECT [Vehicle_Make]
    FROM [Model_Lookup]
    WHERE [Active] = 1
END
GO


print ''
print '*** Create the sp_select_vehicle_models stored procedure ***'
GO

CREATE PROCEDURE [dbo].[sp_select_vehicle_models]
AS
BEGIN
    SELECT [Vehicle_Model]
    FROM [Model_Lookup]
    WHERE [Active] = 1
END
GO