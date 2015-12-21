using Smartlaunch.Api.Client.Models;
using SmartLaunch.Api.Client.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Smartlaunch.Api.Service.Controllers
{
    public class UserController : ApiController
    {

        // GET 
        [Route("api/userLogin/{username}/{password}")]
        [HttpGet]
        public bool UserLogin(string username, string password)
        {
            var sl = new MySqlHelper();
            return sl.IsValidUser(username, password);
        }

        [Route("api/getUser/{username}")]
        [HttpGet]
        public ExtendedUser GetUser(string username)
        {
            var sl = new MySqlHelper();
            return sl.GetUser(username);
        }

        [Route("api/changePassword/{username}/{password}")]
        [HttpPut]
        public void ChangePassword(string username, string password)
        {
            var sl = new MySqlHelper();
            sl.SetUserPassword(username, password);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}