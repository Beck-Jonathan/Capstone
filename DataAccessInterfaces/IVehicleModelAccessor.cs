﻿using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    ///     Provides CRUD operations on the data source for vehicle model objects
    /// </summary>
    public interface IVehicleModelAccessor
    {
        /// <summary>
        ///     Retrieves all active vehicle models
        /// </summary>
        /// <returns>
        ///    <see cref="IEnumerable{VehicleModel}">IEnumerable&lt;VehicleModel&gt;</see>: All active vehicle models
        /// </returns>
        IEnumerable<VehicleModel> GetVehicleModels();

        /// <summary>
        ///     Inserts a new vehicle model
        /// </summary>
        /// <param name="vehicleModel">
        ///    The VehicleModel being added
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: The ID of the new VehicleModel row
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="VehicleModel">VehicleModel</see> vehicleModel: The VehicleModel being inserted
        int InsertVehicleModel(VehicleModel vehicleModel);

        /// <summary>
        ///    Retrieves Vehicle Model by VIN
        /// </summary>
        /// <returns>
        ///    VehicleModel vehicle model object 
        /// </returns>
        /// <remarks>
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-04-24
        /// </remarks>
        VehicleModel getVehicleModelByVIN(string vin);
    }
}
