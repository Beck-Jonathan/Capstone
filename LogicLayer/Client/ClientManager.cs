﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using Microsoft.Win32;

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
        /// <summary>
        /// AUTHOR: Michael Springer
        /// DATE: 2024-04-27
        /// Deactivates the Client
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public int DeactivateClient(int id)
        {
            int result = 0;
            try
            {
                result = _clientAccessor.DeactivateClient(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error deactivating account", ex);
            }
            return result;
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
        /// CONTRIBUTOR: Jared Roberts <br />
        /// CREATED: 2024-02-11
        /// <br/><br/>
        /// UPDATER: Isabella Rosenbohm <br/>
        /// UPDATED: 2024-02-27 <br/>
        ///     Changed intake param of EditClient from newClient to client 
        /// </remarks>
        public int EditClient(Client_VM client)
        {

            int rows = 0;

            try
            {
                rows = _clientAccessor.UpdateClient(client);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update Failed", ex);
            }

            return rows;
        }

        public IEnumerable<DataObjects.Client> GetAllClients()
        {
            IEnumerable<DataObjects.Client> clients = null;

            try
            {
                clients = _clientAccessor.SelectAllClients();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Unable to access clients", ex);
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
        public Client_VM GetClientByEmail(string email)
        {
            Client_VM client = null;
            try
            {
                client = _clientAccessor.SelectClientByEmail(email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Client not found", ex);
            }
            return client;
        }

        public IEnumerable<DataObjects.Client> GetClients()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DataObjects.Client> GetInactiveClients()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// AUTHOR: Michael Springer
        /// CREATED: 2024-04-24
        /// Checks if a client exists
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public bool FindClient(string email)
        {
            try
            {
                return _clientAccessor.SelectClientByEmail(email) != null;
            }
            catch (ApplicationException ax)
            {
                if (ax.Message == "Client not found")
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database Error", ex);
            }
        }
    }
}
