using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataAccessLayer;

namespace LogicLayer.Client
{
    public class ClientDependentRoleManager : IClientDependentRoleManager
    {
        IClientDependentRoleAccessor _clientDependentRoleAccessor = null;

        public ClientDependentRoleManager()
        {
            _clientDependentRoleAccessor = new ClientDependentRoleAccessor();
        }

        public ClientDependentRoleManager(IClientDependentRoleAccessor clientDependentRoleAccessor)
        {
            _clientDependentRoleAccessor = clientDependentRoleAccessor;
        }

        public int AddClientDependentRole(ClientDependentRole clientDependentRole)
        {
            throw new NotImplementedException();
        }

        public void ActivateClientDependentRole(ClientDependentRole clientDependentRole)
        {
            throw new NotImplementedException();
        }

        public void DeactivateClientDependentRole(ClientDependentRole clientDependentRoles)
        {
            throw new NotImplementedException();
        }

        public int EditClientDependentRole(ClientDependentRole clientDependentRole)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClientDependentRole_VM> GetAllClientDependentRoles()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClientDependentRole_VM> GetClientDependentRolesByClient(int id)
        {
            IEnumerable<ClientDependentRole_VM> clientDepentRoles = null;
            try
            {
                clientDepentRoles = _clientDependentRoleAccessor.SelectClientDependentRolesByClientId(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to access client dependent roles.", ex);
            }
            return clientDepentRoles;


        }

        public IEnumerable<ClientDependentRole_VM> GetInactiveClientDependentRoles()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024-03-03
        /// 
        /// Returns a single, unique ClientDependentRole based on clientID and dependentID
        /// </summary>
        ///   
        public ClientDependentRole_VM GetClientDependtRoleByClientIdAndDependentId()
        {
            throw new NotImplementedException();
        }
    }
}
