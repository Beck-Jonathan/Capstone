using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{

    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-02-01
    ///     Logic manager for working with vehicle data.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Everett DeVaux
    /// <br />
    /// UPDATED: 2024-02-13
    /// <br />
    /// 
    /// 
    ///     Initial creation
    ///     Added Vehicle Lookup List
    /// </remarks>
    /// <remarks>
    /// UPDATER: Chris Baenizger
    /// UPDATED: 2024-02-23
    /// Added method for deactivate vehicle.
    /// </remarks>
    ///
    /// <remarks>
    /// UPDATER: Jacob Rohr
    /// UPDATED: 2024-04-01
    /// Removed inheritdoc and migrated Interface file comments.
    /// </remarks>

    public class VehicleManager : IVehicleManager
    {
        IVehicleAccessor _vehicleLookupListAccessor = null;
        IVehicleAccessor _vehicleAccessor = null;
        private List<string> _vehicleTypes = null;
        private List<string> _vehicleMakes = null;
        private List<string> _vehicleModels = null;
        Vehicle _selectedVehicle = null;

        // default constructor
        public VehicleManager()
        {
            _vehicleAccessor = new VehicleAccessor();
            _vehicleLookupListAccessor = new VehicleAccessor();

        }

        // test constructor
        public VehicleManager(IVehicleAccessor vehicleAccessor)
        {
            _vehicleAccessor = vehicleAccessor;
            _vehicleLookupListAccessor = vehicleAccessor;
        }

        public bool AddVehicle(Vehicle vehicle)
        {
            bool result = false;

            try
            {
                result = (1 == _vehicleAccessor.AddVehicle(vehicle));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error adding vehicle.", ex);
            }

            return result;
        }

        public Vehicle GetModelLookupID(Vehicle vehicle)
        {
            try
            {
                vehicle.VehicleModelID = _vehicleAccessor.SelectModelLookupID(vehicle);
            }
            catch (Exception)
            {
                throw new ArgumentException("Unable to find vehicle make and model.");
            }
            return vehicle;
        }

        
        public bool AddModelLookup(Vehicle vehicle)
        {
            
            bool result = false;
            try
            {
                result = (100000 <= _vehicleAccessor.AddModelLookup(vehicle));
            }
            catch (Exception)
            {
                throw new ArgumentException("Unable to find the model.");
            }
            return result;
        }

        
        public List<string> GetVehicleMakes()
        {
            try
            {
                _vehicleMakes = _vehicleAccessor.SelectVehicleMakes();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving vehicle makes.", ex);
            }

            return _vehicleMakes;
        }


        public List<string> GetVehicleModels()
        {
            try
            {
                _vehicleModels = _vehicleAccessor.SelectVehicleModels();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving vehicle models.", ex);
            }

            return _vehicleModels;
        }


        public List<string> GetVehicleTypes()
        {
            try
            {
                _vehicleTypes = _vehicleAccessor.SelectVehicleTypes();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving vehicle types.", ex);
            }

            return _vehicleTypes;
        }


        public List<Vehicle> VehicleLookupList()
        {
            List<Vehicle> vehicleLookupList = new List<Vehicle>();
            try
            {
                vehicleLookupList = _vehicleLookupListAccessor.SelectVehicleForLookupList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return vehicleLookupList;
        }


        public Vehicle GetVehicleByVehicleNumber(string vehicleNumber)
        {
            try
            {
                _selectedVehicle = _vehicleAccessor.SelectVehicleByVehicleNumber(vehicleNumber);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to find vehicle.", ex);
            }
            return _selectedVehicle;
        }


        public bool EditVehicle(Vehicle oldVehicle, Vehicle newVehicle)
        {
            bool result = false;

            try
            {
                result = (1 == _vehicleAccessor.UpdateVehicle(oldVehicle, newVehicle));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to update vehicle.", ex);
            }

            return result;
        }


        public bool DeactivateVehicle(Vehicle vehicle)
        {
            bool result = false;

            try
            {
                result = (1 == _vehicleAccessor.DeactivateVehicle(vehicle));
            }
            catch (Exception)
            {
                throw new ArgumentException("Unable to dactivate vehicle");
            }

            return result;
        }

        /// <summary>
        ///     Get all service orders for a specificed vehicle
        /// </summary>
        /// <param name="VIN">
        ///    The VIN to get associated service orders for..
        /// </param>
        /// 
        /// <returns>
        ///    <see cref="ServiceOrder_VM">List:ServiceOrder_VM</see>: a list of service orders related to the vehicle
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem updating the vehicle.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-04-13
        /// </remarks>

        public List<ServiceOrder_VM> getAllService_OrderByVIN(String VIN)
        {
            List<ServiceOrder_VM> results = new List<ServiceOrder_VM> ();
            try
            {
                results = _vehicleAccessor.SelectServiceOrdersByVin(VIN);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return results;
        }


        /// <summary>
        ///     Retrives a Vehicle from the database by its VIN using a data accessor method
        /// </summary>
        /// <returns>
        ///    <see cref="Vehicle">Vehicle</see> object
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
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
        public Vehicle GetVehicleByVIN(string VIN)
        {
            Vehicle vehicle = new Vehicle();

            try
            {
                vehicle = _vehicleAccessor.SelectVehicleByVIN(VIN);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vehicle;
        }

        public int AddVehicleChecklist(VehicleChecklist checklist)
        {
            int checklistID = 0;

            try
            {
                checklistID = _vehicleAccessor.AddVehicleChecklist(checklist);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to add checklist", ex);
            }

            return checklistID;
        }

        /// <summary>
        ///     Retrieves VIN/Vehicle number tuples to fill drop downs
        /// </summary>
        /// <returns>
        ///    <see cref="List{Vehicle}">Vehicle</see> List of Vin/Vehicle Number tuples for drop downs
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    
        /// <br />
        ///    CREATED: 2024-04-22
        /// <br />
        ///     Initial Creation
        /// <br />
        ///    Creator: Jonathan Beck
        /// <br />
        ///    
        /// <br />
        ///    
        /// </remarks>
        public List<Vehicle> getVehicleTuplesForDropDown()
        {
            List<Vehicle> results = new List<Vehicle>();
            try
            {
                results = _vehicleAccessor.selectVehicleTuplesForDropDown();

            }
            catch (Exception ex)
            {

                throw new ApplicationException("Unable to load select box items", ex);
            }
            return results;


        }
    }
}
