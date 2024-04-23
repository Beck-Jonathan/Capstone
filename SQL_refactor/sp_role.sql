USE Night_Rider;
GO

/******************
Create sp_get_all_active_role Stored Procedure
***************/
-- Initial Creator: James Williams
-- Creation Date: 2024-02-01
-- Last Modified: James Williams
-- Modification Description: Initial Creation
-- Stored Procedure Description: get all active employee roles
print '' print '*** creating sp_get_all_active_role ***'
GO
CREATE PROCEDURE [dbo].[sp_get_all_active_role]
AS 
	BEGIN
		SELECT [Role_ID]
		FROM [dbo].[Role]
		WHERE [Is_Active] = 1
	END
GO

/******************
Create sp_update_role_by_role_ID Stored Procedure
***************/
-- AUTHOR: Jacob Rohr
-- Stored Procedure Description: update roles by role id
print '' print '*** creating sp_update_role_by_role_ID ***'
GO
CREATE PROCEDURE [dbo].[sp_update_role_by_role_ID](
	@RoleID[nvarchar](25),
	@RoleDescription[nvarchar](255),
	@IsActive[bit]
)
AS 
	BEGIN 
		UPDATE [dbo].[Role]
		SET 
			[Role_Description] = @RoleDescription,
			[Is_Active] = @IsActive
		WHERE 
			[Role_ID] = @RoleID
		RETURN @@ROWCOUNT
	END
GO

/******************
Create sp_select_role_by_role_id Stored Procedure
***************/
-- AUTHOR: Jacob Rohr
-- used specifically when looking at an individual role
print '' print '*** creating sp_select_role_by_role_id***'
GO
CREATE PROCEDURE [dbo].[sp_select_role_by_role_id](
	@RoleID[nvarchar](25)
)
AS 
	BEGIN
		SELECT [Role_ID], [Role_Description], [Is_Active]
		FROM [dbo].[Role]
		WHERE [Role_ID] = @RoleID
	END
GO


/******************
Create sp_insert_role Stored Procedure
***************/
-- AUTHOR: Jacob Rohr
print '' print '*** creating sp_insert_role ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_role](
	@RoleID[nvarchar](25),
	@RoleDescription[nvarchar](255)
)
AS 
	BEGIN
		INSERT INTO [dbo].[Role] (
			[Role_ID], [Role_Description]
		) VALUES (
			@RoleID, @RoleDescription
		)
	END
GO