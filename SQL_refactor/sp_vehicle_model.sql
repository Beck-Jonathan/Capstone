USE Night_Rider;
GO

/******************
Create sp_select_vehicle_makes Stored Procedure
***************/
-- AUTHOR: Chris Baenziger
print '' print '*** creating sp_select_vehicle_makes ***'
GO
CREATE PROCEDURE [dbo].[sp_select_vehicle_makes]
AS
BEGIN
    SELECT DISTINCT [Make]
    FROM [Vehicle_Model]
    WHERE [Is_Active] = 1
END
GO

/******************
Create sp_select_vehicle_models Stored Procedure
***************/
-- AUTHOR: Chris Baenziger
print '' print '*** creating sp_select_vehicle_models ***'
GO
CREATE PROCEDURE [dbo].[sp_select_vehicle_models]
AS
BEGIN
    SELECT DISTINCT [Vehicle_Model_ID]
    FROM [Vehicle_Model]
    WHERE [Is_Active] = 1
END
GO

/******************
Create sp_add_vehicle_model Stored Procedure
***************/
-- AUTHOR: Chris Baenziger
print'' print '*** Create the sp_add_vehicle_model ***'
GO
CREATE PROCEDURE [dbo].[sp_add_vehicle_model] 
(
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
        SELECT CAST( SCOPE_IDENTITY() AS int);
    END
GO

/******************
Create sp_lookup_vehicle_model_id_from_make_model_year Stored Procedure
***************/
-- AUTHOR: Chris Baenziger
print '' print '*** create sp_lookup_vehicle_model_id_from_make_model_year ***'
GO
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
        FROM [dbo].[Vehicle_Model]
        WHERE [Year] = @Year
            AND [Make] = @Make
            AND [Vehicle_Model_ID] = @Model_ID
            AND [Max_Passengers] = @Max_Passengers
    END
GO

/******************
Create sp_get_vehicle_models Stored Procedure
***************/
-- AUTHOR: Jared Hutton
print '' print '*** create sp_get_vehicle_models ***'
GO
CREATE PROCEDURE [dbo].[sp_get_vehicle_models]
AS
    BEGIN
      SELECT [Vehicle_Model_ID], [Vehicle_Type_ID], [Name], [Make], [Year], [Max_Passengers], [Is_Active]
      FROM [dbo].[Vehicle_Model]
      WHERE [Is_Active] = 1;
    END
GO
