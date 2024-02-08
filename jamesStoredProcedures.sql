use Night_Rider;

/***************
Stored Procedures
****************/

print '' print '*** SP to Select All Roles ***'
go
CREATE PROCEDURE [dbo].[sp_get_all_active_role]
AS 
	BEGIN
		SELECT [Role_ID]
		FROM [Role]
		WHERE [Is_Active] = 1
	END
go


print '' print '*** SP to enter new Employee record and return its identity int ***'
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
go

print '' print '*** SP Insert Employee Role ***'
go
-- Initial Creator: James Williams
-- Creation Date: 2024-02-01
-- Last Modified: James Williams
-- Modification Description: Initial Creation
-- Stored Procedure Description: Insert employee_role record
CREATE PROCEDURE [dbo].[sp_insert_employee_role]
(
	@p_Employee_ID			int,
	@p_Role_ID				nvarchar(25)
)
as
BEGIN
	INSERT INTO [dbo].[Employee_Role]
	([Employee_ID],[Role_ID])
	VALUES
	(@p_Employee_ID, @p_Role_ID)
END
go

print '' print '*** SP Select All Employees ***'
go
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
go