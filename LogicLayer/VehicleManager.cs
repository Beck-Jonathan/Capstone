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
                throw new ArgumentException("Error adding bug ticket.", ex);
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
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
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
    }
}
