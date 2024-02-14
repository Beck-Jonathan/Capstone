USE Night_Rider;
GO
print '' Print '***Create the retreive Employee List script for the Employee table***' 
 go 
 CREATE PROCEDURE [dbo].[sp_select_all_employees]
AS
BEGIN
    SELECT
        [Employee_ID],
        [Given_Name],
        [Family_Name],
        [Address],
        [Address2],
        [City],
        [State],
        [Country],
        [Zip],
        [Phone_Number],
        [Email],
        [Position],
        [Is_Active]
    FROM
        Employee;
END;
GO
-- Checked by James Williams
print '' Print '***Create the retreive Employee by ID script for the Employee table***' 
 go 

 CREATE PROCEDURE [dbo].[sp_select_employee_by_id]
 (
    @Employee_ID int
 )
 AS
BEGIN
    SELECT 
        [Given_Name],
        [Family_Name],
        [Address],
        [Address2],
        [City],
        [State],
        [Country],
        [Zip],
        [Phone_Number],
        [Email],
        [Position],
        [Is_Active]
    FROM
        [Employee]
    WHERE [Employee_ID] = @Employee_ID;
END;
GO


