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
using DataObjects;
using LogicLayer;
using LogicLayer.Inventory;

namespace NightRiderWPF.PurchaseOrders
{
    /// <summary>
    /// Interaction logic for Parts_Person_Add_Update_Delete_Line_Items.xaml
    /// Initial version created by Jonathan Beck
    /// Date : 3/18/2024
    /// </summary>
    public partial class Parts_Person_Add_Update_Delete_Line_Items : Page
    {
        List<Parts_Inventory> parts;
        Parts_InventoryManager _Parts_InventoryManager;
        PurchaseOrderManager _PurchaseOrderManager;
        SourceManager _SourceManager;
        Dictionary<String, int> partsKeyValues;
        List<String> partnames;
        Purchase_OrderVM _passed;
        String oldInputValue;
        List<POLineItemVM> poLines;
        public Parts_Person_Add_Update_Delete_Line_Items(Purchase_OrderVM passed)
        {
            InitializeComponent();
            _Parts_InventoryManager = new Parts_InventoryManager();
            _SourceManager = new SourceManager();
            _passed = passed;
            oldInputValue = "";
            poLines = new List<POLineItemVM>();
            passed.pOLineItems = poLines;
            _PurchaseOrderManager = new PurchaseOrderManager();

        }
        /// <summary>
        /// Initial version created by Jonathan Beck
        /// Creates a dictionary of part names and ids to make the seleciton more logical
        /// Date : 3/18/2024
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            partsKeyValues = new Dictionary<string, int>();
            partnames = new List<String>();
            try
            {

                parts = _Parts_InventoryManager.GetActiveParts_Inventory();
                if (parts.Count == 0)
                {
                    throw new Exception("Can't load parts");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                NavigationService.GoBack();
            }
            foreach (Parts_Inventory part in parts)
            {
                partnames.Add(part.Part_Name);
                partsKeyValues.Add(part.Part_Name, part.Parts_Inventory_ID);
            }
            cbxParts_InventoryPart_Name.ItemsSource = partnames;

        }

        private void datLineItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        /// <summary>
        /// Initial version created by Jonathan Beck
        /// Updates estimated price and item description on seleciton changed
        /// Date : 3/18/2024
        /// </summary>

        private void cbxParts_InventoryPart_Name_SelectionChanged(object sender, EventArgs e)
        {
            string selectedPart = cbxParts_InventoryPart_Name.Text;
            int selectedPartID = partsKeyValues[selectedPart];
            Source selectedSouce = new Source();
            Parts_Inventory part = new Parts_Inventory();
            try
            {
                part = _Parts_InventoryManager.GetParts_InventoryByID(selectedPartID);
                selectedSouce = _SourceManager.LookupSourceByVendorIDandPartsInventoryID(_passed.Vendor_ID, selectedPartID);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            tbxParts_InventoryItem_Description.Text = part.Item_Description;
            if (selectedSouce.Part_Price != 0)
            {
                string displayPrice = "$" + Math.Round(selectedSouce.Part_Price, 2).ToString();
                tbxParts_InventoryItem_Price.Text = displayPrice;
            }
            else
            {
                tbxParts_InventoryItem_Price.Text = "We do not have an estimate \n available.";
            }

        }
        /// <summary>
        /// Initial version created by Jonathan Beck
        /// validates the user has put in a number
        /// Date : 3/18/2024
        /// </summary>

        private void tbxParts_InventoryPart_Quantity_KeyUp(object sender, KeyEventArgs e)
        {
            String newInputValue = tbxParts_InventoryPart_Quantity.Text;
            if (newInputValue != "")
            {
                if (!Int32.TryParse(newInputValue, out int quantity))
                {
                    tbxParts_InventoryPart_Quantity.Text = oldInputValue;
                    tbxParts_InventoryPart_Quantity.CaretIndex = oldInputValue.Length;
                }
                else
                {
                    oldInputValue = tbxParts_InventoryPart_Quantity.Text;
                }
            }

        }
        /// <summary>
        /// Initial version created by Jonathan Beck
        /// 
        ///Cretes a new line item and adds it to the order
        /// Date : 3/18/2024
        /// </summary>

        private void btnAddLineItem_Click(object sender, RoutedEventArgs e)
        {
            if (cbxParts_InventoryPart_Name.Text.isNotEmptyOrNull()
                && tbxParts_InventoryPart_Quantity.Text.isNotEmptyOrNull()
                && tbxPart_Description.Text.isNotEmptyOrNull()
                && tbxPart_Description.Text.Length < 100
                )
            {
                POLineItemVM pOLineItem = new POLineItemVM();
                string selectedPart = cbxParts_InventoryPart_Name.Text;
                int selectedPartID = partsKeyValues[selectedPart];
                pOLineItem.PurchaseOrderID = 0;
                pOLineItem.PartsInventoryID = selectedPartID;
                pOLineItem.LineNumber = _passed.pOLineItems.Count + 100000;
                pOLineItem.LineItemName = selectedPart;
                pOLineItem.Quantity = Int32.Parse(tbxParts_InventoryPart_Quantity.Text);
                pOLineItem.LineItemDescription = tbxPart_Description.Text;
                //isACive being false means this has not been received yet
                pOLineItem.isActive = false;
                string tryParse = tbxParts_InventoryItem_Price.Text.Substring(1);
                if (Decimal.TryParse(tryParse, out decimal price))
                {
                    pOLineItem.Price = Decimal.Parse(tbxParts_InventoryItem_Price.Text.Substring(1));
                }
                else
                {
                    pOLineItem.Price = 0;
                }



                poLines.Add(pOLineItem);
                datLineItems.ItemsSource = null;
                datLineItems.ItemsSource = poLines;
                datLineItems.Columns.RemoveAt(0);
                datLineItems.Columns.RemoveAt(0);
                datLineItems.Columns.RemoveAt(6);
            }
        }
        /// <summary>
        /// Initial version created by Jonathan Beck
        /// submits the order to the database
        /// Date : 3/18/2024
        /// </summary>
        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            try
            {
                result = _PurchaseOrderManager.CreatePurchaseOrder(_passed);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Purchase Order # " + result.ToString() + " Added");
            NavigationService.Navigate(new Parts_Person_View_Purchase_Orders());
        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Parts_Person_View_Purchase_Orders());
        }
    }
}
