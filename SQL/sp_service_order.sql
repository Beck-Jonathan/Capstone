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
print '' print '*** creating sp_select_all_service_orders ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_service_orders]
AS 
    BEGIN
        SELECT  [Service_Order].[VIN],
                [Service_Order].[Service_Order_ID],
                [Service_Order].[Critical_Issue],
                [Service_Type].[Service_Type_ID],
                [Service_Type].[Service_Description]
        FROM   [dbo].[Service_Order]
        INNER JOIN [dbo].[Service_Type] ON [Service_Order].[Service_Type_ID] = [Service_Type].[Service_Type_ID]
    END;
GO


/******************
Create sp_select_service_order_by_service_order_id Stored Procedure
***************/
-- Initial Creator: Ben Collins
-- Creation Date: 2024-02-10
-- Last Modified: Ben Collins
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select a Service_Order record byt the Service Order ID.
print '' print '*** creating sp_select_service_order_by_service_order_id ***'
GO
CREATE PROCEDURE [dbo].[sp_select_service_order_by_service_order_id]
    @Service_Order_ID [int]
AS 
    BEGIN
        SELECT  [dbo].[Service_Order].[Service_Order_ID],
                [dbo].[Service_Order].[Service_Order_Version],
                [dbo].[Service_Order].[VIN],
                [dbo].[Service_Order].[Service_Type_ID],
                [dbo].[Service_Order].[Created_By_Employee_ID],
                [dbo].[Service_Order].[Serviced_By_Employee_ID],
                [dbo].[Service_Order].[Date_Started],
                [dbo].[Service_Order].[Date_Finished],
                [dbo].[Service_Order].[Is_Active],
                [dbo].[Service_Order].[Critical_Issue],
                [dbo].[Service_Type].[Service_Description]
        FROM   [dbo].[Service_Order]
        INNER JOIN [dbo].[Service_Type] ON [Service_Order].[Service_Type_ID] = [Service_Type].[Service_Type_ID]
        WHERE [Service_Order].[Service_Order_ID] = @Service_Order_ID
    END
GO


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
    @Service_Type_ID [nvarchar] (256)
AS
BEGIN
    -- Update the Service_Order table
    UPDATE Service_Order
    SET 
        Critical_Issue = @Critical_Issue,
        Service_Type_ID = @Service_Type_ID
    WHERE Service_Order_ID = @Service_Order_ID;
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
    -- @Date_Finished DATETIME = null,
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
    INSERT INTO dbo.Service_Order (Service_Order_ID, Service_Order_Version, VIN, Service_Type_ID, Created_By_Employee_ID, Date_Started   /* Date_Finished */  )
    VALUES (@Service_Order_ID, @Service_Order_Version, @VIN, @Service_Type_ID, @Created_By_Employee_ID, @Date_Started /*@Date_Finished*/) ;

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

 /******************
Create sp_deactivate_service_order Stored Procedure
***************/
-- Initial Creator: Max Fare
-- Creation Date: 2024-04-01
-- Stored Procedure Description: Updates a service order record to set the Active field as false
PRINT '*** Creating sp_deactivate_service_order ***'
GO

CREATE PROCEDURE sp_deactivate_service_order
(
    @Service_Order_ID   [int],
    @Version            [int]
)
AS
    BEGIN
        UPDATE [dbo].[Service_Order]
        SET [Is_Active] = 0
        WHERE [Service_Order_ID] = @Service_Order_ID
            AND [Service_Order_Version] = @Version
    END
GO

/******************
Create sp_activate_service_order Stored Procedure
***************/
-- Initial Creator: Max Fare
-- Creation Date: 2024-04-01
-- Stored Procedure Description: Updates a service order record to set the Active field as true
PRINT '*** Creating sp_activate_service_order ***'
GO

CREATE PROCEDURE sp_activate_service_order
(
    @Service_Order_ID   [int],
    @Version            [int]
)
AS
    BEGIN
        UPDATE [dbo].[Service_Order]
        SET [Is_Active] = 1
        WHERE [Service_Order_ID] = @Service_Order_ID
            AND [Service_Order_Version] = @Version
    END
GO

 
 -- Initial Creator: Jared Roberts
-- Creation Date: 2024-02-27
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all pending Service_Order records
CREATE PROCEDURE [dbo].[sp_select_incomplete_service_orders]
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

-- Initial Creator: Jared Roberts
-- Creation Date: 2024-02-27
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all completed Service_Order records
CREATE PROCEDURE [dbo].[sp_select_complete_service_orders]
AS 
    BEGIN
        SELECT  [Service_Order].[VIN],
                [Service_Order].[Service_Order_ID],
                [Service_Order].[Critical_Issue],
                [Service_Type].[Service_Type_ID],
                [Service_Type].[Service_Description]
        FROM   [dbo].[Service_Order]
        INNER JOIN [dbo].[Service_Type] ON [Service_Order].[Service_Type_ID] = [Service_Type].[Service_Type_ID]
        WHERE [Service_Order].[Is_Active] = 0
    END;
GO

-- Initial Creator: Steven Sanchez
-- Creation Date: 2024/04/23
-- Last Modified: 
-- Modification Description: initial creation
-- Stored Procedure Description: Select Vehicles by pending Service_Order records

PRINT '*** Creating sp_select_all_vehicles_by_pending_service_orders ***'
GO


CREATE PROCEDURE sp_select_all_vehicles_by_pending_service_orders
AS
BEGIN
    SELECT
        [Service_Order].[VIN] AS VIN_Number,
        [Vehicle_Model].[Vehicle_Model_ID],
        [Vehicle_Model].[Make],
        [Vehicle_Model].[Name],
        [Vehicle].[Vehicle_Type_ID],
        [Vehicle].[Vehicle_Mileage],
        [Service_Order].[Service_Type_ID] AS Service_Type_ID
    FROM
        [dbo].[Service_Order] [Service_Order]
    INNER JOIN
        [dbo].[Vehicle] [Vehicle] ON [Service_Order].[VIN] = [Vehicle].[VIN]
    INNER JOIN
        [dbo].[Vehicle_Model] [Vehicle_Model] ON [Vehicle].[Vehicle_Model_ID] = [Vehicle_Model].[Vehicle_Model_ID]
    WHERE
        [Service_Order].[Date_Finished] IS NULL 
END
GO

-- Initial Creator: Jonathan Beck
-- Creation Date: 2024/04/30
-- Last Modified: 
-- Modification Description: initial creation
-- Stored Procedure Description: Select max id

PRINT '*** Creating sp_select_max_service_order_id ***'
GO


CREATE PROCEDURE sp_select_max_service_order_id
AS
BEGIN
    SELECT
        MAX (Service_Order_ID)
    FROM
        [dbo].[Service_Order]
    
END
GO
