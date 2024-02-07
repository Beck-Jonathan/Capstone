using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace DataAccessLayer
{
    /// <inheritdoc/>
    public class ClientAccessor : IClientAccessor
    {
        public int InsertClient(Client_VM client)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client_VM> SelectAllClients()
        {
            List<Client_VM> clients = new List<Client_VM>();
            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_all_client";
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
                        clients.Add(new Client_VM() 
                        {
                            ClientID = reader.GetInt32(0),
                            GivenName = reader.GetString(1),
                            FamilyName = reader.GetString(2),
                            // Middle_Name should be GetString(3)
                            DOB = reader.GetDateTime(4),
                            Email = reader.GetString(5),
                            PostalCode = reader.GetString(6),
                            City = reader.GetString(7),
                            Region = reader.GetString(8),
                            Address = reader.GetString(9),
                            TextNumber = reader.GetString(10),
                            VoiceNumber = reader.GetString(11),
                            IsActive = reader.GetBoolean(12)
                        });
                    }
                }

            } catch (Exception ex)
            {
                throw ex;
            }
            finally { conn.Close(); }

            return clients;
        }

        public Client_VM SelectClientById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client_VM> SelectClients()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client_VM> SelectInactiveClients()
        {
            throw new NotImplementedException();
        }

        public int UpdateClient(Client client)
        {
            throw new NotImplementedException();
        }

        public int UpdateClientByIdAsActive(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateClientByIdAsInactive(int id)
        {
            throw new NotImplementedException();
        }
    }
}
