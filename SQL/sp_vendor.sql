USE Night_Rider;
/******************
Create the retreive by ID script for the Vendor table
Jonathan Beck, 3/3
***************/
print '' Print '***Create the retreive by key script for the Vendor table***' 
 go 
CREATE PROCEDURE [DBO].[sp_select_vendor_by_id]
(@Vendor_ID [int]
)
as
 Begin 
 select 
[Vendor_ID] 
,[Vendor_Name] 
,[Vendor_Contact_Given_Name] 
,[Vendor_Contact_Family_Name] 
,[Vendor_Contact_Phone_Number] 
,[Vendor_Contact_Email] 
,[Vendor_Phone_Number] 
,[Vendor_Address] 
,[Vendor_Address2] 
,[Vendor_City] 
,[Vendor_State] 
,[Vendor_Country] 
,[Vendor_Zip] 
,[Preferred] 


 FROM Vendor
where [Vendor_ID]=@Vendor_ID 
 END 
 GO
 
 /******************
Create the retreive by all script for the Vendor table
Jonathan Beck 3-3
***************/
print '' Print '***Create the retreive by all script for the Vendor table***' 
 go 
CREATE PROCEDURE [DBO].[sp_select_active_vendors]
AS
begin 
 SELECT 

[Vendor_ID]
,[Vendor_Name]
,[Vendor_Contact_Given_Name]
,[Vendor_Contact_Family_Name]
,[Vendor_Contact_Phone_Number]
,[Vendor_Contact_Email]
,[Vendor_Phone_Number]
,[Vendor_Address]
,[Vendor_Address2]
,[Vendor_City]
,[Vendor_State]
,[Vendor_Country]
,[Vendor_Zip]
,[Preferred]

 FROM Vendor
 Where [Is_Active]=1
 ;
 END  
 GO


GO