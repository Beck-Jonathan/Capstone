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
    /// AUTHOR: Chris Baenziger, Everett DeVaux
    /// CREATED: 2024-02-01
    ///     Manager for working with vehicle data.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Everett DeVaux
    /// <br />
    /// UPDATED: 2024-02-13
    /// <br />
    /// 
    ///     Added Vehicle Lookup List
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
            /// <summary>
            ///     Add vehicle to the database.
            /// </summary>
            /// <param name="vehicle">
            ///    The vehicle information to be added as a Vehicle object.
            /// </param>
            /// <returns>
            ///    <see cref="int">int</see>: The row count, 1 added, 0 error adding.
            /// </returns>
            /// <remarks>
            ///    Parameters:
            ///    <see cref="Vehicle">Vehicle</see> vehicle: The vehicle information to be added.
            ///    Exceptions:
            ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem writing to the DB.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-01
            /// </remarks>
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
            /// <summary>
            ///     Get the model id for the vehicle.
            /// </summary>
            /// <returns>
            ///    <see cref="Vehicle">Vehicle</see>: The vehicle with the id for the model.
            /// </returns>
            /// <remarks>
            ///    Exceptions:
            ///    <see cref="ApplicationException">ApplicationException</see>: Thrown if there is a problem retrieving the list.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-14
            /// </remarks>
            try
            {
                vehicle.ModelLookupID = _vehicleAccessor.SelectModelLookupID(vehicle);
            }
            catch (Exception)
            {
                throw new ArgumentException("Unable to find vehicle make and model.");
            }
            return vehicle;
        }

        public bool AddModelLookup(Vehicle vehicle)
        {
            /// <summary>
            ///     Add a model to the database.
            /// </summary>
            /// <returns>
            ///    <see cref="Vehicle">Vehicle</see>: The vehicle containing the model to be added.
            /// </returns>
            /// <remarks>
            ///    Exceptions:
            ///    <see cref="ApplicationException">ApplicationException</see>: Thrown if there is a problem retrieving the list.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-14
            /// </remarks>
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
            /// <summary>
            ///     Get a list of vehicle makes from the database.
            /// </summary>
            /// <returns>
            ///    <see cref="List<string>">List<string></see>: The list of vehicle makes.
            /// </returns>
            /// <remarks>
            ///    Exceptions:
            ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem retrieving the list.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>
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
            /// <summary>
            ///     Get a list of vehicle models from the database.
            /// </summary>
            /// <returns>
            ///    <see cref="List<string>">List<string></see>: The list of vehicle models.
            /// </returns>
            /// <remarks>
            ///    Exceptions:
            ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem retrieving the list.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>
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
            /// <summary>
            ///     Get a list of vehicle types from the database.
            /// </summary>
            /// <returns>
            ///    <see cref="List<string>">List<string></see>: The list of vehicle types.
            /// </returns>
            /// <remarks>
            ///    Exceptions:
            ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem retrieving the list.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-06
            /// </remarks>
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
            /// <summary>
            ///     Retrieves vehicle list from the VM
            /// </summary>
            /// <returns>
            ///    <see cref="List{VehicleLookupList_VM}">VehicleLookupList_VM</see> List of VehicleLookupList_VM objects
            /// </returns>
            /// <remarks>
            ///    Exceptions:
            /// <br />
            ///    <see cref="Exception">Exception</see>: Thrown when error encountered
            /// <br /><br />
            ///    CONTRIBUTOR: Everett DeVaux
            /// <br />
            ///    CREATED: 2024-02-10
            /// <br />
            ///     Initial Creation
            /// <br />
            ///    UPDATER: Chris Baenziger
            /// <br />
            ///    UPDATED: 2024-02-17
            /// <br />
            ///     Moved method comment inside of method
            /// </remarks>
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
            /// <summary>
            ///     Get vehicle detail information from the database.
            /// </summary>
            /// <param name="vehicleNumber">
            ///    The vehicle number to be looked up.
            /// </param>
            /// <returns>
            ///    <see cref="Vehicle">Vehicle</see>: Vehicle information.
            /// </returns>
            /// <remarks>
            ///    Exceptions:
            ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem retrieving the vehicle.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-10
            /// </remarks>
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
            /// <summary>
            ///     Verify a data hasn't change and update the vehicle in the database.
            /// </summary>
            /// <param name="OldVehicle">
            ///    The vehicle information to be used to verify database data hasn't changed as a Vehicle object.
            /// </param>
            /// <param name="NewVehicle">
            ///    The vehicle information to be updated in the database as a Vehicle object.
            /// </param>
            /// <returns>
            ///    <see cref="bool">bool</see>: bool if the vehicle was updated.
            /// </returns>
            /// <remarks>
            ///    Exceptions:
            ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem updating the vehicle.
            ///    CONTRIBUTOR: Chris Baenziger
            ///    CREATED: 2024-02-10
            /// </remarks>
            bool result = false;

            try
            {
                result = (1 == _vehicleAccessor.UpdateVehicle(oldVehicle, newVehicle));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to update vehicle er002.", ex);
            }

            return result;
        }
    }
}
