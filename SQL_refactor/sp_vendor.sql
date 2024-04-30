USE Night_Rider;
GO

/******************
Create sp_select_vendor_by_id Stored Procedure
***************/
 -- AUTHOR: Jonathan Beck
 -- DATE CREATED: 3/3/2024
print '' print '*** creating sp_select_vendor_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_vendor_by_id]
(
    @Vendor_ID [int]
)
AS
    BEGIN
        SELECT [Vendor_ID], [Vendor_Name], 
            [Vendor_Contact_Given_Name], [Vendor_Contact_Family_Name], 
            [Vendor_Contact_Phone_Number], [Vendor_Contact_Email],
            [Vendor_Phone_Number], [Vendor_Address],
            [Vendor_Address2], [Vendor_City],
            [Vendor_State], [Vendor_Country],
            [Vendor_Zip], [Preferred]
        FROM [dbo].[Vendor]
        WHERE [Vendor_ID] = @Vendor_ID 
    END 
GO
 
 /******************
Create sp_select_active_vendors Stored Procedure
***************/
 -- AUTHOR: Jonathan Beck
 -- DATE CREATED: 3/3/2024
print '' print '*** creating sp_select_active_vendors ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_vendors]
AS
    BEGIN
        SELECT [Vendor_ID], [Vendor_Name]
            [Vendor_Contact_Given_Name], [Vendor_Contact_Family_Name],
            [Vendor_Contact_Phone_Number], [Vendor_Contact_Email],
            [Vendor_Phone_Number], [Vendor_Address],
            [Vendor_Address2], [Vendor_City],
            [Vendor_State], [Vendor_Country],
            [Vendor_Zip], [Preferred]
        FROM [dbo].[Vendor]
        WHERE [Is_Active] = 1
    END
GO