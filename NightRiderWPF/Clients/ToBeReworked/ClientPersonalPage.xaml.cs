using DataObjects;
using LogicLayer;
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
using static System.Net.Mime.MediaTypeNames;

namespace NightRiderWPF
{
    /// <summary>
    /// Interaction logic for ClientPersonalPage.xaml
    /// </summary>
    public partial class ClientPersonalPage : Page
    {
        private Client_VM client_VM = new Client_VM();
        private IClientManager _clientManager = null;
        public ClientPersonalPage()
        {
            InitializeComponent();
        }

        public ClientPersonalPage(Client_VM c)
        {
            client_VM = c;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (client_VM != null)
                {
                    _clientManager = new ClientManager();

                    txtFirstName.Text = client_VM.GivenName;
                    txtLastName.Text = client_VM.FamilyName;
                    txtMiddleName.Text = client_VM.MiddleName;
                    txtEmail.Text = client_VM.Email;
                    txtAddress.Text = client_VM.Address;
                    txtCity.Text = client_VM.City;
                    txtPostalCode.Text = client_VM.PostalCode;
                    txtTextNumber.Text = client_VM.TextNumber;
                    txtVoiceNumber.Text = client_VM.VoiceNumber;
                }
                else
                {
                    MessageBox.Show("Client not found.", "No data to show.",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);

            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (client_VM != null)
                {
                    client_VM.GivenName = txtFirstName.Text;
                    client_VM.FamilyName = txtLastName.Text;
                    client_VM.MiddleName = txtMiddleName.Text;
                    client_VM.Email = txtEmail.Text;
                    client_VM.Address = txtAddress.Text;
                    client_VM.City = txtCity.Text;
                    client_VM.PostalCode = txtPostalCode.Text;
                    client_VM.TextNumber = txtTextNumber.Text;
                    client_VM.VoiceNumber = txtVoiceNumber.Text;

                    _clientManager.EditClient(client_VM);
                    lblSuccessErrorMessage.Content = "Successfully updated profile";
                }
                else
                {
                    MessageBox.Show("You cant update a non existent Account.", "No data to show.",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                lblSuccessErrorMessage.Content = ex.Message;
            }
        }
    }
}
