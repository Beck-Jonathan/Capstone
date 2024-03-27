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
    /// AUTHOR: Ben Collins, Everett DeVaux
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
    /// UPDATED: 2024-03-02
    /// <br />
    ///     Initial creation
    ///     <br />
    ///     Added public List<Parts_Request> GetPartsRequestDetails()
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


        /// <summary>
        ///     Retrieves Parts Request Details from the database.
        /// </summary>
        /// <returns>
        ///    <see cref="Parts_Request"></see> objects otherwise, <see cref="Exception">execption</see>.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: No records returned
        /// <br /><br />
        ///    CONTRIBUTOR: Everett DeVaux
        /// <br />
        ///    CREATED: 2024-03-02
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        public Parts_Request GetActivePartsRequestDetails(int partsRequestID)
        {
            Parts_Request output = new Parts_Request();
            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_select_all_parts_request_details";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //Add parameters
            cmd.Parameters.AddWithValue("@Parts_Request_Id", partsRequestID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Parts_Request partsRequestDetails = new Parts_Request()
                        {
                            Parts_Request_ID = reader.GetInt32(0),
                            Part_Name = (reader.GetString(1)),
                            Quantity_Requested = reader.GetInt32(2),
                            Vehicle_Year = reader.GetInt32(3).ToString(),
                            Vehicle_Make = reader.GetString(4),
                            Vehicle_Model = reader.GetString(5),
                            Parts_Request_Notes = reader.GetString(6),
                            Date_Requested = reader.GetDateTime(7),
                            Employee_ID = reader.GetInt32(8),
                        };
                        output = partsRequestDetails;
                    }
                    if (output == null)
                    {
                        throw new ArgumentException("No Records were found");
                    }
                }
                else
                {
                    throw new ApplicationException("Parts Request Details not found.");
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
            return output;
        }

        /// <summary>
        ///     Deactivates a Request by Id
        /// </summary>
        /// <returns>
        ///    <see cref="bool">Boolean</see>.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: No records returned
        /// <br /><br />
        ///    CONTRIBUTOR: Parker Svoboda
        /// <br />
        ///    CREATED: 2024-03-26
        /// </remarks>
        public int DeactivateRequestById(int id)
        {
            int rowsAffected = 0;
            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_deactivate_request_by_id";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //Add parameters
            cmd.Parameters.AddWithValue("@Parts_Request_Id", id);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected;
        }
    }
}
