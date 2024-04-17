DROP DATABASE IF EXISTS Night_Rider;
GO
CREATE DATABASE Night_Rider;
GO
USE Night_Rider;
GO
/******************
Create the [dbo].[Vendor] table
***************/
print ''
Print '***Create the [dbo].[Vendor] table***' 
 go


CREATE TABLE [dbo].[Vendor]
(
    [Vendor_ID] [int] IDENTITY	(100000,1) not null /*Vendor name*/,
    [Vendor_Name] [nvarchar](100) not null /*The name of the vendors company*/,
    [Vendor_Contact_Given_Name] [nvarchar](50) not null /*vendor contact person given name*/,
    [Vendor_Contact_Family_Name] [nvarchar](50) not null /*vendor contact person family name*/,
    [Vendor_Contact_Phone_Number] [nvarchar](12) not null /*vendor contact phone number*/,
    [Vendor_Contact_Email] [nvarchar](255) not null /*vendor contact email*/,
    [Vendor_Phone_Number] [nvarchar](12) not null /*vendor main phone number*/,
    [Vendor_Address] [nvarchar](50) not null /*vendor address*/,
    [Vendor_Address2] [nvarchar](50) null /*vendor address detail*/,
    [Vendor_City] [nvarchar](20) not null /*vendor city*/,
    [Vendor_State] [nvarchar](2) not null /*vendor state*/,
    [Vendor_Country] [nvarchar](3) not null /*vendor coutry*/,
    [Vendor_Zip] [nvarchar](9) not null /*vendor zip*/,
    [Preferred] [bit] DEFAULT 0 not null /*is the vendor preferred for ordering from*/,
    [Is_Active] [bit] DEFAULT 1 not null
        /*is the vendor currently in use*/

        CONSTRAINT [pk_vendor] PRIMARY KEY ([Vendor_ID]),
    CONSTRAINT [ak_vendor] UNIQUE ([Vendor_ID])
);
GO

/******************
Insert Sample Data For The  Vendor table
***************/
print ''
Print '***Insert Sample Data For The  Vendor table***' 
 go 

GO
INSERT INTO [dbo].[Vendor]
    ([Vendor_Name], [Vendor_Contact_Given_Name], [Vendor_Contact_Family_Name], [Vendor_Contact_Phone_Number], [Vendor_Contact_Email], [Vendor_Phone_Number], [Vendor_Address], [Vendor_Address2], [Vendor_City], [Vendor_State], [Vendor_Country], [Vendor_Zip])
VALUES
    ('VendorOne', 'VOneGiven', 'VOneFamily', '319-555-1111', 'contact@vendorone.com', '319-555-1112', '123 VendOne St', 'Unit 1', 'VOneCity', 'AA', 'USA', 'VOneZip'),
    ('VendorTwo', 'VTwoGiven', 'VTwoFamily', '319-555-2222', 'contact@vendortwo.com', '319-555-2222', '123 VendTwo St', 'Unit 2', 'VTwoCity', 'BB', 'USA', 'VTwoZip'),
    ('VendorThree', 'VThreeGiven', 'VThreeFamily', '319-555-3333', 'contact@vendorthree.com', '319-555-3332', '123 VendThree St', 'Unit 3', 'VThreeCity', 'CC', 'USA', 'VThreeZip'),
    ('VendorFour', 'VFourGiven', 'VFourFamily', '319-555-4444', 'contact@vendorfour.com', '319-555-4442', '123 VendFour St', 'Unit 4', 'VFourCity', 'DD', 'USA', 'VFourZip'),
    ('VendorFive', 'VFiveGiven', 'VFiveFamily', '319-555-5555', 'contact@vendorfive.com', '319-555-5552', '123 VendFive St', 'Unit 5', 'VFiveCity', 'EE', 'USA', 'VFiveZip')
;
GO
/******************
Create the [dbo].[Purchase_Order] table
***************/
print ''
Print '***Create the [dbo].[Purchase_Order] table***' 
 go
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
    CONSTRAINT 		[pk_Purchase_Order_ID] PRIMARY KEY ([Purchase_Order_ID]),
    CONSTRAINT		[fk_Vendor_ID] 	FOREIGN KEY ([Vendor_ID])  REFERENCES [dbo].[vendor]([vendor_Id])
)
GO

/******************
Insert Sample Data For The  Purchase_Order table
***************/
print '' Print '***Insert Sample Data For The  Purchase_Order table***' 
 go 
 INSERT INTO [dbo].[Purchase_Order]
		([Vendor_ID],[Purchase_Order_Date],[Delivery_Address],[Delivery_Address2],[Delivery_City],[Delivery_State],[Delivery_Country],[Delivery_Zip])
	VALUES
		(100000,'2022-05-05','123 fake street','Apt #3','Cedar Rapids','IA','USA','52002'),
		(100001,'2022-05-06','123 fake street','Apt #3','Cedar Rapids','IA','USA','52002'),
		(100002,'2022-05-07','123 fake street','Apt #3','Cedar Rapids','IA','USA','52002'),
		(100003,'2022-05-08','123 fake street','Apt #3','Cedar Rapids','IA','USA','52002'),
		(100004,'2022-05-09','123 fake street','Apt #3','Cedar Rapids','IA','USA','52002'),
		(100004,'2022-05-10','123 fake street','Apt #3','Cedar Rapids','IA','USA','52002'),
		(100003,'2022-05-11','123 fake street','Apt #3','Cedar Rapids','IA','USA','52002')

GO


/******************
Create the [dbo].[Packing_Slip] table
***************/
print ''
Print '***Create the [dbo].[Packing_Slip] table***' 
 go
CREATE TABLE [dbo].[Packing_Slip]
(
    [Packing_Slip_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Purchase_Order_ID] [int] NOT NULL,
    [Recieving_Notes] [nvarchar](256) NULL,
    [Vendor_ID] [int] NOT NULL,
    [Creation_Date] [date] DEFAULT GETDATE() NOT NULL,
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [PK_Packing_Slip] PRIMARY KEY ([Packing_Slip_ID]),
    CONSTRAINT [FK_Packing_Slip_Purchase_Order] FOREIGN KEY ([Purchase_Order_ID])
    	REFERENCES [dbo].[Purchase_Order] ([Purchase_Order_ID]),
    CONSTRAINT [FK_Packing_Slip_Vendor] FOREIGN KEY ([Vendor_ID])
    	REFERENCES [dbo].[Vendor] ([Vendor_ID])
)
go

/******************
Insert Sample Data For The  Packing_Slip table
***************/
print ''
Print '***Insert Sample Data For The  Packing_Slip table***' 
 go
INSERT INTO [dbo].[Packing_Slip]
    ([Purchase_Order_ID], [Recieving_Notes], [Vendor_ID], [Creation_Date])
VALUES
    (100000, 'All is good.', 100001, GETDATE()),
    (100001, 'One box was broken', 100002, GETDATE()),
    (100002, 'Everything was unharmed in shipping', 100002, GETDATE()),
    (100003, 'Something''s wrong i can feel it', 100003, GETDATE()),
    (100004, 'Hello!', 100004, GETDATE())
go


/******************
Create the [dbo].[Parts_Inventory] table
***************/
print ''
Print '***Create the [dbo].[Parts_Inventory] table***' 
 go


