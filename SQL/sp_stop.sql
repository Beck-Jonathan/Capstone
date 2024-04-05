USE Night_Rider;
GO

-- AUTHOR: Chris Baenziger
-- CREATED: 2024-03-23
print ''
print '*** creating sp_get_all_stops ***'
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
    FROM [Stop]
END
GO

-- AUTHOR: Chris Baenziger
-- CREATED: 2024-03-23
print ''
print '*** creating sp_insert_stop ***'
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
Create the update script for the Stop table
 Created By Jonathan Beck 3/17/2023, revised4/2/2024
***************/
print '' Print '***Create the update script for the Stop table
 Created By Jonathan Beck 3/17/2023, revised4/2/2024***' 
 go 
CREATE PROCEDURE [DBO].[sp_update_stop]
(@oldStop_ID[int]
,@oldStreet_Address[nvarchar](255)
,@newStreet_Address[nvarchar](255)
,@oldZip_Code[varchar](5)
,@newZip_Code[varchar](5)
,@oldLatitude[decimal](8,6)
,@newLatitude[decimal](8,6)
,@oldLongitude[decimal](9,6)
,@newLongitude[decimal](9,6)
,@oldIs_Active[bit]
,@newIs_Active[bit]
)
as
BEGIN
UPDATE Stop
SET
Street_Address = @newStreet_Address
,Zip_Code = @newZip_Code
,Latitude = @newLatitude
,Longitude = @newLongitude
,Is_Active = @newIs_Active
WHERE
Stop_ID = @oldStop_ID
and Street_Address = @oldStreet_Address
and Zip_Code = @oldZip_Code
and Latitude = @oldLatitude
and Longitude = @oldLongitude
and Is_Active = @oldIs_Active
return @@rowcount
end
go
