using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Smartlaunch.Api.Client.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Smartlaunch.Api.Client
{
    public class Smartlaunch
    {
        private Uri UriAddress { get; set; }
        private HttpClient _client;

        public Smartlaunch()
        {
            var server = ConfigurationManager.AppSettings.Get("SmartlaunchServer");
            Console.Out.WriteLine("Smartlaunch Server: " + server);
            NewConnection(server);
        }

        public Tuple<List<User>, List<ExtendedUser>> GetUsers()
        {
            List<User> Users = new List<User>();
            List<ExtendedUser> ExtendedUsers = new List<ExtendedUser>();
            string json = GETFunction("/users");
            Users = JsonConvert.DeserializeObject<List<User>>(json);
            ExtendedUsers = JsonConvert.DeserializeObject<List<ExtendedUser>>(json);
            return new Tuple<List<User>, List<ExtendedUser>>(Users, ExtendedUsers);
        }

        public ExtendedUser GetUser(string username)
        {
            List<User> Users = new List<User>();
            string json = GETFunction(String.Format("/users/{0}", username));
            User user = JsonConvert.DeserializeObject<User>(json);
            ExtendedUser extuser = JsonConvert.DeserializeObject<ExtendedUser>(json);
            extuser.User = user;

            return extuser;
        }

        public bool AddFunds(string username, double amount)
        {
            var response = AddMoney(username, amount);
            return true;
        }

        public HttpResponseMessage CreateUser(User user)
        {
            var u = JsonConvert.SerializeObject(user);
            var content = new StringContent(@"{""UserCreate"":" + u + " }",
                                            Encoding.UTF8, "application/json");
            return POSTFunction("/users", content);
        }

        public HttpResponseMessage UpdateUser(string username, ExtendedUser userinfo)
        {
            var u = JsonConvert.SerializeObject(userinfo);
            var content = new StringContent(@"{""UserUpdate"":" + u + " }",
                                            Encoding.UTF8, "application/json");
            return PUTFunction("/users/" + username, content);
        }

        public HttpResponseMessage AddMoney(string username, double money)
        {
            var content = new StringContent(@"{""UserAddMoney"":{""Amount"":" + money.ToString() + "}}",
                        Encoding.UTF8, "application/json");
            return PUTFunction("/users/" + username + "/addmoney", content);
        }


        private void NewConnection(string address, string port = "7833")
        {
            UriAddress = new Uri(String.Format("http://{0}:{1}", address, port));

            _client = new HttpClient();
            _client.BaseAddress = UriAddress;

        }

        private string GETFunction(string endpoint)
        {
            HttpResponseMessage response = _client.GetAsync(endpoint).Result;
            HttpContent stream = response.Content;
            var data = stream.ReadAsStringAsync();
            return data.Result;
        }

        private HttpResponseMessage POSTFunction(string endpoint, StringContent content)
        {
            HttpResponseMessage response = _client.PostAsync(endpoint, content).Result;
            return response;

        }

        private HttpResponseMessage PUTFunction(string endpoint, StringContent content)
        {
            HttpResponseMessage response = _client.PutAsync(endpoint, content).Result;
            return response;
        }

    }
}
