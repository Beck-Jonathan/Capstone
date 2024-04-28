USE Night_Rider;
GO

/******************
Create sp_select_all_active_scheduled_maintenance Stored Procedure
***************/
-- Initial Creator: Jared Roberts
-- Creation Date: 2024-04-23
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all incomplete Scheduled Service_Order records
print '' print '*** creating sp_select_all_active_scheduled_maintenance ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_incomplete_scheduled_maintenance]
AS 
    BEGIN
        SELECT  [dbo].[Maintenance_Schedule].[Maintenance_Schedule_ID],
				[dbo].[Maintenance_Schedule].[Vehicle_Model_ID],
				[dbo].[Maintenance_Schedule].[Service_Type_ID],
				[dbo].[Maintenance_Schedule].[Frequency_In_Months],
				[dbo].[Maintenance_Schedule].[Frequency_In_Miles],
				[dbo].[Maintenance_Schedule].[Is_Completed],
				[dbo].[Maintenance_Schedule].[Active]
        FROM   [dbo].[Maintenance_Schedule]
        INNER JOIN [dbo].[Service_Type] ON [dbo].[Maintenance_Schedule].[Service_Type_ID] = [dbo].[Service_Type].[Service_Type_ID]
		INNER JOIN [dbo].[Vehicle_Model] ON [dbo].[Maintenance_Schedule].[Vehicle_Model_ID] = [dbo].[Vehicle_Model].[Vehicle_Model_ID]
        WHERE [dbo].[Maintenance_Schedule].[Active] = 1
    END;
GO

/******************
Create sp_select_all_active_scheduled_maintenance Stored Procedure
***************/
-- Initial Creator: Jared Roberts
-- Creation Date: 2024-04-23
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all completed Scheduled Service_Order records
print '' print '*** creating sp_select_all_completed_scheduled_maintenance ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_completed_scheduled_maintenance]
AS 
    BEGIN
        SELECT  [dbo].[Maintenance_Schedule].[Maintenance_Schedule_ID],
				[dbo].[Maintenance_Schedule].[Vehicle_Model_ID],
				[dbo].[Maintenance_Schedule].[Service_Type_ID],
				[dbo].[Maintenance_Schedule].[Frequency_In_Months],
				[dbo].[Maintenance_Schedule].[Frequency_In_Miles],
				[dbo].[Maintenance_Schedule].[Is_Completed],
				[dbo].[Maintenance_Schedule].[Active]
        FROM   [dbo].[Maintenance_Schedule]
        INNER JOIN [dbo].[Service_Type] ON [dbo].[Maintenance_Schedule].[Service_Type_ID] = [dbo].[Service_Type].[Service_Type_ID]
		INNER JOIN [dbo].[Vehicle_Model] ON [dbo].[Maintenance_Schedule].[Vehicle_Model_ID] = [dbo].[Vehicle_Model].[Vehicle_Model_ID]
        WHERE [dbo].[Maintenance_Schedule].[Active] = 0
    END;
GO

/******************
Create sp_select_all_scheduled_maintenance Stored Procedure
***************/
-- Initial Creator: Jared Roberts
-- Creation Date: 2024-04-23
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all Scheduled Service_Order records
print '' print '*** creating sp_select_all_scheduled_maintenance ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_scheduled_maintenance]
AS 
    BEGIN
        SELECT  [dbo].[Maintenance_Schedule].[Maintenance_Schedule_ID],
				[dbo].[Maintenance_Schedule].[Vehicle_Model_ID],
				[dbo].[Maintenance_Schedule].[Service_Type_ID],
				[dbo].[Maintenance_Schedule].[Frequency_In_Months],
				[dbo].[Maintenance_Schedule].[Frequency_In_Miles],
				[dbo].[Maintenance_Schedule].[Is_Completed],
				[dbo].[Maintenance_Schedule].[Active]
        FROM   [dbo].[Maintenance_Schedule]
        INNER JOIN [dbo].[Service_Type] ON [dbo].[Maintenance_Schedule].[Service_Type_ID] = [dbo].[Service_Type].[Service_Type_ID]
		INNER JOIN [dbo].[Vehicle_Model] ON [dbo].[Maintenance_Schedule].[Vehicle_Model_ID] = [dbo].[Vehicle_Model].[Vehicle_Model_ID]
    END;
GO