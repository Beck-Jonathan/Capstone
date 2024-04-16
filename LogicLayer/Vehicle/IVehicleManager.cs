using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-02-01
    ///     Logic manager interface for working with vehicle data.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Everett DeVaux
    /// <br />
    /// UPDATED: 2024-02-13
    /// <br />
    /// 
    ///Initial creation
    ///Added Vehicle Lookup List
    /// </remarks>
    /// <remarks>
    /// UPDATER: Chris Baenizger
    /// UPDATED: 2024-02-23
    /// Added method for deactivate vehicle.
    /// </remarks>

    public interface IVehicleManager
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
        bool AddVehicle(Vehicle vehicle);

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
        List<string> GetVehicleTypes();

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
        List<string> GetVehicleMakes();

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
        List<string> GetVehicleModels();

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
        List<Vehicle> VehicleLookupList();

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
        Vehicle GetVehicleByVehicleNumber(string vehicleNumber);

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
        bool EditVehicle(Vehicle oldVehicle, Vehicle newVehicle);

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
        bool AddModelLookup(Vehicle vehicle);
        
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
        Vehicle GetModelLookupID(Vehicle vehicle);

        /// <summary>
        ///     Deactivate a vehicle in the database.
        /// </summary>
        /// <param name="Vehicle">
        ///    The vehicle information to be used to verify the vehicle to deactivate.
        /// </param>
        /// <returns>
        ///    <see cref="bool">bool</see>: bool if the vehicle was deactivated.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem deactivating the vehicle.
        ///    CONTRIBUTOR: Chris Baenziger
        ///    CREATED: 2024-02-22
        /// </remarks>
        bool DeactivateVehicle(Vehicle vehicle);

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

        List<ServiceOrder_VM> getAllService_OrderByVIN(string VIN);
    }
}
