USE Night_Rider;
GO
print '' print '*** creating sp_insert_employee_role ***'
GO
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
GO

-- Initial Creator: James Williams
-- Creation Date: 2024-02-06
-- Last Modified: James Williams
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select employee role(s) by Employee_ID
print '' print '*** creating sp_get_roles_by_employee_id ***'
GO
CREATE PROCEDURE [dbo].[sp_get_roles_by_employee_id]
(
	@p_Employee_ID				[INT]
)
as
BEGIN
	SELECT [Role_ID]
	FROM [dbo].[Employee_Role]
	WHERE @p_Employee_ID = Employee_ID AND 
		Is_Active = 1
END
GO