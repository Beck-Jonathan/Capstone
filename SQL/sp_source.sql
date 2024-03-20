USE Night_Rider;
GO


/******************
Create the retreive by Vendorid and part number script for the Source table
 Created By Jonathan Beck3/15/2024

***************/
print '' Print '***Create the retreive by key script for the Source table

***' 
 go 
CREATE PROCEDURE [DBO].[sp_select_part_by_vendor_ID_and_part_number]
(@Vendor_Id [int]
,@Parts_inventory_id [int]
)
as
 Begin 
 select 
[Vendor_Id] 
,[Parts_inventory_id] 
,[Vendor_Part_Number] 
,[Estimated_delivery_time_days] 
,[Part_Price] 
,[Minimum_order_Qty] 
,[Active] 

 FROM Source
where [Vendor_Id]=@Vendor_Id 
AND [Parts_inventory_id]=@Parts_inventory_id 
 END 
 GO