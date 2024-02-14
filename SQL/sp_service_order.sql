USE Night_Rider;
GO
/******************
Create sp_select_all_active_service_orders Stored Procedure
***************/
-- Initial Creator: Ben Collins
-- Creation Date: 2024-02-10
-- Last Modified: Ben Collins
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all active Service_Order records
print '' print '*** creating sp_select_all_active_service_orders ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_active_service_orders]
AS 
    BEGIN
        SELECT  [Service_Order].[VIN],
                [Service_Order].[Service_Order_ID],
                [Service_Order].[Critical_Issue],
                [Service_Type].[Service_Type_ID],
                [Service_Type].[Service_Description]
        FROM   [dbo].[Service_Order]
        INNER JOIN [dbo].[Service_Type] ON [Service_Order].[Service_Type_ID] = [Service_Type].[Service_Type_ID]
        WHERE [Service_Order].[Is_Active] = 1
    END
GO