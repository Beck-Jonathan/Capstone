using DataObjects.RouteObjects;
using LogicLayer.RouteStop;
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

namespace NightRiderWPF.RouteStop
{
    /// <summary>
    /// Interaction logic for AddStop.xaml
    /// </summary>
    public partial class AddStop : Page
    {
        private IStopManager _stopManager;
        public AddStop()
        {
            _stopManager = new StopManager();
            InitializeComponent();
        }

        public AddStop(IStopManager stopManager)
        {
            _stopManager = stopManager;
            InitializeComponent();
        }

        /// <summary>
        /// AUTHOR:Chris Baenziger
        /// CREATED: 2024-03-26
        /// </summary>
        private void btnAddStop_Click(object sender, RoutedEventArgs e)
        {
            int results = 0;
            try
            {
                // validation
                if(!ValidationHelpers.isNotEmptyOrNull(txtStreetAddress.Text))
                {
                    MessageBox.Show("Please enter a street address.", "Street Address", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtStreetAddress.Focus();
                    return;
                }

                if (!ValidationHelpers.isValidZip(txtZipCode.Text))
                {
                    MessageBox.Show("Please enter a valid zip code.", "Zip Code", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtZipCode.Focus();
                    return;
                }

                if (!ValidationHelpers.isValidLatitude(txtLatitude.Text))
                {
                    MessageBox.Show("Please enter a Latitude.", "Latitdue", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtLatitude.Focus();
                    return;
                }

                if (!ValidationHelpers.isValidLongitude(txtLongitude.Text))
                {
                    MessageBox.Show("Please enter a Longitude.", "Longitude", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtLongitude.Focus();
                    return;
                }

                // add vehicle
                results = _stopManager.AddStop(new Stop()
                {
                    StreetAddress = txtStreetAddress.Text,
                    ZIPCode = txtZipCode.Text,
                    Latitude = Decimal.Parse(txtLatitude.Text),
                    Longitude = Decimal.Parse(txtLongitude.Text),
                    IsActive = ckbIsActive.IsChecked.Value
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem adding the stop.\n" + ex.Message, "Add Stop Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            if (results != 0)
            {
                MessageBox.Show("Stop " + results + " was added.", "Stop Added", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
        }

        /// <summary>
        /// AUTHOR:Chris Baenziger
        /// CREATED: 2024-03-26
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel?\nChanges will be lost!", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }
    }
}
