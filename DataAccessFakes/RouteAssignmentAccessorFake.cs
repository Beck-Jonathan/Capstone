using DataAccessInterfaces;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// AUTHOR: Steven Sanchez
    /// DATE: 2024-03-24
    /// Fake Database Access for Route Assignment Unit Tests
    /// </summary>
    ///    UPDATER: 
    /// <br />
    ///    UPDATED: 
    /// <br />
    ///    Update Comments
    /// </remarks>
    public class RouteAssignmentAccessorFake : IRouteAssignmentAccessor
    {
        private List<Route_Assignment_VM> route_Assignments;

        public RouteAssignmentAccessorFake()
        {
            route_Assignments = new List<Route_Assignment_VM>
            {
                new Route_Assignment_VM()
                {
                    Assignment_ID = 1,
                    DriverID = 1,
                    Route_ID = 1,
                    VIN_Number = "12345678901234567",
                    Date_Assignment_Started = DateTime.Now,
                    Date_Assignment_Ended = DateTime.Now,
                },
                new Route_Assignment_VM()
                {
                    Assignment_ID = 4,
                    DriverID = 1,
                    Route_ID = 3,
                    VIN_Number = "123456789012345SS",
                    Date_Assignment_Started = DateTime.Now,
                    Date_Assignment_Ended = DateTime.Now,
                },
                new Route_Assignment_VM()
                {
                    Assignment_ID = 2,
                    DriverID = 2,
                    Route_ID = 2,
                    VIN_Number = "09876543210987654",
                    Date_Assignment_Started = DateTime.Now,
                    Date_Assignment_Ended = DateTime.Now,
                },
                 new Route_Assignment_VM()
                {
                    Assignment_ID = 3,
                    DriverID = 3,
                    Route_ID = 3,
                    VIN_Number = "asdfghjklqwe12346",
                    Date_Assignment_Started = DateTime.Now,
                    Date_Assignment_Ended = DateTime.Now,
                }

            };

        }
        public IEnumerable<Route_Assignment_VM> GetAllRouteAssignmentByDriverID(int Driver_ID)
        {
            return route_Assignments.Where(a => a.DriverID == Driver_ID);
        }
    }
}
