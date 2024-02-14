﻿using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{

    public interface IEmployeeAccessor
    {
        List<Employee_VM> GetAllEmployees();
        int InsertEmployee(Employee_VM newEmployee);
        int InsertEmployeeRoles(int employee_ID, string role);


        /// <summary>
        ///   retrieves a list of employees from Employee_Fakes 
        /// </summary>
        /// <param>
        ///    None
        /// </param>
        /// <returns>
        ///     <see cref="IEnumerable{Employee_VM}"/>: returns the list of fake employees.
        /// </returns>
        /// <remarks>
        ///    Parameters:None
        /// <br />
        /// <br /><br />
        ///    Exceptions:None
        /// <br />
        /// <br /><br />
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-02-03
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Update comments go here. Explain what you changed in this method.
        ///     A new remark should be added for each update to this method.
        /// </remarks>
        IEnumerable<Employee_VM> GetEmployees();

        /// <summary>
        ///     Method to retrieve all roles of an employee
        /// </summary>
        /// <param name="employeeID">
        ///    Int representing the id of an employee.
        /// </param>
        /// <returns>
        ///    <see cref="IEnumerable{Role}">IEnumerable{Role}</see>: A list of roles associated with the employee id param
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="int">int</see> a: Employee ID
        /// <br /><br />
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentOutOfRangeException">ArgumentException</see>: No roles found
        /// <br /><br />
        ///    CONTRIBUTOR: James Williams
       
        IEnumerable<Role> GetRolesByEmployeeID(int employee_ID);
        // Reviewed By Steven Sanchez
    }
}
// Checked by Nathan Toothaker