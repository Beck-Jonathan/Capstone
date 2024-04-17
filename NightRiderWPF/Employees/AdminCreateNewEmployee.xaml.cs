using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;



namespace NightRiderWPF.Employees
{
    /// <summary>
    /// AUTHOR: James Williams
    /// <br />
    /// CREATED: 2024-02-01
    /// <br />
    ///     The presentation layer displaying a form for an Admin entering an new Employee into the system.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: updater_name
    /// <br />
    /// UPDATED: 2024-02-01
    /// <br />
    /// Initial Creation
    /// <br />
    /// UPDATED BY: Michael Springer
    /// <br />
    /// UPDATED 2024-04-09
    /// Added userName handling and added creation of Login table data
    /// </remarks>
    public partial class AdminCreateNewEmployee : Page
    {
        RoleManager _roleManager = null;
        EmployeeManager _employeeManager = null;
        LoginManager _loginManager = null;
        IEnumerable<Role> _roleList = null;
        List<string> _roles = null;
        bool _userDataAlreadyExists = false;

        public AdminCreateNewEmployee()
        {
            try
            {
                InitializeComponent();

                _roleManager = new RoleManager();
                _employeeManager = new EmployeeManager();
                _loginManager = new LoginManager();
                _roles = new List<string>();
                //Sets the parameters of the datepicker to auto select current date and not allow for selections past current date
                dateDOB.SelectedDate = DateTime.Now;
                dateDOB.DisplayDateEnd = DateTime.Now;

                //Generates combo box of US states 
                generateCBOStates(cboState);
                cboState.SelectedIndex = 0;

                //Hide fields until Country field has data
                cboState.Visibility = Visibility.Hidden;
                lblState.Visibility = Visibility.Hidden;


                //Generates a list box of possible employee roles
                foreach (var role in _roleManager.GetAllRoles())
                {
                    _roles.Add(role.RoleID.ToString());
                }

                lstRoles.ItemsSource = _roles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error encountered\n" + ex.Message);
            }
        }


        //Form submission
        private void btnCreateEmployee_Click(object sender, RoutedEventArgs e)
        {
            // reset this flag on each click
            _userDataAlreadyExists = false;

            string givenName = txtGivenName.Text;
            string familyName = txtFamilyName.Text;
            string address1 = txtAddress1.Text;
            string address2 = txtAddress2.Text ?? "";
            string city = txtCity.Text;
            string state = "";
            if (txtCountry.Text.ToUpper() == "USA" || txtCountry.Text.ToUpper() == "US")
            {
                state = cboState.SelectedValue.ToString().ToUpper();
            }
            string country = txtCountry.Text.ToUpper();
            DateTime dob = dateDOB.SelectedDate.Value;
            string zip = txtZip.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;
            string position = txtPosition.Text;
            // updated - M. Springer
            string userName = txtUserName.Text;

            // Input validation checks
            if (!FormValidationHelper.IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email");
                return;
            }

            if (!FormValidationHelper.IsValidPhoneNumber(phone))
            {
                MessageBox.Show("Please enter a valid telephone number");
                return;
            }
            if (!FormValidationHelper.isValidUserName(userName))
            {
                MessageBox.Show("Please enter a username of less than 50 characters");
                return;
            }
            

            //Checks required form inputs to ensure they are not empty or null
            if (string.IsNullOrWhiteSpace(givenName) ||
                string.IsNullOrWhiteSpace(familyName) ||
                string.IsNullOrWhiteSpace(address1) ||
                string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(country) ||
                string.IsNullOrWhiteSpace(zip) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(position) ||
                string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // Uses a pair of bools before create methods to prevent creation of employee without
            // creation of login
            IEnumerable<Employee> employees = _employeeManager.GetAllEmployees();
            foreach (var employee in employees)
            {
                if (txtEmail.Text == employee.Email)
                {
                    _userDataAlreadyExists = true;
                    MessageBox.Show("An employee with that email already exists");
                    return;
                }
            }
            List<string> usernames = (List<string>)_loginManager.GetAllUserNames();
            foreach (var username in usernames)
            {
                if(txtUserName.Text == username)
                {
                    _userDataAlreadyExists = true;
                    MessageBox.Show("That username already exists");
                    return;
                }
            }
            // Only activates if both an employee and a login can be created
            if (!_userDataAlreadyExists)
            {
                Employee_VM newEmployee = new Employee_VM()
                {

                    Given_Name = givenName,
                    Family_Name = familyName,
                    DOB = dob,
                    Address = address1,
                    Address2 = address2,
                    City = city,
                    State = state,
                    Country = country,
                    Zip = zip,
                    Phone_Number = phone,
                    Email = email,
                    Position = position
                };


                //Adds role(s) selected from presentation list and adds them to the Employee_VM object.
                List<Role> employeeRoles = new List<Role>();
                foreach (var selectedItem in lstRoles.SelectedItems)
                {
                    employeeRoles.Add(new Role() { RoleID = selectedItem.ToString() });
                }
                if (employeeRoles.Count == 0)
                {
                    MessageBox.Show("Please select at least one employee role");
                    return;
                }

                newEmployee.Roles = employeeRoles;
                int newEmployeeID = 0;
                //Inserts new Employee database record
                try
                {
                    newEmployeeID = _employeeManager.AddEmployee(newEmployee);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                //Inserts new Employee login data record
                if (newEmployeeID != 0)
                {
                    try
                    {
                        _loginManager.AddEmployeeLogin(userName, newEmployeeID);
                        MessageBox.Show("Employee added successfully");
                        clearInputFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Username alrady exists");
                    }
                }
            }
            
        }

        //Clears form input fields
        private void clearInputFields()
        {
            txtGivenName.Text = "";
            txtFamilyName.Text = "";
            txtUserName.Text = "";
            txtAddress1.Text = "";
            dateDOB.SelectedDate = DateTime.Now;
            txtAddress2.Text = "";
            txtCity.Text = "";
            cboState.SelectedIndex = 0;
            txtCountry.Text = "";
            txtZip.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtPosition.Text = "";
            lstRoles.SelectedItems.Clear();
        }


        // Populates combobox of US state acronyms 
        public void generateCBOStates(ComboBox cbo)
        {
            List<string> states = new List<string>
            {
                "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA",
                "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
                "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
                "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
                "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
            };
            foreach (string state in states)
            {
                cbo.Items.Add(state);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("No page to go back to.");
            }
        }


        private void txtCountry_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txtCountry.Text == "" || txtCountry.Text == null)
            {
                cboState.Visibility = Visibility.Hidden;
                lblState.Visibility = Visibility.Hidden;

            }
            else if (txtCountry.Text.ToUpper() == "USA" || txtCountry.Text.ToUpper() == "US")
            {
                cboState.Visibility = Visibility.Visible;
                lblState.Visibility = Visibility.Visible;

            }
            else
            {
                cboState.Visibility = Visibility.Hidden;
                lblState.Visibility = Visibility.Hidden;

            }
        }
    }
}
// Checked by Nathan Toothaker