CREATE TABLE [dbo].[Parts_Inventory]
(
    [Parts_Inventory_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Part_Name] [nvarchar] (30) NOT NULL,
    [Part_Quantity] [int] NOT NULL DEFAULT 0,
    [Part_Unit_Type] [nvarchar] (50) NOT NULL,
    [Item_Description] [nvarchar] (100) NOT NULL,
    [Item_Specifications] [nvarchar] (MAX) NOT NULL,
    [Part_Photo_URL] [nvarchar] (255) NOT NULL,
    [Ordered_Qty] [int] NOT NULL DEFAULT 0,
    [Stock_Level] [int] NOT NULL DEFAULT 0,
    [Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [PK_Parts_Inventory_ID] PRIMARY KEY([Parts_Inventory_ID])
)
GO


/******************
Insert Sample Data For The  Parts_Inventory table
***************/
print ''
Print '***Insert Sample Data For The  Parts_Inventory table***' 
 go

 INSERT INTO [dbo].[Parts_Inventory] (
    
    [Part_Name], 
    [Part_Unit_Type],
    [Item_Description], 
    [Item_Specifications], 
    [Part_Photo_URL]
)
VALUES
    ( 'Fastner A', 'Pieces', 'Standard widget', 'Dimensions: 5cm x 3cm x 2cm, Material: Steel', 'https://4.imimg.com/data4/EN/PJ/MY-7251967/hex-head-bolts-500x500.jpg'),
    ( 'Bolt B', 'Pieces', 'Heavy-duty bolt', 'Thread size: M10, Length: 50mm, Material: Stainless steel', 'https://www.iqsdirectory.com/articles/bolts/types-of-bolts/bolts.jpg'),
    ( 'Grommet C', 'Pieces', 'Rubber grommet for wire protection', 'Diameter: 10mm, Material: Rubber', 'https://images.thdstatic.com/productImages/b2da35ad-9c9b-4f63-87ee-f2f920303f18/svn/everbilt-grommets-812038-64_600.jpg'),
    ( 'Cable D', 'Cord', 'Power cable with 3-prong plug', 'Length: 2 meters, Gauge: 18 AWG', 'https://images.thdstatic.com/productImages/345abf4e-1320-4c4d-bf13-812bf6b841d0/svn/syston-cable-technology-data-cables-1588-sb-bk-100-64_1000.jpg'),
    ( 'Frame E', 'Quarts', 'Aluminum mounting frame', 'Dimensions: 30cm x 20cm, Material: Aluminum', 'https://spn-sta.spinny.com/blog/20220228141808/ezgif.com-gif-maker-90-1.jpg?compress=true&quality=80&w=1200&dpr=2.6'),
    ( 'Washer B', 'Pieces', '1/8 washer', ' Material: Stainless steel', 'https://images.thdstatic.com/productImages/cddb3606-9a40-4149-a4bb-a23fe749a2f4/svn/everbilt-flat-washers-842348-64_1000.jpg'),
    ( 'Screw C', 'Pieces', 'Metal Screws', 'Diameter: 10mm, Material: Steel', 'https://m.media-amazon.com/images/I/61tO5ExJfxL.jpg'),
    ( 'Wire Stripper D', 'Unit', 'Wire Stripper', 'Length: 2 meters', 'https://m.media-amazon.com/images/I/61C-UqAIndL.jpg'),
    ( 'Nails E', 'Pounds', 'Wood Nails', 'Dimensions: 30cm x 20cm, Material: Aluminum', 'https://m.media-amazon.com/images/I/81RoBbXE7RL._AC_UF894,1000_QL80_.jpg'),
	( 'Tires A', 'Unit', 'Sedan Tires', 'Dimensions: 5cm x 3cm x 2cm, Material: rubber', 'https://m.media-amazon.com/images/I/81bkWoDhtKL.jpg'),
    ( 'Tires B', 'Unit', 'Bus Tires', 'Thread size: M10, Length: 50mm, Material: Rubber', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTtNwGpQYMQ9m0EIw1ZKM0IcwAwh17TznAv1Rtysc-nc5r7JGEc3h3IW7vf14jQIr0VGxA&usqp=CAU'),
    ( 'Axel A', 'Unit', 'Mazda 3 Rear Axel', 'Diameter: 10mm, Material: Steel', 'https://m.media-amazon.com/images/I/51Q6MN6ArnL._AC_UF894,1000_QL80_.jpg'),
    ( 'Struts', 'Unit', 'VW Bug front strut', 'Length: 2 meters, Gauge: 18 AWG', 'https://m.media-amazon.com/images/I/71XlY5CA3AL.jpg'),
    ( 'Hubcaps ', 'Unit', 'Sedan Hubcap', 'Dimensions: 30cm x 20cm, Material: Aluminum', 'https://m.media-amazon.com/images/I/613dx6sjZyL._AC_UF894,1000_QL80_.jpg');
GO

go
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
    CONSTRAINT [PK_Purchase_Order_Line_Item] PRIMARY KEY([Purchase_Order_ID], [Parts_Inventory_ID]),
    CONSTRAINT [FK_Purchase_Order_Line_Item_Parts_Inventory_ID_Parts_Inventory_Parts_Inventory_ID]
    FOREIGN KEY([Parts_Inventory_ID]) REFERENCES [dbo].[Parts_Inventory]([Parts_Inventory_ID]),
    CONSTRAINT [FK_Purchase_Order_Line_Item_Purchase_Order_ID_Purchase_Order_Purchase_Order_ID]
    FOREIGN KEY([Purchase_Order_ID]) REFERENCES [dbo].[Purchase_Order]([Purchase_Order_ID])
);
GO
/******************
Insert Sample Data For The  Purchase_Order_Line_Item table
***************/
print ''
Print '***Insert Sample Data For The  Purchase_Order_Line_Item table***' 
GO
INSERT INTO [dbo].[Purchase_Order_Line_Item]
    ([Purchase_Order_ID], [Parts_Inventory_ID], [Line_Number], [Line_Item_Name], [Line_Item_Qty],[Line_Item_Price], [Line_Item_Description])
VALUES
    (100000, 100000, 100000, 'Fastner', 2,1, 'Some Fastner'),
	(100000, 100001, 100001, 'Bolt', 2,1, 'Some Bolt'),
	(100000, 100002, 100002, 'Grommet', 2,3, 'Some Grommet'),
	(100000, 100003, 100003, 'Cable', 2,4, 'Some Cable'),
	(100000, 100004, 100004, 'Frame', 2,5, 'Some Frame'),
	(100000, 100005, 100005, 'Washer', 2,6, 'Some Washer'),
	(100001, 100006, 100001, 'Screw', 2,7, 'Some Screw'),
	(100001, 100007, 100002, 'Wire Stripper D', 2,8, 'Some Stripper'),
	(100001, 100008, 100003, 'Nails', 2,9, 'Some Nails'),
	(100001, 100009, 100004, 'Tires', 2,10, 'Some Tires'),
	(100001, 100010, 100005, 'Tires', 2,11, 'Some Tires'),
	(100001, 100011, 100006, 'Axel', 2,12, 'Some Axel'),
	(100002, 100012, 100000, 'Struts', 2,13.25, 'Some Struts'),
	(100002, 100013, 100001, 'Hubcaps', 2,14.25, 'Some Hubcaps'),
	(100003, 100013, 100003, 'Hubcaps', 2,15.25, 'Some Hubcaps'),
	(100003, 100001, 100000, 'Bolt', 2,16.25, 'Some Bolt'),
	(100003, 100002, 100001, 'Grommet', 2,17.25, 'Some Grommet'),
	(100003, 100003, 100002, 'Cable', 2,21.25, 'Some Cable'),
	(100004, 100004, 100000, 'Frame', 2,22.25, 'Some Frame'),
	(100004, 100005, 100001, 'Washer', 2,23.25, 'Some Washer'),
	(100005, 100006, 100000, 'Screw', 2,24.25, 'Some Screw'),
	(100005, 100007, 100001, 'Wire Stripper D', 2,25.25, 'Some Stripper'),
	(100006, 100008, 100000, 'Nails', 2,26.25, 'Some Nails'),
	(100006, 100009, 100001, 'Tires', 2,27.25, 'Some Tires'),
	(100006, 100010, 100002, 'Tires', 2,31.25, 'Some Tires'),
	(100006, 100011, 100003, 'Axels', 2,32.25, 'Some Axels'),
	(100006, 100012, 100004, 'Struts', 2,33.25, 'Some Struts'),
	
	(100006, 100000, 100006, 'Fastner', 2,35.25, 'Some Fastener')
	
    
GO



/******************
Create the [dbo].[Route] table
***************/
print ''
Print '***Create the [dbo].[Route] table***' 
go
CREATE TABLE [dbo].[Route]
(
    [Route_ID] [int] IDENTITY(100000,1),
    [Route_Name] [nvarchar](255) NOT NULL,
    [Route_Start_Time] [datetime] NOT NULL,
    [Route_Cycle] [time] NOT NULL,
    [Route_End_Time] [datetime] NOT NULL,
    [Days_Of_Service] [char](7) NOT NULL DEFAULT '0000000',
    [Is_Active] [bit] NOT NULL DEFAULT(1),
    CONSTRAINT [PK_Route] PRIMARY KEY([Route_ID])
);
go
/******************
Insert Sample Data For The  Route table
***************/
print ''
Print '***Insert Sample Data For The  Route table***' 
go
INSERT INTO [dbo].[Route]
    ([Route_Name], [Route_Start_Time], [Route_Cycle], [Route_End_Time], [Days_Of_Service])
VALUES
    ('Cedar Rapids Northeast', '1900-01-01 05:00:00', '01:30:00', '1900-01-01 20:00:00', '0111100'),
    ('Cedar Rapids Southwest', '1900-01-01 05:00:00', '02:00:00', '1900-01-01 20:00:00', '0111100'),
    ('Hiawatha', '1900-01-01 06:30:00', '01:00:00', '1900-01-01 19:00:00', '0111111'),
    ('Marion', '1900-01-01 09:00:00', '00:30:00', '1900-01-01 18:30:00', '0010100'),
    ('Center Point', '1900-01-01 05:30:00', '01:00:00', '1900-01-01 22:00:00', '0111110');
go

/******************
Create the [dbo].[Stop] table
***************/
print ''
Print '***Create the [dbo].[Stop] table***' 
 go


CREATE TABLE [dbo].[Stop]
(
    [Stop_ID] [int] NOT NULL IDENTITY(100000, 1),
    [Street_Address] [nvarchar](255) NOT NULL,
    [Zip_Code] [nvarchar](5) NOT NULL,
    [Latitude] [decimal](8,6) NOT NULL,
    [Longitude] [decimal](9,6) NOT NULL,
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [pk_StopID] PRIMARY KEY([Stop_ID])
)
go
/******************
Insert Sample Data For The  Stop table
***************/
print ''
Print '***Insert Sample Data For The  Stop table***' 
 go
INSERT INTO [dbo].[Stop]
    ([Street_Address], [Zip_Code], [Latitude], [Longitude])
VALUES
    ( 'Lindale Mall', '52302', 42.027780, -91.629940),
    ( 'Downtown CR', '52402', 41.978050, -91.669860),
    ( 'Marcus Cedar Rapids Cinema', '52302', 42.032038, -91.655243),
    ( 'Guthridge Park, Hiawatha', '52233', 42.040838, -91.681471),
    ( 'Mount Trashmore', '52402', 41.96175086599706, -91.65086473447506);
go


/******************
Create the [dbo].[Route_Stop] table
***************/
print ''
Print '***Create the [dbo].[Route_Stop] table***' 
go
CREATE TABLE [dbo].[Route_Stop]
(
    [Route_ID] [int] NOT NULL,
    [Stop_ID] [int] NOT NULL,
    [Route_Stop_Number] [int] NOT NULL,
    [Start_Offset] [TIME] NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT(1),
    CONSTRAINT [PK_Route_Stop] PRIMARY KEY ([Route_ID], [Route_Stop_Number]),
    CONSTRAINT [FK_Route_Stop_Route_ID_Route_Route_ID]
    FOREIGN KEY ([Route_ID]) REFERENCES [dbo].[Route]([Route_ID]),
    CONSTRAINT [FK_Route_Stop_Stop_ID_Stop_Stop_ID]
    FOREIGN KEY ([Stop_ID]) REFERENCES [dbo].[Stop]([Stop_ID])
);
go
/******************
Insert Sample Data For The  Route_Stop table
***************/
print ''
Print '***Insert Sample Data For The  Route_Stop table***'
INSERT INTO [dbo].[Route_Stop]
    ([Route_ID], [Stop_ID], [Route_Stop_Number], [Start_Offset])
VALUES
    (100000, 100000, 1, '00:00:00.0000000'),
    (100000, 100001, 2, '00:15:00.0000000'),
    (100000, 100002, 3, '00:30:00.0000000'),
    (100001, 100003, 1, '00:00:00.0000000'),
    (100001, 100004, 2, '00:30:00.0000000')
go

/******************
Create the [dbo].[Vehicle_Type] table
***************/
print ''
Print '***Create the [dbo].[Vehicle_Type] table***' 
 go


CREATE TABLE [dbo].[Vehicle_Type]
(
    [Vehicle_Type_ID] [nvarchar](50) not null /*a type of vehicle, like bus or van*/,
    [Is_Active] [bit] DEFAULT 1 not null
    CONSTRAINT [pk_vehicleType] PRIMARY KEY([Vehicle_Type_ID])
);
GO
/******************
Insert Sample Data For The  Vehicle_Type table
***************/
print ''
Print '***Insert Sample Data For The  Vehicle_Type table***' 
 go

INSERT INTO [dbo].[Vehicle_Type]
    ([Vehicle_Type_ID])
VALUES
    ('City Bus'),
    ('School Bus'),
    ('Lift Bus'),
    ('Work Truck'),
    ('Van'),
    ('Car'),
    ('Truck')
;
GO

/******************
Create the [dbo].[Vehicle_Model] table
***************/
CREATE TABLE [dbo].[Vehicle_Model]
(
    [Vehicle_Model_ID] [int] IDENTITY(100000, 1) NOT NULL,
    [Vehicle_Type_ID] [nvarchar] (50) NULL,
    [Max_Passengers] [int] NOT NULL,
    [Make] [nvarchar] (255) NOT NULL,
    [Name] [nvarchar] (255) NOT NULL,
    [Year] [int] NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [PK_Vehicle_Model_ID] PRIMARY KEY([Vehicle_Model_ID]),
    CONSTRAINT [FK_Vehicle_Model_Vehicle_Type_ID_Vehicle_Type_Vehicle_Type_ID]
      FOREIGN KEY([Vehicle_Type_ID]) REFERENCES [Vehicle_Type]([Vehicle_Type_ID])
)
GO

/******************
Insert Sample Data For The  Vehicle_Model table
***************/
print ''
Print '***Insert Sample Data For The  Vehicle_Model table***' 
 go
INSERT INTO [dbo].[Vehicle_Model]
    (
    [Max_Passengers],
    [Make],
    [Name],
    [Year]
    )
VALUES
    (5, 'Toyota', 'Camry', 2023),
    (7, 'Honda', 'Accord', 2022),
    (4, 'Ford', 'Escape', 2021),
    (2, 'Chevrolet', 'Spark', 2020),
    (8, 'Nissan', 'Altima', 2019);
GO

/******************
Create the [dbo].[Vehicle] table
***************/
print ''
Print '***Create the [dbo].[Vehicle] table***' 
 go

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
    [Maintenance_Notes] [nvarchar](MAX),
    [Rental] [bit] NOT NULL DEFAULT 0,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [PK_Vehicle] PRIMARY KEY ([VIN]),
    CONSTRAINT [AK_VIN] UNIQUE ([VIN]),
    -- Used ak_... for ones that are 
    --unique like Jims example for dotnet2
    CONSTRAINT [AK_Vehicle_Number] UNIQUE ([Vehicle_Number]),
    CONSTRAINT [FK_Vehicle_Vehicle_Type] FOREIGN KEY ([Vehicle_Type_ID]) 
        REFERENCES [Vehicle_Type]([Vehicle_Type_ID]),
    CONSTRAINT [FK_Vehicle_Vehicle_Model] FOREIGN KEY ([Vehicle_Model_ID])
        REFERENCES [dbo].[Vehicle_Model]([Vehicle_Model_ID])
)
GO

/******************
Insert Sample Data For The  Vehicle table
***************/
print ''
Print '***Insert Sample Data For The  Vehicle table***' 
 go

INSERT INTO [dbo].[Vehicle]
    ([VIN], [Vehicle_Number], [Vehicle_Mileage], [Vehicle_License_Plate],
    [Vehicle_Model_ID], [Vehicle_Type_ID], [Date_Entered], [Description], [Maintenance_Notes], [Rental], [Is_Active]
    )
VALUES
    ('1HGCM82633A123456', 'VH123', 50000, 'ABC123', 100000, 'City Bus', '2024-01-15', 'Description', 'Last oil change at 44,250 miles.', 0, 1),
    ('5XYZH4AG4JH123456', 'VH456', 60000, 'XYZ789', 100001, 'School Bus', '2024-02-15', 'Description', 'The tires were replaced at 55,028 miles', 1, 1),
    ('JM1BK32F781234567', 'VH789', 75000, 'MJK456', 100002, 'Van', '2024-03-15', 'Description', 'New rear shocks at 74,000 miles', 0, 3),
    ('WAUZZZ4G6BN123456', 'VH101', 40000, 'WAU789', 100003, 'Truck', '2024-04-15', 'Description', 'New dashboard cluster at 40,000 miles', 1, 4),
    ('1C4RJFAG5FC123456', 'VH202', 55000, 'JFA567', 100004, 'City Bus', '2024-05-15', 'Description', 'The driver side headlight is scratched up, should be replaced soon.', 1, 1); 
GO




/******************
Create the [dbo].[Employee] table
***************/
print ''
Print '***Create the [dbo].[Employee] table***' 
 go


CREATE TABLE [dbo].[Employee] (
	[Employee_ID]	[int] IDENTITY(100000, 1) NOT NULL,
	[Given_Name] 	[nvarchar](50) 	NOT NULL,
	[Family_Name]	[nvarchar](50) 	NOT NULL,
	[DOB]			[datetime]		NOT NULL,
	[Address]		[nvarchar](50) 	NOT NULL,
	[Address2]		[nvarchar](50) 	NULL,
	[City]			[nvarchar](20) 	NOT NULL,
	[State]			[nvarchar](2) 	NOT NULL,
	[Country]		[nvarchar](3) 	NOT NULL,
	[Zip]			[nvarchar](9) 	NOT NULL,
	[Phone_Number]	[nvarchar](20) 	NOT NULL,
	[Email]			[nvarchar](50) 	NOT NULL,
	[Position]		[nvarchar](20) 	NOT NULL,
	[Is_Active]		[bit]			NOT NULL DEFAULT 1,
	CONSTRAINT [pk_Employee] PRIMARY KEY([Employee_ID])
)
GO

/******************
Insert Sample Data For The  Employee table
***************/
print ''
Print '***Insert Sample Data For The  Employee table***' 
 go
INSERT INTO [dbo].[Employee]
    ([Given_Name],[Family_Name],[DOB],[Address],[City],[State],[Country],[Zip],
    [Phone_Number],[Email],[Position]
    )
VALUES(
        'John', 'Smith', '2006-11-01',
        '132 Nowhere Ave', 'Nottingham', '', 'GBR',
        '', '11575011049', 'John@company.com', 'Mechanic'),
    (
        'Dylan', 'Linkelvetch', '1953-02-07',
        '158 Real Pl', 'Iowa City', 'IA', 'USA',
        '52245', '3191231234', 'Dylan@company.com', 'Driver'),
    (
        'Gunter', 'Schneider', '1988-04-01',
        '240 Root St', 'Berlin', '', 'DEU',
        '', '1231231234', 'Gunter@company.com', 'Fleet Admin'),
    (
        'Marissa', 'Graham', '2001-02-08',
        '512 Nix ln ', 'Juno', 'AK', 'USA',
        '99801', '9879871234', 'Marissa@company.com', 'Maintenance'),
    (
        'Auri', 'Koskinen', '1982-11-01',
        '007 Secret St', 'Helsinki', '', 'FIN',
        '', '0095542367', 'Auri@company.com', 'Mechanic'),
    (
        'Linda', 'Flynn', '1968-10-31',
        '879 Perry Ave', 'Danville', 'CT', 'USA',
        '06080', '3194105910', 'Linda@company.com', 'PositionName'),
    (
        'Francis', 'Polesmith', '1984-01-04',
        '51 Joust Ln', 'Pierre', 'SD', 'USA',
        '57501', '4191023103', 'Francis@company.com', 'PositionName'),
    (
        'Theseus', 'Slayer', '1985-03-31',
        '151 Antimino Pl', 'Athens', '', 'GRC',
        '', '5710150113', 'Theseus@company.com', 'PositionName'),
    (
        'Trisha', 'Hallows', '1988-06-06',
        '132 Nowhere Ave', 'Baton Rouge', 'LA', 'USA',
        '70801', '22575011049', 'Trisha@company.com', 'PositionName'),
    (
        'Justin', 'Time', '2002-12-25',
        '510 Clock Circle', 'Hill Valley', 'CA', 'USA',
        '91905', '5961924091', 'Justin@company.com', 'PositionName'
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
    [Role_ID] 			[nvarchar](25) NOT NULL,
	[Role_Description] 	[nvarchar](255) NOT NULL DEFAULT '',
    [Is_Active] 		[bit] NOT NULL DEFAULT 1,
    CONSTRAINT 		[pk_Role_ID] PRIMARY KEY ([Role_ID])
)
GO
/******************
Insert Sample Data For The  Role table
***************/
print '' Print '***Insert Sample Data For The  Role table***' 
GO
 Insert into [dbo].[Role] ([Role_ID], [Role_Description]) VALUES 
	('Admin', 'Manages entire application, typically aids in system setup.'),
	('FleetAdmin', 'Manages the fleet'),
	('Mechanic', 'Fixes the vehicles'),
	('Maintenance', 'Routine maintenance work that doesnt require mechanic'),
	('PartsPerson', 'An invetory specialist, that is the go to for any parts for vehicles'),
    ('Dispatcher', 'The controller of the fleet. Assigns tasks, routes drivers, and ensures services run smoothly.')
