DROP DATABASE IF EXISTS Night_Rider;
GO
CREATE DATABASE Night_Rider;
GO
USE Night_Rider;
GO

/******************
Create the [dbo].[Zipcode] table
***************/
print ''
Print '***Create the [dbo].[Zipcode] table***' 
GO
CREATE TABLE [dbo].[Zipcode]
(
    [Zip_Code] [nvarchar](15) NOT NULL,
    [City] [nvarchar](50) NOT NULL,
    [State] [nvarchar](25) NOT NULL,
    CONSTRAINT 	[pk_Zipcode] PRIMARY KEY ([Zip_Code])
)
GO

/******************
Create the [dbo].[Vendor] table
***************/
print ''
Print '***Create the [dbo].[Vendor] table***' 
GO
CREATE TABLE [dbo].[Vendor]
(
    [Vendor_ID] [int] IDENTITY	(100000,1) NOT NULL,
    /*Vendor name*/
    [Vendor_Name] [nvarchar](100) NOT NULL,
    /*The name of the vendors company*/
    [Vendor_Contact_Given_Name] [nvarchar](50) NOT NULL,
    /*vendor contact person given name*/
    [Vendor_Contact_Family_Name] [nvarchar](50) NOT NULL,
    /*vendor contact person family name*/
    [Vendor_Contact_Phone_Number] [nvarchar](12) NOT NULL,
    /*vendor contact phone number*/
    [Vendor_Contact_Email] [nvarchar](255) NOT NULL,
    /*vendor contact email*/
    [Vendor_Phone_Number] [nvarchar](12) NOT NULL,
    /*vendor main phone number*/
    [Vendor_Address] [nvarchar](50) NOT NULL,
    /*vendor address*/
    [Vendor_Address2] [nvarchar](50) NULL,
    /*vendor address detail*/
    [Vendor_City] [nvarchar](20) NOT NULL,
    /*vendor city*/
    [Vendor_State] [nvarchar](2) NOT NULL,
    /*vendor state*/
    [Vendor_Country] [nvarchar](3) NOT NULL,
    /*vendor coutry*/
    [Vendor_Zip] [nvarchar](9) NOT NULL,
    /*vendor zip*/
    [Preferred] [bit] DEFAULT 0 NOT NULL,
    /*is the vendor preferred for ordering from*/
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    /*is the vendor currently in use*/
    CONSTRAINT [pk_Vendor] PRIMARY KEY ([Vendor_ID])
)
GO

/******************
Create the [dbo].[Purchase_Order] table
***************/
print ''
Print '***Create the [dbo].[Purchase_Order] table***' 
GO
CREATE TABLE [dbo].[Purchase_Order]
(
    [Purchase_Order_ID] [int] IDENTITY(100000, 1) NOT NULL,
    [Vendor_ID] [int] NOT NULL,
    [Purchase_Order_Date] [date] DEFAULT GETDATE(),
    [Delivery_Address] [nvarchar](50) NULL,
    [Delivery_Address2] [nvarchar](50) NULL,
    [Delivery_City] [nvarchar](20) NULL,
    [Delivery_State] [nvarchar](2) NULL,
    [Delivery_Country] [nvarchar](3) NULL,
    [Delivery_Zip] [nvarchar](9) NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Purchase_Order] PRIMARY KEY ([Purchase_Order_ID]),
    CONSTRAINT [fk_Purchase_Order_Vendor_ID] FOREIGN KEY ([Vendor_ID])
		REFERENCES [dbo].[Vendor]([Vendor_Id])
)
GO

/******************
Create the [dbo].[Packing_Slip] table
***************/
print ''
Print '***Create the [dbo].[Packing_Slip] table***' 
GO
CREATE TABLE [dbo].[Packing_Slip]
(
    [Packing_Slip_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Purchase_Order_ID] [int] NOT NULL,
    [Recieving_Notes] [nvarchar](256) NULL,
    [Vendor_ID] [int] NOT NULL,
    [Creation_Date] [date] DEFAULT GETDATE() NOT NULL,
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [pk_Packing_Slip] PRIMARY KEY ([Packing_Slip_ID]),
    CONSTRAINT [fk_Packing_Slip_Purchase_Order_ID] FOREIGN KEY ([Purchase_Order_ID])
    	REFERENCES [dbo].[Purchase_Order] ([Purchase_Order_ID]),
    CONSTRAINT [fk_Packing_Slip_Vendor_ID] FOREIGN KEY ([Vendor_ID])
    	REFERENCES [dbo].[Vendor] ([Vendor_ID])
)
GO

/******************
Create the [dbo].[Parts_Inventory] table
***************/
print ''
Print '***Create the [dbo].[Parts_Inventory] table***' 
GO
CREATE TABLE [dbo].[Parts_Inventory]
(
    [Parts_Inventory_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Part_Name] [nvarchar] (30) NOT NULL,
    [Part_Quantity] [int] NOT NULL DEFAULT 0,
    [Item_Description] [nvarchar] (100) NOT NULL,
    [Item_Specifications] [nvarchar] (MAX) NOT NULL,
    [Part_Photo_URL] [nvarchar] (255) NOT NULL,
    [Ordered_Qty] [int] NOT NULL DEFAULT 0,
    [Stock_Level] [int] NOT NULL DEFAULT 0,
    [Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Parts_Inventory] PRIMARY KEY([Parts_Inventory_ID])
)
GO

/******************
Create the [dbo].[Purchase_Order_Line_Item] table
***************/
print ''
Print '***Create the [dbo].[Purchase_Order_Line_Item] table***' 
GO
CREATE TABLE [dbo].[Purchase_Order_Line_Item]
(
    [Purchase_Order_ID] [int],
    [Parts_Inventory_ID] [int],
    [Line_Number] [int] NOT NULL,
    [Line_Item_Name] [nvarchar](30) NULL,
    [Line_Item_Qty] [int] NOT NULL,
    [Line_Item_Price][money] NOT NULL,
    [Line_Item_Description] [nvarchar](100) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT(1),
    CONSTRAINT [pk_Purchase_Order_Line_Item] PRIMARY KEY([Purchase_Order_ID], [Parts_Inventory_ID]),
    CONSTRAINT [fk_Purchase_Order_Line_Item_Parts_Inventory_ID] FOREIGN KEY([Parts_Inventory_ID]) 
		REFERENCES [dbo].[Parts_Inventory]([Parts_Inventory_ID]),
    CONSTRAINT [fk_Purchase_Order_Line_Item_Purchase_Order_ID] FOREIGN KEY([Purchase_Order_ID]) 
		REFERENCES [dbo].[Purchase_Order]([Purchase_Order_ID])
)
GO

/******************
Create the [dbo].[Route] table
***************/
print ''
Print '***Create the [dbo].[Route] table***' 
GO
CREATE TABLE [dbo].[Route]
(
    [Route_ID] [int] IDENTITY(100000,1),
    [Route_Name] [nvarchar](255) NOT NULL,
    [Route_Start_Time] [datetime] NOT NULL,
    [Route_Cycle] [time] NOT NULL,
    [Route_End_Time] [datetime] NOT NULL,
    [Days_Of_Service] [char](7) NOT NULL DEFAULT '0000000',
    [Is_Active] [bit] NOT NULL DEFAULT(1),
    CONSTRAINT [pk_Route] PRIMARY KEY([Route_ID])
)
GO

/******************
Create the [dbo].[Stop] table
***************/
print ''
Print '***Create the [dbo].[Stop] table***' 
GO
CREATE TABLE [dbo].[Stop]
(
    [Stop_ID] [int] NOT NULL IDENTITY(100000, 1),
    [Street_Address] [nvarchar](255) NOT NULL,
    [Zip_Code] [nvarchar](5) NOT NULL,
    [Latitude] [decimal](8,6) NOT NULL,
    [Longitude] [decimal](9,6) NOT NULL,
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [pk_Stop] PRIMARY KEY([Stop_ID])
)
GO

/******************
Create the [dbo].[Route_Stop] table
***************/
print ''
Print '***Create the [dbo].[Route_Stop] table***' 
GO
CREATE TABLE [dbo].[Route_Stop]
(
    [Route_ID] [int] NOT NULL,
    [Stop_ID] [int] NOT NULL,
    [Route_Stop_Number] [int] NOT NULL,
    [Start_Offset] [time] NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT(1),
    CONSTRAINT [pk_Route_Stop] PRIMARY KEY ([Route_ID], [Route_Stop_Number]),
    CONSTRAINT [fk_Route_Stop_Route_ID] FOREIGN KEY ([Route_ID]) 
		REFERENCES [dbo].[Route]([Route_ID]),
    CONSTRAINT [fk_Route_Stop_Stop_ID] FOREIGN KEY ([Stop_ID])
		REFERENCES [dbo].[Stop]([Stop_ID])
)
GO

