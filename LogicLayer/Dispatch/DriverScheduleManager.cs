using DataAccessFakes;
using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class DriverScheduleManager : IDriverScheduleManager
    {
        IDispatchAccessor _dispatchAccessor = null;

        //Default Constructor
        public DriverScheduleManager()
        {
            _dispatchAccessor = new DispatchAccessor();
        }


        public DriverScheduleManager(IDispatchAccessor dispatchAccessor)
        {
            _dispatchAccessor = dispatchAccessor;
        }


        public List<Dispatch> GetDriverScheduleList()
        {
            List<Dispatch> driverSchedules = new List<Dispatch>();
            try
            {
                driverSchedules = _dispatchAccessor.SelectDriverScheduleForList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving driver's schedules.", ex);
            }
            return driverSchedules;
        }

    }
}
