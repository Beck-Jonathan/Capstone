USE Night_Rider;
GO

-- Sample data for [Zipcode] is in SQL/tbl_Zipcode.sql

/******************
Insert Sample Data For The  Vendor table
***************/
print ''
Print '***Insert Sample Data For The  Vendor table***' 
GO 
INSERT INTO [dbo].[Vendor]
    ([Vendor_Name], [Vendor_Contact_Given_Name], [Vendor_Contact_Family_Name], [Vendor_Contact_Phone_Number], [Vendor_Contact_Email], [Vendor_Phone_Number], [Vendor_Address], [Vendor_Address2], [Vendor_City], [Vendor_State], [Vendor_Country], [Vendor_Zip])
VALUES
    ('VendorOne', 'VOneGiven', 'VOneFamily', '319-555-1111', 'contact@vendorone.com', '319-555-1112', '123 VendOne St', 'Unit 1', 'VOneCity', 'AA', 'USA', 'VOneZip'),
    ('VendorTwo', 'VTwoGiven', 'VTwoFamily', '319-555-2222', 'contact@vendortwo.com', '319-555-2222', '123 VendTwo St', 'Unit 2', 'VTwoCity', 'BB', 'USA', 'VTwoZip'),
    ('VendorThree', 'VThreeGiven', 'VThreeFamily', '319-555-3333', 'contact@vendorthree.com', '319-555-3332', '123 VendThree St', 'Unit 3', 'VThreeCity', 'CC', 'USA', 'VThreeZip'),
    ('VendorFour', 'VFourGiven', 'VFourFamily', '319-555-4444', 'contact@vendorfour.com', '319-555-4442', '123 VendFour St', 'Unit 4', 'VFourCity', 'DD', 'USA', 'VFourZip'),
    ('VendorFive', 'VFiveGiven', 'VFiveFamily', '319-555-5555', 'contact@vendorfive.com', '319-555-5552', '123 VendFive St', 'Unit 5', 'VFiveCity', 'EE', 'USA', 'VFiveZip')
GO

/******************
Insert Sample Data For The  Purchase_Order table
***************/
print '' 
Print '***Insert Sample Data For The  Purchase_Order table***' 
GO
INSERT INTO [dbo].[Purchase_Order]
	([Vendor_ID],[Purchase_Order_Date],[Delivery_Address],[Delivery_Address2],[Delivery_City],[Delivery_State],[Delivery_Country],[Delivery_Zip])
VALUES
	(100000,'2022-05-05','123 fake street','','Cedar Rapids','IA','USA','52402'),
	(100001,'2022-05-06','456 fake road','','Longmeadow','MA','USA','01106'),
	(100002,'2022-05-07','789 fake court','Apt #3','Pittsburgh','PA','USA','15252'),
	(100003,'2022-05-08','321 faker street','Apt #45','Laurel','MS','USA','39440'),
	(100004,'2022-05-09','654 fake road','','Nashport','OH','USA','43830'),
	(100004,'2022-05-10','987 fake court','Apt #228','Seymour','IL','USA','61875'),
	(100003,'2022-05-11','100 phoney way','Apt #953','Fort Gibson','OK','USA','74434')
GO

/******************
Insert Sample Data For The  Packing_Slip table
***************/
print ''
Print '***Insert Sample Data For The  Packing_Slip table***' 
GO
INSERT INTO [dbo].[Packing_Slip]
    ([Purchase_Order_ID], [Recieving_Notes], [Vendor_ID], [Creation_Date])
VALUES
    (100000, 'All is good.', 100001, GETDATE()),
    (100001, 'One box was broken', 100002, GETDATE()),
    (100002, 'Everything was unharmed in shipping', 100002, GETDATE()),
    (100003, 'Something''s wrong i can feel it', 100003, GETDATE()),
    (100004, 'Hello!', 100004, GETDATE())
GO

/******************
Insert Sample Data For The  Parts_Inventory table
***************/
print ''
Print '***Insert Sample Data For The  Parts_Inventory table***' 
GO
INSERT INTO [dbo].[Parts_Inventory] 
	([Part_Name], [Item_Description], [Item_Specifications], [Part_Photo_URL])
