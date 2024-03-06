using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects.HelperObjects;

namespace DataObjects.ScheduleObjects
{
    public interface IScheduleObject
    {
        DateTime getStartOfPeriod();
        DateTime getEndOfPeriod();
        Time getStartTime();
        Time getEndTime();
        bool conflictsWith(IScheduleObject other);

    }
}
