using DataAccessInterfaces;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class StopAccessorFake : IStopAccessor
    {
        List<Stop> _fakeStops;
        public StopAccessorFake() 
        {
            _fakeStops = new List<Stop>()
            {
                new Stop()
                        {
                            StopId = 0,
                            StreetAddress = "6301 Kirkwood Blvd SW, Cedar Rapids, IA",
                            ZIPCode = "52404",
                            Latitude = 41.917250m,
                            Longitude = -91.656470m,
                            IsActive = true
                        },
                new Stop()
                        {
                            StopId = 1,
                            StreetAddress = "5008, 1220 1st Ave NE, Cedar Rapids, IA",
                            ZIPCode = "52402",
                            Latitude = 41.989670m,
                            Longitude = -91.649529m,
                            IsActive = false
                        },
                new Stop()
                        {
                            StopId = 2,
                            StreetAddress = "1330 Elmhurst Dr NE, Cedar Rapids, IA",
                            ZIPCode = "52402",
                            Latitude = 42.002548m,
                            Longitude = -91.652069m,
                            IsActive = true
                        }
            };
        }
        public int ActivateStop(Stop stop)
        {
            int result = 0;
            foreach (Stop fakeStop in _fakeStops)
            {
                if (stop.StopId == fakeStop.StopId && !fakeStop.IsActive)
                {
                    result = fakeStop.StopId;
                    fakeStop.IsActive = true;
                    break;
                }
            }
            return result;
        }

        public int DeactivateStop(Stop stop)
        {
            int result = 0;
            foreach(Stop fakeStop in _fakeStops)
            {
                if(stop.StopId == fakeStop.StopId && fakeStop.IsActive)
                {
                    result = fakeStop.StopId;
                    fakeStop.IsActive = false;
                    break;
                }
            }
            return result;
        }

        public int InsertStop(Stop stop)
        {
            if(_fakeStops.Where(fakeStop => fakeStop.StopId == stop.StopId).Any())
            {
                throw new Exception("Stop with that id already exists");
            }
            _fakeStops.Add(stop);
            return _fakeStops.Count;
        }

        public Stop selectStopById(int stopId)
        {
            return _fakeStops.Where(stop =>  stop.StopId == stopId).FirstOrDefault();
        }

        public List<Stop> selectStops()
        {
            return _fakeStops;
        }

        public int UpdateStop(Stop oldStop, Stop newStop)
        {
            int result = 0;
            if (oldStop.StopId != newStop.StopId) { throw new ArgumentException("Stop Ids don't match"); }
            for(int i = 0; i < _fakeStops.Count; i++)
            {
                if (_fakeStops[i].StopId == oldStop.StopId && oldStop.StopId == newStop.StopId)
                {
                    _fakeStops[i] = newStop;
                    result = i;
                    break;
                }                 
            }
            return result;
        }
    }
}
