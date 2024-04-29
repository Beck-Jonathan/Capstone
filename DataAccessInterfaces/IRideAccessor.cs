using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IRideAccessor
    {
        IEnumerable<Ride_VM> SelectRidesByClientID(int clientID);

        Ride_VM SelectRideById(int rideID);

        int InsertRide(Ride ride);

        int UpdateRide(Ride ride);

        int UpdateRideAsActive(int id, bool active);
    }
}
