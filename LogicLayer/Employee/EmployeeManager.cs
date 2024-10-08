﻿using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// AUTHOR: James Williams
    /// <br />
    /// CREATED: 2024-02-04
    /// <br />
    /// 
    ///     Manager class for Employee that handles database access.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: [Updater's Name]
    /// <br />
    /// UPDATED: 2024-02-04
    /// <br />
    /// initial creation
    /// </remarks>
    public class EmployeeManager : IEmployeeManager
    {
        IEmployeeAccessor _employeeAccessor = null;
        //default constructor
        public EmployeeManager()
        {
            _employeeAccessor = new EmployeeAccessor();
        }

        //parametized constructor to allow use of data fakes
        public EmployeeManager(IEmployeeAccessor employeeAccessor)
        {
            _employeeAccessor = employeeAccessor;
        }


        /// <summary>
        ///     Inserts new employee record into the Employee table and Employee_Role table
        /// </summary>
        /// <param name="newEmployee">
        ///    Employee_VM object that contains the data of the new employee to be added to the database
        /// </param>
        /// <returns>
        ///    <see cref="bool">true</see> if the email address is valid; otherwise, <see cref="bool">false</see>.
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="Employee_VM">Employee_VM</see> a: Employee object to be inserted
        /// <br /><br />
        ///    Exceptions:
        /// <br />
        ///    <see cref="ApplicationException">ApplicationException</see>: Thrown when error entering record
        /// <br /><br />
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-01
        /// <br /><br />
        ///    UPDATER: Michael Springer
        /// <br />
        ///    UPDATED: 2024-04-13
        /// <br />
        ///     Changed output from bool to an int representing the assigned employeeID
        ///     
        /// ///    UPDATER: Jacob Rohr
        /// <br />
        ///    UPDATED: 2024-04-24
        /// <br />
        ///     Changed where it adds roles to add them only if it isn't null.
        /// </remarks>
        /// </remarks>
        public int AddEmployee(Employee_VM newEmployee)
        {
            //new Employee_ID after insert
            int newID = 0;
            
            try
            {
                newID = _employeeAccessor.InsertEmployee(newEmployee);
             
                int rows = 0;
                // Creates a copy of Employe_Roles to be iterated
                
                if(newEmployee.Roles != null)
                {
                    var rolesCopy = newEmployee.Roles.ToList();
                    foreach (var role in rolesCopy)
                    {
                        string roleName = role.RoleID;
                        //inserts new record(s) and returns number of rows inserted
                        rows = _employeeAccessor.InsertEmployeeRoles(newID, roleName);
                    }
                }
                
                // assumes if 0 rows returned that the insert failed
             
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error entering new employee record", ex);
            }
            //return (0 != newID);
            return newID;
        }

        /// <summary>
        ///     Retrieves all Employee records from the database that are active
        /// </summary>
        /// <returns>
        ///    List of Employee_VM objects
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-01
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: 2024-02-22
        /// <br />
        ///     Updated method name and call to accessor to better represent that this method only returns active employee records
        /// </remarks>
        public List<Employee_VM> GetEmployees()
        {
            List<Employee_VM> employees = null;

            try
            {
                employees = _employeeAccessor.GetEmployees();
             
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return employees;
        }

        /// <summary>
        ///     gets all employee records, active and inactive
        /// </summary>
        /// <param>
        ///    None
        /// </param>
        /// <returns>
        ///    <see cref="IEnumerable{Employee_VM} results"/>: Returns the list of employees.
        /// </returns>
        /// <remarks>
        ///    Parameters:None
        /// <br />
        /// <br /><br />
        ///    Exceptions: ArgumentException
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>:Thrown when no employees are found in the database.
        /// <br />
        ///    <br / >
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-02-03
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: 2024-02-22
        /// <br />
        ///    Changed method name and method call to accessor to better represent that it gets all employee records, active or inactive
        /// </remarks>
        public IEnumerable<Employee_VM> GetAllEmployees()
        {


            IEnumerable<Employee_VM> result = null;
            try
            {
                result = _employeeAccessor.GetAllEmployees();
                if (result.Count() == 0)
                {
                    throw new ArgumentException("No Employees found in the database.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

      
        public IEnumerable<Role> GetRolesByEmployeeID(int employeeID)
        {
            IEnumerable<Role> roles = null;
            try
            {
                roles = _employeeAccessor.GetRolesByEmployeeID(employeeID);

            }
            catch(Exception ex)
            {
                throw new ArgumentException("Error finding roles",ex);
            }
            return roles;
        }
        // Reviewed By Steven Sanchez

        /// <summary>
        ///     gets an employee's record
        /// </summary>
        /// <param>
        ///    Employee_ID
        /// </param>
        /// <returns>
        ///    <see cref="Employee_VM results"/>: Returns an employee .
        /// </returns>
        /// <remarks>
        ///    Parameters:id
        /// <br />
        /// <br /><br />
        ///    Exceptions: ArgumentException
        /// <br />
        ///    <see cref="ArgumentException">ArgumentException</see>:Thrown when an employee is not found in the database.
        /// <br />
        ///    <br / >
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

        public Employee_VM GetEmployee(int id)
        {
            Employee_VM result = null;
            try
            {
                result = _employeeAccessor.GetEmployee(id);
                if (result == null)
                {
                    throw new ArgumentException("Employee not found in the database.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
        // checked by Jared R.

        public int EditEmployee(Employee_VM updatedEmployee, Employee_VM originalEmployee)
        {
            int rows = 0;
            try
            {
                rows = _employeeAccessor.UpdateEmployee(updatedEmployee, originalEmployee);
                if (rows == 0) 
                {
                    throw new ApplicationException("Error updating employee");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rows;
        }

        public int DeactivateEmployeeByID(int employeeID)
        {
            int rows = 0;
            try
            {
                rows = _employeeAccessor.DeactivateEmployeeByID(employeeID);
                if(rows == 0)
                {
                    throw new ArgumentException("No records updated at this time");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return rows;
        }

        public int ReactivateEmployeeByID(int employeeID)
        {
            int rows = 0;
            try
            {
                rows = _employeeAccessor.ActivateEmployeeByID(employeeID);
                if (rows == 0)
                {
                    throw new ArgumentException("No records updated at this time");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rows;
        }

        public Employee_VM GetEmployeeByEmail(string email)
        {
            Employee_VM result = null;
            try
            {
                result = _employeeAccessor.GetEmployeeByEmail(email);
                if (result == null)
                {
                    throw new ArgumentException("Employee not found in the database.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
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
        ///    CONTRIBUTOR: Jacob Rohr
        /// <br />
        ///    CREATED: 2024-04-23
        /// <br />
        public int InsertEmployeeRoles(int employee_ID, string role)
        {
            int result = 0;
            try
            {
                result = _employeeAccessor.InsertEmployeeRoles(employee_ID, role);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Role not added!", ex);
            }
            return result;
        }

        /// <summary>
        ///     deletes employee role record into database
        /// </summary>
        /// <param name="employeeID">
        ///    The ID of the employee you would like to delete a role from
        /// </param>
        /// <param name="role">
        ///     The role you would like to get rid of 
        /// </param>
        /// <returns>
        ///    <see cref="int"> Number of rows inserted </see>
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="int">employeeID</see> a: ID of employee to remove roles from
        /// <br />
        /// <br />
        ///    <see cref="string">role</see> a: role to be removed
        /// <br />
        ///    Exceptions:
        ///   <br />
        ///   <br />
        ///    <see cref="ApplicationException">ArgumentException</see>: Throws when no roles found
        /// <br /><br />
        ///    CONTRIBUTOR: Jacob Rohr
        /// <br />
        ///    CREATED: 2024-04-23
        /// <br />
        public int RemoveEmployeeRoles(int employee_ID, string role)
        {
            int result = 0;
            try
            {
                result = _employeeAccessor.RemoveEmployeeRoles(employee_ID, role);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Role not added!", ex);
            }
            return result;
        }



        //Does the same thing as the GetEmployeeByEmail except it only returns the ID
        public int RetrieveEmployeeIDFromEmail(string email)
        {
            try
            {
                return _employeeAccessor.GetEmployeeByEmail(email).Employee_ID;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database error", ex);
            }
        }


    }
    

}
// Checked by Nathan Toothaker