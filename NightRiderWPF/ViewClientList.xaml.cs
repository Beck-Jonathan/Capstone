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
using LogicLayer;

namespace NightRiderWPF
{
    /// <summary>
    /// AUTHOR: Isabella Rosenbohm
    /// <br />
    /// CREATED: 2024-02-05
    /// <br />
    /// 
    ///     Interaction logic for ViewClientList.xaml
    /// </summary>
    /// 
    /// <remarks>
    /// </remarks>
    public partial class ViewClientList : Page
    {
        public ViewClientList()
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
                        datListClients.Columns.RemoveAt(12);
                        datListClients.Columns.RemoveAt(10);
                        datListClients.Columns.RemoveAt(9);
                        datListClients.Columns.RemoveAt(8);
                        datListClients.Columns.RemoveAt(7);
                        datListClients.Columns.RemoveAt(5);
                        datListClients.Columns.RemoveAt(2);
                        datListClients.Columns.RemoveAt(1);
                        datListClients.Columns.RemoveAt(0);

                        // This makes the headers of the columns more readable for the user
                        datListClients.Columns[0].Header = "First Name";
                        datListClients.Columns[1].Header = "Last Name";
                        datListClients.Columns[3].Header = "Phone Number";
                        datListClients.Columns[4].Header = "Active";

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
    }
}
