using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataObjects.HelperObjects
{
    /// <summary>
    /// AUTHOR: Nathan Toothaker
    /// <br />
    /// CREATED: 2024-03-04
    /// <br />
    /// 
    ///     C# representation of JSON data obtained from the Bing Maps API.
    /// </summary>
    /// 
    [JsonObject()]
    public class BingMapsResponse
    {
        [JsonProperty("resourceSets")]
        public List<ResourceSets> ResourceSets { get; set; }
    }
    [JsonObject()]
    public class ResourceSets
    {
        [JsonProperty("resources")]
        public List<Resources> resources { get; set; }
    }
    [JsonObject()]
    public class Resources
    {
        [JsonProperty("routePath")]
        public RoutePath routePath { get; set; }
    }
    [JsonObject()]
    public class RoutePath
    {
        [JsonProperty("line")]
        public Line line { get; set; }
    }
    public class Line
    {
        [JsonProperty("coordinates")]
        public IEnumerable<IEnumerable<Double>> coordinates { get; set; }
    }
}
