using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{

    public interface IEmployeeAccessor
    {
        List<Employee_VM> GetEmployees();

        /// <summary>
        ///     Inserts new employee record into the Employee table.
        /// </summary>
        /// <param name="newEmployee">
        ///    Employee_VM object that contains the data of the new employee to be added to the database
        /// </param>
        /// <returns>
        ///    <see cref="int">true</see> Employee_ID of newly inserted record; otherwise, <see cref="void">execption</see>.
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="Employee_VM">Employee_VM</see> a: Employee object
        /// <br /><br />
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: Throws when error entering record
        /// <br /><br />
        ///         /// <br />
        ///    <see cref="ApplicationException">ArgumentException</see>: Throws when error entering record
        /// <br /><br />
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-01
        /// <br />
        int InsertEmployee(Employee_VM newEmployee);

        /// <summary>
        ///     Inserts new employee role record into database
        /// </summary>
        /// <param name="employeeID">
        ///    The ID of the employee you would like to insert a role for
        /// </param>
        /// <param name="role">
        ///     The role you would like to associate with the employee
        /// </param>
        /// <returns>
        ///    <see cref="int"> Number of rows inserted </see>
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="int">employeeID</see> a: ID of employee to insert roles on
        /// <br />
        /// <br />
        ///    <see cref="string">role</see> a: role to be inserted
        /// <br />
        ///    Exceptions:
        ///   <br />
        ///   <br />
        ///    <see cref="ApplicationException">ArgumentException</see>: Throws when no roles inserted
        /// <br /><br />
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-01
        /// <br />
        int InsertEmployeeRoles(int employee_ID, string role);


        /// <summary>
        ///     Retrieves all active employee records from database, active and inactive
        /// </summary>

        /// <returns>
        ///    <see cref="List<Employee_VM></Employee_VM>">true</see> List of Employee_VM objects otherwise, <see cref="Exception">execption</see>.
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>: No records returned
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-01
        /// <br /><br />
        ///    UPDATER: James Williams
        /// <br />
        ///    UPDATED: 2024-02-22
        /// <br />
        ///     Changed name to represent that class returns all active and inactive employees
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
       
        IEnumerable<Role> GetRolesByEmployeeID(int employee_ID);
       

        /// <summary>
        ///   retrieves an employee by ID from Employee_Fakes 
        /// </summary>
        /// <param>
        ///    Employee_ID
        /// </param>
        /// <returns>
        ///     <see cref="Employee_VM"/>: returns a fake employee.
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
        // checked by Jared R.


        /// <summary>
        ///   Update employee record
        /// </summary>
        /// <param>
        ///    Employee_VM updatedEmployee
        /// </param>
        /// <param>
        ///    Employee_VM originalEmployee
        /// </param>
        /// <returns>
        ///     <see cref="int"/>: returns rows affected
        /// </returns>
        /// <remarks>
        ///    Parameters: updatedEmployee, originalEmployee
        /// <br />
        /// <br /><br />
        ///    Exceptions:ApplicationException
        /// <br />
        /// <br /><br />
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-15
        int UpdateEmployee(Employee_VM updatedEmployee, Employee_VM originalEmployee);

        /// <summary>
        ///   Deactivate employee record by employee id
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
        int ActivateEmployeeByID(int employeeID);
    }
}
