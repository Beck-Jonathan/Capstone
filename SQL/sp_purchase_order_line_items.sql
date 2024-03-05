USE Night_Rider;
GO
/******************
Select by purchase order and line Number for the Purchase_Order_Line_Item
Created By Jonathan Beck : 2/19/2024
***************/
print '' Print '***Create the Select by purchase order and line Number for the Purchase_Order_Line_Item table***' 
 go 
CREATE PROCEDURE [DBO].[sp_select_purchase_order_line_item]
(
@Purchase_Order_ID [int],
@Line_Number [int]
)
AS
begin 
 SELECT 

[Purchase_Order_Line_Item].[Purchase_Order_ID] 
,[Purchase_Order_Line_Item].[Parts_Inventory_ID] 
,[Purchase_Order_Line_Item].[Line_Number] 
,[Purchase_Order_Line_Item].[Line_Item_Name] 
,[Purchase_Order_Line_Item].[Line_Item_Qty] 
,[Purchase_Order_Line_Item].[Line_Item_Price] 
,[Purchase_Order_Line_Item].[Line_Item_Description] 
,[Parts_Inventory].[Parts_Inventory_ID]
,[Parts_Inventory].[Part_Name]
,[Parts_Inventory].[Part_Quantity]
,[Parts_Inventory].[Item_Description]
,[Parts_Inventory].[Item_Specifications]
,[Parts_Inventory].[Part_Photo_URL]
,[Parts_Inventory].[Ordered_Qty]
,[Parts_Inventory].[Stock_Level]
,[Parts_Inventory].[Active]

 FROM [Purchase_Order_Line_Item]
 JOIN [Parts_Inventory]
 ON [Purchase_Order_Line_Item].[Parts_Inventory_ID]  = [Parts_Inventory].[Parts_Inventory_ID] 
 where [Purchase_Order_ID]=@Purchase_Order_ID 
 and [Line_Number]=@Line_Number 
 ;
 END  
 GO
 
/******************
Select by purchase order for the Purchase_Order_Line_Item
Created By Jonathan Beck : 2/19/2024
***************/
print '' Print '***Create the Select by purchase order and for the Purchase_Order_Line_Item table***' 
 go 
CREATE PROCEDURE [DBO].[sp_select_purchase_order_line_item_by_purchase_Order]
(
@Purchase_Order_ID [int]

)
AS
begin 
 SELECT 

[Purchase_Order_Line_Item].[Purchase_Order_ID] 
,[Purchase_Order_Line_Item].[Parts_Inventory_ID] 
,[Purchase_Order_Line_Item].[Line_Number] 
,[Purchase_Order_Line_Item].[Line_Item_Name] 
,[Purchase_Order_Line_Item].[Line_Item_Qty] 
,[Purchase_Order_Line_Item].[Line_Item_Price] 
,[Purchase_Order_Line_Item].[Line_Item_Description] 
,[Parts_Inventory].[Parts_Inventory_ID]
,[Parts_Inventory].[Part_Name]
,[Parts_Inventory].[Part_Quantity]
,[Parts_Inventory].[Item_Description]
,[Parts_Inventory].[Item_Specifications]
,[Parts_Inventory].[Part_Photo_URL]
,[Parts_Inventory].[Ordered_Qty]
,[Parts_Inventory].[Stock_Level]
,[Parts_Inventory].[Active]

 FROM [Purchase_Order_Line_Item]
 JOIN [Parts_Inventory]
 ON [Purchase_Order_Line_Item].[Parts_Inventory_ID]  = [Parts_Inventory].[Parts_Inventory_ID] 
 where [Purchase_Order_ID]=@Purchase_Order_ID 

 ;
 END  
 GO