GO


/******************
Create the [dbo].[Employee_Role] table
***************/
print ''
Print '***Create the [dbo].[Employee_Role] table***' 
 go

CREATE TABLE [dbo].[Employee_Role]
(
    [Employee_ID] [int] NOT NULL,
    [Role_ID] [nvarchar](25) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [fk_Employee_Role_EmployeeID] FOREIGN KEY([Employee_ID])
		REFERENCES [dbo].[Employee]([Employee_ID]),
    CONSTRAINT [fk_Employee_Role_RoleID] FOREIGN KEY([Role_ID])
		REFERENCES [dbo].[Role]([Role_ID]),
    CONSTRAINT [cpk_Employee_Role_RoleID] PRIMARY KEY([Employee_ID], [Role_ID])
)
GO


/******************
Insert Sample Data For The  Employee_Role table
***************/
 print ''
print '*** inserting Employee_roles records ***'
GO
INSERT INTO [dbo].[Employee_Role]
    ([Employee_ID], [Role_ID])
VALUES
    (100000, 'Admin'),
    (100000, 'Dispatcher'),
    (100001, 'FleetAdmin'),
    (100001, 'Dispatcher'),
    (100002, 'Mechanic'),
    (100003, 'Maintenance'),
    (100004, 'PartsPerson'),
    (100005, 'Mechanic'),
    (100006, 'Maintenance'),
    (100007, 'PartsPerson'),
    (100008, 'Mechanic'),
    (100009, 'Maintenance')
GO


/******************
Create the [dbo].[Driver_License_Class] table
***************/
print ''
Print '***Create the [dbo].[Driver_License_Class] table***' 
 go


CREATE TABLE [dbo].[Driver_License_Class]
(
    [Driver_License_Class_ID] [nvarchar](6) NOT NULL DEFAULT 'c',
    [Max_Passenger_Count] [int] NOT NULL DEFAULT 0,
    [Is_Active] [bit] NOT NULL DEFAULT 1
        CONSTRAINT [pk_Driver_License_Class] PRIMARY KEY ([Driver_License_Class_ID])
)
GO

/******************
Insert Sample Data For The  Driver_License_Class table
***************/
GO
INSERT INTO [dbo].[Driver_License_Class]
    ([Driver_License_Class_ID],[Max_Passenger_Count],[Is_Active])
VALUES
    ('D', 6, 1),
    ('CDL-P', 15, 1),
    ('CDL-S', 25, 1),
    ('E', 5, 1),
    ('CDL-T', 4, 1)
GO

/******************
Create the [dbo].[Driver] table
***************/
print ''
Print '***Create the [dbo].[Driver] table***' 
 go


CREATE TABLE [dbo].[Driver]
(
    [Employee_ID] [int] NOT NULL,
    [Driver_License_Class_ID] [nvarchar](6) NOT NULL DEFAULT 'c',
    [Is_Active] [bit] NOT NULL DEFAULT 1
        CONSTRAINT [fk_Driver_Employee_ID] FOREIGN KEY([Employee_ID])
		REFERENCES [dbo].[Employee] ([Employee_ID]),
    CONSTRAINT [fk_Driver_Driver_License_Class_ID] FOREIGN KEY([Driver_License_Class_ID])
		REFERENCES [dbo].[Driver_License_Class] ([Driver_License_Class_ID]),
    CONSTRAINT [cpk_Driver] PRIMARY KEY ([Employee_ID])
)
GO

/******************
Insert Sample Data For The  Driver table
***************/
print ''
Print '***Insert Sample Data For The  Driver table***' 
 go
print ''
print '*** Inserting Sample Data for Driver ***'
GO
INSERT INTO [dbo].[Driver]
    ([Employee_ID], [Driver_License_Class_ID], [Is_Active])
VALUES
    (100000, 'D', 1),
    (100001, 'CDL-P', 1),
    (100002, 'CDL-S', 1),
    (100003, 'CDL-T', 1),
    (100004, 'E', 1)
GO

/******************
Create the [dbo].[Schedule] table
***************/
print ''
Print '***Create the [dbo].[Schedule] table***' 
 go


CREATE TABLE [dbo].[Schedule]
(
    [Schedule_ID] [nvarchar](50) NOT NULL,
    [Driver_ID] [int] NOT NULL,
    [Week_Days] [nvarchar](7) NOT NULL DEFAULT '0000000' ,
    [Start_Time] [datetime] NOT NULL,
    [End_Time] [datetime] NOT NULL,
    [Start_Date] [datetime] NOT NULL,
    [End_Date] [datetime] NULL,
    [Notes] [nvarchar](255) NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [PK_Schedule] PRIMARY KEY ([Schedule_ID]),
    CONSTRAINT [Fk_Schedule_Driver] FOREIGN KEY ([Driver_ID])
        REFERENCES [Driver]([Employee_ID])
)
GO
/******************
Insert Sample Data For The  Schedule table
***************/
print ''
Print '***Insert Sample Data For The  Schedule table***' 
 go
INSERT INTO [dbo].[Schedule]
    ([Schedule_ID], [Driver_ID],[Week_Days],
    [Start_Time],[End_Time],[Start_Date], [End_Date],[Notes],[Is_Active]
    )
VALUES
    ('SCH1', 100000, '1111100', '08:00:00', '17:00:00', '2024-01-01', NULL, 'Notes here', 1),
    ('SCH2', 100001, '0001111', '09:00:00', '18:00:00', '2024-02-01', '2024-02-15', 'Notes here', 1),
    ('SCH3', 100002, '111001', '10:00:00', '19:00:00', '2024-03-01', '2024-03-15', 'Notes...', 0),
    ('SCH4', 100003, '0101010', '07:30:00', '16:30:00', '2024-04-01', '2024-04-15', 'Notes...', 1),
    ('SCH5', 100004, '1111111', '12:00:00', '21:00:00', '2024-05-01', '2024-05-15', 'Notes...', 1);
Go



/******************
Create the [dbo].[Route_Assignment] table
***************/
print ''
Print '***Create the [dbo].[Route_Assignment] table***' 
go
CREATE TABLE [dbo].[Route_Assignment]
(
    [Assignment_ID] [int] IDENTITY(100000, 1),
    [Driver_ID] [int] NOT NULL,
    [Route_ID] [int] NOT NULL,
    [VIN] [nvarchar](17) NOT NULL,
    [Date_Assignment_Started] [datetime] NOT NULL,
    [Date_Assignment_Ended] [datetime] NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [PK_Route_Assignment] PRIMARY KEY([Assignment_ID]),
    CONSTRAINT [FK_Route_Assignment_Driver_ID_Driver_Driver_ID]
    FOREIGN KEY([Driver_ID]) REFERENCES [dbo].[Driver]([Employee_ID]),
    CONSTRAINT [FK_Route_Assignment_Route_ID_Route_Route_ID]
    FOREIGN KEY([Route_ID]) REFERENCES [dbo].[Route]([Route_ID]),
    CONSTRAINT [FK_Route_Assignment_VIN_Vehicle_VIN]
    FOREIGN KEY([VIN]) REFERENCES [dbo].[Vehicle]([VIN])
);
go

/******************
Insert Sample Data For The  Route_Assignment table
***************/
print ''
Print '***Insert Sample Data For The  Route_Assignment table***' 
go
INSERT INTO [dbo].[Route_Assignment]
    ([Driver_ID], [Route_ID], [VIN], [Date_Assignment_Started], [Date_Assignment_Ended])
VALUES
    (100000, 100000, '1HGCM82633A123456', '2023-11-01', NULL),
    (100001, 100000, '5XYZH4AG4JH123456', '2023-12-15', '2024-01-05'),
    (100002, 100001, 'JM1BK32F781234567', '2024-01-20', NULL),
    (100003, 100002, 'WAUZZZ4G6BN123456', '2022-04-23', '2023-02-19'),
    (100004, 100003, '1C4RJFAG5FC123456', '2023-07-12', '2023-09-06')
go


/******************
Create the [dbo].[Safety_Report] table
***************/
print ''
Print '***Create the [dbo].[Safety_Report] table***' 
 go


CREATE TABLE [dbo].[Safety_Report]
(
    [Safety_Report_ID] [int] IDENTITY (100000,1) not null
    /*report identifier*/,
    [Employee_ID] [int] not null /*Employee.Employee_ID	id of employee filing report*/,
    [Date] [datetime] not null /*date when report is filed*/,
    [Time_Of_Event] [datetime] not null/*time of the event*/,
    [Affected_Party] [nvarchar](100) not null /*yourself, coworker, passenger, civilian*/,
    [Description] [nvarchar](1000) null /*description of the event*/,
    [Result_In_Injury] [bit] DEFAULT 0 not null	/*did this event result in an injury?*/,
    [Is_Active] [bit] DEFAULT 1 not null
        /*is the report still active?*/

        CONSTRAINT [pk_safetyReport] PRIMARY KEY ([Safety_Report_ID]),
    CONSTRAINT [ak_safetyReport] UNIQUE ([Safety_Report_ID]),
    CONSTRAINT [fk_safetyReport_employee] FOREIGN KEY([Employee_ID]) 
    REFERENCES [dbo].[Employee]([Employee_ID])
);
GO

/******************
Insert Sample Data For The  Safety_Report table
***************/
print ''
Print '***Insert Sample Data For The  Safety_Report table***' 
 go

INSERT INTO [dbo].[Safety_Report]
    ([Employee_ID], [Date], [Time_Of_Event], [Affected_Party], [Description])
VALUES
    (100000, '2023-01-29', '2023-01-28', 'Passenger', 'Fell out door'),
    (100001, '2023-01-29', '2023-01-28', 'Pedestrian', 'Hit by bus'),
    (100001, '2023-01-29', '2023-01-28', 'Passenger', 'Hit other passenger'),
    (100003, '2023-01-29', '2023-01-28', 'Passenger', 'Fell down aisle'),
    (100005, '2023-01-29', '2023-01-28', 'Self', 'Tripped on steps')
;
GO

/******************
Create the [dbo].[Refuel_Log] table
***************/
print ''
Print '***Create the [dbo].[Refuel_Log] table***' 
 go


CREATE TABLE [dbo].[Refuel_Log]
(
    [Refuel_Log_ID] [int] IDENTITY	(100000,1) not null
    /*generated id for refuel*/,
    [Driver_ID] [int] null
    /*Driver.Employee_ID driver that purchased the fuel*/,
    [VIN] [nvarchar](17) not null
    /*Vehicle.VIN vehicle the fuel was put in*/,
    [Date_Time] [datetime] DEFAULT GETDATE() not null
    /*auto entry as datetime.now date fuel was purchased*/,
    [Mileage] [int] not null
    /*mileage of the vehicle when fueled*/,
    [Fuel_Quantity] [int] not null
    /*amount of fuel purchased*/,
    [Fuel_Price_Per_Gal] [smallmoney] not null
    /*price of the fuel*/,
    [Total_Sale] [smallmoney] not null
    /*sale amount for the fuel*/,
    [Notes] [nvarchar](250) null
    /*notes from the driver*/,
    [Is_Active] [bit] DEFAULT 1 not null
        /*Active status of this refuel log*/

        CONSTRAINT [pk_refuelLog] PRIMARY KEY ([Refuel_Log_ID]),
    CONSTRAINT [ak_refuelLog] UNIQUE ([Refuel_Log_ID]),
    CONSTRAINT [fk_refuelLog_employee] FOREIGN KEY([Driver_ID])
        REFERENCES [dbo].[Employee]([Employee_ID]),
    CONSTRAINT [fk_refuelLog_vehicle] FOREIGN KEY([VIN])
        REFERENCES [dbo].[Vehicle]([VIN])
);
GO

/******************
Insert Sample Data For The  Refuel_Log table
***************/
print ''
Print '***Insert Sample Data For The  Refuel_Log table***' 
 go

INSERT INTO [dbo].[Refuel_Log]
    ([Driver_ID], [VIN], [Date_Time], [Mileage], [Fuel_Quantity], [Fuel_Price_Per_Gal], [Total_Sale], [Notes])
VALUES
    (100002, '1HGCM82633A123456', '2023-01-28', 231254, 10, 2.8, 28, 'Notes'),
    (100002, '5XYZH4AG4JH123456', '2023-01-28', 231254, 10, 2.8, 28, 'Notes'),
    (100003, 'JM1BK32F781234567', '2023-01-28', 231254, 10, 2.8, 28, 'Notes'),
    (100000, 'WAUZZZ4G6BN123456 ', '2023-01-28', 231254, 10, 2.8, 28, 'Notes'),
    (100001, '1C4RJFAG5FC123456', '2023-01-28', 231254, 10, 2.8, 28, 'Notes')
;
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
    CONSTRAINT [PK_Service_Type] PRIMARY KEY([Service_Type_ID]),
    CONSTRAINT [Service_Type_ID] UNIQUE([Service_Type_ID])
)
GO


/******************
Insert Sample Data For The  Service_Type table
***************/
print ''
Print '***Insert Sample Data For The  Service_Type table***' 
go
print ''
Print '***Create the [dbo].[Service_Type] table***' 
go
INSERT INTO [dbo].[Service_Type]
    ([Service_Type_ID], [Service_Description])
VALUES
    ('Oil Change', 'Change the vehicles oil per the manufacturers specification.'),
    ('All Tire Change', 'Change all tires on the vehicle.'),
    ('Tire Rotation', 'Rotate all tires per the manufacturers instructions.'),
    ('Windshield Replacement', 'Replace the windshield with OEM or better per the manufacturers specification.'),
    ('Troubleshooting', 'Troubleshoot the issue described in the Service Notes.')
GO


/******************
Create the [dbo].[Maintenance_Schedule] table
***************/
print ''
Print '***Create the [dbo].[Maintenance_Schedule] table***' 
 go


CREATE TABLE [dbo].[Maintenance_Schedule]
(
    [Maintenance_Schedule_ID] [int] NOT NULL,
    [Vehicle_Model_ID] [int] NOT NULL,
    [Service_Type_ID] [nvarchar] (256) NOT NULL,
    [Frequency_In_Months] [int] NOT NULL,
    [Frequency_In_Miles] [int]                     ,
    [Is_Completed] [bit] NOT NULL,
    [Active] [bit] NOT NULL,
    CONSTRAINT [FK_Maintenance_Schedule_Vehicle_Model_id] FOREIGN KEY([Vehicle_Model_ID])
    	REFERENCES [dbo].[Vehicle_Model]([Vehicle_Model_ID]),
    CONSTRAINT [FK_Maintenance_Schedule_Service_Type_ID] FOREIGN KEY ([Service_Type_ID])
        REFERENCES [dbo].[Service_Type]([Service_Type_ID]) ON UPDATE CASCADE,
    CONSTRAINT [PK_Maintenance_Schedule_ID] PRIMARY KEY([Maintenance_Schedule_ID])
)
GO

