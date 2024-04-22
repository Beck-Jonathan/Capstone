USE Night_Rider;
GO

/******************
Create sp_select_all_service_type Stored Procedure
***************/
-- Initial Creator: Steven Sanchez
-- Creation Date: 2024-04-16
-- Last Modified: Steven Sanchez
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all active Service_type records
print '' print '*** creating sp_select_all_service_type ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_service_type]
AS 
    BEGIN
        SELECT  [Service_Type].[Service_Type_ID],
                [Service_Type].[Service_Description],
                [Service_Type].[Is_Active]
        FROM   [dbo].[Service_Type]
    END;
GO