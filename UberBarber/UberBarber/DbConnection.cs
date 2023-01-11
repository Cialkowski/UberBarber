using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace UberBarber
{
    class DbConnection
    {
        private MySqlConnection _connection;
        private string _server;
        private string _database;
        private string _uid;
        private string _password;

        public DbConnection()
        {
            initialize();
        }

        private void initialize()
        {
            _server = "sql88.lh.pl";
            _database = "serwer165956_projektstudia";
            _uid = "serwer165956_projektstudia";
            _password = "Abc1234!";
            string connection_string = "SERVER=" + _server + ";" + "DATABASE=" +
                                       _database + ";" + "UID=" + _uid + ";" + "PASSWORD=" + _password + ";";

            _connection = new MySqlConnection(connection_string);
        }

        private bool open_connection()
        {
            try
            {
                _connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    // mysql connection error number
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
                
            }
        }

        private bool close_connection()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void given_query()
        {
            //TODO
        }
    }
}
