using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-03-02
    /// <br />
    ///     Provides access to test data stored in memory representing the vehicle model table
    /// </summary>
    public class VehicleModelAccessorFake : IVehicleModelAccessor
    {
        List<VehicleModel> _fakeVehicleModelData;
        List<Vehicle> _fakeVehicles = null;
        /// <summary>
        ///     Instantiates a fake vehicle model accessor; 
        ///     accepts a collection of vehicle model objects mimicking a data source.
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-03-02
        /// </remarks>
        public VehicleModelAccessorFake(List<VehicleModel> fakeVehicleModelData)
        {
            _fakeVehicleModelData = fakeVehicleModelData;
            _fakeVehicles = new List<Vehicle>();
            _fakeVehicles.Add(new Vehicle { VIN = "aaaabbbbcccceeee0", VehicleModelID = 100003 });
            _fakeVehicles.Add(new Vehicle { VIN = "aaaabbbbcccceeee1", VehicleModelID = 100004 });
            _fakeVehicles.Add(new Vehicle { VIN = "aaaabbbbcccceeee2", VehicleModelID = 100005 });
            _fakeVehicles.Add(new Vehicle { VIN = "aaaabbbbcccceeee3", VehicleModelID = 100007 });
        
        }

        /// <summary>
        ///     Retrieves all active vehicle models
        /// </summary>
        /// <returns>
        ///    <see cref="IEnumerable{VehicleModel}">IEnumerable&lt;VehicleModel&gt;</see>: All active vehicle models
        /// </returns>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-03-02
        /// </remarks>
        public IEnumerable<VehicleModel> GetVehicleModels()
        {
            return _fakeVehicleModelData.Where(vehicleModel => vehicleModel.IsActive);
        }

        /// <summary>
        ///     Inserts a new vehicle model
        /// </summary>
        /// <param name="vehicleModel">
        ///    The VehicleModel being added
        /// </param>
        /// <returns>
        ///    <see cref="int">bool</see>: The number of rows affected in the VehicleModel table
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="VehicleModel">VehicleModel</see> vehicleModel: The VehicleModel being inserted
        public int InsertVehicleModel(VehicleModel vehicleModel)
        {
            _fakeVehicleModelData.Add(vehicleModel);

            return 1;
        }

        /// <summary>
        ///     Retreives VehicleModel by vin
        /// </summary>
        /// <param name="vin">
        ///    The Vehicle vin
        /// </param>
        /// <returns>
        ///    <see cref="VehicleModel"></see>: The VehicleModel retrieved from the datafakes
        /// </returns>
        /// <remarks>
        ///    Author: James Williams
        /// 
        public VehicleModel getVehicleModelByVIN(string vin)
        {
            Vehicle selectedVehicle = null;
            VehicleModel vm = null;
            foreach (var vehicle in _fakeVehicles)
            {
                if (vehicle.VIN == vin)
                {
                    selectedVehicle = vehicle;
                }
            }
            if (selectedVehicle == null)
            {
                throw new SystemException();
            }
            foreach (var model in _fakeVehicleModelData)
            {
                if (model.VehicleModelID == selectedVehicle.VehicleModelID)
                {
                    vm = model;
                }
            }

            return vm;
        }
    }
}
