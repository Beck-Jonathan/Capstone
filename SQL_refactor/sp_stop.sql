USE Night_Rider;
GO

/******************
Create sp_select_all_stops Stored Procedure
***************/
-- AUTHOR: Chris Baenziger
-- CREATED: 2024-03-23
print '' print '*** creating sp_get_all_stops ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_stops]
AS
BEGIN
    SELECT
        [Stop_ID],
        [Street_Address],
        [Zip_Code],
        [Latitude],
        [Longitude],
        [Is_Active]
    FROM [dbo].[Stop]
END
GO

/******************
Create sp_insert_stop Stored Procedure
***************/
-- AUTHOR: Chris Baenziger
-- CREATED: 2024-03-23
print '' print '*** creating sp_insert_stop ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_stop]
(
    @Street_Address     [nvarchar](255),
    @Zip_Code           [nvarchar](5),
    @Latitude           [decimal](8,6),
    @Longitude          [decimal](9,6)
)
AS
BEGIN
    DECLARE @stop_num int;
    INSERT INTO [dbo].[Stop]
        ([Street_Address], [Zip_Code], [Latitude], [Longitude])
    VALUES
        (@Street_Address, @Zip_Code, @Latitude, @Longitude)
    SET @stop_num = SCOPE_IDENTITY();
    SELECT @stop_num AS 'Stop_Number';
END
GO

/******************
Create sp_update_stop Stored Procedure
***************/
-- AUTHOR: Jonathan Beck
-- CREATED: 3/17/2023
-- UPDATED: 4/2/2024
-- UPDATER: Jonathan Beck
print '' Print '***creating sp_update_stop***' 
GO
CREATE PROCEDURE [dbo].[sp_update_stop]
(
    @oldStop_ID[int],
    @oldStreet_Address[nvarchar](255),
    @newStreet_Address[nvarchar](255),
    @oldZip_Code[varchar](5),
    @newZip_Code[varchar](5),
    @oldLatitude[decimal](8,6),
    @newLatitude[decimal](8,6),
    @oldLongitude[decimal](9,6),
    @newLongitude[decimal](9,6),
    @oldIs_Active[bit],
    @newIs_Active[bit]
)
AS
    BEGIN
        UPDATE [dbo].[Stop]
        SET [Street_Address] = @newStreet_Address,
            [Zip_Code] = @newZip_Code,
            [Latitude] = @newLatitude,
            [Longitude] = @newLongitude,
            [Is_Active] = @newIs_Active
        WHERE Stop_ID = @oldStop_ID
            AND Street_Address = @oldStreet_Address
            AND Zip_Code = @oldZip_Code
            AND Latitude = @oldLatitude
            AND Longitude = @oldLongitude
            AND Is_Active = @oldIs_Active
        RETURN @@ROWCOUNT
    END
GO