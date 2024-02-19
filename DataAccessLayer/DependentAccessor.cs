/// <summary>
/// Michael Springer
/// Created: 2024/02/04
/// 
/// Data Access class for Dependent objects
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            cmd.Parameters.Add("@Emergency_Phone", System.Data.SqlDbType.NVarChar, 11);

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
    }
}
