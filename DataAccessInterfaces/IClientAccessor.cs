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
        /// Exceptions:
        ///     Throws an exception when the database connection fails.
        /// <br/><br/>
        /// CONTRIBUTOR: Isabella Rosenbohm <br/>
        /// CREATED: 2024-02-05
        /// <br/><br/>
        /// UPDATER: Isabella Rosenbohm <br/>
        /// UPDATED: 2024-02-13 <br/>
        ///     Changed the order of params in clients.Add() section as it was incorrect before
        /// UPDATER: Jared Roberts
        /// UPDATED: 2024-02-11
        ///     Changed the argument type for the UpdateClient method from int to Client_VM
        /// </remarks>
        IEnumerable<Client_VM> SelectAllClients();
        IEnumerable<Client_VM> SelectClients();
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
        Client_VM SelectClientByEmail(string email);
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
        ///    CONTRIBUTOR: Jared Roberts
        /// <br />
        ///    CREATED: 2024-02-11
        /// <br /><br />
        ///     Initial creation
        /// </remarks>
        int UpdateClient(Client_VM newClient);
        int UpdateClientByIdAsInactive(int id);
        int UpdateClientByIdAsActive(int id);
    }
}
