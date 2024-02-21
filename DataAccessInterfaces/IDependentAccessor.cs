/// <summary>
/// Michael Springer
/// Created: 2024/02/04
/// 
/// Interface for the Dependent Data Access Classes
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IDependentAccessor
    {
        int InsertDependent(Dependent dependent);

        /// <summary>
        /// CONTRIBUTOR: Jacob Rohr
        /// CREATED: 2024-02-13
        /// 
        ///     A Method to retrieve a complete list of all dependents
        ///     
        /// <returns> Returns: <see cref = "IEnumerable{DependentVM}" > IEnumerable Of Dependent VM</see></returns>
        ///     
        /// </summary>
        IEnumerable<DependentVM> ListAllDependents();
    }


}
