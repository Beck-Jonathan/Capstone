using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects.RouteObjects;

namespace LogicLayer.RouteStop
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-03-24
    /// Interface for stop manager
    /// </summary>
    public interface IStopManager
    {
        Stop GetStopByID(int stopID);
        List<Stop> GetStops();
        int AddStop(Stop stop);
        bool EditStop(Stop oldStop, Stop newStop);
        bool DeactivateStop(int stopID);
        bool ActivateStop(int stopID);
    }
}