/******************
Insert Sample Data For The  Maintenance_Schedule table
***************/
print ''
print '*** maintenance schedule test records ***'
GO
INSERT INTO [dbo].[Maintenance_Schedule]
    (
    [Maintenance_Schedule_ID],
    [Vehicle_Model_ID],
    [Service_Type_ID],
    [Frequency_In_Months],
    [Frequency_In_Miles],
    [Is_Completed],
    [Active]
    )
VALUES
    (100001, 100001, 'Oil Change', 6, 5000, 0, 1),
    (100002, 100001, 'All Tire Change', 12, 10000, 1, 1),
    (100003, 100002, 'Tire Rotation', 18, NULL, 0, 1),
    (100004, 100003, 'Windshield Replacement', 12, NULL, 0, 1),
    (100005, 100002, 'Troubleshooting', 6, NULL, 1, 1);
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
    [Service_Type_ID] [nvarchar] (256) NOT NULL,
    [Created_By_Employee_ID] [int] NOT NULL,
    [Serviced_By_Employee_ID] [int],
    [Date_Started] [datetime] NOT NULL,
    [Date_Finished] [datetime] NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    [Critical_Issue] [bit] NOT NULL DEFAULT 0,
    CONSTRAINT [FK_Service_Order_Vehicle] FOREIGN KEY([VIN])
        REFERENCES [dbo].[Vehicle] ([VIN]),
    CONSTRAINT [FK_Service_Order_Service_Type] FOREIGN KEY([Service_Type_ID])
        REFERENCES [dbo].[Service_Type] ([Service_Type_ID]) ON UPDATE CASCADE,
    CONSTRAINT [FK_Service_Order_Employee] FOREIGN KEY([Created_By_Employee_ID])
        REFERENCES [dbo].[Employee] ([Employee_ID]),
    CONSTRAINT [FK_Service_Order_Employee_2] FOREIGN KEY([Serviced_By_Employee_ID])
        REFERENCES [dbo].[Employee] ([Employee_ID]),
    CONSTRAINT [CPK_Service_Order] PRIMARY KEY([Service_Order_ID], [Service_Order_Version])
)
GO

/******************
Insert Sample Data For The  Service_Order table
***************/
print ''
Print '***Insert Sample Data For The  Service_Order table***' 
 go
INSERT INTO [dbo].[Service_Order]
    ([Service_Order_ID], [Service_Order_Version], [VIN], [Service_Type_ID], [Created_By_Employee_ID], [Date_Started], [Date_Finished])
VALUES
    (100000, 1, '1HGCM82633A123456', 'Oil Change', 100001, '2024-01-25 10:00:00', '2024-01-25 12:00:00'),
    (100001, 2, '5XYZH4AG4JH123456', 'All Tire Change', 100002, '2024-01-24 14:30:00', '2024-01-24 16:45:00'),
    (100002, 1, 'JM1BK32F781234567', 'Tire Rotation', 100003, '2024-01-23 09:15:00', '2024-01-23 11:00:00'),
    (100003, 3, 'WAUZZZ4G6BN123456', 'Windshield Replacement', 100004, '2024-01-22 15:00:00', '2024-01-22 17:30:00'),
    (100004, 2, '1C4RJFAG5FC123456', 'Troubleshooting', 100005, '2024-01-21 08:45:00', '2024-01-21 10:15:00')
GO


/******************
Create the [dbo].[Service_Line_Item] table
***************/
print ''
Print '***Create the [dbo].[Service_Line_Item] table***' 
 go


CREATE TABLE [dbo].[Service_Line_Item]
(
    [Service_Order_ID]            [int]         NOT NULL,
    [Service_Order_Version]       [int]         NOT NULL,
    [Parts_Inventory_ID]          [int]         NOT NULL,
    [Quantity]                    [int]         NOT NULL,
    CONSTRAINT [FK_Service_Line_Item_Parts_Inventory_ID] FOREIGN KEY([Parts_Inventory_ID])
        REFERENCES [dbo].[Parts_Inventory] ([Parts_Inventory_ID]),
    CONSTRAINT [FK_Service_Line_Item_Service_Order_ID] FOREIGN KEY([Service_Order_ID], [Service_Order_Version])
        REFERENCES [dbo].[Service_Order] ([Service_Order_ID], [Service_Order_Version]),
    CONSTRAINT [CPK_Service_Line_Item] PRIMARY KEY([Service_Order_ID], [Parts_Inventory_ID])
)
GO


/******************
Insert Sample Data For The  Service_Line_Item table
***************/
print '' Print '***Insert Sample Data For The  Service_Line_Item table***' 
 go 
 INSERT INTO [dbo].[Service_Line_Item] 
    ([Service_Order_ID], [Service_Order_Version], [Parts_Inventory_ID], [Quantity])
VALUES
    (100000, 1, 100000, 2),
    (100001, 2, 100001, 3),
    (100002, 1, 100002, 1),
    (100003, 3, 100003, 4),
    (100004, 2, 100004, 5)
GO




/******************
Create the [dbo].[Service_Line] table
***************/
print ''
Print '***Create the [dbo].[Service_Line] table***' 
 go


CREATE TABLE [dbo].[Service_Line]
(


    [Service_Line_ID] [int] not null	
,
    [Service_Line_Item_ID] [int] not null unique	
,
    CONSTRAINT [PK_Service_Line] PRIMARY KEY ([Service_Line_ID],[Service_Line_Item_ID])
,
    CONSTRAINT [AK_Service_Line_Item_ID1] UNIQUE([Service_Line_Item_ID])
/*,CONSTRAINT [fk_Service_Line_Service_Order0] foreign key ([Service_Line_ID]) references [Service_Order]([Service_Order_ID])*/
,
    -- CONSTRAINT [fk_Service_Line_Service_Line_Item1] foreign key ([Service_Line_Item_ID]) references [Service_Line_Item]([Service_Line_Item_ID])

)
go

/******************
Insert Sample Data For The  Service_Line table
***************/
print ''
Print '***Insert Sample Data For The  Service_Line table***' 
 go

/******************
Create the [dbo].[Special_Service_Order] table
***************/
print ''
Print '***Create the [dbo].[Special_Service_Order] table***' 
 go


CREATE TABLE [dbo].[Special_Service_Order]
(
    [Special_Service_Order_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Service_Order_ID] [int] NOT NULL,
    [Service_Order_Version] [int] NOT NULL,
    [Event_Description] [nvarchar](500) NOT NULL,
    [Priority] [nvarchar](500) NOT NULL,
    [Is_Active] [bit] NOT NULL,
    CONSTRAINT [PK_Special_Service_Order] PRIMARY KEY ([Special_Service_Order_ID]),
    CONSTRAINT [fk_Special_Service_Order_Service_order] FOREIGN KEY ([Service_Order_ID],[Service_Order_Version]) 
        REFERENCES [dbo].[Service_order]([Service_Order_ID],[Service_Order_Version])
)
go

/******************
Insert Sample Data For The  Special_Service_Order table
***************/
print ''
Print '***Insert Sample Data For The  Special_Service_Order table***' 
 go


INSERT INTO [dbo].[Special_Service_Order]
    (
        [Service_Order_ID], 
        [Service_Order_Version],
        [Event_Description],
        [Priority],
        [Is_Active]
    )
VALUES
    (100000, 1, 'Hit a Tree', "0", 1),
    (100001, 2, 'Hit a Bus', "1", 1)
GO

/******************
Create the [dbo].[Special_Inspection] table
***************/
print ''
Print '***Create the [dbo].[Special_Inspection] table***' 
 go


CREATE TABLE [dbo].[Special_Inspection]
(


    [Special_Inspection_ID] [int] IDENTITY(100000,1) not null	
,
    [Special_Service_Order_ID] [int] not null	
,
    [Inspection_Description] [nvarchar](500) not null	
,
    [Date] [datetime] not null	
,
    [Employee_ID] [int] not null	
,
    [Is_Active] [bit] not null	
,
    CONSTRAINT [PK_Special_Inspection] PRIMARY KEY ([Special_Inspection_ID])
,
    CONSTRAINT [fk_Special_Inspection_Special_Service_order0] foreign key ([Special_Service_Order_ID]) references [Special_Service_order]([Special_Service_Order_ID])
,
    CONSTRAINT [fk_Special_Inspection_Employee1] foreign key ([Employee_id]) references [Employee]([Employee_id])
)
go

/******************
Insert Sample Data For The  Special_Inspection table
***************/
print ''
Print '***Insert Sample Data For The  Special_Inspection table***' 
 go

INSERT INTO [dbo].[Special_Inspection]
    ([Special_Service_Order_ID], [Inspection_Description],[Date],
    [Employee_ID],[Is_Active]
    )
VALUES
    ('100000', 'Looks Bad', GETDATE(), 100000, 1),
    ('100000', 'Probably the Radiator', GETDATE(), 100001, 1),
    ('100000', 'Might be more!', GETDATE(), 100002, 1),
    ('100001', 'It will be fine', GETDATE(), 100000, 1),
    ('100001', 'It is broken', GETDATE(), 100001, 1);
Go

/******************
Create the [dbo].[Bid] table
***************/
print ''
Print '***Create the [dbo].[Bid] table***' 
 go


CREATE TABLE [dbo].[Bid]
(


    [Bid_ID] [int] IDENTITY(100000,1) not null	
,
    [Special_Service_Order_ID] [int] not null	
,
    [vendor_id] [int] not null	
,
    [Bid_Description] [nvarchar](500) not null	
,
    [Date] [datetime] not null	
,
    [Amount] [money] not null
,
    [is_Approved] [bit] not null	
,
    [Is_Active] [bit] not null	
,
    CONSTRAINT [PK_Bid] PRIMARY KEY ([Bid_ID])
,
    CONSTRAINT [fk_Bid_Special_Service_Order0] foreign key ([Special_Service_Order_ID]) references [Special_Service_Order]([Special_Service_Order_ID])
,
    CONSTRAINT [fk_Bid_vendor1] foreign key ([vendor_id]) references [vendor]([vendor_id])
)
go

/******************
Insert Sample Data For The  Bid table
***************/
print ''
Print '***Insert Sample Data For The  Bid table***' 
 go

INSERT INTO [dbo].[Bid]
    ([Special_Service_Order_ID], [vendor_id],[Bid_Description],
    [Date],[Amount],[is_Approved], [Is_Active]
    )
VALUES
    ('100000', 100000, 'Repair', GETDATE(), 450, 0, 1),
    ('100000', 100001, 'Repair', GETDATE(), 460, 0, 1),
    ('100000', 100002, 'Repair', GETDATE(), 470, 0, 1),
    ('100001', 100001, 'Disposal', GETDATE(), 40, 0, 1),
    ('100001', 100002, 'Disposal', GETDATE(), 60, 0, 1);
Go

/******************
Create the [dbo].[Special_Work_Order] table
***************/
print ''
Print '***Create the [dbo].[Special_Work_Order] table***' 
 go


CREATE TABLE [dbo].[Special_Work_Order]
(


    [Special_Work_Order_ID] [int] IDENTITY(100000,1) not null	
,
    [Bid_ID] [int] not null	
,
    [Work_Order_Description] [nvarchar](500) not null	
,
    [Drop_Off_Date] [datetime] not null	
,
    [Pick_Up_DAte] [datetime] not null	
,
    [Is_Active] [bit] not null	
,
    CONSTRAINT [PK_Special_Work_Order] PRIMARY KEY ([special_Work_Order_ID])
,
    CONSTRAINT [fk_Special_Work_Order_Bid0] foreign key ([Bid_ID]) references [Bid]([Bid_ID])
)
go

/******************
Insert Sample Data For The  Special_Work_Order table
***************/
print ''
Print '***Insert Sample Data For The  Special_Work_Order table***' 
 go

INSERT INTO [dbo].[Special_Work_Order]
    ([Bid_ID], [Work_Order_Description],[Drop_Off_Date],
    [Pick_Up_Date],[Is_Active]
    )
VALUES
    ('100000', 'Drop it at Jimmys', 17/12/2023, 19/12/2015, 1),
    ('100001', 'Take it to the dump', 20/12/2015, 21/12/2015, 1)

Go

/******************
Create the [dbo].[Change_Order] table
***************/
print ''
Print '***Create the [dbo].[Change_Order] table***' 
 go
CREATE TABLE [dbo].[Change_Order]
(
    [Change_Order_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Vendor_ID] [int] NOT NULL,
    [Change_Order_Date] [datetime] NOT NULL,
    [Original_PO_Number] [int] NOT NULL,
    [Employee_ID] [int] NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [PK_Change_Order] PRIMARY KEY([Change_Order_ID]),
    CONSTRAINT [FK_Change_Order_Vendor] FOREIGN KEY ([Vendor_ID])
        REFERENCES [dbo].[Vendor] ([Vendor_ID]),
    CONSTRAINT [FK_Change_Order_Purchase_Order] FOREIGN KEY([Original_PO_Number])
        REFERENCES [dbo].[Purchase_Order] ([Purchase_Order_ID]),
    CONSTRAINT [FK_Change_Order_Employee] FOREIGN KEY ([Employee_ID])
        REFERENCES [dbo].[Employee] ([Employee_ID])
)
go


/******************
Insert Sample Data For The  Change_Order table
***************/
print ''
Print '***Insert Sample Data For The  Change_Order table***' 
 go
INSERT INTO [dbo].[Change_Order]
    ([Vendor_ID], [Change_Order_Date], [Original_PO_Number], [Employee_ID])
VALUES
    (100000, GETDATE(), 100000, 100001),
    (100001, GETDATE(), 100001, 100002),
    (100002, GETDATE(), 100002, 100002),
    (100003, GETDATE(), 100002, 100003),
    (100003, GETDATE(), 100003, 100002);
go


/******************
Create the [dbo].[Change_Order_Line] table
***************/
print ''
Print '***Create the [dbo].[Change_Order_Line] table***' 
 go
CREATE TABLE [dbo].[Change_Order_Line]
(
    [Change_Order_ID] [int] NOT NULL,
    [Parts_Inventory_ID] [int] NOT NULL,
    [Original_Qty] [int] NOT NULL,
    [Updated_Qty] [int] NOT NULL,
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [PK_Change_Order_Line] PRIMARY KEY ([Change_Order_ID]),
    CONSTRAINT [FK_Change_Order_Line_Change_Order] FOREIGN KEY ([Change_Order_ID])
    	REFERENCES [dbo].[Change_Order] ([Change_Order_ID]),
    CONSTRAINT [FK_Change_Order_Line_Parts_Inventory] FOREIGN KEY ([Parts_Inventory_ID])
    	REFERENCES [dbo].[Parts_Inventory] ([Parts_Inventory_ID])
)
go


