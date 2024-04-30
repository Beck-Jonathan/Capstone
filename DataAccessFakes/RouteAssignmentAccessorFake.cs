using DataAccessInterfaces;
using DataObjects;
using DataObjects.Assignment;
using DataObjects.RouteObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessFakes
{
    /// <summary>
    /// AUTHOR: Steven Sanchez
    /// DATE: 2024-03-24
    /// Fake Database Access for Route Assignment Unit Tests
    /// </summary>
    ///    UPDATER: 
    /// <br />
    ///    UPDATED: 
    /// <br />
    ///    Update Comments
    /// </remarks>
    public class RouteAssignmentAccessorFake : IRouteAssignmentAccessor
    {
        private List<Route_Assignment_VM> route_Assignments;
        private List<Route_Assignment> _assignments = new List<Route_Assignment>();
        private List<DriverUnavailability> _driverUnavailablities = new List<DriverUnavailability>();
        private List<VehicleUnavailability> _vehicleUnavailabilities = new List<VehicleUnavailability>();
        private List<Driver> _drivers = new List<Driver>();
        private List<VehicleAssignment> _vehicles = new List<VehicleAssignment>();

        public RouteAssignmentAccessorFake()
        {
            route_Assignments = new List<Route_Assignment_VM>
            {
                new Route_Assignment_VM()
                {
                    Assignment_ID = 1,
                    DriverID = 1,
                    Route_ID = 1,
                    VIN_Number = "12345678901234567",
                    Date_Assignment_Started = DateTime.Now,
                    Date_Assignment_Ended = DateTime.Now,
                },
                new Route_Assignment_VM()
                {
                    Assignment_ID = 4,
                    DriverID = 1,
                    Route_ID = 3,
                    VIN_Number = "123456789012345SS",
                    Date_Assignment_Started = DateTime.Now,
                    Date_Assignment_Ended = DateTime.Now,
                },
                new Route_Assignment_VM()
                {
                    Assignment_ID = 2,
                    DriverID = 2,
                    Route_ID = 2,
                    VIN_Number = "09876543210987654",
                    Date_Assignment_Started = DateTime.Now,
                    Date_Assignment_Ended = DateTime.Now,
                },
                 new Route_Assignment_VM()
                {
                    Assignment_ID = 3,
                    DriverID = 3,
                    Route_ID = 3,
                    VIN_Number = "asdfghjklqwe12346",
                    Date_Assignment_Started = DateTime.Now,
                    Date_Assignment_Ended = DateTime.Now,
                }

            };

            _assignments.Add(new Route_Assignment()
            {
                Assignment_ID = 1,
                Route_ID = 1,
                Date_Assignment_Started = new DateTime(2000, 01, 01),
                Date_Assignment_Ended = new DateTime(2000, 01, 02),
                IsActive = true,
                DriverID = 1,
                VIN_Number = "ABCDEFGHIJKLMNOPQ"
            });
            _assignments.Add(new Route_Assignment()
            {
                Assignment_ID = 2,
                Route_ID = 2,
                Date_Assignment_Started = new DateTime(2001, 01, 01),
                Date_Assignment_Ended = new DateTime(2001, 01, 02),
                IsActive = true,
                DriverID = 2,
                VIN_Number = "12345678901234567"
            });
            _assignments.Add(new Route_Assignment()
            {
                Assignment_ID = 3,
                Route_ID = 3,
                Date_Assignment_Started = new DateTime(2002, 01, 01),
                Date_Assignment_Ended = new DateTime(2002, 01, 02),
                IsActive = true,
                DriverID = 3,
                VIN_Number = "ABCDEFGH123456789"
            });

            _vehicleUnavailabilities.Add(new VehicleUnavailability()
            {
                Unavailability_ID = 1,
                VIN = "ABCDEFGHIJKLMNOPQ",
                Start_Date = new DateTime(2000, 01, 01),
                End_Date = new DateTime(2000, 01, 02),
                IsActive = true
            });
            _vehicleUnavailabilities.Add(new VehicleUnavailability()
            {
                Unavailability_ID = 2,
                VIN = "12345678901234567",
                Start_Date = new DateTime(2001, 01, 01),
                End_Date = new DateTime(2001, 01, 02),
                IsActive = true
            });
            _vehicleUnavailabilities.Add(new VehicleUnavailability()
            {
                Unavailability_ID = 3,
                VIN = "ABCDEFGH123456789",
                Start_Date = new DateTime(2002, 01, 01),
                End_Date = new DateTime(2002, 01, 02),
                IsActive = true
            });
            _driverUnavailablities.Add(new DriverUnavailability()
            {
                UnavailableID = 1,
                DriverID = 1,
                StartDate = new DateTime(2000, 01, 01),
                EndDate = new DateTime(2000, 01, 02),
                Reason = "",
                IsActive = true
            });
            _driverUnavailablities.Add(new DriverUnavailability()
            {
                UnavailableID = 2,
                DriverID = 2,
                StartDate = new DateTime(2001, 01, 01),
                EndDate = new DateTime(2001, 01, 02),
                Reason = "",
                IsActive = true
            });
            _driverUnavailablities.Add(new DriverUnavailability()
            {
                UnavailableID = 3,
                DriverID = 3,
                StartDate = new DateTime(2002, 01, 01),
                EndDate = new DateTime(2002, 01, 02),
                Reason = "",
                IsActive = true
            });

            _drivers.Add(new Driver()
            {
                Employee_ID = 1,
                Given_Name = "Bob",
                Family_Name = "Trapp",
                Driver_License_Class_ID = "C",
                Max_Passenger_Count = 5
            });
            _drivers.Add(new Driver()
            {
                Employee_ID = 2,
                Given_Name = "Jim",
                Family_Name = "Glasgow",
                Driver_License_Class_ID = "CDL",
                Max_Passenger_Count = 20
            });
            _drivers.Add(new Driver()
            {
                Employee_ID = 3,
                Given_Name = "Marc",
                Family_Name = "Hauschildt",
                Driver_License_Class_ID = "C",
                Max_Passenger_Count = 5
            });
            _vehicles.Add(new VehicleAssignment()
            {
                VIN = "ABCDEFGHIJKLMNOPQ",
                Name = "Escape",
                Make = "Ford",
                Max_Passengers = 4
            });

            _vehicles.Add(new VehicleAssignment()
            {
                VIN = "12345678901234567",
                Name = "Transit 10",
                Make = "Ford",
                Max_Passengers = 10
            });
            _vehicles.Add(new VehicleAssignment()
            {
                VIN = "ABCDEFGH123456789",
                Name = "Collins Childcare 14PX",
                Make = "Ford",
                Max_Passengers = 14
            });

        }
        public IEnumerable<Route_Assignment_VM> GetAllRouteAssignmentByDriverID(int Driver_ID)
        {
            return route_Assignments.Where(a => a.DriverID == Driver_ID);
        }

        //Created By: James Williams
        //Date: 2024-04-17
        public int AddRouteAssignment(int driverID, string vin, int routeID, DateTime start, DateTime end)
        {

            Route_Assignment newAssignment = new Route_Assignment()
            {
                Assignment_ID = 4,
                DriverID = driverID,
                VIN_Number = vin,
                Route_ID = routeID,
                Date_Assignment_Started = start,
                Date_Assignment_Ended = end,

            };

            foreach (var assignment in _assignments)
            {
                if (assignment.Route_ID == newAssignment.Route_ID && assignment.DriverID == newAssignment.DriverID &&
                    assignment.VIN_Number == newAssignment.VIN_Number && assignment.Date_Assignment_Ended == newAssignment.Date_Assignment_Ended &&
                    assignment.Date_Assignment_Started == newAssignment.Date_Assignment_Started)
                {
                    throw new SystemException("Assignment already exists");
                }
            }
            return 1;

        }
        //Created By: James Williams
        //Date: 2024-04-17
        public int AddVehicleAndDriverUnavailabilities(string vin, int driverID, DateTime start, DateTime end, string reason)
        {
            int rows = 0;

            _driverUnavailablities.Add(new DriverUnavailability()
            {
                UnavailableID = 4,
                DriverID = driverID,
                StartDate = start,
                EndDate = end,
                Reason = "",
                IsActive = true
            });
            rows++;
            _vehicleUnavailabilities.Add(new VehicleUnavailability()
            {
                Unavailability_ID = 4,
                VIN = vin,
                Start_Date = start,
                End_Date = end,
                IsActive = true
            });
            rows++;
            return rows;
        }
        //Created By: James Williams
        //Date: 2024-04-17
        public List<Driver> GetAvailableDrivers(DateTime start, DateTime end, int passengerCount)
        {
            List<DriverUnavailability> unavailabilities = new List<DriverUnavailability>();
            List<Driver> availableDrivers = new List<Driver>();

            //This checks for overlapping dates between a driver's
            //unavailabilities and dates entered.
            foreach (var driver in _driverUnavailablities)
            {
                if ((driver.StartDate <= start && start <= driver.EndDate) ||
                    (driver.StartDate <= end && end <= driver.EndDate))
                {
                    unavailabilities.Add(driver);
                }
            }

            foreach (var driver in _drivers)
            {
                //This checks if there are any unavailabilities and if not,
                //Then checks the driver's max passenger count agains the passenger count needed
                if (!unavailabilities.Any(availability => availability.DriverID == driver.Employee_ID))
                {
                    if (driver.Max_Passenger_Count >= passengerCount)
                    {
                        availableDrivers.Add(driver);
                    }
                }
            }
            if (availableDrivers.Count == 0)
            {
                throw new SystemException("No available drivers");
            }
            return availableDrivers;
        }

        //Created By: James Williams
        //Date: 2024-04-17
        public List<VehicleAssignment> GetAvailableVehicles(DateTime start, DateTime end, int passengerCount)
        {
            List<VehicleUnavailability> unavailabilities = new List<VehicleUnavailability>();
            List<VehicleAssignment> availableVehicles = new List<VehicleAssignment>();

            foreach (var availability in _vehicleUnavailabilities)
            {
                if ((availability.Start_Date <= start && start <= availability.End_Date) ||
                    (availability.Start_Date <= end && end <= availability.End_Date))
                {
                    foreach (var vehicle in _vehicles)
                    {
                        if (vehicle.Max_Passengers >= passengerCount)
                        {
                            availableVehicles.Add(vehicle);
                        }
                    }
                }
            }

            if (availableVehicles.Count == 0)
            {
                throw new SystemException("No available vehicles");
            }
            return availableVehicles;
        }
        //Created By: James Williams
        //Date: 2024-04-17
        public List<Route_Assignment> GetRouteAssignmentsByRouteIDAndDate(int routeID, DateTime start, DateTime end)
        {
            List<Route_Assignment> routeAssignments = new List<Route_Assignment>();
            foreach (var assignment in _assignments)
            {
                if (assignment.Route_ID == routeID &&
                    assignment.Date_Assignment_Started >= start && assignment.Date_Assignment_Started <= end &&
                    assignment.Date_Assignment_Ended >= start && assignment.Date_Assignment_Ended <= end)
                {
                    routeAssignments.Add(assignment);
                }
            }
            if (routeAssignments.Count == 0)
            {
                throw new SystemException("No routes found");
            }
            return routeAssignments;
        }
        //Created By: James Williams
        //Date: 2024-04-26
        public Driver GetRouteAssignmentDriverByRouteAssignmentID(int routeAssignmentID)
        {
            Driver driverAssignment = null;
            int driverID = 0;
            foreach (var assignment in _assignments)
            {
                if (assignment.Assignment_ID == routeAssignmentID)
                {
                    driverID = assignment.DriverID;
                    break;
                }
            }
            if (driverID == 0)
            {
                throw new SystemException("No assingments found");
            }
            foreach (var driver in _drivers)
            {
                if (driver.Employee_ID == driverID)
                {
                    driverAssignment = driver;
                    break;
                }
            }
            return driverAssignment;
        }
        //Created By: James Williams
        //Date: 2024-04-26
        public List<Driver> GetAvailableDriversByDate(DateTime start, DateTime end)
        {
            List<DriverUnavailability> unavailabilities = new List<DriverUnavailability>();
            List<Driver> availableDrivers = new List<Driver>();

            if (start > end)
            {
                throw new SystemException("Start date must be less than or equal to end date");
            }
            //This checks for overlapping dates between a driver's
            //unavailabilities and dates entered.
            foreach (var driver in _driverUnavailablities)
            {
                if ((driver.StartDate <= start && start <= driver.EndDate) ||
                    (driver.StartDate <= end && end <= driver.EndDate))
                {
                    unavailabilities.Add(driver);
                }
            }

            foreach (var driver in _drivers)
            {
                //This checks if there are any unavailabilities and if not,
                //Then checks the driver's max passenger count agains the passenger count needed
                if (!unavailabilities.Any(availability => availability.DriverID == driver.Employee_ID))
                {
                    availableDrivers.Add(driver);
                }
            }
            return availableDrivers;
        }
        //Created By: James Williams
        //Date: 2024-04-26
        public List<VehicleAssignment> GetAvailableVehiclesByDate(DateTime start, DateTime end)
        {
            List<VehicleUnavailability> unavailabilities = new List<VehicleUnavailability>();
            List<VehicleAssignment> availableVehicles = new List<VehicleAssignment>();
            if (start > end)
            {
                throw new SystemException("Start date must be less than end date");
            }
            foreach (var availability in _vehicleUnavailabilities)
            {
                if ((availability.Start_Date <= start && start <= availability.End_Date) ||
                    (availability.Start_Date <= end && end <= availability.End_Date))
                {
                    foreach (var vehicle in _vehicles)
                    {
                        availableVehicles.Add(vehicle);
                    }
                }
            }

            if (availableVehicles.Count == 0)
            {
                throw new SystemException("No available vehicles");
            }
            return availableVehicles;
        }
        //Created By: James Williams
        //Date: 2024-04-26
        public int UpdateRouteAssignmentDriver(int routeAssignmentID, int driverID)
        {
            int rows = 0;
            foreach (var assignment in _assignments)
            {
                if (assignment.Assignment_ID == routeAssignmentID)
                {
                    assignment.DriverID = driverID;
                    rows++;
                    break;
                }
            }
            if (rows == 0)
            {
                throw new SystemException("Route not found");
            }
            return rows;
        }
        //Created By: James Williams
        //Date: 2024-04-26
        public int UpdateRouteAssignmentVehicle(int routeAssignmentID, string vin)
        {
            int rows = 0;

            foreach (var assignment in _assignments)
            {
                if (assignment.Assignment_ID == routeAssignmentID)
                {
                    assignment.VIN_Number = vin;

                    rows++;
                    break;
                }
            }
            if (rows == 0)
            {
                throw new SystemException("Route Assignment not found");
            }
            return rows;
        }
    }
}
