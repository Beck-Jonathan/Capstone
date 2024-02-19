USE Night_Rider;
GO
print '' print '*** creating sp_get_all_active_role ***'
GO
-- Initial Creator: James Williams
-- Creation Date: 2024-02-01
-- Last Modified: James Williams
-- Modification Description: Initial Creation
-- Stored Procedure Description: get all active employee roles
CREATE PROCEDURE [dbo].[sp_get_all_active_role]
AS 
	BEGIN
		SELECT [Role_ID]
		FROM [Role]
		WHERE [Is_Active] = 1
	END
GO