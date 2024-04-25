USE Night_Rider;
GO

/******************
Create sp_select_purchase_order_by_purchase_order_ID Stored Procedure
***************/
-- AUTHOR: Jonathan Beck
-- CREATED: 2/19/2024
print '' print '*** creating sp_select_purchase_order_by_purchase_order_ID ***'
GO 
CREATE PROCEDURE [dbo].[sp_select_purchase_order_by_purchase_order_ID]
(
  @Purchase_Order_ID [int]
)
AS
  BEGIN
    SELECT
      [Purchase_Order].[Purchase_Order_ID], [Purchase_Order].[Vendor_ID], 
      [Purchase_Order].[Purchase_Order_Date], [Purchase_Order].[Delivery_Address], 
      [Purchase_Order].[Delivery_Address2], [Purchase_Order].[Delivery_City], 
      [Purchase_Order].[Delivery_State], [Purchase_Order].[Delivery_Country], 
      [Purchase_Order].[Delivery_Zip], [Purchase_Order].[Is_Active], 
      [Vendor].[Vendor_ID], [Vendor].[Vendor_Name],
      [Vendor].[Vendor_Contact_Given_Name], [Vendor].[Vendor_Contact_Family_Name],
      [Vendor].[Vendor_Contact_Phone_Number], [Vendor].[Vendor_Contact_Email],
      [Vendor].[Vendor_Phone_Number], [Vendor].[Vendor_Address],
      [Vendor].[Vendor_Address2], [Vendor].[Vendor_City],
      [Vendor].[Vendor_State], [Vendor].[Vendor_Country], 
      [Vendor].[Vendor_Zip], [Vendor].[Preferred], 
      [Vendor].[Is_Active]
    FROM [dbo].[Purchase_Order]
    JOIN [dbo].[Vendor]
      ON [Purchase_Order].[Vendor_ID] = [Vendor].[Vendor_ID] 
    WHERE [Purchase_Order_ID] = @Purchase_Order_ID 
  END
GO
 
 /******************
Create sp_select_purchase_orders_by_date_range Stored Procedure
***************/
-- AUTHOR: Jonathan Beck
-- CREATED: 2/19/2024
print '' print '*** creating sp_select_purchase_orders_by_date_range ***'
GO
CREATE PROCEDURE [dbo].[sp_select_purchase_orders_by_date_range]
(
  @Start_Date [datetime],
  @End_Date [datetime]
)
AS
  BEGIN 
    SELECT 
      [Purchase_Order].[Purchase_Order_ID], [Purchase_Order].[Vendor_ID],
      [Purchase_Order].[Purchase_Order_Date], [Purchase_Order].[Delivery_Address],
      [Purchase_Order].[Delivery_Address2], [Purchase_Order].[Delivery_City],
      [Purchase_Order].[Delivery_State], [Purchase_Order].[Delivery_Country],
      [Purchase_Order].[Delivery_Zip], [Purchase_Order].[Is_Active],
      [Vendor].[Vendor_ID], [Vendor].[Vendor_Name],
      [Vendor].[Vendor_Contact_Given_Name], [Vendor].[Vendor_Contact_Family_Name],
      [Vendor].[Vendor_Contact_Phone_Number], [Vendor].[Vendor_Contact_Email],
      [Vendor].[Vendor_Phone_Number], [Vendor].[Vendor_Address],
      [Vendor].[Vendor_Address2], [Vendor].[Vendor_City],
      [Vendor].[Vendor_State], [Vendor].[Vendor_Country],
      [Vendor].[Vendor_Zip], [Vendor].[Preferred],
      [Vendor].[Is_Active]
    FROM [dbo].[Purchase_Order]
    JOIN [dbo].[Vendor]
      ON [Purchase_Order].[Vendor_ID] = [Vendor].[Vendor_ID] 
    WHERE [Purchase_Order_Date] > @Start_Date
      AND [Purchase_Order_Date] < @End_Date
  END 
GO
 
  /******************
Create sp_insert_purchase_order Stored Procedure
***************/
-- AUTHOR: Jonathan Beck
-- CREATED: 3/1/2024
print '' print '*** creating sp_insert_purchase_order ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_purchase_order]
(
  @Vendor_ID [int], 
  @Purchase_Order_Date [date], 
  @Delivery_Address [nvarchar](50), 
  @Delivery_Address2 [nvarchar](50), 
  @Delivery_City [nvarchar](20), 
  @Delivery_State [nvarchar](2), 
  @Delivery_Country [nvarchar](3), 
  @Delivery_Zip [nvarchar](9)
)
AS 
  BEGIN
    INSERT INTO [dbo].[Purchase_Order] (
      [Vendor_ID],[Purchase_Order_Date],
      [Delivery_Address],[Delivery_Address2],
      [Delivery_City],[Delivery_State],
      [Delivery_Country],[Delivery_Zip])
    VALUES (
      @Vendor_ID, @Purchase_Order_Date,
      @Delivery_Address, @Delivery_Address2,
      @Delivery_City, @Delivery_State,
      @Delivery_Country, @Delivery_Zip
    );
    SELECT SCOPE_IDENTITY();
  END
GO