USE Night_Rider;
GO

/******************
Create sp_select_part_by_part_id Stored Procedure
***************/
-- AUTHOR: Jonathan Beck
print '' print '*** creating sp_select_part_by_part_id ***'
GO 
CREATE PROCEDURE [dbo].[sp_select_part_by_part_id]
(
    @Parts_Inventory_ID [int]
)
AS
    BEGIN
        SELECT [Parts_Inventory_ID], [Part_Name], [Part_Quantity], 
        [Item_Description], [Item_Specifications], [Part_Photo_URL],
        [Ordered_Qty], [Stock_Level], [Active] 
        FROM [dbo].[Parts_Inventory]
        WHERE [Parts_Inventory_ID] = @Parts_Inventory_ID 
    END 
GO
 
/******************
Create sp_select_all_parts Stored Procedure
***************/
-- AUTHOR: Jonathan Beck
print '' print '*** creating sp_select_all_parts ***'
GO 
CREATE PROCEDURE [dbo].[sp_select_all_parts]
AS
    BEGIN 
        SELECT [Parts_Inventory_ID], [Part_Name], [Part_Quantity],
            [Item_Description], [Item_Specifications], [Part_Photo_URL],
            [Ordered_Qty], [Stock_Level], [Active]
        FROM [dbo].[Parts_Inventory]
    END  
GO

/******************
Create sp_select_parts Stored Procedure
***************/
-- AUTHOR: Max Fare
print '' print '*** creating sp_select_parts ***'
GO 
CREATE PROCEDURE [dbo].[sp_select_parts]
AS
    BEGIN 
        SELECT [Parts_Inventory_ID], [Part_Name], 
            [Part_Quantity], [Item_Description], 
            [Item_Specifications], [Part_Photo_URL],
            [Ordered_Qty], [Stock_Level], [Active]
        FROM [dbo].[Parts_Inventory]
        WHERE [Active] = 1
    END  
GO
 
 /******************
Create sp_update_part Stored Procedure
***************/
-- AUTHOR: Max Fare
print '' print '*** creating sp_update_part ***'
GO
CREATE PROCEDURE [dbo].[sp_update_part]
(
    @part_id                [int],

    @new_Part_Name          [nvarchar](30),
    @new_Part_Qty           [int],
    @new_Item_Description   [nvarchar](100),
    @new_Item_Specifications    [nvarchar](MAX),
    @new_Part_Photo_URL     [nvarchar](255),
    @new_ordered_qty        [int],
    @new_stock_lvl          [int],

    @old_Part_Name          [nvarchar](30),
    @old_Part_Qty           [int],
    @old_Item_Description   [nvarchar](100),
    @old_Item_Specifications    [nvarchar](MAX),
    @old_Part_Photo_URL     [nvarchar](255),
    @old_ordered_qty        [int],
    @old_stock_lvl          [int]
)
AS
    BEGIN 
        UPDATE      [Parts_Inventory]
        SET         [Part_Name] = @new_Part_Name,
                    [Part_Quantity] = @new_Part_Qty,
                    [Item_Description] = @new_Item_Description,
                    [Item_Specifications] = @new_Item_Specifications,
                    [Part_Photo_URL] = @new_Part_Photo_URL,
                    [Ordered_Qty] = @new_ordered_qty,
                    [Stock_Level] = @new_stock_lvl
        WHERE       @part_id = [Parts_Inventory_ID]
            AND     @old_Part_Name = [Part_Name]
            AND     @old_Part_Qty = [Part_Quantity]
            AND     @old_Item_Description = [Item_Description]
            AND     @old_Item_Specifications = [Item_Specifications]
            AND     @old_Part_Photo_URL = [Part_Photo_URL]
            AND     @old_ordered_qty = [Ordered_Qty]
            AND     @old_stock_lvl = [Stock_Level]
        SELECT      @@ROWCOUNT
    END
GO

 /******************
Create sp_deactivate_part Stored Procedure
***************/
-- AUTHOR: Max Fare
print '' print '*** creating sp_deactivate_part ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_part]
(
    @part_id                [int]
)
AS
    BEGIN
        UPDATE      [Parts_Inventory]
        SET         [Active] = 0
        WHERE       [Parts_Inventory_ID] = @part_id
        SELECT      @@ROWCOUNT
    END
GO

/******************
Create sp_insert_part Stored Procedure
***************/
-- AUTHOR: Max Fare
print '' print '*** creating sp_insert_part ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_part]
(
    @Part_Name [nvarchar](30),
    @Part_Quantity [int],
    @Item_Description [nvarchar](100),
    @Item_Specifications [nvarchar](MAX),
    @Part_Photo_URL [nvarchar](255),
    @Ordered_Qty [int],
    @Stock_Level [int]
)
AS
    BEGIN
    DECLARE @p_Parts_Inventory_ID int;
    INSERT INTO [dbo].[Parts_Inventory]
    (
        [Part_Name],
        [Part_Quantity],
        [Item_Description],
        [Item_Specifications],
        [Part_Photo_URL],
        [Ordered_Qty],
        [Stock_Level]
    )
    VALUES(
        @Part_Name,
        @Part_Quantity,
        @Item_Description,
        @Item_Specifications,
        @Part_Photo_URL,
        @Ordered_Qty,
        @Stock_Level
    )
    SET @p_Parts_Inventory_ID = SCOPE_IDENTITY()
    SELECT @p_Parts_Inventory_ID AS 'Parts_Inventory_ID'
    END
GO

/******************
Create sp_select_parts_compatible_with_vehicle_model_id Stored Procedure
***************/
-- AUTHOR: Jared Hutton
print '' print '*** creating sp_select_parts_compatible_with_vehicle_model_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_parts_compatible_with_vehicle_model_id] (
  @Vehicle_Model_Id [INT]
)
AS
BEGIN
  SELECT pi.[Part_Name], pi.[Part_Quantity], mc.[Parts_Inventory_ID]
  FROM [dbo].[Parts_Inventory] pi
  JOIN [dbo].[Model_Compatibility] mc ON pi.[Parts_Inventory_ID] = mc.[Parts_Inventory_ID]
  WHERE mc.[Vehicle_Model_ID] = @Vehicle_Model_ID
END
GO

/******************
Create sp_update_parts_inventory_ordered_quantity Stored Procedure
***************/
-- AUTHOR: Jonathan Beck
-- DATE CREATED: 3/20/2024
print '' print '*** creating sp_update_parts_inventory_ordered_quantity ***'
GO
CREATE PROCEDURE [dbo].[sp_update_parts_inventory_ordered_quantity]
(
    @Parts_Inventory_ID[int], @New_Ordered_Qty[int]
)
AS
    BEGIN
        UPDATE [dbo].[Parts_Inventory]
        SET [Ordered_Qty] = @New_Ordered_Qty
        WHERE [Parts_Inventory_ID] = @Parts_Inventory_ID
        RETURN @@ROWCOUNT
    END
GO