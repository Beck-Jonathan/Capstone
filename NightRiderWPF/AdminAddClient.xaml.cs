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
        Client_VM _client = null;
        ClientManager _clientManager = null;

        public AdminAddClient()
        {
            InitializeComponent();
            _clientManager = new ClientManager();
            //Sets the parameters of the datepicker to auto select current date and not allow for selections past current date
            dateDOB.SelectedDate = DateTime.Now;
            dateDOB.DisplayDateEnd = DateTime.Now;
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFirstName.Text;
            string middleName = txtMiddleName.Text;
            string lastName = txtLastName.Text;
            string email = txtEmail.Text;
            string city = txtCity.Text;
            string voiceNumber = txtVoiceNumber.Text;
            string textNumber = txtTextNumber.Text;
            string address = txtStreet.Text;
            string postal = txtPostal.Text;


            //string givenName = txtGivenName.Text;
            //string familyName = txtFamilyName.Text;
            //string address1 = txtAddress1.Text;
            //string address2 = txtAddress2.Text ?? "";
            //string city = txtCity.Text;
            //string state = cboState.SelectedValue.ToString();
            //string country = txtCountry.Text;
            //string zip = txtZip.Text;
            //string phone = txtPhone.Text;
            //string email = txtEmail.Text;
            //string position = txtPosition.Text;

            //// Input validation checks
            //if (!FormValidationHelper.IsValidEmail(email))
            //{
            //    MessageBox.Show("Please enter a valid email");
            //    return;
            //}
            //if (!FormValidationHelper.IsValidZipCode(zip))
            //{
            //    MessageBox.Show("Please enter a valid zipcode");
            //    return;
            //}
            //if (!FormValidationHelper.IsValidPhoneNumber(phone))
            //{
            //    MessageBox.Show("Please enter a valid telephone number");
            //    return;
            //}

            ////Checks required form inputs to ensure they are not empty or null
            //if (string.IsNullOrWhiteSpace(givenName) ||
            //    string.IsNullOrWhiteSpace(familyName) ||
            //    string.IsNullOrWhiteSpace(address1) ||
            //    string.IsNullOrWhiteSpace(city) ||
            //    string.IsNullOrWhiteSpace(state) ||
            //    string.IsNullOrWhiteSpace(country) ||
            //    string.IsNullOrWhiteSpace(zip) ||
            //    string.IsNullOrWhiteSpace(phone) ||
            //    string.IsNullOrWhiteSpace(email) ||
            //    string.IsNullOrWhiteSpace(position))
            //{
            //    MessageBox.Show("Please fill in all required fields.");
            //    return;
            //}



            //Employee_VM newEmployee = new Employee_VM()
            //{

            //    Given_Name = givenName,
            //    Family_Name = familyName,
            //    Address = address1,
            //    Address2 = address2,
            //    City = city,
            //    State = state,
            //    Country = country,
            //    Zip = zip,
            //    Phone_Number = phone,
            //    Email = email,
            //    Position = position
            //};

        }
    }
}
