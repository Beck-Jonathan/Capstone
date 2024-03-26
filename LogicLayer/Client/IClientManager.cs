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
    /// <remarks>
    /// UPDATER: Isabella Rosenbohm <br />
    /// UPDATED: 2024-02-20 <br />
    ///     Implemented AddClient method
    /// <br/><br/>
    /// UPDATER: Isabella Rosenbohm <br/>
    /// UPDATED: 2024-02-27 <br/>
    ///     Changed intake param of EditClient from newClient to client 
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
        ///    CONTRIBUTOR: Isabella Rosenbohm <br />
        ///    CREATED: 2024-02-05
        /// </remarks>
        IEnumerable<DataObjects.Client> GetAllClients();
        IEnumerable<DataObjects.Client> GetClients();
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
        Client_VM GetClientByEmail(string email);
        IEnumerable<DataObjects.Client> GetInactiveClients();
        int EditClient(Client_VM newClient);
        void DeactivateClient(int id);
        void ActivateClient(int id);
    }
}
