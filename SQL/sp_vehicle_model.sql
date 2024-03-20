USE Night_Rider;
GO

print ''
print '*** creating sp_select_vehicle_makes ***'
GO
-- AUTHOR: Chris Baenziger
CREATE PROCEDURE [dbo].[sp_select_vehicle_makes]
AS
BEGIN
    SELECT DISTINCT [Make]
    FROM [Vehicle_Model]
    WHERE [Is_Active] = 1
END
GO

print ''
print '*** creating sp_select_vehicle_models ***'
GO
-- AUTHOR: Chris Baenziger
CREATE PROCEDURE [dbo].[sp_select_vehicle_models]
AS
BEGIN
    SELECT DISTINCT [Vehicle_Model_ID]
    FROM [Vehicle_Model]
    WHERE [Is_Active] = 1
END
GO

print''
print '*** Create the sp_add_vehicle_model ***'
GO
-- AUTHOR: Chris Baenziger
CREATE PROCEDURE [dbo].[sp_add_vehicle_model] (
    @Name            [nvarchar](255),
    @Make            [nvarchar](255),
    @Vehicle_Type_Id [nvarchar](255),
    @Year            [int],
    @Max_Passengers  [int]
)
AS
BEGIN
    INSERT INTO [dbo].[Vehicle_Model] ([Name], [Make], [Year], [Vehicle_Type_Id], [Max_Passengers])
      VALUES (@Name, @Make, @Year, @Vehicle_Type_Id, @Max_Passengers);
    SELECT SCOPE_IDENTITY();
END
GO

print ''
print '*** create sp_lookup_vehicle_model_id_from_make_model_year ***'
GO
-- AUTHOR: Chris Baenziger
CREATE PROCEDURE [dbo].[sp_lookup_vehicle_model_id_from_make_model_year]
    (
    @Max_Passengers [int],
    @Make       [nvarchar](255),
    @Model_ID      [nvarchar](255),
    @Year       [int]
)
AS
BEGIN
    SELECT [Vehicle_Model_ID]
    FROM [Vehicle_Model]
    WHERE [Year] = @Year
        AND [Make] = @Make
        AND [Vehicle_Model_ID] = @Model_ID
        AND [Max_Passengers] = @Max_Passengers
END
GO

print '' print '*** create sp_get_vehicle_models ***'
GO
-- AUTHOR: Jared Hutton
CREATE PROCEDURE [dbo].[sp_get_vehicle_models]
AS
BEGIN
  SELECT [Vehicle_Model_ID], [Vehicle_Type_ID], [Name], [Make], [Year], [Max_Passengers], [Is_Active]
  FROM [dbo].[Vehicle_Model]
  WHERE [Is_Active] = 1;
END;
GO
