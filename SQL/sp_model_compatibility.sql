USE Night_Rider;
GO

print '' print '*** SP add compatible part to model ***'
GO
CREATE PROCEDURE [dbo].[sp_add_compatible_part]
(
	@p_Vehicle_Model_ID int,
	@p_Part_Inventory_ID int
)
AS
BEGIN
	INSERT INTO [dbo].[Model_Compatibility]
	(Vehicle_Model_ID, Parts_Inventory_ID)
	VALUES(
	@p_Vehicle_Model_ID,
	@p_Part_Inventory_ID
	)
END

GO