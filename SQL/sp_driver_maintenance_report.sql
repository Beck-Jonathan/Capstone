USE Night_Rider;
GO

/******************
Create the insert script for the Driver_Maintenance_Report table
While inserting, it marks all reports related to this vehicle as inactive
and the latest report as inactive
 Created By Jonathan Beck 4/16/2024
***************/
print '' Print '***Create the insert script for the Driver_Maintenance_Report table***' 
 go 
CREATE PROCEDURE [DBO].[sp_insert_driver_maintenance_report]
(
@Driver_ID [int],
@Date_Time [datetime],
@VIN [nvarchar](17),
@Severity [nvarchar](20),
@Description [nvarchar](250),
@Is_Active [bit]
)
as 
 begin
 
  insert into [dbo].[Driver_Maintenance_Report](
[Driver_ID],
[Date_Time],
[VIN],
[Severity],
[Description]

)
 VALUES 
(
@Driver_ID,
@Date_Time,
@VIN,
@Severity,
@Description

)
;
 
 update [dbo].[Driver_Maintenance_Report]
 SET
 is_Active=0
 where 
 [VIN]=@VIN
 ;
 
 update[dbo].[Driver_Maintenance_Report]
 set 
 is_Active=1
 where
 Driver_Maintenance_Report_ID = (
 select top (1) [Driver_Maintenance_Report_ID] 
 from [Driver_Maintenance_Report]
 where 
 [VIN] = @VIN
 order by [Date_Time] DESC
 
 )
 
 ;
 

return @@rowcount
end
Go

/******************
Create the retreive by key script for the Driver_Maintenance_Report table
 Created By Jonathan Beck 4/23/2024
***************/
CREATE PROCEDURE [dbo].[sp_get_all_driver_maintenance_report_by_report_id]
(
@Driver_Maintenance_Report_ID [int]
)
as
 Begin 
 select
[Driver_Maintenance_Report].[Driver_Maintenance_Report_ID] as 'Driver_Maintenance_Report_Driver_Maintenance_Report_ID',
[Driver_Maintenance_Report].[Driver_ID] as 'Driver_Maintenance_Report_Driver_ID',
[Driver_Maintenance_Report].[Date_Time] as 'Driver_Maintenance_Report_Date_Time',
[Driver_Maintenance_Report].[VIN] as 'Driver_Maintenance_Report_VIN',
[Driver_Maintenance_Report].[Severity] as 'Driver_Maintenance_Report_Severity',
[Driver_Maintenance_Report].[Description] as 'Driver_Maintenance_Report_Description',
[Driver_Maintenance_Report].[Is_Active] as 'Driver_Maintenance_Report_Is_Active',
[Driver].[Employee_ID] as 'Driver_Employee_ID',
[Driver].[Driver_License_Class_ID] as 'Driver_Driver_License_Class_ID',
[Driver].[Is_Active] as 'Driver_Is_Active',
[Vehicle].[VIN] as 'Vehicle_VIN',
[Vehicle].[Vehicle_Number] as 'Vehicle_Vehicle_Number',
[Vehicle].[Vehicle_Mileage] as 'Vehicle_Vehicle_Mileage',
[Vehicle].[Vehicle_License_Plate] as 'Vehicle_Vehicle_License_Plate',
[Vehicle].[Vehicle_Type_ID] as 'Vehicle_Vehicle_Type',
[Vehicle].[Rental] as 'Vehicle_Rental',
[Vehicle].[Is_Active] as 'Vehicle_Is_Active'
 FROM Driver_Maintenance_Report
join [Driver] on [Driver_Maintenance_Report].[Driver_ID] = [Driver].[Employee_ID]
join [Vehicle] on [Driver_Maintenance_Report].[VIN] = [Vehicle].[VIN]
where [Driver_Maintenance_Report_ID]=@Driver_Maintenance_Report_ID 
 END 
 GO
 
 /******************
Create the retreive by all script for the Driver_Maintenance_Report table
 Created By Jonathan Beck 4/23/2024
***************/
CREATE PROCEDURE [dbo].[sp_get_driver_maintenance_report]
AS
begin 
 SELECT 
