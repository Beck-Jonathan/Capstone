using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    /// <summary>
    /// AUTHOR: Chris Baenziger, Everett DeVaux
    /// CREATED: 2024-02-01
    ///     Data access interface for accessing vehicle information from the database.
    /// </summary>
    /// <remarks>
    /// UPDATER: Everett DeVaux
    /// UPDATED: 2024-02-13
    /// <br />
    /// </remarks>
    /// <remarks>
    /// UPDATER: Chris Baenizger
    /// UPDATED: 2024-02-23
    /// <br />
    /// UPDATER: Ben Collins
    /// UPDATED: 2024-03-24
    /// <br />
    /// Added method for deactivate vehicle.
    /// Added SelectVehicleByVIN(string VIN).
    /// UPDATER: Chris Baenziger
    /// UPDATED: 2024-04-20
    /// Added vehicle checklist
    /// </remarks>
    
    public interface IVehicleAccessor
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
        int AddVehicle(Vehicle vehicle);

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
        List<string> SelectVehicleTypes();

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
        List<string> SelectVehicleMakes();

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
        List<string> SelectVehicleModels();

        /// <summary>
        ///     Accessor that allows us to pull the specific info for the Vehicle Lookup List
        /// </summary>
        /// <returns>
        ///    <see cref="List{Vehicle}">Vehicle</see> List of Vehicle objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ApplicationException">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Everett DeVaux
        /// <br />
        ///    CREATED: 2024-02-10
        ///    Initial Creation
        /// <br />
        /// <br />
        ///    UPDATER: Chris Baenziger
        ///    UPDATED: 2024-02-17
        ///    Moved method comment inside of method
        /// </remarks>
        List<Vehicle> SelectVehicleForLookupList();

        /// <summary>
        ///     Add model lookup to the database.
        /// </summary>
        /// <param name="vehicle">
        ///    The vehicle information to be added as a model lookup object.
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
        ///    CREATED: 2024-02-12
        /// </remarks>
        int AddModelLookup(Vehicle vehicle);

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
        Vehicle SelectVehicleByVehicleNumber(string vehicleNumber);

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
        int UpdateVehicle(Vehicle oldVehicle, Vehicle newVehicle);

        /// <summary>
        ///     Select the model lookup id matching the vehicle to be added
        /// </summary>
        /// <param name="vehicle">
        ///    The vehicle information provide the make/model/year
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: model lookup id matching the make/model/year
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem updating the vehicle.
        ///    CONTRIBUTOR: Chris Baenziger
        ///    CREATED: 2024-02-10
        /// </remarks>
        int SelectModelLookupID(Vehicle vehicle);

        /// <summary>
        ///     Deactivate a vehicle in the database.
        /// </summary>
        /// <param name="vehicle">
        ///    Vehicle to be deactivated
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: Rows affected.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem deactivating the vehicle.
        ///    CONTRIBUTOR: Chris Baenziger
        ///    CREATED: 2024-02-22
        /// </remarks>
        /// <remarks>
        /// UPDATER: Chris Baenizger
        /// UPDATED: 2024-02-23
        /// Added method for deactivate vehicle.
        /// </remarks>
        int DeactivateVehicle(Vehicle vehicle);
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
        List<ServiceOrder_VM> SelectServiceOrdersByVin(String VIN);

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
        Vehicle SelectVehicleByVIN(string VIN);

        /// <summary>
        ///     Add vehicle checklist to the database.
        /// </summary>
        /// <param name="checklist">
        ///    The checklist information to be added as a Vehicle checklist object.
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: The ID for the new checklist.
        /// </returns>
        /// <remarks>
        ///    Parameters:
        ///    <see cref="VehicleChecklist">VehicleChecklist</see> Vehicle Checklist: The checklist information to be added.
        ///    Exceptions:
        ///    <see cref="SqlException">SqlException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: Chris Baenziger
        ///    CREATED: 2024-04-20
        /// </remarks>
        int AddVehicleChecklist(VehicleChecklist checklist);

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
        List<Vehicle> selectVehicleTuplesForDropDown();
    }
}
