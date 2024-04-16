USE Night_Rider;
GO

print ''
print '*** Create sp_select_driver_and_schedule ***'
GO
-- AUTHOR: Everett DeVaux
-- CREATED: 2024-03-24
CREATE PROCEDURE sp_select_driver_and_schedule 
AS
BEGIN
  SELECT 
  	[Driver].[Employee_ID],
	[Driver].[Driver_License_Class_ID],
	[Schedule].[Driver_ID],
	[Schedule].[Schedule_ID],
    [Schedule].[Week_Days], 
	[Schedule].[Start_Time], 
	[Schedule].[End_Time], 
	[Schedule].[Start_Date], 
	[Schedule].[End_Date], 
	[Schedule].[Notes], 
	[Schedule].[Is_Active]
  FROM [dbo].[Driver]
  	INNER JOIN [dbo].[Schedule] ON [Driver].[Employee_ID] = [Schedule].[Driver_ID]
	WHERE [Schedule].[Is_Active] = 1;
END;