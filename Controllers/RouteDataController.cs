using MapFollow.Models;
using MapFollow.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapFollow.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RouteDataController : ControllerBase
    {
        private readonly ILogger<RouteDataController> _logger;
        private readonly RoutesDataService routesService;

        public RouteDataController(ILogger<RouteDataController> logger, RoutesDataService routesDataService)
        {
            _logger = logger;
            routesService = routesDataService;
        }

        [HttpGet]
        public IEnumerable<RouteData> Get()
        {
            var routesData = routesService.GetAsync().Result;
            return routesData;
        }
        [HttpPost]
        public string Post(RouteData newRouteData)
        {
            _ = routesService.CreateAsync(newRouteData);
            return newRouteData.Id;
        }
        [HttpDelete]
        public string Delete(string id)
        {
            _ = routesService.RemoveAsync(id);
            return id;
        }
    }
}