/******************
Create the [dbo].[Vehicle_Type] table
***************/
print ''
Print '***Create the [dbo].[Vehicle_Type] table***' 
GO
CREATE TABLE [dbo].[Vehicle_Type]
(
    [Vehicle_Type_ID] [nvarchar](50) NOT NULL,
    /*a type of vehicle, like bus or van*/
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [pk_Vehicle_Type] PRIMARY KEY([Vehicle_Type_ID])
)
GO

/******************
Create the [dbo].[Vehicle_Model] table
***************/
print ''
Print '***Create the [dbo].[Vehicle_Model] table***' 
GO
CREATE TABLE [dbo].[Vehicle_Model]
(
    [Vehicle_Model_ID] [int] IDENTITY(100000, 1) NOT NULL,
    [Vehicle_Type_ID] [nvarchar] (50) NULL,
    [Max_Passengers] [int] NOT NULL,
    [Make] [nvarchar] (255) NOT NULL,
    [Name] [nvarchar] (255) NOT NULL,
    [Year] [int] NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Vehicle_Model] PRIMARY KEY([Vehicle_Model_ID]),
    CONSTRAINT [fk_Vehicle_Model_Vehicle_Type_ID] FOREIGN KEY([Vehicle_Type_ID]) 
		REFERENCES [dbo].[Vehicle_Type]([Vehicle_Type_ID])
)
GO

/******************
Create the [dbo].[Vehicle] table
***************/
print ''
Print '***Create the [dbo].[Vehicle] table***' 
GO
CREATE TABLE [dbo].[Vehicle]
(
    [VIN] [nvarchar](17) NOT NULL,
    [Vehicle_Number] [nvarchar](10) NOT NULL,
    [Vehicle_Mileage] [int] NOT NULL,
    [Vehicle_License_Plate] [nvarchar](10) NOT NULL,
    [Vehicle_Model_ID] [int] NOT NULL,
    [Vehicle_Type_ID] [nvarchar](50) NOT NULL,
    [Date_Entered] [date] NOT NULL,
    [Description] [nvarchar](256) NOT NULL,
    [Rental] [bit] NOT NULL DEFAULT 0,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Vehicle] PRIMARY KEY ([VIN]),
    CONSTRAINT [ak_Vehicle_Number] UNIQUE ([Vehicle_Number]),
    CONSTRAINT [fk_Vehicle_Vehicle_Type_ID] FOREIGN KEY ([Vehicle_Type_ID]) 
        REFERENCES [dbo].[Vehicle_Type]([Vehicle_Type_ID]),
    CONSTRAINT [fk_Vehicle_Vehicle_Model_ID] FOREIGN KEY ([Vehicle_Model_ID])
        REFERENCES [dbo].[Vehicle_Model]([Vehicle_Model_ID])
)
GO

/******************
Create the [dbo].[Employee] table
***************/
print ''
Print '***Create the [dbo].[Employee] table***' 
GO
CREATE TABLE [dbo].[Employee]
(
    [Employee_ID] [int] IDENTITY(100000, 1) NOT NULL,
    [Given_Name] [nvarchar](50) NOT NULL,
    [Family_Name] [nvarchar](50) NOT NULL,
    [DOB] [datetime] NOT NULL,
    [Address] [nvarchar](50) NOT NULL,
    [Address2] [nvarchar](50) NULL,
    [City] [nvarchar](20) NOT NULL,
    [State] [nvarchar](2) NOT NULL,
    [Country] [nvarchar](3) NOT NULL,
    [Zip] [nvarchar](9) NOT NULL,
    [Phone_Number] [nvarchar](20) NOT NULL,
    [Email] [nvarchar](50) NOT NULL,
    [Position] [nvarchar](20) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Employee] PRIMARY KEY([Employee_ID])
)
GO

/******************
Create the [dbo].[Role] table
***************/
print ''
Print '***Create the [dbo].[Role] table***' 
GO
CREATE TABLE [dbo].[Role]
(
    [Role_ID] [nvarchar](25) NOT NULL,
    [Role_Description] [nvarchar](255) NOT NULL DEFAULT '',
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT 	[pk_Role] PRIMARY KEY ([Role_ID])
)
GO

/******************
Create the [dbo].[Employee_Role] table
***************/
print ''
Print '***Create the [dbo].[Employee_Role] table***' 
GO
CREATE TABLE [dbo].[Employee_Role]
(
    [Employee_ID] [int] NOT NULL,
    [Role_ID] [nvarchar](25) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Employee_Role] PRIMARY KEY([Employee_ID], [Role_ID]),
    CONSTRAINT [fk_Employee_Role_Employee_ID] FOREIGN KEY([Employee_ID])
		REFERENCES [dbo].[Employee]([Employee_ID]),
    CONSTRAINT [fk_Employee_Role_Role_ID] FOREIGN KEY([Role_ID])
		REFERENCES [dbo].[Role]([Role_ID])
)
GO

/******************
Create the [dbo].[Driver_License_Class] table
***************/
print ''
Print '***Create the [dbo].[Driver_License_Class] table***' 
GO
CREATE TABLE [dbo].[Driver_License_Class]
(
    [Driver_License_Class_ID] [nvarchar](6) NOT NULL DEFAULT 'c',
    [Max_Passenger_Count] [int] NOT NULL DEFAULT 0,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Driver_License_Class] PRIMARY KEY ([Driver_License_Class_ID])
)
GO

/******************
Create the [dbo].[Driver] table
***************/
print ''
Print '***Create the [dbo].[Driver] table***' 
GO
CREATE TABLE [dbo].[Driver]
(
    [Employee_ID] [int] NOT NULL,
    [Driver_License_Class_ID] [nvarchar](6) NOT NULL DEFAULT 'c',
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Driver] PRIMARY KEY ([Employee_ID]),
    CONSTRAINT [fk_Driver_Employee_ID] FOREIGN KEY([Employee_ID])
		REFERENCES [dbo].[Employee] ([Employee_ID]),
    CONSTRAINT [fk_Driver_Driver_License_Class_ID] FOREIGN KEY([Driver_License_Class_ID])
		REFERENCES [dbo].[Driver_License_Class] ([Driver_License_Class_ID])
)
GO

/******************
Create the [dbo].[Schedule] table
***************/
print ''
Print '***Create the [dbo].[Schedule] table***' 
GO
CREATE TABLE [dbo].[Schedule]
(
    [Schedule_ID] [nvarchar](50) NOT NULL,
    [Driver_ID] [int] NOT NULL,
    [Week_Days] [char](7) NOT NULL DEFAULT '0000000' ,
    [Start_Time] [time] NOT NULL,
    [End_Time] [time] NOT NULL,
    [Start_Date] [date] NOT NULL,
    [End_Date] [date] NULL,
    [Notes] [nvarchar](255) NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Schedule] PRIMARY KEY ([Schedule_ID]),
    CONSTRAINT [fk_Schedule_Driver_ID] FOREIGN KEY ([Driver_ID])
        REFERENCES [dbo].[Driver]([Employee_ID])
)
GO

/******************
Create the [dbo].[Route_Assignment] table
***************/
print ''
Print '***Create the [dbo].[Route_Assignment] table***' 
GO
CREATE TABLE [dbo].[Route_Assignment]
(
    [Assignment_ID] [int] IDENTITY(100000, 1),
    [Driver_ID] [int] NOT NULL,
    [Route_ID] [int] NOT NULL,
    [VIN] [nvarchar](17) NOT NULL,
    [Date_Assignment_Started] [datetime] NOT NULL,
    [Date_Assignment_Ended] [datetime] NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Route_Assignment] PRIMARY KEY([Assignment_ID]),
    CONSTRAINT [fk_Route_Assignment_Driver_ID] FOREIGN KEY([Driver_ID]) 
        REFERENCES [dbo].[Driver]([Employee_ID]),
    CONSTRAINT [fk_Route_Assignment_Route_ID] FOREIGN KEY([Route_ID]) 
        REFERENCES [dbo].[Route]([Route_ID]),
    CONSTRAINT [fk_Route_Assignment_VIN] FOREIGN KEY([VIN]) 
        REFERENCES [dbo].[Vehicle]([VIN])
)
GO

