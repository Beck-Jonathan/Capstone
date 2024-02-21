/// <summary>
/// Michael Springer
/// Created: 2024/02/04
/// 
/// Fake for Dependent Accessor
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class DependentAccessorFake : IDependentAccessor
    {
        public int InsertDependent(Dependent dependent)
        {
           if (dependent != null)
            {
                return 1;
            }
           else { return 0; }
        }
    }

}
