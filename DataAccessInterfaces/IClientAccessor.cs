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
    /// <remarks>
    /// UPDATER: Isabella Rosenbohm
    /// <br />
    ///    UPDATED: 2024-02-20
    /// <br />
    ///     Implemented InsertClient method
    /// </remarks>

    public interface IClientAccessor
    {
        /// <summary>
        ///     Inserts new client record into the Client table.
        /// </summary>
        /// <param name="newClient">
        ///    Client_VM object that contains the data of the new client to be added to the database
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: Client_ID of newly inserted record; otherwise, <see cref="void">execption</see>.
        /// </returns>
        /// <remarks>
        /// Parameters:
        /// <br />
        ///    <see cref="Client_VM">Client_VM</see> a: Client object
        /// <br /><br />
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: Throws when error entering record
        /// <br />
        ///    <see cref="ApplicationException">ArgumentException</see>: Throws when error entering record
        /// <br/><br/>
        /// CONTRIBUTOR: Isabella Rosenbohm <br/>
        /// CREATED: 2024-02-20
        /// <br/><br/>
        /// UPDATER:  <br/>
        /// UPDATED:  <br/>
        ///     update comment
        /// </remarks>
        int InsertClient(Client_VM newClient);


        Client_VM SelectClientById(int id);
        /// <summary>
        ///     A method that returns an IEnumerable containing all Client_VM records.
        ///     Method does not take in any parameters.
        /// </summary>
        /// <remarks>
        /// Exceptions:
        ///     Throws an exception when the database connection fails.
        /// <br/><br/>
        /// CONTRIBUTOR: Isabella Rosenbohm <br/>
        /// CREATED: 2024-02-05
        /// <br/><br/>
        /// UPDATER: Isabella Rosenbohm <br/>
        /// UPDATED: 2024-02-13 <br/>
        ///     Changed the order of params in clients.Add() section as it was incorrect before
        /// </remarks>
        IEnumerable<Client_VM> SelectAllClients();
        IEnumerable<Client_VM> SelectClients();
        IEnumerable<Client_VM> SelectInactiveClients();

        /// <summary>
        ///     A method that updates the field of a Client and
        ///     returns the number of rows affected.
        /// </summary>
        /// <param name="newClient">
        ///    a Client_VM object
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: The number of rows affected
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: Thrown when incorrect fields are given for the user.
        /// <br /><br />
        /// CONTRIBUTOR: Jared Roberts
        /// CREATED: 2024-02-11
        /// /// <br/><br/>
        /// UPDATER: Jared Roberts <br/>
        /// UPDATED: 2024-02-11 <br/>
        ///     Changed the argument type for the UpdateClient method from int to Client_VM
        /// </remarks>
        int UpdateClient(Client_VM newClient);
        int UpdateClientByIdAsInactive(int id);
        int UpdateClientByIdAsActive(int id);
    }
}
