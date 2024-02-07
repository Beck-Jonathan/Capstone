using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// CREATED: 2024-02-01
    ///     Data access interface for accessing vehicle information from the database.
    /// </summary>

    public interface IVehicleAccessor
    {
        int AddVehicle(Vehicle vehicle);
        List<string> SelectVehicleTypes();
        List<string> SelectVehicleMakes();
        List<string> SelectVehicleModels();
    }
}
