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

namespace NightRiderWPF.Login
{
    /// <summary>
    /// Author:Parker Svoboda 
    /// Interaction logic for UsernameRequest.xaml
    /// </summary>
    /// <remarks>
    /// <br /><br />
    ///    AUTHOR: Parker Svoboda
    /// <br />
    ///    CREATED: 2024-02-25
    /// </remarks>
    public partial class UsernameRequestPage : Page
    {
        private ILoginManager _loginManager;
        private string _email;
        private string _response1;
        private string _response2;
        private string _response3;
        public UsernameRequestPage()
        {
            InitializeComponent();

            _loginManager = new LoginManager(new PasswordHasher());
        }



        private void btnQuestionRequest_Click(object sender, RoutedEventArgs e)
        {
            _email = txtEmail.Text;
            try
            {
                string[] questions = _loginManager.GetSecurityQuestionsforUsernameRetrieval(_email);
                if (questions != null)
                {
                    txtEmail.IsEnabled = false;
                    btnQuestionRequest.IsEnabled = false;

                    lblQuestion1.Visibility = Visibility.Visible;
                    lblQuestion1.Content = questions[0];
                    txtResponse1.Visibility = Visibility.Visible;
                    lblQuestion2.Visibility = Visibility.Visible;
                    lblQuestion2.Content = questions[1];
                    txtResponse2.Visibility = Visibility.Visible;
                    lblQuestion3.Visibility = Visibility.Visible;
                    lblQuestion3.Content = questions[2];
                    txtResponse3.Visibility = Visibility.Visible;
                    btnUsernameRequest.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Your Account Is Either Disabled\nOr Your Account Doesn't Have \nThe Provided Email Attached To It.", "Questions Were Not Found!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Username was found!", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUsernameRequest_Click(object sender, RoutedEventArgs e)
        {
            _email = txtEmail.Text;
            _response1 = txtResponse1.Text;
            _response2 = txtResponse2.Text;
            _response3 = txtResponse3.Text;

            try
            {
                string username = _loginManager.GetUsername(_email, _response1, _response2, _response3);
                if (username != null)
                {
                    lblEmail.Visibility = Visibility.Hidden;
                    txtEmail.Visibility = Visibility.Hidden;
                    btnQuestionRequest.Visibility = Visibility.Hidden;
                    lblQuestion1.Visibility = Visibility.Hidden;
                    txtResponse1.Visibility = Visibility.Hidden;
                    lblQuestion2.Visibility = Visibility.Hidden;
                    txtResponse2.Visibility = Visibility.Hidden;
                    lblQuestion3.Visibility = Visibility.Hidden;
                    txtResponse3.Visibility = Visibility.Hidden;
                    btnUsernameRequest.Visibility = Visibility.Hidden;
                    lblRequestSent.Content = username;
                    lblRequestSent.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Your Answers Were Not Correct.", "Invalid Answers", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Username was found!", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
