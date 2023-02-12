using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberBarber.Service
{
    public class Service
    {
        public Service(MySqlDataReader reader)
        {
            ServiceId = (int)reader["service_id"];
            Name = (string)reader["name"];
            Time = (decimal)reader["time"];
            Price = (decimal)reader["price"];
            BarberId = (int)reader["barber_id"];
            Description = (string)reader["description"];
        }
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public decimal Time { get; set; }
        public decimal Price { get; set; }
        public int BarberId { get; set; }
        public string Description { get; set; }
    }
}
