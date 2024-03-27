using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    /// <summary>
    /// AUTHOR: Michael Springer
    /// <br />
    /// CREATED: 2024-02-25
    /// <br />
    /// 
    ///     Methods for clientdependentrole database access
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: J
    /// <br />
    ///    UPDATED:
    /// <br />
    /// </remarks>
    /// 
    public interface IClientDependentRoleAccessor
    {
        int InsertClientDependentRole(ClientDependentRole clientDepndentRole);

        IEnumerable<ClientDependentRole_VM> SelectAllClientDependentRoles();
        IEnumerable<ClientDependentRole_VM> SelectInactiveClientDependentRoles();


        IEnumerable<ClientDependentRole_VM> SelectClientDependentRolesByClientId(int id);

        int UpdateClientDependentRole(ClientDependentRole clientDepndentRole);
        int UpdateClientDependentRoleAsInactive(ClientDependentRole clientDepndentRole);
        int UpdateClientDependentroleAsActive(ClientDependentRole clientDepndentRole);


    }
}
