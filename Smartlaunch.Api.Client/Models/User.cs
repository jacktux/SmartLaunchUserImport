using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartlaunch.Api.Client.Models
{

    public enum Gender { Female = 0, Male = 1 }

    public class User
    {
        public string UserName { get; set; }
        public string UsergroupName { get; set; }
        public int UserId { get; set; }
    }



    public class ExtendedUser
    {
        private DateTime _birthdate;

        public User User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Birthday { get; set; }
        //{
        //    get
        //    {
        //        if (_birthdate > new DateTime(1900,1,1))
        //            return GetBirthday();
        //        else
        //            return Birthday;
        //    }
        //    set
        //    {
        //        Birthday = value;
        //    }
        //}
        public string Address { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string MobilePhone { get; set; }
        public Gender Sex { get; set; }
        public string PersonalNumber { get; set; }
        public string PasswordHash { get; set; }

        public double Balance { get; set; }

        public void SetBirthday(DateTime dob)
        {
            _birthdate = dob;
        }
        public double GetBirthday()
        {
            return (Double)(_birthdate.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }

}
