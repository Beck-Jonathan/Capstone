using System;
using System.Collections;
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
using NightRiderWPF.Clients;

namespace NightRiderWPF
{
    /// <summary>
    /// AUTHOR: Isabella Rosenbohm
    /// <br />
    /// CREATED: 2024-02-05
    /// <br />
    /// 
    ///     Interaction logic for AdminViewClientList.xaml
    /// </summary>
    /// 
    /// <remarks>
    /// </remarks>
    public partial class AdminViewClientList : Page
    {
        Client_VM _selectedClient = null;
        public AdminViewClientList()
        {
            InitializeComponent();
        }

        /// <remarks>><br />
        ///    CONTRIBUTOR: Isabella Rosenbohm
        /// <br />
        ///    CREATED: 2024-02-06
        /// </remarks>
        private void datListClients_Loaded(object sender, RoutedEventArgs e)
        {

            if(datListClients.ItemsSource == null)
            {
                var clientManager = new ClientManager();
                try
                {
                    if (clientManager.GetAllClients() != null)
                    {
                        datListClients.ItemsSource = clientManager.GetAllClients();
                        
                        // This removes columns that don't need to be seen in List View, but will be shown in Detail View 
                        datListClients.Columns.RemoveAt(13); // voice number
                        datListClients.Columns.RemoveAt(11); // address
                        datListClients.Columns.RemoveAt(10); // region
                        datListClients.Columns.RemoveAt(9); // city
                        datListClients.Columns.RemoveAt(8); // postal code
                        datListClients.Columns.RemoveAt(6); // dob
                        datListClients.Columns.RemoveAt(2); // roles
                        datListClients.Columns.RemoveAt(1); // Username

                        datListClients.Columns[0].DisplayIndex = 6; // moves details column to end

                        // This makes the headers of the columns more readable for the user
                        datListClients.Columns[1].Header = "First Name";
                        datListClients.Columns[2].Header = "Last Name";
                        datListClients.Columns[4].Header = "Phone Number";
                        datListClients.Columns[5].Header = "Active";

                        // this enables a vertical scrollbar if there are more rows than will fit within the size of the datalist
                        datListClients.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    }
                    else
                    {
                        MessageBox.Show("No clients found.", "No clients to show.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message,
                        "Client Retrieval Failed.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            
        }

        private void btnViewClientDetail_Click(object sender, RoutedEventArgs e)
        {
            _selectedClient = ((FrameworkElement)sender).DataContext as Client_VM;
            AdminViewClientDetail adminViewClientDetail = new AdminViewClientDetail(_selectedClient);
            this.NavigationService.Navigate(adminViewClientDetail);
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            AdminAddClient adminAddClient = new AdminAddClient();
            this.NavigationService.Navigate(adminAddClient);
        }
    }
}
