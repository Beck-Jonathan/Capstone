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

        bool UpdateVehicleModel(VehicleModelVM oldModel, VehicleModelVM newModel);
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
        private IParts_InventoryManager _partsManager;

        public VehicleModelManager()
        {
            _vehicleModelAccessor = new VehicleModelAccessor();
            _partsManager = new Parts_InventoryManager();
        }

        public VehicleModelManager(IVehicleModelAccessor vehicleModelAccessor)
        {
            _vehicleModelAccessor = vehicleModelAccessor;
        }

        public VehicleModelManager(IVehicleModelAccessor vehicleModelAccessor, IParts_InventoryManager partsManager)
        {
            _vehicleModelAccessor = vehicleModelAccessor;
            _partsManager = partsManager;
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
        /// <summary>
        ///     Updates a vehicle model
        /// </summary>
        /// <param name="vehicleModel">
        ///    The VehicleModel being added
        /// </param>
        /// <returns>
        ///    <see cref="bool">bool</see>: Whether the vehicle model was successfully updated
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="VehicleModelVM">VehicleModelVM</see> oldModel: The original version of the vehicle
        /// <br /><br />
        /// /// <br />
        ///    <see cref="VehicleModelVM">VehicleModelVM</see> newModel: the udpated version of the vehicle
        /// <br /><br />
        ///    CONTRIBUTOR: Jonathan Beck
        /// <br />
        ///    CREATED: 2024-03-24
        /// </remarks>

        public bool UpdateVehicleModel(VehicleModelVM oldModel, VehicleModelVM newModel)
        {
            
            int updates = 0;
            try
            {
                foreach (Parts_Inventory part in oldModel.Compatible_Parts)
                {
                    bool delete = true;
                    foreach (Parts_Inventory part2 in newModel.Compatible_Parts)
                    {
                        if (part2.Parts_Inventory_ID == part.Parts_Inventory_ID) {
                            delete = false; break;
                        }
                    }
                    if (delete) 
                    {
                        _partsManager.PurgeModelPartCompatibility(oldModel.VehicleModelID, part.Parts_Inventory_ID);
                        updates++;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return (updates > 0);
        }
    }
}
