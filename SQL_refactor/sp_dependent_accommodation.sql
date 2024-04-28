USE Night_Rider;
GO

/******************
Create sp_insert_dependent_accommodation Stored Procedure
***************/
-- AUTHOR: Michael Springer
print '' print '*** Creating sp_insert_dependent_accommodation ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_dependent_accommodation]
(
    @Dependent_ID        [int],
    @Accommodation_ID    [nvarchar](100)
)
AS
    BEGIN
        INSERT INTO [dbo].[Dependent_Accommodation]
            ([Dependent_ID], [Accommodation_ID])
        VALUES
            (@Dependent_ID, @Accommodation_ID)
        SELECT SCOPE_IDENTITY() AS 'Dependent_Accommodation_ID'
    END
GO