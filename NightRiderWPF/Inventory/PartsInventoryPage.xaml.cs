/// <summary>
/// Jonathan Beck
/// Created: 2024/02/01
/// 
/// XAML to display a all records of Parts_Inventory. 
/// This will be updated to allow Adding and editing in a future sprint.
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd 
using DataAccessFakes;
using DataObjects;
using LogicLayer;
using LogicLayer.AppData;
using NightRiderWPF.PurchaseOrders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace NightRiderWPF.Inventory
{
    /// <summary>
    /// Interaction logic for PartsInventoryPage.xaml
    /// </summary>
    public partial class PartsInventoryPage : Page
    {
        //create the list for all parts inventory, the manager
        // a list of dynamics which will be used to store the parts in a user readable form
        //a dictionary to link the id from the dictionary to the part id in the main list of parts
        // and a boolean "loaded" that allows me to delay certain functions from running before the page is ready
        //updated by Jonathan Beck 4/1/2023
        List<Parts_Inventory> all_parts;
        Parts_InventoryManager inventoryManager = null;
        List<dynamic> displayParts = new List<dynamic>(); // TEST THIS static
        Dictionary<int, Parts_Inventory> IDNamePairs = new Dictionary<int, Parts_Inventory>();  // TEST THIS static
        bool loaded;

        public PartsInventoryPage()
        {
            displayParts = new List<dynamic>();
            IDNamePairs = new Dictionary<int, Parts_Inventory>();
            loaded = false;
            InitializeComponent();
            List<String> options = new List<String> {
                    "Show all",
                    "On Hand = 0",
                    "On Hand > 0",
                    "# Ordered = 0",
                    "# Ordered > 0",
                    "Stock Level = 0",
                    "Stock Level > 0"
         };
            cbxFilter.ItemsSource = options;
            cbxFilter.SelectedIndex = 0;
        }

        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/02/25
        /// 
        /// Naviate to the view purchase orders page

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Parts_Person_View_Purchase_Orders());
        }

        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/02/01
        /// 
        /// On Page load, use the manager and get all records, then toss them on a data grid.
        /// </summary>
        /// <throws>Argument Exception</throws>
        /// <remarks>
        /// Updater Name: Max Fare
        /// Updated: 2024-03-26
        /// updated to hide the add part button based on user's role
        /// </remarks>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            loaded = false;

            if(Authentication.HasRole("Admin") 
                || Authentication.HasRole("PartsPerson"))
            {
                btnAddPart.Visibility = Visibility.Visible;
            }
            else
            {
                btnAddPart.Visibility = Visibility.Hidden;
            }


            inventoryManager = new Parts_InventoryManager();
            
            //call accessor to get all parts
            try
            {
                all_parts = inventoryManager.GetActiveParts_Inventory();
                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Uanble to load inventory" + ex.Message);
            }
            //create a list of dynamic objects to display in the datagrid
            if (all_parts != null)
            {
                searchParts();
                if (displayParts.Count > 0)
                {
                    fixDataGrid();
                }
            }
            else { NavigationService.StopLoading();
                tbxParts_InventorySearch.IsEnabled = false;
            }
        }
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/02/01
        /// When a part record is double clicked
        /// Passes Selected record to the AddEditDeleteParts_Inventory Window
        /// </summary>
        /// <throws>Argument Exception</throws>
        /// <remarks>
        /// Updater Name: 
        /// Updated: yyyy/mm/dd 

        private void datParts_Inventory_MouseDoubleClick(object sender, MouseButtonEventArgs e)

        {


            if (datParts_Inventory.SelectedItems.Count != 0)
            {
                var _part = datParts_Inventory.SelectedItem;
                //Property Two holds the part ID number
                var nameOfProperty = "PropertyTwo";
                var propertyInfo = _part.GetType().GetProperty(nameOfProperty);
                int value = Int32.Parse(propertyInfo.GetValue(_part, null).ToString());
                Parts_Inventory passed = returnPart(value);
                if (passed != null)
                {
                    NavigationService.Navigate(new AddUpdateDeleteParts_Inventory(passed));
                }
                else
                {
                    MessageBox.Show("unable to dispaly part details");
                }

            }
            else { MessageBox.Show("Pick something, please"); }
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
        /// Created: 2024/02/12
        /// When the audit button is clicked, passes the part_inventory to the audit window
        /// 
        /// </summary>
        /// <throws>Argument Exception</throws>
        /// <remarks>
        /// Updater Name: 
        /// Updated: yyyy/mm/dd 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (datParts_Inventory.SelectedItems.Count != 0)
            {
                var _part = datParts_Inventory.SelectedItem;
                var nameOfProperty = "PropertyTwo";
                var propertyInfo = _part.GetType().GetProperty(nameOfProperty);
                int value = Int32.Parse(propertyInfo.GetValue(_part, null).ToString());
                Parts_Inventory passed = returnPart(value);
                if (passed != null)
                {
                    //Link to Max's InventoryAudit Page
                    this.NavigationService.Navigate(new InventoryAudit(passed));
                }
                else
                {
                    MessageBox.Show("unable to open audit window");
                }
            }
            else { MessageBox.Show("Pick something, please"); }

        }
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/02/01
        /// Search by part by it's id
        /// </summary>
        /// <throws>Argument Exception</throws>
        /// <remarks>
        /// Updater Name: 
        /// Updated: yyyy/mm/dd 
        private Parts_Inventory returnPart(int partid)
        {
            
            return IDNamePairs[partid];
        }

        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/02/01
        /// On Key up in the search box, filter the parts invnetory
        /// in an appropaite manner.
        /// </summary>
        /// <throws>Argument Exception</throws>
        /// <remarks>
        /// Updater Name: 
        /// Updated: yyyy/mm/dd 

        private void tbxParts_InventorySearch_KeyUp(object sender, KeyEventArgs e)
        {

            applyComboBox();
            
        }

        //navigate ot hte view vendors page
        private void btnViewVendors_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Vendors.ViewAllVendors());
        }
        // Fixes the headers of the data grid
        private void fixDataGrid() {

            if (displayParts.Count > 0)
            {
                datParts_Inventory.ItemsSource = displayParts;
                datParts_Inventory.Columns[0].DisplayIndex = 5;
                datParts_Inventory.Columns[1].Header = "Part Name";
                datParts_Inventory.Columns[2].Header = "Part Number";
                datParts_Inventory.Columns[3].Header = "On Hand Quantity";
                datParts_Inventory.Columns[4].Header = "# Ordered";
                datParts_Inventory.Columns[5].Header = "Stock Level";
                datParts_Inventory.Columns[0].Header = "Audit";
            }
        }

        //when the combo box is changed, runs the apply combo box method to filter
        private void cbxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            applyComboBox();
        }

        //searches the parts name nad id number for the entered field in the search box
        //Jonathan Beck 4/1/2023
        public void searchParts()
        {
            if (loaded)
            {
                if (tbxParts_InventorySearch.Text == "")
                {
                    displayParts.Clear();
                    foreach (Parts_Inventory _part in all_parts)
                    {
                        IDNamePairs[_part.Parts_Inventory_ID] = _part;
                        dynamic part = new
                        {
                            PropertyOne = _part.Part_Name,
                            PropertyTwo = _part.Parts_Inventory_ID.ToString(),
                            PropertyThree = _part.Part_Quantity.ToString(),
                            PropertyFour = _part.Ordered_Qty.ToString(),
                            PropertyFive = _part.Stock_Level.ToString()
                        };
                        displayParts.Add(part);
                    }
                }
                else {
                    displayParts.Clear();
                    foreach (Parts_Inventory _part in all_parts)
                    {
                        if ((_part.Part_Name.ToLower().Contains(tbxParts_InventorySearch.Text.ToLower()) || _part.Parts_Inventory_ID.ToString().Contains(tbxParts_InventorySearch.Text.ToLower())))
                        {
                            IDNamePairs[_part.Parts_Inventory_ID] = _part;
                            dynamic part = new
                            {
                                PropertyOne = _part.Part_Name,
                                PropertyTwo = _part.Parts_Inventory_ID.ToString(),
                                PropertyThree = _part.Part_Quantity.ToString(),
                                PropertyFour = _part.Ordered_Qty.ToString(),
                                PropertyFive = _part.Stock_Level.ToString()
                            };
                            displayParts.Add(part);
                        }
                    }
                }
                    datParts_Inventory.ItemsSource = null;
                    datParts_Inventory.ItemsSource = displayParts;
                    fixDataGrid();
                }
            
        }

        //Filters by on hand. 
        // Parameter int x - 0 if we are searching for stuff with 0 on hand, or 1 if we
        //are loking for stuff with 1 or more on hand
        //Jonathan Beck, 4/1/2023
        private void filterOnHand(int x)
        {
            displayParts.Clear();
            foreach (Parts_Inventory _part in all_parts)
                if (x == 0)
                {
                    {
                        if ((_part.Part_Name.ToLower().Contains(tbxParts_InventorySearch.Text.ToLower()) || _part.Parts_Inventory_ID.ToString().Contains(tbxParts_InventorySearch.Text.ToLower())) && _part.Part_Quantity == 0)
                        {
                            dynamic part = new
                            {
                                PropertyOne = _part.Part_Name,
                                PropertyTwo = _part.Parts_Inventory_ID.ToString(),
                                PropertyThree = _part.Part_Quantity.ToString(),
                                PropertyFour = _part.Ordered_Qty.ToString(),
                                PropertyFive = _part.Stock_Level.ToString()
                            };
                            displayParts.Add(part);
                        }
                    }
                }
                else { 
                    
                        if ((_part.Part_Name.ToLower().Contains(tbxParts_InventorySearch.Text.ToLower()) || _part.Parts_Inventory_ID.ToString().Contains(tbxParts_InventorySearch.Text.ToLower())) && _part.Part_Quantity > 0)
                        {
                            dynamic part = new
                            {
                                PropertyOne = _part.Part_Name,
                                PropertyTwo = _part.Parts_Inventory_ID.ToString(),
                                PropertyThree = _part.Part_Quantity.ToString(),
                                PropertyFour = _part.Ordered_Qty.ToString(),
                                PropertyFive = _part.Stock_Level.ToString()
                            };
                            displayParts.Add(part);
                        }
                    
                }
            datParts_Inventory.ItemsSource = null;
            datParts_Inventory.ItemsSource = displayParts;
            fixDataGrid();
        }
        //Filters by ordered. 
        // Parameter int x - 0 if we are searching for stuff with 0 ordered, or 1 if we
        //are loking for stuff with 1 or more ordered
        //Jonathan Beck, 4/1/2023
        private void filterOrdered(int x)
        {
            displayParts.Clear();
            foreach (Parts_Inventory _part in all_parts)
                if (x == 0)
                {
                    {
                        if ((_part.Part_Name.ToLower().Contains(tbxParts_InventorySearch.Text.ToLower()) || _part.Parts_Inventory_ID.ToString().Contains(tbxParts_InventorySearch.Text.ToLower())) && _part.Ordered_Qty == 0)
                        {
                            dynamic part = new
                            {
                                PropertyOne = _part.Part_Name,
                                PropertyTwo = _part.Parts_Inventory_ID.ToString(),
                                PropertyThree = _part.Part_Quantity.ToString(),
                                PropertyFour = _part.Ordered_Qty.ToString(),
                                PropertyFive = _part.Stock_Level.ToString()
                            };
                            displayParts.Add(part);
                        }
                    }
                }
                else
                {
                    {
                        if ((_part.Part_Name.ToLower().Contains(tbxParts_InventorySearch.Text.ToLower()) || _part.Parts_Inventory_ID.ToString().Contains(tbxParts_InventorySearch.Text.ToLower())) && _part.Ordered_Qty > 0)
                        {
                            dynamic part = new
                            {
                                PropertyOne = _part.Part_Name,
                                PropertyTwo = _part.Parts_Inventory_ID.ToString(),
                                PropertyThree = _part.Part_Quantity.ToString(),
                                PropertyFour = _part.Ordered_Qty.ToString(),
                                PropertyFive = _part.Stock_Level.ToString()
                            };
                            displayParts.Add(part);
                        }
                    }
                }
            datParts_Inventory.ItemsSource = null;
            datParts_Inventory.ItemsSource = displayParts;
            fixDataGrid();
        }
        //Filters by in stock. 
        // Parameter int x - 0 if we are searching for stuff with 0 in stock, or 1 if we
        //are loking for stuff with 1 or more in stock
        //Jonathan Beck, 4/1/2023
        private void filterStock(int x)
        {
            displayParts.Clear();
            foreach (Parts_Inventory _part in all_parts)
                if (x == 0)
                {
                    {
                        if ((_part.Part_Name.ToLower().Contains(tbxParts_InventorySearch.Text.ToLower()) || _part.Parts_Inventory_ID.ToString().Contains(tbxParts_InventorySearch.Text.ToLower())) && _part.Stock_Level == 0)
                        {

                            dynamic part = new
                            {
                                PropertyOne = _part.Part_Name,
                                PropertyTwo = _part.Parts_Inventory_ID.ToString(),
                                PropertyThree = _part.Part_Quantity.ToString(),
                                PropertyFour = _part.Ordered_Qty.ToString(),
                                PropertyFive = _part.Stock_Level.ToString()
                            };
                            displayParts.Add(part);
                        }
                    }
                }
                else
                {
                    {
                        if ((_part.Part_Name.ToLower().Contains(tbxParts_InventorySearch.Text.ToLower()) || _part.Parts_Inventory_ID.ToString().Contains(tbxParts_InventorySearch.Text.ToLower())) && _part.Stock_Level > 0)
                        {
                            dynamic part = new
                            {
                                PropertyOne = _part.Part_Name,
                                PropertyTwo = _part.Parts_Inventory_ID.ToString(),
                                PropertyThree = _part.Part_Quantity.ToString(),
                                PropertyFour = _part.Ordered_Qty.ToString(),
                                PropertyFive = _part.Stock_Level.ToString()
                            };
                            displayParts.Add(part);
                        }
                    }
                }
            datParts_Inventory.ItemsSource = null;
            datParts_Inventory.ItemsSource = displayParts;
            fixDataGrid();
        }

        //Reads the combo box value and runs the appropriate combo box filter
        //fires on change of combo box.
        public void applyComboBox() {
            String choice = cbxFilter.SelectedItem as String;
            switch (choice)
            {
                case "Show all": searchParts(); fixDataGrid(); break;
                case "On Hand = 0": filterOnHand(0); break;
                case "On Hand > 0": filterOnHand(1); break;
                case "# Ordered = 0": filterOrdered(0); break;
                case "# Ordered > 0": filterOrdered(1); break;
                case "Stock Level = 0": filterStock(0); break;
                case "Stock Level > 0": filterStock(1); break; 
            }
        }

        private void btnAddPart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUpdateDeleteParts_Inventory());
        }
    }
}