[Driver_Maintenance_Report].[Driver_Maintenance_Report_ID] as 'Driver_Maintenance_Report_Driver_Maintenance_Report_ID',
[Driver_Maintenance_Report].[Driver_ID] as 'Driver_Maintenance_Report_Driver_ID',
[Driver_Maintenance_Report].[Date_Time] as 'Driver_Maintenance_Report_Date_Time',
[Driver_Maintenance_Report].[VIN] as 'Driver_Maintenance_Report_VIN',
[Driver_Maintenance_Report].[Severity] as 'Driver_Maintenance_Report_Severity',
[Driver_Maintenance_Report].[Description] as 'Driver_Maintenance_Report_Description',
[Driver_Maintenance_Report].[Is_Active] as 'Driver_Maintenance_Report_Is_Active',
[Driver].[Employee_ID] as 'Driver_Employee_ID',
[Driver].[Driver_License_Class_ID] as 'Driver_Driver_License_Class_ID',
[Driver].[Is_Active] as 'Driver_Is_Active',
[Vehicle].[VIN] as 'Vehicle_VIN',
[Vehicle].[Vehicle_Number] as 'Vehicle_Vehicle_Number',
[Vehicle].[Vehicle_Mileage] as 'Vehicle_Vehicle_Mileage',
[Vehicle].[Vehicle_License_Plate] as 'Vehicle_Vehicle_License_Plate',
[Vehicle].[Vehicle_Type_ID] as 'Vehicle_Vehicle_Type',
[Vehicle].[Rental] as 'Vehicle_Rental',
[Vehicle].[Is_Active] as 'Vehicle_Is_Active'
 FROM Driver_Maintenance_Report
join [Driver] on [Driver_Maintenance_Report].[Driver_ID] = [Driver].[Employee_ID]
join [Vehicle] on [Driver_Maintenance_Report].[VIN] = [Vehicle].[VIN]

 ;
 END  
 GO
 
 /******************
Create The Retreive_By_Active script for the Driver_Maintenance_Report table
 Created By Jonathan Beck 4/23/2024
***************/
CREATE PROCEDURE [dbo].[sp_get_all_driver_maintenance_report_by_employee_id]
(
@Driver_ID [int]
)
AS
begin 
 SELECT 
[Driver_Maintenance_Report].[Driver_Maintenance_Report_ID] as 'Driver_Maintenance_Report_Driver_Maintenance_Report_ID',
[Driver_Maintenance_Report].[Driver_ID] as 'Driver_Maintenance_Report_Driver_ID',
[Driver_Maintenance_Report].[Date_Time] as 'Driver_Maintenance_Report_Date_Time',
[Driver_Maintenance_Report].[VIN] as 'Driver_Maintenance_Report_VIN',
[Driver_Maintenance_Report].[Severity] as 'Driver_Maintenance_Report_Severity',
[Driver_Maintenance_Report].[Description] as 'Driver_Maintenance_Report_Description',
[Driver_Maintenance_Report].[Is_Active] as 'Driver_Maintenance_Report_Is_Active',
[Driver].[Employee_ID] as 'Driver_Employee_ID',
[Driver].[Driver_License_Class_ID] as 'Driver_Driver_License_Class_ID',
[Driver].[Is_Active] as 'Driver_Is_Active',
[Vehicle].[VIN] as 'Vehicle_VIN',
[Vehicle].[Vehicle_Number] as 'Vehicle_Vehicle_Number',
[Vehicle].[Vehicle_Mileage] as 'Vehicle_Vehicle_Mileage',
[Vehicle].[Vehicle_License_Plate] as 'Vehicle_Vehicle_License_Plate',
[Vehicle].[Vehicle_Type_ID] as 'Vehicle_Vehicle_Type',
[Vehicle].[Rental] as 'Vehicle_Rental',
[Vehicle].[Is_Active] as 'Vehicle_Is_Active'
 FROM Driver_Maintenance_Report
join [Driver] on [Driver_Maintenance_Report].[Driver_ID] = [Driver].[Employee_ID]
join [Vehicle] on [Driver_Maintenance_Report].[VIN] = [Vehicle].[VIN]


 where [Driver_ID]=@Driver_ID
;
 END  
 GO