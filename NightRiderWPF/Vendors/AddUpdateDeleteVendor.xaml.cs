using DataObjects;
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

namespace NightRiderWPF.Vendors
{
    /// <summary>
    /// Interaction logic for AddUpdateDeleteVendor.xaml
    /// Created By Jonathan Beck 3-3-2024
    /// </summary>
    public partial class AddUpdateDeleteVendor : Page
    {
        Vendor _vendor;
        /// <summary>
        /// fills in text boxes with values from passed vendor object
        /// Created By Jonathan Beck 3-3-2024
        /// </summary>
        public AddUpdateDeleteVendor(Vendor _passed)
        {
            
            InitializeComponent();
            _vendor = _passed;
            string secondLine = "";
            if (_passed.Vendor_Address2 != "") { secondLine = "\n"; }
            string fulladdress = _passed.Vendor_Address + "\n" + _passed.Vendor_Address2 + secondLine + _passed.Vendor_City + "," + _passed.Vendor_State + " " + _passed.Vendor_Zip + " " + _passed.Vendor_Country;
            toggleVisability(0);



            lblVendorVendor_Name.Content = _vendor.Vendor_Name;
            tbxVendorVendor_Contact_Given_Name.Text = _vendor.Vendor_Contact_Given_Name;
            tbxVendorVendor_Contact_Family_Name.Text = _vendor.Vendor_Contact_Family_Name;
            tbxVendorVendor_Contact_Phone_Number.Text = _vendor.Vendor_Contact_Phone_Number;
            tbxVendorVendor_Contact_Email.Text = _vendor.Vendor_Contact_Email;
            tbxVendorVendor_Phone_Number.Text = _vendor.Vendor_Phone_Number;
            tbxVendorVendor_AddressFull.Text = fulladdress;
            tbxVendorVendor_Address.Text = _vendor.Vendor_Address;
            tbxVendorVendor_Address2.Text = _vendor.Vendor_Address2;
            tbxVendorVendor_City.Text = _vendor.Vendor_City;
            tbxVendorVendor_State.Text = _vendor.Vendor_State;
            tbxVendorVendor_Country.Text = _vendor.Vendor_Country;
            tbxVendorVendor_Zip.Text = _vendor.Vendor_Zip;
            chkVendorIs_Active.IsChecked = _vendor.Is_Active;
            chkVendorPreferred.IsChecked = _vendor.Preferred;
            lblVendorIs_Active.Visibility = Visibility.Hidden;
            chkVendorIs_Active.Visibility = Visibility.Hidden;

        }
        /// <summary>
        /// Switches the vissability of all individual address fields (Add1, Add2, city, sTate, Zip, country) and a combined all in one field. 
        /// The all in one field is better for viewing normally, while the separate fields are needed for editing,
        /// Created By Jonathan Beck 3-3-2024
        /// </summary>
        private void toggleVisability(int mode)
        {
            if (mode == 1)
            {
                tbxVendorVendor_Address.Visibility = Visibility.Hidden;
                lblVendorVendor_Address.Visibility = Visibility.Hidden;

                tbxVendorVendor_Address2.Visibility = Visibility.Hidden;
                lblVendorVendor_Address2.Visibility = Visibility.Hidden;

                tbxVendorVendor_City.Visibility = Visibility.Hidden;
                lblVendorVendor_City.Visibility = Visibility.Hidden;

                tbxVendorVendor_State.Visibility = Visibility.Hidden;
                lblVendorVendor_State.Visibility = Visibility.Hidden;

                tbxVendorVendor_Country.Visibility = Visibility.Hidden;
                lblVendorVendor_Country.Visibility = Visibility.Hidden;

                tbxVendorVendor_Zip.Visibility = Visibility.Hidden;
                lblVendorVendor_Zip.Visibility = Visibility.Hidden;

                tbxVendorVendor_AddressFull.Visibility = Visibility.Visible;
                lblVendorVendor_Address.Visibility = Visibility.Visible;


            }
            else
            {
                tbxVendorVendor_Address.Visibility = Visibility.Visible;
                lblVendorVendor_Address.Visibility = Visibility.Visible;

                tbxVendorVendor_Address2.Visibility = Visibility.Visible;
                lblVendorVendor_Address2.Visibility = Visibility.Visible;

                tbxVendorVendor_City.Visibility = Visibility.Visible;
                lblVendorVendor_City.Visibility = Visibility.Visible;

                tbxVendorVendor_State.Visibility = Visibility.Visible;
                lblVendorVendor_State.Visibility = Visibility.Visible;

                tbxVendorVendor_Country.Visibility = Visibility.Visible;
                lblVendorVendor_Country.Visibility = Visibility.Visible;

                tbxVendorVendor_Zip.Visibility = Visibility.Visible;
                lblVendorVendor_Zip.Visibility = Visibility.Visible;

                tbxVendorVendor_AddressFull.Visibility = Visibility.Hidden;
                lblVendorVendor_Address.Visibility = Visibility.Visible;


            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack) { 
            NavigationService.GoBack();
            }
        }
    }
}
