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

            //var users = sl.GetUsers();

            //var user = sl.GetUser("bromleyi");

            // double money = ((5.00 / 60) * 3) * -1;
            //sl.AddMoney("bromleyi", 9.99);


            var sql = new MySqlHelper();
            //sql.SetUserPassword("wdadmin", "Fp3i9C");
            //var user = sql.GetUser("bromleyi");

            var ex = new ExcelImporter();
            var users1 = ex.ImportUsers();
            foreach (ExtendedUser user1 in users1)
            {
                // var ss = sl.CreateUser(new User()
                // {
                //     UserName = user1.PersonalNumber.ToString(),
                //     UsergroupName = "Members"
                // });

                // var tt = sl.UpdateUser(user1.PersonalNumber.ToString(), new ExtendedUser()
                // {
                //     FirstName = user1.FirstName,
                //     LastName = user1.LastName,
                //    Birthday = user1.Birthday,


                //});

                //sl.AddMoney(user1.PersonalNumber.ToString(), 5.00);
                //   sql.SetUserPassword(user1.PersonalNumber.ToString(), user1.Password);
                var user21 =sl.GetUser(user1.PersonalNumber);
                var bal = user21.Balance;
                if (bal<5)
                {
                    bal = 5 - bal;
                    sl.AddMoney(user1.PersonalNumber.ToString(), bal);
                    Console.Out.WriteLine("user" + user1.PersonalNumber.ToString() + " : " + bal);
                }
            }
        }


    }
}
