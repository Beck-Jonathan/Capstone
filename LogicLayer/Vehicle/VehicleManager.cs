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
    }
}
