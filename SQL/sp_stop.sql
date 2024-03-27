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