/******************
Create the [dbo].[Route_Fulfillment] table
***************/
print ''
Print '***Create the [dbo].[Route_Fulfillment] table***' 
GO
CREATE TABLE [dbo].[Route_Fulfillment]
(
    [Assignment_ID] [int] NOT NULL,
    [Actual_Driver_ID] [int] NOT NULL,
    [Actual_VIN] [nvarchar](17) NOT NULL,
    [Start_Time] [datetime] NOT NULL,
    [End_Time] [datetime] NULL,
    CONSTRAINT [pk_Route_Fulfillment] PRIMARY KEY([Assignment_ID], [Actual_Driver_ID], [Actual_VIN]),
    CONSTRAINT [fk_Route_Fulfillment_Assigment_ID] FOREIGN KEY([Assignment_ID])
        REFERENCES [dbo].[Route_Assignment]([Assignment_ID]),
    CONSTRAINT [fk_Route_Fulfillment_Driver_ID] FOREIGN KEY([Actual_Driver_ID]) 
        REFERENCES [dbo].[Driver]([Employee_ID]),
    CONSTRAINT [fk_Route_Fulfillment_VIN] FOREIGN KEY([Actual_VIN]) 
        REFERENCES [dbo].[Vehicle]([VIN])
)
GO

/******************
Create the [dbo].[Safety_Report] table
***************/
print ''
Print '***Create the [dbo].[Safety_Report] table***' 
GO
CREATE TABLE [dbo].[Safety_Report]
(
    [Safety_Report_ID] [int] IDENTITY (100000,1) NOT NULL,
    /*report identifier*/
    [Employee_ID] [int] NOT NULL,
    /*Employee.Employee_ID	id of employee filing report*/
    [Date] [datetime] NOT NULL,
    /*date when report is filed*/
    [Time_Of_Event] [datetime] NOT NULL,
    /*time of the event*/
    [Affected_Party] [nvarchar](100) NOT NULL,
    /*yourself, coworker, passenger, civilian*/
    [Description] [nvarchar](1000) NULL,
    /*description of the event*/
    [Result_In_Injury] [bit] DEFAULT 0 NOT NULL,
    /*did this event result in an injury?*/
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    /*is the report still active?*/
    CONSTRAINT [pk_Safety_Report] PRIMARY KEY ([Safety_Report_ID]),
    CONSTRAINT [fk_Safety_Report_Employee_ID] FOREIGN KEY([Employee_ID]) 
        REFERENCES [dbo].[Employee]([Employee_ID])
)
GO

/******************
Create the [dbo].[Refuel_Log] table
***************/
print ''
Print '***Create the [dbo].[Refuel_Log] table***' 
GO
CREATE TABLE [dbo].[Refuel_Log]
(
    [Refuel_Log_ID] [int] IDENTITY(100000,1) NOT NULL,
    /*generated id for refuel*/
    [Driver_ID] [int] NULL,
    /*Driver.Employee_ID driver that purchased the fuel*/
    [VIN] [nvarchar](17) NOT NULL,
    /*Vehicle.VIN vehicle the fuel was put in*/
    [Date_Time] [datetime] DEFAULT GETDATE() NOT NULL,
    /*auto entry as datetime.now date fuel was purchased*/
    [Mileage] [int] NOT NULL,
    /*mileage of the vehicle when fueled*/
    [Fuel_Quantity] [int] NOT NULL,
    /*amount of fuel purchased*/
    [Fuel_Price_Per_Gal] [smallmoney] NOT NULL,
    /*price of the fuel*/
    [Total_Sale] [smallmoney] NOT NULL,
    /*sale amount for the fuel*/
    [Notes] [nvarchar](250) NULL,
    /*notes from the driver*/
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    /*Active status of this refuel log*/
    CONSTRAINT [pk_Refuel_Log] PRIMARY KEY ([Refuel_Log_ID]),
    CONSTRAINT [fk_Refuel_Log_Driver_ID] FOREIGN KEY([Driver_ID])
        REFERENCES [dbo].[Employee]([Employee_ID]),
    CONSTRAINT [fk_Refuel_Log_VIN] FOREIGN KEY([VIN])
        REFERENCES [dbo].[Vehicle]([VIN])
)
GO

/******************
Create the [dbo].[Service_Type] table
***************/
print ''
Print '***Create the [dbo].[Service_Type] table***' 
GO
CREATE TABLE [dbo].[Service_Type]
(
    [Service_Type_ID] [nvarchar] (256) NOT NULL,
    [Service_Description] [nvarchar] (256) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Service_Type] PRIMARY KEY([Service_Type_ID])
)
GO

/******************
Create the [dbo].[Maintenance_Schedule] table
***************/
print ''
Print '***Create the [dbo].[Maintenance_Schedule] table***' 
GO
CREATE TABLE [dbo].[Maintenance_Schedule]
(
    [Maintenance_Schedule_ID] [int] NOT NULL,
    [Vehicle_Model_ID] [int] NOT NULL,
    [Service_Type_ID] [nvarchar] (256) NOT NULL,
    [Frequency_In_Months] [int] NOT NULL,
    [Frequency_In_Miles] [int] NULL,
    [Is_Completed] [bit] NOT NULL,
    [Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Maintenance_Schedule] PRIMARY KEY([Maintenance_Schedule_ID]),
    CONSTRAINT [fk_Maintenance_Schedule_Vehicle_Model_ID] FOREIGN KEY([Vehicle_Model_ID])
    	REFERENCES [dbo].[Vehicle_Model]([Vehicle_Model_ID]),
    CONSTRAINT [fk_Maintenance_Schedule_Service_Type_ID] FOREIGN KEY ([Service_Type_ID])
        REFERENCES [dbo].[Service_Type]([Service_Type_ID]) ON UPDATE CASCADE
)
GO

/******************
Create the [dbo].[Service_Line_Item] table
***************/
print ''
Print '***Create the [dbo].[Service_Line_Item] table***' 
GO
CREATE TABLE [dbo].[Service_Line_Item]
(
    [Service_Line_Item_ID] [int] NOT NULL,
    [Parts_Inventory_ID] [int] NOT NULL,
    [Quantity] [int] NOT NULL,
    CONSTRAINT [pk_Service_Line_Item] PRIMARY KEY([Service_Line_Item_ID]),
    CONSTRAINT [fk_Service_Line_Item_Parts_Inventory_ID] FOREIGN KEY([Parts_Inventory_ID])
        REFERENCES [dbo].[Parts_Inventory] ([Parts_Inventory_ID])
)
GO

/******************
Create the [dbo].[Service_Order] table
***************/
print ''
Print '***Create the [dbo].[Service_Order] table***' 
GO
CREATE TABLE [dbo].[Service_Order]
(
    [Service_Order_ID] [int] NOT NULL,
    [Service_Order_Version] [int] NOT NULL,
    [VIN] [nvarchar] (17) NOT NULL,
    [Service_Line_Item_ID] [int] NULL,
    [Service_Type_ID] [nvarchar] (256) NOT NULL,
    [Created_By_Employee_ID] [int] NOT NULL,
    [Serviced_By_Employee_ID] [int] NULL,
    [Date_Started] [datetime] NOT NULL,
    [Date_Finished] [datetime] NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    [Critical_Issue] [bit] NOT NULL DEFAULT 0,
    CONSTRAINT [pk_Service_Order] PRIMARY KEY([Service_Order_ID], [Service_Order_Version]),
    CONSTRAINT [fk_Service_Order_VIN] FOREIGN KEY([VIN])
        REFERENCES [dbo].[Vehicle] ([VIN]),
    CONSTRAINT [fk_Service_Order_Service_Line_Item_ID] FOREIGN KEY([Service_Line_Item_ID])
        REFERENCES [dbo].[Service_Line_Item] ([Service_Line_Item_ID]),
    CONSTRAINT [fk_Service_Order_Service_Type_ID] FOREIGN KEY([Service_Type_ID])
        REFERENCES [dbo].[Service_Type] ([Service_Type_ID]) ON UPDATE CASCADE,
    CONSTRAINT [fk_Service_Order_Created_By_Employee_ID] FOREIGN KEY([Created_By_Employee_ID])
        REFERENCES [dbo].[Employee] ([Employee_ID]),
    CONSTRAINT [fk_Service_Order_Serviced_By_Employee_ID] FOREIGN KEY([Serviced_By_Employee_ID])
        REFERENCES [dbo].[Employee] ([Employee_ID])
)
GO

/******************
Create the [dbo].[Service_Line] table
***************/
print ''
Print '***Create the [dbo].[Service_Line] table***' 
GO
CREATE TABLE [dbo].[Service_Line]
(
    [Service_Line_ID] [int] NOT NULL,
    [Service_Line_Item_ID] [int] NOT NULL,
    CONSTRAINT [pk_Service_Line] PRIMARY KEY ([Service_Line_ID],[Service_Line_Item_ID]),
    /* CONSTRAINT [fk_Service_Line_Service_Line_ID] FOREIGN KEY ([Service_Line_ID]) 
        REFERENCES [dbo].[Service_Order]([Service_Order_ID]) */
    CONSTRAINT [fk_Service_Line_Service_Line_Item_ID] FOREIGN KEY ([Service_Line_Item_ID]) 
        REFERENCES [dbo].[Service_Line_Item]([Service_Line_Item_ID])
)
GO

