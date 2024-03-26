using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Client
{
    /// <summary>
    /// AUTHOR: Michael Springer
    /// <br />
    /// CREATED: 2024-02-25
    /// <br />
    /// 
    ///     Provides CRUD operations on the data source for client dependent role data.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER:
    /// <br />
    ///    UPDATED:
    /// <br />
    /// </remarks>
    /// 
    internal interface IClientDependentRoleManager
    {
        // add
        int AddClientDependentRole(ClientDependentRole clientDependentRole);

        //get
        IEnumerable<ClientDependentRole_VM> GetAllClientDependentRoles();
        IEnumerable<ClientDependentRole_VM> GetInactiveClientDependentRoles();
        IEnumerable<ClientDependentRole_VM> GetClientDependentRolesByClient(int id);

        //update & deactivate
        int EditClientDependentRole(ClientDependentRole clientDependentRole);
        void DeactivateClientDependentRole(ClientDependentRole clientDependentRoles);
        void ActivateClientDependentRole(ClientDependentRole clientDependentRole);

        ClientDependentRole_VM GetClientDependtRoleByClientIdAndDependentId();



    }


}
