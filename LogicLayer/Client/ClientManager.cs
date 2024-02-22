using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    /// <inheritdoc/>

    public class ClientManager : IClientManager
    {
        IClientAccessor _clientAccessor = null;

        public ClientManager()
        {
            _clientAccessor = new ClientAccessor();
        }

        public ClientManager(IClientAccessor clientAccessor)
        {
            _clientAccessor = clientAccessor;
        }

        public void ActivateClient(int id)
        {
            throw new NotImplementedException();
        }

        public bool AddClient(Client_VM client)
        {
            bool result = false;

            try
            {
                result = (1 == _clientAccessor.InsertClient(client));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error adding client.", ex);
            }

            return result;
        }

        public void DeactivateClient(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     A method that runs the UpdateClient method from
        ///     the ClientAccessor class and returns the number of rows affected.
        /// </summary>
        /// <param name="newClient">
        ///    The new Client_VM object to be updated in the database.
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: The number of rows affected
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown when the UpdateClient method fails.
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Roberts
        /// <br />
        ///    CREATED: 2024-02-11
        /// <br /><br />
        ///     Initial creation
        /// </remarks>
        public int EditClient(Client_VM newClient)
        {

            int rows = 0;

            try
            {
                rows = _clientAccessor.UpdateClient(newClient);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update Failed", ex);
            }

            return rows;
        }

        public IEnumerable<Client> GetAllClients()
        {
            IEnumerable<Client> clients = null;

            try
            {
                clients = _clientAccessor.SelectAllClients();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Unable to access clients.", ex);
            }
            return clients;
        }
        /// <summary>
        ///     A method that runs the SelectClientById method from
        ///     the ClientAccessor class and returns a Client_VM object.
        /// </summary>
        /// <param name="id">
        ///    The ID of the client to be selected.
        /// </param>
        /// <returns>
        ///    <see cref="Client_VM">Client_VM</see>: A Client_VM object
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown when a Client is not found.
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Roberts
        /// <br />
        ///    CREATED: 2024-02-11
        /// <br /><br />
        ///     Initial creation
        /// </remarks>
        public Client_VM GetClientById(int id)
        {
            Client_VM client = null;
            try
            {
                client = _clientAccessor.SelectClientById(id);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Client not found", ex);
            }
            return client;
        }

        public IEnumerable<Client> GetClients()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> GetInactiveClients()
        {
            throw new NotImplementedException();
        }
    }
}