/******************
Create the [dbo].[Special_Service_Order] table
***************/
print ''
Print '***Create the [dbo].[Special_Service_Order] table***' 
GO
CREATE TABLE [dbo].[Special_Service_Order]
(
    [Special_Service_Order_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Service_Order_ID] [int] NOT NULL,
    [Service_Order_Version] [int] NOT NULL,
    [Event_Description] [nvarchar](500) NOT NULL,
    [Priority] [nvarchar](500) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Special_Service_Order] PRIMARY KEY ([Special_Service_Order_ID]),
    CONSTRAINT [fk_Special_Service_Order_Service_Order_ID_Service_Order_Version] 
        FOREIGN KEY ([Service_Order_ID], [Service_Order_Version])
        REFERENCES [dbo].[Service_Order]([Service_Order_ID], [Service_Order_Version])
)
GO

/******************
Create the [dbo].[Special_Inspection] table
***************/
print ''
Print '***Create the [dbo].[Special_Inspection] table***' 
GO
CREATE TABLE [dbo].[Special_Inspection]
(
    [Special_Inspection_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Special_Service_Order_ID] [int] NOT NULL,
    [Inspection_Description] [nvarchar](500) NOT NULL,
    [Date] [datetime] NOT NULL,
    [Employee_ID] [int] NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Special_Inspection] PRIMARY KEY ([Special_Inspection_ID]),
    CONSTRAINT [fk_Special_Inspection_Special_Service_Order_ID] FOREIGN KEY ([Special_Service_Order_ID])
        REFERENCES [dbo].[Special_Service_Order]([Special_Service_Order_ID]),
    CONSTRAINT [fk_Special_Inspection_Employee_ID] FOREIGN KEY ([Employee_ID]) 
        REFERENCES [dbo].[Employee]([Employee_ID])
)
GO

/******************
Create the [dbo].[Bid] table
***************/
print ''
Print '***Create the [dbo].[Bid] table***' 
GO
CREATE TABLE [dbo].[Bid]
(
    [Bid_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Special_Service_Order_ID] [int] NOT NULL,
    [Vendor_ID] [int] NOT NULL,
    [Bid_Description] [nvarchar](500) NOT NULL,
    [Date] [datetime] NOT NULL,
    [Amount] [money] NOT NULL,
    [Is_Approved] [bit] NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Bid] PRIMARY KEY ([Bid_ID]),
    CONSTRAINT [fk_Bid_Special_Service_Order_ID] FOREIGN KEY ([Special_Service_Order_ID]) 
        REFERENCES [dbo].[Special_Service_Order]([Special_Service_Order_ID]),
    CONSTRAINT [fk_Bid_Vendor_ID] FOREIGN KEY ([Vendor_ID]) 
        REFERENCES [Vendor]([Vendor_ID])
)
GO

/******************
Create the [dbo].[Special_Work_Order] table
***************/
print ''
Print '***Create the [dbo].[Special_Work_Order] table***' 
GO
CREATE TABLE [dbo].[Special_Work_Order]
(
    [Special_Work_Order_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Bid_ID] [int] NOT NULL,
    [Work_Order_Description] [nvarchar](500) NOT NULL,
    [Drop_Off_Date] [datetime] NOT NULL,
    [Pick_Up_Date] [datetime] NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Special_Work_Order] PRIMARY KEY ([Special_Work_Order_ID]),
    CONSTRAINT [fk_Special_Work_Order_Bid_ID] FOREIGN KEY ([Bid_ID]) 
        REFERENCES [dbo].[Bid]([Bid_ID])
)
GO

/******************
Create the [dbo].[Change_Order] table
***************/
print ''
Print '***Create the [dbo].[Change_Order] table***' 
GO
CREATE TABLE [dbo].[Change_Order]
(
    [Change_Order_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Vendor_ID] [int] NOT NULL,
    [Change_Order_Date] [datetime] NOT NULL,
    [Original_PO_Number] [int] NOT NULL,
    [Employee_ID] [int] NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Change_Order] PRIMARY KEY([Change_Order_ID]),
    CONSTRAINT [fk_Change_Order_Vendor_ID] FOREIGN KEY ([Vendor_ID])
        REFERENCES [dbo].[Vendor] ([Vendor_ID]),
    CONSTRAINT [fk_Change_Order_Purchase_Order_ID] FOREIGN KEY([Original_PO_Number])
        REFERENCES [dbo].[Purchase_Order] ([Purchase_Order_ID]),
    CONSTRAINT [fk_Change_Order_Employee_ID] FOREIGN KEY ([Employee_ID])
        REFERENCES [dbo].[Employee] ([Employee_ID])
)
GO

/******************
Create the [dbo].[Change_Order_Line] table
***************/
print ''
Print '***Create the [dbo].[Change_Order_Line] table***' 
GO
CREATE TABLE [dbo].[Change_Order_Line]
(
    [Change_Order_ID] [int] NOT NULL,
    [Parts_Inventory_ID] [int] NOT NULL,
    [Original_Qty] [int] NOT NULL,
    [Updated_Qty] [int] NOT NULL,
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [pk_Change_Order_Line] PRIMARY KEY ([Change_Order_ID]),
    CONSTRAINT [fk_Change_Order_Line_Change_Order_ID] FOREIGN KEY ([Change_Order_ID])
    	REFERENCES [dbo].[Change_Order] ([Change_Order_ID]),
    CONSTRAINT [fk_Change_Order_Line_Parts_Inventory_ID] FOREIGN KEY ([Parts_Inventory_ID])
    	REFERENCES [dbo].[Parts_Inventory] ([Parts_Inventory_ID])
)
GO

/******************
Create the [dbo].[Packing_Slip_Line_Items] table
***************/
print ''
Print '***Create the [dbo].[Packing_Slip_Line_Items] table***' 
GO
CREATE TABLE [dbo].[Packing_Slip_Line_Items]
(
    [Packing_Slip_ID] [int] NOT NULL,
    [Qty_Recieved] [int] NOT NULL,
    [Parts_Inventory_ID] [int] NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Packing_Slip_Line_Items] PRIMARY KEY ([Packing_Slip_ID], [Parts_Inventory_ID]),
    CONSTRAINT [fk_Packing_Slip_Line_Items_Packing_Slip_ID] FOREIGN KEY ([Packing_Slip_ID])
        REFERENCES [dbo].[Packing_Slip]([Packing_Slip_ID]),
    CONSTRAINT [fk_Packing_Slip_Line_Items_Parts_Inventory] FOREIGN KEY ([Parts_Inventory_ID])
        REFERENCES [dbo].[Parts_Inventory] ([Parts_Inventory_ID])
)
GO

/******************
Create the [dbo].[Model_Compatibility] table
***************/
print ''
Print '***Create the [dbo].[Model_Compatibility] table***' 
GO
CREATE TABLE [dbo].[Model_Compatibility]
(
    [Vehicle_Model_ID] [int] NOT NULL,
    [Parts_Inventory_ID] [int] NOT NULL,
    [Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Model_Compatibility] PRIMARY KEY([Vehicle_Model_ID], [Parts_Inventory_ID]),
    CONSTRAINT [fk_Model_Compatibility_Vehicle_Model_ID] FOREIGN KEY([Vehicle_Model_ID])
        REFERENCES [dbo].[Vehicle_Model]([Vehicle_Model_ID]),
    CONSTRAINT [fk_Model_Compatibility_Parts_Inventory_ID] FOREIGN KEY([Parts_Inventory_ID])
        REFERENCES [dbo].[Parts_Inventory]([Parts_Inventory_ID])
)
GO

/******************
Create the [dbo].[Service_Detail] table
***************/
print ''
Print '***Create the [dbo].[Service_Detail] table***' 
GO
CREATE TABLE [dbo].[Service_Detail]
(
    [Service_Detail_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Service_Order_ID] [int] NOT NULL,
    [Service_Order_Version] [int] NOT NULL,
    [Service_Type_ID] [nvarchar](256) NOT NULL,
    [Employee_ID] [int] NOT NULL,
    CONSTRAINT [pk_Service_Detail] PRIMARY KEY ([Service_Detail_ID]),
    CONSTRAINT [fk_Service_Detail_Service_Order_ID_Service_Order_Version]
        FOREIGN KEY ([Service_Order_ID], [Service_Order_Version])
        REFERENCES [dbo].[Service_Order]([Service_Order_ID], [Service_Order_Version]),
    CONSTRAINT [fk_Service_Detail_Service_Type] FOREIGN KEY ([Service_Type_ID])
        REFERENCES [dbo].[Service_Type]([Service_Type_ID]) ON UPDATE CASCADE,
    CONSTRAINT [fk_Service_Detail_Employee_ID] FOREIGN KEY ([Employee_ID])
        REFERENCES [dbo].[Employee]([Employee_ID])
)
GO

