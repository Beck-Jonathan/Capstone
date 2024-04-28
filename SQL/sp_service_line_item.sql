USE Night_Rider;
GO

/******************
Create sp_select_all_service_line_items Stored Procedure
***************/
-- Initial Creator: Ben Collins
-- Creation Date: 2024-02-26
-- Last Modified: Ben Collins
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all Service_Line_Item records
print '' print '*** creating sp_select_all_service_line_items ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_service_line_items]
AS 
    BEGIN
            SELECT  *
            FROM   [dbo].[Service_Line_Item]
    END;
GO

/******************
Create Insert Service Line Item Stored Procedure
***************/
-- Initial Creator: Max Fare
-- Creation Date: 2024-04-05
-- Last Modified: 
-- Modification Description:
print '' print '*** creating sp_insert_service_order_line_item ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_service_order_line_item]
(
    @Service_Order_ID       [int],
    @Service_Order_Version  [int],
    @Parts_Inventory_ID     [int],
    @Quantity               [int]
)
AS
    BEGIN
        INSERT INTO [dbo].[Service_Line_Item]
        ([Service_Order_ID], [Service_Order_Version], [Parts_Inventory_ID], [Quantity])
        VALUES
        (@Service_Order_ID, @Service_Order_Version, @Parts_Inventory_ID, @Quantity)
    END
GO