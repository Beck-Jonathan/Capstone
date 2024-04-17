using DataObjects;
using LogicLayer;
using NightRiderWPF.PurchaseOrders;
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

namespace NightRiderWPF.PurchsaeOrders
{
    /// <summary>
    /// Interaction logic for Parts_Person_View_Current_PO_Details.xaml
    /// </summary>
    public partial class Parts_Person_View_Current_PO_Details : Page
    {
        POLineItemsManager polineItemsManager;


        Purchase_OrderVM _order;
        public Parts_Person_View_Current_PO_Details(Purchase_OrderVM _OrderVM)
        {
            InitializeComponent();
            _order = _OrderVM;



        }
        /// <summary>
        /// Created By Jonathan Beck, fills the data grid with all the Line Items
        /// Created 2/24/2024
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tbxParts_InventoryItem_Description.Text = "";
            tbxParts_InventoryItem_Price.Text = "";
            tbxParts_InventoryPart_Quantity.Text = "";

            foreach (POLineItem round in _order.pOLineItems)
            {
                round.Price = Math.Round(round.Price, 2);
            }
            try
            {
                datLineItems.ItemsSource = _order.pOLineItems;

                datLineItems.Columns.RemoveAt(0);
                datLineItems.Columns.RemoveAt(0);
                datLineItems.Columns[4].Header = "Price ($)";

                datLineItems.SelectedIndex = 0;

            }
            catch (Exception ex)
            {

                MessageBox.Show("unable to display Purchase Orders");
            }
        }

        /// <summary>
        /// Created By Jonathan Beck, fills the detail boxes on top
        /// Created 2/24/2024
        /// </summary>

        private void datLineItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datLineItems.SelectedItems.Count != 0)
            {
                var lineItem = datLineItems.SelectedItem as POLineItemVM;
                Parts_Inventory part = lineItem.Part;






                lblParts_InventoryPart_Name.Content = part.Part_Name;
                tbxParts_InventoryPart_Quantity.Text = lineItem.Quantity.ToString();
                tbxParts_InventoryItem_Description.Text = part.Item_Description;
                //imgParts_InventoryPart_Photo_URL.ToString

                tbxParts_InventoryItem_Price.Text = "$" + Math.Round(lineItem.Price, 2).ToString();
                BitmapImage image = new BitmapImage(new Uri(part.Part_Photo_URL));
                imgParts_InventoryPart_Photo_URL.Source = image;

            }
            else { MessageBox.Show("Pick something, please"); }

        }
        /// <summary>
        /// Created By Jonathan Beck,Goes back
        /// Created 2/24/2024
        /// </summary>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Parts_Person_View_Purchase_Orders());
        }
    }
}
