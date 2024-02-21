using DataObjects;
using LogicLayer;
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
    public partial class ViewWorkOrderPage : Page
    {
        IServiceOrderManager _serviceOrderManager = null;
        List<ServiceOrder_VM> _serviceOrders = null;

        public ViewWorkOrderPage()
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _serviceOrderManager = new ServiceOrderManager();
                _serviceOrders = new List<ServiceOrder_VM>(_serviceOrderManager.GetALlServiceOrders());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            List<dynamic> dataObjects = new List<dynamic>();
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

        private void mntcViewWorkOrderUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            // Action for update work order
            MessageBox.Show("This button's 'mntcViewWorkOrderUpdateBtn_Click()' method is not implemented");
        }

        private void mntcViewWorkOrderCompleteBtn_Click(object sender, RoutedEventArgs e)
        {
            // Action for complete work order
            MessageBox.Show("This button's 'mntcViewWorkOrderCompleteBtn_Click()' method is not implemented");
        }

        private void mntcViewWorkOrderCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            // Action to reurn to the previous page
            MessageBox.Show("This button's 'mntcViewWorkOrderCloseBtn_Click()' method is not implemented");
        }
    }
}
