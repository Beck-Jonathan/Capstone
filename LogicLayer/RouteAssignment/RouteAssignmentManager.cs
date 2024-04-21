using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects.Assignment;
using DataObjects.HelperObjects;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.RouteAssignment
{
    /// <summary>
    /// AUTHOR: Steven Sanchez
    /// DATE: 2024-03-24
    ///  Manages Interaction Logic for Route Assignments
    /// </summary>
    /// <br /><br />
    ///    UPDATER: 
    /// <br />
    ///    UPDATED: 
    /// <br />
    ///     Update Comments
    /// </remarks>
    public class RouteAssignmentManager : IRouteAssignmentManager
    {
        IRouteAssignmentAccessor _routeAssignmentAccessor;
        IRouteStopAccessor _routeStopAccessor;
        IBingMapsAccessor _bingMapsAccessor;
        public RouteAssignmentManager()
        {
            _routeAssignmentAccessor = new RouteAssignmentAccessor();
            _routeStopAccessor = new RouteStopAccessor();
            _bingMapsAccessor = new BingMapsAccessor();
        }
        public RouteAssignmentManager(IRouteAssignmentAccessor routeAssignmentAccessor)
        {
            _routeAssignmentAccessor = routeAssignmentAccessor;
            _routeStopAccessor = new RouteStopAccessor();
            _bingMapsAccessor = new BingMapsAccessor();
        }
        public IEnumerable<Route_Assignment_VM> GetAllRouteAssignmentByDriverID(int Driver_ID)
        {
            List<Route_Assignment_VM> results = _routeAssignmentAccessor.GetAllRouteAssignmentByDriverID(Driver_ID).ToList();
            try
            {
                foreach (Route_Assignment_VM rs in results)
                {
                    if (rs != null && rs.routeVM != null)
                    {
                        rs.routeVM.RouteStops = _routeStopAccessor.selectRouteStopByRouteId(rs.routeVM.RouteId);
                    }
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error getting list of Routes for Driver");
            }

            return results;
        }
        /// <summary>
        /// AUTHOR: Steven Sanchez
        /// <br />
        /// CREATED: 2024-04-06
        /// <br />
        ///     Asynchronous method used to pull data for navigation from the Bing Maps API.
        /// </summary>
        /// <param name="route">The route whose path we're trying to calculate.</param>
        /// <returns>
        /// <see cref="Task">A Task</see> Object which, when awaited, returns a <see cref="BingMapsResponse">BingMapsResponse</see> Object.
        /// </returns>
        public async Task<BingMapsResponse> getRouteLineForRouteAssignmentVM(IEnumerable<Route_Assignment_VM> route)
        {
            BingMapsResponse result;
            try
            {
                result = await _bingMapsAccessor.getMapPolylineForRouteAssignmentVM(route);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Map service unavailable at this time, sorry!");
            }
            return result;
        }

        /// <summary>
        ///     Add Route assignment to database
        /// </summary>
        /// <param name="driverID">
        ///    The ID of the driver to be assigned to the route assignment
        /// </param>
        ///  <param name="vin">
        ///    VIN of vehicle to be added to route assignment
        /// </param>
        /// <param name="routeID">
        ///    ID of route to be added to route assignment
        /// </param>
        /// <param name="start">
        ///    Start date of assignment
        /// </param>
        /// <param name="end">
        ///    End date of assignment
        /// </param>
        /// <returns>
        ///    Boolean, true if successful false if not
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="SystemException">SystemException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: James Williams
        ///    CREATED: 2024-04-17
        /// </remarks>
        public bool AddRouteAssignment(int driverID, string vin, int routeID, DateTime start, DateTime end)
        {
            bool results = false;
            try
            {
                results = 1 == _routeAssignmentAccessor.AddRouteAssignment(driverID, vin, routeID, start, end);
            }
            catch (Exception ex)
            {
                throw new SystemException("Database error", ex);
            }
            return results;
        }

        /// <summary>
        ///     Add Vehicle unavailability record to database
        /// </summary>
        ///  <param name="vin">
        ///    VIN of vehicle to be added to route assignment
        /// </param>
        /// <param name="driverID">
        ///    The ID of the driver to be assigned to the route assignment
        /// </param>
        /// <param name="start">
        ///    Start date of assignment
        /// </param>
        /// <param name="end">
        ///    End date of assignment
        /// </param>
        /// <param name="reason">
        ///    Explaination of unavailability being placed
        /// </param>
        /// <returns>
        ///    Boolean, true if successful false if not
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="SystemException">SystemException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: James Williams
        ///    CREATED: 2024-04-17
        /// </remarks>
        public bool AddVehicleAndDriverUnavailabilites(string vin, int driverID, DateTime start, DateTime end, string reason)
        {
            bool results = false;
            try
            {
                results = 2 == _routeAssignmentAccessor.AddVehicleAndDriverUnavailabilities(vin, driverID, start, end, reason);
            }
            catch (Exception ex)
            {
                throw new SystemException("Database error", ex);
            }
            return results;
        }

        /// <summary>
        ///     Retreives driver records from database that are available between the given dates and that
        ///     have the appropriate license class to carry the passenger count.
        /// </summary>
        /// <param name="start">
        ///    Start date of assignment
        /// </param>
        /// <param name="end">
        ///    End date of assignment
        /// </param>
        /// <param name="passengerCount">
        ///   Number of passengers anticipated
        /// </param>
        /// <returns>
        ///    List of Driver objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="SystemException">SystemException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: James Williams
        ///    CREATED: 2024-04-17
        /// </remarks>
        public List<Driver> GetAvailableDriversByDateAndPassengerCount(DateTime start, DateTime end, int passengerCount)
        {
            List<Driver> drivers = new List<Driver>();
            try
            {
                drivers = _routeAssignmentAccessor.GetAvailableDrivers(start, end, passengerCount);
                if (drivers.Count == 0)
                {
                    throw new ArgumentException("No available drivers found");
                }
            }
            catch (Exception ex)
            {

                throw new SystemException("Database error", ex);
            }
            return drivers;
        }
        /// <summary>
        ///     Retreives vehicle records from database that are available between the given dates and that
        ///     can accomodate the anticiapted passenger count.
        /// </summary>
        /// <param name="start">
        ///    Start date of assignment
        /// </param>
        /// <param name="end">
        ///    End date of assignment
        /// </param>
        /// <param name="passengerCount">
        ///   Number of passengers anticipated
        /// </param>
        /// <returns>
        ///    List of VehicleAssignment objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="SystemException">SystemException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: James Williams
        ///    CREATED: 2024-04-17
        /// </remarks>
        public List<VehicleAssignment> GetAvailableVehiclesByDateAndPassengerCount(DateTime start, DateTime end, int passengerCount)
        {
            List<VehicleAssignment> vehicles = new List<VehicleAssignment>();
            try
            {
                vehicles = _routeAssignmentAccessor.GetAvailableVehicles(start, end, passengerCount);
                if (vehicles.Count == 0)
                {
                    throw new ArgumentException("No available vehicles found");
                }
            }
            catch (Exception ex)
            {

                throw new SystemException("Database error", ex);
            }
            return vehicles;
        }
        /// <summary>
        ///     Retreives route assignments from database that match the routeId and are within the 
        ///     start and end date ranges 
        /// </summary>
        /// <param name="routeID">
        ///   The ID of the route 
        /// </param>
        /// <param name="start">
        ///    Start date of assignment
        /// </param>
        /// <param name="end">
        ///    End date of assignment
        /// </param>
        /// <returns>
        ///    List of Driver objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="SystemException">SystemException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: James Williams
        ///    CREATED: 2024-04-17
        /// </remarks>
        public List<Route_Assignment> GetRouteAssignmentsByRouteIDAndDate(int routeID, DateTime start, DateTime end)
        {
            List<Route_Assignment> assignments = new List<Route_Assignment>();
            try
            {
                assignments = _routeAssignmentAccessor.GetRouteAssignmentsByRouteIDAndDate(routeID, start, end);
            }
            catch (Exception ex)
            {

                throw new SystemException("Database error", ex);
            }
            return assignments;
        }
    }
}
