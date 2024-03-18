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
    /// <remarks>
    /// UPDATER: Chris Baenizger
    /// UPDATED: 2024-02-23
    /// Added fakes for deactivate vehicle.
    /// </remarks>

    public class VehicleAccessorFakes : IVehicleAccessor
    {
        List<Vehicle> fakeVehicles = new List<Vehicle>();
        List<int> fakeModelLookup = new List<int>();
        List<string> fakeVehicleTypes = new List<string>();
        List<string> fakeVehicleMakes = new List<string>();
        List<string> fakeVehicleModels = new List<string>();
        List<Vehicle> _fakeVehicleLookupList = new List<Vehicle>();

        public VehicleAccessorFakes() 
        {
            Vehicle vehicle = new Vehicle()
            {
                VIN = "testaddvin1234567",
                VehicleNumber = "Test-01",
                VehicleMileage = 1000,
                VehicleModelID = 100001,
                VehicleLicensePlate = "Test01",
                VehicleMake = "Mercedes",
                VehicleModel = "Sprinter",
                VehicleYear = 2024,
                MaxPassengers = 3,
                VehicleType = "Van",
                VehicleDescription = "Van",
                Rental = false,
                DateEntered = DateTime.Now
            };

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

            fakeVehicles.Add(vehicle);
            fakeModelLookup.Add(vehicle.VehicleModelID);

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

        public int AddModelLookup(Vehicle vehicle)
        {
            foreach (var v in fakeModelLookup)
            {
                if (v.Equals(vehicle.VehicleModelID))
                {
                    throw new ArgumentException();
                }
            }
            return 100000;
        }

        public Vehicle SelectVehicleByVehicleNumber(string vehicleNumber)
        {
            Vehicle found = null;
            foreach (var v in fakeVehicles)
            {
                if (v.VehicleNumber.Equals(vehicleNumber))
                {
                    found = v;
                    break;
                }
            }
            if (found == null)
            {
                throw new ArgumentException("Vehicle not found.");
            }
            return found;
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

        public int UpdateVehicle(Vehicle oldVehicle, Vehicle newVehicle)
        {
            int result = 0;

            for (int i = 0; i < fakeVehicles.Count; i++)
            {
                if (fakeVehicles[i].VIN.Equals(oldVehicle.VIN))
                {
                    fakeVehicles[i] = newVehicle;
                    result++;
                }
            }

            if (result != 1)
            {
                throw new ArgumentException("Unable to update vehicle.");
            }

            return result;
        }

        public int SelectModelLookupID(Vehicle vehicle)
        {
            if (vehicle.VehicleModelID == fakeVehicles[0].VehicleModelID)
            {
                return fakeVehicles[0].VehicleModelID;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public int DeactivateVehicle(Vehicle vehicle)
        {
            int result = 0;
            if(vehicle.VIN == fakeVehicles[0].VIN)
            {
                return ++result;
            }
            else
            {
                throw new ArgumentException();
            }
            
        }
    }
}
