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
				[dbo].[Maintenance_Schedule].[Time_Last_Completed],
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
				[dbo].[Maintenance_Schedule].[Time_Last_Completed],
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
				[dbo].[Maintenance_Schedule].[Time_Last_Completed],
				[dbo].[Maintenance_Schedule].[Active]
        FROM   [dbo].[Maintenance_Schedule]
        INNER JOIN [dbo].[Service_Type] ON [dbo].[Maintenance_Schedule].[Service_Type_ID] = [dbo].[Service_Type].[Service_Type_ID]
		INNER JOIN [dbo].[Vehicle_Model] ON [dbo].[Maintenance_Schedule].[Vehicle_Model_ID] = [dbo].[Vehicle_Model].[Vehicle_Model_ID]
    END;
GO

/******************
Create the create new Maintenance Schedule stored procedure
***************/
-- AUTHOR: Max 
-- CREATION DATE: 2024-03-29
print '' print '*** creating sp_insert_maintenance_schedule ***'
 GO 
CREATE PROCEDURE [dbo].[sp_insert_maintenance_schedule]
(
    @ModelID   [int],
    @ServiceTypeID  [nvarchar](256),
    @FrequencyInMonths  [int],
    @FrequencyInMiles   [int],
    @TimeLastCompleted  [datetime]
)
as
 Begin 
    DECLARE @p_schedule_ID int

    INSERT INTO [dbo].[Maintenance_Schedule] (
        [Vehicle_Model_ID],
        [Service_Type_ID],
        [Frequency_In_Months],
        [Frequency_In_Miles],
        [Time_Last_Completed]
    )
    VALUES(
        @ModelID,
        @ServiceTypeID,
        @FrequencyInMonths,
        @FrequencyInMiles,
        @TimeLastCompleted
    )
    SET @p_schedule_ID = SCOPE_IDENTITY();
	
	SELECT @p_schedule_ID AS 'Maintenance_Schedule_ID';
 END 
GO