using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-02-01
    ///     Fake data to be used with vehicle manager tests.
    /// </summary>

    public class VehicleAccessorFakes : IVehicleAccessor
    {
        List<Vehicle> fakeVehicles = new List<Vehicle>();    
        List<string> fakeVehicleTypes = new List<string>();
        List<string> fakeVehicleMakes = new List<string>();
        List<string> fakeVehicleModels = new List<string>();

        public VehicleAccessorFakes() 
        {
            fakeVehicles.Add(new Vehicle()
            {
                VIN = "testaddvin1234567",
                VehicleNumber = "Test-01",
                VehicleMileage = 1000,
                VehicleLicensePlate = "Test01",
                VehicleMake = "Mercedes",
                VehicleModel = "Sprinter",
                VehicleYear = 2024,
                MaxPassengers = 3
            });

            fakeVehicleTypes.Add("");
            fakeVehicleTypes.Add("");

            fakeVehicleMakes.Add("");
            fakeVehicleMakes.Add("");

            fakeVehicleModels.Add("");
            fakeVehicleModels.Add("");
        }

        public int AddVehicle(Vehicle vehicle)
        {
            foreach (var v in fakeVehicles)
            {
                if(v.VIN.Equals(vehicle.VIN)){
                    throw new ArgumentException();
                }
            }
            fakeVehicles.Add(vehicle);
            return 1;
        }

        public List<string> SelectVehicleMakes()
        {
            return fakeVehicleMakes;
        }

        public List<string> SelectVehicleModels()
        {
            return fakeVehicleModels;
        }

        public List<string> SelectVehicleTypes()
        {
            return fakeVehicleTypes;
        }
    }
}
