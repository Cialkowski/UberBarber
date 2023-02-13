using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace UberBarber.Appointments
{
    class Appointments
    {
        public Appointments(MySqlDataReader reader)
        {
            Barber_name = (string)reader["barber"];
            Service_name = (string)reader["usluga"];
            Visit_date = (DateTime)reader["date"];
            User_id = (int)reader["user_id"];
        }

        public string Barber_name { get; set; }
        public string Service_name { get; set; }
        public DateTime Visit_date { get; set; }
        public int User_id { get; set; }
    }

}
