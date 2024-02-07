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
    ///     Manager for working with vehicle data.
    /// </summary>

    public class VehicleManager : IVehicleManager
    {
        IVehicleAccessor _vehicleAccessor = null;
        private List<string> _vehicleTypes = null;
        private List<string> _vehicleMakes = null;
        private List<string> _vehicleModels = null;

        // default constructor
        public VehicleManager()
        {
            _vehicleAccessor = new VehicleAccessor();
        }

        // test constructor
        public VehicleManager(IVehicleAccessor vehicleAccessor)
        {
            _vehicleAccessor = vehicleAccessor;
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
    }
}
