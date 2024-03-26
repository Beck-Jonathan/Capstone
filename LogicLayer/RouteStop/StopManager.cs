using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessInterfaces;
using DataAccessFakes;

namespace LogicLayer.RouteStop
{
    public class StopManager : IStopManager
    {
        public IStopAccessor _stopAccessor;
        public StopManager()
        {
            //_stopAccessor = new StopAccessor();
        }
        public StopManager(IStopAccessor stopAccessor)
        {
            _stopAccessor = stopAccessor;
        }

        public int ActivateStop(int stopId)
        {
            throw new NotImplementedException();
        }

        public int AddStop(Stop stop)
        {
            throw new NotImplementedException();
        }

        public int DeactivateStop(int stopId)
        {
            throw new NotImplementedException();
        }

        public int EditStop(Stop oldStop, Stop newStop)
        {
            throw new NotImplementedException();
        }

        public Stop getStopById(int stopId)
        {
            throw new NotImplementedException();
        }

        public List<Stop> getStops()
        {
            throw new NotImplementedException();
        }
    }
}
