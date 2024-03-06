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

namespace NightRiderWPF.Clients
{
    /// <summary>
    /// Interaction logic for AdminEditClient.xaml
    /// </summary>
    public partial class AdminEditClient : Page
    {
        Client_VM _client = null;
        ClientManager _clientManager = null;
        IEnumerable<Client> _clients = null;


        public AdminEditClient(Client_VM client)
        {
            InitializeComponent();
            _client = client;
            _clientManager = new ClientManager();
            populateFields();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel editing this client? All of your changes will be lost.", "Cancel Editing", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                if (this.NavigationService.CanGoBack)
                {
                    this.NavigationService.GoBack();
                }
            }
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
            if (string.IsNullOrEmpty(city))
            {
                city = "";
            }
            string voiceNumber = txtVoiceNumber.Text;
            if (string.IsNullOrEmpty(voiceNumber))
            {
                voiceNumber = "";
            }
            string textNumber = txtTextNumber.Text;
            if (string.IsNullOrEmpty(textNumber))
            {
                textNumber = "";
            }
            string address = txtStreet.Text;
            if (string.IsNullOrEmpty(address))
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
            if (!FormValidationHelper.IsValidPhoneNumber(voiceNumber) && voiceNumber != null)
            {
                MessageBox.Show("Please enter a valid telephone number.");
                return;
            }
            if (!FormValidationHelper.IsValidPhoneNumber(textNumber) && textNumber != null)
            {
                MessageBox.Show("Please enter a valid telephone number.");
                return;
            }
            if (!FormValidationHelper.IsValidZipCode(postal) && postal != null)
            {
                MessageBox.Show("Please enter a valid postal code.");
                return;
            }
            if (!FormValidationHelper.IsValidCity(city) && city != null)
            {
                MessageBox.Show("Please enter a valid city.");
                return;
            }

            // validation for fields with unique attribute in database
            _clients = _clientManager.GetAllClients();
            foreach (var client in _clients)
            {
                if (email == client.Email && email != _client.Email)
                {
                    MessageBox.Show(email + " is already in use. You cannot create a new client with this email.");
                    txtEmail.Text = "";
                    return;
                }
                else if (textNumber == client.TextNumber && textNumber != _client.TextNumber)
                {
                    MessageBox.Show(textNumber + " is already in use. You cannot create a new client with this text number.");
                    txtTextNumber.Text = "";
                    return;
                }
                else if (voiceNumber == client.VoiceNumber && voiceNumber != _client.VoiceNumber)
                {
                    MessageBox.Show(voiceNumber + " voice number is already in use. You cannot create a new client with this voice number.");
                    txtVoiceNumber.Text = "";
                    return;
                }
            }

            // Tries to update the client record in the database
            try
            {
                _clientManager.EditClient(new Client_VM
                {
                    ClientID = _client.ClientID,
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
                    Region = _client.Region,
                    IsActive = _client.IsActive
                });
                MessageBox.Show("Client edited successfully");
                Client_VM updatedClient = null;
                updatedClient = _clientManager.GetClientById(_client.ClientID);
                this.NavigationService.Navigate(new AdminViewClientDetail(updatedClient));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                        "Failed to edit client.", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }


        private void populateFields()
        {
            try
            {
                txtFirstName.Text = _client.GivenName;
                if (_client.MiddleName != null)
                {
                    txtMiddleName.Text = _client.MiddleName;
                }
                txtLastName.Text = _client.FamilyName;
                if (_client.City != null)
                {
                    txtCity.Text = _client.City;
                }
                txtEmail.Text = _client.Email;
                dateDOB.SelectedDate = _client.DOB;
                dateDOB.DisplayDateEnd = DateTime.Now;
                if (_client.TextNumber != null)
                {
                    txtTextNumber.Text = _client.TextNumber;
                }
                if (_client.VoiceNumber != null)
                {
                    txtVoiceNumber.Text = _client.VoiceNumber;
                }
                if (_client.Address != null)
                {
                    txtStreet.Text = _client.Address;
                }
                if (_client.PostalCode != null)
                {
                    txtPostal.Text = _client.PostalCode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                        "Failed to get client data.", MessageBoxButton.OK, MessageBoxImage.Error);
                this.NavigationService.Navigate(new AdminViewClientDetail(_client));
            }

        }
    }
}
