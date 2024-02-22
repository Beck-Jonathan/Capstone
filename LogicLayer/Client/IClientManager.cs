using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// AUTHOR: Isabella Rosenbohm, Jared Roberts, Jacob Wendt
    /// <br />
    /// CREATED: 2024-02-05
    /// <br />
    /// 
    ///     Provides CRUD operations on the data source for client data.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Isabella Rosenbohm
    /// <br />
    ///    UPDATED: 2024-02-20
    /// <br />
    ///     Implemented AddClient method
    /// </remarks>

    public interface IClientManager
    {
        /// <summary>
        ///     Inserts new client record into the Client table
        /// </summary>
        /// <param name="newClient">
        ///    Client_VM object that contains the data of the new client to be added to the database
        /// </param>
        /// <returns>
        ///    <see cref="bool">true</see> if the client object is valid; otherwise, <see cref="bool">false</see>.
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="Client_VM">Client_VM</see> a: Client object to be inserted
        /// <br /><br />
        ///    Exceptions:
        /// <br />
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown when error entering record
        /// <br /><br />
        ///    CONTRIBUTOR: Isabella Rosenbohm
        /// <br />
        ///    CREATED: 2024-02-20
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     update comment
        /// </remarks>
        bool AddClient(Client_VM newClient);
        Client_VM GetClientById(int id);
        /// <summary>
        ///     A method that returns an IEnumerable containing all Client_VM records.
        ///     Method does not take in any parameters.
        /// </summary>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///   Throws an application exception when it catches an exception from ClientAccessor.
        /// <br /><br />
        ///    CONTRIBUTOR: Isabella Rosenbohm
        /// <br />
        ///    CREATED: 2024-02-05
        /// </remarks>
        IEnumerable<Client> GetAllClients();
        IEnumerable<Client> GetClients();
        IEnumerable<Client> GetInactiveClients();
        int EditClient(Client_VM newClient);
        void DeactivateClient(int id);
        void ActivateClient(int id);
    }
}
