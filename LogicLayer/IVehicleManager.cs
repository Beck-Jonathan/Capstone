using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-02-01
    ///     Logic manager interface for working with vehicle data.
    /// </summary>
    /// 
    /// UPDATER: Everett DeVaux
    /// <br />
    /// UPDATED: 2024-02-13
    /// <br />
    /// 
    ///     Initial creation
    ///     Added Vehicle Lookup List
    /// </remarks>

    public interface IVehicleManager
    {
        bool AddVehicle(Vehicle vehicle);
        List<string> GetVehicleTypes();
        List<string> GetVehicleMakes();
        List<string> GetVehicleModels();
        List<Vehicle> VehicleLookupList();
    }
}
