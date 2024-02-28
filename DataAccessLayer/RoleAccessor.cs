using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <inheritdoc/> 
    public class RoleAccessor : IRoleAccessor
    {
        public int CreateRole(Role role)
        {
            int entryID = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_insert_role";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@RoleID", System.Data.SqlDbType.NVarChar, 25);
            cmd.Parameters.Add("@RoleDescription", System.Data.SqlDbType.NVarChar, 255);

            cmd.Parameters["@RoleID"].Value = role.RoleID;
            cmd.Parameters["@RoleDescription"].Value = role.RoleDescription;
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

        public IEnumerable<Role> GetAllRoles()
        {
            List<Role> roles = new List<Role>();
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_get_all_active_role";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role() { RoleID = reader.GetString(0), IsActive = true });
                    }
                }
                else
                {
                    throw new ApplicationException("Roles not found");
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

            return roles;

        }


    }
}
