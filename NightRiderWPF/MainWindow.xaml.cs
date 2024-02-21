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
using DataObjects;
using LogicLayer;
using LogicLayer.Utilities;

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
                }
            }
        }

        /// <summary>
        ///     updates UI during log out
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton, Parker Svoboda
        /// <br />
        ///    CREATED: 2024-02-17
        /// </remarks>
        private void UpdateUIforLogout()
        {
            btnLogin.IsDefault = true;

            txtUsername.Text = "";
            pwdPassword.Password = "";
            txtUsername.Visibility = Visibility.Visible;
            pwdPassword.Visibility = Visibility.Visible;
            lblUsername.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;
            btnLogin.Visibility = Visibility.Visible;
            btnCreateAccount.Visibility = Visibility.Visible;

            lbl_userAuthenticatedConfirmation.Visibility = Visibility.Hidden;
            btn_logout.Visibility = Visibility.Hidden;
        }

        /// <summary>
        ///     Builds the user authenticated label content
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-17
        /// </remarks>
        private string BuildUserAuthenticatedConfirmationContent()
        {
            return $"Welcome, {Authentication.AuthenticatedEmployee.Given_Name}.";
        }

        /// <summary>
        ///     Handles click events for the logout button;
        ///     logout user and purges the active session for security
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton, Parker Svoboda
        /// <br />
        ///    CREATED: 2024-02-20
        /// </remarks>
        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            Authentication.AuthenticatedEmployee = null; // for security purposes
            Authentication.AuthenticatedClient = null; // more security
            UpdateUIforLogout();
            return;
        }
    }
}
