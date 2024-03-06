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
using System.IO.Ports;

namespace DataAccessLayer
{
    /// <inheritdoc/>
    public class ClientAccessor : IClientAccessor
    {
        public int InsertClient(Client_VM client)
        {
            int rows = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_insert_client";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_GivenName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@p_FamilyName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@p_MiddleName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@p_DOB", SqlDbType.Date);
            cmd.Parameters.Add("@p_Email", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@p_PostalCode", SqlDbType.NVarChar, 9);
            cmd.Parameters.Add("@p_City", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@p_Region", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@p_Address", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@p_TextNumber", SqlDbType.NVarChar, 12);
            cmd.Parameters.Add("@p_VoiceNumber", SqlDbType.NVarChar, 12);
            cmd.Parameters.Add("@p_Active", SqlDbType.Bit);

            cmd.Parameters["@p_GivenName"].Value = client.GivenName;
            cmd.Parameters["@p_FamilyName"].Value = client.FamilyName;
            cmd.Parameters["@p_MiddleName"].Value = client.MiddleName;
            cmd.Parameters["@p_DOB"].Value = client.DOB;
            cmd.Parameters["@p_Email"].Value = client.Email;
            cmd.Parameters["@p_PostalCode"].Value = client.PostalCode;
            cmd.Parameters["@p_City"].Value = client.City;
            cmd.Parameters["@p_Region"].Value = client.Region;
            cmd.Parameters["@p_Address"].Value = client.Address;
            cmd.Parameters["@p_TextNumber"].Value = client.TextNumber;
            cmd.Parameters["@p_VoiceNumber"].Value = client.VoiceNumber;
            cmd.Parameters["@p_Active"].Value = client.IsActive;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
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
                            MiddleName = reader.GetString(3),
                            DOB = reader.GetDateTime(4),
                            Email = reader.GetString(5),
                            City = reader.GetString(6),
                            Region = reader.GetString(7),
                            Address = reader.GetString(8),
                            TextNumber = reader.GetString(9),
                            VoiceNumber = reader.GetString(10),
                            PostalCode = reader.GetString(11),
                            IsActive = reader.GetBoolean(12)
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { conn.Close(); }

            return clients;
        }

        /// <summary>
        ///     A method that searches for a Client by their ID in the database
        ///     and returns the Client_VM object for that Client.
        /// </summary>
        /// <param name="id">
        ///    The ID of the Client to be searched for
        /// </param>
        /// <returns>
        ///    <see cref="Client_VM">Client_VM</see>: The Client_VM object for the Client with the given ID.
        /// </returns>
        /// <remarks>
        ///    Exceptions: <br />
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown when a Client isn't found. <br /><br />
        ///    CONTRIBUTOR: Jared Roberts <br />
        ///    CREATED: 2024-02-11 <br />
        ///    UPDATER: Isabella Rosenbohm <br />
        ///    UPDATED: 2024-03-05 <br /><br />
        ///         Changed Parameters.AddWithValues line from @ClientID to @Client_ID as it wasn't correct
        /// </remarks>
        public Client_VM SelectClientById(int id)
        {
            Client_VM clientVM = new Client_VM();

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = @"sp_select_client_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Client_ID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        clientVM.ClientID = reader.GetInt32(0);
                        clientVM.GivenName = reader.GetString(1);
                        clientVM.FamilyName = reader.GetString(2);
                        clientVM.MiddleName = reader.IsDBNull(3) ? null : reader.GetString(3);
                        clientVM.DOB = reader.GetDateTime(4);
                        clientVM.Email = reader.GetString(5);
                        clientVM.PostalCode = reader.GetString(6);
                        clientVM.City = reader.GetString(7);
                        clientVM.Region = reader.GetString(8);
                        clientVM.Address = reader.GetString(9);
                        clientVM.TextNumber = reader.GetString(10);
                        clientVM.VoiceNumber = reader.GetString(11);

                    }
                }
                else
                {
                    throw new ApplicationException("Client not found.");
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

            return clientVM;
        }

        /// <summary>
        ///    A method that returns a Client_VM record containing a matching email field
        /// </summary>
        /// <param name="email">
        ///    An email string
        /// </param>
        /// <returns>
        ///    <see cref="Client_VM">Client_VM</see>
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown when incorrect fields are given for the user.
        /// <br /><br />
        ///    CONTRIBUTOR: Jacob Wendt
        /// <br />
        ///    CREATED: 2024-02-19
        /// <br /><br />
        ///     Initial creation
        /// </remarks>
        public Client_VM SelectClientByEmail(string email)
        {
            Client_VM clientVM = new Client_VM();

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = @"sp_select_client_by_email";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Email", email);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        clientVM.ClientID = reader.GetInt32(0);
                        clientVM.GivenName = reader.GetString(1);
                        clientVM.FamilyName = reader.GetString(2);
                        clientVM.MiddleName = reader.IsDBNull(3) ? null : reader.GetString(3);
                        clientVM.DOB = reader.GetDateTime(4);
                        clientVM.Email = reader.GetString(5);
                        clientVM.PostalCode = reader.GetString(6);
                        clientVM.City = reader.GetString(7);
                        clientVM.Region = reader.GetString(8);
                        clientVM.Address = reader.GetString(9);
                        clientVM.TextNumber = reader.GetString(10);
                        clientVM.VoiceNumber = reader.GetString(11);

                    }
                }
                else
                {
                    throw new ApplicationException("Client not found.");
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

            return clientVM;
        }

        public IEnumerable<Client_VM> SelectClients()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client_VM> SelectInactiveClients()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     A method that updates the field of a Client and
        ///     returns the number of rows affected.
        /// </summary>
        /// <param name="newClient">
        ///    a Client_VM object
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: The number of rows affected
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown when incorrect fields are given for the user.
        /// <br /><br />
        /// CONTRIBUTOR: Jared Roberts
        /// CREATED: 2024-02-11
        /// <br/><br/>
        /// UPDATER: Jared Roberts <br/>
        /// UPDATED: 2024-02-11 <br/>
        ///     Changed the argument type for the UpdateClient method from int to Client_VM
        /// UPDATER: Isabella Rosenbohm <br/>
        /// UPDATED: 2024-02-27 <br/>
        ///     Rewrote UpdateClient method so it no longer needs Old data params as Client_ID should be sufficient
        /// </remarks>
        public int UpdateClient(Client_VM client)
        {
            int rows = 0;

            var conn = DBConnectionProvider.GetConnection();

            var commandText = "sp_update_client";

            var cmd = new SqlCommand(commandText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(@"Client_ID", client.ClientID);
            cmd.Parameters.AddWithValue(@"GivenName", client.GivenName);
            cmd.Parameters.AddWithValue(@"FamilyName", client.FamilyName);
            cmd.Parameters.AddWithValue(@"MiddleName", client.MiddleName);
            cmd.Parameters.AddWithValue(@"DOB", client.DOB);
            cmd.Parameters.AddWithValue(@"Email", client.Email);
            cmd.Parameters.AddWithValue(@"PostalCode", client.PostalCode);
            cmd.Parameters.AddWithValue(@"City", client.City);
            cmd.Parameters.AddWithValue(@"Region", client.Region);
            cmd.Parameters.AddWithValue(@"Address", client.Address);
            cmd.Parameters.AddWithValue(@"TextNumber", client.TextNumber);
            cmd.Parameters.AddWithValue(@"VoiceNumber", client.VoiceNumber);
            cmd.Parameters.AddWithValue(@"Active", client.IsActive);

            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    throw new ArgumentException("User was not updated");
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

            return rows;
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
