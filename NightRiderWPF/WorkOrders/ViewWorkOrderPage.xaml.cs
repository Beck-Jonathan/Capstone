using DataObjects;
using LogicLayer;
using NightRiderWPF.DeveloperView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

namespace NightRiderWPF.WorkOrders
{
    /// <summary>
    /// AUTHOR: Ben Collins
    /// <br />
    /// CREATED: 2024-02-10
    /// <br />
    ///     The presentation layer displaying a form for an Admin entering an new Employee into the system.
    /// </summary>
    /// 
    /// <remarks>
    /// UPDATER: Ben Collins
    /// <br />
    /// UPDATED: 2024-02-11
    /// <br />
    ///     Initial Creation
    ///     
    ///     Added the function from the back end to the front end
    /// </remarks>
    public partial class ViewWorkOrderList : Page
    {
        IServiceOrderManager _serviceOrderManager = null;
        List<ServiceOrder_VM> _serviceOrders = null;

        public ViewWorkOrderList()
        {
            try
            {
                InitializeComponent();  
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message + " occurred.");
            }
        }

        public ViewWorkOrderList(List<ServiceOrder_VM> orders) {
            _serviceOrders = orders;
            InitializeComponent();
        
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_serviceOrders == null)
            {
                try
                {
                    _serviceOrderManager = new ServiceOrderManager();
                    _serviceOrders = new List<ServiceOrder_VM>(_serviceOrderManager.GetALlServiceOrders());
                    

                }
                catch (Exception ex)
                {
                    return;
                }
                
            }
            List<dynamic> dataObjects = new List<dynamic>();
            foreach (ServiceOrder_VM serviceOrder in _serviceOrders)
            {
                bool criticalIssue = serviceOrder.Critical_Issue;
                string serviceType_ID = serviceOrder.Service_Type_ID;
                string serviceDescription = serviceOrder.Service_Description;
                int serviceOrderID = serviceOrder.Service_Order_ID;
                Button updateButton = new Button();
                updateButton.Height = 30;
                updateButton.Width = 80;
                updateButton.Content = "Update";
                Button completeButton = new Button();
                completeButton.Height = 30;
                completeButton.Width = 80;
                completeButton.Content = "Complete";

                dynamic myDynamic = new
                {
                    PropertyOne = criticalIssue, 
                    PropertyTwo = serviceType_ID,
                    PropertyThree = serviceDescription,
                    PropertyFour = updateButton,
                    PropertyFive = completeButton,
                    PropertySix = serviceOrderID
                };
                dataObjects.Add(myDynamic);
            }


