using SmartLaunchUserImport.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Smartlaunch.Api.Service.Controllers
{
    public class UserLoginController : ApiController
    {

        // GET api/<controller>/5
        public bool Get(string username, string password)
        {
            var sl = new MySqlHelper();
            return sl.IsValidUser(username, password);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}