/******************
Create the [dbo].[Parts_Request] table
***************/
print ''
Print '***Create the [dbo].[Parts_Request] table***' 
GO
CREATE TABLE [dbo].[Parts_Request]
(
    [Parts_Request_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Employee_ID] [int] NOT NULL,
    [Service_Detail_ID] [int] NOT NULL,
    [Parts_Request_Notes] [nvarchar](MAX) NULL,
    [Date_Requested] [date] NOT NULL,
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [pk_Parts_Request] PRIMARY KEY ([Parts_Request_ID]),
    CONSTRAINT [fk_Parts_Request_Employee_ID] FOREIGN KEY ([Employee_ID])
    	REFERENCES [dbo].[Employee] ([Employee_ID]),
    CONSTRAINT [fk_Parts_Request_Service_Detail_ID] FOREIGN KEY ([Service_Detail_ID])
    	REFERENCES [dbo].[Service_Detail] ([Service_Detail_ID])
)  
GO

/******************
Create the [dbo].[Parts_Request_Line_Items] table
***************/
print ''
Print '***Create the [dbo].[Parts_Request_Line_Items] table***' 
GO
CREATE TABLE [dbo].[Parts_Request_Line_Items]
(
    [Parts_Request_ID] [int] NOT NULL,
    [Parts_Inventory_ID] [int] NOT NULL,
    [Qty_Requested] [int] NOT NULL,
    [Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Parts_Request_Line_Items] PRIMARY KEY([Parts_Request_ID], [Parts_Inventory_ID]),
    CONSTRAINT [fk_Parts_Request_Line_Items_Parts_Request_ID] FOREIGN KEY([Parts_Request_ID])
        REFERENCES [dbo].[Parts_Request]([Parts_Request_ID]),
    CONSTRAINT [fk_Parts_Request_Line_Items_Parts_Inventory_ID] FOREIGN KEY([Parts_Inventory_ID])
        REFERENCES [dbo].[Parts_Inventory]([Parts_Inventory_ID])
)
GO

/******************
Create the [dbo].[Inspection_Report] table
***************/
print ''
print '***Create the [dbo].[Inspection_Report] table***'
GO
CREATE TABLE [dbo].[Inspection_Report]
(
    [Inspection_Report_ID] [int]IDENTITY(100000, 1) NOT NULL,
    [Employee_ID] [int] NOT NULL,
    [VIN] [nvarchar](17) NOT NULL,
    [Service_Order_ID] [int] NOT NULL,
    [Service_Order_Version] [int] NOT NULL,
    [Oil_Level] [nvarchar](4) NULL,
    [Tire_Pressure] [int] NULL,
    [Front_Left_Turn_Signal] [bit] NULL,
    [Front_Right_Turn_Signal] [bit] NULL,
    [Rear_Left_Turn_Signal] [bit] NULL,
    [Rear_Right_Turn_Signal] [bit] NULL,
    [Left_Brake_Light] [bit] NULL,
    [Right_Brake_Light] [bit] NULL,
    [Windshield_Washer_Fluid] [nvarchar](4) NULL,
    [Problem_Description] [nvarchar](256) NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Inspection_Report] PRIMARY KEY ([Inspection_Report_ID]),
    CONSTRAINT [fk_Inspection_Report_Employee_ID] FOREIGN KEY ([Employee_ID])
        REFERENCES [dbo].[Employee] ([Employee_ID]),
    CONSTRAINT [fk_Inspection_Report_VIN] FOREIGN KEY ([VIN])
        REFERENCES [dbo].[Vehicle] ([VIN]),
    CONSTRAINT [fk_Inspection_Report_Service_Order_ID_Service_Order_Version] 
        FOREIGN KEY ([Service_Order_ID], [Service_Order_Version])
        REFERENCES [dbo].[Service_Order]([Service_Order_ID], [Service_Order_Version])
)
GO

/******************
Create the [dbo].[Client] table
***************/
print ''
Print '***Create the [dbo].[Client] table***'
GO
CREATE TABLE [dbo].[Client]
(
    [Client_ID] [int] IDENTITY(100000, 1) NOT NULL,
    [Given_Name] [nvarchar] (50) NOT NULL,
    [Family_Name] [nvarchar] (50) NOT NULL,
    [Middle_Name] [nvarchar] (50) NULL,
    [DOB] [date] NOT NULL,
    [Email] [nvarchar] (255) UNIQUE NOT NULL,
    [Postal_Code] [nvarchar] (15) NOT NULL,
    -- getting this column from Zipcode table
    [City] [nvarchar] (50) NULL,
    -- optional, can get city from Zipcode table
    [Region] [nvarchar] (50) NULL DEFAULT "",
    -- ignore this column; no longer in use
    [Address] [nvarchar] (100) NULL,
    [Text_Number] [nvarchar] (12) UNIQUE NULL,
    [Voice_Number] [nvarchar] (12) UNIQUE NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Client_ID] PRIMARY KEY ([Client_ID]),
    CONSTRAINT [ak_Client_Email] UNIQUE ([Email]),
    CONSTRAINT [ak_Client_Text_Number] UNIQUE ([Text_Number]),
    CONSTRAINT [ak_Client_Voice_Number] UNIQUE ([Voice_Number]),
    CONSTRAINT [fk_Client_Postal_Code] FOREIGN KEY ([Postal_Code])
        REFERENCES [dbo].[Zipcode] ([Zip_Code])
)
GO

/******************
Create the [dbo].[Client_Credential] table
***************/
print ''
Print '***Create the [dbo].[Client_Credential] table***' 
GO
CREATE TABLE [dbo].[Client_Credential]
(
    [License_Number] [nvarchar] (12) NOT NULL,
    [Driver_License_Class_ID] [nvarchar](6) NOT NULL,
    [License_Expiration] [date] NOT NULL,
    [Client_ID] [int] NOT NULL,
    [Permission] [nvarchar](50) NULL,
    [Certified] [bit] NULL,
    [Certification_Description] [nvarchar](250) NULL,
    [Certification_Date] [date] NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Client_Credential] PRIMARY KEY ([License_Number]),
    CONSTRAINT [fk_Client_Credential_Driver_License_Class_ID] FOREIGN KEY ([Driver_License_Class_ID])
        REFERENCES [dbo].[Driver_License_Class] ([Driver_License_Class_ID]),
    CONSTRAINT [fk_Client_Credential_Client_ID] FOREIGN KEY ([Client_ID])
        REFERENCES [dbo].[Client] ([Client_ID])
)
GO

/******************
Create the [dbo].[Login] table
***************/
print ''
Print '***Create the [dbo].[Login] table***' 
GO
CREATE TABLE [dbo].[Login]
(
    [Username] [nvarchar](50) NOT NULL,
    [Password_Hash] [nvarchar](100) NOT NULL DEFAULT 
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
    [Client_ID] [int] NULL,
    [Employee_ID] [int] NULL,
    [Security_Question_1] [nvarchar](100) NULL,
    [Security_Question_2] [nvarchar](100) NULL,
    [Security_Question_3] [nvarchar](100) NULL,
    [Security_Response_1] [nvarchar](100) NULL,
    [Security_Response_2] [nvarchar](100) NULL,
    [Security_Response_3] [nvarchar](100) NULL,
    [Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Login] PRIMARY KEY ([Username]),
    CONSTRAINT [fk_Login_Client_ID] FOREIGN KEY ([Client_ID])
        REFERENCES [dbo].[Client]([Client_ID]),
    /*CONSTRAINT		[ak_Login_Client_ID] UNIQUE ([Client_ID]),
	CONSTRAINT		[ak_Login_Employee_ID] UNIQUE ([Employee_ID])*/
)
GO
print ''
Print '***Creating Index for [dbo].[Login] table***' 
GO
CREATE UNIQUE INDEX [idx_Login_Username_Client_ID]
	ON [dbo].[Login] ([Username],[Client_ID])
GO

/******************
Create the [dbo].[Client_Role] table
***************/
print ''
Print '***Create the [dbo].[Client_Role] table***' 
GO
CREATE TABLE [dbo].[Client_Role]
(
    [Client_Role_ID] [nvarchar](100) NOT NULL,
    [Role_Description] [nvarchar] (500) NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Client_Role] PRIMARY KEY ([Client_Role_ID])
)
GO

