/******************
Select_Client stored procedures
***************/

USE Night_Rider;
GO


print '' print '*** creating sp_select_client_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_client_id]
(
	@Client_ID			[nvarchar](50)
)
AS
	BEGIN
		SELECT 	[Client_ID],[Given_Name],[Family_Name],[Middle_Name],[DOB],[Email],[Postal_Code],[City],[Region],[Address],[Text_Number],[Voice_Number],[Is_Active]
		FROM 	[Client] 		
		WHERE	@Client_ID = [Client_ID]
	END
GO
