using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LoginService.DAL;
using LoginService.Models;

namespace LoginService.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : ApiController
    {
        private UserManager userManager = new UserManager();

        // POST: api/user/login
        [Route("login")]
        [HttpPost]
        [ResponseType(typeof(TokenResponse))]
        public IHttpActionResult LoginUser(UserCredentials credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TokenResponse tokenResponse = userManager.LoginUser(credentials);

            if (tokenResponse != null)
            {
                return Ok(tokenResponse);
            }
            else
            {
                return BadRequest("Wrong credentials!");
            }
        }

        // POST: api/register
        [Route("register")]
        [HttpPost]
        public IHttpActionResult RegisterUser(RequestUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userManager.RegisterUser(user))
            {
                return Ok("User successfully created.");
            }
            else
            {
                return BadRequest("Login is already taken.");
            }
        }

    }
}