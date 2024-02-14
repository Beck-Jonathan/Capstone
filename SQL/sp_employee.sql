USE Night_Rider;
GO
print '' print '*** creating sp_select_all_employees ***'
GO 
 -- AUTHOR: Steven Sanchez
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
print '' print '*** creating sp_select_employee_by_id ***'
 GO 
 -- AUTHOR: Steven Sanchez
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

print '' print '*** creating sp_insert_employee ***'
GO
-- Initial Creator: James Williams
-- Creation Date: 2024-02-01
-- Last Modified: James Williams
-- Modification Description: Initial Creation
-- Stored Procedure Description: Insert employee record, return Employee_ID
CREATE PROCEDURE [dbo].[sp_insert_employee] 
(
    @p_Given_Name 		nvarchar(50),
    @p_Family_Name 		nvarchar(50),
    @p_Address 			nvarchar(50),
    @p_Address2 			nvarchar(50) = NULL,
    @p_City 				nvarchar(20),
    @p_State 				nvarchar(2),
    @p_Country 			nvarchar(3),
    @p_Zip 				nvarchar(9),
    @p_Phone_Number 		nvarchar(20),
    @p_Email 				nvarchar(50),
    @p_Position 			nvarchar(20)
)
AS
BEGIN
    DECLARE @p_Employee_ID int

    INSERT INTO [dbo].[Employee] (
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
        [Position]
    )
    VALUES (
        @p_Given_Name,
        @p_Family_Name,
        @p_Address,
        @p_Address2,
        @p_City,
        @p_State,
        @p_Country,
        @p_Zip,
        @p_Phone_Number,
        @p_Email,
        @p_Position
    );

    SET @p_Employee_ID = SCOPE_IDENTITY();
	
	SELECT @p_Employee_ID AS 'Employee_ID';
END
GO

print '' print '*** creating sp_select_employees ***'
GO
-- Initial Creator: James Williams
-- Creation Date: 2024-02-01
-- Last Modified: James Williams
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all active Employee records
CREATE PROCEDURE [dbo].[sp_select_employees]
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
        [Position]
	
		FROM [dbo].[Employee]
		WHERE [Is_Active] = 1
END
GO

print '' print '*** creating sp_authenticate_employee_with_security_responses ***'
GO
-- AUTHOR: Jared Hutton
CREATE PROCEDURE [dbo].[sp_authenticate_employee_with_security_responses] (
  @Username [nvarchar](50),
  @Password_Hash [nvarchar](100),
  @Security_Response_1 [nvarchar](100) = NULL,
  @Security_Response_2 [nvarchar](100) = NULL,
  @Security_Response_3 [nvarchar](100) = NULL
)
AS
BEGIN
  SELECT
    er.[Role_ID],
    e.[Employee_ID],
    e.[Given_Name],
    e.[Family_Name],
    e.[Address],
    e.[Address2],
    e.[City],
    e.[State],
    e.[Country],
    e.[Zip],
    e.[Phone_Number],
    e.[Email],
    e.[Position]
  FROM [dbo].[Employee] e
  LEFT JOIN [dbo].[Employee_Role] er ON e.[Employee_ID] = er.[Employee_ID]
  JOIN [dbo].[Login] l ON e.[Employee_ID] = l.[Employee_ID]
  WHERE
    l.[Active] = 1
    AND e.[Is_Active] = 1
    AND l.[Username] = @Username
    AND l.[Password_Hash] = @Password_Hash
    AND (l.[Security_Response_1] IS NULL OR l.[Security_Response_1] = @Security_Response_1)
    AND (l.[Security_Response_2] IS NULL OR l.[Security_Response_2] = @Security_Response_2)
    AND (l.[Security_Response_3] IS NULL OR l.[Security_Response_3] = @Security_Response_3);
END;
GO