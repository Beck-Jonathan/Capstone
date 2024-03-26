/// <summary>
/// Michael Springer
/// Created: 2024/02/04
/// 
/// Data Access class for Dependent objects
/// </summary>
///
/// <remarks>
/// Updater Michael Springer
/// Updated: 2-24-03-04
/// Updated all methods to handle nullable values for middlename and gender
/// </remarks>
using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;

namespace DataAccessLayer
{
    public class DependentAccessor : IDependentAccessor
    {
        public int InsertDependent(Dependent dependent)
        {
            int entryID = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_insert_dependent";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@Given_Name", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Family_Name", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Middle_Name", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@DOB", System.Data.SqlDbType.Date);
            cmd.Parameters.Add("@Gender", System.Data.SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@Emergency_Contact", System.Data.SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Contact_Relationship", System.Data.SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Emergency_Phone", System.Data.SqlDbType.NVarChar, 12);

            cmd.Parameters["@Given_Name"].Value = dependent.GivenName;
            cmd.Parameters["@Family_Name"].Value = dependent.FamilyName;
            cmd.Parameters["@Middle_Name"].Value = dependent.MiddleName ?? Convert.DBNull;
            cmd.Parameters["@DOB"].Value = dependent.DOB;
            cmd.Parameters["@Gender"].Value = dependent.Gender;
            cmd.Parameters["@Emergency_Contact"].Value = dependent.EmergencyContact;
            cmd.Parameters["@Contact_Relationship"].Value = dependent.ContactRelationship;
            cmd.Parameters["@Emergency_Phone"].Value = dependent.EmergencyPhone;

            try
            {
                conn.Open();
                entryID = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return entryID;

        }

        public IEnumerable<DependentVM> ListAllDependents()
        {
            List<DependentVM> dependentList = new List<DependentVM>();

            var conn = DBConnectionProvider.GetConnection();

            var cmdText = "sp_select_all_dependents";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {

                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dependentList.Add(new DependentVM()
                        {
                            DependentID = reader.GetInt32(0),
                            GivenName = reader.GetString(1),
                            FamilyName = reader.GetString(2),
                            MiddleName = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Gender = reader.IsDBNull(4) ? null : reader.GetString(4),
                            EmergencyContact = reader.GetString(6),
                            ContactRelationship = reader.GetString(7),
                            EmergencyPhone = reader.GetString(8),
                            IsActive = reader.GetBoolean(9),

                        });
                    }

                }
                else
                {
                    throw new ApplicationException("No Dependents Found");
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

            return dependentList;
        }

        public DependentVM SelectDependentByID(int id)
        {
            DependentVM dependentVM = null;
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_dependent_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Dependent_ID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        dependentVM = new DependentVM()
                        {
                            DependentID = reader.GetInt32(0),
                            GivenName = reader.GetString(1),
                            FamilyName = reader.GetString(2),
                            MiddleName = reader.IsDBNull(3) ? null : reader.GetString(3),
                            DOB = reader.GetDateTime(4),
                            Gender = reader.IsDBNull(5) ? null : reader.GetString(5),
                            EmergencyContact = reader.GetString(6),
                            ContactRelationship = reader.GetString(7),
                            EmergencyPhone = reader.GetString(8),
                            IsActive = true
                        };
                    }
                    // TODO VM population
                }
                else
                {
                    throw new ApplicationException("No dependent found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { conn.Close(); }
            return dependentVM;
        }

        /// <summary>
        /// CONTRIBUTOR: Michael Springer
        /// CREATED: 2024-02-19
        /// 
        ///     Retrieving dependent by client ID
        ///     
        /// <returns> Returns: <see cref = "IEnumerable{DependentVM}" > IEnumerable Of Dependent VM</see></returns>
        ///     
        /// </summary>
        public IEnumerable<DependentVM> SelectDependentsByClientId(int id)
        {
            List<DependentVM> dependents = new List<DependentVM>();
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_dependents_by_client_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Client_ID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dependents.Add(new DependentVM()
                        {
                            DependentID = reader.GetInt32(0),
                            GivenName = reader.GetString(1),
                            FamilyName = reader.GetString(2),
                            MiddleName = reader.IsDBNull(3) ? null : reader.GetString(3),
                            DOB = reader.GetDateTime(4),
                            Gender = reader.IsDBNull(5) ? null : reader.GetString(5),
                            EmergencyContact = reader.GetString(6),
                            ContactRelationship = reader.GetString(7),
                            EmergencyPhone = reader.GetString(8),
                            IsActive = reader.GetBoolean(9),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { conn.Close(); }
            return dependents;
        }

        public int UpdateDependent(DependentVM oldDependentInfo, DependentVM newDependentInfo, int clientId)
        {
            int rows = 0;
            var conn = DBConnectionProvider.GetConnection();
            var commandText = "sp_update_dependent";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Dependent_ID", SqlDbType.Int);
            cmd.Parameters.Add("@Client_ID", SqlDbType.Int);

            cmd.Parameters.Add("@Old_Given_Name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Old_Family_Name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Old_Middle_Name", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Old_DOB", SqlDbType.Date);
            cmd.Parameters.Add("@Old_Gender", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@Old_Emergency_Contact", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Old_Contact_Relationship", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Old_Emergency_Phone", SqlDbType.NVarChar, 12);
            cmd.Parameters.Add("@Old_Client_Relationship", SqlDbType.NVarChar, 100);

            cmd.Parameters.Add("@New_Given_Name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@New_Family_Name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@New_Middle_Name", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@New_DOB", SqlDbType.Date);
            cmd.Parameters.Add("@New_Gender", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@New_Emergency_Contact", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@New_Contact_Relationship", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@New_Emergency_Phone", SqlDbType.NVarChar, 12);
            cmd.Parameters.Add("@New_Client_Relationship", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Dependent_ID"].Value = oldDependentInfo.DependentID;
            cmd.Parameters["@Client_ID"].Value = clientId;

            cmd.Parameters["@Old_Given_Name"].Value = oldDependentInfo.GivenName;
            cmd.Parameters["@Old_Family_Name"].Value = oldDependentInfo.FamilyName;
            cmd.Parameters["@Old_Middle_Name"].Value = oldDependentInfo.MiddleName ?? (object)DBNull.Value;
            cmd.Parameters["@Old_DOB"].Value = oldDependentInfo.DOB;
            cmd.Parameters["@Old_Gender"].Value = oldDependentInfo.Gender ?? (object)DBNull.Value;
            cmd.Parameters["@Old_Emergency_Contact"].Value = oldDependentInfo.EmergencyContact;
            cmd.Parameters["@Old_Contact_Relationship"].Value = oldDependentInfo.ContactRelationship;
            cmd.Parameters["@Old_Emergency_Phone"].Value = oldDependentInfo.EmergencyPhone;
            // Select the relationship between featured dependent and guardian
            var matchingRole = oldDependentInfo.ClientDependentRoles.FirstOrDefault(role =>
                role.ClientID == clientId && role.DependentID == oldDependentInfo.DependentID);
            if (matchingRole != null)
            {
                cmd.Parameters["@Old_Client_Relationship"].Value = matchingRole.Relationship;
            }
            else
            {
                throw new Exception("Invalid or Missing Guardian-Dependent Relationship");
            }
            cmd.Parameters["@New_Given_Name"].Value = newDependentInfo.GivenName;
            cmd.Parameters["@New_Family_Name"].Value = newDependentInfo.FamilyName;
            cmd.Parameters["@New_Middle_Name"].Value = newDependentInfo.MiddleName ?? (object)DBNull.Value;
            cmd.Parameters["@New_DOB"].Value = newDependentInfo.DOB;
            cmd.Parameters["@New_Gender"].Value = newDependentInfo.Gender ?? (object)DBNull.Value;
            cmd.Parameters["@New_Emergency_Contact"].Value = newDependentInfo.EmergencyContact;
            cmd.Parameters["@New_Contact_Relationship"].Value = newDependentInfo.ContactRelationship;
            cmd.Parameters["@New_Emergency_Phone"].Value = newDependentInfo.EmergencyPhone;
            // Select the relationship between featured dependent and guardian
            var matchingRole2 = newDependentInfo.ClientDependentRoles.FirstOrDefault(role =>
                role.ClientID == clientId && role.DependentID == oldDependentInfo.DependentID);
            if (matchingRole2 != null)
            {
                cmd.Parameters["@New_Client_Relationship"].Value = matchingRole2.Relationship;
            }
            else
            {
                throw new Exception("Invalid or Missing Guardian-Dependent Relationship");
            }

            try
            {
                conn.Open();
                rows = Convert.ToInt32(cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { conn.Close(); }
            return rows;
        }
    }
}
