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
