using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessFakes
{
    public class ClientAccessorFake : IClientAccessor
    {
        IEnumerable<Client_VM> _fakeClientData;

        public ClientAccessorFake(IEnumerable<Client_VM> fakeClientData)
        {
            _fakeClientData = fakeClientData;
        }

        public int InsertClient(Client_VM client)
        {
            _fakeClientData = _fakeClientData.ToList().Append(client);

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

        public IEnumerable<Client_VM> SelectClients()
        {
            return _fakeClientData.Where(client => client.IsActive);
        }

        public IEnumerable<Client_VM> SelectInactiveClients()
        {
            return _fakeClientData.Where(client => !client.IsActive);
        }

        public int UpdateClient(Client_VM newClient)
        {
            var list = _fakeClientData.ToList();

            int removed = list.RemoveAll(c => c.ClientID == newClient.ClientID);

            if (removed == 1)
            {
                list.Add(newClient);

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
    }
}
