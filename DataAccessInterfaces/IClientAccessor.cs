using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    /// <summary>
    /// AUTHOR: Isabella Rosenbohm, Jared Roberts, Jacob Wendt
    /// <br />
    /// CREATED: 2024-02-05
    /// <br />
    /// 
    ///     Methods for the database access for clients.
    /// </summary>
    /// 

    public interface IClientAccessor
    {
        int InsertClient(Client_VM client);
        Client_VM SelectClientById(int id);
        /// <summary>
        ///     A method that returns an IEnumerable containing all Client_VM records.
        ///     Method does not take in any parameters.
        /// </summary>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///   Throws an exception when the database connection fails.
        /// <br /><br />
        ///    CONTRIBUTOR: Isabella Rosenbohm
        /// <br />
        ///    CREATED: 2024-02-05
        /// </remarks>
        IEnumerable<Client_VM> SelectAllClients();
        IEnumerable<Client_VM> SelectClients();
        IEnumerable<Client_VM> SelectInactiveClients();
        int UpdateClient(Client client);
        int UpdateClientByIdAsInactive(int id);
        int UpdateClientByIdAsActive(int id);
    }
}
