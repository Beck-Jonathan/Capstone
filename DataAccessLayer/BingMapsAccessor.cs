using DataAccessInterfaces;
using DataObjects.RouteObjects;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Remoting.Messaging;
using DataObjects.HelperObjects;
using System.Reflection.Emit;
using System.Device.Location;

namespace DataAccessLayer
{
    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// <br />
    /// CREATED: 2024-03-19
    /// <br />
    ///     Accessor class for the Bing Maps API.
    /// </summary>
    ///    UPDATER: Steven Sanchez
    /// <br />
    ///    UPDATED: 2024-04-06
    /// <br />
    ///     Added getMapPolylineForRouteAssignmentVM(IEnumerable<Route_Assignment_VM> stops)
    ///     and GetCurrentSystemLocation() for drivers route assignment
    /// </remarks>
    public class BingMapsAccessor : IBingMapsAccessor
    {
        private string _key;
        public BingMapsAccessor()
        {
            _key = "1QiIMAp08xaIF29LmivZ ~HfNjTuOpTOgAMPb8WVoong~An8kYuvnA_RTEYnNx3_csnKzrL_xvR-VYy7Q3Ri8bOiIu9tEj4rWndfsVMGwQgxV";
        }
        public async Task<BingMapsResponse> getMapPolyline(IEnumerable<RouteStopVM> stops)
        {
            BingMapsResponse output = null;
            if (stops != null && stops.Count() > 1)
            {

                HttpClient wc = new HttpClient();
                StringBuilder locationSet = new StringBuilder("");
                string response;
                List<RouteStopVM> stopList = stops.ToList();
                locationSet.Append($"?wayPoint.1={stopList[0].stop.Latitude},{stopList[0].stop.Longitude}");
                for (int i = 1; i < stopList.Count; i++)
                {
                    locationSet.Append($"&viaWaypoint.{i + 1}={stopList[i].stop.Latitude},{stopList[i].stop.Longitude}");
                }
                locationSet.Append($"&wayPoint.{stopList.Count + 1}={stopList[0].stop.Latitude} , {stopList[0].stop.Longitude}");
                locationSet.Append("&routeAttributes=routePath");
                locationSet.Append($"&distanceUnit=mi&key={_key}");
                try
                {
                    response = await GetResponse(new Uri("http://dev.virtualearth.net/REST/v1/Routes"), locationSet.ToString(), wc);

                    output = (BingMapsResponse)JsonConvert.DeserializeObject(response, typeof(BingMapsResponse));

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                wc.Dispose();
            }
            // interpret response to a MapPolyLine


            return output;
        }
        private async Task<string> GetResponse(Uri uri, string urlParameters, HttpClient wc)
        {
            wc.BaseAddress = uri;

            wc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await wc.GetAsync(urlParameters);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        public async Task<BingMapsResponse> getMapPolylineForRouteAssignmentVM(IEnumerable<Route_Assignment_VM> stops)
        {
            BingMapsResponse output = null;
            if (stops != null && stops.Any())
            {
                HttpClient wc = new HttpClient();
                StringBuilder locationSet = new StringBuilder("");
                List<Route_Assignment_VM> stopList = stops.ToList();

                // Add the system's location as the start waypoint
                Location systemLocation = await GetCurrentSystemLocation();
                if (systemLocation != null)
                {
                    locationSet.Append($"?wayPoint.1={systemLocation.Latitude},{systemLocation.Longitude}");

                    // Add the first stop as the end waypoint
                    locationSet.Append($"&wayPoint.2={stopList[0].stop.Latitude},{stopList[0].stop.Longitude}");

                    locationSet.Append("&routeAttributes=routePath");
                    locationSet.Append($"&distanceUnit=mi&key={_key}");

                    try
                    {
                        string response = await GetResponse(new Uri("http://dev.virtualearth.net/REST/v1/Routes"), locationSet.ToString(), wc);
                        output = JsonConvert.DeserializeObject<BingMapsResponse>(response);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        wc.Dispose();
                    }
                }
            }
            return output;
        }


        private async Task<Location> GetCurrentSystemLocation()
        {
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
            GeoCoordinate coord = null;

            watcher.PositionChanged += (sender, e) =>
            {
                coord = e.Position.Location;
            };
            watcher.Start();
            await Task.Delay(1000);
            watcher.Stop();
            if (coord != null)
            {
                return new Location(coord.Latitude, coord.Longitude);
            }
            else
            {
                return new Location(0, 0); // Default location at (0, 0)
            }
        }
    }
}