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
    /// CREATED: 2024-04-23
    ///     Fake active route accessor for testing.
    /// </summary>
    public class ActiveRouteAccessorFakes : IActiveRouteAccessor
    {
        List<ActiveRoute> activeRoutes = new List<ActiveRoute>();
        ActiveRoute selectedActiveRoute;

        public ActiveRouteAccessorFakes()
        {
            activeRoutes.Add(new ActiveRoute()
            {
                AssignmentID = 100000,
                DriverID = 100000,
                VIN = "1HGCM82633A123456",
                StartTime = DateTime.Now
            });
            activeRoutes.Add(new ActiveRoute()
            {
                AssignmentID = 100002,
                DriverID = 100002,
                VIN = "JM1BK32F781234567",
                StartTime = DateTime.Now
            });

            selectedActiveRoute = activeRoutes[1];
        }

        public int AddActiveRoute(ActiveRoute inRoute)
        {
            int rows = 0;
            foreach (ActiveRoute route in activeRoutes)
            {
                if (route.AssignmentID == inRoute.AssignmentID &&
                    route.DriverID == inRoute.DriverID)
                {
                    rows++;
                }
            }
            if (rows == 1)
            {
                throw new ArgumentException();
            }
            return 1;
        }

        public int EndActiveRoute(ActiveRoute inRoute)
        {
            int rows = 0;
            foreach (ActiveRoute route in activeRoutes)
            {
                if (route.AssignmentID == inRoute.AssignmentID &&
                    route.DriverID == inRoute.DriverID)
                {
                    rows++;
                }
            }
            if (rows == 0)
            {
                throw new ArgumentException();
            }
            return rows;
        }
    }
}
