using DataAccessInterfaces;
using DataAccessLayer.Helpers;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-03-02
    /// <br />
    ///     Provides CRUD operations on the data source for vehicle model objects
    /// </summary>
    public class VehicleModelAccessor : IVehicleModelAccessor
    {
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
            var vehicleModels = new List<VehicleModel>();

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_get_vehicle_models";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var vehicleModel = new VehicleModel
                    {
                        VehicleModelID = reader.GetInt32(0),
                        VehicleTypeID = reader.GetStringNullable(1),
                        Name = reader.GetString(2),
                        Make = reader.GetString(3),
                        Year = reader.GetInt32(4),
                        MaxPassengers = reader.GetInt32(5),
                        IsActive = reader.GetBoolean(6)
                    };

                    vehicleModels.Add(vehicleModel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return vehicleModels;
        }

    }
}
