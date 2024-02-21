using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public class EmployeeAccessor : IEmployeeAccessor
    {
        /// <inheritdoc/>



        /// <summary>
        /// AUTHOR: James Williams
        /// <br />
        /// CREATED: 2024-02-02
        /// <br />
        /// 
        ///     Data access class for Employee 
        /// </summary>
        /// 
        /// <remarks>
        /// UPDATER: [Updater's Name]
        /// <br />
        /// UPDATED: 2024-02-02
        /// <br />
        /// Initial Creation
        /// </remarks>
        /// 



        /// <summary>
        ///     Inserts new employee record into the Employee table.
        /// </summary>
        /// <param name="newEmployee">
        ///    Employee_VM object that contains the data of the new employee to be added to the database
        /// </param>
        /// <returns>
        ///    <see cref="bool">true</see> Employee_ID of newly inserted record; otherwise, <see cref="void">execption</see>.
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
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial creation
        /// </remarks>
        public int InsertEmployee(Employee_VM newEmployee)
        {
            int newID = 0;


            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_insert_employee";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_Given_Name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@p_Family_Name", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@p_DOB", SqlDbType.DateTime, 50);
            cmd.Parameters.Add("@p_Address", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@p_Address2", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@p_City", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@p_State", SqlDbType.NVarChar, 2);
            cmd.Parameters.Add("@p_Country", SqlDbType.NVarChar, 3);
            cmd.Parameters.Add("@p_Zip", SqlDbType.NVarChar, 9);
            cmd.Parameters.Add("@p_Phone_Number", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@p_Email", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@p_Position", SqlDbType.NVarChar, 20);


            cmd.Parameters["@p_Given_Name"].Value = newEmployee.Given_Name;
            cmd.Parameters["@p_Family_Name"].Value = newEmployee.Family_Name;
            cmd.Parameters["@p_DOB"].Value = newEmployee.DOB;
            cmd.Parameters["@p_Address"].Value = newEmployee.Address;
            cmd.Parameters["@p_Address2"].Value = newEmployee.Address2;
            cmd.Parameters["@p_City"].Value = newEmployee.City;
            cmd.Parameters["@p_State"].Value = newEmployee.State;
            cmd.Parameters["@p_Country"].Value = newEmployee.Country;
            cmd.Parameters["@p_Zip"].Value = newEmployee.Zip;
            cmd.Parameters["@p_Phone_Number"].Value = newEmployee.Phone_Number;
            cmd.Parameters["@p_Email"].Value = newEmployee.Email;
            cmd.Parameters["@p_Position"].Value = newEmployee.Position;

            try
            {
                conn.Open();
                newID = Convert.ToInt32(cmd.ExecuteScalar());
                if (newID == 0)
                {
                    throw new ArgumentException("Error inserting employee record");
                }

            }
            catch (Exception e)
            {
                throw new ApplicationException("Error executing command to insert employee record", e);
            }
            finally
            {
                conn.Close();
            }
            return newID;
        }


        /// <summary>
        ///     Retrieves all employee records from database
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
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: 2024-02-01
        /// <br />
        ///     Initial creation
        /// </remarks>




        public List<Employee_VM> GetAllEmployees()
        {
            List<Employee_VM> employees = new List<Employee_VM>();

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_select_employees";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                   
                    Employee_VM employee_VM = new Employee_VM()
                    {
                        Employee_ID = reader.GetInt32(0),
                        Given_Name = reader.GetString(1),
                        Family_Name = reader.GetString(2),
                        DOB = reader.GetDateTime(3),
                        Address = reader.GetString(4),
                        Address2 = reader.IsDBNull(5) ? null : reader.GetString(4),
                        City = reader.GetString(6),
                        State = reader.GetString(7),
                        Country = reader.GetString(8),
                        Zip = reader.GetString(9),
                        Phone_Number = reader.GetString(10),
                        Email = reader.GetString(11),
                        Position = reader.GetString(12)
                    };
                    employees.Add(employee_VM);
                }

                if (employees.Count == 0)
                {
                    throw new ArgumentException("No employee records found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return employees;
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
        ///    CONTRIBUTOR: James Williams
        /// <br />
        ///    CREATED: 2024-02-01
        /// <br /><br />
        ///    UPDATER: updater_name
        /// <br />
        ///    UPDATED: 2024-02-01
        /// <br />
        ///     Initial creation
        /// </remarks>
        public int InsertEmployeeRoles(int employeeID, string role)
        {
            int rows = 0;

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_insert_employee_role";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_Employee_ID", SqlDbType.Int).Value = employeeID;
            cmd.Parameters.Add("@p_Role_ID", SqlDbType.NVarChar, 25).Value = role;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    throw new ApplicationException("Error adding employee roles");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        ///     gets all employees from the Employee Table
        /// </summary>
        /// <param>
        ///    None
        /// </param>
        /// <returns>
        ///     <see cref="IEnumerable{Employee_VM}"/>: returns the list of employees from the database.
        /// </returns>
        /// <remarks>
        ///    Parameters:None
        /// <br />
        /// <br /><br />
        ///    Exceptions: SqlException
        /// <br />
        ///    <see cref="SqlException">SqlException</see>: Thrown when a SQL Server error occurs.
        ///    <br / >
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-02-03
        /// <br /><br />
        ///    UPDATER: James Williams
        /// <br /> 2024-02-16
        /// <br />
        ///     Updated how to handle returning Address2. If it wasn't null, it was setting the value from Address1 instead of Address2
        /// </remarks>
        public IEnumerable<Employee_VM> GetEmployees()
        {
            List<Employee_VM> employeeList = new List<Employee_VM>();
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_select_all_employees";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // There are no parameters to set or add
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var Employee = new Employee_VM();
                        Employee.Employee_ID = reader.GetInt32(0);
                        Employee.Given_Name = reader.GetString(1);
                        Employee.Family_Name = reader.GetString(2);
                        Employee.DOB = reader.GetDateTime(3).Date;
                        Employee.Address = reader.GetString(4);
                        Employee.Address2 = reader.IsDBNull(5) ? null : reader.GetString(5);
                        Employee.City = reader.GetString(6);
                        Employee.State = reader.GetString(7);
                        Employee.Country = reader.GetString(8);
                        Employee.Zip = reader.GetString(9);
                        Employee.Phone_Number = reader.GetString(10);
                        Employee.Email = reader.GetString(11);
                        Employee.Position = reader.GetString(12);
                        Employee.Is_Active = reader.GetBoolean(13);


                        employeeList.Add(Employee);
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return employeeList;
        }

        public IEnumerable<Role> GetRolesByEmployeeID(int employee_ID)
        {
            IEnumerable<Role> roles = null;
            List<Role> roleList = new List<Role>();

            var conn = DBConnectionProvider.GetConnection();
            var cmdText = "sp_get_roles_by_employee_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@p_Employee_ID", SqlDbType.Int).Value = employee_ID;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Role role = new Role()
                        {
                            RoleID = reader.GetString(0),
                            IsActive = true
                        };
                        roleList.Add(role);
                    }
                }
                else
                {
                    throw new ApplicationException("No roles found");
                }
                roles = roleList;
            }

            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving roles", ex);
            }
            finally
            {
                conn.Close();
            }

            return roles;
        }
        // Reviewed By Steven Sanchez

        /// <summary>
        ///     gets an employees from the Employee Table
        /// </summary>
        /// <param>
        ///    Employee_ID int
        /// </param>
        /// <returns>
        ///     <see cref="Employee_VM"/>: returns an employees from the database.
        /// </returns>
        /// <remarks>
        ///    Parameters:id int
        /// <br />
        /// <br /><br />
        ///    Exceptions: SqlException
        /// <br />
        ///    <see cref="SqlException">SqlException</see>: Thrown when a SQL Server error occurs.
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
            Employee_VM employee = new Employee_VM();
            // start with a connection object
            var conn = DBConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_select_employee_by_id";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Employee_ID", id);
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var Employee = new Employee_VM();
                        Employee.Given_Name = reader.GetString(1);
                        Employee.Family_Name = reader.GetString(2);
                        Employee.DOB = reader.GetDateTime(3).Date;
                        Employee.Address = reader.GetString(4);
                        Employee.Address2 = reader.IsDBNull(5) ? null : reader.GetString(4);
                        Employee.City = reader.GetString(6);
                        Employee.State = reader.GetString(7);
                        Employee.Country = reader.GetString(8);
                        Employee.Zip = reader.GetString(9);
                        Employee.Phone_Number = reader.GetString(10);
                        Employee.Email = reader.GetString(11);
                        Employee.Position = reader.GetString(12);
                        Employee.Is_Active = reader.GetBoolean(13);
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return employee;

        }
        // checked by Jared R.

        /// <summary>
        ///   Update employee record in database
        /// </summary>
        /// <param>
        ///    Employee_VM updatedEmployee
        /// </param>
        /// <param>
        ///     Employee_VM originalEmployee
        /// </param>
        /// <returns>
        ///     <see cref="int"/>: Number of rows affected.
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
        ///    CREATED: 2024-02-17
        /// <br />
        public int UpdateEmployee(Employee_VM updatedEmployee, Employee_VM originalEmployee)
        {
            int rowsAffected = 0;

            using (var conn = DBConnectionProvider.GetConnection())
            {
                var cmdText = "sp_update_employee";
                using (var cmd = new SqlCommand(cmdText, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add("@p_Employee_ID", SqlDbType.Int).Value = originalEmployee.Employee_ID;
                    cmd.Parameters.Add("@p_New_Given_Name", SqlDbType.NVarChar, 50).Value = updatedEmployee.Given_Name;
                    cmd.Parameters.Add("@p_New_Family_Name", SqlDbType.NVarChar, 50).Value = updatedEmployee.Family_Name;
                    cmd.Parameters.Add("@p_New_DOB", SqlDbType.DateTime).Value = updatedEmployee.DOB;
                    cmd.Parameters.Add("@p_New_Address", SqlDbType.NVarChar, 50).Value = updatedEmployee.Address;
                    cmd.Parameters.Add("@p_New_Address2", SqlDbType.NVarChar, 50).Value = updatedEmployee.Address2 ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@p_New_City", SqlDbType.NVarChar, 20).Value = updatedEmployee.City;
                    cmd.Parameters.Add("@p_New_State", SqlDbType.NVarChar, 2).Value = updatedEmployee.State;
                    cmd.Parameters.Add("@p_New_Country", SqlDbType.NVarChar, 3).Value = updatedEmployee.Country;
                    cmd.Parameters.Add("@p_New_Zip", SqlDbType.NVarChar, 9).Value = updatedEmployee.Zip;
                    cmd.Parameters.Add("@p_New_Phone_Number", SqlDbType.NVarChar, 20).Value = updatedEmployee.Phone_Number;
                    cmd.Parameters.Add("@p_New_Email", SqlDbType.NVarChar, 50).Value = updatedEmployee.Email;
                    cmd.Parameters.Add("@p_New_Position", SqlDbType.NVarChar, 20).Value = updatedEmployee.Position;

                    cmd.Parameters.Add("@p_Old_Given_Name", SqlDbType.NVarChar, 50).Value = originalEmployee.Given_Name;
                    cmd.Parameters.Add("@p_Old_Family_Name", SqlDbType.NVarChar, 50).Value = originalEmployee.Family_Name;
                    cmd.Parameters.Add("@p_Old_Address", SqlDbType.NVarChar, 50).Value = originalEmployee.Address;
                    cmd.Parameters.Add("@p_Old_City", SqlDbType.NVarChar, 20).Value = originalEmployee.City;
                    cmd.Parameters.Add("@p_Old_State", SqlDbType.NVarChar, 2).Value = originalEmployee.State;
                    cmd.Parameters.Add("@p_Old_Country", SqlDbType.NVarChar, 3).Value = originalEmployee.Country;
                    cmd.Parameters.Add("@p_Old_Zip", SqlDbType.NVarChar, 9).Value = originalEmployee.Zip;
                    cmd.Parameters.Add("@p_Old_Phone_Number", SqlDbType.NVarChar, 20).Value = originalEmployee.Phone_Number;
                    cmd.Parameters.Add("@p_Old_Email", SqlDbType.NVarChar, 50).Value = originalEmployee.Email;
                    cmd.Parameters.Add("@p_Old_Position", SqlDbType.NVarChar, 20).Value = originalEmployee.Position;


                    try
                    {
                        conn.Open();
                    
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error updating Employee", ex);
                    }
                }
            }

            return rowsAffected;
        }

   
    }
}
// Checked by Nathan Toothaker