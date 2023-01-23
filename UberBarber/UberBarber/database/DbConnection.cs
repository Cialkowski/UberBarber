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
        internal MySqlConnection _connection;
        private string _server;
        private string _port;
        private string _database;
        private string _uid;
        private string _password;

        public DbConnection()
        {
            //initialize constructor
            initialize();
        }

        private void initialize()
        {
            //declare constructor
            _server = "sql88.lh.pl";
            _port = "3306";
            _database = "serwer165956_projektstudia";
            _uid = "serwer165956_projektstudia";
            _password = "Abcd123!";
            string connection_string = "server=" + _server + ";" + "port=" + _port + ";" + "uid=" +
                                       _uid + ";" + "pwd=" + _password + ";" + "database=" + _database + ";";

            _connection = new MySqlConnection(connection_string);
        }
    }
}
