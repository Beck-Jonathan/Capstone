﻿using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessFakes
{
    /// <summary>
    /// AUTHOR: Jared Hutton, Jacob Wendt
    /// CREATED: 2024-02-05
    ///     Fake data to be used with client manager tests.
    /// </summary>
    /// <remarks>
    /// UPDATER: Isabella Rosenbohm <br />
    /// UPDATED: 2024-02-21 <br />
    ///    Changed how the InsertClient method works so that duplicate data could properly be tested.
    ///    Changed how the SelectClientByEmail method works so that it could properly catch exceptions for tests
    ///    Added missing comment documentation
    /// <br/><br/>
    /// UPDATER: Isabella Rosenbohm <br/>
    /// UPDATED: 2024-02-27
    ///     Changed UpdateClient intake param from newClient to client
    /// <br />
    /// UPDATER: Michael Springer
    /// UPDATED: 2024-04-025
    ///     Modified Exception messages for consistency
    /// </remarks>

    public class ClientAccessorFake : IClientAccessor
    {
        IEnumerable<Client_VM> _fakeClientData;

        public ClientAccessorFake(IEnumerable<Client_VM> fakeClientData)
        {
            _fakeClientData = fakeClientData;
        }

        public int InsertClient(Client_VM client)
        {
            _fakeClientData = _fakeClientData.ToList();
            foreach (var v in _fakeClientData)
            {
                if (v.Email.Equals(client.Email))
                {
                    throw new ArgumentException();
                }
            }
            _fakeClientData.Append(client);
            return 1;
        }

        public IEnumerable<Client_VM> SelectAllClients()
        {
            return _fakeClientData;
        }

        public Client_VM SelectClientById(int id)
        {
            return _fakeClientData.Single(client => client.ClientID == id);
        }

        public Client_VM SelectClientByEmail(string email)
        {
            Client_VM client;

            try
            {
                client = _fakeClientData.Single(c => c.Email == email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Client not found");
            }

            return client;
        }

        public IEnumerable<Client_VM> SelectClients()
        {
            return _fakeClientData.Where(client => client.IsActive);
        }

        public IEnumerable<Client_VM> SelectInactiveClients()
        {
            return _fakeClientData.Where(client => !client.IsActive);
        }

        public int UpdateClient(Client_VM client)
        {
            var list = _fakeClientData.ToList();

            int removed = list.RemoveAll(c => c.ClientID == client.ClientID);

            if (removed == 1)
            {
                list.Add(client);

                _fakeClientData = list;
            }

            return removed;
        }

        public int UpdateClientByIdAsActive(int id)
        {
            _fakeClientData.Single(client => client.ClientID == id).IsActive = true;

            return 1;
        }

        public int UpdateClientByIdAsInactive(int id)
        {
            _fakeClientData.Single(client => client.ClientID == id).IsActive = false;

            return 1;
        }

        public int DeactivateClient(int id)
        {
            int result = 0;
            foreach (var client in _fakeClientData)
            {
                if (client.ClientID == id)
                {
                    result = client.IsActive ? 1 : 0;
                    client.IsActive = false;
                }
            }
            return result;
        }
    }
}
