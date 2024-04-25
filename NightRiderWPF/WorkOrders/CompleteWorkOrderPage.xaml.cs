using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CompleteWorkOrderPage.xaml
    /// </summary>
    public partial class CompleteWorkOrderPage : Page
    {
        IServiceOrderManager _serviceOrderManager = null;
        ServiceOrder_VM _serviceOrder = null;
        List<ServiceOrderLineItems> _serviceOrderLineItems = null;
        Parts_InventoryManager _inventoryManager = null;
        List<Parts_Inventory> _allInventoryList = null;
        List<Parts_Inventory> _usedInventoryList = new List<Parts_Inventory>();

        public CompleteWorkOrderPage(ServiceOrder_VM serviceOrder)
        {
            try
            {
                InitializeComponent();
                _serviceOrderManager = new ServiceOrderManager();
                _serviceOrder = _serviceOrderManager.SelectServiceOrderByServiceOrderID(serviceOrder.Service_Order_ID);
                _serviceOrderLineItems = _serviceOrder.serviceOrderLineItems;
                _inventoryManager = new Parts_InventoryManager();
                _allInventoryList = _inventoryManager.GetActiveParts_Inventory();
                _usedInventoryList = new List<Parts_Inventory>();
            } catch (Exception ex)
            {
                MessageBox.Show("Error Occurred: " + ex.ToString());
                return;
            }
            
        }

        private void updateProductListBtn_Click(object sender, RoutedEventArgs e)
        {
            int quantity = -1;
            bool add = false;
            try
            {
                quantity = Int32.Parse(quantityTxtBox.Text);
                if(quantity <= 0)
                {
                    MessageBox.Show("Quantity must be greater than zero");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Quantity must be a number");
                return;
            }
            if(productCmbBox.SelectedItem != null && !quantityTxtBox.Text.Equals(""))
            {
                int id = Int32.Parse(productCmbBox.SelectedItem.ToString().Split('|')[0].Trim());
                string partName = productCmbBox.SelectedItem.ToString().Split('|')[1].Trim();

                Parts_Inventory usedPart = _inventoryManager.GetParts_InventoryByID(id);
                usedPart.Part_Quantity = quantity;
                if (_usedInventoryList.Count == 0)
                {
                    Parts_Inventory tempUsedPart = _inventoryManager.GetParts_InventoryByID(id);
                    tempUsedPart.Part_Quantity = quantity;
                    _usedInventoryList.Add(tempUsedPart);
                }
                for (int i = 0; i < _usedInventoryList.Count; i++)
                {
                    if (!_usedInventoryList[i].Part_Name.Equals(usedPart.Part_Name))
                    {
                        add = true;
                    }
                    else if (_usedInventoryList[i].Part_Name.Equals(usedPart.Part_Name))
                    {
                        add = false;
                        _usedInventoryList.RemoveAt(i);
                        Parts_Inventory tempUsedPart = _inventoryManager.GetParts_InventoryByID(id);
                        tempUsedPart.Part_Quantity = quantity;
                        _usedInventoryList.Add(tempUsedPart);
                    }
                }

                if(add)
                {
                    Parts_Inventory tempUsedPart = _inventoryManager.GetParts_InventoryByID(id);
                    tempUsedPart.Part_Quantity = quantity;
                    _usedInventoryList.Add(tempUsedPart);
                }

                productUsedDataGrid.Items.Refresh();
            
                productUsedDataGrid.ItemsSource = _usedInventoryList;
                productUsedDataGrid.Columns[0].Header = "Part Number";
                productUsedDataGrid.Columns[1].Header = "Product";
                productUsedDataGrid.Columns[2].Header = "Quantity";
                productUsedDataGrid.Columns[3].Header = "Unit";
                productUsedDataGrid.Columns[4].Visibility = Visibility.Hidden;
                productUsedDataGrid.Columns[5].Visibility = Visibility.Hidden;
                productUsedDataGrid.Columns[6].Visibility = Visibility.Hidden;
                productUsedDataGrid.Columns[7].Visibility = Visibility.Hidden;
                productUsedDataGrid.Columns[8].Visibility = Visibility.Hidden;
                productUsedDataGrid.Columns[9].Visibility = Visibility.Hidden;
                productUsedDataGrid.Columns[10].Visibility = Visibility.Hidden;
            }
            else if (productCmbBox.SelectedItem == null && quantityTxtBox.Text.Equals(""))
            {
                MessageBox.Show("Select a Product and Enter a Quantity");
            }
            else if (productCmbBox.SelectedItem == null)
            {
                MessageBox.Show("Select a Product");
            }
            else if (quantityTxtBox.Text.Equals(""))
            {
                MessageBox.Show("Enter a Quantity");
            }
        }

        private void confirmCompletionBtn_Click(object sender, RoutedEventArgs e)
        {
            //create full object
            ServiceOrder_VM toComplete = _serviceOrder;
            List<Parts_Inventory> parts = new List<Parts_Inventory>();
            toComplete.serviceOrderLineItems.Clear();
            //update line items with new number of parts used
            for(int i = 0; i < productUsedDataGrid.Items.Count; i++)
            {
                parts.Add((Parts_Inventory)productUsedDataGrid.Items[i]);
            }
            foreach(Parts_Inventory p in parts)
            {
                toComplete.serviceOrderLineItems.Add(new ServiceOrderLineItems()
                {
                    Service_Order_ID = toComplete.Service_Order_ID,
                    Service_Order_Version = toComplete.Service_Order_Version,
                    Parts_Inventory_ID = p.Parts_Inventory_ID,
                    Quantity = p.Part_Quantity
                });
            }
            
            //send the object to the manager class to be completed
            try
            {
                if (1 == _serviceOrderManager.CompleteServiceOrder(toComplete))
                {
                    MessageBox.Show("Service Order Completed, returning to Order list.", "Success", MessageBoxButton.OK, MessageBoxImage.None);
                    
                    NavigationService.Navigate(new WorkOrders.ViewWorkOrderList());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Completion failed, " + ex.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(_allInventoryList.Count == 0)
            {
                NavigationService.GoBack();
            }
            foreach(Parts_Inventory partsInventory in _allInventoryList)
            {
                productCmbBox.Items.Add(partsInventory.Parts_Inventory_ID + " | " + partsInventory.Part_Name);
            }

            serviceTypeTxtBox.Text = _serviceOrder.Service_Type_ID;
            requestDescriptionTxtBox.Text = _serviceOrder.Service_Description;
            mntcNotesTxtBox.Text = _serviceOrder.vehicle.MaintenanceNotes;
        }

        private void cancelCompletionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.GoBack();
            }
            catch (Exception)
            {
                MessageBox.Show("Error canceling.\nClick the Maintenance tab");
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]*\\.[0-9]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void productCmbBox_DropDownClosed(object sender, EventArgs e)
        {
            if (productCmbBox.SelectedItem != null && !productCmbBox.SelectedItem.ToString().Equals(""))
            {
                foreach (Parts_Inventory partsInventory in _allInventoryList)
                {
                    if(partsInventory.Part_Name.Equals(productCmbBox.SelectedItem.ToString().Split('|')[1].Trim()))
                    {
                        unitTxtBox.Text = partsInventory.Part_Unit_Type;
                    }
                    
                }
            }
        }
    }
}
