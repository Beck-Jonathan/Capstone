using DataObjects;
using LogicLayer;
using LogicLayer.ServiceOrder;
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

namespace NightRiderWPF.Maintenance
{
    /// <summary>
    /// Max Fare
    /// Created: 2024-03-02
    /// Interaction logic for AddEditDeleteScheduledMaintenance.xaml
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public partial class AddEditDeleteScheduledMaintenance : Page
    {
        private IVehicleModelManager _vehicleModelManager = null;
        private IServiceOrderManager _serviceTypeManager = null;
        private IMaintenanceScheduleManager _maintenanceScheduleManager = null;
        private IEnumerable<VehicleModel> _models = null;
        private VehicleModel _model = null;

        //Constructor for Adding new Scheduled Maintenance
        public AddEditDeleteScheduledMaintenance()
        {
            InitializeComponent();
            _serviceTypeManager = new ServiceOrderManager();
            _vehicleModelManager = new VehicleModelManager();
            _maintenanceScheduleManager = new MaintenanceScheduleManager();

            FillAddPage();
        }

        public void FillAddPage()
        {
            cmbService.Items.Clear();
            cmbModel.Items.Clear();
            txtServiceDescription.Focusable = false;
            
            _models = _vehicleModelManager.GetVehicleModels();
            IEnumerable<ServiceOrder_VM> serviceOrders = _serviceTypeManager.GetAllServiceTypes();
            List<ServiceTypeVM> serviceTypes = new List<ServiceTypeVM>();
            foreach(var so in serviceOrders)
            {
                serviceTypes.Add(new ServiceTypeVM
                {
                    ServiceTypeID =  so.Service_Type_ID,
                    ServiceDescription = so.Service_Description,
                    IsActive = so.Is_Active
                });

            }

            serviceTypes.OrderBy(service => service.ServiceTypeID);
            _models.OrderBy(model => model.Name);

            foreach (ServiceTypeVM service in serviceTypes)
            {
                cmbService.Items.Add(service.ServiceTypeID);
            }
            foreach (VehicleModel model in _models)
            {
                cmbModel.Items.Add(model.Year + ", " + model.Make + ", " + model.Name);
            }


        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MaintenanceScheduleVM maintenance = null;
            string service = null;

            VehicleModel vehicleModel = null;
            string[] model = null;
            string modelName = null;
            string make = null;
            int year = -1;

            int months = -1;
            int miles = 0;

            try
            {
                if (cmbService.SelectedItem != null
                    && cmbModel.SelectedItem != null)
                {
                    service = cmbService.SelectedItem.ToString();
                    
                    model = cmbModel.SelectedItem.ToString().Split(',');
                    year = Convert.ToInt32(model[0].Trim());
                    make = model[1].Trim();
                    modelName = model[2].Trim();
                }
                else
                {
                    throw new ArgumentException("Must have a service and a model to apply maintenance to.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("All fields with '*' must have a value", "Input Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                months = Convert.ToInt32(txtMonths.Text.Trim());
                if(months == 0)
                {
                    MessageBox.Show("Months field must be a number grater than 0.");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Months field should be a number greater than zero!");
                return;
            }
            try
            {
                if (txtMiles.Text.Length > 0)
                {
                    miles = Convert.ToInt32(txtMiles.Text.Trim());
                    if(miles > 0 && miles < 1000)
                    {
                        MessageBoxResult result = MessageBox.Show("This mileage is not measured in thousands, would you like to change the milage?", "Change Mileage?",
                         MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            txtMiles.Text = "";
                            return;
                        }
                    }
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure that this maintenance should not be scheduled by Mileage?", "Add Mileage?",
                         MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Miles field should be a number!");
                return;
            }
            try
            {
                foreach (VehicleModel m in _models)
                {
                    if (m.Year == year && m.Make.Equals(make) && m.Name.Equals(modelName))
                    {
                        vehicleModel = m;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Something went wrong!");
                return;
            }
            //create the maintenance schedule object
            try
            {
                IEnumerable<ServiceOrder_VM> serviceOrders = _serviceTypeManager.GetAllServiceTypes();
                List<ServiceType> serviceTypes = new List<ServiceType>();
                foreach (var so in serviceOrders)
                {
                    serviceTypes.Add(new ServiceType
                    {
                        ServiceTypeID = so.Service_Type_ID,
                        ServiceDescription = so.Service_Description,
                        IsActive = so.Is_Active
                    });

                }
                maintenance = new MaintenanceScheduleVM()
                {
                    ModelID = vehicleModel.VehicleModelID,
                    ServiceTypeID = service,
                    FrequencyInMonths = months,
                    FrequencyInMiles = miles,
                    TimeLastCompleted = DateTime.Now,
                    IsActive = true,
                    Model = vehicleModel,
                    ServiceType = serviceTypes.First(s => s.ServiceTypeID == service)
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to make a schedule.", "Oops");
                return;
            }

            try
            {
                int id = _maintenanceScheduleManager.AddScheduledMaintenance(maintenance);
                if (id != -1)
                {
                    MessageBox.Show("Successfully added Maintenance Schedule #" + id + ".", "Success!");
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Something went wrong, not added to database.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Failed to add to database");
            }
        }

        private void cmbService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<ServiceOrder_VM> serviceOrders = _serviceTypeManager.GetAllServiceTypes();
            List<ServiceTypeVM> serviceTypes = new List<ServiceTypeVM>();
            foreach(var so in serviceOrders)
            {
                serviceTypes.Add(new ServiceTypeVM
                {
                    ServiceTypeID =  so.Service_Type_ID,
                    ServiceDescription = so.Service_Description,
                    IsActive = so.Is_Active
                });

            }
            foreach(var s in serviceTypes)
            {
                if (s.ServiceTypeID.Equals(cmbService.SelectedValue))
                {
                    txtServiceDescription.Text = s.ServiceDescription;
                }
            }
        }
    }
}
