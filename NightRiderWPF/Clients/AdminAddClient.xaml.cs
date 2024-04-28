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
    /// <remarks>
    /// UPDATER: Michael Springer
    /// UPDATED: 2024-04-28
    ///     Allowed optional zip and phone to accept empty strings since nulls are converted
    /// </remarks>
    public partial class AdminAddClient : Page
    {
        
        ClientManager _clientManager = null;
        IEnumerable<Client> _clients = null;

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

            // sets nullable values to empty string if nothing was entered
            string middleName = txtMiddleName.Text;
            if (string.IsNullOrEmpty(middleName))
            {
                middleName = "";
            }
            string city = txtCity.Text;
            if(string.IsNullOrEmpty(city))
            {
                city = "";
            }
            string voiceNumber = txtVoiceNumber.Text;
            if(string.IsNullOrEmpty(voiceNumber))
            {
                voiceNumber = "";
            }
            string textNumber = txtTextNumber.Text;
            if (string.IsNullOrEmpty(textNumber))
            {
                textNumber = "";
            }
            string address = txtStreet.Text;
            if(string.IsNullOrEmpty(address))
            {
                address = "";
            }
            string postal = txtPostal.Text;
            if (string.IsNullOrEmpty(postal))
            {
                postal = "";
            }

            string dobText = dateDOB.Text;
            DateTime date = DateTime.Parse(dobText);

            //Checks required form inputs to ensure they are not empty or null
            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(dobText) ||
                string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

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
            if (!FormValidationHelper.IsValidPhoneNumber(voiceNumber) && voiceNumber != null && voiceNumber != "")
            {
                MessageBox.Show("Please enter a valid telephone number.");
                return;
            }
            if (!FormValidationHelper.IsValidPhoneNumber(textNumber) && textNumber != null && voiceNumber != "")
            {
                MessageBox.Show("Please enter a valid telephone number.");
                return;
            }
            if (!FormValidationHelper.IsValidZipCode(postal) && postal != null && postal != "")
            {
                MessageBox.Show("Please enter a valid postal code.");
                return;
            }

            // validation for fields with unique attribute in database
            _clients = _clientManager.GetAllClients();
            foreach(var client in _clients)
            {
                if (email == client.Email)
                {
                    MessageBox.Show(email + " is already in use. You cannot create a new client with this email.");
                    txtEmail.Text = "";
                } 
                else if(textNumber == client.TextNumber)
                {
                    MessageBox.Show(textNumber + " is already in use. You cannot create a new client with this text number.");
                    txtTextNumber.Text = "";
                }
                else if(voiceNumber == client.VoiceNumber)
                {
                    MessageBox.Show(voiceNumber + " voice number is already in use. You cannot create a new client with this voice number.");
                    txtVoiceNumber.Text = "";
                }
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
