/// <summary>
/// Michael Springer
/// Created: 2024/02/04
/// 
/// Interface for Dependent Manager classes
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

namespace LogicLayer
{
    internal interface IDependentManager
    {
        int AddDependent(Dependent dependent);


    }
}
