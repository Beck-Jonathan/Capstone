USE Night_Rider;
GO

/******************
Create sp_select_all_employees Stored Procedure
***************/
 -- AUTHOR: Steven Sanchez
print '' print '*** creating sp_select_all_employees ***'
GO 
 CREATE PROCEDURE [dbo].[sp_select_all_employees]
AS
BEGIN
    SELECT
        [Employee_ID],
        [Given_Name],
        [Family_Name],
		[DOB],
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
END
GO

/******************
Create sp_select_employee_by_id Stored Procedure
***************/
 -- AUTHOR: Steven Sanchez
print '' print '*** creating sp_select_employee_by_id ***'
GO 
CREATE PROCEDURE [dbo].[sp_select_employee_by_id]
 (
    @Employee_ID int
 )
 AS
BEGIN
    SELECT 
        [Given_Name],
        [Family_Name],
		[DOB],
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
END
GO

/******************
Create sp_insert_employee Stored Procedure
***************/
-- Initial Creator: James Williams
-- Creation Date: 2024-02-01
-- Last Modified: James Williams
-- Modification Description: Initial Creation
-- Stored Procedure Description: Insert employee record, return Employee_ID
print '' print '*** creating sp_insert_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_employee] 
(
    @p_Given_Name 		nvarchar(50),
    @p_Family_Name 		nvarchar(50),
	@p_DOB				datetime,
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
		[DOB],
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
		@p_DOB,
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

/******************
Create sp_select_employees Stored Procedure
***************/
-- Initial Creator: James Williams
-- Creation Date: 2024-02-01
-- Last Modified: James Williams
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all active Employee records
print '' print '*** creating sp_select_employees ***'
GO
CREATE PROCEDURE [dbo].[sp_select_employees]
AS
BEGIN
	SELECT 
		[Employee_ID],
        [Given_Name],
        [Family_Name],
		[DOB],
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

/******************
Create sp_authenticate_employee_with_security_responses Stored Procedure
***************/
-- AUTHOR: Jared Hutton
print '' print '*** creating sp_authenticate_employee_with_security_responses ***'
GO
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

/******************
Create sp_update_employee Stored Procedure
***************/
--Author: James Williams
--Update Employee record
print'' print'*** Creating sp_update_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_update_employee]
(
	@p_Employee_ID			[int],
	@p_New_Given_Name		[nvarchar](50),
	@p_New_Family_Name		[nvarchar](50),
	@p_New_DOB				[datetime],
	@p_New_Address			[nvarchar](50),
	@p_New_Address2			[nvarchar](50),
	@p_New_City				[nvarchar](20),
	@p_New_State			[nvarchar](2),
	@p_New_Country			[nvarchar](3),
	@p_New_Zip				[nvarchar](9),
	@p_New_Phone_Number		[nvarchar](20),
	@p_New_Email			[nvarchar](50),
	@p_New_Position			[nvarchar](20),
	
	@p_Old_Given_Name		[nvarchar](50),
	@p_Old_Family_Name		[nvarchar](50),
	@p_Old_Address			[nvarchar](50),
	@p_Old_City				[nvarchar](20),
	@p_Old_State			[nvarchar](2),
	@p_Old_Country			[nvarchar](3),
	@p_Old_Zip				[nvarchar](9),
	@p_Old_Phone_Number		[nvarchar](20),
	@p_Old_Email			[nvarchar](50),
	@p_Old_Position			[nvarchar](20)
)
AS
BEGIN
	UPDATE [dbo].[Employee]
	SET
	[Given_Name] = @p_New_Given_Name,
	[Family_Name] = @p_New_Family_Name,		
	[DOB] = @p_New_DOB,				
	[Address] = @p_New_Address,			
	[Address2] = @p_New_Address2,			
	[City] = @p_New_City,				
	[State] = @p_New_State,			
	[Country] =@p_New_Country,			
	[Zip] = @p_New_Zip,				
	[Phone_Number] = @p_New_Phone_Number,		
	[Email] = @p_New_Email,			
	[Position] = @p_New_Position
	WHERE
	[Employee_ID] = @p_Employee_ID AND
	[Given_Name] = @p_Old_Given_Name AND
	[Family_Name] = @p_Old_Family_Name AND				
	[Address] = @p_Old_Address AND						
	[City] = @p_Old_City AND				
	[State] = @p_Old_State AND			
	[Country] =@p_Old_Country AND			
	[Zip] = @p_Old_Zip AND				
	[Phone_Number] = @p_Old_Phone_Number AND		
	[Email] = @p_Old_Email AND			
	[Position] = @p_Old_Position
END
GO

/******************
Create sp_deactivate_employee Stored Procedure
***************/
-- Created By: James Williams
-- Date: 2024-02-22
-- Deactivate employee record
print'' print'*** Creating sp_deactivate_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_employee]
(
	@p_Employee_ID				[int]
)
AS
	BEGIN
		UPDATE [dbo].[Employee]
		SET [Is_Active] = 0
		WHERE @p_Employee_ID = [Employee_ID]
	END
GO

/******************
Create sp_activate_employee Stored Procedure
***************/
-- Created By: James Williams
-- Date: 2024-02-22
-- Activate employee record
print'' print'*** Creating sp_activate_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_activate_employee]
(
	@p_Employee_ID				[int]
)
AS
	BEGIN
		UPDATE [dbo].[Employee]
		SET [Is_Active] = 1
		WHERE @p_Employee_ID = [Employee_ID]
	END
GO

/******************
Create sp_select_employee_by_email Stored Procedure
***************/
--Created By: Jacob Rohr
--Date: 2024-04-24
-- Select Employee By Email
print '' print '*** creating sp_select_employee_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_by_email]
 (
    @p_Email                 [nvarchar](50)
 )
AS
BEGIN
    SELECT 
        [Employee_ID],
        [Given_Name],
        [Family_Name],
        [DOB],
        [Address],
        [Address2],
        [City],
        [State],
        [Country],
        [Zip],
        [Phone_Number],
        [Position],
        [Is_Active]
    FROM [dbo].[Employee]
    WHERE [Email] = @p_Email
END
GO