VALUES
    ( 'Fastner A', 'Standard widget', 'Dimensions: 5cm x 3cm x 2cm, Material: Steel', 'https://4.imimg.com/data4/EN/PJ/MY-7251967/hex-head-bolts-500x500.jpg'),
    ( 'Bolt B', 'Heavy-duty bolt', 'Thread size: M10, Length: 50mm, Material: Stainless steel', 'https://www.iqsdirectory.com/articles/bolts/types-of-bolts/bolts.jpg'),
    ( 'Grommet C', 'Rubber grommet for wire protection', 'Diameter: 10mm, Material: Rubber', 'https://images.thdstatic.com/productImages/b2da35ad-9c9b-4f63-87ee-f2f920303f18/svn/everbilt-grommets-812038-64_600.jpg'),
    ( 'Cable D', 'Power cable with 3-prong plug', 'Length: 2 meters, Gauge: 18 AWG', 'https://images.thdstatic.com/productImages/345abf4e-1320-4c4d-bf13-812bf6b841d0/svn/syston-cable-technology-data-cables-1588-sb-bk-100-64_1000.jpg'),
    ( 'Frame E', 'Aluminum mounting frame', 'Dimensions: 30cm x 20cm, Material: Aluminum', 'https://spn-sta.spinny.com/blog/20220228141808/ezgif.com-gif-maker-90-1.jpg?compress=true&quality=80&w=1200&dpr=2.6'),
    ( 'Washer B', '1/8 washer', ' Material: Stainless steel', 'https://images.thdstatic.com/productImages/cddb3606-9a40-4149-a4bb-a23fe749a2f4/svn/everbilt-flat-washers-842348-64_1000.jpg'),
    ( 'Screw C', 'Metal Screws', 'Diameter: 10mm, Material: Steel', 'https://m.media-amazon.com/images/I/61tO5ExJfxL.jpg'),
    ( 'Wire Stripper D', 'Wire Stripper', 'Length: 2 meters', 'https://m.media-amazon.com/images/I/61C-UqAIndL.jpg'),
    ( 'Nails E', 'Wood Nails', 'Dimensions: 30cm x 20cm, Material: Aluminum', 'https://m.media-amazon.com/images/I/81RoBbXE7RL._AC_UF894,1000_QL80_.jpg'),
	 ( 'Tires A', 'Sedan Tires', 'Dimensions: 5cm x 3cm x 2cm, Material: rubber', 'https://m.media-amazon.com/images/I/81bkWoDhtKL.jpg'),
    ( 'Tires B', 'Bus Tires', 'Thread size: M10, Length: 50mm, Material: Rubber', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTtNwGpQYMQ9m0EIw1ZKM0IcwAwh17TznAv1Rtysc-nc5r7JGEc3h3IW7vf14jQIr0VGxA&usqp=CAU'),
    ( 'Axel A', 'Mazda 3 Rear Axel', 'Diameter: 10mm, Material: Steel', 'https://m.media-amazon.com/images/I/51Q6MN6ArnL._AC_UF894,1000_QL80_.jpg'),
    ( 'Struts', 'VW Bug front strut', 'Length: 2 meters, Gauge: 18 AWG', 'https://m.media-amazon.com/images/I/71XlY5CA3AL.jpg'),
    ( 'Hubcaps ', 'Sedan Hubcap', 'Dimensions: 30cm x 20cm, Material: Aluminum', 'https://m.media-amazon.com/images/I/613dx6sjZyL._AC_UF894,1000_QL80_.jpg')
GO

/******************
Insert Sample Data For The  Purchase_Order_Line_Item table
***************/
print ''
Print '***Insert Sample Data For The  Purchase_Order_Line_Item table***' 
GO
INSERT INTO [dbo].[Purchase_Order_Line_Item](
	[Purchase_Order_ID], [Parts_Inventory_ID], [Line_Number], [Line_Item_Name], [Line_Item_Qty],[Line_Item_Price], [Line_Item_Description])
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
Insert Sample Data For The  Route table
***************/
print ''
Print '***Insert Sample Data For The  Route table***' 
GO
INSERT INTO [dbo].[Route]
    ([Route_Name], [Route_Start_Time], [Route_Cycle], [Route_End_Time], [Days_Of_Service])
VALUES
    ('Cedar Rapids Northeast', '1900-01-01 05:00:00', '01:30:00', '1900-01-01 20:00:00', '0111100'),
    ('Cedar Rapids Southwest', '1900-01-01 05:00:00', '02:00:00', '1900-01-01 20:00:00', '0111100'),
    ('Hiawatha', '1900-01-01 06:30:00', '01:00:00', '1900-01-01 19:00:00', '0111111'),
    ('Marion', '1900-01-01 09:00:00', '00:30:00', '1900-01-01 18:30:00', '0010100'),
    ('Center Point', '1900-01-01 05:30:00', '01:00:00', '1900-01-01 22:00:00', '0111110')
GO

/******************
Insert Sample Data For The  Stop table
***************/
print ''
Print '***Insert Sample Data For The  Stop table***' 
GO
INSERT INTO [dbo].[Stop]
    ([Street_Address], [Zip_Code], [Latitude], [Longitude])
VALUES
    ('Lindale Mall', '52302', 42.027780, -91.629940),
    ('Downtown CR', '52402', 41.978050, -91.669860),
    ('Marcus Cedar Rapids Cinema', '52302', 42.032038, -91.655243),
    ('Guthridge Park, Hiawatha', '52233', 42.040838, -91.681471),
    ('Mount Trashmore', '52402', 41.96175086599706, -91.65086473447506)
GO

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
GO

/******************
Insert Sample Data For The  Vehicle_Type table
***************/
print ''
Print '***Insert Sample Data For The  Vehicle_Type table***' 
GO
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
GO

/******************
Insert Sample Data For The  Vehicle_Model table
***************/
print ''
Print '***Insert Sample Data For The  Vehicle_Model table***' 
GO
INSERT INTO [dbo].[Vehicle_Model]
    ([Max_Passengers], [Make], [Name], [Year])
VALUES
    (5, 'Toyota', 'Camry', 2023),
    (7, 'Honda', 'Accord', 2022),
    (4, 'Ford', 'Escape', 2021),
    (2, 'Chevrolet', 'Spark', 2020),
    (8, 'Nissan', 'Altima', 2019)
GO

/******************
Insert Sample Data For The  Vehicle table
***************/
print ''
Print '***Insert Sample Data For The  Vehicle table***' 
GO
INSERT INTO [dbo].[Vehicle]
    ([VIN], [Vehicle_Number], [Vehicle_Mileage], [Vehicle_License_Plate],
    [Vehicle_Model_ID], [Vehicle_Type_ID], [Date_Entered], [Description], [Rental], [Is_Active])
VALUES
    ('1HGCM82633A123456', 'VH123', 50000, 'ABC123', 100000, 'City Bus', '2024-01-15', 'Description', 0, 1),
    ('5XYZH4AG4JH123456', 'VH456', 60000, 'XYZ789', 100001, 'School Bus', '2024-02-15', 'Description', 1, 1),
    ('JM1BK32F781234567', 'VH789', 75000, 'MJK456', 100002, 'Van', '2024-03-15', 'Description', 0, 3),
    ('WAUZZZ4G6BN123456', 'VH101', 40000, 'WAU789', 100003, 'Truck', '2024-04-15', 'Description', 1, 4),
    ('1C4RJFAG5FC123456', 'VH202', 55000, 'JFA567', 100004, 'City Bus', '2024-05-15', 'Description', 1, 1) 
GO

/******************
Insert Sample Data For The  Employee table
***************/
print ''
Print '***Insert Sample Data For The  Employee table***' 
GO
INSERT INTO [dbo].[Employee]
    ([Given_Name],[Family_Name],[DOB],[Address],[City],[State],[Country],[Zip],
    [Phone_Number],[Email],[Position])
VALUES
		('John', 'Smith', '2006-11-01',
        '132 Nowhere Ave', 'Suffolk', 'VA', 'USA',
        '23432', '1575011049', 'John@company.com', 'Mechanic'),
		('Dylan', 'Linkelvetch', '1953-02-07',
        '158 Real Pl', 'Iowa City', 'IA', 'USA',
        '52245', '3191231234', 'Dylan@company.com', 'Driver'),
		('Gunter', 'Schneider', '1988-04-01',
        '240 Root St', 'Casa Blanca', 'NM', 'USA',
        '87007', '1231231234', 'Gunter@company.com', 'Fleet Admin'),
		('Marissa', 'Graham', '2001-02-08',
        '512 Nix ln ', 'Juneau', 'AK', 'USA',
        '99801', '9879871234', 'Marissa@company.com', 'Maintenance'),
		('Auri', 'Koskinen', '1982-11-01',
        '007 Secret St', 'Sturgeon Bay', 'WI', 'USA',
        '54235', '0095542367', 'Auri@company.com', 'Mechanic'),
		('Linda', 'Flynn', '1968-10-31',
        '879 Perry Ave', 'Suffield', 'CT', 'USA',
        '06080', '3194105910', 'Linda@company.com', 'PositionName'),
		('Francis', 'Polesmith', '1984-01-04',
        '51 Joust Ln', 'Pierre', 'SD', 'USA',
        '57501', '4191023103', 'Francis@company.com', 'PositionName'),
		('Theseus', 'Slayer', '1985-03-31',
        '151 Antimino Pl', 'Athens', 'TX', 'USA',
        '75751', '5710150113', 'Theseus@company.com', 'PositionName'),
		('Trisha', 'Hallows', '1988-06-06',
        '132 Nowhere Ave', 'Baton Rouge', 'LA', 'USA',
        '70801', '2257501049', 'Trisha@company.com', 'PositionName'),
		('Justin', 'Time', '2002-12-25',
        '510 Clock Circle', 'Boulevard', 'CA', 'USA',
        '91905', '5961924091', 'Justin@company.com', 'PositionName')
GO

/******************
Insert Sample Data For The  Role table
***************/
print '' 
Print '***Insert Sample Data For The  Role table***' 
GO
INSERT INTO [dbo].[Role]
	([Role_ID], [Role_Description]) 
VALUES 
	('Admin', 'Manages entire application, typically aids in system setup.'),
	('FleetAdmin', 'Manages the fleet'),
	('Mechanic', 'Fixes the vehicles'),
	('Maintenance', 'Routine maintenance work that doesnt require mechanic'),
	('PartsPerson', 'An invetory specialist, that is the go to for any parts for vehicles')
GO

/******************
Insert Sample Data For The  Employee_Role table
***************/
print ''
print '***Insert Sample Data For The  Employee_Role table***'
GO
INSERT INTO [dbo].[Employee_Role]
    ([Employee_ID], [Role_ID])
VALUES
    (100000, 'Admin'),
    (100001, 'FleetAdmin'),
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
Insert Sample Data For The  Driver_License_Class table
***************/
print ''
print '***Insert Sample Data For The  Driver_License_Class table***'
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
Insert Sample Data For The  Driver table
***************/
print ''
Print '***Insert Sample Data For The  Driver table***' 
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
Insert Sample Data For The  Schedule table
***************/
print ''
Print '***Insert Sample Data For The  Schedule table***' 
GO
INSERT INTO [dbo].[Schedule]
    ([Schedule_ID], [Driver_ID],[Week_Days],
    [Start_Time],[End_Time],[Start_Date], [End_Date],[Notes],[Is_Active])
VALUES
    ('SCH1', 100000, '1111100', '08:00:00', '17:00:00', '2024-01-01', NULL, 'Notes here', 1),
    ('SCH2', 100001, '0001111', '09:00:00', '18:00:00', '2024-02-01', '2024-02-15', 'Notes here', 1),
    ('SCH3', 100002, '1110011', '10:00:00', '19:00:00', '2024-03-01', '2024-03-15', 'Notes...', 0),
    ('SCH4', 100003, '0101010', '07:30:00', '16:30:00', '2024-04-01', '2024-04-15', 'Notes...', 1),
    ('SCH5', 100004, '1111111', '12:00:00', '21:00:00', '2024-05-01', '2024-05-15', 'Notes...', 1)
GO

/******************
Insert Sample Data For The  Route_Assignment table
***************/
print ''
Print '***Insert Sample Data For The  Route_Assignment table***' 
GO
INSERT INTO [dbo].[Route_Assignment]
    ([Driver_ID], [Route_ID], [VIN], [Date_Assignment_Started], [Date_Assignment_Ended])
VALUES
    (100000, 100000, '1HGCM82633A123456', '2023-11-01', NULL),
    (100001, 100000, '5XYZH4AG4JH123456', '2023-12-15', '2024-01-05'),
    (100002, 100001, 'JM1BK32F781234567', '2024-01-20', NULL),
    (100003, 100002, 'WAUZZZ4G6BN123456', '2022-04-23', '2023-02-19'),
    (100004, 100003, '1C4RJFAG5FC123456', '2023-07-12', '2023-09-06')
GO

/******************
Insert Sample Data For The  Route_Fulfillment table
***************/
print ''
Print '***Insert Sample Data For The  Route_Fulfillment table***' 
go
INSERT INTO [dbo].[Route_Fulfillment]
    ([Assignment_ID], [Actual_Driver_ID], [Actual_VIN], [Start_Time], [End_Time])
VALUES
    (100000, 100000, '1HGCM82633A123456', '2023-11-01 11:01:37.000', '2023-11-01 18:31:37.000'),
    (100001, 100000, '5XYZH4AG4JH123456', '2023-12-15 12:15:37.000', '2023-12-15 19:54:15.000'),
    (100002, 100001, 'JM1BK32F781234567', '2024-01-20 13:20:20.000', '2024-01-20 18:20:20.000'),
    (100003, 100002, 'WAUZZZ4G6BN123456', '2022-04-23 16:23:23.000', '2022-04-23 20:23:23.000'),
    (100004, 100003, '1C4RJFAG5FC123456', '2023-07-12 19:12:12.000', '2023-07-12 22:12:12.000')
go

/******************
Insert Sample Data For The  Safety_Report table
***************/
print ''
Print '***Insert Sample Data For The  Safety_Report table***' 
GO
INSERT INTO [dbo].[Safety_Report]
    ([Employee_ID], [Date], [Time_Of_Event], [Affected_Party], [Description])
VALUES
    (100000, '2023-01-29', '2023-01-28', 'Passenger', 'Fell out door'),
    (100001, '2023-01-29', '2023-01-28', 'Pedestrian', 'Hit by bus'),
    (100001, '2023-01-29', '2023-01-28', 'Passenger', 'Hit other passenger'),
    (100003, '2023-01-29', '2023-01-28', 'Passenger', 'Fell down aisle'),
    (100005, '2023-01-29', '2023-01-28', 'Self', 'Tripped on steps')
GO

/******************
Insert Sample Data For The  Refuel_Log table
***************/
print ''
Print '***Insert Sample Data For The  Refuel_Log table***' 
GO
INSERT INTO [dbo].[Refuel_Log]
    ([Driver_ID], [VIN], [Date_Time], [Mileage], [Fuel_Quantity], [Fuel_Price_Per_Gal], [Total_Sale], [Notes])
VALUES
    (100002, '1HGCM82633A123456', '2023-01-28', 231254, 10, 2.8, 28, 'Notes'),
    (100002, '5XYZH4AG4JH123456', '2023-01-28', 231254, 10, 2.8, 28, 'Notes'),
    (100003, 'JM1BK32F781234567', '2023-01-28', 231254, 10, 2.8, 28, 'Notes'),
    (100000, 'WAUZZZ4G6BN123456 ', '2023-01-28', 231254, 10, 2.8, 28, 'Notes'),
    (100001, '1C4RJFAG5FC123456', '2023-01-28', 231254, 10, 2.8, 28, 'Notes')
GO

/******************
Insert Sample Data For The  Service_Type table
***************/
print ''
Print '***Insert Sample Data For The  Service_Type table***' 
GO
INSERT INTO [dbo].[Service_Type]
    ([Service_Type_ID], [Service_Description])
VALUES
    ('ST001', 'General Maintenance'),
    ('ST002', 'Repair'),
    ('ST003', 'Installation'),
    ('ST004', 'Upgrade'),
    ('ST005', 'Troubleshooting')
GO

/******************
Insert Sample Data For The  Maintenance_Schedule table
***************/
print ''
print '***Insert Sample Data For The  Maintenance_Schedule table***'
GO
INSERT INTO [dbo].[Maintenance_Schedule]
    ([Maintenance_Schedule_ID],[Vehicle_Model_ID],[Service_Type_ID],[Frequency_In_Months],[Frequency_In_Miles],[Is_Completed],[Active])
VALUES
    (100001, 100001, 'ST001', 6, 5000, 0, 1),
    (100002, 100001, 'ST002', 12, 10000, 1, 1),
    (100003, 100002, 'ST003', 18, NULL, 0, 1),
    (100004, 100003, 'ST004', 12, NULL, 0, 1),
    (100005, 100002, 'ST005', 6, NULL, 1, 1)
GO

/******************
Insert Sample Data For The  Service_Line_Item table
***************/
print ''
Print '***Insert Sample Data For The  Service_Line_Item table***' 
GO
INSERT INTO [dbo].[Service_Line_Item]
    ([Service_Line_Item_ID], [Parts_Inventory_ID], [Quantity])
VALUES
    (100000, 100000, 2),
    (100001, 100001, 3),
    (100002, 100002, 1),
    (100003, 100003, 4),
    (100004, 100004, 5)
GO

/******************
Insert Sample Data For The  Service_Order table
***************/
print ''
Print '***Insert Sample Data For The  Service_Order table***' 
GO
INSERT INTO [dbo].[Service_Order]
    ([Service_Order_ID], [Service_Order_Version], [VIN], [Service_Type_ID], [Created_By_Employee_ID], [Date_Started], [Date_Finished])
VALUES
    (100000, 1, '1HGCM82633A123456', 'ST001', 100001, '2024-01-25 10:00:00', '2024-01-25 12:00:00'),
    (100001, 1, '5XYZH4AG4JH123456', 'ST002', 100002, '2024-01-24 14:30:00', '2024-01-24 16:45:00'),
    (100002, 1, 'JM1BK32F781234567', 'ST003', 100003, '2024-01-23 09:15:00', '2024-01-23 11:00:00'),
    (100003, 1, 'WAUZZZ4G6BN123456', 'ST004', 100004, '2024-01-22 15:00:00', '2024-01-22 17:30:00'),
    (100004, 1, '1C4RJFAG5FC123456', 'ST005', 100005, '2024-01-21 08:45:00', null)
GO

/******************
Insert Sample Data For The  Service_Line table
***************/
-- Service_Line table sample data here

/******************
Insert Sample Data For The  Special_Service_Order table
***************/
print ''
Print '***Insert Sample Data For The  Special_Service_Order table***' 
GO
INSERT INTO [dbo].[Special_Service_Order]
    ([Service_Order_ID], [Service_Order_Version],[Event_Description],
    [Priority],[Is_Active])
VALUES
    (100000, 1, 'Hit a Tree', 0, 1),
    (100001, 1, 'Hit a Bus', 1, 1)
GO

/******************
Insert Sample Data For The  Special_Inspection table
***************/
print ''
Print '***Insert Sample Data For The  Special_Inspection table***' 
GO
INSERT INTO [dbo].[Special_Inspection]
    ([Special_Service_Order_ID], [Inspection_Description],[Date],
    [Employee_ID],[Is_Active])
VALUES
    (100000, 'Looks Bad', GETDATE(), 100000, 1),
    (100000, 'Probably the Radiator', GETDATE(), 100001, 1),
    (100000, 'Might be more!', GETDATE(), 100002, 1),
    (100001, 'It will be fine', GETDATE(), 100000, 1),
    (100001, 'It is broken', GETDATE(), 100001, 1)
GO

/******************
Insert Sample Data For The  Bid table
***************/
print ''
Print '***Insert Sample Data For The  Bid table***' 
GO
INSERT INTO [dbo].[Bid]
    ([Special_Service_Order_ID], [Vendor_ID],[Bid_Description],
    [Date],[Amount],[Is_Approved], [Is_Active])
VALUES
    (100000, 100000, 'Repair', GETDATE(), 450, 0, 1),
    (100000, 100001, 'Repair', GETDATE(), 460, 0, 1),
    (100000, 100002, 'Repair', GETDATE(), 470, 0, 1),
    (100001, 100001, 'Disposal', GETDATE(), 40, 0, 1),
    (100001, 100002, 'Disposal', GETDATE(), 60, 0, 1)
GO

/******************
Insert Sample Data For The  Special_Work_Order table
***************/
print ''
Print '***Insert Sample Data For The  Special_Work_Order table***' 
GO
INSERT INTO [dbo].[Special_Work_Order]
    ([Bid_ID], [Work_Order_Description],[Drop_Off_Date],
    [Pick_Up_Date],[Is_Active])
VALUES
    (100000, 'Drop it at Jimmys', '2023-12-17', '2023-12-19', 1),
    (100001, 'Take it to the dump', '2021-12-20', '2021-12-21', 1)
GO

/******************
Insert Sample Data For The  Change_Order table
***************/
print ''
Print '***Insert Sample Data For The  Change_Order table***' 
GO
INSERT INTO [dbo].[Change_Order]
    ([Vendor_ID], [Change_Order_Date], [Original_PO_Number], [Employee_ID])
VALUES
    (100000, GETDATE(), 100000, 100001),
    (100001, GETDATE(), 100001, 100002),
    (100002, GETDATE(), 100002, 100002),
    (100003, GETDATE(), 100002, 100003),
    (100003, GETDATE(), 100003, 100002)
GO

/******************
Insert Sample Data For The  Change_Order_Line table
***************/
print ''
Print '***Insert Sample Data For The  Change_Order_Line table***' 
GO
INSERT INTO [dbo].[Change_Order_Line]
    ([Change_Order_ID], [Parts_Inventory_ID], [Original_Qty], [Updated_Qty])
VALUES
    (100000, 100001, 1, 1),
    (100001, 100002, 2, 1),
    (100002, 100003, 2, 4),
    (100003, 100002, 2, 3),
    (100004, 100001, 1, 2)
GO

/******************
Insert Sample Data For The  Packing_Slip_Line_Items table
***************/
print ''
Print '***Insert Sample Data For The  Packing_Slip_Line_Items table***' 
GO
INSERT INTO [dbo].[Packing_Slip_Line_Items]
    ([Packing_Slip_ID], [Qty_Recieved], [Parts_Inventory_ID])
VALUES
    (100000, 2, 100004),
    (100001, 2, 100003),
    (100002, 2, 100002),
    (100003, 2, 100001),
    (100004, 2, 100000)
GO

/******************
Insert Sample Data For The  Model_Compatibility table
***************/
print ''
Print '***Insert Sample Data For The  Model_Compatibility table***' 
GO
INSERT INTO [dbo].[Model_Compatibility]
    ([Vehicle_Model_ID],[Parts_Inventory_ID])
VALUES
    (100001, 100001),
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
    (100004, 100006)
GO

/******************
Insert Sample Data For The  Service_Detail table
***************/
print ''
Print '***Insert Sample Data For The  Service_Detail table***' 
GO
INSERT INTO [dbo].[Service_Detail]
    ([Service_Order_ID],[Service_Order_Version],[Service_Type_ID],[Employee_ID])
VALUES
    (100000, 1, 'ST001', 100000),
    (100001, 1, 'ST001', 100001),
    (100002, 1, 'ST001', 100002),
    (100003, 1, 'ST001', 100003),
    (100004, 1, 'ST001', 100004)
GO

/******************
Insert Sample Data For The  Parts_Request table
***************/
print ''
Print '***Insert Sample Data For The  Parts_Request table***' 
GO
INSERT INTO [dbo].[Parts_Request]
    ([Employee_ID], [Service_Detail_ID], [Parts_Request_Notes], [Date_Requested], [Is_Active])
VALUES
    (100001, 100001, "Needing part to fix issue in vehicle.", '2023-12-05', 1),
    (100002, 100002, "This specific part will allow us to get this vehicle back on the lot.", '2023-05-16', 1),
    (100001, 100002, "We seem to keep running out of this, maybe order more.", '2024-01-18', 1),
    (100003, 100003, "We only need a handful more. Maybe 4 at most.", '2024-02-20', 1),
    (100003, 100004, "Part keeps breaking and needs replaced.", '2024-01-03', 0)
GO

/******************
Insert Sample Data For The  Parts_Request_Line_Items table
***************/
print ''
Print '***Insert Sample Data For The  Parts_Request_Line_Items table***' 
GO
INSERT INTO [dbo].[Parts_Request_Line_Items]
    ([Parts_Request_ID],[Parts_Inventory_ID],[Qty_Requested])
VALUES
    (100000, 100000, 5),
    (100001, 100002, 2),
    (100002, 100003, 1),
    (100003, 100001, 3),
    (100004, 100004, 4)
GO

/******************
Insert Sample Data For The  Inspection_Report table
***************/
print ''
print '*** Inserting Sample Data into Inspection_Report ***'
GO
INSERT INTO [dbo].[Inspection_Report]
	([Employee_ID],[VIN],[Service_Order_ID],[Service_Order_Version],[Oil_Level],[Tire_Pressure],[Front_Left_Turn_Signal],[Front_Right_Turn_Signal],[Rear_Left_Turn_Signal],[Rear_Right_Turn_Signal],[Left_Brake_Light],[Right_Brake_Light],[Windshield_Washer_Fluid],[Problem_Description],[Is_Active])
VALUES
    (100000, '1HGCM82633A123456', 100000, 1, 'Full', 35, 1, 1, 1, 1, 1, 1, 'Full', NULL, 1),
    (100001, '5XYZH4AG4JH123456', 100001, 1, 'Low', 30, 0, 1, 1, 0, 1, 0, 'Low', 'Right rear turn signal not working.', 1),
    (100002, 'JM1BK32F781234567', 100002, 1, 'OK', 32, 1, 1, 1, 1, 0, 1, 'OK', 'Left brake light faulty.', 1),
    (100003, 'WAUZZZ4G6BN123456', 100003, 1, 'Full', 28, 1, 1, 0, 1, 1, 1, 'Full', 'Rear left turn signal not operational.', 1),
    (100004, '1C4RJFAG5FC123456', 100004, 1, 'OK', 33, 1, 0, 1, 1, 1, 1, 'OK', 'Front right turn signal malfunctioning.', 1)
GO

/******************
Insert Sample Data For The  Client table
***************/
print ''
Print '***Insert Sample Data For The  Client table***' 
GO
INSERT INTO [dbo].[Client]
    ([Given_Name],[Family_Name],[Middle_Name],[DOB],[Email],[Postal_Code],[City],[Address],[Text_Number],[Voice_Number],[Is_Active])
VALUES
    ('Christalle', 'Jeandon', 'Morales', '2023-7-3', 'cjeandon0@indiatimes.com', '96120', 'Markleeville', '40223 Hintze Terrace', '265-932-6215', '277-589-9894', 1),
    ('Ignazio', 'Slator', 'Yesson', '2023-7-5', 'islator1@discovery.com', '95125', 'San Jose', '18815 Gerald Pass', '584-621-4234', '723-847-5006', 0),
    ('Norene', 'Elsmor', 'Crady', '2024-1-9', 'nelsmor2@nba.com', '29166', 'Ward', '6 Arkansas Court', '230-927-7810', '913-293-4175', 0),
    ('Theodoric', 'Barrat', 'Donnellan', '2023-4-8', 'tbarrat3@loc.gov', '49064', 'Lawrence', '9 Schurz Road', '367-830-3054', '235-984-5957', 0),
    ('Cesya', 'Rieme', 'Davidovsky', '2023-5-6', 'crieme4@cisco.com', '23827', 'Boykins', '35 American Ash Park', '416-832-0688', '909-892-4935', 1)
GO

/******************
Insert Sample Data For The  Client_Credential table
***************/
print ''
Print '***Insert Sample Data For The  Client_Credential table***' 
GO
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
Insert Sample Data For The  Login table
***************/
print ''
Print '***Insert Sample Data For The  Login table***' 
GO
INSERT INTO [dbo].[Login]
    ([Username], [Employee_ID],[Client_ID],[Security_Question_1],[Security_Response_1],
    [Security_Question_2],[Security_Response_2],[Security_Question_3],[Security_Response_3])
VALUES
    ('JoeSmith1994', 100000, NULL, 'what is your favorite animal?', 'lion', 'what is your favorite food?', 'Ramen', 'what was your first dogs name?', 'Hoola'),
    ('Jacmar125', 100001, NULL, 'what is your favorite animal?', 'Ocelot', 'what is your favorite food?', 'Bibimbap', 'what was your first dogs name?', 'Jeff'),
    ('Lebold2202', 100002, NULL, 'what is your favorite animal?', 'Foxes', 'what is your favorite food?', 'Spaghetti', 'what was your first dogs name?', 'Lola'),
	('Krystal2023', NULL, 100000, 'what is your favorite animal?', 'Foxes', 'what is your favorite food?', 'Spaghetti', 'what was your first dogs name?', 'Lola')
GO

/******************
Insert Sample Data For The  Client_Role table
***************/
print ''
Print '***Insert Sample Data For The  Client_Role table***' 
GO
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
Insert Sample Data For The  Client_Client_Role table
***************/
print ''
Print '***Insert Sample Data For The  Client_Client_Role table***' 
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
Insert Sample Data For The  Accommodation table
***************/
print ''
Print '***Insert Sample Data For The  Accommodation table***' 
GO
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
Insert Sample Data For The  Client_Accommodation table
***************/
print ''
Print '***Insert Sample Data For The  Client_Accommodation table***' 
GO
INSERT INTO [dbo].[Client_Accommodation]
    ([Client_ID],[Accommodation_ID],[Is_Active])
VALUES
    (100000, 'Wheelchair Lift', 1),
    (100001, 'Wheelchair Ramp', 1),
    (100002, 'Raised Roof', 1),
    (100003, 'Transfer Seats', 0),
    (100004, 'Dropped Floor', 1)
GO

/******************
Insert Sample Data For The  Dependent table
***************/
print '' 
Print '***Insert Sample Data For The  Dependent table***' 
GO
INSERT INTO [dbo].[Dependent]
	([Given_Name],[Middle_Name],[Family_Name],[DOB],[Gender],[Emergency_Contact],[Contact_Relationship],[Emergency_Phone])
VALUES
    ('Anita','ko','Feuer','1996-12-12','Female','Thomas Feuer', 'test1', '5552231049'),
    ('Flint','N','Steele','2002-12-12','Male','Cole D. Steele','test1','5554259994'),
	('Jarlson','lo','Flouf','1996-12-12','Male','Thomas Feuer','test1','5552231222'),
    ('Tanner','Ant','Minecraft','2002-12-12','Male','Mother','test1','5554259333'),
	('Christa','Lank','Crank','1996-12-12','Male','Thomas Feuer','test1','5552231444'),
    ('Lincoln','The','Logs','2002-12-12','Male','Pick ingups tix','test1','5554259555'),
	('FatherHeim','l','MotherHerm','1996-12-12','Female','Cousinxeim','test1','5552231666'),
    ('TinkerBell','H','Pixie','2002-12-12','Female','Pixar Studios','test1','5554259777')
GO

/******************
Insert Sample Data For The  Dependent_Accommodation table
***************/
print ''
Print '***Insert Sample Data For The  Dependent_Accommodation table***' 
GO
INSERT INTO [dbo].[Dependent_Accommodation]
	([Dependent_ID], [Accommodation_ID], [Is_Active])
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
Insert Sample Data For The  Client_Dependent_Role table
***************/
print '' 
Print '***Insert Sample Data For The  Client_Dependent_Role table***' 
GO
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
Insert Sample Data For The  Notification table
***************/
print ''
Print '***Insert Sample Data For The  Notification table***' 
GO
INSERT INTO [dbo].[Notification]
    ([Client_ID],[Title],[Notification_Body])
VALUES
    (100000, 'Holiday Deal!', 'Rent vehicles at a 30% discount!'),
    (100001, 'Your Reservation has Been Finalized.', 'Thank you for using our services!'),
    (100002, 'Your Vehicle is Ready', 'Your vehicle is ready to be picked up. Please go to the front desk for your keys.'),
    (100003, 'You have been banned From Renting Coach Buses','After many incidents with you returning coach buses with extensive damages, we have decided to ban you from renting coach buses.')
GO

/******************
Insert Sample Data For The  Ticket_Type table
***************/
print ''
Print '***Insert Sample Data For The  Ticket_Type table***' 
GO
INSERT INTO [dbo].[Ticket_Type]
	([Ticket_Type_ID], [Type_Description])
VALUES
    ( 'Type1', 'Sample Ticket Type Description1'),
    ( 'Type2', 'Sample Ticket Type Description2'),
    ( 'Type3', 'Sample Ticket Type Description3'),
    ( 'Type4', 'Sample Ticket Type Description4'),
    ( 'Type5', 'Sample Ticket Type Description5')
GO

/******************
Insert Sample Data For The  Support_Ticket table
***************/
-- Support_Ticket table sample data here

/******************
Insert Sample Data For The  Support_Ticket_Employee_Line table
***************/
-- Support_Ticket_Employee_Line table sample data here

/******************
Insert Sample Data For The  Charter table
***************/
-- Charter table sample data here

/******************
Insert Sample Data For The  Charter_Assignment table
***************/
-- Charter_Assignment table sample data here

/******************
Insert Sample Data For The  Charter_Accommodation table
***************/
-- Charter_Accommodation table sample data here

/******************
Insert Sample Data For The  Charter_Stop table
***************/
-- Charter_Stop table sample data here

/******************
Insert Sample Data For The  Charter_Rider table
***************/
-- Charter_Rider table sample data here

/******************
Insert Sample Data For The  Vehicle_Checklist table
***************/
print ''
Print '***Insert Sample Data For The  Vehicle_Checklist table***' 
GO
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

/******************
Insert Sample Data For The  Driver_Maintenance_Report table
***************/
print ''
Print '***Insert Sample Data For The  Driver_Maintenance_Report table***'
GO
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
Insert Sample Data For The  Driver_Unavailable table
***************/
print ''
print '***Insert Sample Data For The  Driver_Unavailable table***'
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
Insert Sample Data For The  Service table
***************/
print ''
Print '***Insert Sample Data For The  Service table***' 
GO
INSERT INTO [dbo].[Service]
    ([Service_ID], [Type])
VALUES
    ('Replacing tires', 'manual'),
    ('Engine exploded', 'oopsie'),
    ('redoing paint', 'fun'),
    ('something leaking', 'wet'),
    ('car totaled', 'expensive')
GO

/******************
Insert Sample Data For The  Service_Assignment table
***************/
print ''
Print '***Insert Sample Data For The  Service_Assignment table***' 
GO
INSERT INTO [dbo].[Service_Assignment]
    ([VIN], [Service_ID],[Driver_ID], [Start_Datetime], [End_Datetime], [Is_Active])
VALUES
    ('1HGCM82633A123456', 'Replacing tires', 100000, '2024-01-20 08:00:00', '2024-01-20 12:00:00', 1),
    ('5XYZH4AG4JH123456', 'Engine exploded', 100001, '2024-02-15 10:00:00', '2024-02-15 14:00:00', 1),
    ('JM1BK32F781234567', 'redoing paint', 100002, '2024-03-10 09:00:00', '2024-03-10 13:00:00', 0),
    ('WAUZZZ4G6BN123456', 'something leaking', 100003, '2024-04-05 11:00:00', '2024-04-05 15:00:00', 1),
    ('1C4RJFAG5FC123456', 'car totaled', 100004, '2024-05-01 13:00:00', '2024-05-01 17:00:00', 1);
GO

/******************
Insert Sample Data For The  Ride table
***************/
-- Ride table sample data here

/******************
Insert Sample Data For The  Source table
***************/
print ''
Print '***Insert Sample Data For The  Source table***' 
GO
INSERT INTO [dbo].[Source] 
	([Vendor_ID], [Parts_Inventory_ID], [Vendor_Part_Number], [Estimated_Delivery_Time_Days], [Part_Price], [Minimum_Order_Qty], [Active])
VALUES 
	(100000, 100000, 'Some Fastner', 5, CAST(1.0000 AS SmallMoney), 2, 1),
	(100000, 100001, 'Some Bolt', 5, CAST(1.0000 AS SmallMoney), 2, 1),
	(100000, 100002, 'Some Grommet', 5, CAST(3.0000 AS SmallMoney), 2, 1),
	(100000, 100003, 'Some Cable', 5, CAST(4.0000 AS SmallMoney), 2, 1),
	(100000, 100004, 'Some Frame', 5, CAST(5.0000 AS SmallMoney), 2, 1),
	(100000, 100005, 'Some Washer', 5, CAST(6.0000 AS SmallMoney), 2, 1),
	(100001, 100006, 'Some Screw', 5, CAST(7.0000 AS SmallMoney), 2, 1),
	(100001, 100007, 'Some Stripper', 5, CAST(8.0000 AS SmallMoney), 2, 1),
	(100001, 100008, 'Some Nails', 5, CAST(9.0000 AS SmallMoney), 2, 1),
	(100001, 100009, 'Some Tires', 5, CAST(10.0000 AS SmallMoney), 2, 1),
	(100001, 100010, 'Some Tires', 5, CAST(11.0000 AS SmallMoney), 2, 1),
	(100001, 100011, 'Some Axel', 5, CAST(12.0000 AS SmallMoney), 2, 1),
	(100002, 100012, 'Some Struts', 5, CAST(13.2500 AS SmallMoney), 2, 1),
	(100002, 100013, 'Some Hubcaps', 5, CAST(14.2500 AS SmallMoney), 2, 1),
	(100003, 100000, 'Some Fastner', 5, CAST(35.2500 AS SmallMoney), 2, 1),
	(100003, 100001, 'Some Bolt', 5, CAST(16.2500 AS SmallMoney), 2, 1),
	(100003, 100002, 'Some Grommet', 5, CAST(17.2500 AS SmallMoney), 2, 1),
	(100003, 100003, 'Some Cable', 5, CAST(21.2500 AS SmallMoney), 2, 1),
	(100003, 100008, 'Some Nails', 5, CAST(26.2500 AS SmallMoney), 2, 1),
	(100003, 100009, 'Some Tires', 5, CAST(27.2500 AS SmallMoney), 2, 1),
	(100003, 100010, 'Some Tires', 5, CAST(31.2500 AS SmallMoney), 2, 1),
	(100003, 100011, 'Some Axels', 5, CAST(32.2500 AS SmallMoney), 2, 1),
	(100003, 100012, 'Some Struts', 5, CAST(33.2500 AS SmallMoney), 2, 1),
	(100003, 100013, 'Some Hubcaps', 5, CAST(15.2500 AS SmallMoney), 2, 1),
	(100004, 100004, 'Some Frame', 5, CAST(22.2500 AS SmallMoney), 2, 1),
	(100004, 100005, 'Some Washer', 5, CAST(23.2500 AS SmallMoney), 2, 1),
	(100004, 100006, 'Some Screw', 5, CAST(24.2500 AS SmallMoney), 2, 1),
	(100004, 100007, 'Some Stripper', 5, CAST(25.2500 AS SmallMoney), 2, 1)
GO

/******************
Insert Sample Data For The  Vehicle_Unavailable table
***************/
print ''
Print '***Insert Sample Data For The  Vehicle_Unavailable table***' 
GO
INSERT INTO [dbo].[Vehicle_Unavailable]
    ([VIN], [Start_Datetime], [End_Datetime], [Reason])
VALUES
    ('1HGCM82633A123456', '2023-01-28', '2023-01-28', 'Test unavailability1'),
    ('5XYZH4AG4JH123456', '2023-01-28', '2023-01-28', 'Test unavailablitly2'),
    ('JM1BK32F781234567', '2023-01-28', '2023-01-28', 'Test unavailablitly3'),
    ('WAUZZZ4G6BN123456', '2023-01-28', '2023-01-28', 'Test unavailablitly4'),
    ('1C4RJFAG5FC123456', '2023-01-28', '2023-01-28', 'Test unavailablitly5')
GO

/******************
Insert Sample Data For The  Password_Reset table
***************/
-- Password_Reset table sample data here