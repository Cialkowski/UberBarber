using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace UberBarber.User
{
    public class User
    {
        public User(MySqlDataReader reader)
        {
            username = (string)reader["username"];
            password = (string)reader["password"];
            is_worker = (bool)reader["is_worker"];
            customer_id = (int)reader["customer_id"];
        }
        public string username { get; set; }
        public string password { get; set; }
        public bool is_worker { get; set; }
        public int customer_id { get; set;}
    }
}
