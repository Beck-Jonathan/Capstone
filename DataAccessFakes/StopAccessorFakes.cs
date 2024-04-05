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
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-03-24
    /// Fakes for stop manager tests.
    /// </summary>
    public class StopAccessorFakes : IStopAccessor
    {
        List<Stop> _fakeStops;

        public StopAccessorFakes()
        {
            _fakeStops = new List<Stop>();
            _fakeStops.Add(new Stop()
            {
                StopId = 100000,
                StreetAddress = "123 Fake Street",
                ZIPCode = "52404",
                Latitude = 41.9779m,
                Longitude = 91.6656m,
                IsActive = true
            });
            _fakeStops.Add(new Stop()
            {
                StopId = 100001,
                StreetAddress = "321 Test Street",
                ZIPCode = "52240",
                Latitude = 41.6578m,
                Longitude = 91.5346m,
                IsActive = true
            });
            _fakeStops.Add(new Stop()
            {
                StopId = 100002,
                StreetAddress = "234 Fake Drive",
                ZIPCode = "50010",
                Latitude = 42.0308m,
                Longitude = 93.6319m,
                IsActive = false
            });
            _fakeStops.Add(new Stop()
            {
                StopId = 100003,
                StreetAddress = "432 Test Parkway",
                ZIPCode = "52801",
                Latitude = 41.5236m,
                Longitude = 90.5776m,
                IsActive = false
            });
        }

        public int InsertStop(Stop inStop)
        {
            int stopID = 0;
            foreach (var stop in _fakeStops)
            {
                if (stop.StopId == inStop.StopId)
                {
                    throw new ArgumentException("Stop already exists.");
                }
            }
            if(stopID == 0)
            {
                _fakeStops.Add(inStop);
                stopID = inStop.StopId;
            }
            return stopID;
        }

        public Stop SelectStopByID(int stopID)
        {
            throw new NotImplementedException();
        }

        public List<Stop> SelectStops()
        {
            return _fakeStops;
        }

        public int UpdateStop(Stop oldStop, Stop newStop)
        {
            int result = 0;
            Stop temp = null;
            for (int i = 0; i < _fakeStops.Count ; i++)
            {
                if (_fakeStops[i].StopId == oldStop.StopId) {
                    _fakeStops[i] = newStop;
                    result = 1;
                    break;
                }
            }
            

            return result;
        }

        public int UpdateStopByIDAsActive(int stopID)
        {
            throw new NotImplementedException();
        }

        public int UpdateStopByIDAsInactive(int stopID)
        {
            throw new NotImplementedException();
        }
    }
}
