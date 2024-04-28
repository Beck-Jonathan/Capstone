/// <summary>
/// AUTHOR: Michael Springer
/// DATE: 2024-04-19
/// Script for handling bing map api
/// </summary>
/// <remarks>
/// DOCUMENTATION: https://learn.microsoft.com/en-us/bingmaps/v8-web-control/
/// <br /><br />
///
/// </remarks>


"use strict";

const CRLAT = 41.9779;
const CRLONG = -91.6656;
const DEFAULTZOOM = 10;

//function initializeMap(mapId, bingMapsKey) {
//    var map = new Microsoft.Maps.Map(document.getElementById(mapId), {
//        credentials: bingMapsKey,
//        center: new Microsoft.Maps.Location(CRLAT, CRLONG),
//        zoom: DEFAULTZOOM
//    });
//}


/// <summary>
/// AUTHOR: Michael Springer
/// DATE: 2024-04-19
/// Gets id data and sets up the actual map within the respective div on the Index view
/// </summary>
/// <remarks>
/// 
/// <br /><br />
///
/// </remarks>
function loadMapScenario() {
    var bingMapsKey = $('#mapConfig').data('bing-maps-key');
    var routesDataString = $('#routesData').attr('data-routes'); // Get the JSON string
    // console.log(routesDataString); // DEBUGG: testing the JSON string

    try {
        var routesData = JSON.parse(routesDataString); // Attempt to parse the JSON string
        console.log(routesData); // If parsing is successful, log the resulting object
    } catch (e) {
        console.error("Error parsing JSON:", e); // Log any errors during parsing
    }

    $('[data-map-id]').each(function () {
        var mapId = $(this).data('map-id');
        var routeId = mapId.split('-')[1]; // get the route id from the map id (which was derived from the route id originally)
        var route = routesData.find(r => r.RouteId.toString() === routeId);

        /*var route = routesData.find(r => r.RouteId.toString() === routeId);*/
        console.log('Initializing map for:', mapId)
        initializeMap(mapId, bingMapsKey, route);
    });
}


/// <summary>
/// AUTHOR: Michael Springer
/// DATE: 2024-04-19
/// uses the route data and the BingMaps api to pin the stops and draw lines between them
/// </summary>
/// <remarks>
///
/// <br /><br />
///
/// </remarks>
function initializeMap(mapId, bingMapsKey, route) {
    var mapElement = document.getElementById(mapId);
    var map = new Microsoft.Maps.Map(mapElement, {
        credentials: bingMapsKey,
        mapTypeId: Microsoft.Maps.MapTypeId.road,
        zoom: DEFAULTZOOM
    });

    // Get the direction module
    Microsoft.Maps.loadModule('Microsoft.Maps.Directions', function () {
        var directionsManager = new Microsoft.Maps.Directions.DirectionsManager(map);
        directionsManager.setRenderOptions({ itineraryContainer: document.getElementById('printoutPanel') });

        // Add waypoint object for each stop.
        route.RouteStops.forEach(function (stop) {
            var waypoint = new Microsoft.Maps.Directions.Waypoint({
                location: new Microsoft.Maps.Location(parseFloat(stop.stop.Latitude), parseFloat(stop.stop.Longitude))
            });
            directionsManager.addWaypoint(waypoint);
        });
        // add the first stop again as the ending point
        var waypoint = new Microsoft.Maps.Directions.Waypoint({
            location: new Microsoft.Maps.Location(parseFloat(route.RouteStops[0].stop.Latitude), parseFloat(route.RouteStops[0].stop.Longitude))
        });
        directionsManager.addWaypoint(waypoint);

        // Set some parameters
        directionsManager.setRequestOptions({
            routeMode: Microsoft.Maps.Directions.RouteMode.driving,
            distanceUnit: Microsoft.Maps.Directions.DistanceUnit.miles,
            routeDraggable: false
        });

        // Reset the zoom properly after the directions are calculated and updated
        Microsoft.Maps.Events.addHandler(directionsManager, 'directionsUpdated', function (args) {
            var desiredZoom = DEFAULTZOOM;
            var centerPoint = new Microsoft.Maps.Location(CRLAT, CRLONG);  // Customize as needed
            map.setView({ center: centerPoint, zoom: desiredZoom });
        });

        // Calculate the route.
        directionsManager.calculateDirections();

    });
}


// OLD CODE FOR REFERENCE
//function initializeMap(mapId, bingMapsKey, route) {
//    var mapDiv = document.getElementById(mapId);

//    // default to cender of CR if no stops or failed parse
//    var centerLat = (route.RouteStops.length > 0) ? parseFloat(route.RouteStops[0].stop.Latitude) : CRLAT;
//    var centerLong = (route.RouteStops.length > 0) ? parseFloat(route.RouteStops[0].stop.Longitude) : CRLONG;


//    var map = new Microsoft.Maps.Map(mapDiv, {
//        credentials: bingMapsKey,
//        center: new Microsoft.Maps.Location(centerLat, centerLong),
//        zoom: DEFAULTZOOM
//    });

//    route.RouteStops.forEach(function (stop) {
//        var location = new Microsoft.Maps.Location(stop.stop.Latitude, stop.stop.Longitude);
//        var pin = new Microsoft.Maps.Pushpin(location, {
//            title: stop.stop.StreetAddress,

//        });
//        map.entities.push(pin);
//    })
//}