USE Night_Rider;
GO
/******************
Create the retreive by key script for the Purchase_Order table
Created By Jonathan Beck : 2/19/2024
***************/
print '' Print '***Create the retreive by key script for the Purchase_Order table***' 
 go 
CREATE PROCEDURE [DBO].[sp_select_purchase_order_by_purchase_order_ID]
(@Purchase_Order_ID [int]
)
as
 Begin 
 select 
[Purchase_Order].[Purchase_Order_ID] 
,[Purchase_Order].[Vendor_ID] 
,[Purchase_Order].[Purchase_Order_Date] 
,[Purchase_Order].[Delivery_Address] 
,[Purchase_Order].[Delivery_Address2] 
,[Purchase_Order].[Delivery_City] 
,[Purchase_Order].[Delivery_State] 
,[Purchase_Order].[Delivery_Country] 
,[Purchase_Order].[Delivery_Zip] 
,[Purchase_Order].[Is_Active] 
,[Vendor].[Vendor_ID]
,[Vendor].[Vendor_Name]
,[Vendor].[Vendor_Contact_Given_Name]
,[Vendor].[Vendor_Contact_Family_Name]
,[Vendor].[Vendor_Contact_Phone_Number]
,[Vendor].[Vendor_Contact_Email]
,[Vendor].[Vendor_Phone_Number]
,[Vendor].[Vendor_Address]
,[Vendor].[Vendor_Address2]
,[Vendor].[Vendor_City]
,[Vendor].[Vendor_State]
,[Vendor].[Vendor_Country]
,[Vendor].[Vendor_Zip]
,[Vendor].[Preferred]
,[Vendor].[Is_Active]


 FROM [Purchase_Order]
 JOIN [VENDOR]
 ON [Purchase_Order].[Vendor_ID] = [Vendor].[Vendor_ID] 
where [Purchase_Order_ID]=@Purchase_Order_ID 
 END 
 GO
 
 /******************
Create the retreive by key script for the Purchase_Order table
Created By Jonathan Beck : 2/19/2024
***************/
print '' Print '***Create the retreive by Purchase_Order_ID script for the Purchase_Order_Line_Item table***' 
 go 
CREATE PROCEDURE [DBO].[sp_select_purchase_orders_by_date_range]
(
@Start_Date [datetime],
@End_Date [datetime]
)
as
 Begin 
 select 
[Purchase_Order].[Purchase_Order_ID] 
,[Purchase_Order].[Vendor_ID] 
,[Purchase_Order].[Purchase_Order_Date] 
,[Purchase_Order].[Delivery_Address] 
,[Purchase_Order].[Delivery_Address2] 
,[Purchase_Order].[Delivery_City] 
,[Purchase_Order].[Delivery_State] 
,[Purchase_Order].[Delivery_Country] 
,[Purchase_Order].[Delivery_Zip] 
,[Purchase_Order].[Is_Active] 
,[Vendor].[Vendor_ID]
,[Vendor].[Vendor_Name]
,[Vendor].[Vendor_Contact_Given_Name]
,[Vendor].[Vendor_Contact_Family_Name]
,[Vendor].[Vendor_Contact_Phone_Number]
,[Vendor].[Vendor_Contact_Email]
,[Vendor].[Vendor_Phone_Number]
,[Vendor].[Vendor_Address]
,[Vendor].[Vendor_Address2]
,[Vendor].[Vendor_City]
,[Vendor].[Vendor_State]
,[Vendor].[Vendor_Country]
,[Vendor].[Vendor_Zip]
,[Vendor].[Preferred]
,[Vendor].[Is_Active]

 FROM Purchase_Order
  JOIN [VENDOR]
 ON [Purchase_Order].[Vendor_ID] = [Vendor].[Vendor_ID] 
where [Purchase_Order_Date]>@Start_Date
and [Purchase_Order_Date]<@End_Date
 END 
 GO