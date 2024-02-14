/******************
Create the retreive by key script for the Parts_Inventory table
***************/

USE Night_Rider;
GO
print '' Print '***Create the retreive by key script for the Parts_Inventory table***' 
 go 
CREATE PROCEDURE [DBO].[sp_select_part_by_part_id]
(@Parts_Inventory_ID [int]
)
as
 Begin 
 select 
[Parts_Inventory_ID] 
,[Part_Name] 
,[Part_Quantity] 
,[Item_Description] 
,[Item_Specifications] 
,[Part_Photo_URL] 
,[Ordered_Qty] 
,[Stock_Level] 
,[Active] 

 FROM [dbo].[Parts_Inventory]
where [Parts_Inventory_ID]=@Parts_Inventory_ID 
 END 
 GO
 
 /******************
Create the retreive by all script for the Parts_Inventory table
***************/
print '' Print '***Create the retreive by all script for the Parts_Inventory table***' 
 go 
CREATE PROCEDURE [DBO].[sp_select_all_part]
AS
begin 
 SELECT 

[Parts_Inventory_ID]
,[Part_Name]
,[Part_Quantity]
,[Item_Description]
,[Item_Specifications]
,[Part_Photo_URL]
,[Ordered_Qty]
,[Stock_Level]
,[Active]
 FROM Parts_Inventory
 ;
 END  
 GO
