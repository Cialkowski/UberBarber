using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace UberBarber
{
    class DbConnection
    {
        internal MySqlConnection _connection = connection();

        public DbConnection()
        {
            //initialize constructor
        }

        public static MySqlConnection connection()
        {
            //create connection
            string _server = "sql88.lh.pl";
            string _port = "3306";
            string _database = "serwer165956_projektstudia";
            string _uid = "serwer165956_projektstudia";
            string _password = "Abcd123!";
            string connection_string = "server=" + _server + ";" + "port=" + _port + ";" + "uid=" +
                                       _uid + ";" + "pwd=" + _password + ";" + "database=" + _database + ";";

            MySqlConnection connection = new(connection_string);
            return connection;
        }

        public void open_connection()
        {
            try
            {
                _connection.Open();
            }
            catch (MySqlException)
            {
                MessageBox.Show("Cannot connect to server. Contact administrator");
            }
        }
    }
}
