using DataObjects;
using LogicLayer.Vendor;
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

namespace NightRiderWPF.PurchaseOrders
{
    /// <summary>
    /// Interaction logic for Parts_Person_Add_Update_Delete_Purchase_Order.xaml
    /// Initial version created by Jonathan Beck
    /// Date : 3/18/2024
    /// </summary>
    ///
    public partial class Parts_Person_Add_Update_Delete_Purchase_Order : Page
    {
        /// <summary>
        /// Initial version created by Jonathan Beck
        /// Creates a vendor manager and fills the combo box.
        /// Date : 3/18/2024
        /// </summary>
        Dictionary<String, int> vendors;
        List<VendorVM> allVendor;
        private void fillComboBox()
        {
            VendorManager vendorManager = new VendorManager();
            try
            {
                allVendor = vendorManager.getAllVendors();
                if (allVendor.Count == 0)
                {
                    throw new Exception("Can't Load Vendors");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                NavigationService.GoBack();
            }

            vendors = new Dictionary<String, int>();
            List<String> vendorNames = new List<string>();
            foreach (VendorVM vendor in allVendor)
            {
                vendors.Add(vendor.Vendor_Name, vendor.Vendor_ID);
                vendorNames.Add(vendor.Vendor_Name);
            }
            List<String> countries = new List<string>();
            countries.Add("USA");
            cbxPurchase_OrderVendor_ID.ItemsSource = vendorNames;
            cbxPurchase_OrderDelivery_Country.ItemsSource = countries;
            List<String> states = ValidationHelpers.generateCBOStates();
            cboPurchase_OrderDelivery_State.ItemsSource = states;

        }
        //VendorManager
        public Parts_Person_Add_Update_Delete_Purchase_Order()
        {
            InitializeComponent();
            fillComboBox();
        }
        /// <summary>
        /// Validates the input and moves the user to the create line items screen
        /// Initial version created by Jonathan Beck
        /// Date : 3/18/2024
        /// </summary>
        private void btnAddPurchase_Order_Click(object sender, RoutedEventArgs e)
        {
            if (tbxPurchase_OrderDelivery_Address.Text.isNotEmptyOrNull()
                && tbxPurchase_OrderDelivery_Address.Text.Length < 100
                && tbxPurchase_OrderDelivery_Address.Text.Length > 3

                && tbxPurchase_OrderDelivery_City.Text.isNotEmptyOrNull()
                && cboPurchase_OrderDelivery_State.Text.isNotEmptyOrNull()
                && tbxPurchase_OrderDelivery_Zip.Text.isValidZip()
                && cbxPurchase_OrderVendor_ID.Text.isNotEmptyOrNull()
                && cbxPurchase_OrderDelivery_Country.Text.isNotEmptyOrNull()
                && dpkPurchase_OrderPurchase_Order_Date.Text.isNotEmptyOrNull()
                )
            {
                Purchase_OrderVM passed = new Purchase_OrderVM();
                passed.Delivery_Address = tbxPurchase_OrderDelivery_Address.Text;
                passed.Delivery_Address2 = tbxPurchase_OrderDelivery_Address2.Text;
                passed.Delivery_City = tbxPurchase_OrderDelivery_City.Text;
                passed.Delivery_State = cboPurchase_OrderDelivery_State.Text;
                passed.Delivery_Zip = tbxPurchase_OrderDelivery_Zip.Text;
                passed.Delivery_Country = cbxPurchase_OrderDelivery_Country.Text;
                passed.Purchase_Order_Date = DateTime.Parse(dpkPurchase_OrderPurchase_Order_Date.Text);
                passed.Vendor_ID = vendors[cbxPurchase_OrderVendor_ID.Text];

                NavigationService.Navigate(new Parts_Person_Add_Update_Delete_Line_Items(passed));




            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
