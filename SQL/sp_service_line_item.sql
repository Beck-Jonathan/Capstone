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