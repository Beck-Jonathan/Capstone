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

namespace NightRiderWPF.Clients
{
    /// <summary>
    /// Interaction logic for AdminViewClientDetail.xaml
    /// </summary>
    public partial class AdminViewClientDetail : Page
    {
        Client_VM _client = null;

        public AdminViewClientDetail(Client_VM client)
        {
            InitializeComponent();
            _client = client;
        }

        private void clearFieldsForReload()
        {
            txtUsername.Text = "";
            txtName.Text = "";
            txtDOB.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
        }

        private void populateFields()
        {
            txtUsername.Text = _client.Username; // this currently does not work as Client_VM is not getting username from anything
            string name = "";
            if (_client.MiddleName != null)
            {
                name = _client.GivenName + " " + _client.MiddleName + " " + _client.FamilyName;
            }
            else
            {
                name = _client.GivenName + " " + _client.FamilyName;
            }
            txtName.Text = name;
            txtDOB.Text = _client.DOB.ToShortDateString();
            txtEmail.Text = _client.Email;
            string voiceNum = "";
            if (_client.VoiceNumber != null)
            {
                voiceNum = _client.VoiceNumber;
            }
            txtPhone.Text = voiceNum;
            string address = "";
            string street = "";
            string city = "";
            string postal = "";
            if (_client.Address != null)
            {
                street = _client.Address;
            }
            if (_client.City != null)
            {
                city = _client.City;
            }
            if (_client.PostalCode != null)
            {
                postal = _client.PostalCode;
            }
            address = street + "\n" + city + " " + postal; // this is not ideal, the address/region/city/postal system in client should likely be reworked
            txtAddress.Text = address;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AdminViewClientList());
        }

        private void btnEditDetail_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AdminEditClient(_client));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            clearFieldsForReload();
            populateFields();
        }
        private void btnManageDependents_Click(object sender, RoutedEventArgs e)
        {
            GuardianViewDependentList guardianViewDependentList = new GuardianViewDependentList(_client);
            this.NavigationService.Navigate(guardianViewDependentList);
        }
    }
}