/******************
Insert Sample Data For The  Change_Order_Line table
***************/
print ''
Print '***Insert Sample Data For The  Change_Order_Line table***' 
 go
INSERT INTO [dbo].[Change_Order_Line]
    ([Change_Order_ID], [Parts_Inventory_ID], [Original_Qty], [Updated_Qty])
VALUES
    (100000, 100001, 1, 1),
    (100001, 100002, 2, 1),
    (100002, 100003, 2, 4),
    (100003, 100002, 2, 3),
    (100004, 100001, 1, 2)
go



/******************
Create the [dbo].[Packing_Slip_Line_Items] table
***************/
print ''
Print '***Create the [dbo].[Packing_Slip_Line_Items] table***' 
go
CREATE TABLE [dbo].[Packing_Slip_Line_Items]
(
    [Packing_Slip_ID] [int] NOT NULL,
    [Qty_Recieved] [int] NOT NULL,
    [Parts_Inventory_ID] [int] NOT NULL,
    [Is_Active] [int] NOT NULL default'1',
    CONSTRAINT [FK_Packing_Slip_Line_Items_Packing_Slip_ID] FOREIGN KEY ([Packing_Slip_ID])
        REFERENCES [dbo].[Packing_Slip]([Packing_Slip_ID]),
    CONSTRAINT [FK_Packing_Slip_Line_Items_Parts_Inventory] FOREIGN KEY ([Parts_Inventory_ID])
        REFERENCES [dbo].[Parts_Inventory] ([Parts_Inventory_ID]),
    CONSTRAINT [PK_Packing_Slip_Line_Items] PRIMARY KEY ([Packing_Slip_ID], [Parts_Inventory_ID])
)
go

/******************
Insert Sample Data For The  Packing_Slip_Line_Items table
***************/
print ''
Print '***Insert Sample Data For The  Packing_Slip_Line_Items table***' 
 go
INSERT INTO [dbo].[Packing_Slip_Line_Items]
    ([Packing_Slip_ID], [Qty_Recieved], [Parts_Inventory_ID])
VALUES
    (100000, 2, 100004),
    (100001, 2, 100003),
    (100002, 2, 100002),
    (100003, 2, 100001),
    (100004, 2, 100000)
go


/******************
Create the [dbo].[Model_Compatibility] table
***************/
print ''
Print '***Create the [dbo].[Model_Compatibility] table***' 
 go 


GO
CREATE TABLE [dbo].[Model_Compatibility]
(
    [Vehicle_Model_ID] [int] NOT NULL,
    [Parts_Inventory_ID] [int] NOT NULL,
    [Active] [bit] NOT NULL DEFAULT 1,

    CONSTRAINT [FK_Model_Compatibility_Vehicle_Model_ID] FOREIGN KEY([Vehicle_Model_ID])
        REFERENCES [dbo].[Vehicle_Model]([Vehicle_Model_ID]),
    CONSTRAINT [FK_Model_Compatibility_Parts_Inventory_ID] FOREIGN KEY([Parts_Inventory_ID])
        REFERENCES [dbo].[Parts_Inventory]([Parts_Inventory_ID]),
    CONSTRAINT [CPK_Model_Compatibility] PRIMARY KEY([Vehicle_Model_ID], [Parts_Inventory_ID])
)
GO

/******************
Insert Sample Data For The  Model_Compatibility table
***************/
print ''
Print '***Insert Sample Data For The  Model_Compatibility table***' 
 go

INSERT INTO [dbo].[Model_Compatibility]
    (
    [Vehicle_Model_ID],
    [Parts_Inventory_ID]
    )
VALUES
    (100001, 100000),
    (100001, 100002),
    (100001, 100003),
    (100001, 100004),
    (100001, 100005),
	(100002, 100006),
    (100002, 100007),
    (100002, 100008),
    (100002, 100009),
    (100002, 100010),
	(100003, 100011),
    (100003, 100001),
    (100003, 100002),
    (100003, 100003),
    (100004, 100004),
	(100004, 100005),
    (100004, 100006);
    
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
    CONSTRAINT [PK_Service_Detail] PRIMARY KEY ([Service_Detail_ID]),
    CONSTRAINT [FK_Service_Detail_Service_Order] FOREIGN KEY ([Service_Order_ID], [Service_Order_Version])
        REFERENCES [Service_Order]([Service_Order_ID], [Service_Order_Version]),
    CONSTRAINT [FK_Service_Detail_Service_Type] FOREIGN KEY ([Service_Type_ID])
        REFERENCES [Service_Type]([Service_Type_ID]) ON UPDATE CASCADE,
    CONSTRAINT [FK_Service_Detail_Employee] FOREIGN KEY ([Employee_ID])
        REFERENCES [Employee]([Employee_ID])
)
GO


/******************
Insert Sample Data For The  Service_Detail table
***************/
print ''
Print '***Insert Sample Data For The  Service_Detail table***' 
 go
INSERT INTO [dbo].[Service_Detail]
    ([Service_Order_ID],[Service_Order_Version],[Service_Type_ID],[Employee_ID])
VALUES
    (100000, 1, 'Oil Change', 100000),
    (100001, 2, 'Oil Change', 100001),
    (100002, 1, 'Oil Change', 100002),
    (100003, 3, 'Oil Change', 100003),
    (100004, 2, 'Oil Change', 100004);
GO

/******************
Create the [dbo].[Parts_Request] table
***************/
print ''
Print '***Create the [dbo].[Parts_Request] table***' 
 go
CREATE TABLE [dbo].[Parts_Request]
(
    [Parts_Request_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Employee_ID] [int] NOT NULL,
    [Service_Detail_ID] [int] NOT NULL,
    [Parts_Request_Notes] [nvarchar](MAX) NULL,
    [Date_Requested] [date] NOT NULL,
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [PK_Parts_Request] PRIMARY KEY ([Parts_Request_ID]),
    CONSTRAINT [FK_Parts_Request_Employee] FOREIGN KEY ([Employee_ID])
    	REFERENCES [dbo].[Employee] ([Employee_ID]),
    CONSTRAINT [FK_Parts_Request_Service_Detail] FOREIGN KEY ([Service_Detail_ID])
    	REFERENCES [dbo].[Service_Detail] ([Service_Detail_ID])
)  
go


/******************
Insert Sample Data For The  Parts_Request table
***************/
print ''
Print '***Insert Sample Data For The  Parts_Request table***' 
 go
INSERT INTO [dbo].[Parts_Request]
    ([Employee_ID], [Service_Detail_ID], [Parts_Request_Notes], [Date_Requested], [Is_Active])
VALUES
    (100001, 100001, "Needing part to fix issue in vehicle.", '2023-12-05', 1),
    (100002, 100002, "This specific part will allow us to get this vehicle back on the lot.", '2023-05-16', 1),
    (100001, 100002, "We seem to keep running out of this, maybe order more.", '2024-01-18', 1),
    (100003, 100003, "We only need a handful more. Maybe 4 at most.", '2024-02-20', 1),
    (100003, 100004, "Part keeps breaking and needs replaced.", '2024-01-03', 0)
go

/******************
Create the [dbo].[Parts_Request_Line_Items] table
***************/
print ''
Print '***Create the [dbo].[Parts_Request_Line_Items] table***' 
 go

CREATE TABLE [dbo].[Parts_Request_Line_Items]
(
    [Parts_Request_ID] [int] NOT NULL,
    [Parts_Inventory_ID] [int] NOT NULL,
    [Qty_Requested] [int] NOT NULL,
    [Active] [bit] NOT NULL DEFAULT 1,

    CONSTRAINT [FK_Parts_Request_Line_Items_Parts_Request_ID] FOREIGN KEY([Parts_Request_ID])
        REFERENCES [dbo].[Parts_Request]([Parts_Request_ID]),
    CONSTRAINT [FK_Parts_Request_Line_Items_Parts_Inventory_ID] FOREIGN KEY([Parts_Inventory_ID])
        REFERENCES [dbo].[Parts_Inventory]([Parts_Inventory_ID]),
    CONSTRAINT [CPK_Parts_Request_Line_Items] PRIMARY KEY([Parts_Request_ID], [Parts_Inventory_ID]),
)
GO


/******************
Insert Sample Data For The  Parts_Request_Line_Items table
***************/
print ''
Print '***Insert Sample Data For The  Parts_Request_Line_Items table***' 
 go

/* Parts Request Line Items Test Record */
print ''
print '*** creating parts request line items test records ***'
GO
INSERT INTO [dbo].[Parts_Request_Line_Items]
    (
    [Parts_Request_ID],
    [Parts_Inventory_ID],
    [Qty_Requested])
VALUES
    (100000, 100000, 5),
    (100001, 100002, 2),
    (100002, 100003, 1),
    (100003, 100001, 3),
    (100004, 100004, 4);
GO

/******************
Create the [dbo].[Inspection_Report] table
***************/
print ''
print '*** Creating Inspection_Report Table ***'
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
    [Is_Active] [bit] NOT NULL DEFAULT 1
    CONSTRAINT [fk_Inspection_Report_Employee_ID] FOREIGN KEY ([Employee_ID])
        REFERENCES [dbo].[Driver] ([Employee_ID]),
    CONSTRAINT [fk_Inspection_Report_VIN] FOREIGN KEY ([VIN])
        REFERENCES [dbo].[Vehicle] ([VIN]),
    CONSTRAINT [fk_Inspection_Report_Service_Order] FOREIGN KEY ([Service_Order_ID], [Service_Order_Version])
        REFERENCES [dbo].[Service_Order] ([Service_Order_ID], [Service_Order_Version]),
    CONSTRAINT [pk_Inspection_Report] PRIMARY KEY ([Inspection_Report_ID])
)
GO
/******************
Insert Sample Data For The  Inspection_Report table
***************/
print ''
print '*** Inserting Sample Data into Inspection_Report ***'
GO
INSERT INTO [dbo].[Inspection_Report]
    (
    [Employee_ID],
    [VIN],
    [Service_Order_ID],
    [Service_Order_Version],
    [Oil_Level],
    [Tire_Pressure],
    [Front_Left_Turn_Signal],
    [Front_Right_Turn_Signal],
    [Rear_Left_Turn_Signal],
    [Rear_Right_Turn_Signal],
    [Left_Brake_Light],
    [Right_Brake_Light],
    [Windshield_Washer_Fluid],
    [Problem_Description],
    [Is_Active]
    )
VALUES
    (100000, '1HGCM82633A123456', 100000, 1, 'Full',
        35, 1, 1, 1, 1, 1, 1, 'Full', NULL, 1),
    (100001, '5XYZH4AG4JH123456', 100001, 2, 'Low', 30, 0, 1,
        1, 0, 1, 0, 'Low', 'Right rear turn signal not working.', 1),
    (100002, 'JM1BK32F781234567', 100002, 1, 'OK', 32, 1,
        1, 1, 1, 0, 1, 'OK', 'Left brake light faulty.', 1),
    (100003, 'WAUZZZ4G6BN123456', 100003, 3, 'Full', 28, 1, 1, 0, 1,
        1, 1, 'Full', 'Rear left turn signal not operational.', 1),
    (100004, '1C4RJFAG5FC123456', 100004, 2, 'OK', 33,
        1, 0, 1, 1, 1, 1, 'OK', 'Front right turn signal malfunctioning.', 1)
GO

/******************
Create the [dbo].[Client] table
***************/
print ''
Print '***Create the [dbo].[Client] table***' 
 go


print ''
print '*** creating client table ***'
GO
CREATE TABLE [dbo].[Client]
(
    [Client_ID] [int] IDENTITY(100000, 1) NOT NULL,
    [Given_Name] [nvarchar] (50) NOT NULL,
    [Family_Name] [nvarchar] (50) NOT NULL,
    [Middle_Name] [nvarchar] (50) NULL,
    [DOB] [date] NOT NULL,
    [Email] [nvarchar] (255) UNIQUE NOT NULL,
    [Postal_Code] [nvarchar] (9) NULL,
    [City] [nvarchar] (50) NULL,
    [Region] [nvarchar] (50) NULL,
    [Address] [nvarchar] (100) NULL,
    [Text_Number] [nvarchar] (12) UNIQUE NULL,
    [Voice_Number] [nvarchar] (12) UNIQUE NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Client_ID] PRIMARY KEY ([Client_ID])
)
GO


/******************
Insert Sample Data For The  Client table
***************/
print ''
Print '***Insert Sample Data For The  Client table***' 
 go
INSERT INTO [dbo].[Client]
    ([Given_Name],[Family_Name],[Middle_Name],[DOB],[Email],[Postal_Code],[City],[Region],[Address],[Text_Number],[Voice_Number],[Is_Active])
VALUES
    ('Christalle', 'Jeandon', 'Morales', '2023-7-3', 'cjeandon0@indiatimes.com', '96120', 'Punta del Este', 'RU-UD', '40223 Hintze Terrace', '265-932-6215', '277-589-9894', 1),
    ('Ignazio', 'Slator', 'Yesson', '2023-7-5', 'islator1@discovery.com', '96120', 'Suphan Buri', 'US-NV', '18815 Gerald Pass', '584-621-4234', '723-847-5006', 0),
    ('Norene', 'Elsmor', 'Crady', '2024-1-9', 'nelsmor2@nba.com', '51250', 'Kapshagay', 'CA-NT', '6 Arkansas Court', '230-927-7810', '913-293-4175', 0),
    ('Theodoric', 'Barrat', 'Donnellan', '2023-4-8', 'tbarrat3@loc.gov', '37120', 'Tinambac', 'BR-SP', '9 Schurz Road', '367-830-3054', '235-984-5957', 0),
    ('Cesya', 'Rieme', 'Davidovsky', '2023-5-6', 'crieme4@cisco.com', '11803', 'Doong', 'TZ-18', '35 American Ash Park', '416-832-0688', '909-892-4935', 1)
GO

/******************
Create the [dbo].[Client_Credential] table
***************/
print ''
Print '***Create the [dbo].[Client_Credential] table***' 
 go


CREATE TABLE [dbo].[Client_Credential]
(
    [License_Number] [nvarchar] (12) NOT NULL,
    [Driver_License_Class_ID] [nvarchar]	(6) NOT NULL,
    [License_Expiration] [date] NOT NULL,
    [Client_ID] [int] NOT NULL,
    [Permission] [nvarchar] (50) NULL,
    [Certified] [bit] NULL,
    [Certification_Description] [nvarchar]	(250) NULL,
    [Certification_Date] [date] NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_License_Number] PRIMARY KEY ([License_Number]),
    CONSTRAINT [fk_Driver_License_Class_Driver_License_Class_ID] FOREIGN KEY ([Driver_License_Class_ID])
        REFERENCES [dbo].[Driver_License_Class] ([Driver_License_Class_ID]),
    CONSTRAINT [fk_Client_Client_ID] FOREIGN KEY ([Client_ID])
        REFERENCES [dbo].[Client] ([Client_ID])
)
GO
/******************
Insert Sample Data For The  Client_Credential table
***************/
print ''
Print '***Insert Sample Data For The  Client_Credential table***' 
 go
