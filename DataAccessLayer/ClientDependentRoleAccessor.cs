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
    public class ClientDependentRoleAccessor : IClientDependentRoleAccessor
    {
        public int InsertClientDependentRole(ClientDependentRole clientDepndentRole)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClientDependentRole_VM> SelectAllClientDependentRoles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// CONTRIBUTOR: Michael Springer
        /// CREATED: 2024-02-19
        /// 
        ///     Retrieving dependent by client ID
        ///     
        /// <returns> Returns: <see cref = "IEnumerable{ClientDependentRole_VM}" > IEnumerable Of ClientDependentRole_VM</see></returns>
        ///     
        /// </summary>
        public IEnumerable<ClientDependentRole_VM> SelectClientDependentRolesByClientId(int id)
        {
            List<ClientDependentRole_VM> roles = new List<ClientDependentRole_VM>();
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_client_dependent_role_by_client_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Client_ID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(new ClientDependentRole_VM()
                        {
                            ClientID = reader.GetInt32(0),
                            DependentID = reader.GetInt32(1),
                            Relationship = reader.GetString(2),
                            IsActive = reader.GetBoolean(3),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { conn.Close(); }
            return roles;

        }

        public IEnumerable<ClientDependentRole_VM> SelectInactiveClientDependentRoles()
        {
            throw new NotImplementedException();
        }

        public int UpdateClientDependentRole(ClientDependentRole clientDepndentRole)
        {
            throw new NotImplementedException();
        }

        public int UpdateClientDependentroleAsActive(ClientDependentRole clientDepndentRole)
        {
            throw new NotImplementedException();
        }

        public int UpdateClientDependentRoleAsInactive(ClientDependentRole clientDepndentRole)
        {
            throw new NotImplementedException();
        }
    }
}
