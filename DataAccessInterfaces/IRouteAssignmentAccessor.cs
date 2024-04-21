using DataObjects.Assignment;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// AUTHOR: Steven Sanchez
    /// DATE: 2024-03-24
    /// Database Logic for Route Assignments
    /// </summary>
    /// <br /><br />
    ///    UPDATER: 
    /// <br />
    ///    UPDATED: 
    /// <br />
    ///     Update Comments
    /// </remarks>
    public interface IRouteAssignmentAccessor
    {
        IEnumerable<Route_Assignment_VM> GetAllRouteAssignmentByDriverID(int Driver_ID);
        int AddRouteAssignment(int driverID, string vin, int routeID, DateTime start, DateTime end);
        int AddVehicleAndDriverUnavailabilities(string vin, int driverID, DateTime start, DateTime end, string reason);
        List<VehicleAssignment> GetAvailableVehicles(DateTime start, DateTime end, int passengerCount);
        List<Driver> GetAvailableDrivers(DateTime start, DateTime end, int passengerCount);
        List<Route_Assignment> GetRouteAssignmentsByRouteIDAndDate(int routeID, DateTime start, DateTime end);
    }
}
