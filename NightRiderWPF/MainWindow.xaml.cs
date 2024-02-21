using NightRiderWPF.DeveloperView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using LogicLayer.AppData;
using NightRiderWPF.Clients;
using NightRiderWPF.Vehicles;
using NightRiderWPF.WorkOrders;
using NightRiderWPF.Employees;
using NightRiderWPF.Inventory;
using NightRiderWPF.Login;
using LogicLayer;
using LogicLayer.Utilities;
using DataObjects;

namespace NightRiderWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ILoginManager _loginManager;
        private IPasswordHasher _passwordHasher;

        public MainWindow()
        {
            _passwordHasher = new PasswordHasher();
            _loginManager = new LoginManager(_passwordHasher);
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is ListBoxItem item)
            {
                switch (item.Name)
                {
                    case "SamplePage":
                        PageViewer.Navigate(new SamplePage());
                        break;
                    // Your cases here
                    case "AdminViewClientList":
                        PageViewer.Navigate(new AdminViewClientList());
                        break;
                    case "AdminCreateEmployeePage":
                        PageViewer.Navigate(new AdminCreateNewEmployee());
                        break;
                    case "PartsPersonViewParts":
                        PageViewer.Navigate(new PartsInventoryPage());
                        break;
                    case "AddDependentPage":
                        PageViewer.Navigate(new AddDependent());
                        break;
                    case "AdminEmployeeListPage":
                        PageViewer.Navigate(new AdminEmployeeListPage());
                        break;
                    case "VehicleLookupListPage":
                        PageViewer.Navigate(new VehicleLookupListPage());
                        break;
                    case "EmployeeProfilePage":
                        PageViewer.Navigate(new EmployeeProfilePage(Authentication.AuthenticatedEmployee));
                        break;
                    case "ViewWorkOrderPage":
                        PageViewer.Navigate(new ViewWorkOrderPage());
                        break;
                    case "ClientPersonalPage":
                        PageViewer.Navigate(new ClientPersonalPage(Authentication.AuthenticatedClient));
                        break;
                    case "ClientLookupPage":
                        PageViewer.Navigate(new ClientLookupPage());
                        break;
                }
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = pwdPassword.Password;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter a valid username.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter a valid password.");
                return;
            }

            try
            {
                var authenticatedEmployee = _loginManager.AuthenticateEmployee(username, password);

                Authentication.AuthenticatedEmployee = authenticatedEmployee;

                lblUsername.Visibility = Visibility.Hidden;
                txtUsername.Visibility = Visibility.Hidden;
                lblPassword.Visibility = Visibility.Hidden;
                pwdPassword.Visibility = Visibility.Hidden;
                btnLogin.Visibility = Visibility.Hidden;
                btnCreateAccount.Visibility = Visibility.Hidden;

                lbl_userAuthenticatedConfirmation.Content = BuildUserAuthenticatedConfirmationContent();
                lbl_userAuthenticatedConfirmation.Visibility = Visibility.Visible;

                btn_logout.Visibility = Visibility.Visible;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("No employee account with that username and password could be found.");
            }
        }

        private string BuildUserAuthenticatedConfirmationContent()
        {
            return $"Welcome, {Authentication.AuthenticatedEmployee.Given_Name}.";
        }
    }
}
