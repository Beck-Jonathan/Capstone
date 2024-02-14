using DataObjects;
using LogicLayer;
using LogicLayer.AppData;
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
using System.Windows.Shapes;

namespace NightRiderWPF
{
    /// <summary>
    /// AUTHOR: Jared Hutton
    /// <br />
    /// CREATED: 2024-02-01
    /// <br />
    ///     Page on which a user provides authentication information
    /// </summary>
    public partial class LoginPage : Page 
    {
        private ILoginManager _loginManager;

        private string[] _securityQuestions = null;

        /// <summary>
        ///     Instantiates a LoginPage object
        /// </summary>
        /// <returns>
        ///    <see cref="LoginPage">LoginPage</see>: A LoginPage object
        /// </returns>
        /// <remarks>
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
       public LoginPage()
        {
            InitializeComponent();

            _loginManager = new LoginManager(new PasswordHasher());
        }

        /// <summary>
        ///     Instantiates a LoginPage object
        /// </summary>
        /// <param name="passwordHasher">
        ///    The utility used to hash passwords
        /// </param>
        /// <returns>
        ///    <see cref="LoginPage">LoginPage</see>: A LoginPage object
        /// </returns>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="IPasswordHasher">IPasswordHasher</see> passwordHasher: The utility used to hash passwords
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
       public LoginPage(IPasswordHasher passwordHasher)
        {
            InitializeComponent();

            _loginManager = new LoginManager(passwordHasher);
        }

        /// <summary>
        ///     Event handler for the login button
        ///     <br />
        ///     When security questions have not been loaded, loads the security questions. If security questions
        ///     are null, attempts final authentication. When security questions have been loaded, attempts final
        ///     authentication.
        /// </summary>
        /// <remarks>
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        private void btn_authenticate_Click(object sender, RoutedEventArgs e)
        {
            string username = txtBx_username.Text;
            string password = pwdBx_password.Password;

            if (_securityQuestions == null)
            {
                try
                {
                    _securityQuestions = _loginManager.AuthenticateEmployeeForSecurityQuestions(username, password);

                    if (_securityQuestions.Any(question => !string.IsNullOrWhiteSpace(question)))
                    {
                        lbl_securityQuestion1.Content = _securityQuestions[0];
                        lbl_securityQuestion1.Visibility = Visibility.Visible;
                        txtBx_securityResponse1.Visibility = Visibility.Visible;

                        if (!string.IsNullOrWhiteSpace(_securityQuestions[1]))
                        {
                            lbl_securityQuestion2.Content = _securityQuestions[1];
                            lbl_securityQuestion2.Visibility = Visibility.Visible;
                            txtBx_securityResponse2.Visibility = Visibility.Visible;
                        }

                        if (!string.IsNullOrWhiteSpace(_securityQuestions[2]))
                        {
                            lbl_securityQuestion3.Content = _securityQuestions[2];
                            lbl_securityQuestion3.Visibility = Visibility.Visible;
                            txtBx_securityResponse3.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        CompleteAuthentication(username, password);
                    }
                }
                catch (ArgumentException ex)
                {
                    ShowLoginSuccessErrorMessage(false);
                }
            }
            else
            {
                string securityResponse1 = txtBx_securityResponse1.Text;
                string securityResponse2 = txtBx_securityResponse2.Text;
                string securityResponse3 = txtBx_securityResponse3.Text;

                try
                {
                    CompleteAuthentication(
                        username,
                        password,
                        securityResponse1,
                        securityResponse2,
                        securityResponse3);
                }
                catch (ArgumentException ex)
                {
                    ShowLoginSuccessErrorMessage(false);
                }
            }
        }

        /// <summary>
        ///     Attempts authentication using username, password, and security responses; sets the system
        ///     authenticated user if authentication is successful
        /// </summary>
        /// <param name="username">
        ///    The username of the user attempting to login
        /// </param>
        /// <param name="password">
        ///    The password of the user attempting to login
        /// </param>
        /// <param name="securityResponse1">
        ///    The response to the first security question
        /// </param>
        /// <param name="securityResponse2">
        ///    The response to the second security question
        /// </param>
        /// <param name="securityResponse3">
        ///    The response to the third security question
        /// </param>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="string">string</see> username: The username given by the user
        /// <br />
        ///    <see cref="string">string</see> password: The password given by the user
        /// <br />
        ///    <see cref="string">string</see> securityResponse1: The response to the first security question
        /// <br />
        ///    <see cref="string">string</see> securityResponse2: The response to the second security question
        /// <br />
        ///    <see cref="string">string</see> securityResponse3: The response to the third security question
        /// <br /><br />
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        private void CompleteAuthentication(
            string username,
            string password,
            string securityResponse1 = null,
            string securityResponse2 = null,
            string securityResponse3 = null)
        {
            try
            {
                var employee = _loginManager.AuthenticateEmployeeWithSecurityResponses(
                    username,
                    password,
                    securityResponse1,
                    securityResponse2,
                    securityResponse3);

                ShowLoginSuccessErrorMessage(true);

                Authentication.AuthenticatedEmployee = employee;
            }
            catch (Exception ex)
            {
                ShowLoginSuccessErrorMessage(false);
            }
        }

        /// <summary>
        ///     Update login success/error message based on whether login succeeded or failed
        /// </summary>
        /// <param name="isSuccess">
        ///    Whether the login succeeded
        /// </param>
        /// <remarks>
        ///    Parameters:
        /// <br />
        ///    <see cref="bool">bool</see> isSuccess: Whether the login succeeded
        /// <br /><br />
        ///    AUTHOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-01
        /// </remarks>
        private void ShowLoginSuccessErrorMessage(bool isSuccess)
        {
            if (isSuccess)
            {
                lbl_successErrorMessage.Foreground = Brushes.Black;
                lbl_successErrorMessage.Content = "Login succeeded";
            }
            else
            {
                lbl_successErrorMessage.Foreground = Brushes.Red;
                lbl_successErrorMessage.Content = "Login failed, please try again";
            }
        }
    }
}
