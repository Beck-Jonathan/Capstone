USE Night_Rider;
GO

/******************
Create sp_add_compatible_part Stored Procedure
***************/
 -- AUTHOR: James Williams
print '' print '*** creating sp_add_compatible_part ***'
GO
CREATE PROCEDURE [dbo].[sp_add_compatible_part]
(
	@p_Vehicle_Model_ID int,
	@p_Part_Inventory_ID int
)
AS
	BEGIN
		INSERT INTO [dbo].[Model_Compatibility] (
			[Vehicle_Model_ID], [Parts_Inventory_ID]
		)
		VALUES (
			@p_Vehicle_Model_ID, @p_Part_Inventory_ID
		)
	END
GO

/******************
Create sp_delete_Model_Compatibility Stored Procedure
***************/
-- AUTHOR: Jonathan Beck
-- CREATED: 3/20/2024
print '' print '*** creating sp_delete_Model_Compatibility ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_Model_Compatibility]
(
    @Model_Lookup_ID[int],
    @Parts_Inventory_ID[int]
)
AS
    BEGIN
        DELETE FROM [dbo].[Model_Compatibility]
        WHERE [Vehicle_Model_ID] = @Model_Lookup_ID
        AND [Parts_Inventory_ID] = @Parts_Inventory_ID
        RETURN @@ROWCOUNT
    END
GO
