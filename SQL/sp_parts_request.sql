USE Night_Rider;
GO


/******************
Create sp_select_all_active_part_requests Stored Procedure
***************/
-- Initial Creator: Ben Collins
-- Creation Date: 2024-02-10
-- Last Modified: Ben Collins
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all active Service_Order records
print '' print '*** creating sp_select_all_active_part_requests ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_active_part_requests]
AS 
    BEGIN
        SELECT  [Parts_Request].[Parts_Request_ID],
                [Parts_Request].[Date_Requested],
                [Parts_Request_Line_Items].[Qty_Requested],
                [Parts_Inventory].[Part_Name]
        FROM   [dbo].[Parts_Request_Line_Items]
        INNER JOIN [dbo].[Parts_Request] 
            ON [Parts_Request_Line_Items].[Parts_Request_ID] = [Parts_Request].[Parts_Request_ID]
        INNER JOIN [dbo].[Parts_Inventory] 
            ON [Parts_Request_Line_Items].[Parts_Inventory_ID] = [Parts_Inventory].[Parts_Inventory_ID]
        WHERE [Parts_Request].[Is_Active] = 1
    END;
GO

