USE Night_Rider;
GO


/******************
Create sp_select_all_active_part_requests Stored Procedure
***************/
-- Initial Creator: Ben Collins
-- Creation Date: 2024-02-10
-- Last Modified: Ben Collins
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all active Service_Order records
print '' print '*** creating sp_select_all_active_part_requests ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_active_part_requests]
AS 
    BEGIN
        SELECT  [Parts_Request].[Parts_Request_ID],
                [Parts_Request].[Date_Requested],
                [Parts_Request_Line_Items].[Qty_Requested],
                [Parts_Inventory].[Part_Name]
        FROM   [dbo].[Parts_Request_Line_Items]
        INNER JOIN [dbo].[Parts_Request] 
            ON [Parts_Request_Line_Items].[Parts_Request_ID] = [Parts_Request].[Parts_Request_ID]
        INNER JOIN [dbo].[Parts_Inventory] 
            ON [Parts_Request_Line_Items].[Parts_Inventory_ID] = [Parts_Inventory].[Parts_Inventory_ID]
        WHERE [Parts_Request].[Is_Active] = 1
    END;
GO



/***************/
-- Initial Creator: Everett DeVaux
-- Creation Date: 2024-03-02
-- Last Modified: Everett DeVaux
-- Modification Description: Initial Creation
-- Stored Procedure Description: Select all details in a part request
print '' print '*** creating sp_select_all_parts_request_details ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_parts_request_details]
(
    @Parts_Request_Id INT
)
AS
    BEGIN
        SELECT 
            [Parts_Request_Line_Items].[Parts_Request_ID],
            [Parts_Inventory].[Part_Name],
            [Parts_Request_Line_Items].[Qty_Requested],
            [Vehicle_Model].[Year],
            [Vehicle_Model].[Make],
            [Vehicle_Model].[Name],
            [Parts_Request].[Parts_Request_Notes],
            [Parts_Request].[Date_Requested],
            [Parts_Request].[Employee_ID]
        FROM [dbo].[Parts_Request_Line_Items]
        INNER JOIN [dbo].[Parts_Request]
            ON [Parts_Request_Line_Items].[Parts_Request_ID] = [Parts_Request].[Parts_Request_ID]
        INNER JOIN [dbo].[Parts_Inventory] 
            ON [Parts_Request_Line_Items].[Parts_Inventory_ID] = [Parts_Inventory].[Parts_Inventory_ID]
        INNER JOIN [dbo].[Model_Compatibility] 
            ON [Parts_Inventory].[Parts_Inventory_ID] = [Model_Compatibility].[Parts_Inventory_ID]
        INNER JOIN [dbo].[Vehicle_Model] 
            ON [Model_Compatibility].[Vehicle_Model_ID] = [Vehicle_Model].[Vehicle_Model_ID] 
        WHERE @Parts_Request_Id = [Parts_Request_Line_Items].[Parts_Request_ID]
    END
GO

/***************/
-- Initial Creator: Parker Svoboda
-- Creation Date: 2024-03-26
-- Last Modified: Parker Svoboda
-- Modification Description: Initial Creation
-- Stored Procedure Description: deactivates request effectively archiving it after approval
print '' print '*** creating sp_deactivate_request_by_id ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_request_by_id]
(
    @Parts_Request_Id INT
)
AS
    BEGIN
        UPDATE [dbo].[Parts_Request] SET [Is_Active] = 0 WHERE [Parts_Request_ID] = @Parts_Request_Id;    
    END
GO

/***************/
-- Initial Creator: Parker Svoboda
-- Creation Date: 2024-04-2
-- Last Modified: Parker Svoboda
-- Modification Description: Initial Creation
-- Stored Procedure Description: pushes part request into POLineItem table after approval
print '' print '*** creating sp_approve_request ***'
GO
CREATE PROCEDURE [dbo].[sp_approve_request]
(
	@Parts_Request_ID		[INT],
	@Price					[money],
	@LineNumber				[INT],
	@Vendor_ID				[INT]
)
AS
    BEGIN
		DECLARE @Line_Item_Description 	[nvarchar](100);
		DECLARE @Parts_Inventory_ID		[INT];
		DECLARE @Part_Name 				[nvarchar](30);
		DECLARE @Purchase_Order_ID		[INT];
		DECLARE @Quantity_Requested		[INT];
		BEGIN TRANSACTION;
		SAVE TRANSACTION PreTransact;
		BEGIN TRY
			SELECT @Quantity_Requested = pr.[Qty_Requested], @Parts_Inventory_ID = pr.[Parts_Inventory_ID], @Line_Item_Description = iv.[Item_Description], @Part_Name = iv.[Part_Name]
			FROM [dbo].[Parts_Request_Line_Items] pr
			INNER JOIN [dbo].[Parts_Inventory] iv ON iv.[Parts_Inventory_ID] = pr.[Parts_Inventory_ID]
			WHERE pr.[Parts_Request_ID] = @Parts_Request_ID;
			
			UPDATE [dbo].[Parts_Inventory] SET [Ordered_Qty] += @Quantity_Requested WHERE [Parts_Inventory_ID] = @Parts_Inventory_ID;
			
			SET @Price *= @Quantity_Requested;
			
			INSERT INTO [dbo].[Purchase_Order] ([Vendor_ID]) VALUES (@Vendor_ID);
			
			SET @Purchase_Order_ID = IDENT_CURRENT('dbo.Purchase_Order');
			
			 UPDATE [dbo].[Parts_Request] SET [Is_Active] = 0 WHERE [Parts_Request_ID] = @Parts_Request_Id;
			
			INSERT INTO [dbo].[Purchase_Order_Line_Item] ([Purchase_Order_ID], [Parts_Inventory_ID], [Line_Number], [Line_Item_Name], [Line_Item_Qty],[Line_Item_Price], [Line_Item_Description]) VALUES (@Purchase_Order_ID, @Parts_Inventory_ID, @LineNumber, @Part_Name, @Quantity_Requested, @Price, @Line_Item_Description);
			COMMIT TRANSACTION;
		END TRY
		BEGIN CATCH
			IF @@TRANCOUNT > 0
			BEGIN
				ROLLBACK TRANSACTION PreTransact; -- rollback to PreTransact
			END
		END CATCH
		SELECT CAST( IDENT_CURRENT('dbo.Purchase_Order') AS int);
	END
GO

