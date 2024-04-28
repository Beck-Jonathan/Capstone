USE Night_Rider;
GO

/******************
Create sp_select_dependents_by_clientID Stored Procedure
***************/
-- AUTHOR: Michael Springer
print '' print '*** Creating sp_select_dependents_by_clientID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_dependents_by_clientID]
(
    @Client_ID [int]
)
AS
    BEGIN
        SELECT [Dependent_ID], [Dependent].[Given_Name], [Dependent].[Family_Name], [Dependent].[Middle_Name],
                    [Dependent].[DOB], [Dependent].[Gender], [Dependent].[Emergency_Contact],
                    [Dependent].[Contact_Relationship], [Dependent].[Emergency_Phone], [Client_Dependent_Role].[Relationship]
        FROM [dbo].[Client_DependentRole] INNER JOIN [dbo].[Dependent]
            ON [dbo].[Client_Dependent_Role].[Dependent_ID] = [dbo].[Dependent].[Dependent_ID]
        WHERE [Client_Dependent_Role].[Client_ID] = @Client_ID
            AND [Dependent].[Is_Active] = 1
            AND [Client_Dependent_Role].[Is_Active] = 1
    END
GO

/******************
Create sp_insert_client_dependent_role Stored Procedure
***************/
-- AUTHOR: Michael Springer
print '' print '*** Creating sp_insert_client_dependent_role ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_client_dependent_role]
(
    @Client_ID       [int],
    @Dependent_ID    [int],
    @Relationship    [nvarchar](100)
)
AS
    BEGIN
        INSERT INTO [dbo].[Client_Dependent_Role]
            ([Client_ID], [Dependent_ID], [Relationship])
        VALUES
            (@Client_ID, @Dependent_ID, @Relationship)
        SELECT SCOPE_IDENTITY() AS 'Client_Dependent_Role_ID'
    END
GO

/******************
Create sp_select_client_dependent_role_by_client_id Stored Procedure
***************/
-- AUTHOR: Michael Springer
print '' print '*** Creating sp_select_client_dependent_role_by_client_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_client_dependent_role_by_client_id]
(
    @Client_ID [int]
)
AS
    BEGIN
        SELECT [Client_ID], [Dependent_ID], [Relationship], [Is_Active]
        FROM [dbo].[Client_Dependent_Role]
        WHERE [Client_ID] = @Client_ID
            AND [Is_Active] = 1
    END
GO