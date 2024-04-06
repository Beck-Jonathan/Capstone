using DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
using System.Windows.Shapes;

namespace NightRiderWPF.VehicleModels
{
    /// <summary>
    /// Interaction logic for AddCompatiblePartWindow.xaml
    /// </summary>
    public partial class AddCompatiblePartWindow : Window
    {
        private IEnumerable<Parts_Inventory> _parts;
        private Parts_Inventory _selectedPart;

        public Parts_Inventory SelectedPart { get => _selectedPart; }

        public AddCompatiblePartWindow(IEnumerable<Parts_Inventory> parts)
        {
            _parts = parts;
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            _selectedPart = (Parts_Inventory)cmbPart.SelectedItem;
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbPart.ItemsSource = _parts;
        }
    }
}
