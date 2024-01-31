/// <summary>
/// AUTHOR: Nathan Toothaker
/// CREATED: 2024-01-23 (yyyy-MM-dd)
/// 
///     An example class to show how code is expected to be written and documented.
///     This is where a description of what your file is supposed to contain goes.
///     e.g., "Class with helper methods for input validation.",
///     "Class that defines Vehicle Objects."
/// </summary>
/// 
/// <remarks>
/// UPDATER: updater_name
/// UPDATED: yyyy-MM-dd
/// 
///     Update comments go here, include method or methods were changed or added 
///     (no other details necessary).
///     A new remark should be added for each update.
/// </remarks>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SampleLibrary
{
    public class SampleClass
    {
        public int x { get; set; }
        public int multiply(int a)
        {
            /// <summary>
            /// CONTRIBUTOR: Nathan Toothaker
            /// CREATED: 2024-01-23
            /// 
            ///     A method that returns x of the instantiated SampleClass object 
            ///     multiplied by positive value a.
            ///     
            /// PARAMETERS:
            ///     <param name="a">
            ///         int: the value to be multiplied.
            ///     </param>
            /// 
            /// RETURNS:
            ///     <returns>int: the input value multiplied by x.</returns>
            ///     
            /// THROWS:
            ///     <exception cref="ArgumentOutOfRangeException">
            ///         a was not a positive number.         
            ///     </exception>
            /// 
            /// </summary>
            /// 
            /// <remarks>
            /// UPDATER: updater_name
            /// UPDATED: yyyy-MM-dd
            /// 
            ///     Update comments go here. Explain what you changed in this method.
            ///     A new remark should be added for each update to this method.
            /// </remarks>


            int result = 0;
            if(a < 1)
            {
                throw new ArgumentOutOfRangeException("a");
            }
            else // if a >= 1
            {
                result = a * x;
            }
            return result;
        }
    }
}
