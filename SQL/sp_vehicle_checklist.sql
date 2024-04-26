USE Night_Rider;
GO

print ''
print '*** Create sp_insert_vehicle_checklist ***'
GO
-- AUTHOR: Chris Baenziger
-- CREATED: 2024-04-20
CREATE PROCEDURE [dbo].[sp_insert_vehicle_checklist]
    (
    @Employee_ID [int],
    @VIN [nvarchar] (17),
    @Date [datetime],
    @Clean [bit],
    @Pedals [bit],
    @Dash [bit],
    @Steering [bit],
    @AC_Heat [bit],
    @Mirror_DS [bit],
    @Mirror_PS [bit],
    @Mirror_RV [bit],
    @Cosmetic [nvarchar] (500),
    @Tire_Pressure_DF [int],
    @Tire_Pressure_PF [int],
    @Tire_Pressure_DR [int],
    @Tire_Pressure_PR [int],
    @Blinker_DF [bit],
    @Blinker_PF [bit],
    @Blinker_DR [bit],
    @Blinker_PR [bit],
    @Breaklight_DR [bit],
    @Breaklight_PR [bit],
    @Headlight_DS [bit],
    @Headlight_PS [bit],
    @Taillight_DS [bit],
    @Taillight_PS [bit],
    @Wiper_DS [bit],
    @Wiper_PS [bit],
    @Wiper_R [int],
    @Seat_Belts [bit],
    @Fire_Extinguisher [bit],
    @Airbags [bit],
    @First_Aid [bit],
    @Emergency_Kit [bit],
    @Mileage [int],
    @Fuel_Level [int],
    @Brakes [bit],
    @Accelerator [bit],
    @Clutch [bit],
    @Notes [nvarchar] (1000)
)
AS
BEGIN
    INSERT INTO [Vehicle_Checklist]
        ([Employee_ID], [VIN], [Date], [Clean], [Pedals], [Dash], [Steering], [AC_Heat],
        [Mirror_DS], [Mirror_PS], [Mirror_RV], [Cosmetic],
        [Tire_Pressure_DF], [Tire_Pressure_PF], [Tire_Pressure_DR], [Tire_Pressure_PR],
        [Blinker_DF], [Blinker_PF], [Blinker_DR], [Blinker_PR],
        [Breaklight_DR], [Breaklight_PR],
        [Headlight_DS], [Headlight_PS], [Taillight_DS], [Taillight_PS],
        [Wiper_DS], [Wiper_PS], [Wiper_R],
        [Seat_Belts], [Fire_Extinguisher], [Airbags],
        [First_Aid], [Emergency_Kit], [Mileage], [Fuel_Level],
        [Brakes], [Accelerator], [Clutch], [Notes])
    VALUES
        (@Employee_ID, @VIN, @Date, @Clean, @Pedals, @Dash, @Steering, @AC_Heat,
            @Mirror_DS, @Mirror_PS, @Mirror_RV, @Cosmetic,
            @Tire_Pressure_DF, @Tire_Pressure_PF, @Tire_Pressure_DR, @Tire_Pressure_PR,
            @Blinker_DF, @Blinker_PF, @Blinker_DR, @Blinker_PR,
            @Breaklight_DR, @Breaklight_PR,
            @Headlight_DS, @Headlight_PS, @Taillight_DS, @Taillight_PS,
            @Wiper_DS, @Wiper_PS, @Wiper_R,
            @Seat_Belts, @Fire_Extinguisher, @Airbags,
            @First_Aid, @Emergency_Kit, @Mileage, @Fuel_Level,
            @Brakes, @Accelerator, @Clutch, @Notes)
    SELECT CAST( SCOPE_IDENTITY() AS int);
END
GO