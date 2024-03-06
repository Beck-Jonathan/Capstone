using DataObjects;
using LogicLayer;
using LogicLayer.Utilities;
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

namespace NightRiderWPF.PasswordReset
{
    /// <summary>
    /// Interaction logic for ChangePasswordPage.xaml
    /// </summary>
    public partial class ChangePasswordPage : Page
    {
        private LoginManager _loginManager;

        private string _username;

        public ChangePasswordPage(IPasswordHasher passwordHasher, string username)
        {
            InitializeComponent();

            _loginManager = new LoginManager(passwordHasher);
            _username = username;
        }

        /// <summary>
        ///     Change user password; on success, return to main window;
        ///     on fail, inform user of failure
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        private void btn_changePassword_Click(object sender, RoutedEventArgs e)
        {
            string password = pwdBx_password.Password;
            string confirmPassword = pwdBx_confirmPassword.Password;

            // requires 8 chars, 1 upper, 1 lower, 1 number, 1 special
            if (!ValidationHelpers.IsValidPassword(password))
            {
                MessageBox.Show("A password must be at least 8 characters long with at least one" +
                    " uppercase character, one lowercase character, one number, and one special character.");
            }
            else if (password !=  confirmPassword)
            {
                MessageBox.Show("Both passwords must match.");
            }
            else
            {
                try
                {
                    _loginManager.EditLoginPassword(_username, password);

                    btn_changePassword.IsEnabled = false;

                    MessageBox.Show("Password changed successfully! Please attempt to log in again.");

                    NavigationService.Navigate(null);
                }
                catch (Exception)
                {
                    MessageBox.Show("An error occurred. Please try again.");
                }
            }
        }
    }
}
