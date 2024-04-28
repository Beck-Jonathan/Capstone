using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using DataAccessInterfaces;
using DataAccessLayer;

namespace LogicLayer.RouteStop
{
    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// DATE: 2024-03-05
    /// Interaction Logic for Route Stop
    /// </summary>
    public class RouteStopManager : IRouteStopManager
    {
        private IRouteStopAccessor _routeStopAccessor;
        public RouteStopManager()
        {
            _routeStopAccessor = new RouteStopAccessor();
        }
        public RouteStopManager(IRouteStopAccessor routeStopAccessor)
        {
            _routeStopAccessor = routeStopAccessor;
        }
        /// <summary>
        /// Adds a routestop relation to the database.
        /// </summary>
        /// <param name="routeStopVM">The RouteStop data to be added.</param>
        /// <returns><see cref="int">The ID of the inserted RouteStop object.</see></returns>
        /// <exception cref="ApplicationException">Caugh tand rewrapped from the layer below.</exception>
        public int AddRouteStop(RouteStopVM routeStopVM)
        {
            int result = 0;

            try
            {
                result = _routeStopAccessor.InsertRouteStop(routeStopVM);
            } catch (Exception ex)
            {
                throw new ApplicationException("Unable to insert", ex);
            }

            return result;
        }

        /// <summary>
        /// Deletes a routestop relation from the database.
        /// </summary>
        /// <param name="routeStopVM">The RouteStop data to be deleted.</param>
        /// <returns><see cref="int">The number of rows changed.</see></returns>
        /// <exception cref="ApplicationException">Bubbled up when an error happens in the accessor.</exception>
        public int DeleteRouteStop(RouteStopVM routeStopVM)
        {
            int result = 0;

            try
            {
                result = _routeStopAccessor.DeleteRouteStop(routeStopVM);
            } catch (Exception ex)
            {
                throw new ApplicationException("Unable to delete", ex);
            }

            return result;
        }

        public int EditRouteStop(RouteStopVM existingRouteStop, RouteStopVM newRouteStop)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///     Gets a list of all stops (And RouteStop data) for a given route
        /// </summary>
        /// <param name="routeId">
        ///    The ID of the route whose stops we want
        /// </param>
        /// <returns>
        ///    <see cref="IEnumerable{T}">IEnumerable</see>: The list of stops, in order
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="int">int</see> route: The ID of the route whose stops we want
        /// <br /><br />
        ///    Exceptions:
        /// <br />
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown when an error is caught from the data accessor
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// </remarks>

        public IEnumerable<RouteStopVM> GetRouteStopByRouteId(int routeId)
        {
            IEnumerable<RouteStopVM> results = null;
            try
            {
                results = _routeStopAccessor.selectRouteStopByRouteId(routeId);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Something went wrong, we're verry sorry!", e);
            }
            return results;
        }

        public bool UpdateOrdinal(RouteStopVM routeStop)
        {
            bool result = false;
            try
            {
                result = (1 == _routeStopAccessor.UpdateOrdinal(routeStop));
            } catch (Exception ex)
            {
                throw new ApplicationException("Unable to update database.", ex);
            }

            return result;
        }
    }
}
