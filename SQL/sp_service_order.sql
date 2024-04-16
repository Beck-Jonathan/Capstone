USE Night_Rider; 
GO

/******************
Create sp_select_all_active_service_orders Stored Procedure
***************/
-- Initial Creator: Ben Collins
-- Creation Date: 2024-02-10
-- Last Modified: Ben Collins
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all active Service_Order records
print '' print '*** creating sp_select_all_active_service_orders ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_active_service_orders]
AS 
    BEGIN
        SELECT  [Service_Order].[VIN],
                [Service_Order].[Service_Order_ID],
                [Service_Order].[Critical_Issue],
                [Service_Type].[Service_Type_ID],
                [Service_Type].[Service_Description]
        FROM   [dbo].[Service_Order]
        INNER JOIN [dbo].[Service_Type] ON [Service_Order].[Service_Type_ID] = [Service_Type].[Service_Type_ID]
        WHERE [Service_Order].[Is_Active] = 1
    END;
GO



/******************
Create sp_autofill_complete_active_service_order_page Stored Procedure
***************/
-- Initial Creator: Ben Collins
-- Creation Date: 2024-02-10
-- Last Modified: Ben Collins
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all active Service_Order records
-- print '' print '*** creating sp_autofill_complete_service_order_page ***'
-- GO
-- CREATE PROCEDURE [dbo].[sp_autofill_complete_service_order_page]
-- AS 
--     BEGIN
--         SELECT  [Parts_Inventory].[Part_Name],
--                 [Parts_Inventory].[Part_Unit_Type],
--                 [Service_Type].[Service_Type_ID],
--                 [Service_Type].[Service_Description],
--                 [Vehicle].[Maintenance_Notes]
--         FROM   [dbo].[Parts_Inventory]
--         INNER JOIN [dbo].[Model_Compatibility] 
--             ON [Parts_Inventory].[Parts_Inventory_ID] = [Model_Compatibility].[Parts_Inventory_ID]
--         INNER JOIN [dbo].[Model_Lookup]
--             ON [Model_Compatibility].[Model_Lookup_ID] = [Model_Lookup].[Model_Lookup_ID]
--         INNER JOIN [Maintenance_Schedule]
--             ON [Model_Lookup].[Model_Lookup_ID] = [Maintenance_Schedule].[Model_Lookup_ID]
--         LEFT OUTER JOIN [Service_Type]
--             ON [Maintenance_Schedule].[Service_Type_ID] = [Service_Type].[Service_Type_ID]
--         INNER JOIN [Vehicle]
--             ON [Model_Lookup].[VIN] = [Vehicle].[VIN]
--     END;
-- GO



/******************
Create sp_select_all_active_service_orders Stored Procedure
***************/
-- Initial Creator: Ben Collins
-- Creation Date: 2024-02-10
-- Last Modified: Ben Collins
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all active Service_Order records
-- print '' print '*** creating sp_complete_active_service_order ***'
-- GO
-- CREATE PROCEDURE [dbo].[sp_complete_active_service_order_page]
-- AS 
--     BEGIN
--         SELECT  [Service_Order].[VIN],
--                 [Service_Order].[Service_Order_ID],
--                 [Service_Order].[Critical_Issue],
--                 [Service_Type].[Service_Type_ID],
--                 [Service_Type].[Service_Description]
--         FROM   [dbo].[Service_Order]
--         INNER JOIN [dbo].[Service_Type] ON [Service_Order].[Service_Type_ID] = [Service_Type].[Service_Type_ID]
--         WHERE [Service_Order].[Is_Active] = 1
--     END
-- GO


/******************
Create sp_update_service_order Stored Procedure
***************/
-- Initial Creator: Steven Sanchez
-- Creation Date: 2024/01/18
-- Last Modified: Steven Sanchez
-- Modification Description: Initial creation.
-- Stored Procedure Description: Update a Service_Order record
PRINT '*** Creating sp_update_service_order ***'
GO

CREATE PROCEDURE sp_update_service_order
    @Service_Order_ID INT,
    @Critical_Issue BIT,
    @New_Service_Type_ID NVARCHAR(256),
    @Service_Description NVARCHAR(256)
AS
BEGIN
    SET NOCOUNT ON;

    -- Update the Service_Order table
    UPDATE Service_Order
    SET 
        Critical_Issue = @Critical_Issue
    WHERE Service_Order_ID = @Service_Order_ID;

    -- Update the Service_Type entry with new Service_Type_ID
    UPDATE Service_Type
    SET 
        Service_Type_ID = @New_Service_Type_ID,
        Service_Description = @Service_Description
    WHERE Service_Type_ID = (SELECT Service_Type_ID FROM Service_Order WHERE Service_Order_ID = @Service_Order_ID);
END

GO

-- Initial Creator: Steven Sanchez
-- Creation Date: 2024/03/12
-- Last Modified: 
-- Modification Description: initial creation
-- Stored Procedure Description: insert a Service_Order record

PRINT '*** Creating sp_insert_service_order_and_type ***'
GO

CREATE PROCEDURE [dbo].[sp_insert_service_order_and_type]
(
    @Service_Order_ID INT,
    @Service_Order_Version INT,
    @VIN NVARCHAR(17),
    @Service_Type_ID NVARCHAR(256),
    @Created_By_Employee_ID INT,
    @Date_Started DATETIME,
    @Date_Finished DATETIME,
    @Service_Description NVARCHAR(256)
)
AS
BEGIN

    -- Check if the Service_Type_ID exists
    IF NOT EXISTS (SELECT 1 FROM dbo.Service_Type WHERE Service_Type_ID = @Service_Type_ID)
    BEGIN
        -- If it doesn't exist, insert it
        INSERT INTO dbo.Service_Type (Service_Type_ID, Service_Description)
        VALUES (@Service_Type_ID, @Service_Description);
    END

    -- Now insert into Service_Order table
    INSERT INTO dbo.Service_Order (Service_Order_ID, Service_Order_Version, VIN, Service_Type_ID, Created_By_Employee_ID, Date_Started , Date_Finished )
    VALUES (@Service_Order_ID, @Service_Order_Version, @VIN, @Service_Type_ID, @Created_By_Employee_ID, @Date_Started, @Date_Finished) ;

    RETURN @@ROWCOUNT;

END
GO

/******************
Create the retreive by key script for the Service_Order table
 Created By Jonathan Beck 4/10/2024
***************/
print '' Print '***Create the retreive by key script for the Service_Order table' 
 go 
CREATE PROCEDURE [DBO].[sp_retreive_by_VIN_Service_Order]
(@VIN [nvarchar](17)
)
as
 Begin 
 select 
[Service_Order_ID] 
,[Service_Order_Version] 
,[VIN] 
,[Service_Line_Item_ID] 
,[Service_Type_ID] 
,[Created_By_Employee_ID] 
,[Serviced_By_Employee_ID] 
,[Date_Started] 
,[Date_Finished] 
,[Is_Active] 
,[Critical_Issue] 

 FROM Service_Order
where [VIN]=@VIN

 END 
 GO