INSERT INTO [dbo].[Client_Credential]
    ([License_Number],[Driver_License_Class_ID],[License_Expiration],[Client_ID],[Permission],[Certified],[Certification_Description],[Certification_Date],[Is_Active])
VALUES
    ('1', 'D', '2024/1/1', 100000, 1, '0', 'Sed ante. Vivamus tortor. Duis mattis egestas metus', '2024/1/2', 0),
    ('2', 'CDL-P', '2023/7/3', 100001, 2, '0', 'Sed antet. Vivamus tortor. Duis mattis egestas metus', '2024/1/2', 1),
    ('3', 'CDL-S', '2023/5/1', 100002, 3, '0', 'Sed anteq. Vivamus tortor. Duis mattis egestas metus', '2024/1/2', 0),
    ('4', 'E', '2023/5/2', 100003, 4, '0', 'Sed antez. Vivamus tortor. Duis mattis egestas metus', '2024/1/2', 1),
    ('5', 'CDL-T', '2023/2/8', 100004, 5, '0', 'Sed antex. Vivamus tortor. Duis mattis egestas metus', '2024/1/2', 0)
GO
/******************
Create the [dbo].[Login] table
***************/
print ''
Print '***Creating [dbo].[Login] table***' 
 go
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
    CONSTRAINT 		[pk_Username] PRIMARY KEY ([Username]),
    CONSTRAINT		[fk_Client_ID] 
		FOREIGN KEY ([Client_ID])  REFERENCES [dbo].[Client]([Client_ID]),
    /*CONSTRAINT		[ak_Client_ID] UNIQUE ([Client_ID]),
	CONSTRAINT		[ak_Employee_ID_login] UNIQUE ([Employee_ID])*/
)
GO
print ''
Print '***Creating Index for [dbo].[Login] table***' 
GO
CREATE UNIQUE INDEX [idx_login_Username_and_ID]
	ON [dbo].[Login] ([Username],[Client_ID])
GO

/******************
Insert Sample Data For The  Login table
***************/
print ''
Print '***Insert Sample Data For The  Login table***' 
 go

INSERT INTO [dbo].[Login]
    ([Username], [Employee_ID], [Client_ID],
    [Security_Question_1],[Security_Response_1],
    [Security_Question_2],[Security_Response_2],
    [Security_Question_3],[Security_Response_3])
VALUES
    ('JoeSmith1994', 100000, NULL, 'what is your favorite animal?', 'lion', 'what is your favorite food?', 'Ramen', 'what was your first dogs name?', 'Hoola'),
    ('Jacmar125', 100001, NULL, 'what is your favorite animal?', 'Ocelot', 'what is your favorite food?', 'Bibimbap', 'what was your first dogs name?', 'Jeff'),
    ('Lebold2202', 100002, NULL, 'what is your favorite animal?', 'Foxes', 'what is your favorite food?', 'Spaghetti', 'what was your first dogs name?', 'Lola'),
	('Krystal2023', NULL, 100000, 'what is your favorite animal?', 'Foxes', 'what is your favorite food?', 'Spaghetti', 'what was your first dogs name?', 'Lola')
GO

/******************
Create the [dbo].[Client_Role] table
***************/
print ''
Print '***Create the [dbo].[Client_Role] table***' 
go

CREATE TABLE [dbo].[Client_Role]
(
    [Client_Role_ID] [nvarchar](100) NOT NULL,
    [Role_Description] [nvarchar] (500) NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Client_Role_ID] PRIMARY KEY ([Client_Role_ID])
)
GO
/******************
Insert Sample Data For The  Client_Role table
***************/
print ''
Print '***Insert Sample Data For The  Client_Role table***' 
 go
INSERT INTO [dbo].[Client_Role]
    ([Client_Role_ID],[Role_Description],[Is_Active])
VALUES
    ('Test', 'Duis mattis egestas', 1),
    ('Test1', 'Suspendisse potenti. In eleifend quam a odio', 1),
    ('Test2', 'Donec diam neque', 1),
    ('Test3', 'Pellentesque at nulla', 1),
    ('Test4', 'Fusce consequat', 1)
GO

/******************
Create the [dbo].[Client_Client_Role] table
***************/
print ''
Print '***Create the [dbo].[Client_Client_Role] table***' 
 go


CREATE TABLE [dbo].[Client_Client_Role]
(
    [Client_ID] [int] NOT NULL,
    [Client_Role_ID] [nvarchar](100) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [fk_Client_Client_Role_Client_ID] FOREIGN KEY([Client_ID])
        REFERENCES [dbo].[Client] ([Client_ID]),
    CONSTRAINT [fk_Client_Client_Role_Client_Role_ID] FOREIGN KEY([Client_Role_ID])
        REFERENCES [dbo].[Client_Role] ([Client_Role_ID]),
    CONSTRAINT [cpk_Client_Client_Role] PRIMARY KEY ([Client_ID],[Client_Role_ID])
)
GO

/******************
Insert Sample Data For The  Client_Client_Role table
***************/
print ''
Print '***Insert Sample Data For The  Client_Client_Role table***' 
 go 
GO
INSERT INTO [dbo].[Client_Client_Role]
    ([Client_ID],[Client_Role_ID],[Is_Active])
VALUES
    (100000, 'Test', 0),
    (100001, 'Test', 0),
    (100002, 'Test', 1),
    (100003, 'Test', 0),
    (100004, 'Test', 1)
GO

/******************
Create the [dbo].[Accommodation] table
***************/
print ''
Print '***Create the [dbo].[Accommodation] table***' 
 go


CREATE TABLE [dbo].[Accommodation]
(
    --[Accommodation_ID]				[int]	IDENTITY(100000,1)		NOT NULL,
    [Accommodation_ID] [nvarchar](100) NOT NULL,
    [Accommodation_Description] [nvarchar](255) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [pk_Accommodation] PRIMARY KEY([Accommodation_ID])
)
GO

/******************
Insert Sample Data For The  Accommodation table
***************/
print ''
Print '***Insert Sample Data For The  Accommodation table***' 
 go
INSERT INTO [dbo].[Accommodation]
    ([Accommodation_ID], [Accommodation_Description])
VALUES
    ('Wheelchair Lift', 'A lift for persons with a wheelchair to easily get on and off transport'),
    ('Wheelchair Ramp', 'A ramp to assist persons with a wheel chair to get on and off transport'),
    ('Raised Roof', 'A raised roof to allow more space in the case of a wheelchair'),
    ('Dropped Floor', 'A dropped floor to allow more space in the case of a wheelchair'),
    ('Transfer Seats', 'Used to lift a person into and out of their vehicle.'),
    ('Swivel Seats', 'Lets a regular seat swivel towards the door for easy access'),
	('None', 'No accommodation required')
    
GO


/******************
Create the [dbo].[Client_Accommodation] table
***************/
print ''
Print '***Create the [dbo].[Client_Accommodation] table***' 
 go


CREATE TABLE [dbo].[Client_Accommodation]
(
    [Client_ID] [int] NOT NULL,
    [Accommodation_ID] [nvarchar] (100) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [fk_Client_Accommodation_Client_ID] FOREIGN KEY([Client_ID])
        REFERENCES [dbo].[Client] ([Client_ID]),
    CONSTRAINT [fk_Client_Accommodation_Accommodation_ID] FOREIGN KEY([Accommodation_ID])
        REFERENCES [dbo].[Accommodation] ([Accommodation_ID]),
    CONSTRAINT [pk_Client_Accommodation] PRIMARY KEY ([Client_ID],[Accommodation_ID])
)
GO
/******************
Insert Sample Data For The  Client_Accommodation table
***************/
print ''
Print '***Insert Sample Data For The  Client_Accommodation table***' 
 go
INSERT INTO [dbo].[Client_Accommodation]
    ([Client_ID],[Accommodation_ID],[Is_Active])
VALUES
    (100000, 'Wheelchair Lift', 1),
    (100001, 'Wheelchair Ramp', 1),
    (100002, 'Raised Roof', 1),
    (100003, 'Transfer Seats', 0),
    (100004, 'Dropped Floor', 1);
		
		
GO

/******************
Create the [dbo].[Dependent] table
***************/
print ''
Print '***Creating [dbo].[Dependent] table***' 
 go
CREATE TABLE [dbo].[Dependent]
(
    [Dependent_ID] [int] IDENTITY(100000, 1) NOT NULL,
    [Given_Name] [nvarchar](50) NOT NULL,
    [Family_Name] [nvarchar](50) NOT NULL,
    [Middle_Name] [nvarchar](100) NULL,
    [DOB] [DATE] NOT NULL,
    [Gender] [nvarchar](20) NULL,
    [Emergency_Contact] [nvarchar](100) NOT NULL,
	[Contact_Relationship] 	[nvarchar](100) 				NOT NULL,
    [Emergency_Phone] [nvarchar](12) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT 		[pk_Dependent_ID] PRIMARY KEY ([Dependent_ID])
)
GO
print ''
Print '***Creating Index for [dbo].[Dependent] table***' 
GO
CREATE INDEX [idx_Dependent_ID_And_Family_Name]
	ON [dbo].[Dependent] ([Dependent_ID],[Family_Name])
GO

/******************
Insert Sample Data For The  Dependent table
***************/
print '' Print '***Insert Sample Data For The  Dependent table***' 
 go 
 INSERT INTO [dbo].[Dependent]
        ([Given_Name],[Middle_Name],[Family_Name],[DOB],[Gender],[Emergency_Contact],[Contact_Relationship],[Emergency_Phone])
    VALUES
        ('Anita','ko','Feuer','12-12-1996','Female','Thomas Feuer', 'test1', '5552231049'),
        ('Flint','N','Steele','12-12-2002','Male','Cole D. Steele','test1','5554259994'),
		('Jarlson','lo','Flouf','12-12-1996','Male','Thomas Feuer','test1','5552231222'),
        ('Tanner','Ant','Minecraft','12-12-2002','Male','Mother','test1','5554259333'),
		('Christa','Lank','Crank','12-12-1996','Male','Thomas Feuer','test1','5552231444'),
        ('Lincoln','The','Logs','12-12-2002','Male','Pick ingups tix','test1','5554259555'),
		('FatherHeim','l','MotherHerm','12-12-1996','Female','Cousinxeim','test1','5552231666'),
        ('TinkerBell','H','Pixie','12-12-2002','Female','Pixar Studios','test1','5554259777')
GO 

/******************
Create the [dbo].[Dependent_Accommodation] table
***************/
print '' Print '***Create the [dbo].[Dependent_Accommodation] table***' 
GO
CREATE TABLE [dbo].[Dependent_Accommodation] (
    [Dependent_ID]            [int]                        NOT NULL,
    [Accommodation_ID]        [nvarchar](100)                NOT NULL,                            
    [Is_Active]                [bit]                        NOT NULL    DEFAULT 1,
    CONSTRAINT        [fk_Dependent_ID]
        FOREIGN KEY ([Dependent_ID])  REFERENCES [dbo].[Dependent]([Dependent_ID]),
    CONSTRAINT        [fk_Accommodation_ID]
        FOREIGN KEY ([Accommodation_ID])  REFERENCES [dbo].[Accommodation]([Accommodation_ID]),
	CONSTRAINT [cpk_DependentAccommodation] PRIMARY KEY ([Dependent_ID], [Accommodation_ID])
)
GO

/******************
Insert Sample Data For The  Dependent_Accommodation table
***************/
print '' Print '***Insert Sample Data For The  Dependent_Accommodation table***' 
GO
 
 INSERT INTO [dbo].[Dependent_Accommodation]
        ([Dependent_ID], [Accommodation_ID], [is_active])
    VALUES
        (100000, 'Wheelchair Lift', 1),
		(100001, 'Raised Roof', 1),
		(100002, 'None', 1),
		(100003, 'Raised Roof', 1),
		(100003, 'Dropped Floor', 1),
		(100004, 'None', 1),
		(100005, 'Transfer Seats', 1),
		(100006, 'Swivel Seats', 1),
		(100007, 'Swivel Seats', 1)
GO 


/******************
Create the [dbo].[Client_Dependent_Role] table
***************/
print ''
Print '***Create the [dbo].[Client_Dependent_Role] table***' 
 go

CREATE TABLE [dbo].[Client_Dependent_Role](
[Client_ID]	[int]	not null	
,[Dependent_ID]	[int]	not null	
,[Relationship]	[nvarchar](100)	not null	
,[Is_Active]	[bit]	not null	
,CONSTRAINT [PK_Client_Dependent_Role] PRIMARY KEY ([Client_ID] , [Dependent_ID])
,CONSTRAINT [fk_Client_Dependent_Role_Client0] foreign key ([Client_ID]) references [Client]([Client_ID])
,CONSTRAINT [fk_Client_Dependent_Role_Dependent1] foreign key ([Dependent_ID]) references [Dependent]([Dependent_ID])
)
go

/******************
Insert Sample Data For The  Client_Dependent_Role table
***************/

print '' Print '***Insert Sample Data For The  Client_Dependent_Role table***' 
 go
  go
 INSERT INTO [dbo].[Client_Dependent_Role]
		([Client_ID], [Dependent_ID], [Relationship], [Is_Active])
	VALUES
		(100000, 100000, 'Parent', 0),
		(100000, 100001, 'Parent', 1),
		(100000, 100002, 'Parent', 1),
		(100003, 100002, 'Legal Custodian', 1),
		(100004, 100003, 'Parent', 1),
		(100002, 100003, 'Parent', 1)
GO

/******************
Create the [dbo].[Notification] table
***************/
print ''
Print '***Create the [dbo].[Notification] table***' 
 go
CREATE TABLE [dbo].[Notification]
(
    [Notification_ID] [int] IDENTITY(100000, 1) NOT NULL,
    [Client_ID] [int] NOT NULL,
    [Title] [nvarchar](255) NOT NULL DEFAULT
        'User Notification',
    [Notification_Body] [text] NOT NULL,
    [Time_Sent] [DATETIME] NOT NULL DEFAULT CURRENT_TIMESTAMP,
    [Viewed] [bit] NOT NULL DEFAULT 0,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT         [pk_Notification_ID] PRIMARY KEY ([Notification_ID]),
    CONSTRAINT        [fk_notification_Client_ID] 
        FOREIGN KEY ([Client_ID])  REFERENCES [dbo].[Client]([Client_ID])
)
GO
print ''
Print '***Creating Index for [dbo].[Notification] table***' 
GO
CREATE INDEX [idx_Notification_IDs]
    ON [dbo].[Notification] ([Notification_ID],[Client_ID])
