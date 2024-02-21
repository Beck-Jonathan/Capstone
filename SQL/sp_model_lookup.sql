USE Night_Rider;
GO

print ''
print '*** creating sp_select_vehicle_makes ***'
GO
-- AUTHOR: Chris Baenziger
CREATE PROCEDURE [dbo].[sp_select_vehicle_makes]
AS
BEGIN
    SELECT DISTINCT [Vehicle_Make]
    FROM [Model_Lookup]
    WHERE [Active] = 1
END
GO

print ''
print '*** creating sp_select_vehicle_models ***'
GO
-- AUTHOR: Chris Baenziger
CREATE PROCEDURE [dbo].[sp_select_vehicle_models]
AS
BEGIN
    SELECT DISTINCT [Vehicle_Model]
    FROM [Model_Lookup]
    WHERE [Active] = 1
END
GO


print''
print '*** Create the sp_add_model_lookup ***'
GO
-- AUTHOR: Chris Baenziger
CREATE PROCEDURE [dbo].[sp_add_model_lookup]
    (
    @Max_Passengers         [int],
    @Vehicle_Make           [nvarchar](255),
    @Vehicle_Model          [nvarchar](255),
    @Vehicle_Year           [int]
)
AS
BEGIN
    INSERT INTO [dbo].[Model_Lookup]
        ([Max_Passengers], [Vehicle_Make], [Vehicle_Model], [Vehicle_Year])
    VALUES
        (@Max_Passengers, @Vehicle_Make, @Vehicle_Model, @Vehicle_Year)
    SELECT SCOPE_IDENTITY();
END
GO


print ''
print '*** create sp_lookup_model_lookup_id_from_make_model_year ***'
GO
-- AUTHOR: Chris Baenziger
CREATE PROCEDURE [dbo].[sp_lookup_model_lookup_id_from_make_model_year]
    (
    @Max_passengers [int],
    @Make       [nvarchar](255),
    @Model      [nvarchar](255),
    @Year       [int]
)
AS
BEGIN
    SELECT [Model_Lookup_ID]
    FROM [Model_Lookup]
    WHERE [Vehicle_Year] = @Year
        AND [Vehicle_Make] = @Make
        AND [Vehicle_Model] = @Model
        AND [Max_Passengers] = @Max_Passengers
END
GO
