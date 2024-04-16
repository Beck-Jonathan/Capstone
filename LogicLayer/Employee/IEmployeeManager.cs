using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{

    public interface IEmployeeManager
    {
       
        List<Employee_VM> GetEmployees();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <returns int="newID"></returns>
        /// <remarks>
        /// UPDATER: Michael Springer
        /// <br />
        /// UPDATED: 2024-04-13
        /// Changed return type from bool to integer
        /// </remarks>
        int AddEmployee(Employee_VM newEmployee);
        /// <summary>
        ///   retrieves a list of employees
        /// </summary>
        /// <param>
        ///    None
        /// </param>
        /// <returns>
        ///     <see cref="IEnumerable{Employee_VM}"/>: Returns the list of employees.
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
        IEnumerable<Employee_VM> GetAllEmployees();

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
        IEnumerable<Role> GetRolesByEmployeeID(int employeeID);
        // Reviewed By Steven Sanchez

        /// <summary>
        ///   retrieves an employee by ID
        /// </summary>
        /// <param>
        ///    Employee_ID
        /// </param>
        /// <returns>
        ///     <see cref="Employee_VM"/>: returns an employee.
        /// </returns>
        /// <remarks>
        ///    Parameters: id
        /// <br />
        /// <br /><br />
        ///    Exceptions:None
        /// <br />
        /// <br /><br />
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-02-11
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Update comments go here. Explain what you changed in this method.
        ///     A new remark should be added for each update to this method.
        /// </remarks>
        Employee_VM GetEmployee(int id);
        // Checked by Jared R.

        /// <summary>
        ///   Updates employee record
        /// </summary>
        /// <param>
        ///    Employee_VM updatedEmployee
        /// </param>
        /// <param>
        ///     Employee_VM originalEmployee
        /// </param>
        /// <returns>
        ///     <see cref="int"/>: returns number of employee records affected.
        /// </returns>
        /// <remarks>
        ///    Parameters: updatedEmployee, originalEmployee
        /// <br />
        /// <br /><br />
        ///    Exceptions:None
        /// <br />
        /// <br /><br />
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-17
        /// <br />
        int EditEmployee(Employee_VM updatedEmployee, Employee_VM originalEmployee);

        /// <summary>
        ///   Dectivate employee record by employee id
        /// </summary>
        /// <param>
        ///    int employeeID
        /// </param>
        /// <returns>
        ///     <see cref="int"/>: returns rows affected
        /// </returns>
        /// <remarks>
        ///    Parameters: employeeID
        /// <br />
        /// <br /><br />
        ///    Exceptions:ApplicationException
        /// <br />
        /// <br /><br />
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-22
        int DeactivateEmployeeByID(int employeeID);
        /// <summary>
        ///   Activate employee record by employee id
        /// </summary>
        /// <param>
        ///    int employeeID
        /// </param>
        /// <returns>
        ///     <see cref="int"/>: returns rows affected
        /// </returns>
        /// <remarks>
        ///    Parameters: employeeID
        /// <br />
        /// <br /><br />
        ///    Exceptions:ApplicationException
        /// <br />
        /// <br /><br />
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-22
        int ReactivateEmployeeByID(int employeeID);
    }
}
// Checked by Nathan Toothaker