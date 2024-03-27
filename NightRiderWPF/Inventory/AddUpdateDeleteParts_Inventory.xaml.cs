/// <summary>
/// Jonathan Beck
/// Created: 2024/02/01
/// 
/// XAML to display a single piece of Parts_Inventory. 
/// This will be updated to allow Adding and editing in a future sprint.
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd 

using DataObjects;
using LogicLayer;
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

namespace NightRiderWPF.Inventory
{

    public partial class AddUpdateDeleteParts_Inventory : Page
    {
        Parts_Inventory _part = null;
        Parts_InventoryManager _parts_inventoryManager = null;

        public AddUpdateDeleteParts_Inventory()
        {
            InitializeComponent();
            _parts_inventoryManager = new Parts_InventoryManager();
            _part = new Parts_Inventory();

            tbxParts_InventoryPart_Quantity.Text = "0";
            tbxParts_InventoryOrdered_Qty.Text = "0";
            tbxParts_InventoryStock_Level.Text = "0";
            btnAddUpdateParts_Inventory.Content = "Add Part";
            lblParts_InventoryParts_Inventory_ID.Visibility = Visibility.Hidden;
            tbxParts_InventoryParts_Inventory_ID.Visibility = Visibility.Hidden;

        }
        public AddUpdateDeleteParts_Inventory(Parts_Inventory part)
        {

            InitializeComponent();
            _part = part;
            _parts_inventoryManager = new Parts_InventoryManager();
            lblParts_InventoryPart_Photo_URL.Visibility = Visibility.Hidden;
            tbxParts_InventoryPart_Photo_URL.Visibility = Visibility.Hidden;
            fillPage();

        }
        //Populate all fields with the values from the passed object.
        private void fillPage()
        {
            tbxParts_InventoryParts_Inventory_ID.Text = _part.Parts_Inventory_ID.ToString();
            tbxParts_InventoryItem_Description.Text = _part.Item_Description;
            tbxParts_InventoryPart_Name.Text = _part.Part_Name;
            tbxParts_InventoryPart_Quantity.Text = _part.Part_Quantity.ToString();
            tbxParts_InventoryItem_Description.Text = _part.Item_Description;
            tbxParts_InventoryItem_Specifications.Text = _part.Item_Specifications;
            BitmapImage image = new BitmapImage(new Uri(_part.Part_Photo_URL));
            imgParts_InventoryPart_Photo_URL.Source = image;
            tbxParts_InventoryOrdered_Qty.Text = _part.Ordered_Qty.ToString();
            tbxParts_InventoryStock_Level.Text = _part.Stock_Level.ToString();

            //chkParts_InventoryIs_Active.IsChecked = _part.Is_Active;

            if (_part.Is_Active == false)
            {
                btnRemoveParts_Inventory.Visibility = Visibility.Hidden;
            }
        }

        private void btnBck_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void btnRemoveParts_Inventory_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure? Part will be removed from active inventory.", 
                "Remove Part", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes) 
            {
                try
                {
                    if(_parts_inventoryManager.RemoveParts_Inventory(_part) == 1)
                    {
                        MessageBox.Show("Part removed from active inventory, leaving page.", "Success!");
                        this.NavigationService.GoBack();
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong, removal failed.", "Removal Failed");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Removal Failed", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnAddUpdateParts_Inventory_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(_part.Part_Name))
            { 
                
                try
                {
                    //validate inputs
                    if (!String.IsNullOrEmpty(tbxParts_InventoryPart_Quantity.Text)
                        && !String.IsNullOrEmpty(tbxParts_InventoryOrdered_Qty.Text)
                        && !String.IsNullOrEmpty(tbxParts_InventoryStock_Level.Text)
                        && !String.IsNullOrEmpty(tbxParts_InventoryItem_Specifications.Text)){
                        try
                        { 
                            _part.Item_Specifications = tbxParts_InventoryItem_Specifications.Text;
                            _part.Part_Quantity = Convert.ToInt32(tbxParts_InventoryPart_Quantity.Text);
                            _part.Ordered_Qty = Convert.ToInt32(tbxParts_InventoryOrdered_Qty.Text);
                            _part.Stock_Level = Convert.ToInt32(tbxParts_InventoryStock_Level.Text);
                        }
                        catch (Exception)
                        {

                            throw new ApplicationException("All fields ending with # must be a number.");
                        }
                        
                    }
                    else
                    {
                        throw new ApplicationException("All fields must have a valid value.");
                    }
                    
                   
                    if(tbxParts_InventoryPart_Name.Text.Length < 30 
                        && tbxParts_InventoryPart_Name.Text.Length > 3)
                    {
                        _part.Part_Name = tbxParts_InventoryPart_Name.Text;
                    }
                    else
                    {
                        throw new ApplicationException("The name must be between 4 and 30 characters.");
                    }

                    if(tbxParts_InventoryItem_Description.Text.Length < 100)
                    {
                        _part.Item_Description = tbxParts_InventoryItem_Description.Text;
                    }
                    else
                    {
                        throw new ApplicationException("The Description must be shorter than 100 characters.");
                    }
                    if(Uri.IsWellFormedUriString(tbxParts_InventoryPart_Photo_URL.Text, UriKind.Absolute)
                        && tbxParts_InventoryPart_Photo_URL.Text.EndsWith(".jpg"))
                    {
                        //image input aquiring
                        _part.Part_Photo_URL = tbxParts_InventoryPart_Photo_URL.Text;
                    }
                    else
                    {
                        throw new ApplicationException("Invalid URL format. Image URL must be a link to a .jpg file");
                    }
                    
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Oops",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    _part = new Parts_Inventory();
                    return;
                }
                //add object to database
                try
                {
                    _part.Parts_Inventory_ID = _parts_inventoryManager.AddParts_Inventory(_part);
                    if (_part.Parts_Inventory_ID > 0)
                    {
                        MessageBox.Show("Part #" + _part.Parts_Inventory_ID + " Added to Inventory, returning to Inventory list.", "Success",
                             MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.GoBack();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message, "Woah there",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                

                

            }
        }
    }
}
