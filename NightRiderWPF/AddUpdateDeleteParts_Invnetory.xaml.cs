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

namespace NightRiderWPF
{
    
    public partial class AddUpdateDeleteParts_Invnetory : Page
    {
        //Populate all fields with the values from the passed object.
        public AddUpdateDeleteParts_Invnetory(Parts_Inventory _part)
        {
            
            InitializeComponent();
          
            tbxParts_InventoryParts_Inventory_ID.Text = _part.Parts_Inventory_ID.ToString();
            tbxParts_InventoryItem_Description.Text = _part.Item_Description;
            tbxParts_InventoryPart_Name.Text = _part.Part_Name;
            tbxParts_InventoryPart_Quantity.Text = _part.Part_Quantity.ToString();
            tbxParts_InventoryItem_Description.Text = _part.Item_Description;
            tbxParts_InventoryItem_Specifications.Text = _part.Item_Specifications;
            tbxParts_InventoryPart_Photo_URL.Text = _part.Part_Photo_URL;
            tbxParts_InventoryOrdered_Qty.Text = _part.Ordered_Qty.ToString();
            tbxParts_InventoryStock_Level.Text = _part.Stock_Level.ToString();
            chkParts_InventoryIs_Active.IsChecked = _part.Is_Active;
        }
      




       
        

       
        


        
    }
}
