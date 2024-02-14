using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

     

namespace NightRiderWPF.DeveloperView
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
    /// </remarks>
    public partial class AdminCreateNewEmployee : Page
    {
        RoleManager _roleManager = null;
        EmployeeManager _employeeManager = null;
        IEnumerable<Role> _roleList = null;
        List<string> _roles = null;

        public AdminCreateNewEmployee()
        {
            try
            {
                InitializeComponent();

                _roleManager = new RoleManager();
                _employeeManager = new EmployeeManager();
                _roles = new List<string>();
                //Sets the parameters of the datepicker to auto select current date and not allow for selections past current date
                dateDOB.SelectedDate = DateTime.Now;
                dateDOB.DisplayDateEnd = DateTime.Now;

                //Generates combo box of US states 
                generateCBOStates(cboState);
                cboState.SelectedIndex = 0;

                //Generates a list box of possible employee roles
                foreach (var role in _roleManager.GetAllRoles())
                {
                    _roles.Add(role.Role_ID.ToString());
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

            string givenName = txtGivenName.Text;
            string familyName = txtFamilyName.Text;
            string address1 = txtAddress1.Text;
            string address2 = txtAddress2.Text ?? "";
            string city = txtCity.Text;
            string state = cboState.SelectedValue.ToString();
            string country = txtCountry.Text;
            string zip = txtZip.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;
            string position = txtPosition.Text;

            // Input validation checks
            if (!FormValidationHelper.IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email");
                return;
            }
            if (!FormValidationHelper.IsValidZipCode(zip))
            {
                MessageBox.Show("Please enter a valid zipcode");
                return;
            }
            if (!FormValidationHelper.IsValidPhoneNumber(phone))
            {
                MessageBox.Show("Please enter a valid telephone number");
                return;
            }

            //Checks required form inputs to ensure they are not empty or null
            if (string.IsNullOrWhiteSpace(givenName) ||
                string.IsNullOrWhiteSpace(familyName) ||
                string.IsNullOrWhiteSpace(address1) ||
                string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(state) ||
                string.IsNullOrWhiteSpace(country) ||
                string.IsNullOrWhiteSpace(zip) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(position))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }



            Employee_VM newEmployee = new Employee_VM()
            {

                Given_Name = givenName,
                Family_Name = familyName,
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
                employeeRoles.Add(new Role() { Role_ID = selectedItem.ToString() });
            }
            if (employeeRoles.Count == 0)
            {
                MessageBox.Show("Please select at least one employee role");
                return;
            }

            newEmployee.Employee_Roles = employeeRoles;

            //Inserts new Employee database record
            try
            {
                _employeeManager.AddEmployee(newEmployee);
                MessageBox.Show("Employee added successfully");
                clearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        //Clears form input fields
        private void clearInputFields()
        {
            txtGivenName.Text = "";
            txtFamilyName.Text = "";
            txtAddress1.Text = "";
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
    }
}
// Checked by Nathan Toothaker