            mntcViewWorkOrderPendingDg.ItemsSource = dataObjects;
            if (dataObjects.Count() == 0)
            {
                return;
            }
            mntcViewWorkOrderPendingDg.Columns[0].DisplayIndex = 4;
            mntcViewWorkOrderPendingDg.Columns[1].DisplayIndex = 4;
            mntcViewWorkOrderPendingDg.Columns[0].Header = "";
            mntcViewWorkOrderPendingDg.Columns[0].Width = 120;
            mntcViewWorkOrderPendingDg.Columns[0].CanUserReorder = false;
            mntcViewWorkOrderPendingDg.Columns[0].IsReadOnly = true;
            mntcViewWorkOrderPendingDg.Columns[1].Header = "";
            mntcViewWorkOrderPendingDg.Columns[1].Width = 127;
            mntcViewWorkOrderPendingDg.Columns[1].CanUserReorder = false;
            mntcViewWorkOrderPendingDg.Columns[1].IsReadOnly = true;
            mntcViewWorkOrderPendingDg.Columns[2].Header = "High Priority";
            mntcViewWorkOrderPendingDg.Columns[2].Width = 76;
            mntcViewWorkOrderPendingDg.Columns[2].CanUserReorder = false;
            mntcViewWorkOrderPendingDg.Columns[2].IsReadOnly = true;
            mntcViewWorkOrderPendingDg.Columns[3].Header = "Service Type";
            mntcViewWorkOrderPendingDg.Columns[3].Width = 150;
            mntcViewWorkOrderPendingDg.Columns[3].CanUserReorder = false;
            mntcViewWorkOrderPendingDg.Columns[4].Header = "Service Description";
            mntcViewWorkOrderPendingDg.Columns[4].Width = 236;
            mntcViewWorkOrderPendingDg.Columns[4].CanUserReorder = false;

        }

        private void mntcViewWorkOrdersSearchTxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (mntcViewWorkOrdersSearchTxt.Text == "")
            {
                //remake the list same as above
                List<dynamic> dataObjects = new List<dynamic>();
                if (_serviceOrders == null)
                {
                    return;
                }
                foreach (ServiceOrder_VM serviceOrder in _serviceOrders)
                {
                    bool criticalIssue = serviceOrder.Critical_Issue;
                    string serviceType_ID = serviceOrder.Service_Type_ID;
                    string serviceDescription = serviceOrder.Service_Description;
                    Button updateButton = new Button();
                    updateButton.Height = 30;
                    updateButton.Width = 80;
                    updateButton.Content = "Update";
                    Button completeButton = new Button();
                    completeButton.Height = 30;
                    completeButton.Width = 80;
                    completeButton.Content = "Complete";

                    dynamic myDynamic = new
                    {
                        PropertyOne = criticalIssue,
                        PropertyTwo = serviceType_ID,
                        PropertyThree = serviceDescription,
                        PropertyFour = updateButton,
                        PropertyFive = completeButton

                    };
                    dataObjects.Add(myDynamic);
                }


                mntcViewWorkOrderPendingDg.ItemsSource = dataObjects;
                if (dataObjects.Count() == 0)
                {
                    return;
                }
                mntcViewWorkOrderPendingDg.Columns[0].DisplayIndex = 4;
                mntcViewWorkOrderPendingDg.Columns[1].DisplayIndex = 4;
                mntcViewWorkOrderPendingDg.Columns[0].Header = "";
                mntcViewWorkOrderPendingDg.Columns[0].Width = 120;
                mntcViewWorkOrderPendingDg.Columns[0].CanUserReorder = false;
                mntcViewWorkOrderPendingDg.Columns[0].IsReadOnly = true;
                mntcViewWorkOrderPendingDg.Columns[1].Header = "";
                mntcViewWorkOrderPendingDg.Columns[1].Width = 127;
                mntcViewWorkOrderPendingDg.Columns[1].CanUserReorder = false;
                mntcViewWorkOrderPendingDg.Columns[1].IsReadOnly = true;
                mntcViewWorkOrderPendingDg.Columns[2].Header = "High Priority";
                mntcViewWorkOrderPendingDg.Columns[2].Width = 76;
                mntcViewWorkOrderPendingDg.Columns[2].CanUserReorder = false;
                mntcViewWorkOrderPendingDg.Columns[2].IsReadOnly = true;
                mntcViewWorkOrderPendingDg.Columns[3].Header = "Service Type";
                mntcViewWorkOrderPendingDg.Columns[3].Width = 150;
                mntcViewWorkOrderPendingDg.Columns[3].CanUserReorder = false;
                mntcViewWorkOrderPendingDg.Columns[4].Header = "Service Description";
                mntcViewWorkOrderPendingDg.Columns[4].Width = 236;
                mntcViewWorkOrderPendingDg.Columns[4].CanUserReorder = false;

            }
            else
            {
                //make a new list based the search box
                List<dynamic> dataObjects = new List<dynamic>();
                if (_serviceOrders == null)
                {
                    return;
                }
                foreach (ServiceOrder_VM serviceOrder in _serviceOrders)

                {
                    if (serviceOrder.Service_Type_ID.ToLower().Contains(mntcViewWorkOrdersSearchTxt.Text.ToLower()))
                    {
                        bool criticalIssue = serviceOrder.Critical_Issue;
                        string serviceType_ID = serviceOrder.Service_Type_ID;
                        string serviceDescription = serviceOrder.Service_Description;
                        Button updateButton = new Button();
                        updateButton.Height = 30;
                        updateButton.Width = 80;
                        updateButton.Content = "Update";
                        Button completeButton = new Button();
                        completeButton.Height = 30;
                        completeButton.Width = 80;
                        completeButton.Content = "Complete";

                        dynamic myDynamic = new
                        {
                            PropertyOne = criticalIssue,
                            PropertyTwo = serviceType_ID,
                            PropertyThree = serviceDescription,
                            PropertyFour = updateButton,
                            PropertyFive = completeButton
                        };
                        dataObjects.Add(myDynamic);
                    }

                }
                mntcViewWorkOrderPendingDg.ItemsSource = dataObjects;
                if (dataObjects.Count > 0)
                {
                    mntcViewWorkOrderPendingDg.ItemsSource = dataObjects;
                    mntcViewWorkOrderPendingDg.Columns[0].DisplayIndex = 4;
                    mntcViewWorkOrderPendingDg.Columns[1].DisplayIndex = 4;
                    mntcViewWorkOrderPendingDg.Columns[0].Header = "";
                    mntcViewWorkOrderPendingDg.Columns[0].Width = 120;
                    mntcViewWorkOrderPendingDg.Columns[0].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[0].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[1].Header = "";
                    mntcViewWorkOrderPendingDg.Columns[1].Width = 127;
                    mntcViewWorkOrderPendingDg.Columns[1].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[1].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[2].Header = "High Priority";
                    mntcViewWorkOrderPendingDg.Columns[2].Width = 76;
                    mntcViewWorkOrderPendingDg.Columns[2].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[2].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[3].Header = "Service Type";
                    mntcViewWorkOrderPendingDg.Columns[3].Width = 150;
                    mntcViewWorkOrderPendingDg.Columns[3].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[4].Header = "Service Description";
                    mntcViewWorkOrderPendingDg.Columns[4].Width = 236;
                    mntcViewWorkOrderPendingDg.Columns[4].CanUserReorder = false;
                }
            }
        }

        /// <summary>
        ///  Handles the click event of the Update button in the view work order page.
        ///  Opens the UpdateWorkOrderPage when the button is clicked
        /// </summary>
        /// <returns>
        ///   selected data from the datagrid and allows an employee to update a service order  
        /// </returns>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    <see cref="Exception">Exception</see>: Thrown when error encountered
        /// <br /><br />
        ///    CONTRIBUTOR: Steven Sanchez
        /// <br />
        ///    CREATED: 2024-02-18
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>
        private void mntcViewWorkOrderUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dynamic selectedRow = mntcViewWorkOrderPendingDg.SelectedItem;

                // Extract the necessary properties from the selected row
                bool criticalIssue = selectedRow.PropertyOne;
                string serviceType_ID = selectedRow.PropertyTwo;
                string serviceDescription = selectedRow.PropertyThree;
                int serviceOrderID = selectedRow.PropertySix;
                ServiceOrder_VM selectedWorkOrder = new ServiceOrder_VM
                {
                    Service_Order_ID = serviceOrderID,
                    Critical_Issue = criticalIssue,
                    Service_Type_ID = serviceType_ID,
                    Service_Description = serviceDescription
                };


                // Open the UpdateWorkOrderPage and pass the selected work order object
                UpdateWorkOrderPage updatePage = new UpdateWorkOrderPage(selectedWorkOrder);
                NavigationService.Navigate(updatePage);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void mntcViewWorkOrderCompleteBtn_Click(object sender, RoutedEventArgs e)
        {
            // Action for complete work order
            try
            {
                dynamic selectedRow = mntcViewWorkOrderPendingDg.SelectedItem;
                int serviceOrderID = selectedRow.PropertySix;
                ServiceOrder_VM selectedServiceOrder = new ServiceOrder_VM
                {
                    Service_Order_ID = serviceOrderID,
                };
                CompleteWorkOrderPage completePage = new CompleteWorkOrderPage(selectedServiceOrder);
                NavigationService.Navigate(completePage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred: " + ex.Message);
            }
        }

        private void mntcViewWorkOrderCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            // Action to reurn to the previous page
            try
            {
                this.NavigationService.GoBack();
            }
            catch (Exception)
            {
                MessageBox.Show("No page to navigate back too.");
            }
        }

        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateWorkOrderPage createPage = new CreateWorkOrderPage();
                NavigationService.Navigate(createPage);
            }
            catch(Exception)
            {
                MessageBox.Show("Error loading Create Work Order page.");

            }
        }

        /// <summary>
        ///  FIlters work orders that are complete, incomplete, or all
        /// </summary>
        /// <remarks>
        ///    Exceptions:
        /// <br />
        ///    CONTRIBUTOR: Jared Roberts
        /// <br />
        ///    CREATED: 2024-03-05
        /// <br />
        /// <br />
        ///    UPDATER: [Updater's Name]
        /// <br />
        ///    UPDATED: yyyy-MM-dd
        /// <br />
        ///     Initial Creation
        /// </remarks>


        private void mntcFilterCbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedValue = (mntcFilterCbo.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (selectedValue == "Complete")
            {
                try
                {
                    _serviceOrderManager = new ServiceOrderManager();
                    _serviceOrders = new List<ServiceOrder_VM>(_serviceOrderManager.GetAllCompleteServiceOrders());
                    if (_serviceOrders.Count == 0)
                    {
                        MessageBox.Show("There Are No Completed Work Orders");
                        return;
                    }

                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(ArgumentException))
                    {
                        _serviceOrders = new List<ServiceOrder_VM>();
                    }
                }
                
                List<dynamic> dataObjects = new List<dynamic>();
                foreach (ServiceOrder_VM serviceOrder in _serviceOrders)
                {
                    bool criticalIssue = serviceOrder.Critical_Issue;
                    string serviceType_ID = serviceOrder.Service_Type_ID;
                    string serviceDescription = serviceOrder.Service_Description;
                    int serviceOrderID = serviceOrder.Service_Order_ID;
                    Button updateButton = new Button();
                    updateButton.Height = 30;
                    updateButton.Width = 80;
                    updateButton.Content = "Update";
                    Button completeButton = new Button();
                    completeButton.Height = 30;
                    completeButton.Width = 80;
                    completeButton.Content = "Complete";

                    dynamic myDynamic = new
                    {
                        PropertyOne = criticalIssue,
                        PropertyTwo = serviceType_ID,
                        PropertyThree = serviceDescription,
                        PropertyFour = updateButton,
                        PropertyFive = completeButton,
                        PropertySix = serviceOrderID
                    };
                    dataObjects.Add(myDynamic);
                }

                

                if(dataObjects.Count != 0)
                {
                    mntcViewWorkOrderPendingDg.ItemsSource = dataObjects;
                    mntcViewWorkOrderPendingDg.Columns[0].Visibility = Visibility.Visible;
                    mntcViewWorkOrderPendingDg.Columns[1].Visibility = Visibility.Hidden;
                    mntcViewWorkOrderPendingDg.Columns[5].Visibility = Visibility.Hidden;
                    mntcViewWorkOrderPendingDg.Columns[6].Visibility = Visibility.Hidden;
                    mntcViewWorkOrderPendingDg.Columns[7].Visibility = Visibility.Hidden;
                    mntcViewWorkOrderPendingDg.Columns[0].DisplayIndex = 4;
                    mntcViewWorkOrderPendingDg.Columns[1].DisplayIndex = 4;
                    mntcViewWorkOrderPendingDg.Columns[0].Header = "";
                    mntcViewWorkOrderPendingDg.Columns[0].Width = 120;
                    mntcViewWorkOrderPendingDg.Columns[0].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[0].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[1].Header = "";
                    mntcViewWorkOrderPendingDg.Columns[1].Width = 127;
                    mntcViewWorkOrderPendingDg.Columns[1].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[1].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[2].Header = "High Priority";
                    mntcViewWorkOrderPendingDg.Columns[2].Width = 76;
                    mntcViewWorkOrderPendingDg.Columns[2].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[2].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[3].Header = "Service Type";
                    mntcViewWorkOrderPendingDg.Columns[3].Width = 150;
                    mntcViewWorkOrderPendingDg.Columns[3].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[4].Header = "Service Description";
                    mntcViewWorkOrderPendingDg.Columns[4].Width = 236;
                    mntcViewWorkOrderPendingDg.Columns[4].CanUserReorder = false;
                }
                else
                {
                    dataObjects = new List<dynamic>();
                    dynamic myDynamic = new
                    {
                        PropertyOne = true,
                        PropertyTwo = "Null",
                        PropertyThree = "No Complete Service Orders"
                    };
                    
                    dataObjects.Add(myDynamic);

                    mntcViewWorkOrderPendingDg.ItemsSource = dataObjects;
                    mntcViewWorkOrderPendingDg.Columns[0].Visibility = Visibility.Hidden;
                    mntcViewWorkOrderPendingDg.Columns[1].Visibility = Visibility.Hidden;
                    mntcViewWorkOrderPendingDg.Columns[0].DisplayIndex = 4;
                    mntcViewWorkOrderPendingDg.Columns[1].DisplayIndex = 4;
                    mntcViewWorkOrderPendingDg.Columns[0].Header = "";
                    mntcViewWorkOrderPendingDg.Columns[0].Width = 120;
                    mntcViewWorkOrderPendingDg.Columns[0].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[0].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[1].Header = "";
                    mntcViewWorkOrderPendingDg.Columns[1].Width = 127;
                    mntcViewWorkOrderPendingDg.Columns[1].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[1].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[2].Header = "High Priority";
                    mntcViewWorkOrderPendingDg.Columns[2].Width = 76;
                    mntcViewWorkOrderPendingDg.Columns[2].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[2].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[3].Header = "Service Type";
                    mntcViewWorkOrderPendingDg.Columns[3].Width = 150;
                    mntcViewWorkOrderPendingDg.Columns[3].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[4].Header = "Service Description";
                    mntcViewWorkOrderPendingDg.Columns[4].Width = 236;
                    mntcViewWorkOrderPendingDg.Columns[4].CanUserReorder = false;

                }
                
                mntcViewWorkOrderPendingDg.Items.Refresh();
            }
            else if (selectedValue == "Incomplete")
            {
                try
                {
                    _serviceOrderManager = new ServiceOrderManager();
                    _serviceOrders = new List<ServiceOrder_VM>(_serviceOrderManager.GetAllIncompleteServiceOrders());
                    if (_serviceOrders.Count == 0)
                    {
                        MessageBox.Show("There Are No Incomplete Work Orders");
                        return;
                    }

                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(ArgumentException))
                    {
                        _serviceOrders = new List<ServiceOrder_VM>();
                    }
                }
                
                List<dynamic> dataObjects = new List<dynamic>();
                foreach (ServiceOrder_VM serviceOrder in _serviceOrders)
                {
                    bool criticalIssue = serviceOrder.Critical_Issue;
                    string serviceType_ID = serviceOrder.Service_Type_ID;
                    string serviceDescription = serviceOrder.Service_Description;
                    int serviceOrderID = serviceOrder.Service_Order_ID;
                    Button updateButton = new Button();
                    updateButton.Height = 30;
                    updateButton.Width = 80;
                    updateButton.Content = "Update";
                    Button completeButton = new Button();
                    completeButton.Height = 30;
                    completeButton.Width = 80;
                    completeButton.Content = "Complete";

                    dynamic myDynamic = new
                    {
                        PropertyOne = criticalIssue,
                        PropertyTwo = serviceType_ID,
                        PropertyThree = serviceDescription,
                        PropertyFour = updateButton,
                        PropertyFive = completeButton,
                        PropertySix = serviceOrderID
                    };
                    dataObjects.Add(myDynamic);
                }

                if (dataObjects.Count != 0)
                {
                    mntcViewWorkOrderPendingDg.ItemsSource = dataObjects;
                    mntcViewWorkOrderPendingDg.Columns[0].Visibility = Visibility.Visible;
                    mntcViewWorkOrderPendingDg.Columns[1].Visibility = Visibility.Visible;
                    mntcViewWorkOrderPendingDg.Columns[5].Visibility = Visibility.Hidden;
                    mntcViewWorkOrderPendingDg.Columns[6].Visibility = Visibility.Hidden;
                    mntcViewWorkOrderPendingDg.Columns[7].Visibility = Visibility.Hidden;
                    mntcViewWorkOrderPendingDg.Columns[0].DisplayIndex = 4;
                    mntcViewWorkOrderPendingDg.Columns[1].DisplayIndex = 4;
                    mntcViewWorkOrderPendingDg.Columns[0].Header = "";
                    mntcViewWorkOrderPendingDg.Columns[0].Width = 120;
                    mntcViewWorkOrderPendingDg.Columns[0].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[0].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[1].Header = "";
                    mntcViewWorkOrderPendingDg.Columns[1].Width = 127;
                    mntcViewWorkOrderPendingDg.Columns[1].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[1].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[2].Header = "High Priority";
                    mntcViewWorkOrderPendingDg.Columns[2].Width = 76;
                    mntcViewWorkOrderPendingDg.Columns[2].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[2].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[3].Header = "Service Type";
                    mntcViewWorkOrderPendingDg.Columns[3].Width = 150;
                    mntcViewWorkOrderPendingDg.Columns[3].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[4].Header = "Service Description";
                    mntcViewWorkOrderPendingDg.Columns[4].Width = 236;
                    mntcViewWorkOrderPendingDg.Columns[4].CanUserReorder = false;
                }
                else
                {
                    dataObjects = new List<dynamic>();
                    dynamic myDynamic = new
                    {
                        PropertyOne = true,
                        PropertyTwo = "Null",
                        PropertyThree = "No Incomplete Service Orders"
                    };

                    dataObjects.Add(myDynamic);

                    mntcViewWorkOrderPendingDg.ItemsSource = dataObjects;
                    mntcViewWorkOrderPendingDg.Columns[0].Visibility = Visibility.Hidden;
                    mntcViewWorkOrderPendingDg.Columns[1].Visibility = Visibility.Hidden;
                    mntcViewWorkOrderPendingDg.Columns[0].DisplayIndex = 4;
                    mntcViewWorkOrderPendingDg.Columns[1].DisplayIndex = 4;
                    mntcViewWorkOrderPendingDg.Columns[0].Header = "";
                    mntcViewWorkOrderPendingDg.Columns[0].Width = 120;
                    mntcViewWorkOrderPendingDg.Columns[0].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[0].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[1].Header = "";
                    mntcViewWorkOrderPendingDg.Columns[1].Width = 127;
                    mntcViewWorkOrderPendingDg.Columns[1].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[1].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[2].Header = "High Priority";
                    mntcViewWorkOrderPendingDg.Columns[2].Width = 76;
                    mntcViewWorkOrderPendingDg.Columns[2].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[2].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[3].Header = "Service Type";
                    mntcViewWorkOrderPendingDg.Columns[3].Width = 150;
                    mntcViewWorkOrderPendingDg.Columns[3].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[4].Header = "Service Description";
                    mntcViewWorkOrderPendingDg.Columns[4].Width = 236;
                    mntcViewWorkOrderPendingDg.Columns[4].CanUserReorder = false;

                }

                mntcViewWorkOrderPendingDg.Items.Refresh();
            }
            else
            {
                try
                {
                    _serviceOrderManager = new ServiceOrderManager();
                    _serviceOrders = new List<ServiceOrder_VM>(_serviceOrderManager.GetALlServiceOrders());
                    if (_serviceOrders.Count == 0)
                    {
                        MessageBox.Show("There Are No Work Orders");
                        return;
                    }

                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(ArgumentException))
                    {
                        _serviceOrders = new List<ServiceOrder_VM>();
                    }
                }
                
                List<dynamic> dataObjects = new List<dynamic>();
                foreach (ServiceOrder_VM serviceOrder in _serviceOrders)
                {
                    bool criticalIssue = serviceOrder.Critical_Issue;
                    string serviceType_ID = serviceOrder.Service_Type_ID;
                    string serviceDescription = serviceOrder.Service_Description;
                    int serviceOrderID = serviceOrder.Service_Order_ID;
                    Button updateButton = new Button();
                    updateButton.Height = 30;
                    updateButton.Width = 80;
                    updateButton.Content = "Update";
                    Button completeButton = new Button();
                    completeButton.Height = 30;
                    completeButton.Width = 80;
                    completeButton.Content = "Complete";

                    dynamic myDynamic = new
                    {
                        PropertyOne = criticalIssue,
                        PropertyTwo = serviceType_ID,
                        PropertyThree = serviceDescription,
                        PropertyFour = updateButton,
                        PropertyFive = completeButton,
                        PropertySix = serviceOrderID
                    };
                    dataObjects.Add(myDynamic);
                }


                if (dataObjects.Count != 0)
                {
                    mntcViewWorkOrderPendingDg.ItemsSource = dataObjects;
                    mntcViewWorkOrderPendingDg.Columns[0].Visibility = Visibility.Visible;
                    mntcViewWorkOrderPendingDg.Columns[1].Visibility = Visibility.Visible;
                    mntcViewWorkOrderPendingDg.Columns[5].Visibility = Visibility.Hidden;
                    mntcViewWorkOrderPendingDg.Columns[6].Visibility = Visibility.Hidden;
                    mntcViewWorkOrderPendingDg.Columns[7].Visibility = Visibility.Hidden;
                    mntcViewWorkOrderPendingDg.Columns[0].DisplayIndex = 4;
                    mntcViewWorkOrderPendingDg.Columns[1].DisplayIndex = 4;
                    mntcViewWorkOrderPendingDg.Columns[0].Header = "";
                    mntcViewWorkOrderPendingDg.Columns[0].Width = 120;
                    mntcViewWorkOrderPendingDg.Columns[0].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[0].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[1].Header = "";
                    mntcViewWorkOrderPendingDg.Columns[1].Width = 127;
                    mntcViewWorkOrderPendingDg.Columns[1].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[1].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[2].Header = "High Priority";
                    mntcViewWorkOrderPendingDg.Columns[2].Width = 76;
                    mntcViewWorkOrderPendingDg.Columns[2].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[2].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[3].Header = "Service Type";
                    mntcViewWorkOrderPendingDg.Columns[3].Width = 150;
                    mntcViewWorkOrderPendingDg.Columns[3].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[4].Header = "Service Description";
                    mntcViewWorkOrderPendingDg.Columns[4].Width = 236;
                    mntcViewWorkOrderPendingDg.Columns[4].CanUserReorder = false;
                }
                else
                {
                    dataObjects = new List<dynamic>();
                    dynamic myDynamic = new
                    {
                        PropertyOne = true,
                        PropertyTwo = "Null",
                        PropertyThree = "No Complete Service Orders"
                    };

                    dataObjects.Add(myDynamic);

                    mntcViewWorkOrderPendingDg.ItemsSource = dataObjects;
                    mntcViewWorkOrderPendingDg.Columns[0].Visibility = Visibility.Hidden;
                    mntcViewWorkOrderPendingDg.Columns[1].Visibility = Visibility.Hidden;
                    mntcViewWorkOrderPendingDg.Columns[0].DisplayIndex = 4;
                    mntcViewWorkOrderPendingDg.Columns[1].DisplayIndex = 4;
                    mntcViewWorkOrderPendingDg.Columns[0].Header = "";
                    mntcViewWorkOrderPendingDg.Columns[0].Width = 120;
                    mntcViewWorkOrderPendingDg.Columns[0].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[0].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[1].Header = "";
                    mntcViewWorkOrderPendingDg.Columns[1].Width = 127;
                    mntcViewWorkOrderPendingDg.Columns[1].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[1].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[2].Header = "High Priority";
                    mntcViewWorkOrderPendingDg.Columns[2].Width = 76;
                    mntcViewWorkOrderPendingDg.Columns[2].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[2].IsReadOnly = true;
                    mntcViewWorkOrderPendingDg.Columns[3].Header = "Service Type";
                    mntcViewWorkOrderPendingDg.Columns[3].Width = 150;
                    mntcViewWorkOrderPendingDg.Columns[3].CanUserReorder = false;
                    mntcViewWorkOrderPendingDg.Columns[4].Header = "Service Description";
                    mntcViewWorkOrderPendingDg.Columns[4].Width = 236;
                    mntcViewWorkOrderPendingDg.Columns[4].CanUserReorder = false;

                }

                mntcViewWorkOrderPendingDg.Items.Refresh();
            }

        }

        private void VehicleListbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VehicleMaintenancePage vehicleMaintenancePage = new VehicleMaintenancePage();
                NavigationService.Navigate(vehicleMaintenancePage);
            }
            catch (Exception)
            {
                MessageBox.Show("Error loading vehicle maintenance list page.");

            }
        }
    }
}
