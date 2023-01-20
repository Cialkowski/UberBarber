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
        private MySqlConnection _connection;
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
            _password = "Abc1234!";
            string connection_string = "server=" + _server + ";" + "port=" + _port + ";" + "uid=" +
                                       _uid + ";" + "pwd=" + _password + ";" + "database=" + _database + ";";

            _connection = new MySqlConnection(connection_string);
        }

        internal bool open_connection()
        /// <summary> This function opens connection to server and databse. </summary>>
        /// <returns> True if authentication is successful, otherwise False. </returns>
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

        internal bool close_connection()
        /// <summary> This function closes connection to server and databse. </summary>>
        /// <returns> True if is successful, otherwise False with message for the user. </returns>
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

    }
}
