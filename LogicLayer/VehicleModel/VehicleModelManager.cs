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
    /// <summary>
    /// Performs vehicle model-related operations
    /// </summary>
    public interface IVehicleModelManager
    {
        IEnumerable<VehicleModel> GetVehicleModels();
    }

    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-03-02
    /// <br />
    ///     Performs vehicle model-related operations
    /// </summary>
    public class VehicleModelManager : IVehicleModelManager
    {
        private IVehicleModelAccessor _vehicleModelAccessor;

        public VehicleModelManager()
        {
            _vehicleModelAccessor = new VehicleModelAccessor();
        }

        public VehicleModelManager(IVehicleModelAccessor vehicleModelAccessor)
        {
            _vehicleModelAccessor = vehicleModelAccessor;
        }

        /// <summary>
        ///     Retrieves all active vehicle models
        /// </summary>
        /// <returns>
        ///    <see cref="IEnumerable{VehicleModel}">IEnumerable&lt;VehicleModel&gt;</see>: All active vehicle models
        /// </returns>
        public IEnumerable<VehicleModel> GetVehicleModels()
        {
            return _vehicleModelAccessor.GetVehicleModels();
        }
    }
}
