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

namespace NightRiderWPF.WorkOrders
{
    /// <summary>
    /// Interaction logic for VehicleMaintenancePage.xaml
    /// </summary>
    public partial class VehicleMaintenancePage : Page
    {
        ServiceOrderManager _serviceOrderManager;
        public VehicleMaintenancePage()
        {
            InitializeComponent();
            _serviceOrderManager = new ServiceOrderManager();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                datMaintenance_List.ItemsSource = _serviceOrderManager.GetAllVehiclesWithPendingServiceOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load maintenance list" + ex.Message);
            }
        }

        private void Closebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService?.GoBack();
            }
            catch (Exception)
            {
                MessageBox.Show("No page to navigate back too.");
            }
        }
    }
}