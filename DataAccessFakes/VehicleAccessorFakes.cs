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
    /// /// UPDATER: Jonathan Beck
    /// UPDATED: 2024-04-13
    /// Chagned service orders to service order VMs
    /// </remarks>

    public class VehicleAccessorFakes : IVehicleAccessor
    {
        List<Vehicle> fakeVehicles = new List<Vehicle>();
        List<int> fakeModelLookup = new List<int>();
        List<string> fakeVehicleTypes = new List<string>();
        List<string> fakeVehicleMakes = new List<string>();
        List<string> fakeVehicleModels = new List<string>();
        List<Vehicle> _fakeVehicleLookupList = new List<Vehicle>();
        Vehicle fakeVehicle = new Vehicle();
        List<ServiceOrder_VM> _fakeServiceOrders = new List<ServiceOrder_VM>();

        public VehicleAccessorFakes() 
        {
            _fakeServiceOrders.Add(new ServiceOrder_VM()
            {
                VIN = "2GNALDEK9C6340800",
                Service_Order_ID = 100000,
                Critical_Issue = true,
                Service_Type_ID = "Windshield Wiper Replacement",
                Service_Description = "Replace the windshield wipers with OEM wipers"
            });
            _fakeServiceOrders.Add(new ServiceOrder_VM()
            {
                VIN = "JTLZE4FEXB1123437",
                Service_Order_ID = 100001,
                Critical_Issue = false,
                Service_Type_ID = "Brake Pad Replacement",
                Service_Description = "Replace the brake pads with OEM pads"
            });
            _fakeServiceOrders.Add(new ServiceOrder_VM()
            {
                VIN = "JTLZE4FEXB1123437",
                Service_Order_ID = 100002,
                Critical_Issue = false,
                Service_Type_ID = "Brake rotor Replacement",
                Service_Description = "Replace the brake rotor with OEM pads"
            });
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

            fakeVehicle = vehicle;
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
        //Jonathan Beck 2024-04-13

        public List<ServiceOrder_VM> SelectServiceOrdersByVin(string VIN)
        {
            List<ServiceOrder_VM> results = new List<ServiceOrder_VM>();
            foreach (ServiceOrder_VM order in _fakeServiceOrders) { 
            if (order.VIN == VIN)
                {
                    results.Add(order);
                }
            
            }

            return results;

        }

        /// <summary>
        ///     Retrieves a Vehicle record by the VIN from the database
        /// </summary>
        /// <returns>
        ///    A <see cref="Vehicle">Vehicle</see> object otherwise, <see cref="Exception">execption</see>.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="SqlException">SqlException</see>: No records returned
        /// <br /><br />
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-03-24
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        public Vehicle SelectVehicleByVIN(string VIN)
        {
            if (VIN == fakeVehicle.VIN)
            {
                return fakeVehicle;
            }
            return null;
        }

        public int AddVehicleChecklist(VehicleChecklist checklist)
        {
            int result = 0;
            if (checklist.VIN == fakeVehicle.VIN)
            {
                result = 100001;
            }
            else
            {
                throw new ApplicationException("Error creating checklist.");
            }
            return result;
        }
        //Jonathan Beck 2024-04-23
        public List<Vehicle> selectVehicleTuplesForDropDown()
        {
            return fakeVehicles;
        }
    }
}