/******************
Create the [dbo].[Client_Client_Role] table
***************/
print ''
Print '***Create the [dbo].[Client_Client_Role] table***' 
GO
CREATE TABLE [dbo].[Client_Client_Role]
(
    [Client_ID] [int] NOT NULL,
    [Client_Role_ID] [nvarchar](100) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Client_Client_Role] PRIMARY KEY ([Client_ID],[Client_Role_ID]),
    CONSTRAINT [fk_Client_Client_Role_Client_ID] FOREIGN KEY([Client_ID])
        REFERENCES [dbo].[Client] ([Client_ID]),
    CONSTRAINT [fk_Client_Client_Role_Client_Role_ID] FOREIGN KEY([Client_Role_ID])
        REFERENCES [dbo].[Client_Role] ([Client_Role_ID])
)
GO

/******************
Create the [dbo].[Accommodation] table
***************/
print ''
Print '***Create the [dbo].[Accommodation] table***' 
GO
CREATE TABLE [dbo].[Accommodation]
(
    --[Accommodation_ID] [int]	IDENTITY(100000,1)	NOT NULL,
    [Accommodation_ID] [nvarchar](100) NOT NULL,
    [Accommodation_Description] [nvarchar](255) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Accommodation] PRIMARY KEY([Accommodation_ID])
)
GO

/******************
Create the [dbo].[Client_Accommodation] table
***************/
print ''
Print '***Create the [dbo].[Client_Accommodation] table***' 
GO
CREATE TABLE [dbo].[Client_Accommodation]
(
    [Client_ID] [int] NOT NULL,
    [Accommodation_ID] [nvarchar] (100) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Client_Accommodation] PRIMARY KEY ([Client_ID],[Accommodation_ID]),
    CONSTRAINT [fk_Client_Accommodation_Client_ID] FOREIGN KEY([Client_ID])
        REFERENCES [dbo].[Client] ([Client_ID]),
    CONSTRAINT [fk_Client_Accommodation_Accommodation_ID] FOREIGN KEY([Accommodation_ID])
        REFERENCES [dbo].[Accommodation] ([Accommodation_ID])
)
GO

/******************
Create the [dbo].[Dependent] table
***************/
print ''
Print '***Create the [dbo].[Dependent] table***' 
GO
CREATE TABLE [dbo].[Dependent]
(
    [Dependent_ID] [int] IDENTITY(100000, 1) NOT NULL,
    [Given_Name] [nvarchar](50) NOT NULL,
    [Family_Name] [nvarchar](50) NOT NULL,
    [Middle_Name] [nvarchar](100) NULL,
    [DOB] [DATE] NOT NULL,
    [Gender] [nvarchar](20) NULL,
    [Emergency_Contact] [nvarchar](100) NOT NULL,
    [Contact_Relationship] [nvarchar](100) NOT NULL,
    [Emergency_Phone] [nvarchar](12) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Dependent] PRIMARY KEY ([Dependent_ID])
)
GO
print ''
Print '***Creating Index for [dbo].[Dependent] table***' 
GO
CREATE INDEX [idx_Dependent_Dependent_ID_Family_Name]
	ON [dbo].[Dependent] ([Dependent_ID],[Family_Name])
GO

/******************
Create the [dbo].[Dependent_Accommodation] table
***************/
print ''
Print '***Create the [dbo].[Dependent_Accommodation] table***' 
GO
CREATE TABLE [dbo].[Dependent_Accommodation]
(
    [Dependent_ID] [int] NOT NULL,
    [Accommodation_ID] [nvarchar](100) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Dependent_Accommodation] PRIMARY KEY ([Dependent_ID], [Accommodation_ID]),
    CONSTRAINT [fk_Dependent_Accommodation_Dependent_ID] FOREIGN KEY ([Dependent_ID])
        REFERENCES [dbo].[Dependent]([Dependent_ID]),
    CONSTRAINT [fk_Dependent_Accommodation_Accommodation_ID] FOREIGN KEY ([Accommodation_ID])
        REFERENCES [dbo].[Accommodation]([Accommodation_ID])
)
GO

/******************
Create the [dbo].[Client_Dependent_Role] table
***************/
print ''
Print '***Create the [dbo].[Client_Dependent_Role] table***' 
GO
CREATE TABLE [dbo].[Client_Dependent_Role]
(
    [Client_ID] [int] NOT NULL,
    [Dependent_ID] [int] NOT NULL,
    [Relationship] [nvarchar](100) NOT NULL,
    [Is_Active] [bit] NOT NULL,
    CONSTRAINT [pk_Client_Dependent_Role] PRIMARY KEY ([Client_ID], [Dependent_ID]),
    CONSTRAINT [fk_Client_Dependent_Role_Client_ID] FOREIGN KEY ([Client_ID]) 
        REFERENCES [dbo].[Client]([Client_ID]),
    CONSTRAINT [fk_Client_Dependent_Role_Dependent_ID] FOREIGN KEY ([Dependent_ID]) 
        REFERENCES [dbo].[Dependent]([Dependent_ID])
)
GO

/******************
Create the [dbo].[Notification] table
***************/
print ''
Print '***Create the [dbo].[Notification] table***' 
GO
CREATE TABLE [dbo].[Notification]
(
    [Notification_ID] [int] IDENTITY(100000, 1) NOT NULL,
    [Client_ID] [int] NOT NULL,
    [Title] [nvarchar](255) NOT NULL DEFAULT 'User Notification',
    [Notification_Body] [text] NOT NULL,
    [Time_Sent] [DATETIME] NOT NULL DEFAULT CURRENT_TIMESTAMP,
    [Viewed] [bit] NOT NULL DEFAULT 0,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Notification] PRIMARY KEY ([Notification_ID]),
    CONSTRAINT [fk_Notification_Client_ID] FOREIGN KEY ([Client_ID])
        REFERENCES [dbo].[Client]([Client_ID])
)
GO
print ''
Print '***Creating Index for [dbo].[Notification] table***' 
GO
CREATE INDEX [idx_Notification_Notification_ID_Client_ID]
    ON [dbo].[Notification] ([Notification_ID],[Client_ID])
GO

/******************
Create the [dbo].[Ticket_Type] table
***************/
print ''
Print '***Create the [dbo].[Ticket_Type] table***' 
GO
CREATE TABLE [dbo].[Ticket_Type]
(
    [Ticket_Type_ID] [nvarchar](50) NOT NULL,
    [Type_Description] [nvarchar](500) NULL,
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [pk_Ticket_Type] PRIMARY KEY([Ticket_Type_ID])
)
GO

/******************
Create the [dbo].[Support_Ticket] table
***************/
print ''
Print '***Create the [dbo].[Support_Ticket] table***' 
GO
CREATE TABLE [dbo].[Support_Ticket]
(
    [Support_Ticket_ID] [int] NOT NULL IDENTITY(100000, 1),
    [Ticket_Type_ID] [nvarchar](50) NOT NULL,
    [Client_ID] [int] NOT NULL,
    [Time_Opened] [datetime] NOT NULL,
    [Time_Closed] [datetime] NULL,
    [Support_Note] [nvarchar](3000) NULL,
    [Is_Open] [bit] DEFAULT 1 NOT NULL,
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [pk_Support_Ticket] PRIMARY KEY([Support_Ticket_ID]),
    CONSTRAINT [fk_Support_Ticket_Ticket_Type_ID] FOREIGN KEY ([Ticket_Type_ID])
		REFERENCES [dbo].[Ticket_Type] ([Ticket_Type_ID]),
    CONSTRAINT [fk_Support_Ticket_Client_ID] FOREIGN KEY ([Client_ID])
		REFERENCES [dbo].[Client] ([Client_ID])
)
GO

/******************
Create the [dbo].[Support_Ticket_Employee_Line] table
***************/
print ''
Print '***Create the [dbo].[Support_Ticket_Employee_Line] table***' 
GO
CREATE TABLE [dbo].[Support_Ticket_Employee_Line]
(
    [Support_Ticket_ID] [int] NOT NULL,
    [Employee_ID] [int] NOT NULL,
    [Time_Assigned] [datetime] NOT NULL,
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [pk_Support_Ticket_Employee_Line] PRIMARY KEY([Support_Ticket_ID], [Employee_ID]),
    CONSTRAINT [fk_Support_Ticket_Employee_Line_Support_Ticket_ID] FOREIGN KEY ([Support_Ticket_ID])
		REFERENCES [dbo].[Support_Ticket] ([Support_Ticket_ID]),
    CONSTRAINT [fk_Support_Ticket_Employee_Line_Employee_ID] FOREIGN KEY ([Employee_ID])
		REFERENCES [dbo].[Employee] ([Employee_ID])
)
GO

