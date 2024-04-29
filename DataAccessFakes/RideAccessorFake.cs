using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class RideAccessorFake : IRideAccessor
    {
        private List<Ride_VM> _fakeRides;
        
        public RideAccessorFake(List<Ride_VM> fakeRides)
        {
            _fakeRides = fakeRides;
        }

        public int InsertRide(Ride ride)
        {
            if (ride.VIN == "test_fail")
            {
                return -1;
            }

            if (ride.VIN == "test_except")
            {
                throw new Exception();
            }

            Ride_VM newRide = new Ride_VM { VIN = ride.VIN };

            _fakeRides.Add(newRide);

            return 100000;
        }

        public Ride_VM SelectRideById(int rideID)
        {
            if (rideID == -2)
            {
                throw new Exception();
            }

            return _fakeRides.FirstOrDefault(r => rideID == r.RideID);
        }

        public IEnumerable<Ride_VM> SelectRidesByClientID(int clientID)
        {
            if (clientID == -1)
            {
                throw new Exception();
            }

            return _fakeRides;
        }

        public int UpdateRide(Ride ride)
        {
            if (ride.RideID == -1)
            {
                return 0;
            }

            if (ride.RideID == -1)
            {
                throw new Exception();
            }

            _fakeRides.Where(r => ride.RideID == r.RideID).FirstOrDefault().VIN = ride.VIN;

            return 1;
        }

        public int UpdateRideAsActive(int id, bool active)
        {
            if (id == -1)
            {
                return 0;
            }

            if (id == -2)
            {
                throw new Exception();
            }

            _fakeRides.Where(r => id == r.RideID).FirstOrDefault().IsActive = active;

            return 1;
        }
    }
}
