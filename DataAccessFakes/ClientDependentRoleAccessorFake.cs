using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class ClientDependentRoleAccessorFake : IClientDependentRoleAccessor
    {
        IEnumerable<ClientDependentRole_VM> _fakeClientDependentRoleData;
        public ClientDependentRoleAccessorFake(IEnumerable<ClientDependentRole_VM> fakeData)
        {
            _fakeClientDependentRoleData = fakeData;
        }

        public int InsertClientDependentRole(ClientDependentRole clientDepndentRole)
        {
            _fakeClientDependentRoleData.ToList().Append(clientDepndentRole);
            return 1;
        }

        public IEnumerable<ClientDependentRole_VM> SelectAllClientDependentRoles()
        {
            return _fakeClientDependentRoleData.Where(cdr => cdr.IsActive);
        }

        public IEnumerable<ClientDependentRole_VM> SelectClientDependentRolesByClientId(int id)
        {
            return _fakeClientDependentRoleData.Where(cdr => cdr.ClientID == id);
        }

        public IEnumerable<ClientDependentRole_VM> SelectInactiveClientDependentRoles()
        {
            return _fakeClientDependentRoleData.Where(cdr => !cdr.IsActive);
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
