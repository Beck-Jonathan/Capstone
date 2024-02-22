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

print '' print '*** creating sp_select_client_id ***'
GO
-- AUTHOR: Jared Roberts
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

print '' print '*** creating sp_update_client ***'
GO
-- AUTHOR: Jared Roberts
CREATE PROCEDURE [dbo].[sp_update_client]
(
	@Client_ID			[int],		                               
	@OldGivenName		[nvarchar] (50),
	@OldFamilyName		[nvarchar] (50),
	@OldMiddleName		[nvarchar] (50),
	@OldDOB				[date],
	@OldEmail			[nvarchar] (255),
	@OldPostalCode		[nvarchar] (9),
	@OldCity			[nvarchar] (50),
	@OldRegion			[nvarchar] (50),
	@OldAddress			[nvarchar] (100),
	@OldTextNumber		[nvarchar] (12),
	@OldVoiceNumber		[nvarchar] (12),
	@OldActive			[bit],
	@NewGivenName		[nvarchar] (50),					
	@NewFamilyName		[nvarchar] (50),
	@NewMiddleName		[nvarchar] (50),
	@NewDOB				[date],
	@NewEmail			[nvarchar] (255),
	@NewPostalCode		[nvarchar] (9),
	@NewCity			[nvarchar] (50),
	@NewRegion			[nvarchar] (50),
	@NewAddress			[nvarchar] (100),
	@NewTextNumber		[nvarchar] (12),
	@NewVoiceNumber		[nvarchar] (12),
	@NewActive			[bit]
)
AS	
	BEGIN
		UPDATE	[Client]
		SET		[Given_Name]			= @NewGivenName,
				[Family_Name]			= @NewFamilyName,
				[Middle_Name]			= @NewMiddleName,
				[DOB]					= @NewDOB,
				[Email]					= @NewEmail,
				[Postal_Code]			= @NewPostalCode,
				[City]					= @NewCity,
				[Region]				= @NewRegion,
				[Address]				= @NewAddress,
				[Text_Number]			= @NewTextNumber,
				[Voice_Number]			= @NewVoiceNumber,
				[Is_Active]				= @NewActive
		WHERE	[Client_ID]				= @Client_ID
		RETURN	@@ROWCOUNT
	END
GO

print '' print '*** creating sp_insert_client ***'
GO
-- AUTHOR: Isabella Rosenbohm
CREATE PROCEDURE [dbo].[sp_insert_client]
(
	@p_GivenName		[nvarchar] (50),
	@p_FamilyName		[nvarchar] (50),
	@p_MiddleName		[nvarchar] (50),
	@p_DOB				[date],
	@p_Email			[nvarchar] (255),
	@p_PostalCode		[nvarchar] (9),
	@p_City				[nvarchar] (50),
	@p_Region			[nvarchar] (50),
	@p_Address			[nvarchar] (100),
	@p_TextNumber		[nvarchar] (12),
	@p_VoiceNumber		[nvarchar] (12),
	@p_Active			[bit]
)
AS
BEGIN
    DECLARE @p_ClientID int

    INSERT INTO [dbo].[Client] (
        [Given_Name],
        [Family_Name],
		[Middle_Name],
        [DOB],
        [Email],
        [Postal_Code],
        [City],
        [Region],
        [Address],
        [Text_Number],
        [Voice_Number],
        [Is_Active]
    )
    VALUES (
        @p_GivenName,
		@p_FamilyName,
		@p_MiddleName,
		@p_DOB,
		@p_Email,
		@p_PostalCode,
		@p_City,
		@p_Region,
		@p_Address,
		@p_TextNumber,
		@p_VoiceNumber,
		@p_Active
    );

    SET @p_ClientID = SCOPE_IDENTITY();
	
	SELECT @p_ClientID AS 'Client_ID';
END
GO