/******************
Create the [dbo].[Charter] table
***************/
print ''
Print '***Create the [dbo].[Charter] table***' 
GO
CREATE TABLE [dbo].[Charter]
(
    [Charter_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Client_ID] [int] NOT NULL,
    [Client_Is_Passenger] [bit] NOT NULL DEFAULT 1,
    [Rider_Quantity] [int] NOT NULL,
    [Driver_Needed] [bit] NOT NULL DEFAULT 1,
    [Reviewed_By] [int] NULL,
    [Is_Approved] [bit] NULL,
    [Date_Request_Start] [datetime] NOT NULL,
    [Date_Request_End] [datetime] NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Charter] PRIMARY KEY ([Charter_ID]),
    CONSTRAINT [fk_Charter_Client_ID] FOREIGN KEY ([Client_ID]) 
        REFERENCES [dbo].[Client]([Client_ID]),
    CONSTRAINT [fk_Charter_Employee_ID] FOREIGN KEY ([Reviewed_By]) 
        REFERENCES [dbo].[Employee]([Employee_ID])
)
GO
print ''
Print '***Creating Index for [dbo].[Charter] table***' 
GO
CREATE INDEX [idx_Charter_Charter_ID_Client_ID]
    ON [dbo].[Charter] ([Charter_ID],[Client_ID])
GO

/******************
Create the [dbo].[Charter_Assignment] table
***************/
print ''
Print '***Create the [dbo].[Charter_Assignment] table***' 
GO
CREATE TABLE [dbo].[Charter_Assignment]
(
    [Assignment_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Driver_ID] [int] NULL,
    [Charter_ID] [int] NOT NULL,
    [VIN] [nvarchar](17) NOT NULL,
    [Vehicle_Type_ID] [nvarchar](50) NOT NULL,
    [Date_Issued] [datetime] NULL,
    [Date_Returned] [datetime] NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Charter_Assignment] PRIMARY KEY ([Assignment_ID]),
    CONSTRAINT [fk_Charter_Assignment_Driver_ID] FOREIGN KEY ([Driver_ID])
        REFERENCES [dbo].[Driver]([Employee_ID]),
    CONSTRAINT [fk_Charter_Assignment_Charter_ID] FOREIGN KEY ([Charter_ID])
        REFERENCES [dbo].[Charter]([Charter_ID]),
    CONSTRAINT [fk_Charter_Assignment_VIN] FOREIGN KEY ([VIN])
        REFERENCES [dbo].[Vehicle]([VIN]),
    CONSTRAINT [fk_Charter_Assignment_Vehicle_Type_ID] FOREIGN KEY ([Vehicle_Type_ID])
        REFERENCES [dbo].[Vehicle_Type]([Vehicle_Type_ID])
)
GO

/******************
Create the [dbo].[Charter_Accommodation] table
***************/
print ''
Print '***Create the [dbo].[Charter_Accommodation] table***' 
GO
CREATE TABLE [dbo].[Charter_Accommodation]
(
    [Charter_ID] [int] NOT NULL,
    [Accommodation_ID] [nvarchar](100) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Charter_Accommodation] PRIMARY KEY ([Charter_ID], [Accommodation_ID]),
    CONSTRAINT [fk_Charter_Accommodation_Charter_ID] FOREIGN KEY ([Charter_ID])
        REFERENCES [dbo].[Charter]([Charter_ID]),
    CONSTRAINT [fk_Charter_Accommodation_Accommodation_ID] FOREIGN KEY ([Accommodation_ID])
        REFERENCES [dbo].[Accommodation]([Accommodation_ID])
)
GO
print ''
Print '***Creating Index for [dbo].[Charter_Accommodation] table***' 
GO
CREATE INDEX [idx_Charter_Accommodation_Charter_ID_Accommodation_ID]
    ON [dbo].[Charter_Accommodation] ([Charter_ID],[Accommodation_ID])
GO

/******************
Create the [dbo].[Charter_Stop] table
***************/
print ''
Print '***Create the [dbo].[Charter_Stop] table***' 
GO
CREATE TABLE [dbo].[Charter_Stop]
(
    [Charter_Stop_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Charter_ID] [int] NOT NULL,
    [Street_Address] [nvarchar](255) NOT NULL,
    [Zip_Code] [nvarchar](5) NOT NULL,
    [Latitude] [decimal](8,6) NOT NULL,
    [Longitude] [decimal](9,6) NOT NULL,
    [Duration] [int] NOT NULL,
    [Description] [nvarchar](255) NULL,
    [Stop_Number] [int] NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Charter_Stop] PRIMARY KEY ([Charter_Stop_ID]),
    CONSTRAINT [fk_Charter_Stop_Charter_ID] FOREIGN KEY ([Charter_ID])
        REFERENCES [dbo].[Charter]([Charter_ID])
)
GO

/******************
Create the [dbo].[Charter_Rider] table
***************/
print ''
Print '***Create the [dbo].[Charter_Rider] table***' 
GO
CREATE TABLE [dbo].[Charter_Rider]
(
    [Charter_ID] [int] NOT NULL,
    [Dependent_ID] [int] NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Charter_Rider] PRIMARY KEY ([Charter_ID] , [Dependent_ID]),
    CONSTRAINT [fk_Charter_Rider_Charter_ID] FOREIGN KEY ([Charter_ID]) 
        REFERENCES [dbo].[Charter]([Charter_ID]),
    CONSTRAINT [fk_Charter_Rider_Dependent_ID] FOREIGN KEY ([Dependent_ID]) 
        REFERENCES [dbo].[Dependent]([Dependent_ID])
)
GO

/******************
Create the [dbo].[Vehicle_Checklist] table
***************/
print ''
Print '***Create the [dbo].[Vehicle_Checklist] table***' 
GO
CREATE TABLE [dbo].[Vehicle_Checklist]
(
    [Checklist_ID] [int] IDENTITY(100000, 1) NOT NULL,
    [Employee_ID] [int] NOT NULL,
    [VIN] [nvarchar] (17) NOT NULL,
    [Date] [datetime] NOT NULL,
    [Clean] [bit] NOT NULL DEFAULT 0,
    [Pedals] [bit] NOT NULL DEFAULT 0,
    [Dash] [bit] NOT NULL DEFAULT 0,
    [Steering] [bit] NOT NULL DEFAULT 0,
    [AC_Heat] [bit] NOT NULL DEFAULT 0,
    [Mirror_DS] [bit] NOT NULL DEFAULT 0,
    [Mirror_PS] [bit] NOT NULL DEFAULT 0,
    [Mirror_RV] [bit] NOT NULL DEFAULT 0,
    [Cosmetic] [nvarchar] (500) NULL,
    [Tire_Pressure_DF] [int] NOT NULL,
    [Tire_Pressure_PF] [int] NOT NULL,
    [Tire_Pressure_DR] [int] NOT NULL,
    [Tire_Pressure_PR] [int] NOT NULL,
    [Blinker_DF] [bit] NOT NULL DEFAULT 0,
    [Blinker_PF] [bit] NOT NULL DEFAULT 0,
    [Blinker_DR] [bit] NOT NULL DEFAULT 0,
    [Blinker_PR] [bit] NOT NULL DEFAULT 0,
    [Breaklight_DF] [bit] NOT NULL DEFAULT 0,
    [Breaklight_PF] [bit] NOT NULL DEFAULT 0,
    [Breaklight_DR] [bit] NOT NULL DEFAULT 0,
    [Breaklight_PR] [bit] NOT NULL DEFAULT 0,
    [Headlight_DS] [bit] NOT NULL DEFAULT 0,
    [Headlight_PS] [bit] NOT NULL DEFAULT 0,
    [Taillight_DS] [bit] NOT NULL DEFAULT 0,
    [Taillight_PS] [bit] NOT NULL DEFAULT 0,
    [Wiper_DS] [bit] NOT NULL DEFAULT 0,
    [Wiper_PS] [bit] NOT NULL DEFAULT 0,
    [Wiper_R] [int] NOT NULL DEFAULT 0,
    [Seat_Belts] [bit] NOT NULL DEFAULT 0,
    [Fire_Extinguisher] [bit] NOT NULL DEFAULT 0,
    [Airbags] [bit] NOT NULL DEFAULT 0,
    [First_Aid] [bit] NOT NULL DEFAULT 0,
    [Emergency_Kit] [bit] NOT NULL DEFAULT 0,
    [Mileage] [int] NOT NULL,
    [Fuel_Level] [int] NOT NULL,
    [Brakes] [bit] NOT NULL DEFAULT 0,
    [Accelerator] [bit] NOT NULL DEFAULT 0,
    [Clutch] [bit] NOT NULL DEFAULT 0,
    [Notes] [nvarchar] (1000) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Vehicle_Checklist] PRIMARY KEY([Checklist_ID], [Employee_ID], [VIN]),
    CONSTRAINT [fk_Vehicle_Checklist_Employee_ID] FOREIGN KEY([Employee_ID])
        REFERENCES [dbo].[Employee] ([Employee_ID]),
    CONSTRAINT [fk_Vehicle_Checklist_VIN] FOREIGN KEY([VIN])
        REFERENCES [dbo].[Vehicle] ([VIN])
)
GO
print ''
Print '***Creating Index for [dbo].[Vehicle_Checklist] table***' 
GO
CREATE INDEX [idx_Vehicle_Checklist_Employee_ID_VIN]
    ON [dbo].[Vehicle_Checklist] ([Employee_ID],[VIN])
