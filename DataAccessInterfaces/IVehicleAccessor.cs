using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    /// <summary>
    /// AUTHOR: Chris Baenziger, Everett DeVaux
    /// CREATED: 2024-02-01
    ///     Data access interface for accessing vehicle information from the database.
    /// </summary>
    /// <remarks>
    /// UPDATER: Everett DeVaux
    /// <br />
    /// UPDATED: 2024-02-13
    /// <br />
    /// 
    ///     Update comments go here, include method or methods were changed or added 
    ///     (no other details necessary).
    ///     A new remark should be added for each update.
    /// </remarks>

    public interface IVehicleAccessor
    {
        int AddVehicle(Vehicle vehicle);
        List<string> SelectVehicleTypes();
        List<string> SelectVehicleMakes();
        List<string> SelectVehicleModels();

        List<Vehicle> SelectVehicleForLookupList();
    }
}
