using LogicLayer;
using LogicLayer.AppData;
using LogicLayer.Utilities;
using NightRiderWPF.Helpers;
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
    /// Interaction logic for RequestAndVerifyPasswordResetCodePage.xaml
    /// </summary>
    public partial class RequestAndVerifyPasswordResetCodePage : Page
    {
        private IPasswordHasher _passwordHasher;

        private PasswordResetManager _passwordResetManager;
        private LoginManager _loginManager;

        private string _username;
        private string _email;

        public RequestAndVerifyPasswordResetCodePage(
            IPasswordHasher passwordHasher,
            IVerificationCodeGenerator verificationCodeGenerator)
        {
            InitializeComponent();

            _passwordHasher = passwordHasher;
            _loginManager = new LoginManager(passwordHasher);
            _passwordResetManager = new PasswordResetManager(
                verificationCodeGenerator,
                Authentication.SecondsBeforePasswordResetExpiry);
        }

        /// <summary>
        ///     Handle create password reset button click event; verify the
        ///     username and email match; if they do match, begin the password
        ///     reset process; whether or not they match, change the UI to
        ///     reflect the new page state
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        private void btn_createPwdReset_Click(object sender, RoutedEventArgs e)
        {
            string retrievedEmail = null;
            _username = txtBx_username.Text;
            _email = txtBx_email.Text;

            try
            {
                retrievedEmail = _loginManager.GetLoginEmailByUsername(_username);

                if (retrievedEmail == _email)
                {
                    BeginPasswordResetProcess();
                    ShowVerificationCodeEntry();
                }
            }
            catch (ArgumentException)
            {
                ShowVerificationCodeEntry();
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred, please try again.");
            }
        }

        /// <summary>
        ///     Update the UI when a verification code is requested
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        private void ShowVerificationCodeEntry()
        {
            txtBx_email.IsReadOnly = true;
            txtBx_username.IsReadOnly = true;

            btn_createPwdReset.Content = "Resend Verification Code";

            lbl_verificationCode.Visibility = Visibility.Visible;
            txtBx_verificationCode.Visibility = Visibility.Visible;
            btn_verifyPasswordReset.Visibility = Visibility.Visible;
        }

        /// <summary>
        ///     Invoke password reset manager to begin password reset process,
        ///     create a verification code for the user to receive and fill in
        ///     for authentication purposes
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        private void BeginPasswordResetProcess()
        {
            _passwordResetManager.BeginPasswordReset(_username);
        }

        /// <summary>
        ///     Verify entered verification code; if verified, proceed to password
        ///     change; if not verified, inform user of error
        /// </summary>
        /// <remarks>
        ///    CONTRIBUTOR: Jared Hutton
        /// <br />
        ///    CREATED: 2024-02-24
        /// </remarks>
        private void btn_verifyPasswordReset_Click(object sender, RoutedEventArgs e)
        {
            string verificationCode = txtBx_verificationCode.Text;

            try
            {
                bool verified = _passwordResetManager.VerifyPasswordReset(_username, _email, verificationCode);

                if (verified)
                {
                    NavigationService.Navigate(new ChangePasswordPage(_passwordHasher, _username));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred, please try again.");
            }
        }
    }
}