GO

/******************
Create the [dbo].[Driver_Maintenance_Report] table
***************/
print ''
Print '***Create the [dbo].[Driver_Maintenance_Report] table***' 
GO
CREATE TABLE [dbo].[Driver_Maintenance_Report]
(
    [Driver_Maintenance_Report_ID] [int]IDENTITY(100000, 1) NOT NULL,
    [Driver_ID] [int] NOT NULL,
    [Date_Time] [datetime] NOT NULL DEFAULT GETDATE(),
    [VIN] [nvarchar](17) NOT NULL,
    [Severity] [nvarchar](20) NOT NULL DEFAULT 'low',
    [Description] [nvarchar](250) NOT NULL DEFAULT '',
    [Is_Active] [bit] NOT NULL DEFAULT 1
        CONSTRAINT [pk_Driver_Maintenance_Report] PRIMARY KEY ([Driver_Maintenance_Report_ID]),
    CONSTRAINT [fk_Driver_Maintenance_Report_Driver_ID] FOREIGN KEY ([Driver_ID])
        REFERENCES [dbo].[Driver] ([Employee_ID]),
    CONSTRAINT [fk_Driver_Maintenance_Report_VIN] FOREIGN KEY ([VIN])
        REFERENCES [dbo].[Vehicle] ([VIN])
)
GO
print ''
Print '***Creating Index for [dbo].[Driver_Maintenance_Report] table***' 
GO
CREATE INDEX [idx_Driver_Maintenance_Report_Driver_ID_VIN]
    ON [dbo].[Driver_Maintenance_Report] ([Driver_ID],[VIN])
GO

/******************
Create the [dbo].[Driver_Unavailable] table
***************/
print ''
print '***Create the [dbo].[Driver_Unavailable] table***'
GO
CREATE TABLE [dbo].[Driver_Unavailable]
(
    [Unavailable_ID] [int]IDENTITY(100000, 1) NOT NULL,
    [Driver_ID] [int] NOT NULL,
    [Start_Datetime] [datetime] NOT NULL,
    [End_DateTime] [datetime] NOT NULL,
    [Reason] [nvarchar](250) NOT NULL DEFAULT '',
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Driver_Unavailable] PRIMARY KEY ([Unavailable_ID]),
    CONSTRAINT [fk_Driver_Unavailable_Driver_ID] FOREIGN KEY ([Driver_ID])
		REFERENCES [dbo].[Driver] ([Employee_ID]),
)
GO

/******************
Create the [dbo].[Service] table
***************/
print ''
Print '***Create the [dbo].[Service] table***' 
GO
CREATE TABLE [dbo].[Service]
(
    [Service_ID] [nvarchar](20) NOT NULL,
    [Type] [nvarchar](20) NOT NULL,
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [pk_Service] PRIMARY KEY ([Service_ID])
)
GO

/******************
Create the [dbo].[Service_Assignment] table
***************/
print ''
Print '***Create the [dbo].[Service_Assignment] table***' 
GO
CREATE TABLE [dbo].[Service_Assignment]
(
    [Service_Assignment_ID] [int] IDENTITY(100000,1) NOT NULL,
    [VIN] [nvarchar](17) NOT NULL,
    [Service_ID] [nvarchar](20) NOT NULL,
    [Driver_ID] [int] NOT NULL,
    [Start_Datetime] [datetime] NULL,
    [End_Datetime] [datetime] NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Service_Assignment] PRIMARY KEY ([Service_Assignment_ID]),
    CONSTRAINT [fk_Service_Assignment_VIN] FOREIGN KEY ([VIN]) 
        REFERENCES [dbo].[Vehicle]([VIN]),
    CONSTRAINT [fk_Service_Assignment_Service_ID] FOREIGN KEY ([Service_ID]) 
        REFERENCES [dbo].[Service]([Service_ID]),
    CONSTRAINT [fk_Service_Assignment_Driver_ID] FOREIGN KEY ([Driver_ID]) 
        REFERENCES [dbo].[Driver]([Employee_ID])
)
GO

/******************
Create the [dbo].[Ride] table
***************/
print ''
Print '***Create the [dbo].[Ride] table***' 
GO
CREATE TABLE [dbo].[Ride]
(
    [Ride_ID] [int] IDENTITY(100000, 1),
    [Client_ID] [int] NOT NULL,
    [Service_ID] [nvarchar](20) NOT NULL,
    [Service_Assignment_ID] [int] NULL,
    [Pickup_Location] [nvarchar](255) NOT NULL,
    [Dropoff_Location] [nvarchar](255) NOT NULL,
    [Scheduled_Pickup_Time] [datetime] NULL,
    [Estimated_Dropoff_Time] [datetime] NULL,
    [Actual_Pickup_Time] [datetime] NULL,
    [Actual_Dropoff_Time] [datetime] NULL,
    [Requested] [bit] NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT(1),
    CONSTRAINT [pk_Ride] PRIMARY KEY([Ride_ID]),
    CONSTRAINT [fk_Ride_Client_ID] FOREIGN KEY([Client_ID]) 
        REFERENCES [dbo].[Client]([Client_ID]),
    CONSTRAINT [fk_Ride_Service_ID] FOREIGN KEY([Service_ID]) 
        REFERENCES [dbo].[Service]([Service_ID]),
    CONSTRAINT [fk_Ride_Service_Assignment_ID] FOREIGN KEY([Service_Assignment_ID]) 
        REFERENCES [dbo].[Service_Assignment]([Service_Assignment_ID])
)
GO

/******************
Create the [dbo].[Source] table
***************/
print ''
Print '***Create the [dbo].[Source] table***' 
GO

CREATE TABLE [dbo].[Source]
(
    [Vendor_ID] [int] NOT NULL,
    [Parts_Inventory_ID] [int] NOT NULL,
    [Vendor_Part_Number] [nvarchar](100) NOT NULL,
    [Estimated_Delivery_Time_Days] [int] NULL,
    [Part_Price] [smallmoney] NOT NULL,
    [Minimum_Order_Qty] [int] NULL,
    [Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Source] PRIMARY KEY ([Vendor_Id] , [Parts_Inventory_ID]),
    CONSTRAINT [fk_Source_Vendor_ID] FOREIGN KEY ([Vendor_ID]) 
        REFERENCES [dbo].[Vendor]([Vendor_ID]),
    CONSTRAINT [fk_Source_Parts_Inventory_ID] FOREIGN KEY ([Parts_Inventory_ID]) 
        REFERENCES [dbo].[Parts_Inventory]([Parts_Inventory_ID])
)
GO

/******************
Create the [dbo].[Vehicle_Unavailable] table
***************/
print ''
Print '***Create the [dbo].[Vehicle_Unavailable] table***' 
GO
CREATE TABLE [dbo].[Vehicle_Unavailable]
(
    [Unavailable_ID] [int] IDENTITY(100000,1) NOT NULL,
    [VIN] [nvarchar](17) NOT NULL,
    [Start_Datetime] [datetime] NOT NULL,
    [End_Datetime] [datetime] NOT NULL,
    [Reason] [nvarchar](1000) NOT NULL,
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [pk_Vehicle_Unavailable] PRIMARY KEY ([Unavailable_ID]),
    CONSTRAINT [fk_Vehicle_Unavailable_VIN] FOREIGN KEY ([VIN])
        REFERENCES [dbo].[Vehicle]([VIN])
)
GO

/******************
Create the [dbo].[Password_Reset] table
***************/
print ''
print '***Create the [dbo].[Password_Reset] table***'
CREATE TABLE [dbo].[Password_Reset]
(
    [Password_Reset_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Username] [nvarchar](50) NOT NULL,
    [Request_Datetime] [datetime] NOT NULL DEFAULT CURRENT_TIMESTAMP,
    [Verification_Code] [char](6) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Password_Reset] PRIMARY KEY ([Password_Reset_ID]),
    CONSTRAINT [fk_Password_Reset_Username] FOREIGN KEY ([Username]) 
        REFERENCES [dbo].[Login]([Username])
)
GO