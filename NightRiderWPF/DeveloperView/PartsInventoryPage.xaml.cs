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
using System;
using System.Collections.Generic;
using System.Data;
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

namespace NightRiderWPF.DeveloperView
{
    /// <summary>
    /// Interaction logic for PartsInventoryPage.xaml
    /// </summary>
    public partial class PartsInventoryPage : Page
    {
        List<Parts_Inventory> all_parts;
        Parts_InventoryManager inventoryManager = null;
        static List<dynamic> displayParts;
        public PartsInventoryPage()
        {
            InitializeComponent();
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
        /// Updated: yyyy/mm/dd 
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            inventoryManager = new Parts_InventoryManager();
            List<dynamic> displayParts = new List<dynamic>();
            //call accessor to get all parts
            try
            {
                all_parts = inventoryManager.GetActiveParts_Inventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Uanble to load inventory" + ex.Message);
            }
            //create a list of dynamic objects to display in the datagrid
            if (all_parts != null)
            {
                foreach (Parts_Inventory _part in all_parts)
                {
                    string partname = _part.Part_Name;
                    string partnumber = _part.Parts_Inventory_ID.ToString();
                    string onHand = _part.Part_Quantity.ToString();
                    string noOrdered = _part.Ordered_Qty.ToString();
                    string stockLevel = _part.Stock_Level.ToString();
                    dynamic part = new
                    {
                        PropertyOne = partname,
                        PropertyTwo = partnumber,
                        PropertyThree = onHand,
                        PropertyFour = noOrdered,
                        PropertyFive = stockLevel
                    };

                    displayParts.Add(part);


                }

                datParts_Inventory.ItemsSource = displayParts;
                datParts_Inventory.Columns[0].DisplayIndex = 5;

                datParts_Inventory.Columns[1].Header = "Part Name";
                datParts_Inventory.Columns[2].Header = "Part Number";
                datParts_Inventory.Columns[3].Header = "On Hand Quantity";
                datParts_Inventory.Columns[4].Header = "# Ordered";
                datParts_Inventory.Columns[5].Header = "Stock Level";
                datParts_Inventory.Columns[0].Header = "Audit";

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
                    NavigationService.Navigate(new AddUpdateDeleteParts_Invnetory(passed));
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
                    //Link to Max's window

                    InventoryAudit win2 = new InventoryAudit(passed);
                    bool AuditResult =(bool)win2.ShowDialog();
                    if (AuditResult) {
                        NavigationService.Refresh();
                    }
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
            foreach (Parts_Inventory Part in all_parts)
            {
                if (Part.Parts_Inventory_ID == partid) return Part;
            }
            return null;
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
            if (tbxParts_InventorySearch.Text == "")
            {
                //remake the list same as above
                List<dynamic> displayParts = new List<dynamic>();
                foreach (Parts_Inventory _part in all_parts)
                {
                    string partname = _part.Part_Name;
                    string partnumber = _part.Parts_Inventory_ID.ToString();
                    string onHand = _part.Part_Quantity.ToString();
                    string noOrdered = _part.Ordered_Qty.ToString();
                    string stockLevel = _part.Stock_Level.ToString();
                    dynamic part = new
                    {
                        PropertyOne = partname,
                        PropertyTwo = partnumber,
                        PropertyThree = onHand,
                        PropertyFour = noOrdered,
                        PropertyFive = stockLevel
                    };

                    displayParts.Add(part);


                }
                datParts_Inventory.ItemsSource = displayParts;
                datParts_Inventory.Columns[0].DisplayIndex = 5;
                
                datParts_Inventory.Columns[1].Header = "Part Name";
                datParts_Inventory.Columns[2].Header = "Part Number";
                datParts_Inventory.Columns[3].Header = "On Hand Quantity";
                datParts_Inventory.Columns[4].Header = "# Ordered";
                datParts_Inventory.Columns[5].Header = "Stock Level";

            }
            else
            {
                //make a new list based the search box
                List<dynamic> displayParts = new List<dynamic>();
                foreach (Parts_Inventory _part in all_parts)

                {
                    if (_part.Part_Name.ToLower().Contains(tbxParts_InventorySearch.Text.ToLower()))
                    {
                        string partname = _part.Part_Name;
                        string partnumber = _part.Parts_Inventory_ID.ToString();
                        string onHand = _part.Part_Quantity.ToString();
                        string noOrdered = _part.Ordered_Qty.ToString();
                        string stockLevel = _part.Stock_Level.ToString();
                        dynamic part = new
                        {
                            PropertyOne = partname,
                            PropertyTwo = partnumber,
                            PropertyThree = onHand,
                            PropertyFour = noOrdered,
                            PropertyFive = stockLevel
                        };

                        displayParts.Add(part);
                    }

                }
                datParts_Inventory.ItemsSource = displayParts;
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
        }
    }
}
