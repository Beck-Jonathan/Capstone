using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleLibrary
{
    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// <br />
    /// CREATED: 2024-01-23 (yyyy-MM-dd)
    /// <br />
    /// 
    ///     An example class to show how code is expected to be written and documented.
    ///     This is where a description of what your file is supposed to contain goes.
    ///     e.g., "Class with helper methods for input validation.",
    ///     "Class that defines Vehicle Objects."
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: updater_name
    /// <br />
    /// UPDATED: yyyy-MM-dd
    /// <br />
    /// 
    ///     Update comments go here, include method or methods were changed or added 
    ///     (no other details necessary).
    ///     A new remark should be added for each update.
    /// </remarks>
    public class SampleClass
    {
        public int x { get; set; }

        /// <summary>
        ///     A method that returns X of the instantiated SampleClass object 
        ///     multiplied by positive value a.
        /// </summary>
        /// <param name="a">
        ///    The value to be multiplied
        /// </param>
        /// <returns>
        ///    <see cref="int">int</see>: The input value multiplied by x.
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="int">int</see> a: The value to be multiplied
        /// <br /><br />
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentOutOfRangeException">ArgumentOutOfRangeException</see>: Thrown when a is not a positive number.
        /// <br /><br />
        ///    CONTRIBUTOR: Nathan Toothaker
        /// <br />
        ///    CREATED: 2024-01-23
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Update comments go here. Explain what you changed in this method.
        ///     A new remark should be added for each update to this method.
        /// </remarks>
        public int Multiply(int a)
        {
            int result = 0;
            if (a < 1)
            {
                throw new ArgumentOutOfRangeException("a is not a positive number");
            }
            else // if a >= 1
            {
                result = a * x;
            }

            return result;
        }
    }
}