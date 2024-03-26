USE Night_Rider;
GO
/******************
Create the retrieve by key script for the Parts_Inventory table
***************/
-- AUTHOR: Jonathan Beck
print '' print '*** creating sp_select_part_by_part_id ***'
 GO 
CREATE PROCEDURE [DBO].[sp_select_part_by_part_id]
(@Parts_Inventory_ID [int]
)
as
 Begin 
 select 
[Parts_Inventory_ID] 
,[Part_Name] 
,[Part_Quantity] 
,[Item_Description] 
,[Item_Specifications] 
,[Part_Photo_URL] 
,[Ordered_Qty] 
,[Stock_Level] 
,[Active] 

 FROM [dbo].[Parts_Inventory]
where [Parts_Inventory_ID]=@Parts_Inventory_ID 
 END 
 GO
 
 /******************
Create the retrieve by all script for the Parts_Inventory table
***************/
-- AUTHOR: Jonathan Beck
print '' print '*** creating sp_select_all_parts ***'
 GO 
CREATE PROCEDURE [DBO].[sp_select_all_parts]
AS
begin 
 SELECT 

[Parts_Inventory_ID]
,[Part_Name]
,[Part_Quantity]
,[Item_Description]
,[Item_Specifications]
,[Part_Photo_URL]
,[Ordered_Qty]
,[Stock_Level]
,[Active]
 FROM Parts_Inventory
 ;
 END  
 GO

  /******************
Create the retrieve by all script for the Parts_Inventory table
***************/
-- AUTHOR: Max Fare
print '' print '*** creating sp_select_parts ***'
 GO 
CREATE PROCEDURE [DBO].[sp_select_parts]
AS
begin 
 SELECT 

[Parts_Inventory_ID]
,[Part_Name]
,[Part_Quantity]
,[Item_Description]
,[Item_Specifications]
,[Part_Photo_URL]
,[Ordered_Qty]
,[Stock_Level]
,[Active]
 FROM Parts_Inventory
 WHERE [Active] = 1
 ;
 END  
 GO
 
 /******************
Create the update_part stored procedure
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
Create the deactivate_part stored procedure
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
        WHERE       [Parts_Inventory_ID] = @part_id;
        SELECT      @@ROWCOUNT
    END
GO

/******************
Create the insert_part stored procedure
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
    SET @p_Parts_Inventory_ID = SCOPE_IDENTITY();
    SELECT @p_Parts_Inventory_ID AS 'Parts_Inventory_ID';
    END
GO