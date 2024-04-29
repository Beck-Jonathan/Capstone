using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IRideManager
    {
        IEnumerable<Ride_VM> GetRidesByClientID(int clientID);

        Ride_VM GetRideByID(int rideID);

        bool AddRide(Ride_VM ride);

        bool EditRide(Ride_VM ride);

        bool DeactivateRide(int rideID);
    }

    /// <summary>
    /// AUTHOR: Jared Hutton, Jacob Wendt
    /// <br />
    /// CREATED: 2024-04-23
    /// <br />
    ///  Logic for managing scheduled rides
    /// </summary>
    public class RideManager : IRideManager
    {
        private IRideAccessor _rideAccessor;

        public RideManager(IRideAccessor rideAccessor)
        {
            _rideAccessor = rideAccessor;
        }

        public RideManager()
        {
            _rideAccessor = new RideAccessor();
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-23
        ///  <br />
        ///  Schedule a new ride
        /// </summary>
        /// <param name="ride">
        ///    The ride to schedule
        /// </param>
        public bool AddRide(Ride_VM ride)
        {
            ride.CalculatePickupTime();

            try
            {
                int rideId = _rideAccessor.InsertRide(ride);

                if (rideId < 100000)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to schedule the ride", ex);
            }

            return true;
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-23
        ///  <br />
        ///  Deactivate an existing ride
        /// </summary>
        /// <param name="rideID">
        ///    The ID of the ride to deactivate
        /// </param>
        public bool DeactivateRide(int rideID)
        {
            int rows = 0;

            try
            {
                rows = _rideAccessor.UpdateRideAsActive(rideID, false);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to cancel the ride", ex);
            }

            if (rows == 0)
            {
                throw new ArgumentException("Ride was not found");
            }

            return true;
        }

        /// <summary>
        ///  AUTHOR: Jacob Wendt
        ///  <br />
        ///  DATE: 2024-04-23
        ///  <br />
        ///  Retrieve rides for a specific client
        /// </summary>
        /// <param name="clientID">
        ///    The ID of the client
        /// </param>
        public IEnumerable<Ride_VM> GetRidesByClientID(int clientID)
        {
            IEnumerable<Ride_VM> rides;

            try
            {
                rides = _rideAccessor
                    .SelectRidesByClientID(clientID)
                    .OrderByDescending(r => r.IsActive)
                    .ThenBy(r => r.ScheduledPickupTime);
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to get rides.", ex);
            }
            return rides;
        }

        /// <summary>
        ///  AUTHOR: Jacob Wendt
        ///  <br />
        ///  DATE: 2024-04-23
        ///  <br />
        ///  Retrieve rides for a specific client
        /// </summary>
        /// <param name="clientID">
        ///    The ID of the client
        /// </param>
        public Ride_VM GetRideByID(int rideID)
        {
            Ride_VM ride = null;

            try
            {
                ride = _rideAccessor.SelectRideById(rideID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load details of scheduled ride", ex);
            }

            if (ride == null || !ride.IsActive)
            {
                throw new ArgumentException("Ride was not found");
            }

            ride.ScheduledDate = ride.ScheduledPickupTime.Date;
            ride.ScheduledTime = ride.ScheduledPickupTime.TimeOfDay;

            return ride;
        }

        /// <summary>
        ///  AUTHOR: Jared Hutton
        ///  <br />
        ///  DATE: 2024-04-23
        ///  <br />
        ///  Edits an existing scheduled ride
        /// </summary>
        /// <param name="ride">
        ///    The existing ride to edit
        /// </param>
        public bool EditRide(Ride_VM ride)
        {
            int rows = 0;

            ride.CalculatePickupTime();

            try
            {
                rows = _rideAccessor.UpdateRide(ride);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while trying to edit the ride", ex);
            }

            if (rows == 0)
            {
                throw new ArgumentException("Ride was not found");
            }

            return true;
        }
    }
}
