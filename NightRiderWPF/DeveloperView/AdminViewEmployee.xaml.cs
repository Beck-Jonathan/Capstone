using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace NightRiderWPF.DeveloperView
{
    /// <summary>
    /// Interaction logic for AdminViewEmployee.xaml
    /// </summary>
    public partial class AdminViewEmployee : Page
    {
        EmployeeManager _employeeManager = null;
        Employee_VM _selected_employee = null;
        IEnumerable<Role> _roles = null;
        public AdminViewEmployee(EmployeeManager employeeManager, Employee_VM employee_VM)
        {
            _selected_employee = employee_VM;
            _employeeManager = employeeManager;
            
            InitializeComponent();
            try
            {
                _roles = _employeeManager.GetRolesByEmployeeID(_selected_employee.Employee_ID);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error accessing employee information\n\n", ex.InnerException.Message);
            }
            generateView();
        }

        //Helper method that populates form data
        public void generateView()
        {
            txtGivenName.Text = _selected_employee.Given_Name;
            txtFamilyName.Text = _selected_employee.Family_Name;
            txtEmail.Text = _selected_employee.Email;
            txtZip.Text = _selected_employee.Zip;
            txtAddress1.Text = _selected_employee.Address;
            txtAddress2.Text = _selected_employee.Address2;
            txtCity.Text = _selected_employee.City;
            txtCountry.Text = _selected_employee.Country;
            txtState.Text = _selected_employee.State;
            txtPhone.Text = _selected_employee.Phone_Number;
            txtPosition.Text = _selected_employee.Position;
            lstRolesView.Items.Clear();
            foreach(var role in _roles)
            {
                lstRolesView.Items.Add(role.RoleID.ToString());
            }
        }

        //Back button click action. Returns to the previous page
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

    }
}
//Reviewed by Steven Sanchez
