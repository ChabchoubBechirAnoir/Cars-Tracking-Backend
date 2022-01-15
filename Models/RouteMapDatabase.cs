using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapFollow.Models
{
    public class RouteMapDatabase
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string RoutesCollectionName { get; set; } = null!;

        public string VehiculesCollectionName { get; set; } = null!;
    }
}
