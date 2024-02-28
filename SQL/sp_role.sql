USE Night_Rider;
GO
print '' print '*** creating sp_get_all_active_role ***'
GO
-- Initial Creator: James Williams
-- Creation Date: 2024-02-01
-- Last Modified: Jacob Rohr
-- Modification Description: Added sp for create/update/get individual
-- Stored Procedure Description: get all active employee roles
CREATE PROCEDURE [dbo].[sp_get_all_active_role]
AS 
	BEGIN
		SELECT [Role_ID]
		FROM [Role]
		WHERE [Is_Active] = 1
	END
GO

print '' print '*** creating sp_update_role_by_role_ID ***'
GO


-- Stored Procedure Description: update roles by role id
CREATE PROCEDURE [dbo].[sp_update_role_by_role_ID](
	@RoleID[nvarchar](25),
	@RoleDescription[nvarchar](255),
	@IsActive[bit]
)
AS 
	BEGIN 
		UPDATE [role]
		SET 
			[Role_Description] = @RoleDescription,
			[Is_Active] = @IsActive
		WHERE 
			[Role_ID] = @RoleID
		RETURN @@ROWCOUNT
	END
GO
print '' print '*** creating sp_select_role_by_role_id***'
GO


-- used specifically when looking at an individual role
CREATE PROCEDURE [dbo].[sp_select_role_by_role_id](
	@RoleID[nvarchar](25)
)
AS 
	BEGIN
		SELECT [Role_ID], [Role_Description], [Is_Active]
		FROM [Role]
		WHERE [Role_ID] = @RoleID
	END
GO



print '' print '*** creating sp_create_role ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_role](
	@RoleID[nvarchar](25),
	@RoleDescription[nvarchar](255)
)
AS 
	BEGIN
		INSERT INTO [dbo].[role](
		[Role_ID], [Role_Description]
		)
		VALUES(
		@RoleID, 
		@RoleDescription
		)
	END
GO
		