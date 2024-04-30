using DataObjects;
using LogicLayer;
using LogicLayer.Client;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NightRiderWPF.Clients
{
    /// <summary>
    /// Interaction logic for GuardianViewDependentList.xaml
    /// </summary>
    public partial class GuardianViewDependentList : Page
    {
        DependentVM _dependentVM = null;
        DependentManager _dependentManager = null;
        Client _client = null;
        // Checks for Select all Dependents or Select by ClientID
        bool _isSelectedByClient;

        public GuardianViewDependentList()
        {
            InitializeComponent();
            _isSelectedByClient = false;
        }

        public GuardianViewDependentList(Client client)
        {
            InitializeComponent();
            this._client = client;
            _isSelectedByClient = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<DependentVM> dependents = null;
            List<dynamic> displayDependents = new List<dynamic>();
            if (datDependentList.ItemsSource == null)
            {

                var dependentManager = new DependentManager();
                if (_isSelectedByClient)
                {
                    try
                    {   // Get dependents
                        dependents = dependentManager.GetDependentListByClientId(_client.ClientID).ToList();
                        ClientDependentRoleManager cdrManager = new ClientDependentRoleManager();
                        List<ClientDependentRole_VM> relationships = cdrManager.GetClientDependentRolesByClient(_client.ClientID).ToList();

                        foreach (var dependent in dependents)
                        {
                            List<ClientDependentRole> cdRoles = new List<ClientDependentRole>(); ;
                            foreach (var role in relationships)
                            {
                                if (role.DependentID == dependent.DependentID)
                                {
                                    cdRoles.Add(role);
                                }
                            }
                            dependent.ClientDependentRoles = cdRoles;
                        }
                        datDependentList.ItemsSource = dependents;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Could not find Dependent List");
                        dependents = dependentManager.GetDependentList().ToList();
                        datDependentList.ItemsSource = dependents;
                    }
                }
                else
                {
                    MessageBox.Show("Client Not found");

                }
            }
            if (dependents != null)
            {   // generating a list of dependent properties for the displaygrid
                foreach (DependentVM dependent in dependents)
                {
                    string name = dependent.FamilyName + ", " + dependent.GivenName;
                    string DOB = dependent.DOB.ToShortDateString();
                    string gender = dependent.Gender;
                    string emergencyContact = dependent.EmergencyContact;
                    string emergencyRelationship = dependent.ContactRelationship;
                    string emergencyPhone = dependent.EmergencyPhone;
                    string relationship = "";
                    int dependentID = dependent.DependentID;
                    foreach (var role in dependent.ClientDependentRoles)
                    {
                        if (role.ClientID == _client.ClientID)
                        {
                            relationship = role.Relationship;
                        }
                    }
                    dynamic dependentDisplay = new
                    {
                        PropertyOne = name,
                        PropertyTwo = relationship,
                        PropertyThree = DOB,
                        PropertyFour = gender,
                        PropertyFive = emergencyContact,
                        PropertySix = emergencyRelationship,
                        PropertySeven = emergencyPhone,
                        PropertyEight = dependentID
                    };
                    displayDependents.Add(dependentDisplay);
                }

                if (displayDependents.Count > 0)
                {
                    datDependentList.ItemsSource = displayDependents;
                    datDependentList.Columns[0].DisplayIndex = 8;
                    datDependentList.Columns[0].Header = "Update";
                    datDependentList.Columns[1].DisplayIndex = 8;
                    datDependentList.Columns[1].Header = "Delete";

                    datDependentList.Columns[2].Header = "Name";
                    datDependentList.Columns[3].Header = "Client Relationship";
                    datDependentList.Columns[4].Header = "DOB";
                    datDependentList.Columns[5].Header = "Gender";
                    datDependentList.Columns[6].Header = "Emer. Contact";
                    datDependentList.Columns[7].Header = "Emer. Relationship";
                    datDependentList.Columns[8].Header = "Emer. Phone";
                    datDependentList.Columns[9].Visibility = Visibility.Collapsed;


                }
            }
        }

        private void datDependentList_AutoGeneratedColumns(object sender, EventArgs e)
        {
            datDependentList.Columns.RemoveAt(0);
            datDependentList.Columns.RemoveAt(0);
            datDependentList.Columns.RemoveAt(2);
            datDependentList.Columns.RemoveAt(3);
            datDependentList.Columns.RemoveAt(4);
            datDependentList.Columns.RemoveAt(5);
            datDependentList.IsReadOnly = true;
        }
        /// <summary>
        /// Michael Springer
        /// Created: 2024-03/03
        /// 
        /// Loads dependent view based on selected dependent
        /// </summary>
        ///
        private void btnUpdateDependent_Click(object sender, RoutedEventArgs e)
        {
            var dep = datDependentList.SelectedItem;
            var nameOfProperty = "PropertyEight";
            var propertyInfo = dep.GetType().GetProperty(nameOfProperty);
            int depID = int.Parse(propertyInfo.GetValue(dep, null).ToString());

            this.NavigationService.Navigate(new GuardianViewEditDependent(_client.ClientID, depID));
        }

        private void btnDeleteDependent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("No page to go back to.");
            }
        }
    }
}
