using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IStopAccessor
    {
        Stop selectStopById(int stopId);
        List<Stop> selectStops();
        int InsertStop(Stop stop);
        int UpdateStop(Stop oldStop, Stop newStop);
        int DeactivateStop(Stop stop);
        int ActivateStop(Stop stop);
    }
}
