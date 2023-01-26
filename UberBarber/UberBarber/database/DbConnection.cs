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
        internal MySqlConnection _connection = connection();
        private MySqlDataReader? _reader;

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

        public void Open_connection()
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

        public void Close_connection()
        {
            _connection.Close();
        }

        public void Read_simple(MySqlCommand query, string message)
        {
            try
            {
                _reader = query.ExecuteReader();

                if (_reader.Read())
                {
                    _connection.Close();
                }
                else
                {
                    MessageBox.Show(message);
                }

            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.ToString());
                _connection.Close();
            }
        }

        public void Read_simple(MySqlCommand query, string good_message, string wrong_message)
        {
            try
            {
                _reader = query.ExecuteReader();
                MessageBox.Show(good_message);
            }
            catch (MySqlException e)
            {
                MessageBox.Show(wrong_message);
                MessageBox.Show(e.ToString());
                _connection.Close();
            }
        }


    }
}
