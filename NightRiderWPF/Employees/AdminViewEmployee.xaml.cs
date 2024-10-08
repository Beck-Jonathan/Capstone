﻿using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static DataObjects.FormValidationHelper;

namespace NightRiderWPF.Employees
{
    /// <summary>
    /// Interaction logic for AdminViewEmployee.xaml
    /// </summary>
    public partial class AdminViewEmployee : Page
    {
        EmployeeManager _employeeManager = null;
        Employee_VM _selected_employee = null;
        Employee_VM _updated_employee = null;
        IEnumerable<Role> _roles = null;

        //Created By: James Williams
        //Creation Date: 2024-02-17
        public AdminViewEmployee(EmployeeManager employeeManager, Employee_VM employee_VM)
        {
            _selected_employee = employee_VM;
            _employeeManager = employeeManager;

            InitializeComponent();
            //attempt to populate employee roles list
            try
            {
                _roles = _employeeManager.GetRolesByEmployeeID(_selected_employee.Employee_ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accessing employee information\n\n", ex.InnerException.Message);
            }
            generateView();
        }

        //Created By: James Williams
        //Creation Date: 2024-02-17
        //Description: Helper method that populates form data
        public void generateView()
        {
            txtGivenName.Text = _selected_employee.Given_Name;
            txtFamilyName.Text = _selected_employee.Family_Name;
            txtEmail.Text = _selected_employee.Email;
            txtDOB.Text = _selected_employee.DOB.ToString("MM-dd-yyyy");
            txtZip.Text = _selected_employee.Zip;
            txtAddress1.Text = _selected_employee.Address;
            txtAddress2.Text = _selected_employee.Address2;
            txtCity.Text = _selected_employee.City;
            if (_selected_employee.Country.ToUpper() == "USA" || _selected_employee.Country.ToUpper() == "US")
            {
                lblState.Visibility = Visibility.Visible;
                txtState.Visibility = Visibility.Visible;
                txtState.Text = _selected_employee.State;
            }
            else
            {
                lblState.Visibility = Visibility.Hidden;
                txtState.Visibility = Visibility.Hidden;
            }
            txtCountry.Text = _selected_employee.Country;
            txtState.Text = _selected_employee.State;
            txtPhone.Text = _selected_employee.Phone_Number;
            txtPosition.Text = _selected_employee.Position;
            lstRolesView.Items.Clear();
            btnEmployeeActivation.Visibility = Visibility.Hidden;
            if (_selected_employee.Is_Active)
            {
                btnEmployeeActivation.Content = "Deactivate Employee";
            }
            else
            {
                btnEmployeeActivation.Content = "Activate Employee";
            }

            foreach (var role in _roles)
            {
                lstRolesView.Items.Add(role.RoleID.ToString());
            }
        }
        //Created By: James Williams
        //Creation Date: 2024-02-17
        //Description: Back button click action. Returns to the previous page
        private void btnCancelView_Click(object sender, RoutedEventArgs e)
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
        //Created By: James Williams
        //Creation Date: 2024-02-17
        //Description:When Update button is clicked
        private void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {

            //Update UI from view-only to update
            txtGivenName.IsReadOnly = false;
            txtFamilyName.IsReadOnly = false;
            txtEmail.IsReadOnly = false;
            txtAddress1.IsReadOnly = false;
            txtAddress2.IsReadOnly = false;
            txtCity.IsReadOnly = false;
            txtZip.IsReadOnly = false;
            txtCountry.IsReadOnly = false;
            txtPhone.IsReadOnly = false;
            txtPosition.IsReadOnly = false;
            if (txtCountry.Text.ToUpper() == "USA" || txtCountry.Text.ToUpper() == "US")
            {
                cboState.Visibility = Visibility.Visible;
            }
            else
            {
                cboState.Visibility = Visibility.Hidden;
            }
            txtState.Visibility = Visibility.Hidden;
            generateCBOStates(cboState);
            cboState.SelectedValue = _selected_employee.State;
            dateDOB.Visibility = Visibility.Visible;
            dateDOB.Text = txtDOB.Text;
            txtDOB.Visibility = Visibility.Hidden;
            btnEmployeeActivation.Visibility = Visibility.Visible;
            if (cboState.SelectedItem != null)
            {
                _selected_employee.State = cboState.SelectedItem.ToString();
            }
            btnUpdateEmployeeSubmit.Visibility = Visibility.Visible;
            btnUpdateEmployee.Visibility = Visibility.Hidden;


        }
        //Created By: James Williams
        //Creation Date: 2024-02-17
        //Description:Submit update button event
        private void btnUpdateEmployeeSubmit_Click(object sender, RoutedEventArgs e)
        {


            //Input validation
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email");
                return;

            }
            else if (!IsValidPhoneNumber(txtPhone.Text))
            {
                MessageBox.Show("Please enter a valid phone number");
                return;

            }

            else if (string.IsNullOrWhiteSpace(txtGivenName.Text) ||
                string.IsNullOrWhiteSpace(txtFamilyName.Text) ||
                string.IsNullOrWhiteSpace(txtAddress1.Text) ||
                string.IsNullOrWhiteSpace(txtCity.Text) ||
                string.IsNullOrWhiteSpace(txtZip.Text) ||
                string.IsNullOrWhiteSpace(txtCountry.Text) ||
                string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                MessageBox.Show("Please complete required fields");
                return;
            }
            //if validation passes
            else
            {
                //create an updated employee record
                _updated_employee = new Employee_VM()
                {
                    Given_Name = txtGivenName.Text,
                    Family_Name = txtFamilyName.Text,
                    DOB = dateDOB.SelectedDate.Value,
                    Address = txtAddress1.Text,
                    Address2 = txtAddress2.Text,
                    City = txtCity.Text,
                    Zip = txtZip.Text,
                    Email = txtEmail.Text,

                    Country = txtCountry.Text,
                    Phone_Number = txtPhone.Text,
                    Position = txtPosition.Text
                };

                string state = "";
                if (txtCountry.Text.ToUpper() == "USA" || txtCountry.Text.ToUpper() == "US")
                {
                    state = cboState.SelectedValue.ToString().ToUpper();

                }
                _updated_employee.State = state;
                //attempt update
                int rows = 0;
                try
                {
                    rows = _employeeManager.EditEmployee(_updated_employee, _selected_employee);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating employee\n\n" + ex.InnerException.Message);
                }
                //update successfull
                if (rows > 0)
                {
                    //revert UI back to view only 
                    MessageBox.Show("Success!");
                    txtGivenName.IsReadOnly = true;
                    txtFamilyName.IsReadOnly = true;
                    txtEmail.IsReadOnly = true;
                    txtAddress1.IsReadOnly = true;
                    txtAddress2.IsReadOnly = true;
                    txtCity.IsReadOnly = true;
                    txtZip.IsReadOnly = true;
                    txtCountry.IsReadOnly = true;
                    txtPhone.IsReadOnly = true;
                    txtPosition.IsReadOnly = true;
                    cboState.Visibility = Visibility.Hidden;
                    txtState.Visibility = Visibility.Visible;
                    txtState.Text = _updated_employee.State;
                    dateDOB.Visibility = Visibility.Hidden;
                    txtDOB.Visibility = Visibility.Visible;
                    btnUpdateEmployeeSubmit.Visibility = Visibility.Hidden;
                    btnUpdateEmployee.Visibility = Visibility.Visible;
                    btnEmployeeActivation.Visibility = Visibility.Hidden;
                    _selected_employee = _updated_employee;
                    this.NavigationService.GoBack();

                }
            }

        }
        //Created By: James Williams
        //Creation Date: 2024-02-17
        //Description: Populates combobox of US state acronyms 
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

