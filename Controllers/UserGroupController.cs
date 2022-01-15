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
using static MapFollow.Models.DBContexts;

namespace MapFollow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private MyDBContext myDbContext;

        public UserGroupController(MyDBContext context)
        {
            myDbContext = context;
        }

        [HttpGet]
        public IList<UserGroup> Get()
        {
            return (this.myDbContext.UserGroups.ToList());
        }
    }
}
