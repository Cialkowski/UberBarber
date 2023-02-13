using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberBarber.Customer
{ 
    class Customer
    {
        public Customer(MySqlDataReader reader)
        {
            Customer_id = (int)reader["customer_id"];
            Customer_name = (string)reader["name"];
            Customer_surrname = (string)reader["surrname"];
            Phone_number = (string)reader["phone_number"];
            Date = (DateTime)reader["date"];
            Service_id = (int)reader["service_id"];
            Barber_id = (int)reader["barber_id"];
            Description = (string)reader["description"];

        }

        public int Customer_id { get; set; }
        public string Customer_name { get; set; }
        public string Customer_surrname { get; set; }
        public string Phone_number { get; set; }
        public DateTime Date { get; set; }
        public int Service_id { get; set;}
        public int Barber_id { get;set; }
        public string Description { get; set; }
    }

}
