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
    }
}