GO

/******************
Insert Sample Data For The  Notification table
***************/
print ''
Print '***Insert Sample Data For The  Notification table***' 
 go
INSERT INTO [dbo].[Notification]
    ([Client_ID],[Title],[Notification_Body])
VALUES
    (100000, 'Holiday Deal!', 'Rent vehicles at a 30% discount!'),
    (100001, 'Your Reservation has Been Finalized.', 'Thank you for using our services!'),
    (100002, 'Your Vehicle is Ready', 'Your vehicle is ready to be picked up. Please go to the front desk for your keys.'),
    (100003, 'You have been banned From Renting Coach Buses',
        'After many incidents with you returning coach buses with extensive damages to their onboard bathrooms, 
        we have decided to ban you from renting coach buses.
        \n\n
        Thank You,\n
        Management')
GO

/******************
Create the [dbo].[Ticket_Type] table
***************/
print ''
Print '***Create the [dbo].[Ticket_Type] table***' 
 go


CREATE TABLE [dbo].[Ticket_Type]
(
    [Ticket_Type_ID] [nvarchar](50) NOT NULL,
    [Type_Description] [nvarchar](500),
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [pk_Ticket_Type_ID] PRIMARY KEY([Ticket_Type_ID])
)
go

/******************
Insert Sample Data For The  Ticket_Type table
***************/
print ''
Print '***Insert Sample Data For The  Ticket_Type table***' 
 go


INSERT INTO [dbo].[Ticket_Type]
    ([Ticket_Type_ID], [Type_Description])
VALUES
    ( 'Type1', 'Sample Ticket Type Description'),
    ( 'Type2', 'Sample Ticket Type Description'),
    ( 'Type3', 'Sample Ticket Type Description'),
    ( 'Type4', 'Sample Ticket Type Description'),
    ( 'Type5', 'Sample Ticket Type Description')
go


/******************
Create the [dbo].[Support_Ticket] table
***************/
print ''
Print '***Create the [dbo].[Support_Ticket] table***' 
 go


CREATE TABLE [dbo].[Support_Ticket]
(
    [Support_Ticket_ID] [int] NOT NULL IDENTITY(100000, 1),
    [Ticket_Type_ID] [nvarchar](50) NOT NULL,
    [Client_ID] [int] NOT NULL,
    [Time_Opened] [datetime] NOT NULL,
    [Time_Closed] [datetime],
    [Support_Note] [nvarchar](3000),
    [Is_Open] [bit] DEFAULT 1 NOT NULL,
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [pk_Support_Ticket_ID] PRIMARY KEY([Support_Ticket_ID]),
    CONSTRAINT [fk_Support_Ticket_Ticket_Type_ID] FOREIGN KEY ([Ticket_Type_ID])
		REFERENCES [dbo].[Ticket_Type] ([Ticket_Type_ID]),
    CONSTRAINT [fk_Support_Ticket_Client_ID] FOREIGN KEY ([Client_ID])
		REFERENCES [dbo].[Client] ([Client_ID])
)
go

/******************
Insert Sample Data For The  Support_Ticket table
***************/
print ''
Print '***Insert Sample Data For The  Support_Ticket table***' 
 go

/******************
Create the [dbo].[Support_Ticket_Employee_Line] table
***************/
print ''
Print '***Create the [dbo].[Support_Ticket_Employee_Line] table***' 
 go


CREATE TABLE [dbo].[Support_Ticket_Employee_Line]
(
    [Support_Ticket_ID] [int] NOT NULL,
    [Employee_ID] [int] NOT NULL,
    [Time_Assigned] [datetime] NOT NULL,
    [Is_Active] [bit] DEFAULT 1 NOT NULL,
    CONSTRAINT [pk_Support_Ticket_Employee_Line_ID] PRIMARY KEY([Support_Ticket_ID], [Employee_ID]),
    CONSTRAINT [fk_Support_Ticket_Employee_Line_Support_Ticket_ID] FOREIGN KEY ([Support_Ticket_ID])
		REFERENCES [dbo].[Support_Ticket] ([Support_Ticket_ID]),
    CONSTRAINT [fk_Support_Ticket_Employee_Line_Employee_ID] FOREIGN KEY ([Employee_ID])
		REFERENCES [dbo].[Employee] ([Employee_ID])
)
go


/******************
Insert Sample Data For The  Support_Ticket_Employee_Line table
***************/
print ''
Print '***Insert Sample Data For The  Support_Ticket_Employee_Line table***' 
 go

/******************
Create the [dbo].[Charter] table
***************/
print ''
Print '***Create the [dbo].[Charter] table***' 
 go


CREATE TABLE [dbo].[Charter]
(


    [Charter_ID] [int] IDENTITY(100000,1) not null unique	
,
    [Client_ID] [int] not null	
,
    [Client_Is_Passenger] [bit] not null	
,
    [Rider_Quantity] [int] not null	
,
    [Driver_Needed] [bit] not null	
,
    [Employee_ID] [int] not null	
,
    [Is_Approved] [bit] not null	
,
    [Date_Request_Start] [datetime] not null	
,
    [Date_Request_End] [datetime] not null	
,
    [Is_Active] [bit] not null	
,
    CONSTRAINT [PK_Charter] PRIMARY KEY ([Charter_ID])
,
    CONSTRAINT [AK_Charter_ID] UNIQUE([Charter_ID])
,
    CONSTRAINT [fk_Charter_Client0] foreign key ([Client_ID]) references [Client]([Client_ID])
,
    CONSTRAINT [fk_Charter_Employee1] foreign key ([Employee_ID]) references [Employee]([Employee_ID])
)
go

/******************
Insert Sample Data For The  Charter table
***************/
print ''
Print '***Insert Sample Data For The  Charter table***' 
 go

/******************
Create the [dbo].[Charter_Assignment] table
***************/
print ''
Print '***Create the [dbo].[Charter_Assignment] table***' 
 go


CREATE TABLE [dbo].[Charter_Assignment]
(


    [Assignment_ID] [int] identity(100000,1) not null unique	
,
    [Employee_ID] [int] null	
,
    [Charter_ID] [int] not null	
,
    [VIN] [nvarchar](17) not null unique	
,
    [Vehicle_Type_ID] [nvarchar](50) not null	
,
    [Date_Issued] [datetime] not null	
,
    [Date_Returned] [datetime] not null	
,
    [Is_Active] [bit] not null	
,
    CONSTRAINT [PK_Charter_Assignment] PRIMARY KEY ([Assignment_ID])
,
    CONSTRAINT [AK_Assignment_ID1] UNIQUE([Assignment_ID])
,
    CONSTRAINT [fk_Charter_Assignment_Driver0] foreign key ([Employee_ID]) references [Driver]([Employee_ID])
,
    CONSTRAINT [fk_Charter_Assignment_Charter1] foreign key ([Charter_ID]) references [Charter]([Charter_ID])
,
    CONSTRAINT [fk_Charter_Assignment_Vehicle2] foreign key ([VIN]) references [Vehicle]([VIN])
,
    CONSTRAINT [fk_Charter_Assignment_Vehicle_Type3] foreign key ([Vehicle_Type_ID]) references [Vehicle_Type]([Vehicle_Type_ID])
)
go

/******************
Insert Sample Data For The  Charter_Assignment table
***************/
print ''
Print '***Insert Sample Data For The  Charter_Assignment table***' 
 go

/******************
Create the [dbo].[Charter_Accommodation] table
***************/
print ''
Print '***Create the [dbo].[Charter_Accommodation] table***' 
 go


CREATE TABLE [dbo].[Charter_Accommodation]
(


    [Charter_ID] [int] IDENTITY(100000, 1)
,
    [Accommodation_ID] [nvarchar](100) not null	
,
    [Is_Active] [bit] not null	
,
    CONSTRAINT [PK_Charter_Accommodation] PRIMARY KEY ([Charter_ID] , [Accommodation_ID])
,
    CONSTRAINT [AK_Charter_ID1] UNIQUE([Charter_ID])
,
    CONSTRAINT [fk_Charter_Accommodation_Charter0] foreign key ([Charter_ID]) references [Charter]([Charter_ID])
,
    CONSTRAINT [fk_Charter_Accommodation_Accommodation1] foreign key ([Accommodation_ID]) references [Accommodation]([Accommodation_ID])
)
go

/******************
Insert Sample Data For The  Charter_Accommodation table
***************/
print ''
Print '***Insert Sample Data For The  Charter_Accommodation table***' 
 go

/******************
Create the [dbo].[Charter_Stop] table
***************/
print ''
Print '***Create the [dbo].[Charter_Stop] table***' 
 go


CREATE TABLE [dbo].[Charter_Stop]
(


    [Charter_Stop_ID] [int] identity(100000,1) not null	
,
    [Charter_ID] [int] not null	
,
    [Street_Address] [nvarchar](255) not null	
,
    [Zip_Code] [nvarchar](5) not null	
,
    [Latitude] [decimal] not null	
,
    [Longitude] [decimal] not null	
,
    [Duration] [int] not null	
,
    [Description] [nvarchar](255) not null	
,
    [Stop_Number] [int] not null	
,
    [Is_Active] [bit] not null	
,
    CONSTRAINT [PK_Charter_Stop] PRIMARY KEY ([Charter_Stop_ID])
,
    CONSTRAINT [fk_Charter_Stop_Charter0] foreign key ([Charter_ID]) references [Charter]([Charter_ID])
)
go

/******************
Insert Sample Data For The  Charter_Stop table
***************/
print ''
Print '***Insert Sample Data For The  Charter_Stop table***' 
 go

/******************
Create the [dbo].[Charter_Rider] table
***************/
print ''
Print '***Create the [dbo].[Charter_Rider] table***' 
 go


CREATE TABLE [dbo].[Charter_Rider]
(


    [Charter_ID] [int] not null	
,
    [Dependent_ID] [int] not null	
,
    [Is_Active] [bit] not null	
,
    CONSTRAINT [PK_Charter_Rider] PRIMARY KEY ([Charter_ID] , [Dependent_ID])
,
    CONSTRAINT [fk_Charter_Rider_Charter0] foreign key ([Charter_ID]) references [Charter]([Charter_ID])
,
    CONSTRAINT [fk_Charter_Rider_Dependent1] foreign key ([Dependent_ID]) references [Dependent]([Dependent_ID])
)
go

/******************
Insert Sample Data For The  Charter_Rider table
***************/
print ''
Print '***Insert Sample Data For The  Charter_Rider table***' 
 go

/******************
Create the [dbo].[Vehicle_Checklist] table
***************/
print ''
Print '***Create the [dbo].[Vehicle_Checklist] table***' 
 go


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
    [Cosmetic] [nvarchar] (500),
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
    CONSTRAINT [FK_Vehicle_Checklist_Employee] FOREIGN KEY([Employee_ID])
        REFERENCES [dbo].[Employee] ([Employee_ID]),
    CONSTRAINT [FK_Vehicle_Checklist_Vehicle] FOREIGN KEY([VIN])
        REFERENCES [dbo].[Vehicle] ([VIN]),
    CONSTRAINT [CPK_Vehicle_Checklist_Employee_Vehicle] PRIMARY KEY([Checklist_ID], [Employee_ID], [VIN]),
    CONSTRAINT [Checklist_ID] UNIQUE([Checklist_ID])
)
GO

/******************
Insert Sample Data For The  Vehicle_Checklist table
***************/
print ''
Print '***Insert Sample Data For The  Vehicle_Checklist table***' 
 go

SET IDENTITY_INSERT [dbo].[Vehicle_Checklist] ON
INSERT INTO [dbo].[Vehicle_Checklist]
    ([Checklist_ID], [Employee_ID], [VIN], [Date], [Cosmetic], [Tire_Pressure_DF], [Tire_Pressure_PF], [Tire_Pressure_DR], [Tire_Pressure_PR], [Mileage], [Fuel_Level], [Notes])
VALUES
    (100000, 100001, '1HGCM82633A123456', '2024-01-26 10:00:00', 'Minor scratches on front bumper', 32, 35, 34, 33, 25000, 75, 'Vehicle in good condition. Minor scratch on passenger door.'),
    (100001, 100002, '5XYZH4AG4JH123456', '2024-01-25 12:30:00', 'No cosmetic issues', 35, 35, 35, 35, 38000, 50, 'Headlight bulb on driver''s side needs replacement.'),
    (100002, 100003, 'JM1BK32F781234567', '2024-01-24 15:45:00', 'Small dent on passenger door', 30, 34, 32, 31, 12500, 90, 'Interior needs cleaning.'),
    (100003, 100004, 'WAUZZZ4G6BN123456', '2024-01-23 17:00:00', 'Scratches on rear windshield', 33, 32, 34, 35, 41000, 25, 'Brakes feel slightly worn. Recommend inspection.'),
    (100004, 100005, '1C4RJFAG5FC123456', '2024-01-22 09:30:00', 'Minor paint chips on hood', 31, 31, 33, 32, 15000, 80, 'No issues found.')
SET IDENTITY_INSERT [dbo].[Vehicle_Checklist] OFF
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
        CONSTRAINT [fk_Driver_Maintenance_Report_Driver] FOREIGN KEY ([Driver_ID])
        REFERENCES [dbo].[Driver] ([Employee_ID]),
    CONSTRAINT [fk_Driver_Maintenance_Report_VIN] FOREIGN KEY ([VIN])
        REFERENCES [dbo].[Vehicle] ([VIN]),
    CONSTRAINT [pk_Driver_Maintenance_Report] PRIMARY KEY ([Driver_Maintenance_Report_ID])
)
GO


/******************
Insert Sample Data For The  Driver_Maintenance_Report table
***************/

print ''
Print '***Insert Sample records for Driver_Maintenance_Report ***'
INSERT INTO [dbo].[Driver_Maintenance_Report]
    ([Driver_ID],[VIN],[Severity],[Description],[Is_Active])
VALUES
    (100000, '1HGCM82633A123456', 'medium', 'Left rear tire pressure low.', 1),
    (100001, '5XYZH4AG4JH123456', 'high', 'Engine overheating frequently', 1),
    (100002, 'JM1BK32F781234567', 'low', 'Windshield wiper fluid needs refill', 0),
    (100003, 'WAUZZZ4G6BN123456', 'medium', 'Brake pads showing wear, please check', 1),
    (100004, '1C4RJFAG5FC123456', 'low', 'Minor scratch on rear bumper', 0)
GO


/******************
Create The  Driver_Unavailable  table
***************/

