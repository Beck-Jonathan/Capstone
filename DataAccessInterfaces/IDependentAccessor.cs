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
    }


}
