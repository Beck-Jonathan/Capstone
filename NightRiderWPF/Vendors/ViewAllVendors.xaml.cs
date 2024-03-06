using DataObjects;
using LogicLayer;
using LogicLayer.Vendor;
using NightRiderWPF.Inventory;
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
    /// Interaction logic for ViewAllVendors.xaml
    /// </summary>
    public partial class ViewAllVendors : Page
    {
        List<VendorVM> all_vendors;
        VendorManager VendorManager = null;
        static List<dynamic> displayVendors;
        public ViewAllVendors()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/03/003
        /// 
        /// On Page load, use the manager and get all records, then toss them on a data grid.
        /// </summary>
        /// <throws>Argument Exception</throws>
        /// <remarks>
        /// Updater Name: Max Fare
        /// Updated: yyyy/mm/dd 
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            VendorManager = new VendorManager();
            List<dynamic> displayVendors = new List<dynamic>();
            //call accessor to get all parts
            try
            {
                all_vendors = VendorManager.getAllVendors();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Uanble to load vendors" + ex.Message);
            }
            //create a list of dynamic objects to display in the datagrid
            if (all_vendors != null)
            {
                foreach (VendorVM _vendor in all_vendors)
                {
                    string vendorName = _vendor.Vendor_Name;
                    string vendorContact = _vendor.Vendor_Contact_Given_Name + " " + _vendor.Vendor_Contact_Family_Name;
                    string vendorEmail = _vendor.Vendor_Contact_Email;
                    string vendorPhone = _vendor.Vendor_Phone_Number;
                    string vendorCity = _vendor.Vendor_City;
                    string vendorState = _vendor.Vendor_State;
                    dynamic vendor = new
                    {
                        PropertyOne = vendorName,
                        PropertyTwo = vendorContact,
                        PropertyThree = vendorEmail,
                        PropertyFour = vendorPhone,
                        PropertyFive = vendorCity,
                        PropertySix = vendorState
                    };

                    displayVendors.Add(vendor);


                }
                if (displayVendors.Count > 0)
                {
                    datVendors.ItemsSource = displayVendors;
                    datVendors.Columns[0].DisplayIndex = 6;

                    datVendors.Columns[1].Header = "Vendor Name";
                    datVendors.Columns[2].Header = "Contact Name";
                    datVendors.Columns[3].Header = "Email";
                    datVendors.Columns[4].Header = "Phone";
                    datVendors.Columns[5].Header = "City";
                    datVendors.Columns[6].Header = "State";
                    datVendors.Columns[0].Header = "Details";
                }

            }
            else
            {
                NavigationService.StopLoading();
                tbxVendorSearch.IsEnabled = false;
            }


        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/03/04
        /// When the details button is clicked, passes the part_inventory to the details window
        /// 
        /// </summary>
        /// <throws>Argument Exception</throws>
        /// <remarks>
        /// Updater Name: 
        /// Updated: yyyy/mm/dd 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (datVendors.SelectedItems.Count != 0)
            {



                var _vendor = datVendors.SelectedItem;
                var nameOfProperty = "PropertyOne";
                var propertyInfo = _vendor.GetType().GetProperty(nameOfProperty);
                String value = propertyInfo.GetValue(_vendor, null).ToString();
                Vendor passed = returnVendor(value);
                if (passed != null)
                {

                    NavigationService.Navigate(new AddUpdateDeleteVendor(passed));


                }
                else
                {
                    MessageBox.Show("unable to open details window");
                }
            }
            else { MessageBox.Show("Pick something, please"); }

        }
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/02/01
        /// Search for Vendor by it's name
        /// </summary>
        /// <throws>Argument Exception</throws>
        /// <remarks>
        /// Updater Name: 
        /// Updated: yyyy/mm/dd 
        private Vendor returnVendor(String vendorName)
        {
            foreach (Vendor _vendor in all_vendors)
            {
                if (_vendor.Vendor_Name == vendorName) return _vendor;
            }
            return null;
        }

        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/03/04
        /// On Key up in the search box, filter the parts Vendors. Searches for vendor name or state
        /// in an appropaite manner.
        /// </summary>
        /// <throws>Argument Exception</throws>
        /// <remarks>
        /// Updater Name: 
        /// Updated: yyyy/mm/dd 

        private void tbxVendorSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbxVendorSearch.Text == "")
            {
                //remake the list same as above
                List<dynamic> displayVendors = new List<dynamic>();
                foreach (VendorVM _vendor in all_vendors)
                {
                    string vendorName = _vendor.Vendor_Name;
                    string vendorContact = _vendor.Vendor_Contact_Given_Name + " " + _vendor.Vendor_Contact_Family_Name;
                    string vendorEmail = _vendor.Vendor_Contact_Email;
                    string vendorPhone = _vendor.Vendor_Phone_Number;
                    string vendorCity = _vendor.Vendor_City;
                    string vendorState = _vendor.Vendor_State;
                    dynamic vendor = new
                    {
                        PropertyOne = vendorName,
                        PropertyTwo = vendorContact,
                        PropertyThree = vendorEmail,
                        PropertyFour = vendorPhone,
                        PropertyFive = vendorCity,
                        PropertySix = vendorState
                    };

                    displayVendors.Add(vendor);


                }
                if (displayVendors.Count > 0)
                {
                    datVendors.ItemsSource = displayVendors;
                    datVendors.Columns[0].DisplayIndex = 6;

                    datVendors.Columns[1].Header = "Vendor Name";
                    datVendors.Columns[2].Header = "Contact Name";
                    datVendors.Columns[3].Header = "Email";
                    datVendors.Columns[4].Header = "Phone";
                    datVendors.Columns[5].Header = "City";
                    datVendors.Columns[6].Header = "State";
                    datVendors.Columns[0].Header = "Details";
                }
            }
            else
            {
                //make a new list based the search box
                List<dynamic> displayVendors = new List<dynamic>();
                foreach (Vendor _vendor in all_vendors)

                {
                    if (_vendor.Vendor_Name.ToLower().Contains(tbxVendorSearch.Text.ToLower())
                        || _vendor.Vendor_Contact_Given_Name.ToLower().Contains(tbxVendorSearch.Text.ToLower())
                        || _vendor.Vendor_Contact_Family_Name.ToLower().Contains(tbxVendorSearch.Text.ToLower())
                        || _vendor.Vendor_Contact_Email.ToLower().Contains(tbxVendorSearch.Text.ToLower())
                        || _vendor.Vendor_City.ToLower().Contains(tbxVendorSearch.Text.ToLower())
                        || _vendor.Vendor_Contact_Phone_Number.ToLower().Contains(tbxVendorSearch.Text.ToLower())

                        || _vendor.Vendor_State.ToLower().Contains(tbxVendorSearch.Text.ToLower()))
                    {
                        string vendorName = _vendor.Vendor_Name;
                        string vendorContact = _vendor.Vendor_Contact_Given_Name + " " + _vendor.Vendor_Contact_Family_Name;
                        string vendorEmail = _vendor.Vendor_Contact_Email;
                        string vendorPhone = _vendor.Vendor_Phone_Number;
                        string vendorCity = _vendor.Vendor_City;
                        string vendorState = _vendor.Vendor_State;
                        dynamic vendor = new
                        {
                            PropertyOne = vendorName,
                            PropertyTwo = vendorContact,
                            PropertyThree = vendorEmail,
                            PropertyFour = vendorPhone,
                            PropertyFive = vendorCity,
                            PropertySix = vendorState
                        };

                        displayVendors.Add(vendor);
                    }

                }
                datVendors.ItemsSource = displayVendors;
                if (displayVendors.Count > 0)
                {
                    datVendors.ItemsSource = displayVendors;
                    datVendors.Columns[0].DisplayIndex = 6;

                    datVendors.Columns[1].Header = "Vendor Name";
                    datVendors.Columns[2].Header = "Contact Name";
                    datVendors.Columns[3].Header = "Email";
                    datVendors.Columns[4].Header = "Phone";
                    datVendors.Columns[5].Header = "City";
                    datVendors.Columns[6].Header = "State";
                    datVendors.Columns[0].Header = "Details";
                }

            }
        }


    }
}
