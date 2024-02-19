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
    /// AUTHOR: Chris Baenziger, Everett DeVaux
    /// CREATED: 2024-02-01
    ///     Fake data to be used with vehicle manager tests.
    /// </summary>
    ///     
    /// <remarks>
    /// UPDATER: Everett DeVaux
    /// <br />
    /// UPDATED: 2024-02-13
    /// <br />
    /// 
    ///     Update comments go here, include method or methods were changed or added 
    ///     (no other details necessary).
    ///     A new remark should be added for each update.
    /// </remarks>

    public class VehicleAccessorFakes : IVehicleAccessor
    {
        List<Vehicle> fakeVehicles = new List<Vehicle>();    
        List<string> fakeVehicleTypes = new List<string>();
        List<string> fakeVehicleMakes = new List<string>();
        List<string> fakeVehicleModels = new List<string>();
        List<Vehicle> _fakeVehicleLookupList = new List<Vehicle>();

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

            _fakeVehicleLookupList.Add(new Vehicle()
            {
                VehicleNumber = "THX138",
                VehicleModel = "Honda",
                MaxPassengers = 1,
                VehicleMileage = 25350,
                VehicleDescription = "This car is exciting to drive.",
            });

            _fakeVehicleLookupList.Add(new Vehicle()
            {
                VehicleNumber = "PWD159",
                VehicleModel = "Toyota",
                MaxPassengers = 1,
                VehicleMileage = 25350,
                VehicleDescription = "This car is crazy to drive.",
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

        /// <summary>
        ///     Returns all fake vehicle lookup list records
        /// </summary>
        /// <returns>
        ///    <see cref="List{Vehicle}">Vehicle</see> List of Vehicle objects
        /// </returns>
        /// <remarks>
        ///    CONTRIBUTOR: Everett DeVaux
        /// <br />
        ///    CREATED: 2024-02-10
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        public List<Vehicle> SelectVehicleForLookupList()
        {
            return _fakeVehicleLookupList;
        }
    }
}
