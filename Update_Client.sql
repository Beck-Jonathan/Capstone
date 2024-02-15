/******************
Select_Client stored procedures
***************/

USE Night_Rider;
GO


print '' print '*** creating sp_update_client ***'
GO
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