        //Created By: James Williams
        //Creation Date: 2024-02-23
        //Description: Employee activation controller
        private void btnEmployeeActivation_Click(object sender, RoutedEventArgs e)
        {
            string employeeStatus = _selected_employee.Is_Active ? "deactivate" : "reactivate";
            var result = MessageBox.Show("Are you sure you want to " + employeeStatus + " this employee's record?", "Confirm Update", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                try
                {
                    //Control to change if employee record is active
                    if (btnEmployeeActivation.Content.ToString() == "Activate Employee")
                    {

                        int rows = _employeeManager.ReactivateEmployeeByID(_selected_employee.Employee_ID);
                        if (rows == 0)
                        {
                            MessageBox.Show("Error reactivating employee");
                        }
                        else
                        {
                            MessageBox.Show("Employee reactivated successfully");
                            this.NavigationService.GoBack();
                        }
                    }
                    else
                    {
                        int rows = _employeeManager.DeactivateEmployeeByID(_selected_employee.Employee_ID);
                        if (rows == 0)
                        {
                            MessageBox.Show("Error deactivating employee");
                        }
                        else
                        {
                            MessageBox.Show("Employee deactivated successfully");
                            this.NavigationService.GoBack();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating employee record\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void txtCountry_KeyUp(object sender, KeyEventArgs e)
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

//Reviewed by Steven Sanchez