print ''
print '*** Creating Driver_Unavailable table ***'
GO
CREATE TABLE [dbo].[Driver_Unavailable]
(
    [Unavailable_ID] [int]IDENTITY(100000, 1) NOT NULL,
    [Driver_ID] [int] NOT NULL,
    [Start_Datetime] [datetime] NOT NULL,
    [End_DateTime] [datetime] NOT NULL,
    [Reason] [nvarchar](250) NOT NULL DEFAULT '',
    [Is_Active] [bit] NOT NULL DEFAULT 1
        CONSTRAINT [FK_Driver_Unavailable_Driver_ID] FOREIGN KEY ([Driver_ID])
		REFERENCES [dbo].[Driver] ([Employee_ID]),
    CONSTRAINT [pk_Driver_Unavailable] PRIMARY KEY ([Unavailable_ID])
)
GO

/******************
Insert Sample Data For The  Driver_Unavailable  table
***************/

print '*** Inserting Sample Data into Driver_Unavailable ***'
GO
INSERT INTO [dbo].[Driver_Unavailable]
    ([Driver_ID],[Start_Datetime],[End_DateTime],[Reason],[Is_Active])
VALUES
    (100000, '2024-02-01 09:00:00', '2024-02-05 17:00:00', 'Annual leave', 1),
    (100001, '2024-02-10 08:00:00', '2024-02-12 18:00:00', 'Medical appointment', 1),
    (100002, '2024-02-15 07:30:00', '2024-02-20 16:00:00', 'Training session', 1),
    (100003, '2024-02-22 10:00:00', '2024-02-25 15:00:00', 'Family event', 1),
    (100004, '2024-03-01 12:00:00', '2024-03-03 20:00:00', 'Personal leave', 1)
GO


/******************
Create the [dbo].[Service] table
***************/
print ''
Print '***Create the [dbo].[Service] table***' 
 go


CREATE TABLE [dbo].[Service]
(
    [Service_ID] [nvarchar](20) not null,
    [Type] [nvarchar](20) not null,
    [Is_Active] [bit] DEFAULT 1 not null,
    CONSTRAINT [PK_Service] PRIMARY KEY ([Service_ID])
)
go

/******************
Insert Sample Data For The  Service table
***************/
print ''
Print '***Insert Sample Data For The  Service table***' 
go
INSERT INTO [dbo].[Service]
    ([Service_ID], [Type])
VALUES
    ('Replacing tires', 'manual'),
    ('Engine exploded', 'oopsie'),
    ('redoing paint', 'fun'),
    ('something leaking', 'wet'),
    ('car totaled', 'expensive')
go


/******************
Create the [dbo].[Service_Assignment] table
***************/
print ''
Print '***Create the [dbo].[Service_Assignment] table***' 
 go
CREATE TABLE [dbo].[Service_Assignment]
(
    [Service_Assignment_ID] [int] IDENTITY(100000,1) NOT NULL,
    [VIN] [nvarchar](17) NOT NULL,
    [Service_ID] [nvarchar](20) NOT NULL,
    [Driver_ID] [int] NOT NULL,
    [Start_Datetime] [datetime] NULL,
    [End_Datetime] [datetime] NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [PK_Service_Assignment] PRIMARY KEY ([Service_Assignment_ID]),
    CONSTRAINT [FK_Service_Assignment_Vehicle] FOREIGN KEY ([VIN]) 
        REFERENCES [Vehicle]([VIN]),
    CONSTRAINT [FK_ServiceAssignment_Service] FOREIGN KEY ([Service_ID]) 
        REFERENCES [Service]([Service_ID]),
    CONSTRAINT [FK_ServiceAssignment_Driver] FOREIGN KEY ([Driver_ID]) 
        REFERENCES [Driver]([Employee_ID]),
)

go

/******************
Insert Sample Data For The  Service_Assignment table
***************/
print ''
Print '***Insert Sample Data For The  Service_Assignment table***' 
 go
INSERT INTO [dbo].[Service_Assignment]
    ([VIN], [Service_ID],
    [Driver_ID], [Start_Datetime], [End_Datetime], [Is_Active]
    )
VALUES
    ('1HGCM82633A123456', 'Replacing tires', 100000, '2024-01-20 08:00:00', '2024-01-20 12:00:00', 1),
    ('5XYZH4AG4JH123456', 'Engine exploded', 100001, '2024-02-15 10:00:00', '2024-02-15 14:00:00', 1),
    ('JM1BK32F781234567', 'redoing paint', 100002, '2024-03-10 09:00:00', '2024-03-10 13:00:00', 0),
    ('WAUZZZ4G6BN123456', 'something leaking', 100003, '2024-04-05 11:00:00', '2024-04-05 15:00:00', 1),
    ('1C4RJFAG5FC123456', 'car totaled', 100004, '2024-05-01 13:00:00', '2024-05-01 17:00:00', 1);
GO

/******************
Create the [dbo].[Ride] table
***************/
print ''
Print '***Create the [dbo].[Ride] table***' 
go
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
    CONSTRAINT [PK_Ride] PRIMARY KEY([Ride_ID]),
    CONSTRAINT [FK_Ride_Client_ID_Client_Client_ID]
    FOREIGN KEY([Client_ID]) REFERENCES [dbo].[Client]([Client_ID]),
    CONSTRAINT [FK_Ride_Service_ID_Service_Service_ID]
    FOREIGN KEY([Service_ID]) REFERENCES [dbo].[Service]([Service_ID]),
    CONSTRAINT [FK_Ride_Service_Assignment_ID_Service_Assignment_Service_Assignment_ID]
    FOREIGN KEY([Service_Assignment_ID]) REFERENCES [dbo].[Service_Assignment]([Service_Assignment_ID])
);
go

/******************
Insert Sample Data For The  Ride table
***************/
print ''
Print '***Create the [dbo].[Source] table***' 
 GO


CREATE TABLE [dbo].[Source](


[Vendor_Id]	[int]	not null	
,[Parts_inventory_id]	[int]	not null	
,[Vendor_Part_Number]	[nvarchar](100)	not null	
,[Estimated_delivery_time_days]	[int]	not null	
,[Part_Price]	[smallmoney]	not null	
,[Minimum_order_Qty]	[int]	not null	
,[Active]	[bit]	not null	
,CONSTRAINT [CPK_Source] PRIMARY KEY ([Vendor_Id] , [Parts_inventory_id])
,CONSTRAINT [fk_Source_Vendor0] foreign key ([Vendor_ID]) references [Vendor]([Vendor_ID])
,CONSTRAINT [fk_Source_Parts_Inventory1] foreign key ([Parts_inventory_id]) references [Parts_Inventory]([Parts_Inventory_ID])
)
go

/******************
Insert Sample Data For The  Source table
***************/
print ''
Print '***Insert Sample Data For The  Source table***' 
 GO
 
 INSERT INTO [dbo].[Source] ([Vendor_Id], [Parts_inventory_id], [Vendor_Part_Number], [Estimated_delivery_time_days], [Part_Price], [Minimum_order_Qty], [Active])
 VALUES 
 (100000, 100000, N'Some Fastner', 5, CAST(1.0000 AS SmallMoney), 2, 1)
  ,(100000, 100001, 'Some Bolt', 5, CAST(1.0000 AS SmallMoney), 2, 1)
  ,(100000, 100002, 'Some Grommet', 5, CAST(3.0000 AS SmallMoney), 2, 1)
  ,(100000, 100003, 'Some Cable', 5, CAST(4.0000 AS SmallMoney), 2, 1)
  ,(100000, 100004, 'Some Frame', 5, CAST(5.0000 AS SmallMoney), 2, 1)
  ,(100000, 100005, 'Some Washer', 5, CAST(6.0000 AS SmallMoney), 2, 1)
  ,(100001, 100006, 'Some Screw', 5, CAST(7.0000 AS SmallMoney), 2, 1)
  ,(100001, 100007, 'Some Stripper', 5, CAST(8.0000 AS SmallMoney), 2, 1)
  ,(100001, 100008, 'Some Nails', 5, CAST(9.0000 AS SmallMoney), 2, 1)
  ,(100001, 100009, 'Some Tires', 5, CAST(10.0000 AS SmallMoney), 2, 1)
  ,(100001, 100010, 'Some Tires', 5, CAST(11.0000 AS SmallMoney), 2, 1)
  ,(100001, 100011, 'Some Axel', 5, CAST(12.0000 AS SmallMoney), 2, 1)
  ,(100002, 100012, 'Some Struts', 5, CAST(13.2500 AS SmallMoney), 2, 1)
  ,(100002, 100013, 'Some Hubcaps', 5, CAST(14.2500 AS SmallMoney), 2, 1)
  ,(100003, 100000, 'Some Fastner', 5, CAST(35.2500 AS SmallMoney), 2, 1)
  ,(100003, 100001, 'Some Bolt', 5, CAST(16.2500 AS SmallMoney), 2, 1)
  ,(100003, 100002, 'Some Grommet', 5, CAST(17.2500 AS SmallMoney), 2, 1)
  ,(100003, 100003, 'Some Cable', 5, CAST(21.2500 AS SmallMoney), 2, 1)
  ,(100003, 100008, 'Some Nails', 5, CAST(26.2500 AS SmallMoney), 2, 1)
  ,(100003, 100009, 'Some Tires', 5, CAST(27.2500 AS SmallMoney), 2, 1)
  ,(100003, 100010, 'Some Tires', 5, CAST(31.2500 AS SmallMoney), 2, 1)
  ,(100003, 100011, 'Some Axels', 5, CAST(32.2500 AS SmallMoney), 2, 1)
  ,(100003, 100012, 'Some Struts', 5, CAST(33.2500 AS SmallMoney), 2, 1)
  ,(100003, 100013, 'Some Hubcaps', 5, CAST(15.2500 AS SmallMoney), 2, 1)
  ,(100004, 100004, 'Some Frame', 5, CAST(22.2500 AS SmallMoney), 2, 1)
  ,(100004, 100005, 'Some Washer', 5, CAST(23.2500 AS SmallMoney), 2, 1)
  ,(100004, 100006, 'Some Screw', 5, CAST(24.2500 AS SmallMoney), 2, 1)
  ,(100004, 100007, 'Some Stripper', 5, CAST(25.2500 AS SmallMoney), 2, 1)
go








/******************
Create the [dbo].[Vehicle_Unavailable] table
***************/
print ''
Print '***Create the [dbo].[Vehicle_Unavailable] table***' 
 go


CREATE TABLE [dbo].[Vehicle_Unavailable]
(
    [Unavailable_ID] [int] IDENTITY	(1,	1) NOT NULL,
    [VIN] [nvarchar](17) NOT NULL /*Vehicle.VIN	*/,
    [Start_Datetime] [datetime] NOT NULL,
    [End_Datetime] [datetime] NOT NULL,
    [Reason] [nvarchar](1000) NOT NULL,
    [is_active] [bit] DEFAULT 1 NOT NULL

        CONSTRAINT [pk_vehicleUnavailable] PRIMARY KEY([Unavailable_ID]),
    CONSTRAINT [ak_vehicleUnavailable] UNIQUE([Unavailable_ID]),
    CONSTRAINT [fk_vehicleUnavailable_vehicle] FOREIGN KEY
    ([VIN])
        REFERENCES [dbo].[Vehicle]
    ([VIN])
);
GO
go

/******************
Insert Sample Data For The  Vehicle_Unavailable table
***************/
print ''
Print '***Insert Sample Data For The  Vehicle_Unavailable table***' 
 go

INSERT INTO [dbo].[Vehicle_Unavailable]
    ([VIN], [Start_Datetime], [End_Datetime], [Reason])
VALUES
    ('1HGCM82633A123456', '2023-01-28', '2023-01-28', 'Test unavailablitly'),
    ('5XYZH4AG4JH123456', '2023-01-28', '2023-01-28', 'Test unavailablitly'),
    ('JM1BK32F781234567', '2023-01-28', '2023-01-28', 'Test unavailablitly'),
    ('WAUZZZ4G6BN123456', '2023-01-28', '2023-01-28', 'Test unavailablitly'),
    ('1C4RJFAG5FC123456', '2023-01-28', '2023-01-28', 'Test unavailablitly')
;
GO

/******************
Create The Vehicle Driver Table
***************/

print ''
print '*** Creating Vehicle_Driver Table***'
CREATE TABLE [dbo].[Vehicle_Driver]
(
    [Vehicle_Driver_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Driver_ID] [int] NULL,
    [VIN] [nvarchar](17) NOT NULL,
    [Date_Assignment_Started] [date] NOT NULL,
    [Date_Assignment_Ended] [date] NULL,
    [Notes] [nvarchar](255) NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [PK_Vehicle_Driver] PRIMARY KEY ([Vehicle_Driver_ID]),
    CONSTRAINT [FK_Vehicle_Driver_Driver]  FOREIGN KEY ([Driver_ID]) 
        REFERENCES [Driver]([Employee_ID]),
    CONSTRAINT [FK_Vehicle_Driver_Vehicle] FOREIGN KEY ([VIN])
        REFERENCES [Vehicle]([VIN])
)
GO

/******************
Insert Sample Data For The  Vehicle_Driver table
***************/
print ''
print '*** Inserting Vehicle_Driver Data Sample ***'
GO
INSERT INTO [dbo].[Vehicle_Driver]
    ([Driver_ID], [VIN], [Date_Assignment_Started], [Date_Assignment_Ended], [Notes], [Is_Active])
VALUES
    (100000, '1HGCM82633A123456', '2024-01-15', NULL, 'Assignment started on Jan 15', 1),
    (100001, '5XYZH4AG4JH123456', '2024-02-01', '2024-02-15', 'Assignment from Feb 1 to Feb 15', 1),
    (100002, 'JM1BK32F781234567', '2024-03-01', NULL, 'Assignment started on Mar 1', 1),
    (100003, 'WAUZZZ4G6BN123456', '2024-04-01', '2024-04-15', 'Assignment from Apr 1 to Apr 15', 1),
    (100004, '1C4RJFAG5FC123456', '2024-05-01', NULL, 'Assignment started on May 1', 1);
go

/******************
Create the Password_Reset table
***************/

print ''
print '*** Creating Password_Reset Table***'
CREATE TABLE [dbo].[Password_Reset] (
    [Password_Reset_ID] [int] IDENTITY(100000,1) NOT NULL,
    [Username] [nvarchar](50) NOT NULL,
    [Request_Datetime] [datetime] NOT NULL DEFAULT CURRENT_TIMESTAMP,
    [Verification_Code] [char](6) NOT NULL,
    [Is_Active] [bit] NOT NULL DEFAULT 1,
    CONSTRAINT [PK_Password_Reset] PRIMARY KEY ([Password_Reset_ID]),
    CONSTRAINT [FK_Password_Reset_Username_Login_Username]  FOREIGN KEY ([Username]) 
        REFERENCES [Login]([Username])
);
GO
