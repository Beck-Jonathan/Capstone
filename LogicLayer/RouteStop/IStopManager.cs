using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects.RouteObjects;

namespace LogicLayer.RouteStop
{
    public interface IStopManager
    {
        Stop getStopById(int stopId);
        List<Stop> getStops();
        int AddStop(Stop stop);
        int EditStop(Stop oldStop, Stop newStop);
        int DeactivateStop(int stopId);
        int ActivateStop(int stopId);
    }
}
