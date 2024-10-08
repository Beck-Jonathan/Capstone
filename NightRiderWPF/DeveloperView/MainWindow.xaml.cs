﻿using NightRiderWPF.DeveloperView;
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
using NightRiderWPF.PasswordReset;
using NightRiderWPF.PurchaseOrders;
using NightRiderWPF.VehicleModels;

namespace NightRiderWPF.DeveloperView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ILoginManager _loginManager;
        private IVehicleModelManager _vehicleModelManager;

        private IPasswordHasher _passwordHasher;
        private IVerificationCodeGenerator _verificationCodeGenerator;
		
        public MainWindow()
        {
            _passwordHasher = new PasswordHasher();
            _verificationCodeGenerator = new VerificationCodeGenerator();
            _loginManager = new LoginManager(_passwordHasher);
            _vehicleModelManager = new VehicleModelManager();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowNavItemsBasedOnRoles();
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
                    case "lstbxit_vehicleModels":
                        PageViewer.Navigate(new VehicleModelsListPage(_vehicleModelManager, null, null));
                        break;
                    case "VehicleLookupListPage":
                        PageViewer.Navigate(new VehicleLookupListPage());
                        break;
                    case "EmployeeProfilePage":
                        PageViewer.Navigate(new EmployeeProfilePage(Authentication.AuthenticatedEmployee));
                        break;
                    case "ViewWorkOrderPage":
                        PageViewer.Navigate(new ViewWorkOrderList());
                        break;
                    case "ClientPersonalPage":
                        PageViewer.Navigate(new ClientPersonalPage(Authentication.AuthenticatedClient));
                        break;
                    case "ClientLookupPage":
                        PageViewer.Navigate(new ClientLookupPage());
						break;
                    case "GuardianViewDependentList":
                        PageViewer.Navigate(new GuardianViewDependentList());
                        break;
                    case "AdminHome":
                        PageViewer.Navigate(new AdminHome());
                        break;
                    case "CreateRole":
                        PageViewer.Navigate(new RoleViews.CreateRole());
                        break;
                    case "ViewPurchaseOrders":
                        PageViewer.Navigate(new Parts_Person_View_Purchase_Orders());
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
            btnForgotPassword.Visibility = Visibility.Visible;

            lbl_userAuthenticatedConfirmation.Visibility = Visibility.Hidden;
            btn_logout.Visibility = Visibility.Hidden;

            ShowNavItemsBasedOnRoles();
        }

        /// <summary>
        ///     Builds the user authenticated label content
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-17
        /// </remarks>
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
                btnForgotPassword.Visibility = Visibility.Hidden;

                lbl_userAuthenticatedConfirmation.Content = BuildUserAuthenticatedConfirmationContent();
                lbl_userAuthenticatedConfirmation.Visibility = Visibility.Visible;

                btn_logout.Visibility = Visibility.Visible;

                ShowNavItemsBasedOnRoles();
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

        private void btnForgotUsername_Click(object sender, RoutedEventArgs e)
        {
            PageViewer.Navigate(new UsernameRequestPage());
		}
		
        /// <summary>
        ///     Handles click behavior for the "Forgot Password" button
        ///     open password reset request page
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        private void btnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            PageViewer.Navigate(new RequestAndVerifyPasswordResetCodePage(_passwordHasher, _verificationCodeGenerator));
        }

        /// <summary>
        ///     Shows/hides navigation menu items based on the currently authenticated user's roles
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-03-02
        /// </remarks>
        private void ShowNavItemsBasedOnRoles()
        {
            if (Authentication.AuthenticatedEmployee != null)
            {
                lstbxit_vehicleModels.Visibility = Visibility.Visible;
            }
            else
            {
                lstbxit_vehicleModels.Visibility = Visibility.Collapsed;
            }
        }
    }
}
