using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// AUTHOR: Ben Collins
    /// <br />
    /// CREATED: 2024-03-02
    /// <br />
    /// 
    ///     Data access class for Parts Requests.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: [Updater's Name]
    /// <br />
    /// UPDATED: yyyy-MM-dd
    /// <br />
    ///     Initial creation
    /// </remarks>
    public class PartsRequestAccessor : IPartsRequestAccessor
    {
        /// <summary>
        ///     Retrieves all Parts Request records from the database.
        /// </summary>
        /// <returns>
        ///    List of <see cref="List{Parts_Request}">Parts_Request</see> objects otherwise, <see cref="Exception">execption</see>.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: No records returned
        /// <br /><br />
        ///    CONTRIBUTOR: Ben Collins
        /// <br />
        ///    CREATED: 2024-03-02
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creat
        public List<Parts_Request> GetAllActivePartsRequests()
        {
            List<Parts_Request> partsRequests = new List<Parts_Request>();

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_all_active_part_requests";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Parts_Request partsRequest = new Parts_Request()
                    { 
                        Parts_Request_ID = reader.GetInt32(0),
                        Date_Requested = reader.GetDateTime(1),
                        Quantity_Requested = reader.GetInt32(2),
                        Part_Name = reader.GetString(3)
                    };
                    partsRequests.Add(partsRequest);
                }

                if (partsRequests.Count == 0)
                {
                    throw new ArgumentException("No parts request records found");
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
            return partsRequests;
        }
    }
}
