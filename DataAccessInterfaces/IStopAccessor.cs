using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects.RouteObjects;

namespace DataAccessInterfaces
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-03-24
    /// Interface for stop accessor
    /// </summary>
    public interface IStopAccessor
    {
        Stop SelectStopByID(int stopID);
        List<Stop> SelectStops();
        int UpdateStop(Stop oldStop, Stop newStop);
        int InsertStop(Stop stop);
        int UpdateStopByIDAsInactive(int stopID);
        int UpdateStopByIDAsActive(int stopID);
    }
}
