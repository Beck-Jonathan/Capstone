USE Night_Rider;
GO
/******************
Create Stored Procedures for the Dependent table
***************/
-- AUTHOR: Michael Springer
print '' print '*** Creating sp_insert_dependent ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_dependent]
(
    @Given_Name			[nvarchar](50),
    @Family_Name        [nvarchar](50),
    @Middle_Name        [nvarchar](100),
    @DOB                [DATE],
    @Gender             [nvarchar](20),
    @Emergency_Contact  [nvarchar](100),
    @Contact_Relationship [nvarchar](100),
    @Emergency_Phone    [nvarchar](12)

)
AS
    BEGIN
        INSERT INTO [dbo].[Dependent]
            ([Given_Name], [Family_Name], [Middle_Name], [DOB], [Gender],
                [Emergency_Contact], [Contact_Relationship], [Emergency_Phone])
        VALUES
            (@Given_Name, @Family_Name, @Middle_Name, @DOB, @Gender,
                @Emergency_Contact, @Contact_Relationship, @Emergency_Phone)
        SELECT SCOPE_IDENTITY() AS 'Dependent_ID'
    END
GO

print '' print '*** Creating sp_select_dependent_by_id ***'
-- AUTHOR: Michael Springer
GO
CREATE PROCEDURE [dbo].[sp_select_dependent_by_id]
(
    @Dependent_ID [int]
)
AS
    BEGIN
        SELECT [Dependent].[Dependent_ID], [Dependent].[Given_Name], [Dependent].[Family_Name], [Dependent].[Middle_Name],
                    [Dependent].[DOB], [Dependent].[Gender], [Dependent].[Emergency_Contact],
                    [Dependent].[Contact_Relationship], [Dependent].[Emergency_Phone],
                    [Client_Dependent_Role].[Relationship]
        FROM [dbo].[Dependent]
        LEFT JOIN [dbo].[Client_Dependent_Role] ON [dbo].[Dependent].[Dependent_ID] = [dbo].[Client_Dependent_Role].[Dependent_ID]
            AND [dbo].[Client_Dependent_Role].[Is_Active] = 1
        WHERE [Dependent].[Dependent_ID] = @Dependent_ID
            AND [Dependent].[Is_Active] = 1
    END
GO

print '' print '*** Creating sp_update_dependent ***'
-- AUTHOR: Michael Springer
GO
CREATE PROCEDURE [dbo].[sp_update_dependent]
(
    @Dependent_ID       [int],
    @Client_ID          [int],
    @Old_Given_Name     [nvarchar](50),
    @Old_Family_Name    [nvarchar](50),
    @Old_Middle_Name    [nvarchar](100),
    @Old_DOB            [DATE],
    @Old_Gender         [nvarchar](20),
    @Old_Emergency_Contact [nvarchar](100),
    @Old_Contact_Relationship [nvarchar](100),
    @Old_Emergency_Phone [nvarchar](12),
    @Old_Client_Relationship [nvarchar](100),
    @New_Given_Name     [nvarchar](50),
    @New_Family_Name    [nvarchar](50),
    @New_Middle_Name    [nvarchar](100),
    @New_DOB            [DATE],
    @New_Gender         [nvarchar](20),
    @New_Emergency_Contact [nvarchar](100),
    @New_Contact_Relationship [nvarchar](100),
    @New_Emergency_Phone [nvarchar](12),
    @New_Client_Relationship [nvarchar](100)
)
AS
    BEGIN
        BEGIN TRANSACTION;
            UPDATE [dbo].[Dependent]
                SET [Given_Name] = @New_Given_Name,
                    [Family_Name] = @New_Family_Name,
                    [Middle_Name] = @New_Middle_Name,
                    [DOB] = @New_DOB,
                    [Gender] = @New_Gender,
                    [Emergency_Contact] = @New_Emergency_Contact,
                    [Contact_Relationship] = @New_Contact_Relationship,
                    [Emergency_Phone] = @New_Emergency_Phone
                WHERE [Dependent_ID] = @Dependent_ID
                    AND [Given_Name] = @Old_Given_Name
                    AND [Family_Name] = @Old_Family_Name
                    AND ([Middle_Name] = @Old_Middle_Name OR(@Old_Middle_Name IS NULL AND [Middle_Name] IS NULL))
                    AND [DOB] = @Old_DOB
                    AND ([Gender] = @Old_Gender OR (@Old_Gender IS NULL AND [Gender] IS NULL))
                    AND [Emergency_Contact] = @Old_Emergency_Contact
                    AND [Contact_Relationship] = @Old_Contact_Relationship
                    AND [Emergency_Phone] = @Old_Emergency_Phone;

            UPDATE [dbo].[Client_Dependent_Role]
                SET [Relationship] = @New_Client_Relationship
                WHERE [Dependent_ID] = @Dependent_ID
                    AND [Client_ID] = @Client_ID
                    AND [Relationship] = @Old_Client_Relationship;
        COMMIT TRANSACTION;
    END
GO


DROP PROCEDURE IF EXISTS sp_select_all_dependents
print '' print '*** creating sp_select_all_dependents***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_dependents]
AS
BEGIN
		SELECT
		[Dependent_ID],	
		[Given_Name],			
		[Family_Name],			
		[Middle_Name],			
		[DOB],		
		[Gender],		
		[Emergency_Contact],		
		[Contact_Relationship], 	
		[Emergency_Phone],		
		[Is_Active]			
		FROM [Dependent] 
		WHERE [is_active] = 1
	END
GO

DROP PROCEDURE IF EXISTS sp_select_dependents_by_client_id
print '' print '*** creating sp_select_dependents_by_client_id***'
GO
CREATE PROCEDURE [dbo].[sp_select_dependents_by_client_id]
(
    @Client_ID [int]
)
AS
    BEGIN
        SELECT
        [Dependent].[Dependent_ID],	
        [Dependent].[Given_Name],			
        [Dependent].[Family_Name],			
        [Dependent].[Middle_Name],			
        [Dependent].[DOB],
        [Dependent].[Gender],
        [Emergency_Contact],
        [Contact_Relationship],
        [Emergency_Phone],
        [Dependent].[Is_Active]
        FROM [Dependent]
        INNER JOIN [Client_Dependent_Role] ON [Dependent].[Dependent_ID] = [Client_Dependent_Role].[Dependent_ID]
        WHERE [Client_Dependent_Role].[Client_ID] = @Client_ID
            AND [Client_Dependent_role].[Is_Active] = 1
            AND [Dependent].[Is_Active] = 1
        ORDER BY [Dependent].[Dependent_ID]
    END
GO