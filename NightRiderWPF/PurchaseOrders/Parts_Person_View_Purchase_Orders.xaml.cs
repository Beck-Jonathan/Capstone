using DataObjects;
using LogicLayer;
using NightRiderWPF.PurchsaeOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
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
    /// Interaction logic for Parts_Person_View_Purchase_Orders.xaml
    /// </summary>
    public partial class Parts_Person_View_Purchase_Orders : Page
    {
        List<Purchase_OrderVM> purchase_OrderVMs;
        List<dynamic> Filteredpurchase_OrderVMs;
        List<dynamic> dataObjects;
        IPurchase_OrderManager purchase_OrderManager;
        
        bool loaded;
        bool pending;


        /// <summary>
        /// Initial Version : Jonathan Beck 2/24/2024
        /// </summary>
        public Parts_Person_View_Purchase_Orders()
        {
            loaded = false;
            pending = false;
            InitializeComponent();
            dpkEndDate.SelectedDate = DateTime.Now;
            dpkStartDate.SelectedDate = new DateTime(2021, 01, 01);

        }

        /// <summary>
        /// Initial Version : Jonathan Beck 2/24/2024
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            purchase_OrderManager = new PurchaseOrderManager();
            loaded = true;
            dataObjects = new List<dynamic>();
            GetOrders();

        }
        /// <summary>
        /// Initial Version : Jonathan Beck 2/24/2024
        /// Passes a Purchase Order object to the view details page
        /// </summary>

        private void btnViewOrder_Click(object sender, RoutedEventArgs e)
        {
            if (datPurchaseOrders.SelectedItems.Count != 0)
            {
                NavigationWindow navigationWindow = new NavigationWindow();

                var clicked = datPurchaseOrders.SelectedItem;
                var nameOfProperty = "PropertyOne";
                var propertyInfo = clicked.GetType().GetProperty(nameOfProperty);
                int value = Int32.Parse(propertyInfo.GetValue(clicked, null).ToString());
                Purchase_OrderVM passed = returnPurchaseOrder(value);

                NavigationService.Navigate(new Parts_Person_View_Current_PO_Details(passed));


            }
            else { MessageBox.Show("Pick something, please"); }
        }
        /// <summary>
        /// Initial Version : Jonathan Beck 2/24/2024 
        /// Grabs a purcahse order by its ID to get ready to pass to the next page.
        /// </summary>
        private Purchase_OrderVM returnPurchaseOrder(int purchaseID)
        {
            foreach (Purchase_OrderVM Po in purchase_OrderVMs)
            {
                if (Po.Purchase_Order_ID == purchaseID) return Po;
            }
            return null;

        }
        /// <summary>
        /// Initial Version : Jonathan Beck 2/24/2024
        /// On date changed, checks that start date is before end date, then 
        /// sends a query to the db to get all appropraite purchase orders.
        /// If dates are backwards, colors the bankground pink
        /// </summary>

        private void dpkStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {


            if (loaded)
            {
                DateTime start = (DateTime)dpkStartDate.SelectedDate;
                DateTime end = (DateTime)dpkEndDate.SelectedDate;
                if (start < end)
                {
                    GetOrders();
                    dpkEndDate.Background = Brushes.Transparent;
                    dpkStartDate.Background = Brushes.Transparent;
                }
                else
                {
                    dpkEndDate.Background = Brushes.DeepPink;
                    dpkStartDate.Background = Brushes.DeepPink;
                }
            }
        }

        /// <summary>
        /// Initial Version : Jonathan Beck 2/24/2024
        /// Gets orders within a date range, turnst hem into dynamics, and populates the data grid
        /// </summary>
        private void GetOrders()
        {
            cbxFilter.Items.Clear();
            cbxFilter.SelectedItem = null;
            DateTime start = (DateTime)dpkStartDate.SelectedDate;
            DateTime end = (DateTime)dpkEndDate.SelectedDate;
            if (purchase_OrderVMs != null && purchase_OrderVMs.Count > 0)
            {
                purchase_OrderVMs.Clear();
            }
            purchase_OrderVMs = new List<Purchase_OrderVM>();
            purchase_OrderVMs = purchase_OrderManager.LookupPurchaseOrderByDateRange(start, end);
            //et all the purchase order VM and put them in a list
            try
            {
                purchase_OrderVMs = purchase_OrderManager.LookupPurchaseOrderByDateRange(start, end);

            }
            catch (Exception)
            {

                MessageBox.Show("unable to retreive purchase orders");
            }
            dataObjects.Clear();

            //make a new list of "dynamic" objects, which you can freely define
            //List<dynamic> dataObjects = new List<dynamic>();
            //loop through the purchase order vm, and make a dynamic object for each
            foreach (Purchase_OrderVM purchase in purchase_OrderVMs)
            {
                string POnumber = purchase.Purchase_Order_ID.ToString();
                string vendor = purchase.vendor.Vendor_Name;
                DateTime creationDate = purchase.Purchase_Order_Date;
                int itemCount = 0;
                foreach (POLineItemVM pOLineItem in purchase.pOLineItems)
                {
                    itemCount += pOLineItem.Quantity;
                }
                decimal totalPrice = 0;
                foreach (POLineItemVM pOLineItem in purchase.pOLineItems)
                {
                    totalPrice += pOLineItem.Price * pOLineItem.Quantity;
                }
                string printedPrice = "$" + Math.Round(totalPrice, 2).ToString();
                bool fulfilled = false;
                //make the button, set a few properties
                Button button = new Button();
                button.Height = 30;
                button.Width = 30;

                button.Content = POnumber;
                //event handler for mouse click on button
                button.Click += (s, ex) =>
                {
                    NavigationWindow navigationWindow = new NavigationWindow();
                    NavigationService.Navigate(new Parts_Person_View_Current_PO_Details(purchase));
                };
                //create the new dynamic object, by defining properties one throgh five. 
                //I beleive this can have over 30 properties, maybe unlimited?
                dynamic myDynamic = new
                {
                    PropertyOne = POnumber,
                    PropertyTwo = vendor,
                    PropertyThree = creationDate,
                    PropertyFour = fulfilled,
                    PropertyFive = itemCount,
                    PropertySix = button,
                    PropertySevem = printedPrice

                };
                //add the data object to the list of data objects

                dataObjects.Add(myDynamic);
                if (!cbxFilter.Items.Contains(vendor))
                {
                    cbxFilter.Items.Add(vendor);
                }



            }

            //set the items source to the list of dynamics
            datPurchaseOrders.ItemsSource = null;
            datPurchaseOrders.ItemsSource = dataObjects;
            organizeDataGrid();

            lblCount.Content = "There are " + dataObjects.Count + " Purchase Orders";
        }

        /// <summary>
        /// Initial Version : Jonathan Beck 2/24/2024 : When the filter is changed, 
        /// filters down to the matching vendor
        /// </summary>
        private void cbxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            datPurchaseOrders.ItemsSource = dataObjects;
            List<dynamic> Filteredpurchase_OrderVMs = new List<dynamic>();

            if (cbxFilter.SelectedItem != null)
            {
                String selectedVendor = cbxFilter.SelectedItem.ToString();

                foreach (dynamic x in datPurchaseOrders.Items)
                {
                    var vendorName = x.GetType().GetProperty("PropertyTwo");
                    String value = vendorName.GetValue(x, null).ToString();
                    if (value == selectedVendor)
                    {
                        Filteredpurchase_OrderVMs.Add(x);
                    }
                }

                datPurchaseOrders.ItemsSource = null;
                datPurchaseOrders.ItemsSource = Filteredpurchase_OrderVMs;
            }
            organizeDataGrid();



        }
        /// <summary>
        /// Initial Version : Jonathan Beck 2/24/2024
        /// Sets headers and display order
        /// </summary>
        private void organizeDataGrid()
        {
            if (dataObjects.Count > 0)
            {
                datPurchaseOrders.Columns[0].DisplayIndex = 5;
                datPurchaseOrders.Columns[0].Header = "View Details";
                datPurchaseOrders.Columns[1].Header = "PO Number";
                datPurchaseOrders.Columns[2].Header = "Vendor Name";
                datPurchaseOrders.Columns[3].Header = "Date Order Placed";
                datPurchaseOrders.Columns[5].Header = "Item Count";
                datPurchaseOrders.Columns[4].Header = "Recieved";
                datPurchaseOrders.Columns[4].DisplayIndex = 0;
                datPurchaseOrders.Columns[7].Header = "Total";

                datPurchaseOrders.Columns.RemoveAt(6);

            }

            else
            {
                MessageBox.Show("No Orders Found");
            }



        }
        /// <summary>
        /// Initial Version : Jonathan Beck 2/24/2024
        /// Go back
        /// </summary>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
        /// <summary>
        /// Initial Version : Jonathan Beck 3/19/2024
        /// Go to the add purchase order window
        /// </summary>

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Parts_Person_Add_Update_Delete_Purchase_Order());
        }
    }

}

