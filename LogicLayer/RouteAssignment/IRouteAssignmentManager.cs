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
    /// Interaction Logic for Route Assignments
    /// </summary>
    /// <br /><br />
    ///    UPDATER: 
    /// <br />
    ///    UPDATED: 
    /// <br />
    ///     Update Comments
    /// </remarks>
    public interface IRouteAssignmentManager
    {
        IEnumerable<Route_Assignment_VM> GetAllRouteAssignmentByDriverID(int Driver_ID);
        Task<BingMapsResponse> getRouteLineForRouteAssignmentVM(IEnumerable<Route_Assignment_VM> route);
        bool AddRouteAssignment(int driverID, string vin, int routeID, DateTime start, DateTime end);
        bool AddVehicleAndDriverUnavailabilites(string vin, int driverID, DateTime start, DateTime end, string reason);
        List<VehicleAssignment> GetAvailableVehiclesByDateAndPassengerCount(DateTime start, DateTime end, int passengerCount);
        List<Driver> GetAvailableDriversByDateAndPassengerCount(DateTime start, DateTime end, int passengerCount);
        List<Route_Assignment> GetRouteAssignmentsByRouteIDAndDate(int routeID, DateTime start, DateTime end);
        Driver GetRouteAssignmentDriverByAssignmentID(int routeAssignmentID);
        List<VehicleAssignment> GetAvailableVehiclesByDate(DateTime start, DateTime end);
        List<Driver> GetAvailableDriversByDate(DateTime start, DateTime end);
        bool UpdateRouteAssignmentDriver(int routeAssignmentID, int driverID);
        bool UpdateRouteAssignmentVehicle(int routeAssignmentID, string vin);
    }
}
