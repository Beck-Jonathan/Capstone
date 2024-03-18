﻿using DataAccessInterfaces;
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
    }
}