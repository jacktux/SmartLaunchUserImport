using MySql.Data.MySqlClient;
using Smartlaunch.Api.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLaunch.Api.Client.Helpers
{
    public class MySqlHelper
    {
        MySqlConnection conn = null;
        MySqlDataReader rdr = null;

        public MySqlHelper()
        {
            string cs = @"server=192.168.1.181;userid=smartlaunch;
            password=Fp3i9C;database=smartlaunch";

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }
        }

        public bool IsValidUser(string username, string password)
        {
            string stm = @"SELECT * FROM Users WHERE Username='" + username + "'";

            MySqlCommand cmd = new MySqlCommand(stm, conn);
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.GetString(2) == GetPasswordHash(password))
                    return true;
            }

            return false;
        }

        //public ExtendedUser GetUser(string username)
        //{
        //    ExtendedUser user = new ExtendedUser();
        //    string stm = @"SELECT * FROM Users WHERE Username='" + username + "'";

        //    MySqlCommand cmd = new MySqlCommand(stm, conn);
        //    rdr = cmd.ExecuteReader();

        //    while (rdr.Read())
        //    {
        //        user.FirstName = rdr.GetString(19);
        //        user.PasswordHash = rdr.GetString(2);
        //    }

        //    return user;
        //}

        public void SetUserPassword(string username, string password)
        {
            string sql = @"UPDATE Users
                           SET PasswordHash = @PasswordHash
                           WHERE Username = @Username";
            var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@PasswordHash", GetPasswordHash(password));
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.ExecuteNonQuery();
        }

        private string GetPasswordHash(string password)
        {
            var enc = Encoding.UTF8;

            var SHA1Hasher = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            var hash = Convert.ToBase64String(SHA1Hasher.ComputeHash(enc.GetBytes(password)));

            return hash;
        }

    }
}
