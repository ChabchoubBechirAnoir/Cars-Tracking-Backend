using MapFollow.Models;
using MapFollow.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using Microsoft.IdentityModel.Tokens;
using Realms.Sync.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static MapFollow.Models.DBContexts;

namespace MapFollow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private MyDBContext myDbContext;

        public UserController(MyDBContext context)
        {
            myDbContext = context;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User model)
        {
            if (string.IsNullOrEmpty(model.Mail) || string.IsNullOrEmpty(model.Password))
                return null;

            var user = myDbContext.Users.SingleOrDefault(x => x.Mail == model.Mail);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (model.Password != user.Password)
                return null;

            // authentication successful
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            IdentityOptions _options = new IdentityOptions();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("UserID",user.Id.ToString()),
                        new Claim("Role", user.UserGroupId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456")), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return Ok(new { token });
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User model)
        {
            
            try
            {
                // validation
                if (string.IsNullOrWhiteSpace(model.Password))
                    return BadRequest(new { message = "Password" });

                if (myDbContext.Users.Any(x => x.Mail == model.Mail))
                    return BadRequest(new { message = "Already Exist" });

                myDbContext.Users.Add(model);
                myDbContext.SaveChanges();
                return Ok(new { message = "Done" });

            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        //[AllowAnonymous]
        //[HttpPost("register")]
        //public IActionResult Register([FromBody] User model)
        //{
        //    // map model to entity
        //    var user = _mapper.Map<User>(model);

        //    try
        //    {
        //        // create user
        //        _userService.Create(user, model.Password);
        //        return Ok();
        //    }
        //    catch (AppException ex)
        //    {
        //        // return error message if there was an exception
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}
        [HttpGet]
        public IList<User> Get()
        {
            return (this.myDbContext.Users.ToList());
        }
    }
}
