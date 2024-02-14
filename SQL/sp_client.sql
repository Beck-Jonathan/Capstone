USE Night_Rider;
GO
/******************
Create sp_select_all_client Stored Procedure
***************/
-- AUTHOR: Isabella Rosenbohm
print '' print '*** creating sp_select_all_client ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_client]
AS 
	BEGIN
		SELECT	[Client_ID],
				[Given_Name],
				[Family_Name],
				[Middle_Name],
				[DOB],
				[Email],
				[City],
				[Region],
				[Address],
				[Text_Number],
				[Voice_Number],
				[Postal_Code],
				[Is_Active]
		FROM	[Client]
	END
GO

print '' print '*** creating sp_authenticate_client_with_security_responses ***'
GO
-- AUTHOR: Jared Hutton
CREATE PROCEDURE [dbo].[sp_authenticate_client_with_security_responses] (
  @Username [nvarchar](50),
  @Password_Hash [nvarchar](100),
  @Security_Response_1 [nvarchar](100) = NULL,
  @Security_Response_2 [nvarchar](100) = NULL,
  @Security_Response_3 [nvarchar](100) = NULL
)
AS
BEGIN
  SELECT
    cr.[Client_Role_ID],
    cr.[Role_Description],
    c.[Client_ID],
    c.[Given_Name],
    c.[Family_Name],
    c.[Address],
    c.[City],
    c.[Voice_Number],
    c.[Email]
  FROM [dbo].[Client] c
  LEFT JOIN [dbo].[Client_Client_Role] ccr ON c.[Client_ID] = ccr.[Client_ID]
  JOIN [dbo].[Client_Role] cr ON ccr.[Client_Role_ID] = cr.[Client_Role_ID]
  JOIN [dbo].[Login] l ON c.[Client_ID] = l.[Client_ID]
  WHERE
    l.[Active] = 1
    AND c.[Is_Active] = 1
    AND l.[Username] = @Username
    AND l.[Password_Hash] = @Password_Hash
    AND (l.[Security_Response_1] IS NULL OR l.[Security_Response_1] = @Security_Response_1)
    AND (l.[Security_Response_2] IS NULL OR l.[Security_Response_2] = @Security_Response_2)
    AND (l.[Security_Response_3] IS NULL OR l.[Security_Response_3] = @Security_Response_3);
END;
GO