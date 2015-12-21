using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartlaunch.Api.Client;
using Smartlaunch.Api.Client.Models;
using SmartLaunchUserImport.Helpers;
using SmartLaunch.Api.Client.Helpers;

namespace SmartLaunchUserImport
{
    class Program
    {
        static void Main(string[] args)
        {
            var sl = new Smartlaunch.Api.Client.Smartlaunch();

            var users = sl.GetUsers();

            var user = sl.GetUser("bromleyi");

            double money = ((5.00 / 60) * 3) * -1;
            sl.AddMoney("bromleyi", 9.99);


            var sql = new MySqlHelper();
            sql.SetUserPassword("bromleyi", "apple");
            //var user = sql.GetUser("bromleyi");

            //var ex = new ExcelImporter();
            //var users = ex.ImportUsers();

            //var ss = sl.CreateUser(new User()
            //{
            //    UserName = users[0].User.UserId.ToString(),
            //    UsergroupName = "Members"
            //});

            //var tt = sl.UpdateUser(users[0].User.UserId.ToString(), new ExtendedUser()
            //{
            //    FirstName = users[0].FirstName,
            //    LastName = users[0].LastName,
            //    Birthday = users[0].Birthday,

            //});

        }


    }
}
