using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.RouteAssignment
{
    public class ActiveRouteManager : IActiveRouteManager
    {
        IActiveRouteAccessor _ActiveRouteAccessor = null;

        public ActiveRouteManager()
        {
            _ActiveRouteAccessor = new ActiveRouteAccessor();
        }

        public ActiveRouteManager(IActiveRouteAccessor activeRouteAccessor)
        {
            _ActiveRouteAccessor = activeRouteAccessor;
        }

        public bool AddActiveRoute(ActiveRoute activeRoute)
        {
            bool result = false;

            try
            {
                result = (1 == _ActiveRouteAccessor.AddActiveRoute(activeRoute));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error adding active route.", ex);
            }

            return result;
        }

        public bool EndActiveRoute(ActiveRoute activeRoute)
        {
            bool result = false;

            try
            {
                result = (1 == _ActiveRouteAccessor.EndActiveRoute(activeRoute));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error ending route.", ex);
            }

            return result;
        }
    }
}
