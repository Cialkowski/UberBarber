using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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
        internal MySqlConnection _connection = Connection();
        internal MySqlDataReader? _reader;

        public static MySqlConnection Connection()
        {
            // This method creates database connection.
            // Returns appropriate MySqlConnection element.

            string _server = "sql88.lh.pl";
            string _port = "3306";
            string _database = "serwer165956_projektstudia";
            string _uid = "serwer165956_projektstudia";
            string _password = "Abcd123!";
            string connection_string = $"server={_server};port={_port};uid={_uid};pwd={_password};database={_database};";

            MySqlConnection connection = new(connection_string);
            return connection;
        }

        public void Open_connection()
        {
            // This method opens MySqlConnection and informs about server problems if they appear.

            try
            {
                _connection.Open();
            }
            catch (MySqlException)
            {
                MessageBox.Show("Cannot connect to server. Contact administrator");
            }
        }

        public void Close_connection()
        {
            // This method closes MySqlConnection.

            _connection.Close();
        }

    }
}
