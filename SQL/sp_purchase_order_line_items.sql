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

 /******************
Create the insert script for the Purchase_Order_Line_Item table
Created By Jonathan Beck 3/1/2024
***************/
print '' Print '***Create the insert script for the Purchase_Order_Line_Item table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insert_purchase_order_line_item]
(@Purchase_Order_ID [int]
,@Parts_Inventory_ID [int]
,@Line_Number [int]
,@Line_Item_Name [nvarchar](30)
,@Line_Item_Qty [int]
,@Line_Item_Price [money]
,@Line_Item_Description [nvarchar](100)

)as 
 begin
 insert into [dbo].Purchase_Order_Line_Item(
[Purchase_Order_ID],[Parts_Inventory_ID],[Line_Number],[Line_Item_Name],[Line_Item_Qty],[Line_Item_Price],[Line_Item_Description],[Is_Active])
 VALUES (
@Purchase_Order_ID
,@Parts_Inventory_ID
,@Line_Number
,@Line_Item_Name
,@Line_Item_Qty
,@Line_Item_Price
,@Line_Item_Description
,0

)
return @@rowcount
end
Go