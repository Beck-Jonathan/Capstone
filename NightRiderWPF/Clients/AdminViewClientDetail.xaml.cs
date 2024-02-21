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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            txtUsername.Text = _client.Username; // this currently does not work as Client_VM is not getting username from anything
            String name = _client.GivenName + " " + _client.FamilyName;
            txtName.Text = name;
            txtDOB.Text = _client.DOB.ToShortDateString();
            txtEmail.Text = _client.Email;
            txtPhone.Text = _client.VoiceNumber;
            String address = _client.Address + "\n" + _client.City + " " + _client.PostalCode; // this is not ideal, the address/region/city/postal system in client should likely be reworked
            txtAddress.Text = address;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
