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
        bool AddVehicleModel(VehicleModel vehicleModel);
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

        /// <summary>
        ///     Adds a new vehicle model
        /// </summary>
        /// <param name="vehicleModel">
        ///    The VehicleModel being added
        /// </param>
        /// <returns>
        ///    <see cref="bool">bool</see>: Whether the vehicle model was successfully added
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="VehicleModel">VehicleModel</see> vehicleModel: The VehicleModel being added
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-03-19
        /// </remarks>
        public bool AddVehicleModel(VehicleModel vehicleModel)
        {
            int rowsAffected = _vehicleModelAccessor.InsertVehicleModel(vehicleModel);

            if (rowsAffected == 0)
            {
                throw new ApplicationException("Failed to insert vehicle model");
            }

            return true;
        }
    }
}
