﻿using System;
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
using DataAccessFakes;
using DataObjects.HelperObjects;
using DataObjects.RouteObjects;
using LogicLayer.RouteStop;


namespace NightRiderWPF.RouteStop
{
    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// DATE: 2024-03-05
    /// Interaction logic for RouteList.xaml
    /// </summary>
    public partial class RouteList : Page
    {
        private List<RouteVM> _routes;
        private IRouteManager _routeManager;
        public RouteList()
        {
            InitializeComponent();
            _routeManager = new RouteManager();
            try
            {
                _routes = _routeManager.getRoutes();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);
                
                NavigationService.GoBack();
                
            }
            grdRouteList.ItemsSource = _routes;
        }
    }
}