USE Night_Rider;
GO

/******************
Create sp_select_part_by_vendor_ID_and_part_number Stored Procedure
***************/
-- AUTHOR: Jonathan Beck
-- DATE CREATED: 3/15/2024
print '' print '*** creating sp_select_part_by_vendor_ID_and_part_number ***'
GO
CREATE PROCEDURE [dbo].[sp_select_part_by_vendor_ID_and_part_number]
(
    @Vendor_ID [int], @Parts_Inventory_ID [int]
)
AS
    BEGIN
        SELECT 
            [Vendor_ID], [Parts_Inventory_ID], 
            [Vendor_Part_Number], [Estimated_Delivery_Time_Days],
            [Part_Price], [Minimum_Order_Qty],
            [Active] 
        FROM [dbo].[Source]
        WHERE [Vendor_ID] = @Vendor_ID 
        AND [Parts_Inventory_ID] = @Parts_Inventory_ID 
    END 
GO