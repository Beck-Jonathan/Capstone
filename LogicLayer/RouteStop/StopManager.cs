
﻿using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.RouteStop
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-03-24
    /// Stop manager
    /// </summary>
    public class StopManager : IStopManager
    {
        IStopAccessor _stopAccessor;

        public StopManager()
        {
            _stopAccessor = new StopAccessor();
        }

        public StopManager(IStopAccessor stopAccessor)
        {
            _stopAccessor = stopAccessor;
        }

        public bool ActivateStop(int stopID)
        {
            throw new NotImplementedException();
        }
		
        /// <summary>
        ///     Add a stop to database.
        /// </summary>
        /// <param name="Stop">
        ///    The stop information to be added.
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: The stop number that was added.
        /// </returns>
        /// <remarks>
        ///    Parameters:
        ///    <see cref="Stop">Stop</see> The stop information to be added.
        ///    Exceptions:
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: Chris Baenziger
        ///    CREATED: 2024-03-26
        /// </remarks>
        public int AddStop(Stop stop)
        {
            int stopID = 0;
            try
            {
                stopID = _stopAccessor.InsertStop(stop);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to add stop.", ex);
            }
            if (stopID == 0)
            {
                throw new ArgumentException("Unable to add stop.");
            }
            return stopID;
        }

        public bool DeactivateStop(int stopID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Edit a stop in the database
        /// </summary>
        /// <param name="oldStop">
        ///    The stop to be edited.
        /// </param>
        /// <param name="newStop">
        ///    The new info for the stop.
        /// </param>
        /// <returns>
        ///    <see cref="bool">bool</see>: True/false, was a stop updated
        /// </returns>
        /// <remarks>
        ///    Parameters <br/>:
        ///    <see cref="Stop">oldStop</see> The stop to be edited. <br/>
        ///    <see cref="Stop">newStop</see>  The new info for the stop. <br/>
        ///    Exceptions: <br/>
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown if there is a problem writing to the DB.
        ///    CONTRIBUTOR: Jonathan Beck
        ///    CREATED: 2024-04-02
        /// </remarks>
        public bool EditStop(Stop oldStop, Stop newStop)
        {
            bool result = false;
            try
            {
                result=(1== _stopAccessor.UpdateStop(oldStop, newStop));
                if (!result)
                {
                    throw new ArgumentException("Unable to update Stop");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Stop GetStopByID(int stopID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Select all stops from the database.
        /// </summary>
        /// <returns>
        ///    <see cref="List</Stop>">List</see>: The list of all stops.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown if there is a problem retrieving from the DB.
        ///    CONTRIBUTOR: Chris Baenziger
        ///    CREATED: 2024-03-26
        /// </remarks>
        public List<Stop> GetStops()
        {
            List<Stop> stopList = null;

            try
            {
                stopList = _stopAccessor.SelectStops();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to retrieve routes.", ex);
            }
            if (stopList.Count == 0) 
            {
                throw new ApplicationException();
            }

            return stopList;
        }
    }
}
