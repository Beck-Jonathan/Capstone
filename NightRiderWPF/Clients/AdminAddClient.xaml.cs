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
using DataObjects;
using LogicLayer;

namespace NightRiderWPF
{
    /// <summary>
    /// Interaction logic for AdminAddClient.xaml
    /// </summary>
    public partial class AdminAddClient : Page
    {
        
        ClientManager _clientManager = null;

        public AdminAddClient()
        {
            InitializeComponent();
            _clientManager = new ClientManager();
            clearInputFields();
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AdminViewClientList());
        }

        //Clears form input fields
        private void clearInputFields()
        {
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            txtCity.Text = "";
            txtEmail.Text = "";
            dateDOB.SelectedDate = DateTime.Now;
            dateDOB.DisplayDateEnd = DateTime.Now;
            txtVoiceNumber.Text = "";
            txtTextNumber.Text = "";
            txtStreet.Text = "";
            txtPostal.Text = "";
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string email = txtEmail.Text;
            string city = txtCity.Text;
            string voiceNumber = txtVoiceNumber.Text;
            string textNumber = txtTextNumber.Text;
            string address = txtStreet.Text;
            string postal = txtPostal.Text;

            // sets middle name value to empty string if nothing was entered
            string middleName = txtMiddleName.Text;
            if (string.IsNullOrEmpty(middleName))
            {
                middleName = "";
            }

            string dobText = dateDOB.Text;
            DateTime date = DateTime.Parse(dobText);

            // Input validation checks
            if (!ValidationHelpers.IsValidGivenName(firstName))
            {
                MessageBox.Show("Please enter a valid first name.");
                return;
            }
            if (!ValidationHelpers.IsValidFamilyName(lastName))
            {
                MessageBox.Show("Please enter a valid last name.");
                return;
            }
            if (!FormValidationHelper.IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email.");
                return;
            }
            if (!FormValidationHelper.IsValidPhoneNumber(voiceNumber))
            {
                MessageBox.Show("Please enter a valid telephone number.");
                return;
            }
            if (!FormValidationHelper.IsValidPhoneNumber(textNumber))
            {
                MessageBox.Show("Please enter a valid telephone number.");
                return;
            }
            if (!FormValidationHelper.IsValidZipCode(postal))
            {
                MessageBox.Show("Please enter a valid postal code.");
                return;
            }
            

            //Checks required form inputs to ensure they are not empty or null
            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(dobText) ||
                string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(textNumber) ||
                string.IsNullOrWhiteSpace(voiceNumber) ||
                string.IsNullOrWhiteSpace(address) ||
                    string.IsNullOrWhiteSpace(postal))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            Client_VM newClient = new Client_VM()
            {
                GivenName = firstName,
                MiddleName = middleName,
                FamilyName = lastName,
                DOB = date,
                Email = email,
                PostalCode = postal,
                City = city,
                Address = address,
                TextNumber = textNumber,
                VoiceNumber = voiceNumber,
                Region = "", // empty string for now
                IsActive = true // client is active upon creation
            };

            //Inserts new client database record
            try
            {
                _clientManager.AddClient(newClient);
                MessageBox.Show("Client added successfully");
                clearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                        "Failed to add client.", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
    }
}
