using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberBarber.Barber
{
    public class Barber
    {
        public Barber(MySqlDataReader reader)
        {
            BarberId = (int)reader["barber_id"];
            Name = (string)reader["name"];
            Title = (string)reader["title"];
            PhoneNumber = (string)reader["phone_number"];
            Age = (int)reader["age"];
            UserId = (int)reader["user_id"];
        }
        public int BarberId { get; set; }   
        public string Name { get; set; }
        public string Title { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public int UserId { get; set; }
    }
}
