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

        public int AddClient(Client client)
        {
            throw new NotImplementedException();
        }

        public void DeactivateClient(int id)
        {
            throw new NotImplementedException();
        }

        public void EditClient(int id)
        {
            throw new NotImplementedException();
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

        public Client GetClientById(int id)
        {
            throw new NotImplementedException();
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
