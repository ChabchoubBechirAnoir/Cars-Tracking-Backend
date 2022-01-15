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
    public class VehiculeController : ControllerBase
    {
        private readonly ILogger<VehiculeController> _logger;
        private readonly RoutesDataService routesService;

        public VehiculeController(ILogger<VehiculeController> logger, RoutesDataService routesDataService)
        {
            _logger = logger;
            routesService = routesDataService;
        }

        [HttpGet]
        public IEnumerable<Vehicule> Get()
        {
            var routesData = routesService.GetAsyncVehicule().Result;
            return routesData;
        }
        [HttpPost]
        public string Post(Vehicule newVehicule)
        {
            _ = routesService.CreateAsyncVehicule(newVehicule);
            return newVehicule.Id;
        }
        [HttpDelete]
        public string Delete(string id)
        {
            _ = routesService.RemoveAsyncVehicule(id);
            return id;
        }
    }
}
