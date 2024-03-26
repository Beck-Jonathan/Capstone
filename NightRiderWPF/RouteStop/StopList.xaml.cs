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
using DataObjects.HelperObjects;
using DataObjects.RouteObjects;
using LogicLayer.RouteStop;

namespace NightRiderWPF.RouteStop
{
    /// <summary>
    /// AUTHOR: Chris Baenziger
    /// DATE: 2024-03-23
    /// Interaction logic for StopList.xaml
    /// </summary>
    public partial class StopList : Page
    {
        private List<Stop> _stops;
        private IStopManager _stopManager;
        public StopList()
        {
            InitializeComponent();
            _stopManager = new StopManager();
            refreshStopList();
        }

        /// <summary>
        /// AUTHOR:Chris Baenziger
        /// CREATED: 2024-03-26
        /// </summary>
        private void refreshStopList()
        {
            try
            {
                _stops = _stopManager.GetStops();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);

                NavigationService.GoBack();
            }
            grdStopList.ItemsSource = _stops;
        }

        /// <summary>
        /// AUTHOR:Chris Baenziger
        /// CREATED: 2024-03-26
        /// </summary>
        private void btnAddStop_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddStop(_stopManager));
            
        }

        /// <summary>
        /// AUTHOR:Chris Baenziger
        /// CREATED: 2024-03-26
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            refreshStopList();
        }
    }
}
