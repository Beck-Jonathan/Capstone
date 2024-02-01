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
        Parts_InventoryManager inventoryManager = null;
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
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            inventoryManager = new Parts_InventoryManager();
            try
            {
                datParts_Inventory.ItemsSource = inventoryManager.getAllParts_Inventory();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Uanble to load inventory" +ex.Message);
            }
            
            
        }
        /// <summary>
        /// Jonathan Beck
        /// Created: 2024/02/01
        /// 
        /// Passes Selected record to the AddEditDeleteParts_Inventory Window
        /// </summary>
        /// <throws>Argument Exception</throws>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 

        private void datParts_Inventory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        
        {

            
            if (datParts_Inventory.SelectedItems.Count != 0)
            {
                NavigationWindow navWIN = new NavigationWindow();
                
               
                var _part = datParts_Inventory.SelectedItem as Parts_Inventory;
                navWIN.Content = new AddUpdateDeleteParts_Invnetory(_part);
                navWIN.Show();

            }
            else { MessageBox.Show("Pick something, please"); }
        }
    }
}
